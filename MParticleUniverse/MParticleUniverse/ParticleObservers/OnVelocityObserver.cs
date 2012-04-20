using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// The OnVelocityObserver determines whether the velocity of a particles is lower or higher than a certain
    ///	threshold value.
    /// </summary>
    public class OnVelocityObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnVelocityObserver> observerInstances;

        internal static OnVelocityObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnVelocityObserver>();

            OnVelocityObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnVelocityObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnVelocityObserver(IntPtr ptr)
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
            OnVelocityObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public float DEFAULT_VELOCITY_THRESHOLD = 0.0f;

        public OnVelocityObserver()
            : base(OnVelocityObserver_New())
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
            return OnVelocityObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        public float Threshold { get { return OnVelocityObserver_GetThreshold(nativePtr); } set { OnVelocityObserver_SetThreshold(nativePtr, value); } }

        public ComparisionOperator Compare { get { return OnVelocityObserver_GetCompare(nativePtr); } set { OnVelocityObserver_SetCompare(nativePtr, value); } }


        /// <summary>
        /// Copy attributes to another observer.
        /// </summary>
        /// <param name="observer"></param>
        public void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            OnVelocityObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnVelocityObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnVelocityObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnVelocityObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_GetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float OnVelocityObserver_GetThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_SetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnVelocityObserver_SetThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_GetCompare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComparisionOperator OnVelocityObserver_GetCompare(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_SetCompare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnVelocityObserver_SetCompare(IntPtr ptr, ComparisionOperator op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnVelocityObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnVelocityObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);

        #endregion
    }
}
