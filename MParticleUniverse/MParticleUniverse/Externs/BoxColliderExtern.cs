using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MParticleUniverse.ParticleAffectors;
using System.Runtime.InteropServices;

namespace MParticleUniverse.Externs
{
    public class BoxColliderExtern : Attachable, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        private BoxCollider parent;

        internal BoxColliderExtern(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }
        internal static Dictionary<IntPtr, BoxColliderExtern> externInstances;

        internal static BoxColliderExtern GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (externInstances == null)
                externInstances = new Dictionary<IntPtr, BoxColliderExtern>();

            BoxColliderExtern newvalue;

            if (externInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BoxColliderExtern(ptr);
            externInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public BoxColliderExtern()
            : base(BoxColliderExtern_New())
        {
            nativePtr = base.nativePtr;
            externInstances.Add(nativePtr, this);
        }

        /** see Extern::_preProcessParticles */
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            BoxColliderExtern__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /** see Extern::_interface */
        public void _interface(ParticleTechnique technique,
                Particle particle,
                float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BoxColliderExtern__interface(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy both the Extern and the derived BoxCollider properties.
        */
        public void CopyAttributesTo(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            BoxColliderExtern_CopyAttributesTo(nativePtr, externObject.NativePointer);
        }

        #region BoxColliderExtern
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxColliderExtern_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxColliderExtern_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxColliderExtern_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxColliderExtern_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxColliderExtern__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxColliderExtern__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxColliderExtern__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxColliderExtern__interface(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxColliderExtern_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxColliderExtern_CopyAttributesTo(IntPtr ptr, IntPtr externObject);
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
            BoxColliderExtern_Destroy(NativePointer);
            externInstances.Remove(nativePtr);
        }

        #endregion

    }
}
