using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Randomises the position or the direction of a particle.
    /// </summary>
    class Randomiser : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal Randomiser(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public static Mogre.Vector3 DEFAULT_MAX_DEVIATION { get { return new Mogre.Vector3(0, 0, 0); } }
        public const float DEFAULT_TIME_STEP = 0.0f;
        public const bool DEFAULT_RANDOM_DIRECTION = true;


        internal static Dictionary<IntPtr, Randomiser> affectorInstances;

        internal static Randomiser GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, Randomiser>();

            Randomiser newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Randomiser(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public Randomiser()
            : base(Randomiser_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        /** 
        */
        float MaxDeviationX { get { return Randomiser_GetMaxDeviationX(nativePtr); } set { Randomiser_SetMaxDeviationX(nativePtr, value); } }

        /** 
        */
        float MaxDeviationY { get { return Randomiser_GetMaxDeviationY(nativePtr); } set { Randomiser_SetMaxDeviationY(nativePtr, value); } }

        /** 
        */
        float MaxDeviationZ { get { return Randomiser_GetMaxDeviationZ(nativePtr); } set { Randomiser_SetMaxDeviationZ(nativePtr, value); } }

        /** 
        */
        float TimeStep { get { return Randomiser_GetTimeStep(nativePtr); } set { Randomiser_SetTimeStep(nativePtr, value); } }

        /** 
        */
        bool IsRandomDirection { get { return Randomiser_IsRandomDirection(nativePtr); } set { Randomiser_SetRandomDirection(nativePtr, value); } }

        /** 
        */
        void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            Randomiser__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** 
        */
        void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Randomiser__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            Randomiser__postProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            Randomiser_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            Randomiser_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Randomiser_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_GetMaxDeviationX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Randomiser_GetMaxDeviationX(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_SetMaxDeviationX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_SetMaxDeviationX(IntPtr ptr, float maxDeviationX);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_GetMaxDeviationY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Randomiser_GetMaxDeviationY(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_SetMaxDeviationY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_SetMaxDeviationY(IntPtr ptr, float maxDeviationZ);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_GetMaxDeviationZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Randomiser_GetMaxDeviationZ(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_SetMaxDeviationZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_SetMaxDeviationZ(IntPtr ptr, float maxDeviationZ);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_GetTimeStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Randomiser_GetTimeStep(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_SetTimeStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_SetTimeStep(IntPtr ptr, float timeStep);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_IsRandomDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Randomiser_IsRandomDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_SetRandomDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_SetRandomDirection(IntPtr ptr, bool randomDirection);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Randomiser__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Randomiser__postProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        #endregion

    }
}
