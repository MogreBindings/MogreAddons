using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The SphereSurfaceEmitter emits particles from the surface of a sphere (instead within the sphere´s
    ///	volume). The particles are emitted in a direction perpendicular to the tangentvector where 
    ///	the particle emits. Using the angle attribute, the direction can be influenced.
    /// </summary>
    public class SphereSurfaceEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, SphereSurfaceEmitter> emitterInstances;

        internal static SphereSurfaceEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, SphereSurfaceEmitter>();

            SphereSurfaceEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SphereSurfaceEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal SphereSurfaceEmitter(IntPtr ptr)
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
            SphereSurfaceEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public const float DEFAULT_RADIUS = 10.0f;

        public SphereSurfaceEmitter()
            : base(SphereSurfaceEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }
        /** 
        */
        public float Radius { get { return SphereSurfaceEmitter_GetRadius(nativePtr); } set { SphereSurfaceEmitter_SetRadius(nativePtr, value); } }

        /** 
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SphereSurfaceEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void _initParticleDirection(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SphereSurfaceEmitter__initParticleDirection(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            SphereSurfaceEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }


        #region SphereSurfaceEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSurfaceEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSurfaceEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter_GetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SphereSurfaceEmitter_GetRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter_SetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSurfaceEmitter_SetRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSurfaceEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter__initParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSurfaceEmitter__initParticleDirection(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSurfaceEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSurfaceEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
}
