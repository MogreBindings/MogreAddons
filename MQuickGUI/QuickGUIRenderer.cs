using System;
using System.Collections.Generic;
using Mogre;
using System.Text;

namespace MQuickGUI
{
    public class QuickGUIRenderer
    {
        private static QuickGUIRenderer instance = null;

        // Number of Vertices the buffer holds initially. (512 quads)
        const int BUFFER_SIZE_INITIAL = 2048;

        private RenderSystem mRenderSystem;

        // HARDWARE VERTEX BUFFER - variables
        private HardwareVertexBufferSharedPtr mVertexBuffer;
        private int mVertexBufferSize;	// the maximum number of vertices the buffer can hold.
        private int mVertexBufferUsage;	// the current number of vertices stored in the buffer.
        unsafe private QuickGUIVertex* mVertexBufferPtr;	// pointer used to iterate through vertices in the buffer. NULL when buffer unlocked. (must be locked to work with) // GDZ

        // Every RenderSystem::_render(mRenderOperation) constitutes a batch.
        private RenderOperation mRenderOperation;

        private Vector2 mTexelOffset; //holds the pixel level texel offset that needs to be applied to each vertex to maintain pixel alignment

        //Cache Data
        private TextureUnitState.UVWAddressingMode mTextureAddressMode; //we cache this to save cpu time
        private LayerBlendModeEx_NativePtr mColorBlendMode; //we cache this to save cpu time
        private LayerBlendModeEx_NativePtr mAlphaBlendMode; //we cache this to save cpu time
        private Vector2 mViewportPixelShift; // holds the pixel shift necessary for correcting pixel alignment in the current render context (value assumes 0.0 to 1.0 viewport size)

        private RenderQueueGroupID mQueueID;		//!< ID of the queue that we are hooked into
        private List<QuickGUIQuad> mRenderList;
        // if a quad was moved/removed, the Hardware buffer needs to be repopulated.
        private bool mRenderListDirty;

        public QuickGUIRenderer()
        {
            mQueueID = RenderQueueGroupID.RENDER_QUEUE_OVERLAY;
            unsafe
            {
                mVertexBufferPtr = null;
            }
            mRenderListDirty = false;

            mRenderSystem = Root.Singleton.RenderSystem;

            mRenderOperation = new RenderOperation();

            mRenderList = new List<QuickGUIQuad>();

            mTexelOffset.x = mRenderSystem.HorizontalTexelOffset;
            mTexelOffset.y = mRenderSystem.VerticalTexelOffset;

            // Initialise blending modes to be used. We use these every frame, so we'll set them up now to save time later.

            mColorBlendMode = LayerBlendModeEx_NativePtr.Create();

            mColorBlendMode.blendType = LayerBlendType.LBT_COLOUR;
            mColorBlendMode.source1 = LayerBlendSource.LBS_TEXTURE;
            mColorBlendMode.source2 = LayerBlendSource.LBS_DIFFUSE;
            mColorBlendMode.operation = LayerBlendOperationEx.LBX_MODULATE;

            mAlphaBlendMode = LayerBlendModeEx_NativePtr.Create();

            mAlphaBlendMode.blendType = LayerBlendType.LBT_ALPHA;
            mAlphaBlendMode.source1 = LayerBlendSource.LBS_TEXTURE;
            mAlphaBlendMode.source2 = LayerBlendSource.LBS_DIFFUSE;
            mAlphaBlendMode.operation = LayerBlendOperationEx.LBX_MODULATE;

            mTextureAddressMode.u = TextureUnitState.TextureAddressingMode.TAM_CLAMP;
            mTextureAddressMode.v = TextureUnitState.TextureAddressingMode.TAM_CLAMP;
            mTextureAddressMode.w = TextureUnitState.TextureAddressingMode.TAM_CLAMP;

            // set up buffer and tracking variables
            _createVertexBuffer(BUFFER_SIZE_INITIAL);

// For now, assume the first scene manager instance is the one in use.
// TODO: reparar esto
//Root.Singleton.GetSceneManagerIterator().Current.RenderQueueStarted += new RenderQueueListener.RenderQueueStartedHandler(Current_RenderQueueStarted);
        }

