using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleBehaviours
{
    /// <summary>
    /// The SlaveBehaviour makes the particle act as a slave, so it follows another particle to which it is related.
    /// </summary>
    public class SlaveBehaviour : ParticleBehaviour, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, SlaveBehaviour> behaviourInstances;

        internal static SlaveBehaviour GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (behaviourInstances == null)
                behaviourInstances = new Dictionary<IntPtr, SlaveBehaviour>();

            SlaveBehaviour newvalue;

            if (behaviourInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SlaveBehaviour(ptr);
            behaviourInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal SlaveBehaviour(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        // Constants
        public Particle MasterParticle { get { return Particle.GetInstance(SlaveBehaviour_GetMasterParticle(nativePtr)); } set { SlaveBehaviour_SetMasterParticle(nativePtr, value.NativePointer); } }

        public SlaveBehaviour()
            : base(SlaveBehaviour_New())
        {
            nativePtr = base.nativePtr;
            behaviourInstances.Add(nativePtr, this);
        }

        public void _processParticle(ParticleTechnique technique, Particle particle, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SlaveBehaviour__processParticle(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// <see cref="ParticleBehaviour.CopyAttributesTo"/>
        /// </summary>
        /// <param name="affector"></param>
        public void CopyAttributesTo(ParticleBehaviour affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            SlaveBehaviour_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveBehaviour_SetMasterParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveBehaviour_SetMasterParticle(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveBehaviour_GetMasterParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SlaveBehaviour_GetMasterParticle(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveBehaviour_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SlaveBehaviour_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveBehaviour_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveBehaviour_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveBehaviour__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveBehaviour__processParticle(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveBehaviour_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveBehaviour_CopyAttributesTo(IntPtr ptr, IntPtr affector);

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
            SlaveBehaviour_Destroy(NativePointer);
            behaviourInstances.Remove(nativePtr);
        }

        #endregion

    }
}
