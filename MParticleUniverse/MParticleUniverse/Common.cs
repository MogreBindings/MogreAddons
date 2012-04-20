using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public enum ComparisionOperator
	{
		CO_LESS_THAN = 0,
		CO_EQUALS = 1,
		CO_GREATER_THAN = 2
	};

    public enum InterpolationType
	{
		IT_LINEAR = 0,
		IT_SPLINE = 1
	};

    /// <summary>
    /// Identifies the different components used in the Particle Universe plugin.
    /// </summary>
    public enum ComponentType
	{
		CT_VISUAL_PARTICLE = 0,
		CT_SYSTEM = 1,
		CT_TECHNIQUE = 2,
		CT_EMITTER = 3,
		CT_AFFECTOR = 4,
		CT_OBSERVER = 5
	};

    /// <summary>
    /// These are the eventtypes used by the ParticleSystemListener. As soon as one of these events occurs, the handleParticleSystemEvent() function is 
	///	called, passing the eventtype and additional information. The eventtypes are used by several components in Particle Universe itself
	///	and also by external system. Therefore it is put on namespace level.
	///	Note, that this differs from the Observers and EventHandler components that are used for internal use. The event types listed here and their
	///	usage in the ParticleSystemListener is for external use (the client application).
    /// </summary>
    public enum EventType
	{
        /// <summary>
        /// Submit event when the particle system is being attached or detached.
        /// </summary>
		PU_EVT_SYSTEM_ATTACHING = 0,
        /// <summary>
        /// Submit event when the particle system is attached or detached.
        /// </summary>
		PU_EVT_SYSTEM_ATTACHED = 1,			
        /// <summary>
        /// Submit event when the particle system is preparing.
        /// </summary>
        PU_EVT_SYSTEM_PREPARING = 2,		
        /// <summary>
        /// Submit event when the particle system is prepared.
        /// </summary>
        PU_EVT_SYSTEM_PREPARED = 3,			
        /// <summary>
        /// Submit event when the particle system is starting.
        /// </summary>
        PU_EVT_SYSTEM_STARTING = 4,			
        /// <summary>
        /// Submit event when the particle system is started.
        /// </summary>
        PU_EVT_SYSTEM_STARTED = 5,			
        /// <summary>
        /// Submit event when the particle system is stopping.
        /// </summary>
        PU_EVT_SYSTEM_STOPPING = 6,			
        /// <summary>
        /// Submit event when the particle system is stopped.
        /// </summary>
        PU_EVT_SYSTEM_STOPPED = 7,			
        /// <summary>
        /// Submit event when the particle system is pausing.
        /// </summary>
        PU_EVT_SYSTEM_PAUSING = 8,			
        /// <summary>
        /// Submit event when the particle system is paused.
        /// </summary>
        PU_EVT_SYSTEM_PAUSED = 9,			
        /// <summary>
        /// Submit event when the particle system is resuming (after a pause).
        /// </summary>
        PU_EVT_SYSTEM_RESUMING = 10,			
        /// <summary>
        /// Submit event when the particle system is resumed (after a pause).
        /// </summary>
        PU_EVT_SYSTEM_RESUMED = 11,			
        /// <summary>
        /// Submit event when the particle system is being deleted.
        /// </summary>
        PU_EVT_SYSTEM_DELETING = 12,			
        /// <summary>
        /// Submit event when the particle system switches to another technique when a LOD-level is exceeded.
        /// </summary>
        PU_EVT_LOD_TRANSITION = 13,			
        /// <summary>
        /// Submit event when an emitter is started.
        /// </summary>
        PU_EVT_EMITTER_STARTED = 14,			
        /// <summary>
        /// Submit event when an emitter is stopped.
        /// </summary>
        PU_EVT_EMITTER_STOPPED = 15,			
        /// <summary>
        /// Submit event when all particles have been expired.
        /// </summary>
        PU_EVT_NO_PARTICLES_LEFT = 16
	};

    /// <summary>
    /// This struct is used to pass specific information to a ParticleSystemListener when a certain event occurs. 
	///	A struct is used to make it possible to update the info in the future, without an interface change.
    /// </summary>
	public class ParticleUniverseEvent
	{
        public EventType eventType;
        public ComponentType componentType;
        public String componentName;
        public ParticleTechnique technique; // Is filled if the componentType is a technique
        public ParticleEmitter emitter; // Is filled if the componentType is an emitter
        public IntPtr NativePointer;
	}

    //public class Common
    //{

    //}
}
