using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using MParticleUniverse.ParticleEmitters;

namespace MParticleUniverse
{
    /// <summary>
    /// Abstract class defining the interface to be implemented by particle emitters.
    ///	Subclasses of ParticleEmitters are responsible of spawning the particles of a particle technique.
    ///	This class defines the interface, and provides a few implementations of some basic functions.
    /// </summary>
    public unsafe abstract class ParticleEmitter : Particle, IAlias, IElement, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal ParticleEmitter(IntPtr ptr)
        {
            nativePtr = ptr;
            ParticleSystem p = ParticleSystem.GetInstances(ptr);
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

        internal static ParticleEmitter GetParticleEmitterByPointer(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            
            String emitterType = Marshal.PtrToStringAnsi(ParticleEmitter_GetEmitterType(ptr));
            //System.Console.WriteLine("emitterType: " + emitterType);
            //System.Console.WriteLine("ParticleEmitter_GetName: " +  ParticleEmitter_GetName(ptr));
            //System.Console.WriteLine("ParticleEmitter_GetEmitsName: " +  ParticleEmitter_GetEmitsName(ptr));

            if (emitterType == "Box")
                return BoxEmitter.GetInstance(ptr);
            if (emitterType == "Circle")
                return CircleEmitter.GetInstance(ptr);
            if (emitterType == "Line")
                return LineEmitter.GetInstance(ptr);
            if (emitterType == "MeshSurface")
                return MeshSurfaceEmitter.GetInstance(ptr);
            if (emitterType == "Point")
                return PointEmitter.GetInstance(ptr);
            if (emitterType == "Position")
                return PositionEmitter.GetInstance(ptr);
            if (emitterType == "Slave")
                return SlaveEmitter.GetInstance(ptr);
            if (emitterType == "SphereSurface")
                return SphereSurfaceEmitter.GetInstance(ptr);
            if (emitterType == "Vertex")
                return VertexEmitter.GetInstance(ptr);
            
            return null;
        }

        internal static ParticleEmitter GetParticleEmitterByPointer(IntPtr ptr, String emitterType)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            
            if (emitterType == "Box")
                return BoxEmitter.GetInstance(ptr);
            if (emitterType == "Circle")
                return CircleEmitter.GetInstance(ptr);
            if (emitterType == "Line")
                return LineEmitter.GetInstance(ptr);
            if (emitterType == "MeshSurface")
                return MeshSurfaceEmitter.GetInstance(ptr);
            if (emitterType == "Point")
                return PointEmitter.GetInstance(ptr);
            if (emitterType == "Position")
                return PositionEmitter.GetInstance(ptr);
            if (emitterType == "Slave")
                return SlaveEmitter.GetInstance(ptr);
            if (emitterType == "SphereSurface")
                return SphereSurfaceEmitter.GetInstance(ptr);
            if (emitterType == "Vertex")
                return VertexEmitter.GetInstance(ptr);

            return null;
        }

