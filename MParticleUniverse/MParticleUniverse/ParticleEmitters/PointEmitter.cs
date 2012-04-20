using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The PointEmitter is a ParticleEmitter that emits particles from a 3D point.
    /// </summary>
    public class PointEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, PointEmitter> emitterInstances;

        internal static PointEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, PointEmitter>();

            PointEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new PointEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal PointEmitter(IntPtr ptr)
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
        protected virtual void Dispose(bool disposing)
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
            PointEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public PointEmitter() : base(PointEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }

			/** 
	        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            PointEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }


        #region PointEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "PointEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PointEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "PointEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PointEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PointEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PointEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
}
