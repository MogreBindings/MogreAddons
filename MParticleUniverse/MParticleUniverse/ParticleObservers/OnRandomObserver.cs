using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe whether a random generated number is within a specified interval.
    /// </summary>
    public class OnRandomObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnRandomObserver> observerInstances;

        internal static OnRandomObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnRandomObserver>();

            OnRandomObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnRandomObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnRandomObserver(IntPtr ptr)
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
            OnRandomObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public float DEFAULT_THRESHOLD = 0.5f;

        public OnRandomObserver()
            : base(OnRandomObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// <see cref="ParticleObserver._preProcessParticles"/>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            OnRandomObserver__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// <see cref="ParticleObserver._processParticle"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="firstParticle"></param>
        public void _processParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed, bool firstParticle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            OnRandomObserver__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnRandomObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        public float Threshold { get { return OnRandomObserver_GetThreshold(nativePtr); } set { OnRandomObserver_SetThreshold(nativePtr, value); } }

        /// <summary>
        /// Copy attributes to another observer.
        /// </summary>
        /// <param name="observer"></param>
        public void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            OnRandomObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnRandomObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnRandomObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnRandomObserver__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnRandomObserver__processParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed, bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnRandomObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver_GetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float OnRandomObserver_GetThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver_SetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnRandomObserver_SetThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnRandomObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnRandomObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);
        #endregion
    }
}
