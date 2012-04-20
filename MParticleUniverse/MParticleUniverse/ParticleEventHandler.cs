using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using MParticleUniverse.ParticleEventHandlers;

namespace MParticleUniverse
{
    /// <summary>
    /// A ParticleEventHandlers is used to perform a task in case a certain event happens.
    /// <remarks>
    ///	A ParticleEventHandler is associated with a ParticleObserver; The ParticleObserver signals what event occurs,
    ///	while the ParticleEventHandler performs some action.
    ///	</remarks>
    /// </summary>
    public abstract class ParticleEventHandler : IAlias, IElement, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal ParticleEventHandler(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        #region IAlias Implementation
        public AliasType AliasType
        {
            get { return IAlias_GetAliasType(NativePointer); }
            set { IAlias_SetAliasType(NativePointer, value); }
        }
        public String AliasName
        {
            get { return Marshal.PtrToStringAnsi(IAlias_GetAliasName(NativePointer)); }
            set { IAlias_SetAliasName(NativePointer, value); }
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern AliasType IAlias_GetAliasType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasType(IntPtr ptr, AliasType aliasType);


        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr IAlias_GetAliasName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string aliasName);

        #endregion
        #endregion


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
            ParticleEventHandler_Destroy(NativePointer);
        }

        #endregion

        /// <summary>
        /// Todo
        /// </summary>
        public String Name { get { return Marshal.PtrToStringAnsi(ParticleEventHandler_GetName(nativePtr)); } set { ParticleEventHandler_SetName(nativePtr, value); } }

        /// <summary>
        /// Todo
        /// </summary>
        public ParticleObserver ParentObserver 
        {
            get { return ParticleObserver.GetObserverByPtr(ParticleEventHandler_GetParentObserver(nativePtr)); } 
            set { 
                if (value == null)
                    ParticleEventHandler_SetParentObserver(nativePtr, IntPtr.Zero);
                else
                    ParticleEventHandler_SetParentObserver(nativePtr, value.nativePtr);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public String EventHandlerType { get { return Marshal.PtrToStringAnsi(ParticleEventHandler_GetEventHandlerType(nativePtr)); } set { ParticleEventHandler_SetEventHandlerType(nativePtr, value); } }

        /// <summary>
        /// Notify that the event handler is rescaled.
        /// </summary>
        /// <param name="scale"></param>
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            ParticleEventHandler__notifyRescaled(nativePtr, scale);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Copy attributes to another event handler.
        /// </summary>
        /// <param name="eventHandler"></param>
        public void CopyAttributesTo(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            ParticleEventHandler_CopyAttributesTo(nativePtr, eventHandler.nativePtr);
        }

        /// <summary>
        /// Copy parent attributes to another event handler.
        /// </summary>
        /// <param name="eventHandler"></param>
        public void CopyParentAttributesTo(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            ParticleEventHandler_CopyParentAttributesTo(nativePtr, eventHandler.nativePtr);
        }

        internal static ParticleEventHandler GetEventHandlerFromPtr(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            
            String observerType = Marshal.PtrToStringAnsi(ParticleEventHandler_GetEventHandlerType(ptr));
            switch (observerType)
            {
                case "DoAffector":
                    return DoAffectorEventHandler.GetInstance(ptr);
                case "DoEnableComponent":
                    return DoEnableComponentEventHandler.GetInstance(ptr);
                case "DoExpire":
                    return DoExpireEventHandler.GetInstance(ptr);
                case "DoFreeze":
                    return DoFreezeEventHandler.GetInstance(ptr);
                case "DoPlacementParticle":
                    return DoPlacementParticleEventHandler.GetInstance(ptr);
                case "DoScale":
                    return DoScaleEventHandler.GetInstance(ptr);
                case "DoStopSystem":
                    return DoStopSystemEventHandler.GetInstance(ptr);
            }
            return null;
        }
        internal static ParticleEventHandler GetEventHandlerFromPtr(IntPtr ptr, String observerType)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;

            //String observerType = ParticleEventHandler_GetEventHandlerType(ptr);
            switch (observerType)
            {
                case "DoAffector":
                    return DoAffectorEventHandler.GetInstance(ptr);
                case "DoEnableComponent":
                    return DoEnableComponentEventHandler.GetInstance(ptr);
                case "DoExpire":
                    return DoExpireEventHandler.GetInstance(ptr);
                case "DoFreeze":
                    return DoFreezeEventHandler.GetInstance(ptr);
                case "DoPlacementParticle":
                    return DoPlacementParticleEventHandler.GetInstance(ptr);
                case "DoScale":
                    return DoScaleEventHandler.GetInstance(ptr);
                case "DoStopSystem":
                    return DoStopSystemEventHandler.GetInstance(ptr);
            }
            return null;
        }
        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_GetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEventHandler_GetName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_SetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler_SetName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_GetParentObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEventHandler_GetParentObserver(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_SetParentObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler_SetParentObserver(IntPtr ptr, IntPtr parentObserver);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_GetEventHandlerType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEventHandler_GetEventHandlerType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_SetEventHandlerType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler_SetEventHandlerType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string eventHandlerType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler_CopyAttributesTo(IntPtr ptr, IntPtr eventHandler);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEventHandler_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEventHandler_CopyParentAttributesTo(IntPtr ptr, IntPtr eventHandler);
        #endregion
    }
}
