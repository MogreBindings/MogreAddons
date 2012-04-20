using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using MParticleUniverse.ParticleObservers;

namespace MParticleUniverse
{
    /// <summary>
    /// ParticleObservers are used to observe whether a certain condition occurs. This condition is often related to 
	///	the state of a Particle, but also certain situations regarding a ParticleTechnique, ParticleEmitter or even
	///	the ParticleSystem can be validated.
	/// <remarks>
	///	ParticleEventHandlers can be added to a ParticleObserve to handle the condition that is registered by the
	///	ParticleObserver. This mechanism provides a extendable framework for determination of events and processing
	///	these events.
    ///	</remarks>
	/// <para>
	///	ParticleObservers are defined on the same level as a ParticleEmitter and not as part of a ParticleEmitter. 
	///	This is because the ParticleObserver observes ALL particles in the ParticleTechniques´ Particle pool.
    ///	</para>
    /// <para>
	///	A ParticleObserver can contain one or more ParticleEventHandlers.
    ///	</para>
    /// </summary>
    public abstract unsafe class ParticleObserver : IAlias, IElement, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal ParticleObserver(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        internal static ParticleObserver GetObserverByPtr(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;

            String observerType = Marshal.PtrToStringAnsi(ParticleObserver_GetObserverType(ptr));
            switch (observerType)
            {
                case "OnClear":
                    return OnClearObserver.GetInstance(ptr);
                case "OnCollision":
                    return OnCollisionObserver.GetInstance(ptr);
                case "OnCount":
                    return OnCountObserver.GetInstance(ptr);
                case "OnEmission":
                    return OnEmissionObserver.GetInstance(ptr);
                case "OnEventFlag":
                    return OnEventFlagObserver.GetInstance(ptr);
                case "OnExpire":
                    return OnExpireObserver.GetInstance(ptr);
                case "OnPosition":
                    return OnPositionObserver.GetInstance(ptr);
                case "OnQuota":
                    return OnQuotaObserver.GetInstance(ptr);
                case "OnRandom":
                    return OnRandomObserver.GetInstance(ptr);
                case "OnTime":
                    return OnTimeObserver.GetInstance(ptr);
                case "OnVelocity":
                    return OnVelocityObserver.GetInstance(ptr);
            }
            return null;
        }

        internal static ParticleObserver GetObserverByPtr(IntPtr ptr, String observerType)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            
            //String observerType = ParticleObserver_GetObserverType(ptr);
            switch (observerType)
            {
                case "OnClear":
                    return OnClearObserver.GetInstance(ptr);
                case "OnCollision":
                    return OnCollisionObserver.GetInstance(ptr);
                case "OnCount":
                    return OnCountObserver.GetInstance(ptr);
                case "OnEmission":
                    return OnEmissionObserver.GetInstance(ptr);
                case "OnEventFlag":
                    return OnEventFlagObserver.GetInstance(ptr);
                case "OnExpire":
                    return OnExpireObserver.GetInstance(ptr);
                case "OnPosition":
                    return OnPositionObserver.GetInstance(ptr);
                case "OnQuota":
                    return OnQuotaObserver.GetInstance(ptr);
                case "OnRandom":
                    return OnRandomObserver.GetInstance(ptr);
                case "OnTime":
                    return OnTimeObserver.GetInstance(ptr);
                case "OnVelocity":
                    return OnVelocityObserver.GetInstance(ptr);
            }
            return null;
        }

        #region IAlias Implementation
        public AliasType AliasType
        {
            get { return IAlias_GetAliasType(NativePointer); }
            set { IAlias_SetAliasType(NativePointer, value); }
        }
        public String AliasName
        {
            get { return Marshal.PtrToStringAnsi(IAlias_GetAliasName(NativePointer)); }
            set { IAlias_SetAliasName(NativePointer, value); }
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern AliasType IAlias_GetAliasType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasType(IntPtr ptr, AliasType aliasType);


        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr IAlias_GetAliasName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string aliasName);