        public const bool DEFAULT_ENABLED = true;
        public static Mogre.Vector3 DEFAULT_POSITION { get { return new Mogre.Vector3(0, 0, 0); } }
        public const bool DEFAULT_KEEP_LOCAL = false;
        public static Mogre.Vector3 DEFAULT_DIRECTION { get { return new Mogre.Vector3(0, 1, 0); } }
        public static Mogre.Quaternion DEFAULT_ORIENTATION { get { return new Mogre.Quaternion(1, 0, 0, 0); } }
        public static Mogre.Quaternion DEFAULT_ORIENTATION_RANGE_START { get { return new Mogre.Quaternion(1, 0, 0, 0); } }
        public static Mogre.Quaternion DEFAULT_ORIENTATION_RANGE_END { get { return new Mogre.Quaternion(1, 0, 0, 0); } }
        public const Particle.ParticleType DEFAULT_EMITS = VisualParticle.ParticleType.PT_VISUAL;
        public const ushort DEFAULT_START_TEXTURE_COORDS = 0;
        public const ushort DEFAULT_END_TEXTURE_COORDS = 0;
        public const ushort DEFAULT_TEXTURE_COORDS = 0;
        public static Mogre.ColourValue DEFAULT_START_COLOUR_RANGE { get { return new Mogre.ColourValue(0, 0, 0); } }
        public static Mogre.ColourValue DEFAULT_END_COLOUR_RANGE { get { return new Mogre.ColourValue(1, 1, 1); } }
        public static Mogre.ColourValue DEFAULT_COLOUR { get { return new Mogre.ColourValue(1, 1, 1); } }
        public const bool DEFAULT_AUTO_DIRECTION = false;
        public const bool DEFAULT_FORCE_EMISSION = false;
        public const float DEFAULT_EMISSION_RATE = 10.0f;
        public const float DEFAULT_TIME_TO_LIVE = 3.0f;
        public const float DEFAULT_MASS = 1.0f;
        public const float DEFAULT_VELOCITY = 100.0f;
        public const float DEFAULT_DURATION = 0.0f;
        public const float DEFAULT_REPEAT_DELAY = 0.0f;
        public const float DEFAULT_ANGLE = 20.0f;
        public const float DEFAULT_DIMENSIONS = 0.0f;
        public const float DEFAULT_WIDTH = 0.0f;
        public const float DEFAULT_HEIGHT = 0.0f;
        public const float DEFAULT_DEPTH = 0.0f;

