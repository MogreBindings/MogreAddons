using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// Abstract class defining the interface to be implemented by particle affectors.
    ///<remarks>
    ///	Particle affectors modify particles in a particle system over their lifetime. This class defines 
    ///	the interface, and provides a few implementations of some basic functions.</remarks>
    /// </summary>
    public unsafe abstract class ParticleAffector : Particle, IAlias, IElement, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr;}
            set { nativePtr = value; }
        }

        internal ParticleAffector(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        internal static String[] ParticleAffectorTypes = 
        {
            "Align",
            "BoxCollider",
            "CollisionAvoidance",
            "Colour",
            "FlockCentering",
            "ForceField",
            "GeometryRotator",
            "Gravity",
            "InterParticleCollider",
            "Jet",
            "Line",
            "LinearForce",
            "ParticleFollower",
            "PathFollower",
            "PlaneCollider",
            "Randomiser",
            "Scale",
            "ScaleVelocity",
            "SineForce",
            "SphereCollider",
            "TextureAnimator",
            "TextureRotator",
            "VelocityMatching",
            "Vortex"
        };

        internal static ParticleAffector GetParticleAffectorByType(IntPtr particleAffectorReference)
        {
            if (particleAffectorReference == null || particleAffectorReference == IntPtr.Zero)
                return null;

            String typeToLookup = Marshal.PtrToStringAnsi(ParticleAffector_GetAffectorType(particleAffectorReference));
            int affectorIdx = -1;
            for (int i = 0; i < ParticleAffectorTypes.Length; i++)
            {
                if (typeToLookup == ParticleAffectorTypes[i])
                {
                    affectorIdx = i;
                    break;
                }
            }

            switch (affectorIdx)
            {
                case 0:
                    return ParticleAffectors.AlignAffector.GetInstance(particleAffectorReference);
                case 1:
                    return ParticleAffectors.BoxCollider.GetInstance(particleAffectorReference);
                case 2:
                    return ParticleAffectors.CollisionAvoidanceAffector.GetInstance(particleAffectorReference);
                case 3:
                    return ParticleAffectors.ColourAffector.GetInstance(particleAffectorReference);
                case 4:
                    return ParticleAffectors.FlockCenteringAffector.GetInstance(particleAffectorReference);
                case 5:
                    return ParticleAffectors.ForceFieldAffector.GetInstance(particleAffectorReference);
                case 6:
                    return ParticleAffectors.GeometryRotator.GetInstance(particleAffectorReference);
                case 7:
                    return ParticleAffectors.GravityAffector.GetInstance(particleAffectorReference);
                case 8:
                    return ParticleAffectors.InterParticleCollider.GetInstance(particleAffectorReference);
                case 9:
                    return ParticleAffectors.JetAffector.GetInstance(particleAffectorReference);
                case 10:
                    return ParticleAffectors.LineAffector.GetInstance(particleAffectorReference);
                case 11:
                    return ParticleAffectors.LinearForceAffector.GetInstance(particleAffectorReference);
                case 12:
                    return ParticleAffectors.ParticleFollower.GetInstance(particleAffectorReference);
                case 13:
                    return ParticleAffectors.PathFollower.GetInstance(particleAffectorReference);
                case 14:
                    return ParticleAffectors.PlaneCollider.GetInstance(particleAffectorReference);
                case 15:
                    return ParticleAffectors.Randomiser.GetInstance(particleAffectorReference);
                case 16:
                    return ParticleAffectors.ScaleAffector.GetInstance(particleAffectorReference);
                case 17:
                    return ParticleAffectors.ScaleVelocityAffector.GetInstance(particleAffectorReference);
                case 18:
                    return ParticleAffectors.SineForceAffector.GetInstance(particleAffectorReference);
                case 19:
                    return ParticleAffectors.SphereCollider.GetInstance(particleAffectorReference);
                case 20:
                    return ParticleAffectors.TextureAnimator.GetInstance(particleAffectorReference);
                case 21:
                    return ParticleAffectors.TextureRotator.GetInstance(particleAffectorReference);
                case 22:
                    return ParticleAffectors.VelocityMatchingAffector.GetInstance(particleAffectorReference);
                case 23:
                    return ParticleAffectors.VortexAffector.GetInstance(particleAffectorReference);
                default: //Not Found!
                    return null;
            }
        }

        /// <summary>
        ///The AffectSpecialisation enumeration is used to specialise the affector even more. This enumeration 
        ///isn't used by all affectors; in some cases it isn't even applicable.
        /// </summary>
        public enum AffectSpecialisation
        {
            AFSP_DEFAULT = 0,
            AFSP_TTL_INCREASE = 1,
            AFSP_TTL_DECREASE = 2
        };

        // Default values
        public const bool DEFAULT_ENABLED = true;
        public static Mogre.Vector3 DEFAULT_POSITION { get { return new Mogre.Vector3(0, 0, 0); } }
        public const AffectSpecialisation DEFAULT_SPECIALISATION = ParticleAffector.AffectSpecialisation.AFSP_DEFAULT;

        //public ParticleAffector()
        //{
        //    nativePtr = ParticleAffector_New();
        //}

        public String AffectorType { get { return Marshal.PtrToStringAnsi(ParticleAffector_GetAffectorType(nativePtr)); } set { ParticleAffector_SetAffectorType(nativePtr, value); } }

        public String Name { get { return Marshal.PtrToStringAnsi(ParticleAffector_GetName(nativePtr)); } set { ParticleAffector_SetName(nativePtr, value); } }

        public ParticleTechnique ParentTechnique { 
            get { return ParticleTechnique.GetInstances(ParticleAffector_GetParentTechnique(nativePtr)); } 
            set { 
                if (value == null)
                    ParticleAffector_SetParentTechnique(nativePtr, IntPtr.Zero); 
                else
                    ParticleAffector_SetParentTechnique(nativePtr, value.nativePtr); 
            }
        }

        /// <summary>
        /// Perform initialisation actions.
        ///			<remarks>
        ///				The _prepare() function is automatically called during initialisation (prepare) activities of a 
        ///				ParticleTechnique. A subclass could implement this function to perform initialisation 
        ///				actions.</remarks>
        /// </summary>
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            ParticleAffector__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /// <summary>
        /// Reverse the actions from the _prepare.
        /// </summary>
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            ParticleAffector__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleAffector is started.
        /// </summary>
        public void _notifyStart()
        {
            ParticleAffector__notifyStart(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleAffector is stopped.
        /// </summary>
        public void _notifyStop()
        {
            ParticleAffector__notifyStop(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleAffector is paused.
        ///
        public void _notifyPause()
        {
            ParticleAffector__notifyPause(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleAffector is resumed.
        /// </summary>
        public void _notifyResume()
        {
            ParticleAffector__notifyResume(nativePtr);
        }

        /// <summary>
        /// Notify that the Affector is rescaled.
        /// </summary>
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null");
            ParticleAffector__notifyRescaled(nativePtr, scale);
        }

        /// <summary>
        /// Perform activities before the individual particles are processed.
        ///			<remarks>
        ///				This function is called before the ParticleTechnique update-loop where all particles are traversed.
        ///				the preProcess is typically used to perform calculations where the result must be used in 
        ///				processing each individual particle.
        ///				</remarks>
        /// </summary>
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            ParticleAffector__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// Perform precalculations if the first Particle in the update-loop is processed.
        /// </summary>
        public void _firstParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null");
            ParticleAffector__firstParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Initialise the ParticleAffector before it is emitted itself.
        /// </summary>
        public void _initForEmission()
        {
            ParticleAffector__initForEmission(nativePtr);
        }

        /// <summary>
        /// Initialise the ParticleAffector before it is expired itself.
        /// </summary>
        public void _initForExpiration(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null");
            ParticleAffector__initForExpiration(nativePtr, technique.nativePtr, timeElapsed);
        }

        /// <summary>
        ///Initialise a newly emitted particle.
        ///<param name="particle">
        ///	particle Pointer to a Particle to initialise.</param>
        /// </summary>
        public void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null");
            ParticleAffector__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Entry point for affecting a Particle.
        ///	<remarks>
        ///	Before the actual _affect() function is called, validations have to take place whether
        ///	affecting the Particle is really needed. Particles which are emitted by a ParticleEmitter
        ///	that has been excluded, will not be affected. This _affect() function is internally called.
        ///	</remarks>
        ///	<param name="particleTechnique">
        ///	particle Pointer to a ParticleTechnique to which the particle belongs.</param>
        ///	<param name="particle">
        ///	particle Pointer to a Particle.</param>
        ///	<param name="timeElapsed">
        ///	timeElapsed The number of seconds which have elapsed since the last call.</param>
        ///	<param name="firstParticle">
        ///	firstParticle Determines whether the ParticleAffector encounters the first particle of all active particles.</param>
        /// </summary>
        public void _processParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed,
            bool firstParticle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null");
            ParticleAffector__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        /// <summary>
        ///Perform activities after the individual particles are processed.
        ///	<remarks>
        ///	This function is called after the ParticleTechnique update-loop where all particles are traversed.
        ///	</remarks>
        /// </summary>
        public void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null");
            ParticleAffector__postProcessParticles(nativePtr, technique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// Affect a particle.
        ///	<param name="particleTechnique">
        ///	particle Pointer to a ParticleTechnique to which the particle belongs.</param>
        ///	<param name="particle">
        ///	particle Pointer to a Particle.</param>
        ///	<param name="timeElapsed">
        ///	timeElapsed The number of seconds which have elapsed since the last call.</param>
        /// </summary>
        public void _affect(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null");
            ParticleAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Add a ParticleEmitter name that excludes Particles emitted by this ParticleEmitter from being
        ///	affected.
        /// </summary>
        public void AddEmitterToExclude(String emitterName)
        {
            ParticleAffector_AddEmitterToExclude(nativePtr, emitterName);
        }

        /// <summary>
        /// Remove a ParticleEmitter name that excludes Particles emitted by this ParticleEmitter.
        /// </summary>
        public void RemoveEmitterToExclude(String emitterName)
        {
            ParticleAffector_RemoveEmitterToExclude(nativePtr, emitterName);
        }

        /// <summary>
        /// Remove all ParticleEmitter names that excludes Particles emitted by this ParticleEmitter.
        /// </summary>
        public void RemoveAllEmittersToExclude()
        {
            ParticleAffector_RemoveAllEmittersToExclude(nativePtr);
        }

        /// <summary>
        /// Return the list with emitters to exclude.
        /// </summary>
        public String[] GetEmittersToExclude()
        {
            try
            {
                int bufSize = ParticleAffector_GetEmittersToExcludeSize(NativePointer);
                if (bufSize == 0)
                    return null;
                char[] buf = new char[bufSize]; //[bufSize];

                String[] toReturn;

                fixed (char* bufHandle = buf)
                {
                    ParticleAffector_GetEmittersToExclude(NativePointer, buf, buf.Length);

                    String strArr = new String(buf);
                    toReturn = strArr.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                }
                return toReturn;
            }
            catch (Exception e) { }
            return null;
        }

        /// <summary>
        /// Returns true if the list with excluded emitters contains a given name.
        /// </summary>
        public bool HasEmitterToExclude(String emitterName)
        {
            return ParticleAffector_HasEmitterToExclude(nativePtr, emitterName);
        }

        /// <summary>
        /// Copy attributes to another affector.
        /// </summary>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null");
            Particle_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /// <summary>
        /// Copy parent attributes to another affector.
        /// </summary>
        public void CopyParentAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null");
            ParticleAffector_CopyParentAttributesTo(nativePtr, affector.nativePtr);
        }

        /// <summary>
        /// Calculate the derived position of the affector.
        ///	<remarks>
        ///	Note, that in script, the position is set as localspace, while if the affector is
        ///	emitted, its position is automatically transformed. This function always returns 
        ///	the derived position.</remarks>
        /// </summary>
        public Mogre.Vector3 DerivedPosition
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleAffector_GetDerivedPosition(nativePtr).ToPointer()));
                return vec;
            }
        }

        /// <summary>
        /// If the mAffectSpecialisation is used to specialise the affector, a factor can be calculated and used
        ///	in a child class. This factor depends on the value of mAffectSpecialisation.
        ///	<remarks>
        ///	This helper method assumes that the particle pointer is valid.</remarks>
        /// </summary>
        public float _calculateAffectSpecialisationFactor(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null");
            return ParticleAffector__calculateAffectSpecialisationFactor(nativePtr, particle.NativePointer);
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
            ParticleAffector_Destroy(NativePointer);
        }

        #endregion


        #region PInvoke
        //[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_New", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr ParticleAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetAffectSpecialisation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleAffector_GetAffectSpecialisation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_SetAffectSpecialisation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_SetAffectSpecialisation(IntPtr ptr, IntPtr affectSpecialisation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetAffectorType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleAffector_GetAffectorType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_SetAffectorType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_SetAffectorType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string affectorType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleAffector_GetName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_SetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_SetName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleAffector_GetParentTechnique(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__notifyStop(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__notifyPause", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__notifyPause(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__notifyResume", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__notifyResume(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__firstParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__firstParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__initForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__initForEmission(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__initForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__initForExpiration(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__initParticleForEmission(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__processParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed, bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__postProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_AddEmitterToExclude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_AddEmitterToExclude(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitterName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_RemoveEmitterToExclude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_RemoveEmitterToExclude(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitterName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_RemoveAllEmittersToExclude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_RemoveAllEmittersToExclude(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetEmittersToExcludeSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleAffector_GetEmittersToExcludeSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetEmittersToExclude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_GetEmittersToExclude(IntPtr ptr, [Out]  [MarshalAs(UnmanagedType.LPArray)] char[] templateNameBuffer, int bufferSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_HasEmitterToExclude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleAffector_HasEmitterToExclude(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitterName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleAffector_CopyParentAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector_GetDerivedPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleAffector_GetDerivedPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleAffector__calculateAffectSpecialisationFactor", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleAffector__calculateAffectSpecialisationFactor(IntPtr ptr, IntPtr particle);

        #endregion
    }
}
