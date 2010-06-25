using System;
using System.Collections.Generic;
using System.Text;
using Mogre;

namespace MQuickGUI
{
    struct QuickGUIVertex
	{
		public Vector3 pos;
        public uint color;
        public Vector2 uv;
	}


    public class QuickGUIQuad
    {
        public const int VERTEX_PER_QUAD = 4;

		private GUIManager	mGUIManager;
		private Widget		mOwner;

		// Name of Widget
		private string		mID;
		private string		mTextureName;
		private int			mZOrder;

		private bool		mAddedToRenderQueue;
		private bool		mDimensionsChanged;
		private bool		mTextureChanged;
		private bool		mZOrderChanged;

		private QuickGUIVertex[]	mVertices = new QuickGUIVertex[VERTEX_PER_QUAD];
		
        private float		mXTexelOffset;
		private float		mYTexelOffset;
	
		public QuickGUIQuad(Widget owner) {
            mOwner = owner;
		    mDimensionsChanged = true;
		    mZOrderChanged = true;
		    mAddedToRenderQueue = false;

            mID = mOwner.getInstanceName();
		    //mTextureName = mOwner->getTextureName();
		    mTextureName = "qgui.logo.jpg";
		    mGUIManager = GUIManager.Singleton;

		    RenderSystem rs = Root.Singleton.RenderSystem;
		    mXTexelOffset = rs.HorizontalTexelOffset;
		    mYTexelOffset = rs.VerticalTexelOffset;

		    _computeVertices();
		    if(!string.Empty.Equals(mTextureName))
                notifyRenderQueue();
        }

		// Allows the renderer to notify the quad its changes were accepted.
		internal void _notifyChangesHandled()
        {
            mDimensionsChanged = false;
            mTextureChanged = false;
            mZOrderChanged = false;
        }

		void _notifyDimensionsChanged()
        {
            mDimensionsChanged = true;
            _computeVertices();
            notifyRenderQueue();
        }

        void _notifyTextureChanged()
        {
		    mTextureChanged = true;
		    notifyRenderQueue();
	    }

		void _notifyZOrderChanged()
        {
            mZOrderChanged = true;
            notifyRenderQueue();
        }

		public string getID()
        {
            return mID;
        }

        internal string getTextureName()
        {
            return mTextureName;
        }

        internal QuickGUIVertex getVertex(int index)
		{
		    if( index >= QuickGUIQuad.VERTEX_PER_QUAD )
                return new QuickGUIVertex();
		    return mVertices[index];
	    }

        internal int getZOrder()
        {
            return mZOrder;
        }

		bool dimensionsChanged()
        {
            return mDimensionsChanged;
        }

		// Adds quad to render queue, or if already added, updates it.
        void notifyRenderQueue()
	    {
        	/*MAT
            QuickGUIRenderer r = QuickGUIRenderer.Singleton;

		    if(mAddedToRenderQueue)
                r.updateQuadInRenderList(this);
		    else 
		    {
			    r.addQuadToRenderList(this);
			    mAddedToRenderQueue = true;
		    }
		    */
	    }

		// used to update a vertex
        internal void setVertex(int index, QuickGUIVertex v)
        {
            if (index >= VERTEX_PER_QUAD)
                return;

            mVertices[index].pos = v.pos;
            mVertices[index].color = v.color;
            mVertices[index].uv = v.uv;
        }

        internal bool textureChanged()
        {
            return mTextureChanged;
        }

        internal bool zOrderChanged()
        {
            return mZOrderChanged;
        }

		private void _computeVertices()
        {
		    // Setup the coordinates for the quad
		    Vector4 absDim = mOwner.getDimensions(QGuiMetricsMode.QGUI_GMM_ABSOLUTE,QGuiMetricsMode.QGUI_GMM_ABSOLUTE);
		    //Ogre::Vector4 absDim = mOwner->getDimensions(QGUI_GMM_PIXELS,QGUI_GMM_PIXELS);

		    int windowWidth = mGUIManager.getRenderWindowWidth();
		    int windowHeight = mGUIManager.getRenderWindowHeight();

		    /* Convert positions into -1, 1 coordinate space (homogenous clip space).
			    - Left / right is simple range conversion
			    - Top / bottom also need inverting since y is upside down - this means
			    that top will end up greater than bottom and when computing texture
			    coordinates, we have to flip the v-axis (ie. subtract the value from
			    1.0 to get the actual correct value).
		    */
		    float left = (absDim.x * 2) - 1;
		    float right = left + (absDim.z * 2);
		    float top = -((absDim.y * 2) - 1);
		    float bottom = top - (absDim.w * 2);

            mVertices[0].pos = new Vector3(left, top, 0);
            mVertices[0].uv = new Vector2(0, 0);

            mVertices[0].pos = new Vector3(left, bottom, 0);
            mVertices[0].uv = new Vector2(0, 1.0f);

            mVertices[0].pos = new Vector3(right, top, 0);
            mVertices[0].uv = new Vector2(1.0f, 0);

            mVertices[0].pos = new Vector3(right, bottom, 0);
		    mVertices[0].uv = new Vector2(1.0f,1.0f);

    /*
		    // TRIANGLE 1
		    mVertices[0].pos = Ogre::Vector3(left,bottom,0);	// left-bottom
		    mVertices[0].uv = Ogre::Vector2(0.0,1.0);

		    mVertices[1].pos = Ogre::Vector3(right,bottom,0);	// right-bottom
		    mVertices[1].uv = Ogre::Vector2(1.0,1.0);

		    mVertices[2].pos = Ogre::Vector3(left,top,0);		// left-top
		    mVertices[2].uv = Ogre::Vector2(0.0,0.0);

		    // TRIANGLE 2
		    mVertices[3].pos = Ogre::Vector3(right,bottom,0);	// right-bottom
		    mVertices[3].uv = Ogre::Vector2(1.0,1.0);

		    mVertices[4].pos = Ogre::Vector3(right,top,0);		// right-top
		    mVertices[4].uv = Ogre::Vector2(1.0,0.0);

		    mVertices[5].pos = Ogre::Vector3(left,top,0);		// left-top
		    mVertices[5].uv = Ogre::Vector2(0.0,0.0);
    */

        }

    }
}