        /// <summary>
        /// Todo
        /// </summary>
        public ParticleTechnique ParentTechnique 
        {
            get { return ParticleTechnique.GetInstances(ParticleEmitter_GetParentTechnique(nativePtr)); } 
            set { 
                if (value == null)
                    ParticleEmitter_SetParentTechnique(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetParentTechnique(nativePtr, value.nativePtr);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public String EmitterType { get { return Marshal.PtrToStringAnsi(ParticleEmitter_GetEmitterType(nativePtr)); } set { ParticleEmitter_SetEmitterType(nativePtr, value); } }

        /// <summary>
        /// Todo
        /// </summary>
        public String Name { get { return Marshal.PtrToStringAnsi(ParticleEmitter_GetName(nativePtr)); } set { ParticleEmitter_SetName(nativePtr, value); } }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynAngle { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynAngle(nativePtr)); } 
            set {
                if (value == null)
                    ParticleEmitter_SetDynAngle(nativePtr, IntPtr.Zero); 
                else
                    ParticleEmitter_SetDynAngle(nativePtr, value.NativePointer); 
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynEmissionRate { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynEmissionRate(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynEmissionRate(nativePtr, IntPtr.Zero); 
                else
                    ParticleEmitter_SetDynEmissionRate(nativePtr, value.NativePointer);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynTotalTimeToLive { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynTotalTimeToLive(nativePtr)); }
            set {
                if (value == null)
                    ParticleEmitter_SetDynTotalTimeToLive(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynTotalTimeToLive(nativePtr, value.NativePointer);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynParticleMass {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynParticleMass(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynParticleMass(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynParticleMass(nativePtr, value.NativePointer);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynVelocity { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynVelocity(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynVelocity(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynVelocity(nativePtr, value.NativePointer);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynDuration {
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynDuration(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynDuration(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynDuration(nativePtr, value.NativePointer);
            }
        }
        public void DynDurationSet(bool durationSet)
        {
            ParticleEmitter_SetDynDurationSet(nativePtr, durationSet);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynRepeatDelay { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynRepeatDelay(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynRepeatDelay(nativePtr, IntPtr.Zero); 
                else
                    ParticleEmitter_SetDynRepeatDelay(nativePtr, value.NativePointer); 
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="repeatDelaySet"></param>
        public void DynRepeatDelaySet(bool repeatDelaySet)
        {
            ParticleEmitter_SetDynRepeatDelaySet(nativePtr, repeatDelaySet);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynParticleAllDimensions { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynParticleAllDimensions(nativePtr));
            }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynParticleAllDimensions(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynParticleAllDimensions(nativePtr, value.NativePointer);
            }
        }
        
        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="particleAllDimensionsSet"></param>
        public void DynParticleAllDimensionsSet(bool particleAllDimensionsSet)
        {
            ParticleEmitter_SetDynParticleAllDimensionsSet(nativePtr, particleAllDimensionsSet);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynParticleWidth { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynParticleWidth(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynParticleWidth(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynParticleWidth(nativePtr, value.NativePointer);
            }
        }
        
        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="particleWidthSet"></param>
        public void DynParticleWidthSet(bool particleWidthSet)
        {
            ParticleEmitter_SetDynParticleWidthSet(nativePtr, particleWidthSet);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynParticleHeight { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynParticleHeight(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynParticleHeight(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynParticleHeight(nativePtr, value.NativePointer);
            }
        }
        
        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="particleHeightSet"></param>
        public void DynParticleHeightSet(bool particleHeightSet)
        {
            ParticleEmitter_SetDynParticleHeightSet(nativePtr, particleHeightSet);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public DynamicAttribute DynParticleDepth { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(ParticleEmitter_GetDynParticleDepth(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetDynParticleDepth(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetDynParticleDepth(nativePtr, value.NativePointer);
            }
        }
        
        /// <summary>
        /// Todo
        /// </summary>
        /// <param name="particleDepthSet"></param>
        public void DynParticleDepthSet(bool particleDepthSet)
        {
            ParticleEmitter_SetDynParticleDepthSet(nativePtr, particleDepthSet);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public ParticleType EmitsType { 
            get { return ParticleEmitter_GetEmitsType(nativePtr); }
            set
            {
                ParticleEmitter_SetEmitsType(nativePtr, value);
            }
        }

        /// <summary>
        /// Todo
        /// </summary>
        public String EmitsName {
            get { return Marshal.PtrToStringAnsi(ParticleEmitter_GetEmitsName(nativePtr)); }
            set
            {
                ParticleEmitter_SetEmitsName(nativePtr, value);
            }
        }

        /// <summary>
        /// Set the distance-value and inc. indication to recalculate the emission rate.
        /// </summary>
        /// <param name="squareDistance"></param>
        /// <param name="inc"></param>
        public void SetEmissionRateCameraDependency(float squareDistance, bool inc = false)
        {
            ParticleEmitter_SetEmissionRateCameraDependency(nativePtr, squareDistance, inc);
        }

        /// <summary>
        /// Todo
        /// </summary> 
        public CameraDependency EmissionRateCameraDependency { 
            get { return CameraDependency.GetInstance(ParticleEmitter_GetEmissionRateCameraDependency(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleEmitter_SetEmissionRateCameraDependency(nativePtr, IntPtr.Zero);
                else
                    ParticleEmitter_SetEmissionRateCameraDependency(nativePtr, value.nativePtr);
            }
        }

        /// <summary>
        /// Returns the base direction of the particle that is going to be emitted.
        /// Sets the direction of the particle that the emitter is emitting.
        ///	<remarks>
        ///		Don't confuse this with the emitters own direction.
        /// </remarks>
        /// </summary>
        public Mogre.Vector3 ParticleDirection
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleEmitter_GetParticleDirection(nativePtr).ToPointer()));
                return vec;
            }
        set{
            ParticleEmitter_SetParticleDirection(nativePtr, value);
        }
        }

        /// <summary>
        /// Returns the originally set particle direction. This value is not affected by affectors, angle, etc.
        /// </summary>
        public Mogre.Vector3 OriginalParticleDirection
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleEmitter_GetOriginalParticleDirection(nativePtr).ToPointer()));
                return vec;
            }
        }

        /// <summary>
        /// Set the orientation of the particle.
        /// Returns the base orientation of the particle that is going to be emitted.
        /// </summary>
        public Mogre.Quaternion ParticleOrientation
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)ParticleEmitter_GetParticleOrientation(nativePtr).ToPointer()));
                return vec;
            }
            set { ParticleEmitter_SetParticleOrientation(nativePtr, value); }
        }

        /// <summary>
        /// Set start orientation of the particle that is going to be emitted.
        ///	<remarks>
        ///		The orientation is generated random between mParticleOrientationRangeStart and mParticleOrientationRangeEnd.
        ///	</remarks>
        ///		Returns the start orientation of the particle that is going to be emitted.
        ///	<remarks>
        ///		The orientation is generated random between mParticleOrientationRangeStart and mParticleOrientationRangeEnd.
        /// </remarks>
        /// </summary>
        public Mogre.Quaternion ParticleOrientationRangeStart
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)ParticleEmitter_GetParticleOrientationRangeStart(nativePtr).ToPointer()));
                return vec;
            }
            set { ParticleEmitter_SetParticleOrientationRangeStart(nativePtr, value); }
        }

        /// <summary>
        /// Set end orientation of the particle that is going to be emitted.
        ///	<remarks>
        ///		The orientation is generated random between mParticleOrientationRangeStart and mParticleOrientationRangeEnd.
        /// </remarks>
        /// Returns the end orientation of the particle that is going to be emitted.
        ///	<remarks>
        ///		The orientation is generated random between mParticleOrientationRangeStart and mParticleOrientationRangeEnd.
        /// </remarks>
        /// </summary>
        public Mogre.Quaternion ParticleOrientationRangeEnd
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)ParticleEmitter_GetParticleOrientationRangeEnd(nativePtr).ToPointer()));
                return vec;
            }
            set { ParticleEmitter_SetParticleOrientationRangeEnd(nativePtr, value); }
        }

        /// <summary>
        /// Enables or disables the emitter.
        /// </summary>
        /// <param name="enabled"></param>
        public void SetEnabled(bool enabled)
        {
            ParticleEmitter_SetEnabled(nativePtr, enabled);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public bool AutoDirection { get { return ParticleEmitter_IsAutoDirection(nativePtr); } set { ParticleEmitter_SetAutoDirection(nativePtr, value); } }

        /// <summary>
        /// Todo
        /// </summary>
        public bool ForceEmission { get { return ParticleEmitter_IsForceEmission(nativePtr); } set { ParticleEmitter_SetForceEmission(nativePtr, value); } }

        /// <summary>
        /// Perform initialisation actions.
        /// </summary>
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            ParticleEmitter__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /// <summary>
        /// Reverse initialisation actions.
        /// </summary>
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            ParticleEmitter__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        /// <summary>
        /// Perform activities before the individual particles are processed.
        /// </summary>
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleEmitter__preProcessParticles(nativePtr, technique.NativePointer, timeElapsed);
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
            ParticleEmitter__postProcessParticles(nativePtr, technique.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Perform initialising activities as soon as the emitter is emitted.
        /// </summary>
        public void _initForEmission()
        {
            ParticleEmitter__initForEmission(nativePtr);
        }

        /// <summary>
        /// Initialise the ParticleEmitter before it is expired itself.
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _initForExpiration(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleEmitter__initForExpiration(nativePtr, technique.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Todo
        /// </summary>
        public void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Internal method for generating the particle direction.
        /// </summary>
        /// <param name="particle"></param>
        public void _initParticleDirection(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleDirection(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Internal method for generating the particle orientation.
        /// </summary>
        /// <param name="particle"></param>
        public void _initParticleOrientation(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleOrientation(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Internal method for generating the angle.
        /// </summary>
        /// <param name="angle"></param>
        public void _generateAngle(Mogre.Radian angle)
        {
            IntPtr radianPtr = Marshal.AllocHGlobal(Marshal.SizeOf(angle));
            Marshal.StructureToPtr(angle, radianPtr, true);
            ParticleEmitter__generateAngle(nativePtr, radianPtr);
        }

        /// <summary>
        /// Internal method for generating the particle velocity.
        /// </summary>
        /// <param name="particle">Reference to vector direction * velocity</param>
        public void _initParticleVelocity(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleVelocity(nativePtr, particle.NativePointer);
        }

        /// <summary>
        ///  Internal method for generating the mass of a particle.
        /// </summary>
        /// <param name="particle"></param>
        public void _initParticleMass(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleMass(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Internal method for generating the colour of a particle.
        /// </summary>
        /// <param name="particle"></param>
        public void _initParticleColour(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleColour(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Internal method for setting the texture coords of a particle.
        /// </summary>
        /// <param name="particle"></param>
        public void _initParticleTextureCoords(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleTextureCoords(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Internal method for generating the time to live of a particle.
        /// </summary>
        /// <returns></returns>
        public float _initParticleTimeToLive()
        {
            return ParticleEmitter__initParticleTimeToLive(nativePtr);
        }

        /// <summary>
        /// Calculate the number of particles that the emitter wants to emit.
        /// </summary>
        /// <param name="timeElapsed"></param>
        /// <returns></returns>
        public ushort _calculateRequestedParticles(float timeElapsed)
        {
            return ParticleEmitter__calculateRequestedParticles(nativePtr, timeElapsed);
        }

        /// <summary>
        /// Internal method for generating particles' own dimensions.
        ///	<remarks>
        ///		Particles' own dimensions will only be set if the mDynParticleAllDimensions or the mDynParticleWidth, 
        ///		mDynParticleHeight and/or mDynParticleDepth have been defined in the emitter.
        ///	</remarks>
        /// </summary>
        /// <param name="particle">Pointer to a particle</param>
        public void _initParticleDimensions(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleEmitter__initParticleDimensions(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Initialise some attributes that are time-based.
        /// </summary>
        public void _initTimeBased()
        {
            ParticleEmitter__initTimeBased(nativePtr);
        }

        /// <summary>
        /// Calculate the derived position of the emitter.
        ///	<remarks>
        ///		Note, that in script, the position is set into localspace, while if the emitter is
        ///		emitted, its position is automatically transformed . This function always returns the 
        ///		transformed (derived) position.
        ///	</remarks>
        /// </summary>
        /// <returns></returns>
        public Mogre.Vector3 GetDerivedPosition()
        {
            Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleEmitter_GetDerivedPosition(nativePtr).ToPointer()));
            return vec;
        }

        /// <summary>
        /// Perform activities when a ParticleEmitter is started.
        /// </summary>
        public void _notifyStart()
        {
            ParticleEmitter__notifyStart(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleEmitter is stopped.
        /// </summary>
        public void _notifyStop()
        {
            ParticleEmitter__notifyStop(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleEmitter is paused.
        /// </summary>
        public void _notifyPause()
        {
            ParticleEmitter__notifyPause(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleEmitter is resumed.
        /// </summary>
        public void _notifyResume()
        {
            ParticleEmitter__notifyResume(nativePtr);
        }

        /// <summary>
        /// Notify that the Particle System is rescaled.
        /// </summary>
        /// <param name="scale"></param>
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            ParticleEmitter__notifyRescaled(nativePtr, scale);
        }

        /// <summary>
        /// Copy attributes to another emitter.
        /// </summary>
        /// <param name="emitter"></param>
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            ParticleEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }

        /// <summary>
        /// Copy parent attributes to another emitter.
        /// </summary>
        /// <param name="emitter"></param>
        public void CopyParentAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            ParticleEmitter_CopyParentAttributesTo(nativePtr, emitter.nativePtr);
        }

        /// <summary>
        /// Sets the colour of an emitted particle.
        /// Gets the colour of a particle that will be emitted.
        /// </summary>
        public Mogre.ColourValue ParticleColour
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)ParticleEmitter_GetParticleColour(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, colourPtr, true);
                ParticleEmitter_SetParticleColour(nativePtr, colourPtr);
            }
        }

        /// <summary>
        /// Sets the colour range start of an emitted particle. This is the lower value used to generate a random colour.
        /// Gets the colour range start of an emitted particle.
        /// </summary>
        public Mogre.ColourValue ParticleColourRangeStart
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)ParticleEmitter_GetParticleColourRangeStart(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, colourPtr, true);
                ParticleEmitter_SetParticleColourRangeStart(nativePtr, colourPtr);
            }
        }

        /// <summary>
        /// Set the colour range end of an emitted particle. This is the upper value used to generate a random colour.
        /// Get the colour range end of an emitted particle.
        /// </summary>
        public Mogre.ColourValue ParticleColourRangeEnd
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)ParticleEmitter_GetParticleColourRangeEnd(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, colourPtr, true);
                ParticleEmitter_SetParticleColourRangeEnd(nativePtr, colourPtr);
            }
        }

        /// <summary>
        /// Sets the texture coords of an emitted particle.
        /// Gets the texture coords of an emitted particle.
        /// </summary>
        public ushort ParticleTextureCoords { get { return ParticleEmitter_GetParticleTextureCoords(nativePtr); } set { ParticleEmitter_SetParticleTextureCoords(nativePtr, value); } }

        /// <summary>
        /// Set the texture coords range start of an emitted particle. This is the lower value used to set a random texture coords.
        /// Get the texture coords range start  of an emitted particle.
        /// </summary>
        public ushort ParticleTextureCoordsRangeStart { get { return ParticleEmitter_GetParticleTextureCoordsRangeStart(nativePtr); } set { ParticleEmitter_SetParticleTextureCoordsRangeStart(nativePtr, value); } }

        /// <summary>
        /// Set the texture coords range end of an emitted particle. This is the upper value used to set a random texture coords.
        /// Get the texture coords range end of an emitted particle.
        /// </summary>
        public ushort ParticleTextureCoordsRangeEnd { get { return ParticleEmitter_GetParticleTextureCoordsRangeEnd(nativePtr); } set { ParticleEmitter_SetParticleTextureCoordsRangeEnd(nativePtr, value); } }

        /// <summary>
        /// If this attribute is set to 'true', the particles are emitted relative to the emitter
        /// </summary>
        public bool KeepLocal { get { return ParticleEmitter_IsKeepLocal(nativePtr); } set { ParticleEmitter_SetKeepLocal(nativePtr, value); } }

        /// <summary>
        /// Transforms the particle position in a local position relative to the emitter
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public bool MakeParticleLocal(Particle particle)
        {
            return ParticleEmitter_MakeParticleLocal(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Forwards an event to the parent technique.
        /// </summary>
        /// <param name="particleUniverseEvent"></param>
        public void pushEvent(ParticleUniverseEvent particleUniverseEvent)
        {
            if (particleUniverseEvent == null)
                throw new ArgumentNullException("particleUniverseEvent cannot be null!");
            ParticleEmitter_PushEvent(nativePtr, particleUniverseEvent.NativePointer);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParentTechnique(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetEmitterType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetEmitterType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetEmitterType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetEmitterType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitterType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynAngle(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynAngle(IntPtr ptr, IntPtr dynAngle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynEmissionRate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynEmissionRate(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynEmissionRate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynEmissionRate(IntPtr ptr, IntPtr dynEmissionRate);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynTotalTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynTotalTimeToLive(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynTotalTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynTotalTimeToLive(IntPtr ptr, IntPtr dynTotalTimeToLive);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynParticleMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynParticleMass(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleMass(IntPtr ptr, IntPtr dynParticleMass);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynVelocity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynVelocity(IntPtr ptr, IntPtr dynVelocity);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynDuration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynDuration(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynDuration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynDuration(IntPtr ptr, IntPtr dynDuration);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynDurationSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynDurationSet(IntPtr ptr, bool durationSet);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynRepeatDelay", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynRepeatDelay(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynRepeatDelay", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynRepeatDelay(IntPtr ptr, IntPtr dynRepeatDelay);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynRepeatDelaySet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynRepeatDelaySet(IntPtr ptr, bool repeatDelaySet);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynParticleAllDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynParticleAllDimensions(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleAllDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleAllDimensions(IntPtr ptr, IntPtr dynParticleAllDimensions);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleAllDimensionsSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleAllDimensionsSet(IntPtr ptr, bool particleAllDimensionsSet);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynParticleWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynParticleWidth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleWidth(IntPtr ptr, IntPtr dynParticleWidth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleWidthSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleWidthSet(IntPtr ptr, bool particleWidthSet);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynParticleHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynParticleHeight(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleHeight(IntPtr ptr, IntPtr dynParticleHeight);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleHeightSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleHeightSet(IntPtr ptr, bool particleHeightSet);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDynParticleDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDynParticleDepth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleDepth(IntPtr ptr, IntPtr dynParticleDepth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetDynParticleDepthSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetDynParticleDepthSet(IntPtr ptr, bool particleDepthSet);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetEmitsType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Particle.ParticleType ParticleEmitter_GetEmitsType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetEmitsType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetEmitsType(IntPtr ptr, Particle.ParticleType emitsType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetEmitsName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetEmitsName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetEmitsName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetEmitsName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitsName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetEmissionRateCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetEmissionRateCameraDependency(IntPtr ptr, IntPtr cameraDependency);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetEmissionRateCameraDependency2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetEmissionRateCameraDependency(IntPtr ptr, float squareDistance, bool inc = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetEmissionRateCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetEmissionRateCameraDependency(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetOriginalParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetOriginalParticleDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleOrientation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleOrientation(IntPtr ptr, Mogre.Quaternion orientation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleOrientationRangeStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleOrientationRangeStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleOrientationRangeStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleOrientationRangeStart(IntPtr ptr, Mogre.Quaternion orientationRangeStart);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleOrientationRangeEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleOrientationRangeEnd(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleOrientationRangeEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleOrientationRangeEnd(IntPtr ptr, Mogre.Quaternion orientationRangeEnd);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetEnabled(IntPtr ptr, bool enabled);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleDirection(IntPtr ptr, Mogre.Vector3 direction);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_IsAutoDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleEmitter_IsAutoDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetAutoDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetAutoDirection(IntPtr ptr, bool autoDirection);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_IsForceEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleEmitter_IsForceEmission(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetForceEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetForceEmission(IntPtr ptr, bool forceEmission);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__postProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initForEmission(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initForExpiration(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleForEmission(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleDirection(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleOrientation(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__generateAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__generateAngle(IntPtr ptr, IntPtr angle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleVelocity(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleMass(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleColour(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleTextureCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleTextureCoords(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleTimeToLive", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleEmitter__initParticleTimeToLive(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__calculateRequestedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleEmitter__calculateRequestedParticles(IntPtr ptr, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initParticleDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initParticleDimensions(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__initTimeBased", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__initTimeBased(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetDerivedPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetDerivedPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__notifyStop(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__notifyPause", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__notifyPause(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__notifyResume", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__notifyResume(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_CopyParentAttributesTo(IntPtr ptr, IntPtr emitter);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleColour(IntPtr ptr, IntPtr particleColour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleColourRangeStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleColourRangeStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleColourRangeStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleColourRangeStart(IntPtr ptr, IntPtr particleColourRangeStart);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleColourRangeEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleEmitter_GetParticleColourRangeEnd(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleColourRangeEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleColourRangeEnd(IntPtr ptr, IntPtr particleColourRangeEnd);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleTextureCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleEmitter_GetParticleTextureCoords(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleTextureCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleTextureCoords(IntPtr ptr, ushort particleTextureCoords);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleTextureCoordsRangeStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleEmitter_GetParticleTextureCoordsRangeStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleTextureCoordsRangeStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleTextureCoordsRangeStart(IntPtr ptr, ushort particleTextureCoordsRangeStart);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_GetParticleTextureCoordsRangeEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleEmitter_GetParticleTextureCoordsRangeEnd(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetParticleTextureCoordsRangeEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetParticleTextureCoordsRangeEnd(IntPtr ptr, ushort particleTextureCoordsRangeEnd);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_IsKeepLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleEmitter_IsKeepLocal(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_SetKeepLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_SetKeepLocal(IntPtr ptr, bool keepLocal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_MakeParticleLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleEmitter_MakeParticleLocal(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleEmitter_PushEvent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleEmitter_PushEvent(IntPtr ptr, IntPtr particleUniverseEvent);
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
            ParticleEmitter_Destroy(NativePointer);
        }

        #endregion

    }
}