        ~QuickGUIRenderer()
        {
            _destroyVertexBuffer();
        }

		// HARDWARE VERTEX BUFFER - functions
        // NOTE: we do not need a _clearVertexBuffer function, since we get new, blank memory when locking with HBL_DISCARD.
        private void _createVertexBuffer(int numberOfVertices)
        {
		    mVertexBufferSize = numberOfVertices;

		    mRenderOperation.vertexData = new VertexData();
		    mRenderOperation.vertexData.vertexStart = 0;

		    _declareVertexStructure();

		    // Create the Vertex Buffer, using the Vertex Structure we previously declared in _declareVertexStructure.
		    mVertexBuffer =  HardwareBufferManager.Singleton.CreateVertexBuffer(
			    mRenderOperation.vertexData.vertexDeclaration.GetVertexSize(0), // declared Vertex used
			    (uint)mVertexBufferSize,
			    HardwareBuffer.Usage.HBU_DYNAMIC_WRITE_ONLY_DISCARDABLE,
			    false );

		    // Bind the created buffer to the renderOperation object.  Now we can manipulate the buffer, and the RenderOp keeps the changes.
		    mRenderOperation.vertexData.vertexBufferBinding.SetBinding( 0, mVertexBuffer );
		    mRenderOperation.operationType = RenderOperation.OperationTypes.OT_TRIANGLE_STRIP;
		    mRenderOperation.useIndexes = false;
        }

        /*
        * Define the size and data types that form a *Vertex*, to be used in the VertexBuffer.
        * NOTE: For ease of use, we define a structure QuickGUI::Vertex, with the same exact data types.
        */
        private void _declareVertexStructure()
        {
		    VertexDeclaration vd = mRenderOperation.vertexData.vertexDeclaration;

		    // Add position - Ogre::Vector3 : 4 bytes per float * 3 floats = 12 bytes

		    vd.AddElement( 0, 0, VertexElementType.VET_FLOAT3, VertexElementSemantic.VES_POSITION );

		    // Add color - Ogre::RGBA : 8 bits per channel (1 byte) * 4 channels = 4 bytes

		    vd.AddElement( 0, VertexElement.GetTypeSize( VertexElementType.VET_FLOAT3 ), VertexElementType.VET_FLOAT4, VertexElementSemantic.VES_DIFFUSE );

		    // Add texture coordinates - Ogre::Vector2 : 4 bytes per float * 2 floats = 8 bytes

		    vd.AddElement( 0, VertexElement.GetTypeSize( VertexElementType.VET_FLOAT3 ) +
						       VertexElement.GetTypeSize( VertexElementType.VET_FLOAT4 ),
                               VertexElementType.VET_FLOAT2, VertexElementSemantic.VES_TEXTURE_COORDINATES);

		    /* Our structure representing the Vertices used in the buffer (24 bytes):
			    struct Vertex
			    {
				    Ogre::Vector3 pos;
				    Ogre::RGBA color;
				    Ogre::Vector2 uv;
			    };
		    */

        }

        private void _destroyVertexBuffer()
        {
        	mRenderOperation.vertexData.vertexBufferBinding.UnsetAllBindings();
		    mRenderOperation.vertexData.Dispose();
		    //mRenderOperation.vertexData = null;
		    mVertexBuffer.Dispose();
        }

