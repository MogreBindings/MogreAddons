using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEventHandlers
{
    /// <summary>
    /// The DoStopSystemEventHandler stops the ParticleSystem.
    /// </summary>
    public class DoStopSystemEventHandler : ParticleEventHandler, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, DoStopSystemEventHandler> eventHandlerInstances;

        internal static DoStopSystemEventHandler GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (eventHandlerInstances == null)
                eventHandlerInstances = new Dictionary<IntPtr, DoStopSystemEventHandler>();

            DoStopSystemEventHandler newvalue;

            if (eventHandlerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new DoStopSystemEventHandler(ptr);
            eventHandlerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal DoStopSystemEventHandler(IntPtr ptr)
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
            DoStopSystemEventHandler_Destroy(NativePointer);
            eventHandlerInstances.Remove(nativePtr);
        }

        #endregion

        public DoStopSystemEventHandler()
            : base(DoStopSystemEventHandler_New())
        {
            nativePtr = base.nativePtr;
            eventHandlerInstances.Add(nativePtr, this);
        }

        /** 
        */
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoStopSystemEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoStopSystemEventHandler_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoStopSystemEventHandler_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoStopSystemEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoStopSystemEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoStopSystemEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoStopSystemEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion
    }
}
