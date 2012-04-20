using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEventHandlers
{
    /// <summary>
    /// The DoScaleEventHandler scales different particle attributes.
    /// </summary>
    public class DoScaleEventHandler : ParticleEventHandler, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, DoScaleEventHandler> eventHandlerInstances;

        internal static DoScaleEventHandler GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (eventHandlerInstances == null)
                eventHandlerInstances = new Dictionary<IntPtr, DoScaleEventHandler>();

            DoScaleEventHandler newvalue;

            if (eventHandlerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new DoScaleEventHandler(ptr);
            eventHandlerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal DoScaleEventHandler(IntPtr ptr)
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
            DoScaleEventHandler_Destroy(NativePointer);
            eventHandlerInstances.Remove(nativePtr);
        }

        #endregion

        public const ScaleTypes DEFAULT_SCALE_TYPE = ScaleTypes.ST_TIME_TO_LIVE;
        public const float DEFAULT_SCALE_FRACTION = 0.2f;

        public enum ScaleTypes
        {
            ST_TIME_TO_LIVE,
            ST_VELOCITY
        };

        public DoScaleEventHandler()
            : base(DoScaleEventHandler_New())
        {
            nativePtr = base.nativePtr;
            eventHandlerInstances.Add(nativePtr, this);
        }
        /** Returns the scale type
        Set the scale type. This scale type identifies to which attribute the scale factor is applied.
        */
        public ScaleTypes ScaleType { get { return DoScaleEventHandler_GetScaleType(nativePtr); } set { DoScaleEventHandler_SetScaleType(nativePtr, value); } }

        /** Returns the scale fraction
        Set the scale fraction. This scale fraction value is used to scale different attributes if the 
            event handler is called.
        */
        public float ScaleFraction { get { return DoScaleEventHandler_GetScaleFraction(nativePtr); } set { DoScaleEventHandler_SetScaleFraction(nativePtr, value); } }

        /** 
        */
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoScaleEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy attributes to another event handler.
        */
        public void CopyAttributesTo(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            DoScaleEventHandler_CopyAttributesTo(nativePtr, eventHandler.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoScaleEventHandler_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoScaleEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_GetScaleType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ScaleTypes DoScaleEventHandler_GetScaleType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_SetScaleType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoScaleEventHandler_SetScaleType(IntPtr ptr, ScaleTypes scaleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_GetScaleFraction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DoScaleEventHandler_GetScaleFraction(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_SetScaleFraction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoScaleEventHandler_SetScaleFraction(IntPtr ptr, float scaleFraction);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoScaleEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoScaleEventHandler_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoScaleEventHandler_CopyAttributesTo(IntPtr ptr, IntPtr eventHandler);
        #endregion
    }
}
