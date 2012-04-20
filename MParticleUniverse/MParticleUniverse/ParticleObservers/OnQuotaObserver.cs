using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe whether all particles or all particles of a specific type are emitted.
    /// </summary>
    public class OnQuotaObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnQuotaObserver> observerInstances;

        internal static OnQuotaObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnQuotaObserver>();

            OnQuotaObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnQuotaObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnQuotaObserver(IntPtr ptr)
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
            OnQuotaObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public OnQuotaObserver()
            : base(OnQuotaObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// <see cref="ParticleObserver._notifyStart"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        /// <returns></returns>
        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnQuotaObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// <see cref="ParticleObserver._notifyStart"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="timeElapsed"></param>
        public void _postProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            OnQuotaObserver__postProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnQuotaObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnQuotaObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnQuotaObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnQuotaObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnQuotaObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnQuotaObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnQuotaObserver__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnQuotaObserver__postProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        #endregion
    }
}
