using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    public unsafe class BeamRenderer : ParticleRenderer, TechniqueListener, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, BeamRenderer> rendererInstances;

        internal static BeamRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, BeamRenderer>();

            BeamRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BeamRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal BeamRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const bool DEFAULT_USE_VERTEX_COLOURS = false;
        public const uint DEFAULT_MAX_ELEMENTS = 10;
        public const float DEFAULT_UPDATE_INTERVAL = 0.1f;
        public const float DEFAULT_DEVIATION = 300;
        public const uint DEFAULT_NUMBER_OF_SEGMENTS = 2;
        public const Mogre.BillboardChain.TexCoordDirection DEFAULT_TEXTURE_DIRECTION = Mogre.BillboardChain.TexCoordDirection.TCD_V;


        public BeamRenderer()
            : base(BeamRenderer_New())
        { 
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }

        /** Getters and Setters
        */
        public bool UseVertexColours { get { return BeamRenderer_IsUseVertexColours(nativePtr); } set { BeamRenderer_SetUseVertexColours(nativePtr, value); } }

        public uint MaxChainElements { get { return BeamRenderer_GetMaxChainElements(nativePtr); } set { BeamRenderer_SetMaxChainElements(nativePtr, value); } }

        public float UpdateInterval { get { return BeamRenderer_GetUpdateInterval(nativePtr); } set { BeamRenderer_SetUpdateInterval(nativePtr, value); } }

        public float Deviation { get { return BeamRenderer_GetDeviation(nativePtr); } set { BeamRenderer_SetDeviation(nativePtr, value); } }

        public uint NumberOfSegments { get { return BeamRenderer_GetNumberOfSegments(nativePtr); } set { BeamRenderer_SetNumberOfSegments(nativePtr, value); } }

        public bool Jump { get { return BeamRenderer_IsJump(nativePtr); } set { BeamRenderer_SetJump(nativePtr, value); } }

        public Mogre.BillboardChain.TexCoordDirection TexCoordDirection { get { return BeamRenderer_GetTexCoordDirection(nativePtr); } set { BeamRenderer_SetTexCoordDirection(nativePtr, value); } }

        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BeamRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BeamRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** Destroys the BillboarChain
        */
        public void _destroyAll()
        {
            BeamRenderer__destroyAll(nativePtr);
        }

        /** 
        */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            BeamRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** See ParticleRenderer
        */
        public void _processParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed,
            bool firstParticle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BeamRenderer__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        /** 
        */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            BeamRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            BeamRenderer__setMaterialName(nativePtr, materialName);
        }

        /** 
        */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            BeamRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** 
        */
        public void _notifyParticleQuota(uint quota)
        {
            BeamRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** 
        */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            BeamRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** 
        */
        public void _notifyParticleResized()
        {
            BeamRenderer__notifyParticleResized(nativePtr);
        }

        /** 
        */
        public void _notifyParticleZRotated()
        {
            BeamRenderer__notifyParticleZRotated(nativePtr);
        }

        /** 
        */
        public void SetRenderQueueGroup(byte queueId)
        {
            BeamRenderer_SetRenderQueueGroup(nativePtr, queueId);
        }

        /** @copydoc ParticleRenderer::_getSortMode */
        public Mogre.SortMode _getSortMode()
        {
            return BeamRenderer__GetSortMode(nativePtr);
        }

        /** @copydoc ParticleRenderer::setVisible */
        public void SetVisible(bool visible)
        {
            BeamRenderer_SetVisible(nativePtr, visible);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            BeamRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }

        /*  See TechniqueListener.
        */
        public void ParticleEmitted(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BeamRenderer_ParticleEmitted(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }

        /*  See TechniqueListener.
        */
        public void ParticleExpired(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BeamRenderer_ParticleExpired(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }

        #region BeamRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BeamRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_IsUseVertexColours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BeamRenderer_IsUseVertexColours(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetUseVertexColours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetUseVertexColours(IntPtr ptr, bool useVertexColours);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_GetMaxChainElements", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint BeamRenderer_GetMaxChainElements(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetMaxChainElements", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetMaxChainElements(IntPtr ptr, uint maxChainElements);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_GetUpdateInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BeamRenderer_GetUpdateInterval(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetUpdateInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetUpdateInterval(IntPtr ptr, float updateInterval);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_GetDeviation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BeamRenderer_GetDeviation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetDeviation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetDeviation(IntPtr ptr, float deviation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_GetNumberOfSegments", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint BeamRenderer_GetNumberOfSegments(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetNumberOfSegments", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetNumberOfSegments(IntPtr ptr, uint numberOfSegments);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_IsJump", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BeamRenderer_IsJump(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetJump", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetJump(IntPtr ptr, bool jump);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_GetTexCoordDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.BillboardChain.TexCoordDirection BeamRenderer_GetTexCoordDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetTexCoordDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetTexCoordDirection(IntPtr ptr, Mogre.BillboardChain.TexCoordDirection texCoordDirection);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__destroyAll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__destroyAll(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__processParticle(IntPtr ptr, IntPtr particleTechnique,
                        IntPtr particle,
                        float timeElapsed,
                        bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode BeamRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_SetVisible(IntPtr ptr, bool visible);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_ParticleEmitted", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_ParticleEmitted(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BeamRenderer_ParticleExpired", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BeamRenderer_ParticleExpired(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        #endregion


        #region IDispose Stuff
        /// <summary>Occurs when the manager is being disposed.</summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                lock (this)
                {

                    if (Disposed != null)
                    {
                        Disposed(this, EventArgs.Empty);
                    }

                }
            }
            BeamRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
