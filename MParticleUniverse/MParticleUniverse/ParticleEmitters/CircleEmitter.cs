using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The CircleEmitter is a ParticleEmitter that emits particles on a circle shape. Particle can be emitted
    ///	random on the circle, but it can also follow the circles' contours.
    /// </summary>
    public unsafe class CircleEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, CircleEmitter> emitterInstances;

        internal static CircleEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, CircleEmitter>();

            CircleEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new CircleEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal CircleEmitter(IntPtr ptr)
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
            CircleEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public const float DEFAULT_RADIUS = 100.0f;
        public const float DEFAULT_STEP = 0.1f;
        public const float DEFAULT_ANGLE = 0.0f;
        public const bool DEFAULT_RANDOM = true;
        public static Mogre.Vector3 DEFAULT_NORMAL { get { return Mogre.Vector3.ZERO; } }

        public CircleEmitter()
            : base(CircleEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }


        /** 
        */
        public float Radius { get { return CircleEmitter_GetRadius(nativePtr); } set { CircleEmitter_SetRadius(nativePtr, value); } }

        /** 
        */
        public float CircleAngle { get { return CircleEmitter_GetCircleAngle(nativePtr); } set { CircleEmitter_SetCircleAngle(nativePtr, value); } }

        /** 
        */
        public float Step { get { return CircleEmitter_GetStep(nativePtr); } set { CircleEmitter_SetStep(nativePtr, value); } }

        /** 
        */
        public bool Random { get { return CircleEmitter_IsRandom(nativePtr); } set { CircleEmitter_SetRandom(nativePtr, value); } }

        /* 
        */
        public Mogre.Quaternion Orientation
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)CircleEmitter_GetOrientation(nativePtr).ToPointer()));
                return vec;
            }

        }
        public Mogre.Vector3 Normal
        {
            get
            {

                Mogre.Vector3 vec = *(((Mogre.Vector3*)CircleEmitter_GetNormal(nativePtr).ToPointer()));
                return vec;
            }
            set { CircleEmitter_SetNormal(nativePtr, value); }
        }

        /** See ParticleEmiter
        */
        void _notifyStart()
        {
            CircleEmitter__notifyStart(nativePtr);
        }

        /** Determine a particle position on the circle.
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            CircleEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** Determine the particle direction.
        */
        public void _initParticleDirection(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            CircleEmitter__initParticleDirection(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            CircleEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }

        #region CircleEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CircleEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_GetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float CircleEmitter_GetRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_SetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_SetRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_GetCircleAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float CircleEmitter_GetCircleAngle(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_SetCircleAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_SetCircleAngle(IntPtr ptr, float circleAngle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_GetStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float CircleEmitter_GetStep(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_SetStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_SetStep(IntPtr ptr, float step);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_IsRandom", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CircleEmitter_IsRandom(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_SetRandom", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_SetRandom(IntPtr ptr, bool random);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_GetOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CircleEmitter_GetOrientation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_GetNormal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CircleEmitter_GetNormal(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_SetNormal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_SetNormal(IntPtr ptr, Mogre.Vector3 normal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter__initParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter__initParticleDirection(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CircleEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CircleEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion

    }
}
