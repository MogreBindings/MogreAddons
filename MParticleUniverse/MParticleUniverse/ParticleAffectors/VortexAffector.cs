using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The VortexAffector rotates particles around a given rotation axis.
    /// </summary>
    public unsafe class VortexAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal VortexAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public static Mogre.Vector3 DEFAULT_ROTATION_VECTOR { get { return new Mogre.Vector3(0, 0, 0); } }
        public const float DEFAULT_ROTATION_SPEED = 1.0f;


        internal static Dictionary<IntPtr, VortexAffector> affectorInstances;

        internal static VortexAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, VortexAffector>();

            VortexAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new VortexAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public VortexAffector()
            : base(VortexAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        /** 
        */
        public Mogre.Vector3 RotationVector
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)VortexAffector_GetRotationVector(nativePtr).ToPointer()));
                return vec;
            }
            set { VortexAffector_SetRotationVector(nativePtr, value); }
        }

        /** 
        */
        public DynamicAttribute RotationSpeed
        {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(VortexAffector_GetRotationSpeed(nativePtr)); }
            set
            {
                if (value == null)
                    VortexAffector_SetRotationSpeed(nativePtr, IntPtr.Zero);
                else
                    VortexAffector_SetRotationSpeed(nativePtr, value.NativePointer);
            }
        }
        
        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            VortexAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            VortexAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            VortexAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            VortexAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VortexAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_GetRotationVector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VortexAffector_GetRotationVector(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_SetRotationVector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexAffector_SetRotationVector(IntPtr ptr, Mogre.Vector3 rotationVector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_GetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VortexAffector_GetRotationSpeed(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_SetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexAffector_SetRotationSpeed(IntPtr ptr, IntPtr dynRotationSpeed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);

        #endregion
    }
}
