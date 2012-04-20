using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// The OnEventFlagObserver looks at each particle is one or more eventflags are set.
    /// </summary>
    public class OnEventFlagObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnEventFlagObserver> observerInstances;

        internal static OnEventFlagObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnEventFlagObserver>();

            OnEventFlagObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnEventFlagObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnEventFlagObserver(IntPtr ptr)
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
            OnEventFlagObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public uint DEFAULT_EVENT_FLAG = 0;

        public OnEventFlagObserver()
            : base(OnEventFlagObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        /** 
        */
        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnEventFlagObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        public uint EventFlag { get { return OnEventFlagObserver_GetEventFlag(nativePtr); } set { OnEventFlagObserver_SetEventFlag(nativePtr, value); } }

        /// <summary>
        /// Copy attributes to another observer.
        /// </summary>
        /// <param name="observer"></param>
        public void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            OnEventFlagObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEventFlagObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnEventFlagObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEventFlagObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnEventFlagObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEventFlagObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnEventFlagObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEventFlagObserver_GetEventFlag", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint OnEventFlagObserver_GetEventFlag(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEventFlagObserver_SetEventFlag", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnEventFlagObserver_SetEventFlag(IntPtr ptr, uint eventFlag);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnEventFlagObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnEventFlagObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);
        #endregion
    }
}
