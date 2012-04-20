using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    public unsafe class BillboardRenderer : ParticleRenderer, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, BillboardRenderer> rendererInstances;

        internal static BillboardRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, BillboardRenderer>();

            BillboardRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BillboardRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal BillboardRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        /** Alternative billboard type. 
			@remarks
				This is an extended version of Ogre's billboard type. It has BBT_ORIENTED_SHAPE added
				which basicly means that the billboard orientation is derived from the particle 
				orientation.
			*/
        public enum BillboardTypes
        {
            BBT_POINT,
            BBT_ORIENTED_COMMON,
            BBT_ORIENTED_SELF,
            BBT_ORIENTED_SHAPE,
            BBT_PERPENDICULAR_COMMON,
            BBT_PERPENDICULAR_SELF
        };

        public const BillboardRenderer.BillboardTypes DEFAULT_BILLBOARD_TYPE = BillboardRenderer.BillboardTypes.BBT_POINT;
        public const bool DEFAULT_ACCURATE_FACING = false;
        public const Mogre.BillboardOrigin DEFAULT_ORIGIN = Mogre.BillboardOrigin.BBO_CENTER;
        public const Mogre.BillboardRotationType DEFAULT_ROTATION_TYPE = Mogre.BillboardRotationType.BBR_TEXCOORD;
        public static Mogre.Vector3 DEFAULT_COMMON_DIRECTION { get { return new Mogre.Vector3(0, 0, 1); } }
        public static Mogre.Vector3 DEFAULT_COMMON_UP_VECTOR { get { return new Mogre.Vector3(0, 1, 0); } }
        public const bool DEFAULT_POINT_RENDERING = false;

        public BillboardRenderer()
            : base(BillboardRenderer_New())
        {
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }

        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BillboardRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BillboardRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** 
        */
        public BillboardTypes BillboardType { get { return BillboardRenderer_GetBillboardType(nativePtr); } set { BillboardRenderer_SetBillboardType(nativePtr, value); } }

        /** 
        */
        public bool UseAccurateFacing { get { return BillboardRenderer_IsUseAccurateFacing(nativePtr); } set { BillboardRenderer_SetUseAccurateFacing(nativePtr, value); } }

        /** 
        */
        public Mogre.BillboardOrigin BillboardOrigin { get { return BillboardRenderer_GetBillboardOrigin(nativePtr); } set { BillboardRenderer_SetBillboardOrigin(nativePtr, value); } }

        /** 
        */
        public Mogre.BillboardRotationType BillboardRotationType { get { return BillboardRenderer_GetBillboardRotationType(nativePtr); } set { BillboardRenderer_SetBillboardRotationType(nativePtr, value); } }

        /** 
        */
        public Mogre.Vector3 CommonDirection
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)BillboardRenderer_GetCommonDirection(nativePtr).ToPointer()));
                return vec;
            }
            set { BillboardRenderer_SetCommonDirection(nativePtr, value); }
        }

        /** 
        */
        public Mogre.Vector3 CommonUpVector
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)BillboardRenderer_GetCommonUpVector(nativePtr).ToPointer()));
                return vec;
            }
            set { BillboardRenderer_SetCommonUpVector(nativePtr, value); }
        }

        /** 
        */
        public bool PointRenderingEnabled { get { return BillboardRenderer_IsPointRenderingEnabled(nativePtr); } set { BillboardRenderer_SetPointRenderingEnabled(nativePtr, value); } }

        /** 
        */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            BillboardRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** 
        */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            BillboardRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            BillboardRenderer__setMaterialName(nativePtr, materialName);
        }

        /** 
        */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            BillboardRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** 
        */
        public void _notifyParticleQuota(uint quota)
        {
            BillboardRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** 
        */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            BillboardRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** 
        */
        public void _notifyParticleResized()
        {
            BillboardRenderer__notifyParticleResized(nativePtr);
        }

        /** 
        */
        public void _notifyParticleZRotated()
        {
            BillboardRenderer__notifyParticleZRotated(nativePtr);
        }

        /** 
        */
        public void SetRenderQueueGroup(byte queueId)
        {
            BillboardRenderer_SetRenderQueueGroup(nativePtr, queueId);
        }

        /** 
        */
        public Mogre.SortMode _getSortMode()
        {
            return BillboardRenderer__GetSortMode(nativePtr);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            BillboardRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }

        /** 
        */
        public Mogre.BillboardSet GetBillboardSet()
        {
            IntPtr scalePtr = BillboardRenderer_GetBillboardSet(nativePtr);
            if (scalePtr == null || scalePtr == IntPtr.Zero)
                return null;
            Mogre.BillboardSet aabtype = new Mogre.BillboardSet("BillboardSetTypeRandomGen" + DateTime.Now.Ticks);
            Mogre.BillboardSet vec = (Mogre.BillboardSet)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
            aabtype = null;
            return vec;
        }

        /** @copydoc ParticleRenderer::setVisible */
        public void SetVisible(bool visible)
        {
            BillboardRenderer_SetVisible(nativePtr, visible);
        }


        #region BillboardRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BillboardRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetBillboardType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetBillboardType(IntPtr ptr, BillboardRenderer.BillboardTypes bbt);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_GetBillboardType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern BillboardRenderer.BillboardTypes BillboardRenderer_GetBillboardType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetUseAccurateFacing", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetUseAccurateFacing(IntPtr ptr, bool acc);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_IsUseAccurateFacing", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BillboardRenderer_IsUseAccurateFacing(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetBillboardOrigin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetBillboardOrigin(IntPtr ptr, Mogre.BillboardOrigin origin);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_GetBillboardOrigin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.BillboardOrigin BillboardRenderer_GetBillboardOrigin(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetBillboardRotationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetBillboardRotationType(IntPtr ptr, Mogre.BillboardRotationType rotationType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_GetBillboardRotationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.BillboardRotationType BillboardRenderer_GetBillboardRotationType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetCommonDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetCommonDirection(IntPtr ptr, Mogre.Vector3 vec);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_GetCommonDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BillboardRenderer_GetCommonDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetCommonUpVector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetCommonUpVector(IntPtr ptr, Mogre.Vector3 vec);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_GetCommonUpVector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BillboardRenderer_GetCommonUpVector(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetPointRenderingEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetPointRenderingEnabled(IntPtr ptr, bool enabled);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_IsPointRenderingEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BillboardRenderer_IsPointRenderingEnabled(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_GetBillboardSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BillboardRenderer_GetBillboardSet(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode BillboardRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BillboardRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BillboardRenderer_SetVisible(IntPtr ptr, bool visible);
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
            BillboardRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
