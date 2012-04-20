using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// This affector makes particles follow its predecessor.
    /// </summary>
    public class ParticleFollower : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ParticleFollower(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_MAX_DISTANCE = 3.40282e+038f;
        public const float DEFAULT_MIN_DISTANCE = 10.0f;


        internal static Dictionary<IntPtr, ParticleFollower> affectorInstances;

        internal static ParticleFollower GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, ParticleFollower>();

            ParticleFollower newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ParticleFollower(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public ParticleFollower()
            : base(ParticleFollower_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            ParticleFollower_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** Validate if first particle.
        */
        public void _firstParticle(ParticleTechnique particleTechnique,
                Particle particle,
                float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleFollower__firstParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleFollower__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public float MaxDistance { get { return ParticleFollower_GetMaxDistance(nativePtr); } set { ParticleFollower_SetMaxDistance(nativePtr, value); } }

        /** 
        */
        public float MinDistance { get { return ParticleFollower_GetMinDistance(nativePtr); } set { ParticleFollower_SetMinDistance(nativePtr, value); } }


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
            ParticleFollower_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleFollower_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleFollower_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleFollower_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower__firstParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleFollower__firstParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleFollower__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_GetMaxDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleFollower_GetMaxDistance(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_SetMaxDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleFollower_SetMaxDistance(IntPtr ptr, float maxDistance);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_GetMinDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleFollower_GetMinDistance(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleFollower_SetMinDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleFollower_SetMinDistance(IntPtr ptr, float minDistance);
        #endregion

    }
}
