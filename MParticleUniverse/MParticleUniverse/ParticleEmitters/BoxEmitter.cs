using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The BoxEmitter is a ParticleEmitter that emits particles within a box shape.
    /// </summary>
    public class BoxEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, BoxEmitter> emitterInstances;

        internal static BoxEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, BoxEmitter>();

            BoxEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BoxEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal BoxEmitter(IntPtr ptr)
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
            BoxEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion


        public const float DEFAULT_WIDTH = 100.0f;
        public const float DEFAULT_HEIGHT = 100.0f;
        public const float DEFAULT_DEPTH = 100.0f;

        public BoxEmitter()
            : base(BoxEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }
	       

			/** 
	        */
        public float Height { get { return BoxEmitter_GetHeight(nativePtr); } set { BoxEmitter_SetHeight(nativePtr, value); } }

			/** 
	        */
        public float Width { get { return BoxEmitter_GetWidth(nativePtr); } set { BoxEmitter_SetWidth(nativePtr, value); } }

			/** 
	        */
        public float Depth { get { return BoxEmitter_GetDepth(nativePtr); } set { BoxEmitter_SetDepth(nativePtr, value); } }

			/** 
	        */
			public void _initParticlePosition(Particle particle)
            {
                if (particle == null)
                    throw new ArgumentNullException("particle cannot be null!");
                BoxEmitter__initParticlePosition(nativePtr, particle.NativePointer);
            }

			/** 
	        */
			public void CopyAttributesTo (ParticleEmitter emitter)
            {
                if (emitter == null)
                    throw new ArgumentNullException("emitter cannot be null!");
                BoxEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
            }

            #region BoxEmitter Exports
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr BoxEmitter_New();
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BoxEmitter_Destroy(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_GetHeight", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float BoxEmitter_GetHeight(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_SetHeight", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BoxEmitter_SetHeight(IntPtr ptr, float height);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_GetWidth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float BoxEmitter_GetWidth(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_SetWidth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BoxEmitter_SetWidth(IntPtr ptr, float width);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_GetDepth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float BoxEmitter_GetDepth(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_SetDepth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BoxEmitter_SetDepth(IntPtr ptr, float depth);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BoxEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
            [DllImport("ParticleUniverse.dll", EntryPoint = "BoxEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void BoxEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
            #endregion
    }
}
