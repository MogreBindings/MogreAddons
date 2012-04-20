using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// Visual data specific for this type of renderer.
    /// </summary>
    public unsafe class RibbonTrailRenderer : ParticleRenderer, IDisposable 
    {

        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, RibbonTrailRenderer> rendererInstances;

        internal static RibbonTrailRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, RibbonTrailRenderer>();

            RibbonTrailRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new RibbonTrailRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal RibbonTrailRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const bool DEFAULT_USE_VERTEX_COLOURS = true;
        public const uint DEFAULT_MAX_ELEMENTS = 10;
        public const float DEFAULT_LENGTH = 400;
        public const float DEFAULT_WIDTH = 5;
        public const bool DEFAULT_RANDOM_INITIAL_COLOUR = true;
        public static Mogre.ColourValue DEFAULT_INITIAL_COLOUR { get { return new Mogre.ColourValue(1, 1, 1); } }
        public static Mogre.ColourValue DEFAULT_COLOUR_CHANGE { get { return  new Mogre.ColourValue(0.5f, 0.5f, 0.5f, 0.5f);}}

        public RibbonTrailRenderer()
            : base(RibbonTrailRenderer_New())
        {
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }


        /** Notify that the Particle System is rescaled.
        */
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            RibbonTrailRenderer__notifyRescaled(nativePtr, scale);
        }

        /** Getters and Setters
        */
        bool IsUseVertexColours { get { return RibbonTrailRenderer_IsUseVertexColours(nativePtr); } set { RibbonTrailRenderer_SetUseVertexColours(nativePtr, value); } }

        uint MaxChainElements { get { return RibbonTrailRenderer_GetMaxChainElements(nativePtr); } set { RibbonTrailRenderer_SetMaxChainElements(nativePtr, value); } }

        float TrailLength { get { return RibbonTrailRenderer_GetTrailLength(nativePtr); } set { RibbonTrailRenderer_SetTrailLength(nativePtr, value); } }

        float TrailWidth { get { return RibbonTrailRenderer_GetTrailWidth(nativePtr); } set { RibbonTrailRenderer_SetTrailWidth(nativePtr, value); } }

        bool IsRandomInitialColour { get { return RibbonTrailRenderer_IsRandomInitialColour(nativePtr); } set { RibbonTrailRenderer_SetRandomInitialColour(nativePtr, value); } }

        Mogre.ColourValue InitialColour
        {
            get
            {
                IntPtr ic = RibbonTrailRenderer_GetInitialColour(nativePtr);
                //if (ic == null || ic == IntPtr.Zero)
                //    return null;
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)ic.ToPointer()));
                return vec;
            }
            set
            {
                if (value == null)
                    RibbonTrailRenderer_SetInitialColour(nativePtr, IntPtr.Zero);
                else
                {
                    IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                    Marshal.StructureToPtr(value, colourPtr, true);
                    RibbonTrailRenderer_SetInitialColour(nativePtr, colourPtr);
                }
            }
        }

        Mogre.ColourValue ColourChange
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)RibbonTrailRenderer_GetColourChange(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                if (value == null)
                    RibbonTrailRenderer_SetColourChange(nativePtr, IntPtr.Zero);
                else
                {
                    IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                    Marshal.StructureToPtr(value, colourPtr, true);
                    RibbonTrailRenderer_SetColourChange(nativePtr, colourPtr);
                }
            }
        }

        /** Deletes all ChildSceneNodes en Entities.
        */
        public void _destroyAll()
        {
            RibbonTrailRenderer__destroyAll(nativePtr);
        }

        /** Enable the RibbonTrail
        */
        //			virtual void _notifyStart(void);

        /** Disable the RibbonTrail
        */
        //			virtual void _notifyStop(void);

        /** Make all objects visible or invisible.
        */
        public void SetVisible(bool visible)
        {
            RibbonTrailRenderer_SetVisible(nativePtr, visible);
        }

        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            RibbonTrailRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            RibbonTrailRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** 
        */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            RibbonTrailRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** 
        */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            RibbonTrailRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            RibbonTrailRenderer__setMaterialName(nativePtr, materialName);
        }

        /** 
        */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            RibbonTrailRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** 
        */
        public void _notifyParticleQuota(uint quota)
        {
            RibbonTrailRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** 
        */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            RibbonTrailRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** 
        */
        public void _notifyParticleResized()
        {
            RibbonTrailRenderer__notifyParticleResized(nativePtr);
        }

        /** 
        */
        public void _notifyParticleZRotated()
        {
            RibbonTrailRenderer__notifyParticleZRotated(nativePtr);
        }

        /** 
        */
        public void SetRenderQueueGroup(byte queueId)
        {
            RibbonTrailRenderer_SetRenderQueueGroup(nativePtr, queueId);
        }

        /** 
        */
        public Mogre.SortMode _getSortMode()
        {
            return RibbonTrailRenderer__GetSortMode(nativePtr);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            RibbonTrailRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }

        /*  See TechniqueListener.
        */
        public void ParticleEmitted(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            RibbonTrailRenderer_ParticleEmitted(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }

        /*  See TechniqueListener.
        */
        public void ParticleExpired(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            RibbonTrailRenderer_ParticleExpired(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }


        #region RibbonTrailRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr RibbonTrailRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_IsUseVertexColours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool RibbonTrailRenderer_IsUseVertexColours(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetUseVertexColours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetUseVertexColours(IntPtr ptr, bool useVertexColours);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_GetMaxChainElements", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint RibbonTrailRenderer_GetMaxChainElements(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetMaxChainElements", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetMaxChainElements(IntPtr ptr, uint maxChainElements);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_GetTrailLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float RibbonTrailRenderer_GetTrailLength(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetTrailLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetTrailLength(IntPtr ptr, float trailLength);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_GetTrailWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float RibbonTrailRenderer_GetTrailWidth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetTrailWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetTrailWidth(IntPtr ptr, float trailWidth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_IsRandomInitialColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool RibbonTrailRenderer_IsRandomInitialColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetRandomInitialColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetRandomInitialColour(IntPtr ptr, bool randomInitialColour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_GetInitialColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr RibbonTrailRenderer_GetInitialColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetInitialColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetInitialColour(IntPtr ptr, IntPtr initialColour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_GetColourChange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr RibbonTrailRenderer_GetColourChange(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetColourChange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetColourChange(IntPtr ptr, IntPtr colourChange);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__destroyAll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__destroyAll(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetVisible(IntPtr ptr, bool visible);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode RibbonTrailRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_ParticleEmitted", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_ParticleEmitted(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "RibbonTrailRenderer_ParticleExpired", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void RibbonTrailRenderer_ParticleExpired(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
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
            RibbonTrailRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