        private void _populateVertexBuffer()
        {
            unsafe
            {
                // lock the buffer if we haven't already.
                if (mVertexBufferPtr == null)
                {
                    // Note that locking with HBL_DISCARD will give us new, blank memory.
                    mVertexBufferPtr = (QuickGUIVertex*)mVertexBuffer.Lock(HardwareVertexBuffer.LockOptions.HBL_DISCARD);
                }

                int vertexCount = 0;

                foreach (QuickGUIQuad q in mRenderList)
                {
                    // for each quad, add its 6 vertices to the buffer
                    for (int vertIndex = 0; vertIndex < QuickGUIQuad.VERTEX_PER_QUAD; ++vertIndex)
                    {
                        QuickGUIVertex v = q.getVertex(vertIndex);
                        mVertexBufferPtr[vertIndex].pos = v.pos;
                        mRenderSystem.ConvertColourValue(ColourValue.White, out mVertexBufferPtr[vertIndex].color);
                        mVertexBufferPtr[vertIndex].uv = v.uv;

                        ++vertexCount;
                    }

                    mVertexBufferPtr += QuickGUIQuad.VERTEX_PER_QUAD;
                }

                mVertexBuffer.Unlock();
                mVertexBufferPtr = null;

                mVertexBufferUsage = vertexCount;

                mRenderListDirty = false;
            }
        }

        private void _renderVertexBuffer()
        {
		    if( mRenderList.Count==0 || (mVertexBufferUsage == 0) )
                return;
    		
		    uint bufferPosition = 0;

		    /*
		    * Since mRenderList is sorted by zOrder and by Texture, we can send quads with similar textures into one renderOperation.
		    * Everything rendered in one _render call will receive the texture set previously by _setTexture.
		    */
            for (int it=0; it < mRenderList.Count; it++)
            {
                QuickGUIQuad iq = mRenderList[it];
                string currentTexture = iq.getTextureName();
			    mRenderOperation.vertexData.vertexStart = bufferPosition;
			    // Iterate over quads with the same texture.
			    while( (it < mRenderList.Count) && (iq.getTextureName() == currentTexture) )
			    {
				    bufferPosition += QuickGUIQuad.VERTEX_PER_QUAD;
				    ++it;
			    }
			    // render quads
			    mRenderOperation.vertexData.vertexCount = bufferPosition - mRenderOperation.vertexData.vertexStart;
			    mRenderSystem._setTexture(0,true,currentTexture);
			    initRenderState();
			    mRenderSystem._render(mRenderOperation);

			    // with the while loop it is possible to iterate to the end of the list.
			    // At this point the for loop will increment it again, causing a crash.
			    if(it == mRenderList.Count)
                    break;
		    }
        }

        private void _resizeVertexBuffer(int numberOfVertices)
        {
        }
        private void initRenderState()
        {

		    // set-up matrices
		    mRenderSystem._setWorldMatrix(Matrix4.IDENTITY);
		    mRenderSystem._setViewMatrix(Matrix4.IDENTITY);
		    mRenderSystem._setProjectionMatrix(Matrix4.IDENTITY);

		    // initialise render settings
		    mRenderSystem.SetLightingEnabled(false);
		    mRenderSystem._setDepthBufferParams(false, false);
		    mRenderSystem._setDepthBias(0, 0);
		    mRenderSystem._setCullingMode(CullingMode.CULL_NONE);
		    mRenderSystem._setFog(FogMode.FOG_NONE);
		    mRenderSystem._setColourBufferWriteEnabled(true, true, true, true);
		    mRenderSystem.UnbindGpuProgram(GpuProgramType.GPT_FRAGMENT_PROGRAM);
		    mRenderSystem.UnbindGpuProgram(GpuProgramType.GPT_VERTEX_PROGRAM);
		    mRenderSystem.SetShadingType(ShadeOptions.SO_GOURAUD);
		    mRenderSystem._setPolygonMode(PolygonMode.PM_SOLID);

		    // initialise texture settings
		    mRenderSystem._setTextureCoordCalculation(0, TexCoordCalcMethod.TEXCALC_NONE);
		    mRenderSystem._setTextureCoordSet(0, 0);
            mRenderSystem._setTextureUnitFiltering(0, FilterOptions.FO_LINEAR, FilterOptions.FO_LINEAR, FilterOptions.FO_POINT);
		    mRenderSystem._setTextureAddressingMode(0, mTextureAddressMode);
		    mRenderSystem._setTextureMatrix(0, Matrix4.IDENTITY);
		    mRenderSystem._setAlphaRejectSettings( CompareFunction.CMPF_ALWAYS_PASS, 0);
		    mRenderSystem._setTextureBlendMode(0, mColorBlendMode);
		    mRenderSystem._setTextureBlendMode(0, mAlphaBlendMode);
		    mRenderSystem._disableTextureUnitsFrom(1);

		    // enable alpha blending
            mRenderSystem._setSceneBlending(SceneBlendFactor.SBF_SOURCE_ALPHA, SceneBlendFactor.SBF_ONE_MINUS_SOURCE_ALPHA);

        }
        
