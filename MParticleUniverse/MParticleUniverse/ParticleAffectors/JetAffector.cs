using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Adds a non-linear boost to a particle.
    /// </summary>
    public class JetAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal JetAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_ACCELERATION = 1.0f;


        internal static Dictionary<IntPtr, JetAffector> affectorInstances;

        internal static JetAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, JetAffector>();

            JetAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new JetAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public JetAffector()
            : base(JetAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /** 
        */
        public DynamicAttribute DynAcceleration
        {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(JetAffector_GetDynAcceleration(nativePtr)); }
            set
            {
                if (value == null)
                    JetAffector_SetDynAcceleration(nativePtr, IntPtr.Zero);
                else
                    JetAffector_SetDynAcceleration(nativePtr, value.NativePointer);
            }
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            JetAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            JetAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            JetAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "JetAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr JetAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "JetAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void JetAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "JetAffector_GetDynAcceleration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr JetAffector_GetDynAcceleration(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "JetAffector_SetDynAcceleration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void JetAffector_SetDynAcceleration(IntPtr ptr, IntPtr dynAcceleration);
        [DllImport("ParticleUniverse.dll", EntryPoint = "JetAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void JetAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "JetAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void JetAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);

        #endregion

    }
}
