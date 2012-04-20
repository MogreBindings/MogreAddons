using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe whether a Particle has been expired.
    /// </summary>
    public class OnExpireObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnExpireObserver> observerInstances;

        internal static OnExpireObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnExpireObserver>();

            OnExpireObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnExpireObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnExpireObserver(IntPtr ptr)
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
            OnExpireObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public OnExpireObserver()
            : base(OnExpireObserver_New())
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
            return OnExpireObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnExpireObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnExpireObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnExpireObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnExpireObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnExpireObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnExpireObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion
    }
}
