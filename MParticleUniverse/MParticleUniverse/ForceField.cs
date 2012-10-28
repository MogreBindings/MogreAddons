using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// Force Field Affector Class:
    ///	This class defines a force field to affect the particle direction. The force field is based on 3D noise. The force can be calculated in
    ///	realtime or based on a precreated 3D force field matrix, which essentially involves one lookup. To speed things up, the 3d matrix can be
    ///	precreated in a separate thread (optionally).
    /// </summary>
    public unsafe class ForceField : IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, ForceField> forceFieldInstances;

        internal static ForceField GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (forceFieldInstances == null)
                forceFieldInstances = new Dictionary<IntPtr, ForceField>();

            ForceField newvalue;

            if (forceFieldInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ForceField(ptr);
            forceFieldInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ForceField(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public enum ForceFieldTypes
        {
            FF_REALTIME_CALC = 0,
            FF_MATRIX_CALC = 1
        };

        public ForceField()
        {
            nativePtr = ForceField_New();
            forceFieldInstances.Add(nativePtr, this);
        }

        /** Initialises a ForceField */
        public void Initialise(ForceFieldTypes type,
            Mogre.Vector3 position,
            uint forceFieldSize,
            ushort octaves,
            double frequency,
            double amplitude,
            double persistence,
            Mogre.Vector3 worldSize)
        {
            ForceField_Initialise(nativePtr, type, position, forceFieldSize, octaves, frequency, amplitude, persistence, worldSize);
        }

        /** Initialises a ForceField */
        public void Initialise(ForceFieldTypes type,
            uint forceFieldSize,
            ushort octaves,
            double frequency,
            double amplitude,
            double persistence,
            Mogre.Vector3 worldSize)
        {
            ForceField_Initialise(nativePtr, type, forceFieldSize, octaves, frequency, amplitude, persistence, worldSize);
        }

        /** Get/Set the base position of the force field */
        public Mogre.Vector3 ForceFieldPositionBase
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ForceField_GetForceFieldPositionBase(nativePtr).ToPointer()));
                return vec;
            }
            set { ForceField_SetForceFieldPositionBase(nativePtr, value); }
        }

        /** Calculate the force, based on a certain position */
        public void DetermineForce(Mogre.Vector3 position, Mogre.Vector3 force, float delta)
        {
            ForceField_DetermineForce(nativePtr, position, force, delta);
        }

        /** Getters/Setters
        */
        public ushort Octaves { get { return ForceField_GetOctaves(nativePtr); } set { ForceField_SetOctaves(nativePtr, value); } }
        public double Frequency { get { return ForceField_GetFrequency(nativePtr); } set { ForceField_SetFrequency(nativePtr, value); } }
        public double Amplitude { get { return ForceField_GetAmplitude(nativePtr); } set { ForceField_SetAmplitude(nativePtr, value); } }
        public double Persistence { get { return ForceField_GetPersistence(nativePtr); } set { ForceField_SetPersistence(nativePtr, value); } }
        public uint ForceFieldSize { get { return ForceField_GetForceFieldSize(nativePtr); } set { ForceField_SetForceFieldSize(nativePtr, value); } }
        public Mogre.Vector3 WorldSize
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ForceField_GetWorldSize(nativePtr).ToPointer()));
                return vec;
            }
            set { ForceField_SetWorldSize(nativePtr, value); }
        }

        /** Get/Set the Forcefield type
        */
        ForceFieldTypes ForceFieldType { get { return ForceField_GetForceFieldType(nativePtr); } set { ForceField_SetForceFieldType(nativePtr, value); } }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ForceField_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_Initialise", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_Initialise(IntPtr ptr, ForceField.ForceFieldTypes type, Mogre.Vector3 position, uint forceFieldSize, ushort octaves, double frequency, double amplitude, double persistence, Mogre.Vector3 worldSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_Initialise2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_Initialise(IntPtr ptr, ForceField.ForceFieldTypes type, uint forceFieldSize, ushort octaves, double frequency, double amplitude, double persistence, Mogre.Vector3 worldSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetForceFieldPositionBase", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ForceField_GetForceFieldPositionBase(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetForceFieldPositionBase", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetForceFieldPositionBase(IntPtr ptr, Mogre.Vector3 position);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_DetermineForce", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_DetermineForce(IntPtr ptr, Mogre.Vector3 position, Mogre.Vector3 force, float delta);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetOctaves", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ForceField_GetOctaves(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetOctaves", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetOctaves(IntPtr ptr, ushort octaves);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ForceField_GetFrequency(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetFrequency(IntPtr ptr, double frequency);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetAmplitude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ForceField_GetAmplitude(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetAmplitude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetAmplitude(IntPtr ptr, double amplitude);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetPersistence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern double ForceField_GetPersistence(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetPersistence", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetPersistence(IntPtr ptr, double persistence);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetForceFieldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ForceField_GetForceFieldSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetForceFieldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetForceFieldSize(IntPtr ptr, uint forceFieldSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetWorldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ForceField_GetWorldSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetWorldSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetWorldSize(IntPtr ptr, Mogre.Vector3 worldSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_GetForceFieldType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ForceField.ForceFieldTypes ForceField_GetForceFieldType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ForceField_SetForceFieldType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ForceField_SetForceFieldType(IntPtr ptr, ForceField.ForceFieldTypes forceFieldType);
        #endregion


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
            ForceField_Destroy(NativePointer);
            forceFieldInstances.Remove(nativePtr);
        }

        #endregion

    }
}
