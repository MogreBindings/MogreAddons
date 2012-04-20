using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEventHandlers
{
    /// <summary>
    /// This class makes it possible to enable or disable a Component. This component is an 
    ///	named emitter, affector or technique.
    /// </summary>
    public class DoEnableComponentEventHandler : ParticleEventHandler, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, DoEnableComponentEventHandler> eventHandlerInstances;

        internal static DoEnableComponentEventHandler GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (eventHandlerInstances == null)
                eventHandlerInstances = new Dictionary<IntPtr, DoEnableComponentEventHandler>();

            DoEnableComponentEventHandler newvalue;

            if (eventHandlerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new DoEnableComponentEventHandler(ptr);
            eventHandlerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal DoEnableComponentEventHandler(IntPtr ptr)
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
            DoEnableComponentEventHandler_Destroy(NativePointer);
            eventHandlerInstances.Remove(nativePtr);
        }

        #endregion

        public DoEnableComponentEventHandler()
            : base(DoEnableComponentEventHandler_New())
        {
            nativePtr = base.nativePtr;
            eventHandlerInstances.Add(nativePtr, this);
        }

        /** Get the name of the component that must be enabled or disabled.
        Set the name of the component that must be enabled or disables*/
        public String ComponentName { get { return Marshal.PtrToStringAnsi(DoEnableComponentEventHandler_GetComponentName(nativePtr)); } set { DoEnableComponentEventHandler_SetComponentName(nativePtr, value); } }

        /** Get the value that identifies whether the component must be enabled or disabled.
        Set the value that identifies whether the component must be enabled or disabled.*/
        public bool IsComponentEnabled { get { return DoEnableComponentEventHandler_IsComponentEnabled(nativePtr); } set { DoEnableComponentEventHandler_SetComponentEnabled(nativePtr, value); } }

        /** Get the value that identifies whether the component must be enabled or disabled.
        Set the value that identifies whether the component must be enabled or disabled.
        */
        public ComponentType ComponentType { get { return DoEnableComponentEventHandler_GetComponentType(nativePtr); } set { DoEnableComponentEventHandler_SetComponentType(nativePtr, value); } }

        /** If the _handle() function of this class is invoked (by an Observer), it searches the 
            ParticleEmitter, ParticleAffector or ParticleTechnique defined by the its name. 
            The ParticleEmitter/Affector is either part of the ParticleTechnique in which the 
            DoEnableComponentEventHandler is defined, or if the ParticleEmitter/Affector is not 
            found, other ParticleTechniques are searched.
        */
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoEnableComponentEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy attributes to another event handler.
        */
        public void CopyAttributesTo(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            DoEnableComponentEventHandler_CopyAttributesTo(nativePtr, eventHandler.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoEnableComponentEventHandler_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoEnableComponentEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_GetComponentName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoEnableComponentEventHandler_GetComponentName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_SetComponentName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoEnableComponentEventHandler_SetComponentName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string componentName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_IsComponentEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoEnableComponentEventHandler_IsComponentEnabled(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_SetComponentEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoEnableComponentEventHandler_SetComponentEnabled(IntPtr ptr, bool enabled);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_GetComponentType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ComponentType DoEnableComponentEventHandler_GetComponentType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_SetComponentType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoEnableComponentEventHandler_SetComponentType(IntPtr ptr, ComponentType componentType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoEnableComponentEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoEnableComponentEventHandler_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoEnableComponentEventHandler_CopyAttributesTo(IntPtr ptr, IntPtr eventHandler);
        #endregion
    }
}
