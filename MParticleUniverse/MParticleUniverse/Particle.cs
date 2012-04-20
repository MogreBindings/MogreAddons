using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

//using Mogre; 

//#include "ParticleUniversePrerequisites.h"
//#include "ParticleUniverseBehaviour.h"
//#include "ParticleUniverseIVisualData.h"
//#include "ParticleUniverseAny.h"
//#include "ParticleUniversePhysicsActor.h"

namespace MParticleUniverse
{
    /// <summary>
    /// IParticle is the abstract/virtual class that represents the object to be emitted.
    /// 	Several types of particles are distinguished, where the visual particle is the most obvious one.
    /// 	Other types of particles are also possible. ParticleAffectors, ParticleEmitters, ParticleTechniques
    /// 	and even ParticleSystems can act as a particle.
    /// </summary>
    public interface IParticle
    {
        IntPtr NativePointer { get; }
        //List<ParticleBehaviour> ParticleBehaviourList { get; set;}
        //ParticleBehaviourIterator;

        /// <summary>
        /// Emitter that has emitted the particle.
        /// <remarks>Since the particle can be reused by several emitters, this values can change.</remarks>
        /// </summary>
        ParticleEmitter ParentEmitter { get; set; }

        /// <summary>Position.
        /// <remarks>Unlike Ogre's particle plugin, the ParticleUniverse plugin doesn't distinguish between local and worldspace.</remarks>
        /// </summary>
        Mogre.Vector3 Position { get; set; }

        /// <summary>
        /// Direction (and speed)
        /// </summary>
        Mogre.Vector3 Direction { get; set; }

        /// <summary>
        /// Mass of a particle.
        /// <remarks>In case of simulations where mass of a particle is needed (i.e. exploding particles of different mass) this attribute can be used.</remarks>
        /// </summary>
        float Mass { get; set; }

        /// <summary>
        /// Time to live, number of seconds left of particles natural life
        /// </summary>
        float TimeToLive { get; set; }

        /// <summary>
        /// Total Time to live, number of seconds of particles natural life
        /// </summary>
        float TotalTimeToLive { get; set; }

        /// <summary>
        /// The timeFraction is calculated every update. It is used in other observers, affectors, etc. so it is
        /// better to calculate it once at the Particle level.
        /// </summary>
        float TimeFraction { get; set; }

        /// <summary>
        /// Determine type of particle, to prevent Realtime type checking
        /// </summary>
        Particle.ParticleType Type { get; set; }

        /// <summary>
        /// Keep the posibility to attach some custom data. This is additional to the Behaviour data. The
        /// advantage of a UserDefinedObject in favour of a ParticleBehaviour is, that no search is
        /// needed.
        /// <remarks>The UserDefinedObject is not managed by the Particle itself, so assigned UserDefinedObjects must
        ///	be deleted outside the Particle.</remarks>
        /// </summary>
        object UserDefinedObject { get; set; }

        /// <summary>
        ///If a physics engine is used, this attribute is set as soon as a particle is emitted.
        /// </summary>
        PhysicsActor PhysicsActor { get; set; }

        /// <summary>
        /// For some renderers it is needed to relate a particle to some visual data
        /// <remarks>The visual data is set into the Particle instead of the VisualParticle, to enable other
        ///	particle types do use visual data (if needed). The IVisualData is not managed by the Particle 
        ///	itself, so assigned IVisualData must be deleted outside the Particle.</remarks>
        /// </summary>
        IVisualData VisualData { get; set; }

        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        Mogre.Vector3 OriginalPosition { get; set; }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        Mogre.Vector3 OriginalDirection { get; set; }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        float OriginalVelocity { get; set; }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// Length of the direction that has been set
        /// </summary>
        float OriginalDirectionLength { get; set; }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// Length of the direction after multiplication with the velocity
        /// </summary>
        float OriginalScaledDirectionLength { get; set; }

