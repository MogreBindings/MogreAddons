using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// In analogy of Ogre's material system, the ParticleTechnique is introduced. It forms an extra layer 
    ///	between particle emitters, affectors, etc. on one side, and the particle system and the other side.
    ///	A ParticleTechnique has a few benefits. For example, with the use of a ParticleTechnique it is possible
    ///	to implement Particle LOD (Level Of Detail). Also combining multiple renderers and material within one 
    ///	ParticleSystem is possible.
    /// </summary>
    public unsafe class ParticleTechnique : Particle, IAlias, IElement, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, ParticleTechnique> particleTechniqueInstances;

        internal static ParticleTechnique GetInstances(IntPtr ptr)
        {
            if (particleTechniqueInstances == null)
                particleTechniqueInstances = new Dictionary<IntPtr, ParticleTechnique>();

            ParticleTechnique newvalue;

            if (particleTechniqueInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ParticleTechnique(ptr);
            particleTechniqueInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ParticleTechnique(IntPtr ptr)
        {

            emitters =  new List<ParticleEmitter>();
            affectors = new List<ParticleAffector>();
            observers = new List<ParticleObserver>();
            behaviours = new List<ParticleBehaviour>();
            externs = new List<Extern>();
            listeners = new List<TechniqueListener>();


            nativePtr = ptr;
            //try
            //{
            //    IntPtr parentPtr = ParticleTechnique_GetParentSystem(nativePtr);
            //    if (parentPtr != IntPtr.Zero && parentPtr != ptr)
            //        this.parent = new ParticleSystem(parentPtr);
            //}
            //catch (Exception) { }

            this.name = Marshal.PtrToStringAnsi(ParticleTechnique_GetName(nativePtr));

            //IntPtr scalePtr = ParticleTechnique_GetWorldBoundingBox(nativePtr);
            //Mogre.AxisAlignedBox aabtype = new Mogre.AxisAlignedBox();
            //Mogre.AxisAlignedBox vec = (Mogre.AxisAlignedBox)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
            //worldBoundingBox = vec;

            wCameraDependency = CameraDependency.GetInstance(ParticleTechnique_GetWidthCameraDependency(nativePtr));
            if (wCameraDependency == null || wCameraDependency.nativePtr.Equals(IntPtr.Zero))
                wCameraDependency = null;
            hCameraDependency = CameraDependency.GetInstance(ParticleTechnique_GetHeightCameraDependency(nativePtr));
            if (hCameraDependency == null || hCameraDependency.nativePtr.Equals(IntPtr.Zero))
                hCameraDependency = null;
            dCameraDependency = CameraDependency.GetInstance(ParticleTechnique_GetDepthCameraDependency(nativePtr));
            if (dCameraDependency == null || dCameraDependency.nativePtr.Equals(IntPtr.Zero))
                dCameraDependency = null;

            uint count = (uint)ParticleTechnique__getNumBehaviourTemplates(nativePtr);
            for (uint i = 0; i < count; i++)
            {
                ParticleBehaviour b = ParticleBehaviourHelper.GetParticleBehaviour(ParticleTechnique__getBehaviourTemplate(nativePtr, i));
                if (b != null)
                    behaviours.Add(b);
            }

            renderer = ParticleRendererHelper.GetParticleRenderer(ParticleTechnique_GetRenderer(nativePtr));

            count = ParticleTechnique_GetNumObservers(nativePtr);
            for (uint i = 0; i < count; i++)
            {
                ParticleObserver o = ParticleObserver.GetObserverByPtr(ParticleTechnique_GetObserver(nativePtr, i));
                if (o != null)
                    observers.Add(o);
            }

            count = ParticleTechnique_GetNumAffectors(nativePtr);
            for (uint i = 0; i < count; i++)
            {
                ParticleAffector a = ParticleAffector.GetParticleAffectorByType(ParticleTechnique_GetAffector(nativePtr, i));
                if (a != null)
                    affectors.Add(a);
            }

            count = ParticleTechnique_GetNumEmitters(nativePtr);
            for (uint i = 0; i < count; i++)
            {
                ParticleEmitter e = ParticleEmitter.GetParticleEmitterByPointer(ParticleTechnique_GetEmitter(nativePtr, i));
                if (e != null)
                    emitters.Add(e);
            }

        }

        // Default values
        public const bool DEFAULT_ENABLED = true;
        public static Mogre.Vector3 DEFAULT_POSITION { get { return new Mogre.Vector3(0, 0, 0); } }
        public const bool DEFAULT_KEEP_LOCAL = false;
        public const uint DEFAULT_VISUAL_PARTICLE_QUOTA = 500;
        public const uint DEFAULT_EMITTED_EMITTER_QUOTA = 50;
        public const uint DEFAULT_EMITTED_TECHNIQUE_QUOTA = 10;
        public const uint DEFAULT_EMITTED_AFFECTOR_QUOTA = 10;
        public const uint DEFAULT_EMITTED_SYSTEM_QUOTA = 10;
        public const ushort DEFAULT_LOD_INDEX = 0;
        public const float DEFAULT_DEFAULT_WIDTH = 50;
        public const float DEFAULT_DEFAULT_HEIGHT = 50;
        public const float DEFAULT_DEFAULT_DEPTH = 50;
        public const ushort DEFAULT_SPATIAL_HASHING_CELL_DIM = 15;
        public const ushort DEFAULT_SPATIAL_HASHING_CELL_OVERLAP = 0;
        public const uint DEFAULT_SPATIAL_HASHING_TABLE_SIZE = 50;
        public const float DEFAULT_SPATIAL_HASHING_INTERVAL = 0.05f;
        public const float DEFAULT_MAX_VELOCITY = 9999.0f;

        ParticleSystem parent;
        String name;
        String materialName;
        List<ParticleEmitter> emitters;
        List<ParticleAffector> affectors;
        List<ParticleObserver> observers;
        ParticleRenderer renderer;
        List<ParticleBehaviour> behaviours;
        List<Extern> externs;
        Mogre.AxisAlignedBox worldBoundingBox;
        CameraDependency wCameraDependency;
        CameraDependency hCameraDependency;
        CameraDependency dCameraDependency;
        List<TechniqueListener> listeners;


        public ParticleTechnique()
        {
            nativePtr = ParticleTechnique_New();

            emitters = new List<ParticleEmitter>();
            affectors = new List<ParticleAffector>();
            observers = new List<ParticleObserver>();
            behaviours = new List<ParticleBehaviour>();
            externs = new List<Extern>();
            listeners = new List<TechniqueListener>();

            particleTechniqueInstances.Add(nativePtr, this);
        }

        public ParticleSystem ParentSystem { 
            get { 
                try{
                    parent = ParticleSystem.GetInstances(ParticleTechnique_GetParentSystem(nativePtr));
                }
                catch (SEHException){ }
                return parent;
            }
            set{
                if (value == null)
                {
                    ParticleTechnique_SetParentSystem(nativePtr, IntPtr.Zero);
                    parent = null;
                }
                else
                {
                    ParticleTechnique_SetParentSystem(nativePtr, value.nativePtr);
                    parent = value;
                }
            }
        }

        public String Name { 
            get {
                name = Marshal.PtrToStringAnsi(ParticleTechnique_GetName(nativePtr));
                return name; 
            }
            set { 
                ParticleTechnique_SetName(nativePtr, value);
                name = value;
            }
        }

        public uint VisualParticleQuota { get { return ParticleTechnique_GetVisualParticleQuota(nativePtr); } set { ParticleTechnique_SetVisualParticleQuota(nativePtr, value); } }

        public uint EmittedEmitterQuota { get { return ParticleTechnique_GetEmittedEmitterQuota(nativePtr); } set { ParticleTechnique_SetEmittedEmitterQuota(nativePtr, value); } }

        public uint EmittedTechniqueQuota { get { return ParticleTechnique_GetEmittedTechniqueQuota(nativePtr); } set { ParticleTechnique_SetEmittedTechniqueQuota(nativePtr, value); } }

        public uint EmittedAffectorQuota { get { return ParticleTechnique_GetEmittedAffectorQuota(nativePtr); } set { ParticleTechnique_SetEmittedAffectorQuota(nativePtr, value); } }

        public uint EmittedSystemQuota { get { return ParticleTechnique_GetEmittedSystemQuota(nativePtr); } set { ParticleTechnique_SetEmittedSystemQuota(nativePtr, value); } }

        public float DefaultWidth { get { return ParticleTechnique_GetDefaultWidth(nativePtr); } set { ParticleTechnique_SetDefaultWidth(nativePtr, value); } }

        public float DefaultHeight { get { return ParticleTechnique_GetDefaultHeight(nativePtr); } set { ParticleTechnique_SetDefaultHeight(nativePtr, value); } }

        public float DefaultDepth { get { return ParticleTechnique_GetDefaultWidth(nativePtr); } set { ParticleTechnique_SetDefaultWidth(nativePtr, value); } }

        /// <summary>
        /// Returns the derived position of the technique.
        ///	<remarks>
        ///		Note, that in script, the position is set into localspace, while if the technique is
        ///		emitted, its position is automatically transformed. This function always returns the 
        ///		derived position.
        ///		</remarks>
        /// </summary>
        public Mogre.Vector3 DerivedPosition
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleTechnique_GetDerivedPosition(nativePtr).ToPointer()));
                return vec;
            }
        }

        /// <summary>
        /// Get/Set the squared distance between camera and ParticleTechnique.
        /// </summary>
        public float CameraSquareDistance { get { return ParticleTechnique_GetCameraSquareDistance(nativePtr); } set { ParticleTechnique_SetCameraSquareDistance(nativePtr, value); } }

        /// <summary>
        /// Function to suppress notification of an emission change.
        /// </summary>
        /// <param name="suppress"></param>
        public void SuppressNotifyEmissionChange(bool suppress)
        {
            ParticleTechnique_SuppressNotifyEmissionChange(nativePtr, suppress);
        }

        /// <summary>
        /// Set the name of the material used by the renderer.
        /// </summary>
        public String MaterialName { 
            get { 
                materialName = Marshal.PtrToStringAnsi(ParticleTechnique_GetMaterialName(nativePtr));
                return materialName;
            }
            set { ParticleTechnique_SetMaterialName(nativePtr, value);
            materialName = value;
            }
        }

        /// <summary>
        /// Get the material pointer.
        /// </summary>
        public Mogre.MaterialPtr Material 
        {
            get { 
                IntPtr mat = ParticleTechnique_GetMaterial(nativePtr);
                if (mat == IntPtr.Zero)
                    return null;
                return (Mogre.MaterialPtr)Marshal.PtrToStructure(mat, typeof(Mogre.MaterialPtr)); 
            }
        }

        /// <summary>
        /// Set the name of the material used by the renderer.
        /// </summary>
        /// <param name="materialName"></param>
        //public void SetMaterialName(String materialName)
       // {
        //    ParticleTechnique_SetMaterialName(nativePtr, materialName);
        //}

        #region -------------------------------------- Emitter --------------------------------------
        /// <summary>
        /// Create a ParticleEmitter and add it to this ParticleTechnique.
        /// </summary>
        /// <param name="emitterType"></param>
        /// <returns></returns>

        public ParticleEmitter CreateEmitter(String emitterType)
        {
            IntPtr ptr = ParticleTechnique_CreateEmitter(nativePtr, emitterType);
            ParticleEmitter e = ParticleEmitter.GetParticleEmitterByPointer(ptr, emitterType);
            if (e != null && !emitters.Contains(e))
                emitters.Add(e);
            return e;
        }

        /// <summary>
        /// Add a ParticleEmitter to this ParticleTechnique.
        ///	<remarks>
        ///		It must be possible to add a previously created emitter to the list. This is the case with 
        ///		emitters that were created outside the technique. An example is the creation of emitters by means
        ///		of a script. The emitter will be placed under control of the technique. The Emitter Factory
        ///		however, deletes the emitters (since they are also created by the factory).</remarks>
        /// </summary>
        /// <param name="emitter">emitter Pointer to a previously created emitter.</param>
        public void AddEmitter(ParticleEmitter emitter)
        {
            if (emitter != null)
            {
                ParticleTechnique_AddEmitter(nativePtr, emitter.nativePtr);
                if (!emitters.Contains(emitter))
                    emitters.Add(emitter);
            }
        }

        /// <summary>
        /// Remove a ParticleEmitter from the ParticleTechnique, but don´t delete it.
        /// </summary>
        /// <param name="emitter">Pointer to a ParticleEmitter object.</param>
        public void RemoveEmitter(ParticleEmitter emitter)
        {
            if (emitter != null)
            {
                ParticleTechnique_RemoveEmitter(nativePtr, emitter.nativePtr);
                emitters.Remove(emitter);
            }
        }

        /// <summary>
        /// Get a ParticleEmitter. 
        /// </summary>
        /// <param name="index">Search by index.</param>
        /// <returns></returns>
        public ParticleEmitter GetEmitter(uint index)
        {
            ParticleEmitter e = ParticleEmitter.GetParticleEmitterByPointer(ParticleTechnique_GetEmitter(nativePtr, index));
            if (e != null && !emitters.Contains(e))
                emitters.Add(e);
            return e;
        }

        /// <summary>
        /// Get a ParticleEmitter. 
        /// </summary>
        /// <param name="emitterName">Search by name.</param>
        /// <returns></returns>
        public ParticleEmitter GetEmitter(String emitterName)
        {
            ParticleEmitter e = ParticleEmitter.GetParticleEmitterByPointer(ParticleTechnique_GetEmitter(nativePtr, emitterName));
            if (e != null && !emitters.Contains(e))
                emitters.Add(e);
            return e;
        }

        /// <summary>
        /// Get the number of ParticleEmitters added to this ParticleTechnique.
        /// </summary>
        /// <returns></returns>
        public uint GetNumEmitters()
        {
            return ParticleTechnique_GetNumEmitters(nativePtr);
        }

        /// <summary>
        ///  Delete a ParticleEmitter that is part of this ParticleTechnique. 
        /// </summary>
        /// <param name="index">Search by index.</param>
        public void DestroyEmitter(uint index)
        {
            ParticleTechnique_DestroyEmitter(nativePtr, index);
            emitters.RemoveAt((int)index);
        }

        /// <summary>
        /// Delete a ParticleEmitter that is part of this ParticleTechnique.
        /// </summary>
        /// <param name="emitter"></param>
        public void DestroyEmitter(ParticleEmitter emitter)
        {
            if (emitter != null)
            {
                ParticleTechnique_DestroyEmitter(nativePtr, emitter.nativePtr);
                emitters.Remove(emitter);
            }
        }

        /// <summary>
        /// Delete all ParticleEmitters of this ParticleTechnique.
        /// </summary>
        public void DestroyAllEmitters()
        {
            ParticleTechnique_DestroyAllEmitters(nativePtr);
            emitters.Clear();
        }

        /// <summary>
        /// Get the number of emitted ParticleEmitters.
        ///<remarks>
        ///	This doesn't return the real number of emitted emitters, but the number of emitters that are
        ///	marked for emission.</remarks>
        /// </summary>
        /// <returns></returns>
        public uint GetNumEmittedEmitters()
        {
            return ParticleTechnique_GetNumberOfEmittedParticles(nativePtr);
        }

        #endregion
        #region -------------------------------------- Affector --------------------------------------
        /// <summary>
        /// Create a ParticleAffector and add it to this ParticleTechnique.
        /// </summary>
        /// <param name="affectorType"></param>
        /// <returns></returns>
        public ParticleAffector CreateAffector(String affectorType)
        {
            ParticleAffector a = ParticleAffector.GetParticleAffectorByType(ParticleTechnique_CreateAffector(nativePtr, affectorType));
            if (a != null)
                affectors.Add(a);
            return a;
        }

        /// <summary>
        /// Add a ParticleAffector to this ParticleTechnique.
        /// </summary>
        /// <param name="affector">Pointer to a previously created affector.</param>
        public void AddAffector(ParticleAffector affector)
        {
            if (affector != null)
            {
                ParticleTechnique_AddAffector(nativePtr, affector.nativePtr);
                affectors.Add(affector);
            }
        }

        /// <summary>
        /// Remove a ParticleAffector from the ParticleTechnique, but don´t delete it.
        /// </summary>
        /// <param name="affector">Pointer to a ParticleAffector object.</param>
        public void RemoveAffector(ParticleAffector affector)
        {
            if (affector != null)
            {
                ParticleTechnique_RemoveAffector(nativePtr, affector.nativePtr);
                affectors.Remove(affector);
            }
        }

        /// <summary>
        ///  Get a ParticleAffector. 
        /// </summary>
        /// <param name="index">Search by index.</param>
        /// <returns></returns>
        public ParticleAffector GetAffector(uint index)
        {
            ParticleAffector a = ParticleAffector.GetParticleAffectorByType(ParticleTechnique_GetAffector(nativePtr, index));
            if (index > affectors.Count && a != null)
                affectors.Add(a);
            else if (index > affectors.Count && a == null)
                return null;
            affectors[(int)index] = (a);
            return a;
        }

        /// <summary>
        /// Get a ParticleAffector. 
        /// </summary>
        /// <param name="affectorName">Search by name.</param>
        /// <returns></returns>
        public ParticleAffector GetAffector(String affectorName)
        {
            ParticleAffector a = ParticleAffector.GetParticleAffectorByType(ParticleTechnique_GetAffector(nativePtr, affectorName));
            if (a != null && !affectors.Contains(a))
                affectors.Add(a);
            return a;
        }

        /// <summary>
        /// Get the number of ParticleAffectors added to this ParticleTechnique.
        /// </summary>
        /// <returns></returns>
        public uint GetNumAffectors()
        {
            return ParticleTechnique_GetNumAffectors(nativePtr);
        }

        /// <summary>
        /// Delete a ParticleAffector that is part of this ParticleTechnique. 
        /// </summary>
        /// <param name="index">Search by index.</param>
        public void DestroyAffector(uint index)
        {
            ParticleTechnique_DestroyAffector(nativePtr, index);
            affectors.RemoveAt((int)index);
        }

        /// <summary>
        /// Delete a ParticleAffector that is part of this ParticleTechnique.
        /// </summary>
        /// <param name="affector"></param>
        public void DestroyAffector(ParticleAffector affector)
        {
            if (affector != null)
            {
                ParticleTechnique_DestroyAffector(nativePtr, affector.nativePtr);
                affectors.Remove(affector);
            }
        }

        /// <summary>
        /// Delete all ParticleAffectors of this ParticleTechnique.
        /// </summary>
        public void DestroyAllAffectors()
        {
            ParticleTechnique_DestroyAllAffectors(nativePtr);
            affectors.Clear();
        }

        /// <summary>
        /// Get the number of emitted ParticleAffectors.
        ///<remarks>
        ///	This doesn't return the real number of emitted affectors, but the number of affectors that are
        ///	marked for emission.</remarks>
        /// </summary>
        /// <returns></returns>
        public uint GetNumEmittedAffectors()
        {
            return ParticleTechnique_GetNumEmittedAffectors(nativePtr);
        }

        #endregion
        #region -------------------------------------- Observer --------------------------------------
        /// <summary>
        /// Create a ParticleObserver and add it to this ParticleTechnique.
        /// </summary>
        /// <param name="observerType"></param>
        /// <returns></returns>
        public ParticleObserver CreateObserver(String observerType)
        {
            IntPtr ptr = ParticleTechnique_CreateObserver(nativePtr, observerType);
            ParticleObserver o = ParticleObserver.GetObserverByPtr(ptr, observerType);
            if (o != null)
                observers.Add(o);
            return o;
        }

        /// <summary>
        /// Add a ParticleObserver to this ParticleTechnique.				
        /// </summary>
        /// <param name="observer">Pointer to a previously created observer.</param>
        public void AddObserver(ParticleObserver observer)
        {
            if (observer != null)
            {
                ParticleTechnique_AddObserver(nativePtr, observer.nativePtr);
                observers.Add(observer);
            }
        }

        /// <summary>
        /// Remove a ParticleObserver from the ParticleTechnique, but don´t delete it.
        /// </summary>
        /// <param name="observer">Pointer to a ParticleObserver object.</param>
        public void RemoveObserver(ParticleObserver observer)
        {
            if (observer != null)
            {
                ParticleTechnique_RemoveObserver(nativePtr, observer.nativePtr);
                observers.Remove(observer);
            }
        }

        /// <summary>
        /// Get a ParticleObserver. 
        /// </summary>
        /// <param name="index">Search by index.</param>
        /// <returns></returns>
        public ParticleObserver GetObserver(uint index)
        {
            ParticleObserver o = ParticleObserver.GetObserverByPtr(ParticleTechnique_GetObserver(nativePtr, index));
            if (o != null)
                observers[(int)index] = o;
            return o;
        }

        /// <summary>
        /// Get a ParticleObserver. 
        /// </summary>
        /// <param name="observerName">Search by name.</param>
        /// <returns></returns>
        public ParticleObserver GetObserver(String observerName)
        {
            ParticleObserver o = ParticleObserver.GetObserverByPtr(ParticleTechnique_GetObserver(nativePtr, observerName));
            if (!observers.Contains(o))
                observers.Add(o);
            return o;
        }

        /// <summary>
        /// Get the number of ParticleObservers added to this ParticleTechnique.
        /// </summary>
        /// <returns></returns>
        public uint GetNumObservers()
        {
            return ParticleTechnique_GetNumObservers(nativePtr);
        }

        /// <summary>
        /// Delete a ParticleObserver that is part of this ParticleTechnique.
        /// </summary>
        /// <param name="index"> Search by index.</param>
        public void DestroyObserver(uint index)
        {
            ParticleTechnique_DestroyObserver(nativePtr, index);
            observers.RemoveAt((int)index);
        }

        /// <summary>
        /// Delete a ParticleObserver that is part of this ParticleTechnique.
        /// </summary>
        /// <param name="observer"></param>
        public void DestroyObserver(ParticleObserver observer)
        {
            if (observer != null)
            {
                ParticleTechnique_DestroyObserver(nativePtr, observer.nativePtr);
                observers.Remove(observer);
            }
        }

        /// <summary>
        /// Delete all ParticleObservers of this ParticleTechnique.
        /// </summary>
        public void DestroyAllObservers()
        {
            ParticleTechnique_DestroyAllObservers(nativePtr);
            observers.Clear();
        }
        #endregion
        #region -------------------------------------- Renderer --------------------------------------
        /// <summary>
        ///  Returns the pointer to the renderer.
        /// </summary>
        /// <returns></returns>
        public ParticleRenderer GetRenderer()
        {
            renderer = ParticleRendererHelper.GetParticleRenderer(ParticleTechnique_GetRenderer(nativePtr));
            return renderer;
        }

        /// <summary>
        /// Set a renderer by means of the type of renderer.
        /// </summary>
        /// <param name="rendererType"></param>
        public void SetRenderer(String rendererType)
        {
            ParticleTechnique_SetRenderer(nativePtr, rendererType);
            GetRenderer();
        }

        /// <summary>
        /// Remove a renderer; this doesn't detroy it.
        /// </summary>
        /// <param name="renderer"></param>
        public void RemoveRenderer(ParticleRenderer newRenderer)
        {
            if (newRenderer != null)
            {
                ParticleTechnique_RemoveRenderer(nativePtr, newRenderer.nativePtr);
                renderer = null;
            }
        }

        /// <summary>
        /// Set a renderer.
        /// </summary>
        /// <param name="renderer"></param>
        public void SetRenderer(ParticleRenderer newRenderer)
        {
            if (newRenderer != null)
            {
                ParticleTechnique_SetRenderer(nativePtr, newRenderer.nativePtr);
                renderer = newRenderer;
            }
        }

        /// <summary>
        /// Delete the renderer of this ParticleTechnique.
        /// </summary>
        public void DestroyRenderer()
        {
            ParticleTechnique_DestroyRenderer(nativePtr);
            renderer = null;
        }
        #endregion
        #region -------------------------------------- Behaviour --------------------------------------
        /// <summary>
        /// Add a ParticleBehaviour template to this ParticleTechnique.
        ///<remarks>
        ///	The ParticleBehaviour only serves as a blueprint for other ParticleBehaviour objects that
        ///	are attached to a Particle, so the method should only be used internally.</remarks>
        /// </summary>
        /// <param name="behaviourTemplate">Pointer to a previously created Behaviour object.</param>
        public void _addBehaviourTemplate(ParticleBehaviour behaviourTemplate)
        {
            if (behaviourTemplate != null)
            {
                ParticleTechnique__addBehaviourTemplate(nativePtr, behaviourTemplate.nativePtr);
                behaviours.Add(behaviourTemplate);
            }
        }

        /// <summary>
        /// Remove a ParticleBehaviour template.
        /// </summary>
        /// <param name="behaviourTemplate"></param>
        public void _removeBehaviourTemplate(ParticleBehaviour behaviourTemplate)
        {
            if (behaviourTemplate != null)
            {
                ParticleTechnique__removeBehaviourTemplate(nativePtr, behaviourTemplate.nativePtr);
                behaviours.Remove(behaviourTemplate);
            }
        }

        /// <summary>
        ///  Get a ParticleBehaviour template.
        /// </summary>
        /// <param name="index"> Search by index.</param>
        /// <returns></returns>
        public ParticleBehaviour _getBehaviourTemplate(uint index)
        {
            ParticleBehaviour b = ParticleBehaviourHelper.GetParticleBehaviour(ParticleTechnique__getBehaviourTemplate(nativePtr, index));
            if (b != null)
                behaviours[(int)index] = b;
            return b;
        }

        /// <summary>
        /// Get a ParticleBehaviour template. 
        /// </summary>
        /// <param name="behaviourType">Search by type.</param>
        /// <returns></returns>
        public ParticleBehaviour _getBehaviourTemplate(String behaviourType)
        {
            ParticleBehaviour b = ParticleBehaviourHelper.GetParticleBehaviour(ParticleTechnique__getBehaviourTemplate(nativePtr, behaviourType));
            if (!behaviours.Contains(b))
                behaviours.Add(b);
            return b;
        }

        /// <summary>
        /// Get the number of ParticleBehaviour templates added to this ParticleTechnique.
        /// </summary>
        /// <returns></returns>
        public uint _getNumBehaviourTemplates()
        {
            return ParticleTechnique__getNumBehaviourTemplates(nativePtr);
        }

        /// <summary>
        ///  Delete a ParticleBehaviour template that is part of this ParticleTechnique.
        /// </summary>
        /// <param name="behaviourTemplate"></param>
        public void _destroyBehaviourTemplate(ParticleBehaviour behaviourTemplate)
        {
            if (behaviourTemplate != null)
            {
                ParticleTechnique__destroyBehaviourTemplate(nativePtr, behaviourTemplate.nativePtr);
                behaviours.Remove(behaviourTemplate);
            }
        }

        /// <summary>
        /// Delete all ParticleBehaviour templates of this ParticleTechnique.
        /// </summary>
        public void _destroyAllBehaviourTemplates()
        {
            ParticleTechnique__destroyAllBehaviourTemplates(nativePtr);
            behaviours.Clear();
        }
        #endregion
        #region -------------------------------------- Extern --------------------------------------
        /// <summary>
        /// Create an Extern and add it to this ParticleTechnique.
        /// 
        /// </summary>
        /// <param name="externType"></param>
        /// <returns></returns>
        public Extern CreateExtern(String externType)
        {
            Extern e = ExternFactory.GetExternByType(externType);
            if (e != null)
                externs.Add(e);
            return e;
        }

        /// <summary>
        /// Add an Extern to this ParticleTechnique.
        /// </summary>
        /// <param name="externObject">Pointer to a previously created Extern object.</param>
        public void AddExtern(Extern externObject)
        {
            if (externObject != null)
            {
                ParticleTechnique_AddExtern(nativePtr, externObject.NativePointer);
                externs.Add(externObject);
            }
        }

        /// <summary>
        /// Remove an Extern from the ParticleTechnique, but don´t delete it.
        /// </summary>
        /// <param name="externObject">Pointer to an Extern object.</param>
        public void RemoveExtern(Extern externObject)
        {
            if (externObject != null)
            {
                ParticleTechnique_RemoveExtern(nativePtr, externObject.NativePointer);
                externs.Remove(externObject);
            }
        }

        /// <summary>
        /// Get an Extern. 
        /// </summary>
        /// <param name="index">Search by index.</param>
        /// <returns></returns>
        public Extern GetExtern(uint index)
        {
            Extern e = ExternFactory.GetExternByPointer(ParticleTechnique_GetExtern(nativePtr, index));
            if (e != null)
                externs[(int)index] = e;
            return e;
        }

        /// <summary>
        /// Get an Extern. 
        /// </summary>
        /// <param name="externName">Search by name.</param>
        /// <returns></returns>
        public Extern GetExtern(String externName)
        {
            Extern e = ExternFactory.GetExternByPointer(ParticleTechnique_GetExtern(nativePtr, externName));
            if (e != null)
            {
                int index = externs.IndexOf(e);
                externs[(int)index] = e;
            }
            return e;
        }

        /// <summary>
        /// Get an Extern. 
        /// </summary>
        /// <param name="externType">Search by type.</param>
        /// <returns></returns>
        public Extern GetExternType(String externType)
        {
            Extern e = ExternFactory.GetExternByPointer(ParticleTechnique_GetExtern(nativePtr, externType));
            if (e != null)
            {
                int index = externs.IndexOf(e);
                externs[(int)index] = e;
            }
            return e;
        }

        /// <summary>
        /// Get the number of Externs added to this ParticleTechnique.
        /// </summary>
        /// <returns></returns>
        public uint GetNumExterns()
        {
            return ParticleTechnique_GetNumExterns(nativePtr);
        }

        /// <summary>
        /// Delete an Extern that is part of this ParticleTechnique. Search by index.
        /// </summary>
        /// <param name="index"></param>
        public void DestroyExtern(uint index)
        {
            ParticleTechnique_DestroyExtern(nativePtr, index);
            externs.RemoveAt((int)index);
        }

        /// <summary>
        ///  Delete an Extern that is part of this ParticleTechnique.
        /// </summary>
        /// <param name="externObject"></param>
        public void DestroyExtern(Extern externObject)
        {
            if (externObject != null)
            {
                ParticleTechnique_DestroyExtern(nativePtr, externObject.NativePointer);
                externs.Remove(externObject);
            }
        }

        /// <summary>
        /// Delete all Externs of this ParticleTechnique.
        /// </summary>
        public void DestroyAllExterns()
        {
            ParticleTechnique_DestroyAllExterns(nativePtr);
            externs.Clear();
        }
        #endregion
        #region -------------------------------------- Rest --------------------------------------
        /// <summary>
        /// Update the renderqueue.
        ///<remarks>
        ///	This function invokes the renderer and updates the renderqueue of that renderer. This is not
        ///	only performed for this ParticleTechnique, but also for the pooled ParticleTechniques if
        ///	available. Updating the renderqueue causes the particles to be actually rendered.</remarks>
        /// </summary>
        /// <param name="queue"></param>
        public void _updateRenderQueue(Mogre.RenderQueue queue)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            ParticleTechnique__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr);
        }

        /// <summary>
        /// Set the renderqueue group in the renderer
        /// </summary>
        /// <param name="queueId"></param>
        public void SetRenderQueueGroup(byte queueId)
        {
            ParticleTechnique_SetRenderQueueGroup(nativePtr, queueId);
        }

        /// <summary>
        /// Perform some initialisation activities.
        ///<remarks>
        ///	To reduce initialisation activities as soon as the particle system is started, these activities 
        ///	can also be performed in front.</remarks>
        /// </summary>
        public void _prepare()
        {
            ParticleTechnique__prepare(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of system elements.
        /// </summary>
        public void _prepareSystem()
        {
            ParticleTechnique__prepareSystem(nativePtr);
        }

        /// <summary>
        /// Perform uninitialisation activities of system elements.
        /// </summary>
        public void _unprepareSystem()
        {
            ParticleTechnique__unprepareSystem(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of the technique itself.
        /// </summary>
        public void _prepareTechnique()
        {
            ParticleTechnique__prepareTechnique(nativePtr);
        }
        /// <summary>
        /// Perform uninitialisation activities of the technique itself.
        /// </summary>

        public void _unprepareTechnique()
        {
            ParticleTechnique__unprepareTechnique(nativePtr);
        }

        /// <summary>
        /// initialise visual particles if needed.
        /// </summary>
        public void _prepareVisualParticles()
        {
            ParticleTechnique__prepareVisualParticles(nativePtr);
        }

        /// <summary>
        /// Uninitialise visual particles if needed.
        /// </summary>
        public void _unprepareVisualParticles()
        {
            ParticleTechnique__unprepareVisualParticles(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of the renderer.
        /// </summary>
        public void _prepareRenderer()
        {
            ParticleTechnique__prepareRenderer(nativePtr);
        }

        /// <summary>
        /// Perform uninitialisation activities of the renderer.
        /// </summary>
        public void _unprepareRenderer()
        {
            ParticleTechnique__unprepareRenderer(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of emitters.
        /// </summary>
        public void _prepareEmitters()
        {
            ParticleTechnique__prepareEmitters(nativePtr);
        }

        /// <summary>
        /// Perform uninitialisation activities of emitters.
        /// </summary>
        public void _unprepareEmitters()
        {
            ParticleTechnique__unprepareEmitters(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of affectors.
        /// </summary>
        public void _prepareAffectors()
        {
            ParticleTechnique__prepareAffectors(nativePtr);
        }

        /// <summary>
        /// Perform uninitialisation activities of affectors.
        /// </summary>
        public void _unprepareAffectors()
        {
            ParticleTechnique__unprepareAffectors(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of behaviours.
        /// </summary>
        public void _prepareBehaviours()
        {
            ParticleTechnique__prepareBehaviours(nativePtr);
        }

        /// <summary>
        /// Perform uninitialisation activities of behaviours.
        /// </summary>
        public void _unprepareBehaviours()
        {
            ParticleTechnique__unprepareBehaviours(nativePtr);
        }

        /// <summary>
        /// Perform initialisation activities of externs.
        /// </summary>
        public void _prepareExterns()
        {
            ParticleTechnique__prepareExterns(nativePtr);
        }

        /// <summary>
        /// Perform uninitialisation activities of externs.
        /// </summary>
        public void _unprepareExterns()
        {
            ParticleTechnique__unprepareExterns(nativePtr);
        }

        /// <summary>
        /// Update this ParticleTechnique.
        /// <remarks>
        /// Updating the ParticleTechnique actually sets all particles in motion. The ParticleTechnique is
        ///		only updated if the ParticleSystem to which the ParticleTechnique belongs is started.
        /// </remarks>
        /// </summary>
        /// <param name="timeElapsed"></param>
        public void _update(float timeElapsed)
        {
            ParticleTechnique__update(nativePtr, timeElapsed);
        }

        /// <summary>
        /// Is called as soon as a new emitter is added or deleted, which leads to a re-evaluation of the 
        ///		emitted objects.
        ///	<remarks>
        ///		Emitters are able to emit other objects (emitters, techniques, affectors) besides visual 
        ///		particles, and removing or adding an emitter could lead to a broken chain of references. This 
        ///		means that 1). Emitters that were emitted by a deleted emitter, aren't emitted anymore. 2).
        ///		An added emitter could emit another emitter (or affector); the other emitter has to know that 
        ///		it will be emitted. 3). Another emitter could emit the emitter that is added; the added emitter 
        ///		has to know that. 4). If an already existing emitter sets its emitsName the chain is broken.
        ///	</remarks>
        ///	
        /// <para>
        ///		This method runs through the whole chain of emitters each time a new emitter is 
        ///		added or an emitter is deleted. This has a performance penalty, but since the number of emitters
        ///		is usually not very large we can get away with it.</para>
        ///	<para>
        ///		If an emitter is deleted and this emitter is also emitted itself, the effect of deletion is not always
        ///		instantly noticable. Emitted emitters are part of the particle pool and are NOT deleted if the base
        ///		emitter (from which the pooled emitters are cloned) is deleted.</para>
        /// </summary>
        public void _notifyEmissionChange()
        {
            ParticleTechnique__notifyEmissionChange(nativePtr);
        }

        /// <summary>
        /// Implementation of the _notifyAttached, needed for each technique that is part of a 
        ///		particle system.
        /// <remarks>Delegates to the renderer.</remarks>
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="isTagPoint"></param>
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            ParticleTechnique__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /// <summary>
        /// Notify the pooled techniques that its parent system has been attached or detached.
        /// <remarks>This is done for emitted ParticleTechniques.</remarks>
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="isTagPoint"></param>
        public void _notifyAttachedPooledTechniques(Mogre.Node parent, bool isTagPoint)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            ParticleTechnique__notifyAttachedPooledTechniques(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /// <summary>
        /// Implementation of the _notifyCurrentCamera, needed for each technique that is part of a 
        ///		particle system.
        ///		
        /// <remarks>
        /// Delegates to the renderer.
        /// </remarks>
        /// </summary>
        /// <param name="camera"></param>
        public void _notifyCurrentCamera(Mogre.Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera cannot be null!");
            ParticleTechnique__notifyCurrentCamera(nativePtr, (IntPtr)camera.NativePtr);
        }

        /// <summary>
        /// Notify the pooled techniques with the current camera.
        /// <remarks>
        /// This is done for emitted ParticleTechniques.
        /// </remarks>
        /// </summary>
        /// <param name="camera"></param>
        public void _notifyCurrentCameraPooledTechniques(Mogre.Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera cannot be null!");
            ParticleTechnique__notifyCurrentCameraPooledTechniques(nativePtr, (IntPtr)camera.NativePtr);
        }

        /// <summary>
        /// Implementation of the _notifyParticleResized, needed for each technique that is part of a 
        ///		particle system.
        ///		
        /// <remarks>
        /// Delegates to the renderer.
        /// </remarks>
        /// </summary>
        public void _notifyParticleResized()
        {
            ParticleTechnique__notifyParticleResized(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleTechnique is started.
        /// <remarks>
        /// This is only used to set some attributes to their default value, so a re-start can be performed.
        ///		Note, that one cannot assume that the _prepare() function has been called, so don´t perform 
        ///		initialisation activities on objects that are not created yet (for instance the renderer).
        /// </remarks>
        /// </summary>
        public void _notifyStart()
        {
            ParticleTechnique__notifyStart(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleTechnique is stopped.
        /// </summary>
        public void _notifyStop()
        {
            ParticleTechnique__notifyStop(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleTechnique is paused.
        /// </summary>
        public void _notifyPause()
        {
            ParticleTechnique__notifyPause(nativePtr);
        }

        /// <summary>
        /// Perform activities when a ParticleTechnique is resumed.
        /// </summary>
        public void _notifyResume()
        {
            ParticleTechnique__notifyResume(nativePtr);
        }

        /// <summary>
        /// Validate whether a particle is expired.
        /// </summary>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        /// <returns></returns>
        public bool _isExpired(Particle particle, float timeElapsed)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return ParticleTechnique__isExpired(nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Forces emission of particles.
        /// <remarks>
        /// The number of requested particles are the exact number that are emitted. No down-scalling is applied.
        /// </remarks>
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="requested"></param>
        public void ForceEmission(ParticleEmitter emitter, uint requested)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            ParticleTechnique_ForceEmission(nativePtr, emitter.nativePtr, requested);
        }

        /// <summary>
        /// Emits particles of the first emitter it encounters in this technique.
        /// </summary>
        /// <param name="particleType"></param>
        /// <param name="requested"></param>
        public void ForceEmission(ParticleType particleType, uint requested)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            ParticleTechnique_ForceEmission(nativePtr, particleType, requested);
        }

        /// <summary>
        /// Copy the attributes of this ParticleTechnique to another ParticleTechnique.
        /// </summary>
        /// <param name="technique"></param>
        public void CopyAttributesTo(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleTechnique_CopyAttributesTo(nativePtr, technique.nativePtr);
        }

        /// <summary>
        /// Get the Lod index.
        /// <remarks>
        /// The Lod index determines at which distance this ParticleTechnique is active. This has only effect
        ///		if the Lod distances of the ParticleSystem to which this ParticleTechnique belongs have been defined.
        /// </remarks>
        /// </summary>
        public ushort LodIndex
        {
            get { return ParticleTechnique_GetLodIndex(nativePtr); }
            set { ParticleTechnique_SetLodIndex(nativePtr, value); }
        }

        /// <summary>
        /// Determine which techniques, affectors, emitters will be emitted.
        /// <remarks>
        /// All objects that are able to be emitted will get an indication when they are emitted. This function
        ///		runs through all ParticleEmitters and if the ParticleEmitter emits objects other than visual particles,
        ///		these objects are marked.
        /// </remarks>
        /// </summary>
        public void _markForEmission()
        {
            ParticleTechnique__markForEmission(nativePtr);
        }

        /// <summary>
        /// Determines which techniques, affectors, emitters will be emitted (or not) by the given emitter.
        /// </summary>
        /// <param name="emitter"></param>
        /// <param name="mark"></param>
        public void _markForEmission(ParticleEmitter emitter, bool mark = true)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            ParticleTechnique__markForEmission(nativePtr, emitter.nativePtr, mark);
        }

        /// <summary>
        /// Notify updating the axis aligned bounding box.
        /// <remarks>The Particle System calls this function to make the ParticleTechnique calculating its mWorldAABB.</remarks>
        /// </summary>
        public void _notifyUpdateBounds()
        {
            ParticleTechnique__notifyUpdateBounds(nativePtr);
        }

        /// <summary>
        /// Reset the bounds.
        /// </summary>
        public void _resetBounds()
        {
            ParticleTechnique__resetBounds(nativePtr);
        }

        /// <summary>
        /// Notify that the Particle System is rescaled.
        /// </summary>
        /// <param name="scale"></param>
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            ParticleTechnique__notifyRescaled(nativePtr, scale);
        }

        /// <summary>
        /// Notify that the velocity is rescaled.
        /// </summary>
        /// <param name="scaleVelocity"></param>
        public void _notifyVelocityRescaled(float scaleVelocity)
        {
            ParticleTechnique__notifyVelocityRescaled(nativePtr, scaleVelocity);
        }

        /// <summary>
        /// Returns the world aabb.
        /// </summary>
        public Mogre.AxisAlignedBox WorldBoundingBox
        {
            get
            {
                IntPtr scalePtr = ParticleTechnique_GetWorldBoundingBox(nativePtr);
                if (scalePtr == IntPtr.Zero)
                    return null;
                Mogre.AxisAlignedBox aabtype = new Mogre.AxisAlignedBox();
                Mogre.AxisAlignedBox vec = (Mogre.AxisAlignedBox)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
                worldBoundingBox = vec;
                return vec;
            }
        }

        /// <summary>
        /// Sort the visual particles.
        /// <remarks>
        /// Only the visual particles are sorted, because sorting non-visual particles doesn't make sense.
        /// </remarks>
        /// </summary>
        /// <param name="camera"></param>
        public void _sortVisualParticles(Mogre.Camera camera)
        {
            if (camera == null)
                throw new ArgumentNullException("camera cannot be null!");
            ParticleTechnique__sortVisualParticles(nativePtr, (IntPtr)camera.NativePtr);
        }

        public void SetWidthCameraDependency(float squareDistance, bool inc)
        {
            ParticleTechnique_SetWidthCameraDependency(nativePtr, squareDistance, inc);
        }

        public CameraDependency WidthCameraDependency
        {
            get {
                wCameraDependency = CameraDependency.GetInstance(ParticleTechnique_GetWidthCameraDependency(nativePtr));
                return wCameraDependency; 
            }
            set
            {
                if (value != null)
                {
                    ParticleTechnique_SetWidthCameraDependency(nativePtr, value.NativePointer);
                    wCameraDependency = value;
                }
                else
                {
                    ParticleTechnique_SetWidthCameraDependency(nativePtr, IntPtr.Zero);
                    wCameraDependency = value;
                }
            }
        }

        public void SetHeightCameraDependency(float squareDistance, bool inc)
        {
            ParticleTechnique_SetHeightCameraDependency(nativePtr, squareDistance, inc);
        }

        public CameraDependency HeightCameraDependency
        {
            get
            {
                hCameraDependency = CameraDependency.GetInstance(ParticleTechnique_GetHeightCameraDependency(nativePtr));
                return hCameraDependency;
            }
            set
            {
                if (value != null)
                {
                    ParticleTechnique_SetHeightCameraDependency(nativePtr, value.NativePointer);
                    hCameraDependency = value;
                }
                else
                {
                    ParticleTechnique_SetHeightCameraDependency(nativePtr, IntPtr.Zero);
                    hCameraDependency = value;
                }
            }
        }

        public void SetDepthCameraDependency(float squareDistance, bool inc)
        {
            ParticleTechnique_SetDepthCameraDependency(nativePtr, squareDistance, inc);
        }

        public CameraDependency DepthCameraDependency
        {
            get
            {
                dCameraDependency = CameraDependency.GetInstance(ParticleTechnique_GetDepthCameraDependency(nativePtr));
                return dCameraDependency;
            }
            set
            {
                if (value != null)
                {
                    ParticleTechnique_SetDepthCameraDependency(nativePtr, value.NativePointer);
                    dCameraDependency = value;
                }
                else
                {
                    ParticleTechnique_SetDepthCameraDependency(nativePtr, IntPtr.Zero);
                    dCameraDependency = value;
                }
            }
        }

        public uint GetNumberOfEmittedParticles()
        {
            return ParticleTechnique_GetNumberOfEmittedParticles(nativePtr);
        }

        public uint GetNumberOfEmittedParticles(ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return ParticleTechnique_GetNumberOfEmittedParticles(nativePtr, particleType);
        }

        /// <summary>
        /// Expire all active (emitted) particles and put them back into the pool.
        /// </summary>
        public void _initAllParticlesForExpiration()
        {
            ParticleTechnique__initAllParticlesForExpiration(nativePtr);
        }

        /// <summary>
        ///  Put all emitted particles back into the pool.
        /// </summary>
        public void LockAllParticles()
        {
            ParticleTechnique_LockAllParticles(nativePtr);
        }

        /// <summary>
        /// Reset the visual data in the pool.
        /// <remarks>
        /// Visual particles may keep some additional visual data that needs to be reset in some cases.
        ///		This function puts all particles back into the pool.
        /// </remarks>
        /// </summary>
        public void InitVisualDataInPool()
        {
            ParticleTechnique_InitVisualDataInPool(nativePtr);
        }

        /// <summary>
        /// Returns a pointer to the particle pool.
        /// <remarks>Caution: Only use this function if you know what you are doing.  If this function is called from a function that
        ///		is already iterating over the pool, unexpected behaviour can occur.</remarks>
        /// </summary>
        /// <returns></returns>
        public ParticlePool _getParticlePool()
        {
            return ParticlePool.GetInstance(ParticleTechnique__getParticlePool(nativePtr));
        }

        /// <summary>
        /// If this attribute is set to 'true', the particles are emitted relative to the technique 
        /// </summary>
        public bool KeepLocal
        {
            get { return ParticleTechnique_IsKeepLocal(nativePtr); }
            set { ParticleTechnique_SetKeepLocal(nativePtr, value); }
        }

        /// <summary>
        /// Transforms the particle position in a local position relative to the technique
        /// </summary>
        /// <param name="particle"></param>
        /// <returns></returns>
        public bool MakeParticleLocal(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            return ParticleTechnique_MakeParticleLocal(nativePtr, particle.NativePointer);
        }

        ///* Returns the Spatial Hashtable.
        //*/
        //SpatialHashTable<Particle> SpatialHashTable
        //{
        //    get { ;}
        //}

        /// <summary>
        /// Returns true if spatial hashing is used.
        /// Defines whether spatial hashing is used.
        /// </summary>
        public bool SpatialHashingUsed
        {
            get { return false; }
            //set { ;}
        }

        /// <summary>
        /// Returns the celsize used in spatial hashing.
        /// Set the celsize used in spatial hashing. A cel represents a small part of the 3d space in which 
        ///		particles may exist. The size of the cel is the same for both x, y and z dimension.
        /// </summary>
        public ushort SpatialHashingCellDimension
        {
            get { return ParticleTechnique_GetSpatialHashingCellDimension(nativePtr); }
            set { ParticleTechnique_SetSpatialHashingCellDimension(nativePtr, value); }
        }

        /// <summary>
        /// Set the size of the overlap.
        /// <remarks>The cell overlap is used to put a particle in multiple cells if needed. This is a better way to 
        ///	determine nearby particles. A particle in one cell is not considered nearby in relation to a 
        ///	particle in another cel, although they can be nearby in terms of their world position. If you want to
        ///	inspect all particles positioned in a radius around a central particle, and that particle is close
        /// to the border of a cel, you miss a few particles in the neighbouring cell that are nearby. By 
        ///	defining an overlap you duplicate particles in multiple cells, but you are able to determine all 
        ///	nearby particles.</remarks>
        ///	Because particle are duplicated in multiple cells, you have to consider that validations between
        ///	2 particles in one cell are also performed in another cell. Set a flag to a particle, indicating that
        ///	it has been validated.
        /// </summary>
        public ushort SpatialHashingCellOverlap
        {
            get { return ParticleTechnique_GetSpatialHashingCellOverlap(nativePtr); }
            set { ParticleTechnique_SetSpatialHashingCellOverlap(nativePtr, value); }
        }

        /// <summary>
        /// Returns the size of the hashtable used in spatial hashing.
        /// Sets the size of the hashtable used in spatial hashing.
        /// </summary>
        public uint SpatialHashTableSize
        {
            get { return ParticleTechnique_GetSpatialHashTableSize(nativePtr); }
            set { ParticleTechnique_SetSpatialHashTableSize(nativePtr, value); }
        }

        /// <summary>
        /// Return the interval when the spatial hashtable is updated.
        ///
        /// Set the interval when the spatial hashtable is updated.
        /// </summary>
        public float SpatialHashingInterval
        {
            get { return ParticleTechnique_GetSpatialHashingInterval(nativePtr); }
            set { ParticleTechnique_SetSpatialHashingInterval(nativePtr, value); }
        }

        /// <summary>
        /// Return the indication whether to use only the particle position (false) or take the particle size
        ///	    into account (true).
        /// Set the indication whether to use only the particle position (false) or take the particle size
        ///	    into account (true).
        /// </summary>
        public bool SpatialHashingParticleSizeUsed
        {
            get { return ParticleTechnique_IsSpatialHashingParticleSizeUsed(nativePtr); }
            set { ParticleTechnique_SetSpatialHashingParticleSizeUsed(nativePtr, value); }
        }

        /// <summary>
        /// Return the maximum velocity a particle can have, even if the velocity of the particle has been set higher (either by 
        ///    initialisation of the particle or by means of an affector).
        /// Set the maximum velocity a particle can have.
        /// </summary>
        public float MaxVelocity
        {
            get { return ParticleTechnique_GetMaxVelocity(nativePtr); }
            set { ParticleTechnique_SetMaxVelocity(nativePtr, value); }
        }

        /// <summary>
        /// Add a TechniqueListener, which gets called in case a particle is emitted or expired.
        /// </summary>
        /// <param name="techniqueListener"></param>
        public void AddTechniqueListener(TechniqueListener techniqueListener)
        {
            if (techniqueListener != null)
            {
                ParticleTechnique_AddTechniqueListener(nativePtr, techniqueListener.NativePointer);
                if (!listeners.Contains(techniqueListener))
                    listeners.Add(techniqueListener);
            }
        }

        /// <summary>
        /// Removes the TechniqueListener, but it isn't destroyed.
        /// </summary>
        /// <param name="techniqueListener"></param>
        public void RemoveTechniqueListener(TechniqueListener techniqueListener)
        {
            if (techniqueListener != null)
            {
                ParticleTechnique_RemoveTechniqueListener(nativePtr, techniqueListener.NativePointer);
                listeners.Remove(techniqueListener);
            }
        }

        /// <summary>
        /// Generates debug information, such as max. used particles in a technique.
        /// </summary>
        public void LogDebug()
        {
            ParticleTechnique_LogDebug(nativePtr);
        }

        /// <summary>
        /// Returns the velocity scale, defined in the particle system, but passed to the technique for convenience.
        /// </summary>
        public float ParticleSystemScaleVelocity
        {
            get { return ParticleTechnique_GetParticleSystemScaleVelocity(nativePtr); }

        }

        /// <summary>
        /// Forwards an event to the parent particle system.
        /// </summary>
        /// <param name="particleUniverseEvent"></param>
        public void PushEvent(ParticleUniverseEvent particleUniverseEvent)
        {
            if (particleUniverseEvent == null)
                throw new ArgumentNullException("particleUniverseEvent cannot be null!");
            ParticleTechnique_PushEvent(nativePtr, particleUniverseEvent.NativePointer);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetParentSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetParentSystem(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetParentSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetParentSystem(IntPtr ptr, IntPtr parentSystem);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetVisualParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetVisualParticleQuota(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetVisualParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetVisualParticleQuota(IntPtr ptr, uint quota);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetEmittedEmitterQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetEmittedEmitterQuota(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetEmittedEmitterQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetEmittedEmitterQuota(IntPtr ptr, uint quota);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetEmittedTechniqueQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetEmittedTechniqueQuota(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetEmittedTechniqueQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetEmittedTechniqueQuota(IntPtr ptr, uint quota);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetEmittedAffectorQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetEmittedAffectorQuota(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetEmittedAffectorQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetEmittedAffectorQuota(IntPtr ptr, uint quota);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetEmittedSystemQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetEmittedSystemQuota(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetEmittedSystemQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetEmittedSystemQuota(IntPtr ptr, uint quota);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetDefaultWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetDefaultWidth(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetDefaultWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetDefaultWidth(IntPtr ptr, float width);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetDefaultHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetDefaultHeight(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetDefaultHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetDefaultHeight(IntPtr ptr, float height);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetDefaultDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetDefaultDepth(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetDefaultDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetDefaultDepth(IntPtr ptr, float depth);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetDerivedPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetDerivedPosition(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetCameraSquareDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetCameraSquareDistance(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetCameraSquareDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetCameraSquareDistance(IntPtr ptr, float cameraSquareDistance);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SuppressNotifyEmissionChange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SuppressNotifyEmissionChange(IntPtr ptr, bool suppress);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetMaterialName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetMaterial", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetMaterial(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_CreateEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_CreateEmitter(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitterType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_AddEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_AddEmitter(IntPtr ptr, IntPtr emitter);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_RemoveEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_RemoveEmitter(IntPtr ptr, IntPtr emitter);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetEmitter(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetEmitter2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetEmitter(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string emitterName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumEmitters", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumEmitters(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyEmitter(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyEmitter2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyEmitter(IntPtr ptr, IntPtr emitter);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyAllEmitters", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyAllEmitters(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumEmittedEmitters", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumEmittedEmitters(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_CreateAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_CreateAffector(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string affectorType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_AddAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_AddAffector(IntPtr ptr, IntPtr affector);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_RemoveAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_RemoveAffector(IntPtr ptr, IntPtr affector);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetAffector(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetAffector2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetAffector(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string affectorName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumAffectors", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumAffectors(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyAffector(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyAffector2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyAffector(IntPtr ptr, IntPtr affector);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyAllAffectors", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyAllAffectors(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumEmittedAffectors", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumEmittedAffectors(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_CreateObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_CreateObserver(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string observerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_AddObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_AddObserver(IntPtr ptr, IntPtr observer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_RemoveObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_RemoveObserver(IntPtr ptr, IntPtr observer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetObserver(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetObserver2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetObserver(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string observerName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumObservers", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumObservers(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyObserver(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyObserver2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyObserver(IntPtr ptr, IntPtr observer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyAllObservers", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyAllObservers(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetRenderer(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetRenderer(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string rendererType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_RemoveRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_RemoveRenderer(IntPtr ptr, IntPtr renderer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetRenderer2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetRenderer(IntPtr ptr, IntPtr renderer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyRenderer(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__addBehaviourTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__addBehaviourTemplate(IntPtr ptr, IntPtr behaviourTemplate);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__removeBehaviourTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__removeBehaviourTemplate(IntPtr ptr, IntPtr behaviourTemplate);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__getBehaviourTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique__getBehaviourTemplate(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__getBehaviourTemplate2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique__getBehaviourTemplate(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string behaviourType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__getNumBehaviourTemplates", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique__getNumBehaviourTemplates(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__destroyBehaviourTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__destroyBehaviourTemplate(IntPtr ptr, IntPtr behaviourTemplate);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__destroyAllBehaviourTemplates", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__destroyAllBehaviourTemplates(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_CreateExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_CreateExtern(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_AddExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_AddExtern(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_RemoveExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_RemoveExtern(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetExtern(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetExtern2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetExtern(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string externName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetExternType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetExternType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumExterns", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumExterns(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyExtern(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyExtern2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyExtern(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_DestroyAllExterns", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_DestroyAllExterns(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__updateRenderQueue(IntPtr ptr, IntPtr queue);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetRenderQueueGroup(IntPtr ptr, byte queueId);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepare(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareSystem(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareSystem(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareVisualParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareVisualParticles(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareVisualParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareVisualParticles(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareRenderer(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareRenderer(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareEmitters", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareEmitters(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareEmitters", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareEmitters(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareAffectors", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareAffectors(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareAffectors", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareAffectors(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareBehaviours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareBehaviours(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareBehaviours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareBehaviours(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__prepareExterns", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__prepareExterns(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__unprepareExterns", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__unprepareExterns(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__update", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__update(IntPtr ptr, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyEmissionChange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyEmissionChange(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyAttachedPooledTechniques", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyAttachedPooledTechniques(IntPtr ptr, IntPtr parent, bool isTagPoint);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyCurrentCamera(IntPtr ptr, IntPtr camera);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyCurrentCameraPooledTechniques", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyCurrentCameraPooledTechniques(IntPtr ptr, IntPtr camera);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyParticleResized(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyStart(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyStop(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyPause", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyPause(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyResume", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyResume(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__isExpired", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleTechnique__isExpired(IntPtr ptr, IntPtr particle, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_ForceEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_ForceEmission(IntPtr ptr, IntPtr emitter, uint requested);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_ForceEmission2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_ForceEmission(IntPtr ptr, ParticleType particleType, uint requested);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_CopyAttributesTo(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetLodIndex", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleTechnique_GetLodIndex(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetLodIndex", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetLodIndex(IntPtr ptr, ushort lodIndex);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__markForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__markForEmission(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__markForEmission2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__markForEmission(IntPtr ptr, IntPtr emitter, bool mark = true);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyUpdateBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyUpdateBounds(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__resetBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__resetBounds(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__notifyVelocityRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__notifyVelocityRescaled(IntPtr ptr, float scaleVelocity);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetWorldBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetWorldBoundingBox(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__sortVisualParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__sortVisualParticles(IntPtr ptr, IntPtr camera);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetWidthCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetWidthCameraDependency(IntPtr ptr, IntPtr cameraDependency);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetWidthCameraDependency2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetWidthCameraDependency(IntPtr ptr, float squareDistance, bool inc);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetWidthCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetWidthCameraDependency(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetHeightCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetHeightCameraDependency(IntPtr ptr, IntPtr cameraDependncy);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetHeightCameraDependency2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetHeightCameraDependency(IntPtr ptr, float squareDistance, bool inc);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetHeightCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetHeightCameraDependency(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetDepthCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetDepthCameraDependency(IntPtr ptr, IntPtr cameraDependency);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetDepthCameraDependency2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetDepthCameraDependency(IntPtr ptr, float squareDistance, bool inc);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetDepthCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetDepthCameraDependency(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumberOfEmittedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumberOfEmittedParticles(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetNumberOfEmittedParticles2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetNumberOfEmittedParticles(IntPtr ptr, ParticleType particleType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__initAllParticlesForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique__initAllParticlesForExpiration(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_LockAllParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_LockAllParticles(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_InitVisualDataInPool", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_InitVisualDataInPool(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique__getParticlePool", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique__getParticlePool(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_IsKeepLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleTechnique_IsKeepLocal(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetKeepLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetKeepLocal(IntPtr ptr, bool keepLocal);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_MakeParticleLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleTechnique_MakeParticleLocal(IntPtr ptr, IntPtr particle);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetSpatialHashTable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleTechnique_GetSpatialHashTable(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_IsSpatialHashingUsed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleTechnique_IsSpatialHashingUsed(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetSpatialHashingUsed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetSpatialHashingUsed(IntPtr ptr, bool spatialHashingUsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetSpatialHashingCellDimension", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleTechnique_GetSpatialHashingCellDimension(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetSpatialHashingCellDimension", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetSpatialHashingCellDimension(IntPtr ptr, ushort spatialHashingCellDimension);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetSpatialHashingCellOverlap", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort ParticleTechnique_GetSpatialHashingCellOverlap(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetSpatialHashingCellOverlap", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetSpatialHashingCellOverlap(IntPtr ptr, ushort spatialHashingCellOverlap);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetSpatialHashTableSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleTechnique_GetSpatialHashTableSize(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetSpatialHashTableSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetSpatialHashTableSize(IntPtr ptr, uint spatialHashTableSize);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetSpatialHashingInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetSpatialHashingInterval(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetSpatialHashingInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetSpatialHashingInterval(IntPtr ptr, float spatialHashingInterval);
        //
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_IsSpatialHashingParticleSizeUsed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleTechnique_IsSpatialHashingParticleSizeUsed(IntPtr ptr);
        //
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetSpatialHashingParticleSizeUsed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetSpatialHashingParticleSizeUsed(IntPtr ptr, bool spatialHashingParticleSizeUsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetMaxVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetMaxVelocity(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_SetMaxVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_SetMaxVelocity(IntPtr ptr, float maxVelocity);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_AddTechniqueListener", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_AddTechniqueListener(IntPtr ptr, IntPtr techniqueListener);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_RemoveTechniqueListener", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_RemoveTechniqueListener(IntPtr ptr, IntPtr techniqueListener);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_LogDebug", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_LogDebug(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_GetParticleSystemScaleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleTechnique_GetParticleSystemScaleVelocity(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleTechnique_PushEvent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleTechnique_PushEvent(IntPtr ptr, IntPtr particleUniverseEvent);


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
            ParticleTechnique_Destroy(nativePtr);
            particleTechniqueInstances.Remove(nativePtr);
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

    }
}
