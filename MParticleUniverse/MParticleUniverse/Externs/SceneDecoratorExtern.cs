using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.Externs
{
    /// <summary>
    /// The SceneDecoratorExtern is a test class that can be used to add additional objects to the scene. This allows quick
	///	prototyping, but it it not really usable in a real situation.
    /// </summary>
    public unsafe class SceneDecoratorExtern : Extern, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal SceneDecoratorExtern(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        internal static Dictionary<IntPtr, SceneDecoratorExtern> externInstances;

        internal static SceneDecoratorExtern GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (externInstances == null)
                externInstances = new Dictionary<IntPtr, SceneDecoratorExtern>();

            SceneDecoratorExtern newvalue;

            if (externInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SceneDecoratorExtern(ptr);
            externInstances.Add(ptr, newvalue);
            return newvalue;
        }
        
        public SceneDecoratorExtern()
        {
            nativePtr = SceneDecoratorExtern_New();
            externInstances.Add(nativePtr, this);
        }


			/** see Extern::_prepare
			*/
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            SceneDecoratorExtern__prepare(nativePtr, technique.nativePtr);
        }

			/** see Extern::_unprepare
			*/
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            SceneDecoratorExtern__unprepare(nativePtr, technique.nativePtr);
        }

			/** see Extern::_interface
			*/
        public void _interface(ParticleTechnique technique, 
				Particle particle, 
				float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SceneDecoratorExtern__interface(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

			/** Create the desired entity
	        */
        public void CreateEntity()
        {
            SceneDecoratorExtern_CreateEntity(nativePtr);
        }

			/** Destroy the entity again
	        */
        public void DestroyEntity()
        {
            SceneDecoratorExtern_DestroyEntity(nativePtr);
        }

			/** Return the name of the mesh
	        */
        public String MeshName { get { return Marshal.PtrToStringAnsi(SceneDecoratorExtern_GetMeshName(nativePtr)); } set { SceneDecoratorExtern_SetMeshName(nativePtr, value); } }

			/** Return the name of the material used for the entity.
	        */
        public String MaterialName { get { return Marshal.PtrToStringAnsi(SceneDecoratorExtern_GetMaterialName(nativePtr)); } set { SceneDecoratorExtern_SetMaterialName(nativePtr, value); } }

			/** Returns the scale value to which the node must be set.
	        */
        public Mogre.Vector3 Scale
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)SceneDecoratorExtern_GetScale(nativePtr).ToPointer()));
                return vec;
            }
            set { SceneDecoratorExtern_SetScale(nativePtr, value); }
        }

			/** Returns the position value to which the node must be set.
	        */
        public Mogre.Vector3 Position
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)SceneDecoratorExtern_GetPosition(nativePtr).ToPointer()));
                return vec;
            }
            set { SceneDecoratorExtern_SetPosition(nativePtr, value); }
        }

			/** See Extern::_notifyStart.
			*/
        public void _notifyStart()
            {
                SceneDecoratorExtern__notifyStart(nativePtr);
            }

			/** See Extern::_notifyStop.
			*/
        public void _notifyStop()
            {
                SceneDecoratorExtern__notifyStop(nativePtr);
            }

			/** See Extern:copyAttributesTo
	        */
        public void CopyAttributesTo(Extern externObject)
            {
                if (externObject == null)
                    throw new ArgumentNullException("externObject cannot be null!");
                SceneDecoratorExtern_CopyAttributesTo(nativePtr, externObject.NativePointer);
            }

        #region SceneDecoratorExtern
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SceneDecoratorExtern_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_CopyAttributesTo(IntPtr ptr, IntPtr externObject);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern__interface(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_CreateEntity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_CreateEntity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_DestroyEntity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_DestroyEntity(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_GetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SceneDecoratorExtern_GetMeshName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_SetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_SetMeshName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string meshName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_GetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SceneDecoratorExtern_GetMaterialName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_SetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_SetMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_GetScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SceneDecoratorExtern_GetScale(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_SetScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_SetScale(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_GetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SceneDecoratorExtern_GetPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern_SetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern_SetPosition(IntPtr ptr, Mogre.Vector3 position);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SceneDecoratorExtern__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SceneDecoratorExtern__notifyStop(IntPtr ptr);
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
            SceneDecoratorExtern_Destroy(NativePointer);
            externInstances.Remove(nativePtr);
        }

        #endregion

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
        #endregion

        #region IAlias PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern AliasType IAlias_GetAliasType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasType(IntPtr ptr, AliasType aliasType);


        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr IAlias_GetAliasName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string aliasName);
        #endregion

        #region Extern Implementation

        /// <summary>
        /// Name of the extern (optional)
        /// </summary>
        public String Name
        { get { return Marshal.PtrToStringAnsi(Extern_GetName(nativePtr)); } set { Extern_SetName(nativePtr, value); } }

        /// <summary>
        /// Type of extern
        /// </summary>
        public String Type
        { get { return Marshal.PtrToStringAnsi(Extern_GetExternType(nativePtr)); } set { Extern_SetExternType(nativePtr, value); } }

        /// <summary>
        /// Parent
        /// </summary>
        public ParticleTechnique ParentTechnique
        {
            get { return ParticleTechnique.GetInstances(Extern_GetParentTechnique(nativePtr)); }
            set
            {
                if (value == null)
                    Extern_SetParentTechnique(nativePtr, IntPtr.Zero);
                else
                    Extern_SetParentTechnique(nativePtr, value.NativePointer);
            }
        }


        /// <summary>
        /// Notify that the Particle System is rescaled.
        /// </summary>
        /// <param name="scale">Scale Value</param>
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            Extern__notifyRescaled(nativePtr, scale);
        }


        /// <summary>
        /// Copy parent attributes to another extern object.
        /// </summary>
        /// <param name="externObject"></param>
        public void CopyParentAttributesTo(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            Extern_CopyParentAttributesTo(nativePtr, externObject.NativePointer);
        }

        /// <summary>
        /// Perform activities when an Extern is paused.
        /// </summary>
        public void _notifyPause()
        {
            Extern__notifyPause(nativePtr);
        }

        /// <summary>
        /// Perform activities when an Extern is resumed.
        /// </summary>
        public void _notifyResume()
        {
            Extern__notifyResume(nativePtr);
        }

        /// <summary>
        /// Perform activities before the individual particles are processed.
        /// <remarks>
        /// This function is called before the ParticleTechnique update-loop where all particles are traversed.
        ///    the preProcess is typically used to perform calculations where the result must be used in 
        ///    processing each individual particle.
        /// </remarks>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            Extern__preProcessParticles(nativePtr, technique.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Initialise a newly emitted particle.
        /// </summary>
        /// <param name="particle">particle Pointer to a Particle to initialise.</param>
        public void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Extern__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        /// <summary>
        ///  Perform actions if a particle gets expired.
        /// </summary>
        /// <param name="particle"></param>
        public void _initParticleForExpiration(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Extern__initParticleForExpiration(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Perform precalculations if the first Particle in the update-loop is processed.
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        public void _firstParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Extern__firstParticle(nativePtr, particleTechnique.NativePointer, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Processes a particle.
        /// <remarks>
        /// Some processing on a particle can be performed. This function is automatically called in the 
        ///    ParticleTechnique update-loop where all particles are traversed.
        /// </remarks>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        /// <param name="firstParticle"></param>
        public void _processParticle(ParticleTechnique technique, Particle particle, float timeElapsed, bool firstParticle)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Extern__processParticle(nativePtr, technique.NativePointer, particle.NativePointer, timeElapsed, firstParticle);
        }

        /// <summary>
        /// Perform activities after the individual particles are processed.
        /// <remarks>
        /// This function is called after the ParticleTechnique update-loop where all particles are traversed.
        /// </remarks>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        public void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            Extern__postProcessParticles(nativePtr, technique.NativePointer, timeElapsed);
        }


        #endregion

        #region Extern PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_GetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Extern_GetName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "voidExtern_SetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern_SetName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_GetExternType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Extern_GetExternType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_SetExternType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern_SetExternType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_GetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Extern_GetParentTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern_CopyAttributesTo(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern_CopyParentAttributesTo(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__prepare(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__unprepare(IntPtr ptr, IntPtr particleTechnique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__notifyStart(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__notifyPause", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__notifyPause(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__notifyResume", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__notifyResume(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__notifyStop(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__preProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__initParticleForEmission(IntPtr ptr, IntPtr particle);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__initParticleForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__initParticleForExpiration(IntPtr ptr, IntPtr particle);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__firstParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__firstParticle(IntPtr ptr, IntPtr particleTechnique,
                        IntPtr particle,
                        float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__processParticle(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed, bool firstParticle);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__interface(IntPtr ptr, IntPtr technique,
                        IntPtr particle,
                        float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Extern__postProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Extern__postProcessParticles(IntPtr ptr, IntPtr technique, float timeElapsed);


        #endregion
    }
}
