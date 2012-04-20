using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// A FlockCenteringAffector determines the center (position) of all particles and affects each particle to go towards that center.
    /// </summary>
    public class FlockCenteringAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal FlockCenteringAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        internal static Dictionary<IntPtr, FlockCenteringAffector> affectorInstances;

        internal static FlockCenteringAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, FlockCenteringAffector>();

            FlockCenteringAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new FlockCenteringAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public FlockCenteringAffector()
            : base(FlockCenteringAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }



        /** @copydoc ParticleAffector::_preProcessParticles */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            FlockCenteringAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            FlockCenteringAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            FlockCenteringAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            FlockCenteringAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "FlockCenteringAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr FlockCenteringAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "FlockCenteringAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FlockCenteringAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "FlockCenteringAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FlockCenteringAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "FlockCenteringAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FlockCenteringAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "FlockCenteringAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void FlockCenteringAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
