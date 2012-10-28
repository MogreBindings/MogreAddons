using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MParticleUniverse.ParticleAffectors;
using System.Runtime.InteropServices;

namespace MParticleUniverse.Externs
{
    /// <summary>
    /// The VortexExtern is a wrapper of the VortexAffector, adding the functionality of a MovableObject.
    ///	The VortexExtern can be attached to another SceneNode than the one where the ParticleSystem at 
    ///	which the VortexExtern is registered, is attached. This makes it possible to affect the particles 
    ///	in the Particle System, while both SceneNodes moves different from each other. This approach makes
    ///	it possible to simulate something like a helicopter (SceneNode to which the VortexExtern is 
    ///	attached) that flies over a certain area and rotates the leaves on the ground (Particle System attached
    ///	to another SceneNode).
    /// </summary>
    public class VortexExtern : Attachable, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        private VortexAffector parent;

        internal static Dictionary<IntPtr, VortexExtern> externInstances;

        internal static VortexExtern GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (externInstances == null)
                externInstances = new Dictionary<IntPtr, VortexExtern>();

            VortexExtern newvalue;

            if (externInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new VortexExtern(ptr);
            externInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal VortexExtern(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }
        public VortexExtern()
            : base(VortexExtern_New())
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
            VortexExtern__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
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
            VortexExtern__interface(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Copy both the Extern and the derived VortexAffector properties.
        */
        public void CopyAttributesTo(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            VortexExtern_CopyAttributesTo(nativePtr, externObject.NativePointer);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexExtern_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VortexExtern_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexExtern_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexExtern_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexExtern__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexExtern__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexExtern__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexExtern__interface(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VortexExtern_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VortexExtern_CopyAttributesTo(IntPtr ptr, IntPtr externCopy);
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
            VortexExtern_Destroy(NativePointer);
            externInstances.Remove(nativePtr);
        }

        #endregion

    }
}
