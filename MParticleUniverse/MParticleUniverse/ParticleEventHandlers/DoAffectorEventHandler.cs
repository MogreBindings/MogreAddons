using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEventHandlers
{
    /// <summary>
    /// This class explicitly calls an affector to affect the particles. There are several reasons why
    ///	this is appropriate. One reason is, that you only want to affect a particle if a certain event
    ///	occurs. Disable an affector and call it using this event handler, is the method (calling the affector
    ///	from the event handler doesn't take into consideration that the affector is disabled).
    /// </summary>
    public class DoAffectorEventHandler : ParticleEventHandler, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, DoAffectorEventHandler> eventHandlerInstances;

        internal static DoAffectorEventHandler GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (eventHandlerInstances == null)
                eventHandlerInstances = new Dictionary<IntPtr, DoAffectorEventHandler>();

            DoAffectorEventHandler newvalue;

            if (eventHandlerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new DoAffectorEventHandler(ptr);
            eventHandlerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal DoAffectorEventHandler(IntPtr ptr)
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
            DoAffectorEventHandler_Destroy(NativePointer);
            eventHandlerInstances.Remove(nativePtr);
        }

        #endregion

        public const bool DEFAULT_PRE_POST = false;

        public DoAffectorEventHandler()
            : base(DoAffectorEventHandler_New())
        {
            nativePtr = base.nativePtr;
            eventHandlerInstances.Add(nativePtr, this);
        }

        /** Get the indication whether pre- and postprocessing must be done.
         * Set the indication whether pre- and postprocessing must be done.
        */
        public bool PrePost { get { return DoAffectorEventHandler_GetPrePost(nativePtr); } set { DoAffectorEventHandler_SetPrePost(nativePtr, value); } }

        /** Get the name of the affector that must be enabled or disabled.
        Set the name of the affector.*/
        public String AffectorName { get { return Marshal.PtrToStringAnsi(DoAffectorEventHandler_GetAffectorName(nativePtr)); } set { DoAffectorEventHandler_SetAffectorName(nativePtr, value); } }

        /** If the _handle() function of this class is invoked (by an Observer), it searches the 
            ParticleAffector defined by the its name. 
            The ParticleAffector is either part of the ParticleTechnique in which the 
            DoAffectorEventHandler is defined, or if the Affector is not found, other 
            ParticleTechniques are searched.
        */
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoAffectorEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy attributes to another event handler.
        */
        public void CopyAttributesTo(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            DoAffectorEventHandler_CopyAttributesTo(nativePtr, eventHandler.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoAffectorEventHandler_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoAffectorEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_GetPrePost", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoAffectorEventHandler_GetPrePost(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_SetPrePost", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoAffectorEventHandler_SetPrePost(IntPtr ptr, bool prePost);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_GetAffectorName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoAffectorEventHandler_GetAffectorName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_SetAffectorName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoAffectorEventHandler_SetAffectorName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string affectorName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoAffectorEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoAffectorEventHandler_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoAffectorEventHandler_CopyAttributesTo(IntPtr ptr, IntPtr eventHandler);
        #endregion
    }
}
