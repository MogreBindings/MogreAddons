using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// The SphereRenderer class is responsible to render particles as a sphere.
    /// </summary>
    public unsafe class SphereRenderer : ParticleRenderer, IDisposable 
    {

        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, SphereRenderer> rendererInstances;

        internal static SphereRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, SphereRenderer>();

            SphereRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SphereRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal SphereRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public SphereRenderer()
            : base(SphereRenderer_New())
        {
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }


        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            SphereRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            SphereRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_updateRenderQueue */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            SphereRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyAttached */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            SphereRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            SphereRenderer__setMaterialName(nativePtr, materialName);
        }

        /** @copydoc ParticleRenderer::_notifyCurrentCamera */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            SphereRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyParticleQuota */
        public void _notifyParticleQuota(uint quota)
        {
            SphereRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** @copydoc ParticleRenderer::_notifyDefaultDimensions */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            SphereRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** @copydoc ParticleRenderer::_notifyParticleResized */
        public void _notifyParticleResized()
        {
            SphereRenderer__notifyParticleResized(nativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyParticleZRotated */
        public void _notifyParticleZRotated()
        {
            SphereRenderer__notifyParticleZRotated(nativePtr);
        }

        /** @copydoc ParticleRenderer::setRenderQueueGroup */
        public void SetRenderQueueGroup(byte queueId)
        {
            SphereRenderer_SetRenderQueueGroup(nativePtr, queueId);
        }

        /** @copydoc ParticleRenderer::_getSortMode */
        public Mogre.SortMode _getSortMode()
        {
            return SphereRenderer__GetSortMode(nativePtr);
        }

        /** @copydoc ParticleRenderer::copyAttributesTo */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            SphereRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }

        /** Access SphereSet in use */
        public SphereSet GetSphereSet()
        {
            return SphereSet.GetInstance(SphereRenderer_GetSphereSet(nativePtr));
        }

        /** @copydoc ParticleRenderer::setVisible */
        public void SetVisible(bool visible)
        {
            SphereRenderer_SetVisible(nativePtr, visible);
        }


        #region SphereRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode SphereRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer_GetSphereSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereRenderer_GetSphereSet(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereRenderer_SetVisible(IntPtr ptr, bool visible);
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
            SphereRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
