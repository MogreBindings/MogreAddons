using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// This affector applies Newton's law of universal gravitation. The distance between a particle
	///	and the GravityAffector is important in the calculation of the gravity. Therefor, this affector needs
	///	to have its position set.
    /// </summary>
    public class GravityAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal GravityAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_GRAVITY = 1.0f;


        internal static Dictionary<IntPtr, GravityAffector> affectorInstances;

        internal static GravityAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, GravityAffector>();

            GravityAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new GravityAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public GravityAffector()
            : base(GravityAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            GravityAffector__CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            GravityAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            GravityAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public float Gravity { get { return GravityAffector_GetGravity(nativePtr); } set { GravityAffector_SetGravity(nativePtr, value); } }

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
            GravityAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GravityAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector__CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityAffector__CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector_GetGravity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float GravityAffector_GetGravity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityAffector_SetGravity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityAffector_SetGravity(IntPtr ptr, float gravity);
        #endregion

    }
}
