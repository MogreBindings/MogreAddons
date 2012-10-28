using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Force Field Affector Class:
	///	This Affector Class uses a force field to affect the particle direction.
    /// </summary>
    public unsafe class ForceFieldAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ForceFieldAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        public const ForceField.ForceFieldTypes DEFAULT_FORCEFIELD_TYPE = ForceField.ForceFieldTypes.FF_REALTIME_CALC;
        public const float DEFAULT_DELTA = 1.0f;
        public const float DEFAULT_FORCE = 400.0f;
        public const ushort DEFAULT_OCTAVES = 2;
        public const double DEFAULT_FREQUENCY = 1.0f;
        public const double DEFAULT_AMPLITUDE = 1.0f;
        public const double DEFAULT_PERSISTENCE = 3.0f;
        public const uint DEFAULT_FORCEFIELDSIZE = 64;
        public static Mogre.Vector3 DEFAULT_WORLDSIZE { get { return new Mogre.Vector3(500.0f, 500.0f, 500.0f); } }
        public static Mogre.Vector3 DEFAULT_MOVEMENT { get { return new Mogre.Vector3(500.0f, 0.0f, 0.0f); } }
        public const float DEFAULT_MOVEMENT_FREQUENCY = 5.0f;

        internal static Dictionary<IntPtr, ForceFieldAffector> affectorInstances;

        internal static ForceFieldAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, ForceFieldAffector>();

            ForceFieldAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ForceFieldAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public ForceFieldAffector()
            : base(ForceFieldAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }




        /// <summary>
        /// <see cref="ParticleAffector._prepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>

        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            ForceFieldAffector__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /** @copydoc ParticleAffector::_preProcessParticles */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            ForceFieldAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** @copydoc ParticleAffector::_notifyStart */
        public void _notifyStart()
        {
            ForceFieldAffector__notifyStart(nativePtr);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ForceFieldAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Get/Set Forcefield type
        */
        public ForceField.ForceFieldTypes ForceFieldType { get { return ForceFieldAffector_GetForceFieldType(nativePtr); } set { ForceFieldAffector_SetForceFieldType(nativePtr, value); } }

        /** Get/Set Delta
        */
        public float Delta { get { return ForceFieldAffector_GetDelta(nativePtr); } set { ForceFieldAffector_SetDelta(nativePtr, value); } }

        /** Get/Set scale Force
        */
        public float ScaleForce { get { return ForceFieldAffector_GetScaleForce(nativePtr); } set { ForceFieldAffector_SetScaleForce(nativePtr, value); } }

        /** Get/Set scale Octaves
        */
        public ushort Octaves { get { return ForceFieldAffector_GetOctaves(nativePtr); } set { ForceFieldAffector_SetOctaves(nativePtr, value); } }

        /** Get/Set scale Frequency
        */
        public double Frequency { get { return ForceFieldAffector_GetFrequency(nativePtr); } set { ForceFieldAffector_SetFrequency(nativePtr, value); } }

        /** Get/Set scale Amplitude
        */
        public double Amplitude { get { return ForceFieldAffector_GetAmplitude(nativePtr); } set { ForceFieldAffector_SetAmplitude(nativePtr, value); } }

        /** Get/Set scale Persistence
        */
        public double Persistence { get { return ForceFieldAffector_GetPersistence(nativePtr); } set { ForceFieldAffector_SetPersistence(nativePtr, value); } }

        /** Get/Set scale ForceFieldSize
        */
        public uint ForceFieldSize { get { return ForceFieldAffector_GetForceFieldSize(nativePtr); } set { ForceFieldAffector_SetForceFieldSize(nativePtr, value); } }

        /** Get/Set scale worldSize
        */
        public Mogre.Vector3 WorldSize
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ForceFieldAffector_GetWorldSize(nativePtr).ToPointer()));
                return vec;
            }
            set { ForceFieldAffector_SetWorldSize(nativePtr, value); }
        }

        /** Get/Set scale flip attributes
        */
        public bool IgnoreNegativeX { get { return ForceFieldAffector_GetIgnoreNegativeX(nativePtr); } set { ForceFieldAffector_SetIgnoreNegativeX(nativePtr, value); } }
        public bool IgnoreNegativeY { get { return ForceFieldAffector_GetIgnoreNegativeY(nativePtr); } set { ForceFieldAffector_SetIgnoreNegativeY(nativePtr, value); } }
        public bool IgnoreNegativeZ { get { return ForceFieldAffector_GetIgnoreNegativeZ(nativePtr); } set { ForceFieldAffector_SetIgnoreNegativeZ(nativePtr, value); } }

        /** Get/Set Movement
        @remarks
            The movement vector determines the position of the movement. This movement is a displacement of the particle position 
            mapped to the forcefield.
        */
        public Mogre.Vector3 Movement
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ForceFieldAffector_GetMovement(nativePtr).ToPointer()));
                return vec;
            }
            set { ForceFieldAffector_SetMovement(nativePtr, value); }
        }

        /** Get/Set Movement frequency
        */
        public float MovementFrequency { get { return ForceFieldAffector_GetMovementFrequency(nativePtr); } set { ForceFieldAffector_SetMovementFrequency(nativePtr, value); } }

        /** Suppress (re)generation of the forcefield everytime an attribute is changed.
        */
        public void SuppressGeneration(bool suppress)
        {
            ForceFieldAffector_SuppressGeneration(nativePtr, suppress);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            ForceFieldAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            ForceFieldAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ForceFieldAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetForceFieldType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ForceField.ForceFieldTypes ForceFieldAffector_GetForceFieldType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetForceFieldType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetForceFieldType(IntPtr ptr, ForceField.ForceFieldTypes forceFieldType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetDelta", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ForceFieldAffector_GetDelta(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetDelta", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetDelta(IntPtr ptr, float delta);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetScaleForce", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ForceFieldAffector_GetScaleForce(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetScaleForce", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetScaleForce(IntPtr ptr, float scaleForce);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetOctaves", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ForceFieldAffector_GetOctaves(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetOctaves", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetOctaves(IntPtr ptr, ushort octaves);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ForceFieldAffector_GetFrequency(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetFrequency(IntPtr ptr, double frequency);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetAmplitude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ForceFieldAffector_GetAmplitude(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetAmplitude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetAmplitude(IntPtr ptr, double amplitude);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetPersistence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ForceFieldAffector_GetPersistence(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetPersistence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetPersistence(IntPtr ptr, double persistence);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetForceFieldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ForceFieldAffector_GetForceFieldSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetForceFieldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetForceFieldSize(IntPtr ptr, uint forceFieldSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetWorldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ForceFieldAffector_GetWorldSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetWorldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetWorldSize(IntPtr ptr, Mogre.Vector3 worldSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetIgnoreNegativeX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ForceFieldAffector_GetIgnoreNegativeX(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetIgnoreNegativeX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetIgnoreNegativeX(IntPtr ptr, bool ignoreNegativeX);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetIgnoreNegativeY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ForceFieldAffector_GetIgnoreNegativeY(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetIgnoreNegativeY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetIgnoreNegativeY(IntPtr ptr, bool ignoreNegativeY);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetIgnoreNegativeZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ForceFieldAffector_GetIgnoreNegativeZ(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetIgnoreNegativeZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetIgnoreNegativeZ(IntPtr ptr, bool ignoreNegativeZ);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetMovement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ForceFieldAffector_GetMovement(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetMovement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetMovement(IntPtr ptr, Mogre.Vector3 movement);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_GetMovementFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ForceFieldAffector_GetMovementFrequency(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SetMovementFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SetMovementFrequency(IntPtr ptr, float movementFrequency);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_SuppressGeneration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_SuppressGeneration(IntPtr ptr, bool suppress);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceFieldAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceFieldAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
