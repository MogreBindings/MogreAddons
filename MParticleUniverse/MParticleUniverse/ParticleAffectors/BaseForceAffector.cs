using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// This is a baseclass for other Force Affector classes.
    /// </summary>
    public abstract unsafe class BaseForceAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        public enum ForceApplications
        {
            FA_AVERAGE,
            FA_ADD
        };

        public static Mogre.Vector3 DEFAULT_FORCE_VECTOR { get { return new Mogre.Vector3(0, 0, 0); } }
        public const ForceApplications DEFAULT_FORCE_APPL = ForceApplications.FA_ADD;

        internal BaseForceAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        /// <summary>
        /// <see cref="ParticleAffector.CopyAttributesTo"/>
        /// </summary>
        /// <param name="affector"></param>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            BaseForceAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** 
        */
        public Mogre.Vector3 ForceVector
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)BaseForceAffector_GetForceVector(nativePtr).ToPointer()));
                return vec;
            }
            set { BaseForceAffector_SetForceVector(nativePtr, value); }
        }

        /** 
        */
        public ForceApplications ForceApplication
        {
            get
            {
                return BaseForceAffector_GetForceApplication(nativePtr);
            }
            set { BaseForceAffector_SetForceApplication(nativePtr, value); }
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
            BaseForceAffector_Destroy(NativePointer);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseForceAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseForceAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseForceAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseForceAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseForceAffector_GetForceVector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BaseForceAffector_GetForceVector(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseForceAffector_SetForceVector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseForceAffector_SetForceVector(IntPtr ptr, Mogre.Vector3 forceVector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseForceAffector_GetForceApplication", CallingConvention = CallingConvention.Cdecl)]
        internal static extern BaseForceAffector.ForceApplications BaseForceAffector_GetForceApplication(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseForceAffector_SetForceApplication", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseForceAffector_SetForceApplication(IntPtr ptr, BaseForceAffector.ForceApplications forceApplication);

        #endregion
    }
}
