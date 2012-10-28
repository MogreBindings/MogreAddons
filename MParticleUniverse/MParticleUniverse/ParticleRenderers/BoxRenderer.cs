using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// The BoxRenderer class is responsible to render particles as a box.
    /// </summary>
    public unsafe class BoxRenderer : ParticleRenderer, IDisposable 
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, BoxRenderer> rendererInstances;

        internal static BoxRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, BoxRenderer>();

            BoxRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BoxRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal BoxRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public BoxRenderer()
            : base(BoxRenderer_New())
        {
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }

        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BoxRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BoxRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_updateRenderQueue */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            BoxRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyAttached */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            BoxRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            BoxRenderer__setMaterialName(nativePtr, materialName);
        }

        /** @copydoc ParticleRenderer::_notifyCurrentCamera */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            BoxRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyParticleQuota */
        public void _notifyParticleQuota(uint quota)
        {
            BoxRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** @copydoc ParticleRenderer::_notifyDefaultDimensions */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            BoxRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** @copydoc ParticleRenderer::_notifyParticleResized */
        public void _notifyParticleResized()
        {
            BoxRenderer__notifyParticleResized(nativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyParticleZRotated */
        public void _notifyParticleZRotated()
        {
            BoxRenderer__notifyParticleZRotated(nativePtr);
        }

        /** @copydoc ParticleRenderer::setRenderQueueGroup */
        public void SetRenderQueueGroup(byte queueId)
        {
            BoxRenderer_SetRenderQueueGroup(nativePtr, queueId);
        }

        /** @copydoc ParticleRenderer::_getSortMode */
        public Mogre.SortMode _getSortMode()
        {
            return BoxRenderer__GetSortMode(nativePtr);
        }

        /** @copydoc ParticleRenderer::copyAttributesTo */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            BoxRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }

        /** Access BoxSet in use */
        public BoxSet GetBoxSet()
        {
            return BoxSet.GetInstance(BoxRenderer_GetBoxSet(nativePtr));
        }

        /** @copydoc ParticleRenderer::setVisible */
        public void SetVisible(bool visible)
        {
            BoxRenderer_SetVisible(nativePtr, visible);
        }

        #region BoxRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode BoxRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxRenderer_SetVisible(IntPtr ptr, bool visible);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxRenderer_GetBoxSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxRenderer_GetBoxSet(IntPtr ptr);
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
            BoxRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
