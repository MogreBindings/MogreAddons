using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MParticleUniverse.ParticleAffectors;
using System.Runtime.InteropServices;

namespace MParticleUniverse.Externs
{
    public class GravityExtern : Attachable, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        private GravityAffector parent;

        internal static Dictionary<IntPtr, GravityExtern> externInstances;

        internal static GravityExtern GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (externInstances == null)
                externInstances = new Dictionary<IntPtr, GravityExtern>();

            GravityExtern newvalue;

            if (externInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new GravityExtern(ptr);
            externInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal GravityExtern(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public GravityExtern()
            : base(GravityExtern_New())
        {
            nativePtr = base.nativePtr;
            externInstances.Add(nativePtr, this);
        }

        /** The _preProcessParticles() function sets the position and mDerivedPosition attributes to
            the actual world position of the node to which it is attached.
        */
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            GravityExtern__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /** Processes a particle.
        */
        public void _interface(ParticleTechnique technique,
                Particle particle,
                float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            GravityExtern__interface(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy both the Extern and the derived GravityAffector properties.
        */
        public void CopyAttributesTo(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            GravityExtern_CopyAttributesTo(nativePtr, externObject.NativePointer);
        }

        #region GravityExtern
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityExtern_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr GravityExtern_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityExtern_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityExtern_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityExtern__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityExtern__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityExtern__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityExtern__interface(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "GravityExtern_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void GravityExtern_CopyAttributesTo(IntPtr ptr, IntPtr externObject);
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
            GravityExtern_Destroy(NativePointer);
            externInstances.Remove(nativePtr);
        }

        #endregion

    }
}
