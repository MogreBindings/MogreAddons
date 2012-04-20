using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Applies a sine force to a particle.
    /// </summary>
    public class SineForceAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal SineForceAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }
        public const float DEFAULT_FREQ_MIN = 1.0f;
        public const float DEFAULT_FREQ_MAX = 1.0f;


        internal static Dictionary<IntPtr, SineForceAffector> affectorInstances;

        internal static SineForceAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, SineForceAffector>();

            SineForceAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SineForceAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public SineForceAffector()
            : base(SineForceAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            SineForceAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            SineForceAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SineForceAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public float FrequencyMin { get { return SineForceAffector_GetFrequencyMin(nativePtr); } set { SineForceAffector_SetFrequencyMin(nativePtr, value); } }

        /** 
        */
        public float FrequencyMax { get { return SineForceAffector_GetFrequencyMax(nativePtr); } set { SineForceAffector_SetFrequencyMax(nativePtr, value); } }

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
            SineForceAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SineForceAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SineForceAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SineForceAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SineForceAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SineForceAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_GetFrequencyMin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SineForceAffector_GetFrequencyMin(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_SetFrequencyMin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SineForceAffector_SetFrequencyMin(IntPtr ptr, float frequencyMin);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_GetFrequencyMax", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SineForceAffector_GetFrequencyMax(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SineForceAffector_SetFrequencyMax", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SineForceAffector_SetFrequencyMax(IntPtr ptr, float frequencyMax);
        #endregion

    }
}
