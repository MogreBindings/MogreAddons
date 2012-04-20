using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Scales the velocity of a particle. This can be a linear scale, but scaling that changes over time alos is possible.
    /// </summary>
    public class ScaleVelocityAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ScaleVelocityAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_VELOCITY_SCALE = 1.0f;


        internal static Dictionary<IntPtr, ScaleVelocityAffector> affectorInstances;

        internal static ScaleVelocityAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, ScaleVelocityAffector>();

            ScaleVelocityAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ScaleVelocityAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public ScaleVelocityAffector()
            : base(ScaleVelocityAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            ScaleVelocityAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ScaleVelocityAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public DynamicAttribute DynScaleVelocity { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ScaleVelocityAffector_GetDynScaleVelocity(nativePtr)); }
            set {
                if (value == null)
                    ScaleVelocityAffector_SetDynScaleVelocity(nativePtr, IntPtr.Zero);
                else
                    ScaleVelocityAffector_SetDynScaleVelocity(nativePtr, value.NativePointer);
            }
        }
        public void ResetDynScaleVelocity(bool resetToDefault = true)
        {
            ScaleVelocityAffector_ResetDynScaleVelocity(nativePtr, resetToDefault);
        }

        /** 
        */
        public bool SinceStartSystem { get { return ScaleVelocityAffector_IsSinceStartSystem(nativePtr); } set { ScaleVelocityAffector_SetSinceStartSystem(nativePtr, value); } }

        /** 
        */
        public bool StopAtFlip { get { return ScaleVelocityAffector_IsStopAtFlip(nativePtr); } set { ScaleVelocityAffector_SetStopAtFlip(nativePtr, value); } }


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
            ScaleVelocityAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleVelocityAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_GetDynScaleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleVelocityAffector_GetDynScaleVelocity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_SetDynScaleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector_SetDynScaleVelocity(IntPtr ptr, IntPtr dynScaleVelocity);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_ResetDynScaleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector_ResetDynScaleVelocity(IntPtr ptr, bool resetToDefault = true);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_IsSinceStartSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ScaleVelocityAffector_IsSinceStartSystem(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_SetSinceStartSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector_SetSinceStartSystem(IntPtr ptr, bool sinceStartSystem);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_IsStopAtFlip", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ScaleVelocityAffector_IsStopAtFlip(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleVelocityAffector_SetStopAtFlip", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleVelocityAffector_SetStopAtFlip(IntPtr ptr, bool stopAtFlip);
        #endregion

    }
}
