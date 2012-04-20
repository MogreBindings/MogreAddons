using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The LineEmitter is a ParticleEmitter that emits particles on or near a line.
    /// </summary>
    public unsafe class LineEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, LineEmitter> emitterInstances;

        internal static LineEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, LineEmitter>();

            LineEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new LineEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal LineEmitter(IntPtr ptr)
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
            LineEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public static Mogre.Vector3 DEFAULT_END { get { return Mogre.Vector3.ZERO; } }
        public const float DEFAULT_MIN_INCREMENT = 0.0f;
        public const float DEFAULT_MAX_INCREMENT = 0.0f;
        public const float DEFAULT_MAX_DEVIATION = 0.0f;

        public LineEmitter()
            : base(LineEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }

        /** 
        */
        public void _notifyStart()
        {
            LineEmitter__notifyStart(nativePtr);
        }

        /** Override the default implementation, to allow that no particles are emitted if there
            is an incremental emission of particles (along a path), and the end of the line has
            been reached.
        */
        public ushort _calculateRequestedParticles(float timeElapsed)
        {
            return LineEmitter__calculateRequestedParticles(nativePtr, timeElapsed);
        }

        /** 
        */
        public float MaxDeviation { get { return LineEmitter_GetMaxDeviation(nativePtr); } set { LineEmitter_SetMaxDeviation(nativePtr, value); } }

        /** 
        */
        public float MaxIncrement { get { return LineEmitter_GetMaxIncrement(nativePtr); } set { LineEmitter_SetMaxIncrement(nativePtr, value); } }

        /** 
        */
        public float MinIncrement { get { return LineEmitter_GetMinIncrement(nativePtr); } set { LineEmitter_SetMinIncrement(nativePtr, value); } }

        /** Get the end vector. This is the vector that defines the end of the line (in local space).
        */
        public Mogre.Vector3 End
        {
            get
            {

                Mogre.Vector3 vec = *(((Mogre.Vector3*)LineEmitter_GetEnd(nativePtr).ToPointer()));
                return vec;
            }
            set { LineEmitter_SetEnd(nativePtr, value); }
        }

        /** 
        */
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            LineEmitter__notifyRescaled(nativePtr, scale);
        }

        /** 
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            LineEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void _initParticleDirection(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            LineEmitter__initParticleDirection(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            LineEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }

        #region LineEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LineEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter__calculateRequestedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort LineEmitter__calculateRequestedParticles(IntPtr ptr, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_GetMaxDeviation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LineEmitter_GetMaxDeviation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_SetMaxDeviation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter_SetMaxDeviation(IntPtr ptr, float maxDeviation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_GetMaxIncrement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LineEmitter_GetMaxIncrement(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_SetMaxIncrement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter_SetMaxIncrement(IntPtr ptr, float maxIncrement);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_GetMinIncrement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LineEmitter_GetMinIncrement(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_SetMinIncrement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter_SetMinIncrement(IntPtr ptr, float minIncrement);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_GetEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LineEmitter_GetEnd(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_SetEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter_SetEnd(IntPtr ptr, Mogre.Vector3 end);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter__initParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter__initParticleDirection(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
}
