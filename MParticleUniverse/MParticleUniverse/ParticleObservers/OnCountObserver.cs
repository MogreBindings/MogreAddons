using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleObservers
{
    /// <summary>
    /// This class is used to observe if an x number of Particles have been emitted.
    /// <remarks>
    ///	OnCountObserver supports 2 possible situations. If the comparison operator is set to 
    ///	'less than', the _observe() function returns true until the counter exceeds the threshold.
    ///	If the comparison operator is set to 'greater than', the _observe() function only returns 
    ///	true if the counter exceeds the threshold.
    /// </remarks>
    /// </summary>
    public class OnCountObserver : ParticleObserver, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, OnCountObserver> observerInstances;

        internal static OnCountObserver GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (observerInstances == null)
                observerInstances = new Dictionary<IntPtr, OnCountObserver>();

            OnCountObserver newvalue;

            if (observerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new OnCountObserver(ptr);
            observerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal OnCountObserver(IntPtr ptr)
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
            OnCountObserver_Destroy(NativePointer);
            observerInstances.Remove(nativePtr);
        }

        #endregion

        public uint DEFAULT_THRESHOLD = 0;
        public ComparisionOperator DEFAULT_COMPARE = ComparisionOperator.CO_LESS_THAN;

        public OnCountObserver()
            : base(OnCountObserver_New())
        {
            nativePtr = base.nativePtr;
            observerInstances.Add(nativePtr, this);
        }

        public void _notifyStart()
        {
            OnCountObserver__notifyStart(nativePtr);
        }

        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return OnCountObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        public uint Threshold { get { return OnCountObserver_GetThreshold(nativePtr); } set { OnCountObserver_SetThreshold(nativePtr, value); } }

        public ComparisionOperator Compare { get { return OnCountObserver_GetCompare(nativePtr); } set { OnCountObserver_SetCompare(nativePtr, value); } }

        /* Copy attributes to another observer.
        */

        void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            OnCountObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr OnCountObserver_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnCountObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnCountObserver__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool OnCountObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_GetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint OnCountObserver_GetThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_SetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnCountObserver_SetThreshold(IntPtr ptr, uint threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_GetCompare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComparisionOperator OnCountObserver_GetCompare(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_SetCompare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnCountObserver_SetCompare(IntPtr ptr, ComparisionOperator op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "OnCountObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void OnCountObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);
        #endregion
    }
}