        /// <summary>
        /// Keeps latest position
        /// </summary>
        Mogre.Vector3 LatestPosition { get; set; }

        /// <summary>
        /// Todo
        /// </summary>
        bool _IsMarkedForEmission { get; set; }

        //bool _isMarkedForEmission();
        //void _setMarkedForEmission(bool markedForEmission);

        /// <summary>
        /// Perform initialising activities as soon as the particle is emitted.
        /// </summary>
        void _initForEmission();

        /// <summary>
        /// Perform some action if the particle expires.
        /// <remarks>Note, that this function applies to all particle types (including Particle Techniques, Emitters and
        ///	Affectors).</remarks>
        /// </summary>
        void _initForExpiration(ParticleTechnique technique, float timeElapsed);

        /// <summary>
        /// Sets weither or not this Particle is enabled.
        /// </summary>
        bool Enabled { get; set; }

        //bool isEnabled();
        //void SetEnabled(bool enabled);

        /// <summary>
        /// This function sets the original 'enabled' value of the particle.
        /// Returns the original 'enabled' value of the particle
        /// <remarks>Only use this function if you really know what you're doing. Otherwise it shouldn't be used for regular usage.</remarks>
        /// </summary>
        bool _OriginalEnabled { get; set; }

        //void _setOriginalEnabled(bool originalEnabled);
        //bool _getOriginalEnabled();


        /// <summary>
        /// Freeze the particle, so it doesn't move anymore.
        /// Returns true if the particle is freezed and doesn't move anymore.
        /// <remarks>Although it is freezed, repositioning the particle is still possible.</remarks>
        /// </summary>
        bool Freezed { get; set; }

        //bool isFreezed();
        //void SetFreezed(bool freezed);

        /// <summary>
        /// Sets the event flags.
        /// </summary>
        void SetEventFlags(uint flags);

        /// <summary>
        /// As setEventFlags, except the flags passed as parameters are appended to the
        ///	existing flags on this object.
        /// </summary>
        void AddEventFlags(uint flags);

        /// <summary>
        /// The flags passed as parameters are removed from the existing flags.
        /// </summary>
        void RemoveEventFlags(uint flags);

        /// <summary>
        /// Return the event flags.
        /// </summary>
        uint GetEventFlags();

        /// <summary>
        /// Determines whether it has certain flags set.
        /// </summary>
        bool HasEventFlags(uint flags);

        /// <summary>
        /// Copy a vector of ParticleBehaviour objects to this particle.
        /// </summary>
        void CopyBehaviours(ParticleBehaviour[] behaviours);


        /// <summary>
        /// Perform actions on the particle itself during the update loop of a ParticleTechnique.
        /// <remarks>Active particles may want to do some processing themselves each time the ParticleTechnique is updated.
        ///	One example is to perform actions by means of the registered ParticleBehaviour objects. 
        ///	ParticleBehaviour objects apply internal behaviour of each particle individually. They add both 
        ///	data and behaviour to a particle, which means that each particle can be extended with functionality.</remarks>
        /// </summary>
        void _process(ParticleTechnique technique, float timeElapsed);

        /// <summary>
        /// Returns the first occurence of the ParticleBehaviour specified by its type.
        /// </summary>
        ParticleBehaviour GetBehaviour(String behaviourType);

        /// <summary>
        /// Calculates the velocity, based on the direction vector.
        /// </summary>
        float CalculateVelocity();

        /// <summary>
        /// Copy the data of this particle to another particle.
        /// </summary>
        void CopyAttributesTo(IParticle particle);
    }

    public class ParticleFactory
    {
        public static Particle NewParticleObject()
        {
            return new Particle();   // factory method
        }
        //    public static Particle NewAddOnObject(IParticle Caller)
        //{   return new ConcreteAddOnClass(Caller);   // factory method }
    }

