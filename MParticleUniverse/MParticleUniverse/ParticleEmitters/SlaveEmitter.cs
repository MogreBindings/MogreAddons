using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The SlaveEmitter makes the particle act as a slave, so it follows another particle to which it is related. This only applies
    ///	at the creation (initialisation) of the particle. For further slave behaviour during the lifespan of the particle, the 
    ///	SlaveBehaviour must be used also.
    /// </summary>
    public class SlaveEmitter : ParticleEmitter, TechniqueListener, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, SlaveEmitter> emitterInstances;

        internal static SlaveEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, SlaveEmitter>();

            SlaveEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SlaveEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal SlaveEmitter(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
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
            SlaveEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public SlaveEmitter()
            : base(SlaveEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }

        /** 
        */
        public String MasterTechniqueName { get { return Marshal.PtrToStringAnsi(SlaveEmitter_GetMasterTechniqueName(nativePtr)); } set { SlaveEmitter_SetMasterTechniqueName(nativePtr, value); } }
        /** 
        */
        public String MasterEmitterName { get { return Marshal.PtrToStringAnsi(SlaveEmitter_GetMasterEmitterName(nativePtr)); } set { SlaveEmitter_SetMasterEmitterName(nativePtr, value); } }

        /** See ParticleEmitter.
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SlaveEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** See ParticleEmitter.
        */
        public void _initParticleDirection(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SlaveEmitter__initParticleDirection(nativePtr, particle.NativePointer);
        }

        /** See ParticleEmitter.
        */
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            SlaveEmitter__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /** See ParticleEmitter.
        */
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            SlaveEmitter__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        /** See ParticleEmitter.
        */
        public void _notifyStart()
        {
            SlaveEmitter__notifyStart(nativePtr);
        }

        /** Initialise the emitted particle. This means that its position is set.
        */
        public void ParticleEmitted(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SlaveEmitter_ParticleEmitted(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }

        /** No implementation.
        */
        public void ParticleExpired(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SlaveEmitter_ParticleExpired(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }


        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            SlaveEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }

        #region SlaveEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SlaveEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_GetMasterTechniqueName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SlaveEmitter_GetMasterTechniqueName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_SetMasterTechniqueName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter_SetMasterTechniqueName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string masterTechniqueName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_GetMasterEmitterName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SlaveEmitter_GetMasterEmitterName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter__initParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter__initParticleDirection(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_ParticleEmitted", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter_ParticleEmitted(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_ParticleExpired", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter_ParticleExpired(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_SetMasterEmitterName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter_SetMasterEmitterName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string masterEmitterName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SlaveEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SlaveEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
}