        #endregion

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
            ParticleObserver_Destroy(NativePointer);
        }

        #endregion


        public  bool DEFAULT_ENABLED = true;
        public  Particle.ParticleType DEFAULT_PARTICLE_TYPE = Particle.ParticleType.PT_VISUAL;
        public  float DEFAULT_INTERVAL = 0.05f;
        public  bool DEFAULT_UNTIL_EVENT = false;

        /// <summary>
        /// Todo
        /// </summary>
        public String ObserverType { get { return Marshal.PtrToStringAnsi(ParticleObserver_GetObserverType(nativePtr)); } set { ParticleObserver_SetObserverType(nativePtr, value); } }

        /// <summary>
        /// Todo
        /// </summary>
        public bool Enabled { get { return ParticleObserver_IsEnabled(nativePtr); } set { ParticleObserver_SetEnabled(nativePtr, value); } }

        /// <summary>
        /// Returns the 'enabled' value that was set in setEnabled() and not altered during execution.
        /// </summary>
        /// <returns></returns>
        public bool _getOriginalEnabled()
        {
            return ParticleObserver__getOriginalEnabled(nativePtr);
        }

        /// <summary>
        /// Reset internal values for 'enabled'. This means that both the mEnabled and mOriginalEnabled can be set again using setEnabled.
        ///	<remarks>
        ///		Using _resetEnabled() makes it possible to use setEnabled() without the restriction of having a fixed mOriginalEnabled value.
        /// </remarks>
        /// </summary>
        public void _resetEnabled()
        {
            ParticleObserver__resetEnabled(nativePtr);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public ParticleTechnique ParentTechnique { 
            get { return ParticleTechnique.GetInstances( ParticleObserver_GetParentTechnique(nativePtr)); } 
            set 
            { 
                if (value == null)
                    ParticleObserver_SetParentTechnique(nativePtr, IntPtr.Zero); 
                else
                    ParticleObserver_SetParentTechnique(nativePtr, value.nativePtr); 
            } 
        }

        /// <summary>
        /// Todo
        /// </summary>
        public String Name { get { return Marshal.PtrToStringAnsi(ParticleObserver_GetName(nativePtr)); } set { ParticleObserver_SetName(nativePtr, value); } }

        /// <summary>
        /// Todo
        /// </summary>
        public Particle.ParticleType ParticleTypeToObserve { get { return ParticleObserver_GetParticleTypeToObserve(nativePtr); } set { ParticleObserver_SetParticleTypeToObserve(nativePtr, value); } }

        /// <summary>
        /// Perform activities when a ParticleTechnique is started.
        /// </summary>
        public void _notifyStart()
        {
            ParticleObserver__notifyStart(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleTechnique is stopped.
        /// </summary>
        public void _notifyStop()
        {
            ParticleObserver__notifyStop(nativePtr);
        }

        /// <summary>
        /// Notify that the Particle System is rescaled.
        /// </summary>
        /// <param name="scale"></param>
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            ParticleObserver__notifyRescaled(nativePtr, scale);
        }

        /// <summary>
        /// Perform activities before the individual particles are processed.
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleObserver__preProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// Executes the ParticleObserver.
        ///	<remarks>
        ///		This function calls the _observe() function to determine whether the event must be handled or not.
        ///	</remarks>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="firstParticle"></param>
        public void _processParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed, bool firstParticle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleObserver__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        /// <summary>
        /// Perform precalculations if the first Particle in the update-loop is processed.
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        public void _firstParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleObserver__firstParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Perform activities after the individual particles are processed.
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleObserver__postProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// This function determines whether a condition (the event) is true or false.
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        /// <returns></returns>
        public bool _observe(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return ParticleObserver__observe(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="eventHandlerType"></param>
        /// <returns></returns>
        public ParticleEventHandler CreateEventHandler(String eventHandlerType)
        {
            return ParticleEventHandler.GetEventHandlerFromPtr(ParticleObserver_CreateEventHandler(nativePtr, eventHandlerType), eventHandlerType);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="eventHandler"></param>
        public void AddEventHandler(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            ParticleObserver_AddEventHandler(nativePtr, eventHandler.nativePtr);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="eventHandler"></param>
        public void RemoveEventHandler(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            ParticleObserver_RemoveEventHandler(nativePtr, eventHandler.nativePtr);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ParticleEventHandler GetEventHandler(uint index)
        {
            return ParticleEventHandler.GetEventHandlerFromPtr(ParticleObserver_GetEventHandler(nativePtr, index));
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="eventHandlerName"></param>
        /// <returns></returns>
        public ParticleEventHandler GetEventHandler(String eventHandlerName)
        {
            return ParticleEventHandler.GetEventHandlerFromPtr(ParticleObserver_GetEventHandler(nativePtr, eventHandlerName));
        }

        /// <summary>
        /// Todo
        /// </summary>
        public uint NumEventHandlers { get { return ParticleObserver_GetNumEventHandlers(nativePtr); } }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="eventHandler"></param>
        public void DestroyEventHandler(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            ParticleObserver_DestroyEventHandler(nativePtr, eventHandler.nativePtr);
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="index"></param>
        public void DestroyEventHandler(uint index)
        {
            ParticleObserver_DestroyEventHandler(nativePtr, index);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public void DestroyAllEventHandlers()
        {
            ParticleObserver_DestroyAllEventHandlers(nativePtr);
        }

        /// <summary>
        /// Copy attributes to another observer.
        /// </summary>
        /// <param name="observer"></param>
        public void CopyAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            ParticleObserver_CopyAttributesTo(nativePtr, observer.nativePtr);
        }

        /// <summary>
        /// Copy parent attributes to another observer.
        /// </summary>
        /// <param name="observer"></param>
        public void CopyParentAttributesTo(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            ParticleObserver_CopyParentAttributesTo(nativePtr, observer.nativePtr);
        }

        /// <summary>
        /// Set the interval value, which defines at what interval the observer is called.
        /// Return the interval value, which defines at what interval the observer is called.
        /// </summary>
        public float ObserverInterval { get { return ParticleObserver_GetObserverInterval(nativePtr); } set { ParticleObserver_SetObserverInterval(nativePtr, value); } }

        /// <summary>
        /// Sets the value of mObserveUntilEvent. This value determines whether observation must be continued
        ///		after an event ocurred and the event handlers are called.
        /// Return the value of mObserveUntilEvent.
        /// </summary>
        public bool ObserveUntilEvent { get { return ParticleObserver_GetObserveUntilEvent(nativePtr); } set { ParticleObserver_SetObserveUntilEvent(nativePtr, value); } }

        /// <summary>
        /// Returns true if the particle type to observe specifically has been set.
        /// </summary>
        public bool ParticleTypeToObserveSet { get { return ParticleObserver_IsParticleTypeToObserveSet(nativePtr); } }


        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetObserverType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleObserver_GetObserverType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetObserverType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetObserverType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string observerType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_IsEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleObserver_IsEnabled(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__getOriginalEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleObserver__getOriginalEnabled(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetEnabled(IntPtr ptr, bool enabled);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__resetEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__resetEnabled(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleObserver_GetParentTechnique(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleObserver_GetName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetParticleTypeToObserve", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Particle.ParticleType ParticleObserver_GetParticleTypeToObserve(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetParticleTypeToObserve", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetParticleTypeToObserve(IntPtr ptr, Particle.ParticleType particleTypeToObserve);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__notifyStop(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__processParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed, bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__firstParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__firstParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver__postProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver__observe", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleObserver__observe(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_CreateEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleObserver_CreateEventHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string eventHandlerType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_AddEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_AddEventHandler(IntPtr ptr, IntPtr eventHandler);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_RemoveEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_RemoveEventHandler(IntPtr ptr, IntPtr eventHandler);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleObserver_GetEventHandler(IntPtr ptr, uint index);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleObserver_GetEventHandler(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string eventHandlerName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetNumEventHandlers", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleObserver_GetNumEventHandlers(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_DestroyEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_DestroyEventHandler(IntPtr ptr, IntPtr eventHandler);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_DestroyEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_DestroyEventHandler(IntPtr ptr, uint index);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_DestroyAllEventHandlers", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_DestroyAllEventHandlers(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_CopyAttributesTo(IntPtr ptr, IntPtr observer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_CopyParentAttributesTo(IntPtr ptr, IntPtr observer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetObserverInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleObserver_GetObserverInterval(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetObserverInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetObserverInterval(IntPtr ptr, float observerInterval);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_GetObserveUntilEvent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleObserver_GetObserveUntilEvent(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_SetObserveUntilEvent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleObserver_SetObserveUntilEvent(IntPtr ptr, bool observeUntilEvent);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleObserver_IsParticleTypeToObserveSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleObserver_IsParticleTypeToObserveSet(IntPtr ptr);
        #endregion
    }
}
