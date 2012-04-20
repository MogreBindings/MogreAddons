using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// The CameraDependency class is used to define a relation between an attribute (for example the emission
	///	rate of a ParticleEmitter) and the camera. The camera distance influences the value of the attribute.
	/// <remarks>
	///	In case of the emission rate for example, it can be defined that that number of emitted particles 
	///	decreases if the camera gets further away.
    ///	</remarks>
    /// </summary>
    public class CameraDependency : IDependency, IDisposable
    {
        public IntPtr NativePointer { get { return nativePtr; } set { nativePtr = value; } }

        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, CameraDependency> cameraDependencyInstances;

        internal static CameraDependency GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (cameraDependencyInstances == null)
                cameraDependencyInstances = new Dictionary<IntPtr, CameraDependency>();

            CameraDependency newvalue;

            if (cameraDependencyInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new CameraDependency(ptr);
            cameraDependencyInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal CameraDependency(IntPtr ptr)
        {
            NativePointer = ptr;
        }

        public const float DEFAULT_DISTANCE_THRESHOLD = 1000000.0f;
        public const bool DEFAULT_INCREASE = false;

        public CameraDependency()
        {
            nativePtr = CameraDependency_New();
            cameraDependencyInstances.Add(nativePtr, this);
        }

        public CameraDependency(float threshold, bool inc)
        {
            nativePtr = CameraDependency_New(threshold, inc);
            cameraDependencyInstances.Add(nativePtr, this);
        }

        /** Todo
        */
        public bool Affect(float baseValue, float dependencyValue)
        {
            return CameraDependency_Affect(nativePtr, baseValue, dependencyValue);
        }

        /** Todo
        */
        public float Threshold { get { return CameraDependency_GetThreshold(nativePtr); } set { CameraDependency_SetThreshold(nativePtr, value); } }

        /** Todo
        */
        public bool Increase { get { return CameraDependency_IsIncrease(nativePtr); } set { CameraDependency_SetIncrease(nativePtr, value); } }

        /** Copy attributes to another camera dependency.
        */
        public void CopyAttributesTo(CameraDependency cameraDependency)
        {
            if (cameraDependency == null)
                throw new ArgumentNullException("cameraDependency cannot be null!");
            CameraDependency_CopyAttributesTo(nativePtr, cameraDependency.NativePointer);
        }

        #region PInvoke

        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CameraDependency_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CameraDependency_New(float threshold, bool inc);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CameraDependency_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_Affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CameraDependency_Affect(IntPtr ptr, float baseValue, float dependencyValue);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_GetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float CameraDependency_GetThreshold(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_SetThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CameraDependency_SetThreshold(IntPtr ptr, float threshold);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_IsIncrease", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool CameraDependency_IsIncrease(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_SetIncrease", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CameraDependency_SetIncrease(IntPtr ptr, bool increase);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CameraDependency_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CameraDependency_CopyAttributesTo(IntPtr ptr, IntPtr cameraDependency);
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
            CameraDependency_Destroy(NativePointer);
            cameraDependencyInstances.Remove(nativePtr);
        }

        #endregion

    }
}
