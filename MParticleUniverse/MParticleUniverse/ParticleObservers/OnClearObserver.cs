using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe whether all particles of a technique are no longer emitted.
    /// </summary>
    public class OnClearObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnClearObserver> observerInstances;

        internal static OnClearObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnClearObserver>();

            OnClearObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnClearObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnClearObserver(IntPtr ptr)
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
            OnClearObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public OnClearObserver()
            : base(OnClearObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        public void _notifyStart()
        {
            OnClearObserver__notifyStart(nativePtr);
        }

        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnClearObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// The _processParticle() function is overridden, because we don´t observe an individual particle.
        ///		even if there isn´t a particle left anymore (and that is the situation we want to validate).
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
            OnClearObserver__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        /// <summary>
        /// Instead of the _processParticle(), the _postProcessParticles() is used because it is called
        ///		even if there isn´t a particle left anymore (and that is the situation we want to validate).
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            OnClearObserver__postProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnClearObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnClearObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnClearObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnClearObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnClearObserver__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnClearObserver__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnClearObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnClearObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnClearObserver__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnClearObserver__processParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed, bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnClearObserver__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnClearObserver__postProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        #endregion
    }
}
