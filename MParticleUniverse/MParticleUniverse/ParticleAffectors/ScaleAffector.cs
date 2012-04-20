using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Scales a particle. This can be a linear scale, but scaling that changes over time is possible.
    /// </summary>
    public class ScaleAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ScaleAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_X_SCALE = 1.0f;
        public const float DEFAULT_Y_SCALE = 1.0f;
        public const float DEFAULT_Z_SCALE = 1.0f;
        public const float DEFAULT_XYZ_SCALE = 1.0f;


        internal static Dictionary<IntPtr, ScaleAffector> affectorInstances;

        internal static ScaleAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, ScaleAffector>();

            ScaleAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ScaleAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public ScaleAffector()
            : base(ScaleAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            ScaleAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ScaleAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** 
        */
        public DynamicAttribute DynScaleX {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ScaleAffector_GetDynScaleX(nativePtr)); } 
            set { 
                if (value == null)
                    ScaleAffector_SetDynScaleX(nativePtr, IntPtr.Zero); 
                else
                    ScaleAffector_SetDynScaleX(nativePtr, value.NativePointer); 
            } }
        public void ResetDynScaleX(bool resetToDefault = true)
        { ScaleAffector_ResetDynScaleX(nativePtr, resetToDefault); }

        /** 
        */
        public DynamicAttribute DynScaleY { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ScaleAffector_GetDynScaleY(nativePtr)); }
            set
            {
                if (value == null)
                    ScaleAffector_SetDynScaleY(nativePtr, IntPtr.Zero);
                else
                    ScaleAffector_SetDynScaleY(nativePtr, value.NativePointer);
            }
        }
        public void ResetDynScaleY(bool resetToDefault = true)
        { ScaleAffector_ResetDynScaleY(nativePtr, resetToDefault); }

        /** 
        */
        public DynamicAttribute DynScaleZ
        {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ScaleAffector_GetDynScaleZ(nativePtr)); }
            set
            {
                if (value == null)
                    ScaleAffector_SetDynScaleZ(nativePtr, IntPtr.Zero);
                else
                    ScaleAffector_SetDynScaleZ(nativePtr, value.NativePointer);
            }
        }
        public void ResetDynScaleZ(bool resetToDefault = true)
        { ScaleAffector_ResetDynScaleZ(nativePtr, resetToDefault); }

        /** 
        */
        public DynamicAttribute DynScaleXYZ { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ScaleAffector_GetDynScaleXYZ(nativePtr)); } 
            set { if (value == null)
                ScaleAffector_SetDynScaleXYZ(nativePtr, IntPtr.Zero);
                else
                    ScaleAffector_SetDynScaleXYZ(nativePtr, value.NativePointer); } }
        public void ResetDynScaleXYZ(bool resetToDefault = true)
        { ScaleAffector_ResetDynScaleXYZ(nativePtr, resetToDefault); }

        /** 
        */
        public bool SinceStartSystem { get { return ScaleAffector_IsSinceStartSystem(nativePtr); } set { ScaleAffector_SetSinceStartSystem(nativePtr, value); } }



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
            ScaleAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_GetDynScaleX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleAffector_GetDynScaleX(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_SetDynScaleX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_SetDynScaleX(IntPtr ptr, IntPtr dynScaleX);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_ResetDynScaleX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_ResetDynScaleX(IntPtr ptr, bool resetToDefault = true);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_GetDynScaleY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleAffector_GetDynScaleY(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_SetDynScaleY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_SetDynScaleY(IntPtr ptr, IntPtr dynScaleY);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_ResetDynScaleY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_ResetDynScaleY(IntPtr ptr, bool resetToDefault = true);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_GetDynScaleZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleAffector_GetDynScaleZ(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_SetDynScaleZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_SetDynScaleZ(IntPtr ptr, IntPtr dynScaleZ);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_ResetDynScaleZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_ResetDynScaleZ(IntPtr ptr, bool resetToDefault = true);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_GetDynScaleXYZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScaleAffector_GetDynScaleXYZ(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_SetDynScaleXYZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_SetDynScaleXYZ(IntPtr ptr, IntPtr dynScaleXYZ);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_ResetDynScaleXYZ", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_ResetDynScaleXYZ(IntPtr ptr, bool resetToDefault = true);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_IsSinceStartSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ScaleAffector_IsSinceStartSystem(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScaleAffector_SetSinceStartSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScaleAffector_SetSinceStartSystem(IntPtr ptr, bool sinceStartSystem);
        #endregion

    }
}
