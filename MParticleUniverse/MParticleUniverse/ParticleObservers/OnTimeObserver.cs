using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// The OnTimeObserver observers how much time has been elapsed. This can be both the particles own time
    ///	and the time since the ParticleSystem was started.
    /// </summary>
    public class OnTimeObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnTimeObserver> observerInstances;

        internal static OnTimeObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnTimeObserver>();

            OnTimeObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnTimeObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnTimeObserver(IntPtr ptr)
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
            OnTimeObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public float DEFAULT_THRESHOLD = 0.0f;
        public bool DEFAULT_SINCE_START_SYSTEM = false;

        public OnTimeObserver()
            : base(OnTimeObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// In case there are no particles, but the observation returns true, the event handlers must still be
        ///		called.
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            OnTimeObserver__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnTimeObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        public float Threshold { get { return OnTimeObserver_GetThreshold(nativePtr); } set { OnTimeObserver_SetThreshold(nativePtr, value); } }

        public ComparisionOperator Compare { get { return OnTimeObserver_GetCompare(nativePtr); } set { OnTimeObserver_SetCompare(nativePtr, value); } }

        public bool SinceStartSystem { get { return OnTimeObserver_IsSinceStartSystem(nativePtr); } set { OnTimeObserver_SetSinceStartSystem(nativePtr, value); } }

        /* Copy attributes to another observer.
        */
        public void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            OnTimeObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnTimeObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnTimeObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnTimeObserver__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnTimeObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_GetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float OnTimeObserver_GetThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_SetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnTimeObserver_SetThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_GetCompare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComparisionOperator OnTimeObserver_GetCompare(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_SetCompare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnTimeObserver_SetCompare(IntPtr ptr, ComparisionOperator op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_IsSinceStartSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnTimeObserver_IsSinceStartSystem(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_SetSinceStartSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnTimeObserver_SetSinceStartSystem(IntPtr ptr, bool sinceStartSystem);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnTimeObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnTimeObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);

        #endregion
    }
}