    /// <summary>
    /// Particle is the Implementation of the IParticle class that represents the object to be emitted.
    /// 	Several types of particles are distinguished, where the visual particle is the most obvious one.
    /// 	Other types of particles are also possible. ParticleAffectors, ParticleEmitters, ParticleTechniques
    /// 	and even ParticleSystems can act as a particle.
    /// </summary>
    public unsafe class Particle : IParticle, IDisposable
    {
        private IntPtr nativePointer;
        public IntPtr NativePointer { get { return nativePointer; } }

        internal Particle(IntPtr ptr)
        {
            nativePointer = ptr;
        }

        internal static Dictionary<IntPtr, Particle> particleInstances;

        internal static Particle GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (particleInstances == null)
                particleInstances = new Dictionary<IntPtr, Particle>();

            Particle newvalue;

            if (particleInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Particle(ptr);
            particleInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public Particle()
        {
            nativePointer = Particle_New();
            particleInstances.Add(nativePointer, this);
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
            Particle_Destroy(nativePointer);
            particleInstances.Remove(nativePointer);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public enum ParticleType
        {
            PT_VISUAL = 0,
            PT_TECHNIQUE = 1,
            PT_EMITTER = 2,
            PT_AFFECTOR = 3,
            PT_SYSTEM = 4
        };

        /// <summary>
        /// Enumeration which lists a number of reserved event flags. Although custom flags can be used to
        ///		indicate that a certain condition occurs, the first number of flags may not be used as custom flags.
        /// </summary>
        public enum ReservedParticleEventFlags
        {
            PEF_EXPIRED = 1 << 0,
            PEF_EMITTED = 1 << 1,
            PEF_COLLIDED = 1 << 2
        };

        //public List<ParticleBehaviour> ParticleBehaviourList { get; set; }
        //ParticleBehaviourIterator;

        public static float DEFAULT_TTL
        {
            get { return Particle_GetDEFAULT_TTL(); }
            set { Particle_SetDEFAULT_TTL(value); }
        }
        public static float DEFAULT_MASS
        {
            get { return Particle_GetDEFAULT_MASS(); }
            set { Particle_SetDEFAULT_MASS(value); }
        }

        private ParticleEmitter parentEmitter;

        /// <summary>
        /// Emitter that has emitted the particle.
        /// <remarks>Since the particle can be reused by several emitters, this values can change.</remarks>
        /// </summary>
        public ParticleEmitter ParentEmitter
        {
            get
            {
                try{
                    IntPtr p = Particle_GetParentEmitter(nativePointer);
                    if (p == IntPtr.Zero)
                        return null;
                    if (parentEmitter != null && p == parentEmitter.nativePointer && p != IntPtr.Zero)
                        return parentEmitter;
                    
                    parentEmitter = ParticleEmitter.GetParticleEmitterByPointer(p);
                    return parentEmitter;
                }
                catch (AccessViolationException) { }
                return null;
            }
            set
            {
                parentEmitter = value;
                if (value == null)
                    Particle_SetParentEmitter(nativePointer, IntPtr.Zero); 
                else
                Particle_SetParentEmitter(nativePointer, value.NativePointer);
            }
        }

        /// <summary>Position.
        /// <remarks>Unlike Ogre's particle plugin, the ParticleUniverse plugin doesn't distinguish between local and worldspace.</remarks>
        /// </summary>
        public Mogre.Vector3 Position
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Particle_GetPosition(nativePointer).ToPointer()));
                return vec;
            }
            set
            {
                Particle_SetPosition(nativePointer, value);
            }
        }

        /// <summary>
        /// Direction (and speed)
        /// </summary>
        public Mogre.Vector3 Direction
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Particle_GetDirection(nativePointer).ToPointer()));
                return vec;
            }
            set
            {
                Particle_SetDirection(nativePointer, value);
            }
        }

        /// <summary>
        /// Mass of a particle.
        /// <remarks>In case of simulations where mass of a particle is needed (i.e. exploding particles of different mass) this attribute can be used.</remarks>
        /// </summary>
        public float Mass
        {
            get { return Particle_GetMass(nativePointer); }
            set { Particle_SetMass(nativePointer, value); }
        }

        /// <summary>
        /// Time to live, number of seconds left of particles natural life
        /// </summary>
        public float TimeToLive
        {
            get { return Particle_GetTimeToLive(nativePointer); }
            set { Particle_SetTimeToLive(nativePointer, value); }
        }

        /// <summary>
        /// Total Time to live, number of seconds of particles natural life
        /// </summary>
        public float TotalTimeToLive
        {
            get { return Particle_GetTotalTimeToLive(nativePointer); }
            set { Particle_SetTotalTimeToLive(nativePointer, value); }
        }

        /// <summary>
        /// The timeFraction is calculated every update. It is used in other observers, affectors, etc. so it is
        /// better to calculate it once at the Particle level.
        /// </summary>
        public float TimeFraction
        {
            get { return Particle_GetTimeFraction(nativePointer); }
            set { Particle_SetTimeFraction(nativePointer, value); }
        }

        /// <summary>
        /// Determine type of particle, to prevent Realtime type checking
        /// </summary>
        public ParticleType Type
        {
            get { return Particle_GetParticleType(nativePointer); }
            set { Particle_SetParticleType(nativePointer, value); }
        }

        /// <summary>
        /// Keep the posibility to attach some custom data. This is additional to the Behaviour data. The
        /// advantage of a UserDefinedObject in favour of a ParticleBehaviour is, that no search is
        /// needed.
        /// <remarks>The UserDefinedObject is not managed by the Particle itself, so assigned UserDefinedObjects must
        ///	be deleted outside the Particle.</remarks>
        /// </summary>
        public Object UserDefinedObject
        {
            get
            {
                throw new NotImplementedException("Mogre doesn't have an equivalent to Ogre::Any");
                try { 
                    Object o = new object();
                    Marshal.PtrToStructure(Particle_GetMUserDefinedObject(nativePointer), o);
                    return o; 
                }
                catch (AccessViolationException) { }
                return null;
            }
            set
            {
                throw new NotImplementedException("Mogre doesn't have an equivalent to Ogre::Any");
                if (value == null)
                    Particle_SetMUserDefinedObject(nativePointer, IntPtr.Zero);
                else
                    Particle_SetMUserDefinedObject(nativePointer, value);
            }
        }

        PhysicsActor physicsActor;

        /// <summary>
        /// If a physics engine is used, this attribute is set as soon as a particle is emitted.
        /// </summary>
        public PhysicsActor PhysicsActor
        {
            get
            {
                try {
                    IntPtr p = Particle_GetPhysicsActor(nativePointer);
                    if (p == IntPtr.Zero)
                        return null;
                    if (physicsActor != null && physicsActor.nativePtr == p && p != IntPtr.Zero)
                        return physicsActor;
                    physicsActor = PhysicsActor.GetInstance(p);
                    return physicsActor;
                }
                catch (AccessViolationException) { } //PhysicsActor is null.
                return null;
            }
            set
            {
                physicsActor = value;
                if (value == null)
                    Particle_SetPhysicsActor(nativePointer, IntPtr.Zero);
                else
                    Particle_SetPhysicsActor(nativePointer, value.NativePointer);
            }
        }

        private IVisualData visualData;

        /// <summary>
        /// For some renderers it is needed to relate a particle to some visual data
        /// <remarks>The visual data is set into the Particle instead of the VisualParticle, to enable other
        ///	particle types do use visual data (if needed). The IVisualData is not managed by the Particle 
        ///	itself, so assigned IVisualData must be deleted outside the Particle.</remarks>
        /// </summary>
        public IVisualData VisualData
        {
            get
            {
                try
                {
                    IntPtr p = Particle_GetVisualData(nativePointer);
                    if (p == IntPtr.Zero)
                        return null;
                    if (visualData != null && visualData.nativePtr == p && p != IntPtr.Zero)
                        return visualData;
                    visualData = IVisualData.GetInstance(p);
                    return visualData;
                }
                catch (AccessViolationException) { }
                return null;
            }
            set
            {
                visualData = value;
                if (value == null)
                    Particle_SetVisualData(nativePointer, IntPtr.Zero);
                else
                    Particle_SetVisualData(nativePointer, value.NativePointer);
            }
        }

        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        public Mogre.Vector3 OriginalPosition
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Particle_GetOriginalPosition(nativePointer).ToPointer()));
                return vec;
            }
            set
            {
                Particle_SetOriginalPosition(nativePointer, value);
            }
        }

        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        public Mogre.Vector3 OriginalDirection
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Particle_GetOriginalDirection(nativePointer).ToPointer()));
                return vec;
            }
            set
            {
                Particle_SetOriginalDirection(nativePointer, value);
            }
        }

        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        public float OriginalVelocity
        {
            get { return Particle_GetOriginalVelocity(nativePointer); }
            set { Particle_SetOriginalVelocity(nativePointer, value); }
        }
        
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// Length of the direction that has been set
        /// </summary>
        public float OriginalDirectionLength
        {
            get { return Particle_GetOriginalDirectionLength(nativePointer); }
            set { Particle_SetOriginalDirectionLength(nativePointer, value); }
        }
        
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// Length of the direction after multiplication with the velocity
        /// </summary>
        public float OriginalScaledDirectionLength
        {
            get { return Particle_GetOriginalScaledDirectionLength(nativePointer); }
            set { Particle_SetOriginalScaledDirectionLength(nativePointer, value); }
        }
        
        /// <summary>
        /// Keeps latest position
        /// </summary>
        public Mogre.Vector3 LatestPosition
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Particle_GetLatestPosition(nativePointer).ToPointer()));
                return vec;
            }
            set
            {
                Particle_SetLatestPosition(nativePointer, value);
            }
        }

        //// <summary>
        /// Todo
        /// </summary>
        public bool _IsMarkedForEmission
        {
            get { return Particle__isMarkedForEmission(nativePointer); }
            set { Particle__setMarkedForEmission(nativePointer, value); }
        }
        //public bool _isMarkedForEmission() { return Particle__isMarkedForEmission(nativePointer); }
        //public void _setMarkedForEmission(bool markedForEmission) { Particle__setMarkedForEmission(nativePointer, markedForEmission); }
        
        /// <summary>
        /// Perform initialising activities as soon as the particle is emitted.
        /// </summary>
        public void _initForEmission() { Particle__initForEmission(nativePointer); }
        
        // <summary>
        /// Perform some action if the particle expires.
        /// <remarks>Note, that this function applies to all particle types (including Particle Techniques, Emitters and
        ///	Affectors).</remarks>
        /// </summary>
        public void _initForExpiration(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            try
            {
                Particle__initForExpiration(nativePointer, technique.NativePointer, timeElapsed);
            }
            catch (AccessViolationException) { }
        }

        /// <summary>
        /// Sets weither or not this Particle is enabled.
        /// </summary>
        public bool Enabled
        {
            get { return Particle_IsEnabled(nativePointer); }
            set { Particle_SetEnabled(nativePointer, value); }
        }
        //public bool isEnabled() { return Particle_IsEnabled(nativePointer); }
        //public void SetEnabled(bool enabled) { Particle_SetEnabled(nativePointer, enabled); }
        
        /// <summary>
        /// This function sets the original 'enabled' value of the particle.
        /// Returns the original 'enabled' value of the particle
        /// <remarks>Only use this function if you really know what you're doing. Otherwise it shouldn't be used for regular usage.</remarks>
        /// </summary>
        public bool _OriginalEnabled
        {
            get { return Particle__getOriginalEnabled(nativePointer); }
            set { Particle__setOriginalEnabled(nativePointer, value); }
        }
        //public void _setOriginalEnabled(bool originalEnabled) { Particle__setOriginalEnabled(nativePointer, originalEnabled); }
        //public bool _getOriginalEnabled() { return Particle__getOriginalEnabled(nativePointer); }
        
        /// <summary>
        /// Freeze the particle, so it doesn't move anymore.
        /// Returns true if the particle is freezed and doesn't move anymore.
        /// <remarks>Although it is freezed, repositioning the particle is still possible.</remarks>
        /// </summary>
        public bool Freezed
        {
            get { return Particle_IsFreezed(nativePointer); }
            set { Particle_SetFreezed(nativePointer, value); }
        }
        //public bool isFreezed() { return Particle_IsFreezed(nativePointer); }
        //public void SetFreezed(bool freezed) { Particle_SetFreezed(nativePointer, freezed); }
        
        /// <summary>
        /// Sets the event flags.
        /// </summary>
        public void SetEventFlags(uint flags) { Particle_SetEventFlags(nativePointer, flags); }
        
        /// <summary>
        /// As setEventFlags, except the flags passed as parameters are appended to the
        ///	existing flags on this object.
        /// </summary>
        public void AddEventFlags(uint flags) { Particle_AddEventFlags(nativePointer, flags); }
        
        /// <summary>
        /// The flags passed as parameters are removed from the existing flags.
        /// </summary>
        public void RemoveEventFlags(uint flags) { Particle_RemoveEventFlags(nativePointer, flags); }

        /// <summary>
        /// Return the event flags.
        /// </summary>
        public uint GetEventFlags() { return Particle_GetEventFlags(nativePointer); }

        /// <summary>
        /// Determines whether it has certain flags set.
        /// </summary>
        public bool HasEventFlags(uint flags) { return Particle_HasEventFlags(nativePointer, flags); }
        
        /// <summary>
        /// Copy a vector of ParticleBehaviour objects to this particle.
        /// </summary>
        public void CopyBehaviours(ParticleBehaviour[] behaviours)
        {
            if (behaviours == null)
                throw new ArgumentNullException("behaviours cannot be null!");
            Particle_CopyBehaviours(nativePointer, ref behaviours, behaviours.Count());
        }
       
        /// <summary>
        /// Perform actions on the particle itself during the update loop of a ParticleTechnique.
        /// <remarks>Active particles may want to do some processing themselves each time the ParticleTechnique is updated.
        ///	One example is to perform actions by means of the registered ParticleBehaviour objects. 
        ///	ParticleBehaviour objects apply internal behaviour of each particle individually. They add both 
        ///	data and behaviour to a particle, which means that each particle can be extended with functionality.</remarks>
        /// </summary>
        public void _process(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            try
            {
                Particle__process(nativePointer, technique.NativePointer, timeElapsed);
            }
            catch (AccessViolationException) { }
        }

        private ParticleBehaviour behaviour;

        /// <summary>
        /// Returns the first occurence of the ParticleBehaviour specified by its type.
        /// </summary>
        public ParticleBehaviour GetBehaviour(String behaviourType)
        {
            IntPtr p = Particle_GetBehaviour(nativePointer, behaviourType);
            if (p == IntPtr.Zero)
                return null;
            if (behaviour != null && behaviour.nativePtr == p)
                return behaviour;
            behaviour = ParticleBehaviourHelper.GetParticleBehaviour(p);
            return behaviour;
        }

        /// <summary>
        /// Calculates the velocity, based on the direction vector.
        /// </summary>
        public float CalculateVelocity() { return Particle_CalculateVelocity(nativePointer); }

        /// <summary>
        /// Copy the data of this particle to another particle.
        /// </summary>
        public void CopyAttributesTo(IParticle particle) 
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Particle_CopyAttributesTo(nativePointer, particle.NativePointer); 
        }

        #region P/Invoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetDEFAULT_TTL", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetDEFAULT_TTL();
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetDEFAULT_TTL", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetDEFAULT_TTL(float newTTL);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetDEFAULT_MASS", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetDEFAULT_MASS();
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetDEFAULT_MASS", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetDEFAULT_MASS(float newDefaultMass);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetParentEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetParentEmitter(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetParentEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetParentEmitter(IntPtr ptr, IntPtr newParent);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetPosition(IntPtr ptr, Mogre.Vector3 newPos);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetDirection(IntPtr ptr, Mogre.Vector3 newDir);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetMass(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetMass(IntPtr ptr, float newMass);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetTimeToLive(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetTimeToLive(IntPtr ptr, float newTTL);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetTotalTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetTotalTimeToLive(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetTotalTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetTotalTimeToLive(IntPtr ptr, float newTotalTTL);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetTimeFraction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetTimeFraction(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetTimeFraction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetTimeFraction(IntPtr ptr, float newTimeFrac);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetParticleType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MParticleUniverse.Particle.ParticleType Particle_GetParticleType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetParticleType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetParticleType(IntPtr ptr, MParticleUniverse.Particle.ParticleType newType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetMUserDefinedObject", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetMUserDefinedObject(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetMUserDefinedObject", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetMUserDefinedObject(IntPtr ptr, [MarshalAs(UnmanagedType.Interface)]Object newObj);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetPhysicsActor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetPhysicsActor(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetPhysicsActor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetPhysicsActor(IntPtr ptr, IntPtr newActor);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetVisualData", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetVisualData(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetVisualData", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetVisualData(IntPtr ptr, IntPtr newVisualData);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetOriginalPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetOriginalPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetOriginalPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetOriginalPosition(IntPtr ptr, Mogre.Vector3 newOrigPos);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetOriginalDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetOriginalDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetOriginalDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetOriginalDirection(IntPtr ptr, Mogre.Vector3 newOrigDir);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetOriginalVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetOriginalVelocity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetOriginalVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetOriginalVelocity(IntPtr ptr, float origVel);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetOriginalDirectionLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetOriginalDirectionLength(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetOriginalDirectionLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetOriginalDirectionLength(IntPtr ptr, float origLen);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetOriginalScaledDirectionLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_GetOriginalScaledDirectionLength(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetOriginalScaledDirectionLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetOriginalScaledDirectionLength(IntPtr ptr, float origScaleLen);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetLatestPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetLatestPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetLatestPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetLatestPosition(IntPtr ptr, Mogre.Vector3 latestPos);


        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__isMarkedForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Particle__isMarkedForEmission(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__setMarkedForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle__setMarkedForEmission(IntPtr ptr, bool markedForEmission);


        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__initForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle__initForEmission(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__initForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle__initForExpiration(IntPtr ptr, IntPtr technique, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_IsEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Particle_IsEnabled(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetEnabled(IntPtr ptr, bool enabled);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__setOriginalEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle__setOriginalEnabled(IntPtr ptr, bool originalEnabled);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__getOriginalEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Particle__getOriginalEnabled(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_IsFreezed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Particle_IsFreezed(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetFreezed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetFreezed(IntPtr ptr, bool freezed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_SetEventFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_SetEventFlags(IntPtr ptr, uint flags);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_AddEventFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_AddEventFlags(IntPtr ptr, uint flags);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_RemoveEventFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_RemoveEventFlags(IntPtr ptr, uint flags);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetEventFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint Particle_GetEventFlags(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_HasEventFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Particle_HasEventFlags(IntPtr ptr, uint flags);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_CopyBehaviours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_CopyBehaviours(IntPtr ptr, ref ParticleBehaviour[] behaviours, int arrSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle__process", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle__process(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_GetBehaviour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Particle_GetBehaviour(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string behaviourType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_CalculateVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Particle_CalculateVelocity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Particle_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Particle_CopyAttributesTo(IntPtr ptr, IntPtr particle);

        #endregion
    }
}