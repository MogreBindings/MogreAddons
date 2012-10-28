using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe whether a Particle is just emitted.
    /// </summary>
    public class OnEmissionObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnEmissionObserver> observerInstances;

        internal static OnEmissionObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnEmissionObserver>();

            OnEmissionObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnEmissionObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnEmissionObserver(IntPtr ptr)
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
            OnEmissionObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public OnEmissionObserver()
            : base(OnEmissionObserver_New())
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
            return OnEmissionObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEmissionObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnEmissionObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEmissionObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnEmissionObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEmissionObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnEmissionObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion
    }
}
