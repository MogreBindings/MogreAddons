using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEventHandlers
{
    /// <summary>
    /// The DoExpireEventHandler expires a particle.
    /// </summary>
    public class DoExpireEventHandler : ParticleEventHandler, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, DoExpireEventHandler> eventHandlerInstances;

        internal static DoExpireEventHandler GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (eventHandlerInstances == null)
                eventHandlerInstances = new Dictionary<IntPtr, DoExpireEventHandler>();

            DoExpireEventHandler newvalue;

            if (eventHandlerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new DoExpireEventHandler(ptr);
            eventHandlerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal DoExpireEventHandler(IntPtr ptr)
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
            DoExpireEventHandler_Destroy(NativePointer);
            eventHandlerInstances.Remove(nativePtr);
        }

        #endregion

        public DoExpireEventHandler()
            : base(DoExpireEventHandler_New())
        {
            nativePtr = base.nativePtr;
            eventHandlerInstances.Add(nativePtr, this);
        }

        /** Get indication that all particles are expired
        Set indication that all particles are expired
        */
        //bool ExpireAll { get { return DoExpireEventHandler_GetExpireAll(nativePtr); } set { DoExpireEventHandler_SetExpireAll(nativePtr, value); } }

        /** 
        */
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoExpireEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoExpireEventHandler_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoExpireEventHandler_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoExpireEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoExpireEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoExpireEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoExpireEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion
    }
}
