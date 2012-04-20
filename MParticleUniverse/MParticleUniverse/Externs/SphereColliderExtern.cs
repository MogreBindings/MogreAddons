using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MParticleUniverse.ParticleAffectors;
using System.Runtime.InteropServices;

namespace MParticleUniverse.Externs
{
    public class SphereColliderExtern : Attachable, IDisposable
    {
        internal IntPtr nativePtr;

        private SphereCollider parent;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, SphereColliderExtern> externInstances;

        internal static SphereColliderExtern GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (externInstances == null)
                externInstances = new Dictionary<IntPtr, SphereColliderExtern>();

            SphereColliderExtern newvalue;

            if (externInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SphereColliderExtern(ptr);
            externInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal SphereColliderExtern(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public SphereColliderExtern()
            : base(SphereColliderExtern_New())
        {
            nativePtr = base.nativePtr;
            externInstances.Add(nativePtr, this);
        }

        /** see Extern::_preProcessParticles */
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            SphereColliderExtern__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /** see Extern::_interface */
        public void _interface(ParticleTechnique technique, Particle particle, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SphereColliderExtern__interface(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy both the Extern and the derived SphereCollider properties.
        */
        public void CopyAttributesTo(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            SphereColliderExtern_CopyAttributesTo(nativePtr, externObject.NativePointer);
        }

        #region SphereColliderExtern
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereColliderExtern_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereColliderExtern_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereColliderExtern_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereColliderExtern_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereColliderExtern__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereColliderExtern__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereColliderExtern__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereColliderExtern__interface(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereColliderExtern_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereColliderExtern_CopyAttributesTo(IntPtr ptr, IntPtr externObject);
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
            SphereColliderExtern_Destroy(NativePointer);
            externInstances.Remove(nativePtr);
        }

        #endregion

    }
}
