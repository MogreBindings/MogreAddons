using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The GeometryRotator rotates particles around its orientation axis. The rotation speed can be 
	///	adjusted and can be set to a 'global' rotation speed, which affects all particles in the
	///	Particle Technique the same way. It is also possible to use the rotation speed of the particles
	///	itself.
    /// </summary>
    public unsafe class GeometryRotator : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal GeometryRotator(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const bool DEFAULT_USE_OWN = false;
        public const float DEFAULT_ROTATION_SPEED = 10.0f;
        public static Mogre.Vector3 DEFAULT_ROTATION_AXIS { get { return new Mogre.Vector3(0, 0, 0); } }


        internal static Dictionary<IntPtr, GeometryRotator> affectorInstances;

        internal static GeometryRotator GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, GeometryRotator>();

            GeometryRotator newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new GeometryRotator(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public GeometryRotator()
            : base(GeometryRotator_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }



        /** Returns the rotation speed. This is the speed controlled by the affector. Besides
            the default rotation speed, it is also possible to use the particles own rotation speed.
        */
        public DynamicAttribute RotationSpeed {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(GeometryRotator_GetRotationSpeed(nativePtr)); } 
            set { 
                if (value == null)
                    GeometryRotator_SetRotationSpeed(nativePtr, IntPtr.Zero);
                else
                GeometryRotator_SetRotationSpeed(nativePtr, value.NativePointer); 
            } }

        /** Returns an indication whether the rotation speed is the same for all particles in this 
            particle technique, or whether the rotation speed of the particle itself is used.
        */
        /** Set the indication whether rotation speed of the particle itself is used.
        */
        public bool UseOwnRotationSpeed { get { return GeometryRotator_UseOwnRotationSpeed(nativePtr); } set { GeometryRotator_SetUseOwnRotationSpeed(nativePtr, value); } }

        /** 
        */
        public Mogre.Vector3 RotationAxis
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)GeometryRotator_GetRotationAxis(nativePtr).ToPointer()));
                return vec;
            }
            set { GeometryRotator_SetRotationAxis(nativePtr, value); }
        }

        /** 
        */
        public void ResetRotationAxis()
        { GeometryRotator_ResetRotationAxis(nativePtr); }

        /** Returns a rotation speed value, depending on the type of dynamic attribute.
        */
        public float _calculateRotationSpeed(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return GeometryRotator__calculateRotationSpeed(nativePtr, particle.NativePointer);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            GeometryRotator_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** @copydoc ParticleAffector::_initParticleForEmission */
        public void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            GeometryRotator_initParticleForEmission(nativePtr, particle.NativePointer);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            GeometryRotator__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            GeometryRotator_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GeometryRotator_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_GetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GeometryRotator_GetRotationSpeed(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_SetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_SetRotationSpeed(IntPtr ptr, IntPtr dynRotationSpeed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_UseOwnRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool GeometryRotator_UseOwnRotationSpeed(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_SetUseOwnRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_SetUseOwnRotationSpeed(IntPtr ptr, bool useOwnRotationSpeed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_GetRotationAxis", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GeometryRotator_GetRotationAxis(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_SetRotationAxis", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_SetRotationAxis(IntPtr ptr, Mogre.Vector3 rotationAxis);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_ResetRotationAxis", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_ResetRotationAxis(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator__calculateRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float GeometryRotator__calculateRotationSpeed(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator_initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator_initParticleForEmission(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GeometryRotator__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GeometryRotator__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion

    }
}
