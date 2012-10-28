using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEventHandlers
{
    /// <summary>
    /// This class 'sticks' one or more particles to the position of a particle that is passed through the 
    ///	DoPlacementParticleEventHandler.
    /// <remarks>
    ///	The position of each particle that is passed through the DoPlacementParticleEventHandler is used to emit
    ///	one or more particles on that same position. The ParticleEmitter that is used for emitting the particle(s)
    ///	is searched (once) by means of its name.
    ///	</remarks>
    /// <par>
    ///	This class uses a 'TechniqueListener' to pass the newly created particle from the ParticleTechnique to the 
    ///	DoPlacementParticleEventHandler, where it is initialised.
    ///	</par>
    /// </summary>
    public class DoPlacementParticleEventHandler : ParticleEventHandler, TechniqueListener, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, DoPlacementParticleEventHandler> eventHandlerInstances;

        internal static DoPlacementParticleEventHandler GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (eventHandlerInstances == null)
                eventHandlerInstances = new Dictionary<IntPtr, DoPlacementParticleEventHandler>();

            DoPlacementParticleEventHandler newvalue;

            if (eventHandlerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new DoPlacementParticleEventHandler(ptr);
            eventHandlerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal DoPlacementParticleEventHandler(IntPtr ptr)
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
        protected void Dispose(bool disposing)
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
            DoPlacementParticleEventHandler_Destroy(NativePointer);
            eventHandlerInstances.Remove(nativePtr);
        }

        #endregion

        public const uint DEFAULT_NUMBER_OF_PARTICLES = 1;

        public DoPlacementParticleEventHandler()
            : base(DoPlacementParticleEventHandler_New())
        {
            nativePtr = base.nativePtr;
            eventHandlerInstances.Add(nativePtr, this);
        }
        /** Getters/Setters
        */
        public bool InheritPosition { get { return DoPlacementParticleEventHandler_IsInheritPosition(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritPosition(nativePtr, value); } }
        public bool InheritDirection { get { return DoPlacementParticleEventHandler_IsInheritDirection(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritDirection(nativePtr, value); } }
        public bool InheritOrientation { get { return DoPlacementParticleEventHandler_IsInheritOrientation(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritOrientation(nativePtr, value); } }
        public bool InheritTimeToLive { get { return DoPlacementParticleEventHandler_IsInheritTimeToLive(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritTimeToLive(nativePtr, value); } }
        public bool InheritMass { get { return DoPlacementParticleEventHandler_IsInheritMass(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritMass(nativePtr, value); } }
        public bool InheritTextureCoordinate { get { return DoPlacementParticleEventHandler_IsInheritTextureCoordinate(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritTextureCoordinate(nativePtr, value); } }
        public bool InheritColour { get { return DoPlacementParticleEventHandler_IsInheritColour(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritColour(nativePtr, value); } }
        public bool InheritParticleWidth { get { return DoPlacementParticleEventHandler_IsInheritParticleWidth(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritParticleWidth(nativePtr, value); } }
        public bool InheritParticleHeight { get { return DoPlacementParticleEventHandler_IsInheritParticleHeight(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritParticleHeight(nativePtr, value); } }
        public bool InheritParticleDepth { get { return DoPlacementParticleEventHandler_IsInheritParticleDepth(nativePtr); } set { DoPlacementParticleEventHandler_SetInheritParticleDepth(nativePtr, value); } }

        /** Get the name of the emitter that is used to emit its particles.
        Set the name of the emitter that is used to emit its particles.
        */
        public String ForceEmitterName { get { return Marshal.PtrToStringAnsi(DoPlacementParticleEventHandler_GetForceEmitterName(nativePtr)); } set { DoPlacementParticleEventHandler_SetForceEmitterName(nativePtr, value); } }

        /** Returns a pointer to the emitter that is used as a force emitter.
        */
        public ParticleEmitter GetForceEmitter()
        {
            return ParticleEmitter.GetParticleEmitterByPointer(DoPlacementParticleEventHandler_GetForceEmitter(nativePtr));
        }

        /** Remove this as a listener from the technique.
        @remarks
            If a new force-emitter name has been set, the removeAsListener must be called, to remove the DoPlacementParticleEventHandler
            from the old technique (to which the force-emitter belongs. Only then the new force-emitter is used. 
            The reason why it is not called automatically in the setForceEmitterName() funtion is to offer some flexibility on 
            the moment the removeAsListener() is called.
        */
        public void RemoveAsListener()
        {
            DoPlacementParticleEventHandler_RemoveAsListener(nativePtr);
        }

        /** Get the number of particles to emit.
        Set the number of particles to emit.
        */
        public uint NumberOfParticles { get { return DoPlacementParticleEventHandler_GetNumberOfParticles(nativePtr); } set { DoPlacementParticleEventHandler_SetNumberOfParticles(nativePtr, value); } }

        /** Boolean that determines whether always the position of the particle that is handled must be used for emission of 
            the new particle.
        */
        /** Set the boolean to indicate whether the position of the particle that is handled must be used for emission of 
            the new particle or whether the contact point of the physics actor must be used. This only applies if a physics angine
            is used, otherwise the default is used.
        */
        public bool AlwaysUsePosition { get { return DoPlacementParticleEventHandler_AlwaysUsePosition(nativePtr); } set { DoPlacementParticleEventHandler_SetAlwaysUsePosition(nativePtr, value); } }

        /** If the _handle() function of this class is invoked (by an Observer), it searches the 
            ParticleEmitter defined by the mForceEmitterName. This ParticleEmitter is either part of 
            the ParticleTechnique in which the DoPlacementParticleEventHandler is defined, and if the ParticleEmitter
            is not found, other ParticleTechniques are searched. The ParticleEmitter is 'forced' to emit the
            requested number of particles.
        */
        public void _handle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoPlacementParticleEventHandler__handle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /** Initialise the emitted particle. This means that its position is set.
        */
        public void ParticleEmitted(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoPlacementParticleEventHandler_ParticleEmitted(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }

        /** No implementation.
        */
        public void ParticleExpired(ParticleTechnique particleTechnique, Particle particle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            DoPlacementParticleEventHandler_ParticleExpired(nativePtr, particleTechnique.nativePtr, particle.NativePointer);
        }

        /** Copy attributes to another event handler.
        */
        public void CopyAttributesTo(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            DoPlacementParticleEventHandler_CopyAttributesTo(nativePtr, eventHandler.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoPlacementParticleEventHandler_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritOrientation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritTimeToLive(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritMass(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritTextureCoordinate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritTextureCoordinate(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritParticleWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritParticleWidth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritParticleHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritParticleHeight(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_IsInheritParticleDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_IsInheritParticleDepth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritPosition(IntPtr ptr, bool inheritPosition);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritDirection(IntPtr ptr, bool inheritDirection);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritOrientation(IntPtr ptr, bool inheritOrientation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritTimeToLive(IntPtr ptr, bool inheritTimeToLive);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritMass(IntPtr ptr, bool inheritMass);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritTextureCoordinate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritTextureCoordinate(IntPtr ptr, bool inheritTextureCoordinate);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritColour(IntPtr ptr, bool inheritColour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritParticleWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritParticleWidth(IntPtr ptr, bool inheritParticleWidth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritParticleHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritParticleHeight(IntPtr ptr, bool inheritParticleHeight);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetInheritParticleDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetInheritParticleDepth(IntPtr ptr, bool inheritParticleDepth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_GetForceEmitterName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoPlacementParticleEventHandler_GetForceEmitterName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetForceEmitterName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetForceEmitterName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string forceEmitterName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_GetForceEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DoPlacementParticleEventHandler_GetForceEmitter(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_RemoveAsListener", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_RemoveAsListener(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_GetNumberOfParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint DoPlacementParticleEventHandler_GetNumberOfParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetNumberOfParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetNumberOfParticles(IntPtr ptr, uint numberOfParticles);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_AlwaysUsePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool DoPlacementParticleEventHandler_AlwaysUsePosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_SetAlwaysUsePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_SetAlwaysUsePosition(IntPtr ptr, bool alwaysUsePosition);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler__handle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler__handle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_ParticleEmitted", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_ParticleEmitted(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_ParticleExpired", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_ParticleExpired(IntPtr ptr, IntPtr particleTechnique, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DoPlacementParticleEventHandler_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DoPlacementParticleEventHandler_CopyAttributesTo(IntPtr ptr, IntPtr eventHandler);
        #endregion
    }
}
