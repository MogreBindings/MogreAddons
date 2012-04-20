using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The VelocityMatchingAffector is used to adjust the velocity of a particle to the velocity of its neighbours.
    /// </summary>
    public class VelocityMatchingAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal VelocityMatchingAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_RADIUS = 100.0f;


        internal static Dictionary<IntPtr, VelocityMatchingAffector> affectorInstances;

        internal static VelocityMatchingAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, VelocityMatchingAffector>();

            VelocityMatchingAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new VelocityMatchingAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public VelocityMatchingAffector()
            : base(VelocityMatchingAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        /** Todo
        */
        public float Radius
        {
            get { return VelocityMatchingAffector_GetRadius(nativePtr); }
            set { VelocityMatchingAffector_SetRadius(nativePtr, value); }
        }

        
        /// <summary>
        /// <see cref="ParticleAffector._prepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            VelocityMatchingAffector__prepare(nativePtr, particleTechnique.nativePtr);
        }
        /// <summary>
        /// <see cref="ParticleAffector._unprepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            VelocityMatchingAffector__unprepare(nativePtr, particleTechnique.nativePtr);
        }
        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            VelocityMatchingAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }
        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            VelocityMatchingAffector_CopyAttributesTo(nativePtr, affector.nativePtr); 
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
            VelocityMatchingAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VelocityMatchingAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VelocityMatchingAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector_GetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float VelocityMatchingAffector_GetRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector_SetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VelocityMatchingAffector_SetRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VelocityMatchingAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VelocityMatchingAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VelocityMatchingAffector__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VelocityMatchingAffector__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VelocityMatchingAffector__unprepare(IntPtr ptr, IntPtr particleTechnique);
        #endregion
    }
}
