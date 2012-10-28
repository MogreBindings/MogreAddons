using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe whether a Particle reaches a certain position.
    /// </summary>
    public class OnPositionObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnPositionObserver> observerInstances;

        internal static OnPositionObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnPositionObserver>();

            OnPositionObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnPositionObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnPositionObserver(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

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
        protected void Dispose(bool disposing)
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
            OnPositionObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public static Mogre.Vector3 DEFAULT_POSITION_THRESHOLD { get { return Mogre.Vector3.ZERO; } }

        public OnPositionObserver()
            : base(OnPositionObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnPositionObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        public float PositionXThreshold { get { return OnPositionObserver_GetPositionXThreshold(nativePtr); } set { OnPositionObserver_SetPositionXThreshold(nativePtr, value); } }
        public float PositionYThreshold { get { return OnPositionObserver_GetPositionYThreshold(nativePtr); } set { OnPositionObserver_SetPositionYThreshold(nativePtr, value); } }
        public float PositionZThreshold { get { return OnPositionObserver_GetPositionZThreshold(nativePtr); } set { OnPositionObserver_SetPositionZThreshold(nativePtr, value); } }

        public bool PositionXThresholdSet { get { return OnPositionObserver_IsPositionXThresholdSet(nativePtr); } }
        public bool PositionYThresholdSet { get { return OnPositionObserver_IsPositionYThresholdSet(nativePtr); } }
        public bool PositionZThresholdSet { get { return OnPositionObserver_IsPositionZThresholdSet(nativePtr); } }

        public void ResetPositionXThreshold()
        {
            OnPositionObserver_ResetPositionXThreshold(nativePtr);
        }
        public void ResetPositionYThreshold()
        {
            OnPositionObserver_ResetPositionYThreshold(nativePtr);
        }
        public void ResetPositionZThreshold()
        {
            OnPositionObserver_ResetPositionZThreshold(nativePtr);
        }

        public ComparisionOperator ComparePositionX { get { return OnPositionObserver_GetComparePositionX(nativePtr); } set { OnPositionObserver_SetComparePositionX(nativePtr, value); } }
        public ComparisionOperator ComparePositionY { get { return OnPositionObserver_GetComparePositionY(nativePtr); } set { OnPositionObserver_SetComparePositionY(nativePtr, value); } }
        public ComparisionOperator ComparePositionZ { get { return OnPositionObserver_GetComparePositionZ(nativePtr); } set { OnPositionObserver_SetComparePositionZ(nativePtr, value); } }

        /// <summary>
        /// Copy attributes to another observer.
        /// </summary>
        /// <param name="observer"></param>
        public void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            OnPositionObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnPositionObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnPositionObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_SetPositionXThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_SetPositionXThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_SetPositionYThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_SetPositionYThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_SetPositionZThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_SetPositionZThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_GetPositionXThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float OnPositionObserver_GetPositionXThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_GetPositionYThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float OnPositionObserver_GetPositionYThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_GetPositionZThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float OnPositionObserver_GetPositionZThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_IsPositionXThresholdSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnPositionObserver_IsPositionXThresholdSet(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_IsPositionYThresholdSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnPositionObserver_IsPositionYThresholdSet(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_IsPositionZThresholdSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnPositionObserver_IsPositionZThresholdSet(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_ResetPositionXThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_ResetPositionXThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_ResetPositionYThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_ResetPositionYThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_ResetPositionZThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_ResetPositionZThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_SetComparePositionX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_SetComparePositionX(IntPtr ptr, ComparisionOperator op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_SetComparePositionY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_SetComparePositionY(IntPtr ptr, ComparisionOperator op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_SetComparePositionZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_SetComparePositionZ(IntPtr ptr, ComparisionOperator op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_GetComparePositionX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComparisionOperator OnPositionObserver_GetComparePositionX(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_GetComparePositionY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComparisionOperator OnPositionObserver_GetComparePositionY(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_GetComparePositionZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComparisionOperator OnPositionObserver_GetComparePositionZ(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnPositionObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnPositionObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);
        #endregion
    }
}
