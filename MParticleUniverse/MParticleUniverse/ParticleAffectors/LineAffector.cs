using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Affects a particle depending on a line shape. Particles are getting a new position along the line.
    /// </summary>
    public unsafe class LineAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal LineAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_MAX_DEVIATION = 1.0f;
        public const float DEFAULT_TIME_STEP = 0.1f;
        public static Mogre.Vector3 DEFAULT_END { get { return new Mogre.Vector3(0, 0, 0); } }
        public const float DEFAULT_DRIFT = 0.0f;


        internal static Dictionary<IntPtr, LineAffector> affectorInstances;

        internal static LineAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, LineAffector>();

            LineAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new LineAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public LineAffector()
            : base(LineAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /** 
        */
        public float MaxDeviation { get { return LineAffector_GetMaxDeviation(nativePtr); } set { LineAffector_SetMaxDeviation(nativePtr, value); } }

        /** 
        */
        public Mogre.Vector3 End
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)LineAffector_GetEnd(nativePtr).ToPointer()));
                return vec;
            }
            set { LineAffector_SetEnd(nativePtr, value); }
        }

        /** 
        */
        public float TimeStep { get { return LineAffector_GetTimeStep(nativePtr); } set { LineAffector_SetTimeStep(nativePtr, value); } }

        /** 
        */
        public float Drift { get { return LineAffector_GetDrift(nativePtr); } set { LineAffector_SetDrift(nativePtr, value); } }

        /**
        */
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            LineAffector__notifyRescaled(nativePtr, scale);
        }

        /** 
        */
        public void _firstParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            LineAffector__firstParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }
        /** 
        */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            LineAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            LineAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            LineAffector__postProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            LineAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            LineAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LineAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_GetMaxDeviation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LineAffector_GetMaxDeviation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_SetMaxDeviation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector_SetMaxDeviation(IntPtr ptr, float maxDeviation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_GetEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LineAffector_GetEnd(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_SetEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector_SetEnd(IntPtr ptr, Mogre.Vector3 end);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_GetTimeStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LineAffector_GetTimeStep(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_SetTimeStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector_SetTimeStep(IntPtr ptr, float timeStep);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_GetDrift", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LineAffector_GetDrift(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_SetDrift", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector_SetDrift(IntPtr ptr, float drift);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector__firstParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector__firstParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector__postProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LineAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LineAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
