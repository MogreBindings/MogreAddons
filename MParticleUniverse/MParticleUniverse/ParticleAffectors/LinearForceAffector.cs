using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Adds a force to particles.
    /// </summary>
    public class LinearForceAffector : BaseForceAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal LinearForceAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        internal static Dictionary<IntPtr, LinearForceAffector> affectorInstances;

        internal static LinearForceAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, LinearForceAffector>();

            LinearForceAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new LinearForceAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public LinearForceAffector()
            : base(LinearForceAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            LinearForceAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            LinearForceAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            LinearForceAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            LinearForceAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "LinearForceAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LinearForceAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "LinearForceAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LinearForceAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LinearForceAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LinearForceAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LinearForceAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LinearForceAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LinearForceAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LinearForceAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion

    }
}