        void Current_RenderQueueStarted(byte queueGroupId, string invocation, out bool skipThisInvocation)
        {
            if (Convert.ToByte(mQueueID) ==  queueGroupId )
            {
                render();
            }
            skipThisInvocation = false;
        }

		/*
		* Quads are kept sorted by zOrder, and within zOrder, by Texture.  In this way, we will
		* minimized batches.
		*/
		public void addQuadToRenderList(QuickGUIQuad q) {
		    bool added = false;

		    for (int it=0; it<mRenderList.Count; it++)
		    {
                QuickGUIQuad iq = mRenderList[it];
			    // quad is already in the list! update quad instead
			    if( iq.getID() == q.getID() )
			    {
				    updateQuadInRenderList(q);
				    added = true;
				    break;
			    }

			    // zOrders are the same, now we want to group by texture
			    if( iq.getZOrder() == q.getZOrder() )
			    {
				    // iterate until texture name found, or end of zOrder grouping
				    while( !iq.getTextureName().Equals(iq.getTextureName()) && (iq.getZOrder() == q.getZOrder()) && (it < mRenderList.Count) )
					    ++it;

				    // it is pointing to the end of the list, a quad with the same texture name, or a quad of another zOrder
				    mRenderList.Insert(it, q);
				    q._notifyChangesHandled();
				    added = true;
			    }
		    }
    		
		    if(!added)
                mRenderList.Remove(q);

		    mRenderListDirty = true;

        }

		/** Returns the Listener singleton object */
		public static QuickGUIRenderer Singleton {
            get {
                if (instance == null)
                {
                    instance = new QuickGUIRenderer();
                }
                return instance;
            }
        }

		/** Returns a pointer to the Listener singleton object */
		//static Renderer* getSingletonPtr();

        public void removeQuadFromRenderList(QuickGUIQuad q)
        {
            QuickGUIQuad qr = null;
            foreach(QuickGUIQuad iq in mRenderList)
		    {
			    // quad is already in the list! update quad instead
			    if( iq.getID() == q.getID() )
			    {
				    qr = iq;
				    mRenderListDirty = true;
				    break;
			    }
		    }
            if (qr!=null) {
                mRenderList.Remove(qr);
            }
        }

		public void render() 
        {
    		if(mRenderListDirty)
                _populateVertexBuffer();
	        _renderVertexBuffer();
        }

		public void updateQuadInRenderList(QuickGUIQuad q)
        {
	        // minor optimization, where only the quad's dimensions have changed.
	        if(!q.zOrderChanged() && !q.textureChanged())
	        {
                foreach(QuickGUIQuad iq in mRenderList) {
			        if( iq.getID() == q.getID() )
			        {
				        // update all vertices
				        for( int vertIndex = 0; vertIndex < QuickGUIQuad.VERTEX_PER_QUAD; ++vertIndex )
				        {
                            iq.setVertex(vertIndex, q.getVertex(vertIndex));
				        }
                        q._notifyChangesHandled();
				        break;
			        }
		        }
	        }
	        // otherwise its easier/more efficient to remove the quad and re-add it into the list.
	        else
	        {
		        removeQuadFromRenderList(q);
		        addQuadToRenderList(q);
	        }

	        mRenderListDirty = true;
        }

        public void Initialise()
        {
            // For now, assume the first scene manager instance is the one in use.
            Root.Singleton.GetSceneManagerIterator().Current.RenderQueueStarted += new RenderQueueListener.RenderQueueStartedHandler(Current_RenderQueueStarted);
        }
    }
}
