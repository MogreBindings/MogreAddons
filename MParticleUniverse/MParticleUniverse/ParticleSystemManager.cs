using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using MParticleUniverse.ParticleRenderers;

namespace MParticleUniverse
{
    /// <summary>
    /// The ParticleSystemManager manages particle systems, particle system scripts (templates), etc. It is also 
    ///	responsible for actually creating techniques, emitters, observers, etc.
    ///	
    /// You'll Notice most functions catch all Exceptions (I Hate Exceptions and avoid them at all cost!)
    /// </summary>
    public unsafe class ParticleSystemManager : IDisposable
    {
        /// <summary>
        /// Used while Debugging
        /// </summary>
        private const bool printDebug = true;

        
        //protected SceneManager manager;
        //protected Camera camera;

        /// <summary>
        /// Contains the Pointer to the ParticleSystemManager in actual memory. (Where the DLL lives)
        /// </summary>
        protected IntPtr NativePointer;

        #region the Meat
        /// <summary>
        /// Constructor for Marticle System Manager.
        /// It is Recommented to Use Singleton()
        /// <see cref="Singleton"/>
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public ParticleSystemManager()
        {
            NativePointer = ParticleSystemManager_New();
            particleSystemManagerInstances.Add(NativePointer, this);
        }

        internal static Dictionary<IntPtr, ParticleSystemManager> particleSystemManagerInstances;

        internal static ParticleSystemManager GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;

            if (particleSystemManagerInstances == null)
                particleSystemManagerInstances = new Dictionary<IntPtr, ParticleSystemManager>();

            ParticleSystemManager newvalue;

            if (particleSystemManagerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ParticleSystemManager(ptr);
            particleSystemManagerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ParticleSystemManager(IntPtr pointer)
        {
            NativePointer = pointer;
        }

        /// <summary>
        /// Removes and deletes any SceneNode was created by a Particle System
        /// </summary>
        /// <param name="sceneNode">SceneNodes are created for certain situations (i.e. EntityRenderer), but just deleting them in the destructor of the 
        ///		Particle System gives unpredictable results.</param>
        public void RemoveAndDestroyDanglingSceneNodes(Mogre.SceneNode sceneNode)
        {
            try
            {
                if (sceneNode == null)
                    ParticleSystemManager_RemoveAndDestroyDanglingSceneNodes(NativePointer, IntPtr.Zero);
                else
                    ParticleSystemManager_RemoveAndDestroyDanglingSceneNodes(NativePointer, (IntPtr)sceneNode.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Remove all registered templates
        /// </summary>
        public void DestroyAllParticleSystemTemplates()
        {
            try
            {
                ParticleSystemManager_DestroyAllParticleSystemTemplates(NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a BoxSet. This is similar to a BillboardSet, instead it uses boxes.
        /// </summary>
        /// <param name="name">Name of the BoxSet.</param>
        /// <param name="sceneManager">Mogre SceneManager to attach to.</param>
        /// <param name="poolSize">Size of the pool? Normally 20.</param>
        /// <returns>Newly Created BoxSet</returns>
        public BoxSet CreateBoxSet(String name, Mogre.SceneManager sceneManager, uint poolSize = 20)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be nullor Empty!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager Cannot be null!");
            try
            {
                IntPtr boxSetPtr = ParticleSystemManager_CreateBoxSet(NativePointer, name, (IntPtr)sceneManager.NativePtr, poolSize);
                if (boxSetPtr == IntPtr.Zero)
                    return null;
                return BoxSet.GetInstance(boxSetPtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Destroy the BoxSet.
        /// </summary>
        /// <param name="boxSet">The BoxSet to destroy</param>
        /// <param name="sceneManager">Manager to unattach BoxSet</param>
        public void DestroyBoxSet(BoxSet boxSet, Mogre.SceneManager sceneManager)
        {
            if (boxSet == null)
                return;
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_DestroyBoxSet(NativePointer, boxSet.NativePointer, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a SphereSet. This is similar to a BillboardSet, instead it uses spheres.
        /// </summary>
        /// <param name="name">Name of the SphereSet.</param>
        /// <param name="sceneManager">Mogre SceneManager to attach to.</param>
        /// <param name="poolSize">Size of the pool? Normally 20.</param>
        /// <returns>Newly Created SphereSet</returns>
        public SphereSet CreateSphereSet(String name, Mogre.SceneManager sceneManager, uint poolSize = 20)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be nullor Empty!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager Cannot be null!");
            try
            {
                IntPtr pss = ParticleSystemManager_CreateSphereSet(NativePointer, name, (IntPtr)sceneManager.NativePtr, poolSize);
                if (pss == IntPtr.Zero)
                    return null;
                return SphereSet.GetInstance(pss);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Destroy the SphereSet.
        /// </summary>
        /// <param name="sphereSet">SphereSet to destroy.</param>
        /// <param name="sceneManager">Manager to unattach SphereSet</param>
        public void DestroySphereSet(SphereSet sphereSet, Mogre.SceneManager sceneManager)
        {
            if (sphereSet == null)
                return;
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_DestroySphereSet(NativePointer, sphereSet.NativePointer, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Registers a previously created Attachable. This is needed, because the scenemanager has to know.
        /// </summary>
        /// <param name="attachable">the Attachable to Add/Register.</param>
        /// <param name="sceneManager">SceneManager to notify/link to.</param>
        public void RegisterAttachable(IAttachable attachable, Mogre.SceneManager sceneManager)
        {
            if (attachable == null)
                throw new ArgumentNullException("attachable cannot be null!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_RegisterAttachable(NativePointer, attachable.NativePointer, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }

        }

        /// <summary>
        /// Unregisters a created Attachable.
        /// </summary>
        /// <param name="attachable">the Attachable to Remove/Unregister.</param>
        /// <param name="sceneManager">SceneManager to notify/unlink from.</param>
        public void UnregisterAttachable(IAttachable attachable, Mogre.SceneManager sceneManager)
        {
            if (attachable == null)
                throw new ArgumentNullException("attachable cannot be null!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_UnregisterAttachable(NativePointer, attachable.NativePointer, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Adds a new 'factory' object for emitters to the list of available emitter types.
        /// This method allows plugins etc to add new particle emitter types. Particle emitters
        /// 	are sources of particles, and generate new particles with their start positions, colours and
        /// 	momentums appropriately.
        /// All particle emitter factories have an assigned name which is used to identify the emitter
        /// 	type. This must be unique.
        /// </summary>
        /// <param name="factory">ParticleEmitterFactory subclass created by the plugin or application code.</param>
        public void AddEmitterFactory(ParticleEmitterFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddEmitterFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches a ParticleEmitterFactory and returns the Requested Factory if found
        /// </summary>
        /// <param name="emitterType">Factory Name</param>
        /// <returns>Previously created ParticleEmitterFactory</returns>
        public ParticleEmitterFactory GetEmitterFactory(String emitterType)
        {
            if (emitterType == null || emitterType.Length == 0)
                throw new ArgumentNullException("emitterType cannot be null or empty!");
            try
            {
                IntPtr pef = ParticleSystemManager_GetEmitterFactory(NativePointer, emitterType);
                if (pef == IntPtr.Zero) //not found?
                    return null;
                return new ParticleEmitterFactory(pef);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleEmitterFactory, but doesn't delete it. 
        /// </summary>
        /// <param name="emitterType">Search the factory by its type.</param>
        public void RemoveEmitterFactory(String emitterType)
        {
            if (emitterType == null || emitterType.Length == 0)
                throw new ArgumentNullException("emitterType cannot be null or empty!");
            try
            {
                ParticleSystemManager_RemoveEmitterFactory(NativePointer, emitterType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete an ParticleEmitterFactory. 
        /// </summary>
        /// <param name="emitterType">Search the factory by its type.</param>
        public void DestroyEmitterFactory(String emitterType)
        {
            if (emitterType == null || emitterType.Length == 0)
                throw new ArgumentNullException("emitterType cannot be null or empty!");
            try
            {
                ParticleSystemManager_DestroyEmitterFactory(NativePointer, emitterType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Method for creating a new emitter from a factory.
        /// </summary>
        /// <param name="emitterType">emitterType String name of the emitter type to be created. 
        ///     A factory of this type must have been registered.</param>
        /// <returns>A new ParticleEmitter</returns>
        public ParticleEmitter CreateEmitter(String emitterType)
        {
            if (emitterType == null || emitterType.Length == 0)
                throw new ArgumentNullException("emitterType cannot be null or empty!");
            try
            {
                return ParticleEmitter.GetParticleEmitterByPointer(ParticleSystemManager_CreateEmitter(NativePointer, emitterType));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a ParticleEmitter.
        /// </summary>
        /// <param name="emitter">Source Emitter.</param>
        /// <returns>A copy of the Source Emitter.</returns>
        public ParticleEmitter CloneEmitter(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            try
            {
                return ParticleEmitter.GetParticleEmitterByPointer(ParticleSystemManager_CloneEmitter(NativePointer, emitter.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete a ParticleEmitter.
        /// </summary>
        /// <param name="emitter">ParticleEmitter to Destroy.</param>
        public void DestroyEmitter(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            try
            {
                ParticleSystemManager_DestroyEmitter(NativePointer, emitter.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add a ParticleAffectorFactory to this ParticleSystemManager.
        /// </summary>
        /// <param name="factory">ParticleAffectorFactory subclass created by the plugin or application code.</param>
        public void AddAffectorFactory(ParticleAffectorFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddAffectorFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches a ParticleAffectorFactory.
        /// </summary>
        /// <param name="affectorType">Type of Affector to find.</param>
        /// <returns>ParticleAffectorFactory of searched type.</returns>
        public ParticleAffectorFactory GetAffectorFactory(String affectorType)
        {
            if (affectorType == null || affectorType.Length == 0)
                throw new ArgumentNullException("affectorType cannot be null or empty!");
            try
            {
                IntPtr paf = ParticleSystemManager_GetAffectorFactory(NativePointer, affectorType);
                if (paf == IntPtr.Zero)
                    return null;
                return new ParticleAffectorFactory(paf);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleAffectorFactory, but doesn't delete it. 
        /// </summary>
        /// <param name="affectorType">Search the factory by its type.</param>
        public void RemoveAffectorFactory(String affectorType)
        {
            if (affectorType == null || affectorType.Length == 0)
                throw new ArgumentNullException("affectorType cannot be null or empty!");
            try
            {
                ParticleSystemManager_RemoveAffectorFactory(NativePointer, affectorType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a ParticleAffectorFactory. 
        /// </summary>
        /// <param name="affectorType">Search by its type.</param>
        public void DestroyAffectorFactory(String affectorType)
        {
            if (affectorType == null || affectorType.Length == 0)
                throw new ArgumentNullException("affectorType cannot be null or empty!");
            try
            {
                ParticleSystemManager_DestroyAffectorFactory(NativePointer, affectorType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a ParticleAffector given a type.
        /// </summary>
        /// <param name="affectorType">Type of Affector to Create.</param>
        /// <returns>A new ParticleAffector of Specified type.</returns>
        public ParticleAffector CreateAffector(String affectorType)
        {
            if (affectorType == null || affectorType.Length == 0)
                throw new ArgumentNullException("affectorType cannot be null or empty!");
            try
            {
                return ParticleAffector.GetParticleAffectorByType(ParticleSystemManager_CreateAffector(NativePointer, affectorType));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a ParticleAffector.
        /// </summary>
        /// <param name="affector">Source Affector to Copy.</param>
        /// <returns>Duplicate of Source.</returns>
        public ParticleAffector CloneAffector(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            try
            {
                return ParticleAffector.GetParticleAffectorByType(ParticleSystemManager_CloneAffector(NativePointer, affector.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete a ParticleAffector.
        /// </summary>
        /// <param name="affector">Affector to destroy.</param>
        public void DestroyAffector(ParticleAffector affector)
        {
            if (affector == null)
                return;
            try
            {
                ParticleSystemManager_DestroyAffector(NativePointer, affector.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a ParticleTechnique.
        /// </summary>
        /// <returns>A new ParticleTechnique</returns>
        public ParticleTechnique CreateTechnique()
        {
            try
            {
                return ParticleTechnique.GetInstances(ParticleSystemManager_CreateTechnique(NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a ParticleTechnique.
        /// </summary>
        /// <param name="technique">Source ParticleTechnique to Copy.</param>
        /// <returns>Duplicate ParticleTechnique of Source.</returns>
        public ParticleTechnique CloneTechnique(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("affector cannot be null!");
            try
            {
                return ParticleTechnique.GetInstances(ParticleSystemManager_CloneTechnique(NativePointer, technique.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete a ParticleTechnique.
        /// </summary>
        /// <param name="technique">ParticleTechnique to Destroy.</param>
        public void DestroyTechnique(ParticleTechnique technique)
        {
            if (technique == null)
                return;
            try
            {
                ParticleSystemManager_DestroyTechnique(NativePointer, technique.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add a ParticleObserverFactory to this ParticleSystemManager.
        /// </summary>
        /// <param name="factory">ParticleObserverFactory subclass created by the plugin or application code.</param>
        public void AddObserverFactory(ParticleObserverFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddObserverFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches a ParticleObserverFactory.
        /// </summary>
        /// <param name="observerType">Type of ParticleObserverFactory to find.</param>
        /// <returns>ParticleObserverFactory of specified type if found.</returns>
        public ParticleObserverFactory GetObserverFactory(String observerType)
        {
            if (observerType == null || observerType.Length == 0)
                throw new ArgumentNullException("factory cannot be null or empty!");
            try
            {
                IntPtr pof = ParticleSystemManager_GetObserverFactory(NativePointer, observerType);
                if (pof == IntPtr.Zero)
                    return null;
                return new ParticleObserverFactory(pof);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleObserverFactory, but doesn't delete it.
        /// </summary>
        /// <param name="observerType">Search the factory by its type.</param>
        public void RemoveObserverFactory(String observerType)
        {
            if (observerType == null || observerType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_RemoveObserverFactory(NativePointer, observerType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a ParticleObserverFactory given its type.
        /// </summary>
        /// <param name="observerType">Type of ObserverFactory to Destroy.</param>
        public void DestroyObserverFactory(String observerType)
        {
            if (observerType == null || observerType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_DestroyObserverFactory(NativePointer, observerType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a ParticleObserver given a certain type.
        /// </summary>
        /// <param name="observerType">Type of ParticleObserver to Create.</param>
        /// <returns>A new ParticleObserver of Specified type.</returns>
        public ParticleObserver CreateObserver(String observerType)
        {
            if (observerType == null || observerType.Length == 0)
                throw new ArgumentNullException("observerType cannot be null or empty!");
            try
            {
                return ParticleObserver.GetObserverByPtr(ParticleSystemManager_CreateObserver(NativePointer, observerType));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a ParticleObserver.
        /// </summary>
        /// <param name="observer">Source ParticleObserver to copy.</param>
        /// <returns>Duplicate of Source ParticleObserver.</returns>
        public ParticleObserver CloneObserver(ParticleObserver observer)
        {
            if (observer == null)
                throw new ArgumentNullException("observer cannot be null!");
            try
            {
                return ParticleObserver.GetObserverByPtr(ParticleSystemManager_CloneObserver(NativePointer, observer.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Destroye a ParticleObserver.
        /// </summary>
        /// <param name="observer">ParticleObserver to Destroy.</param>
        public void DestroyObserver(ParticleObserver observer)
        {
            if (observer == null)
                return;
            try
            {
                ParticleSystemManager_DestroyObserver(NativePointer, observer.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add a ParticleEventHandlerFactory to this ParticleSystemManager.
        /// </summary>
        /// <param name="factory">ParticleEventHandlerFactory subclass created by the plugin or application code.</param>
        public void AddEventHandlerFactory(ParticleEventHandlerFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddEventHandlerFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches for a ParticleEventHandlerFactory.
        /// </summary>
        /// <param name="eventHandlerType">Type of Event Handler to find.</param>
        /// <returns>ParticleEventHandlerFactory if found.</returns>
        public ParticleEventHandlerFactory GetEventHandlerFactory(String eventHandlerType)
        {
            if (eventHandlerType == null || eventHandlerType.Length == 0)
                throw new ArgumentNullException("eventHandlerType cannot be null!");
            try
            {
                IntPtr pehf = ParticleSystemManager_GetEventHandlerFactory(NativePointer, eventHandlerType);
                if (pehf == IntPtr.Zero)
                    return null;
                return new ParticleEventHandlerFactory(pehf);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleEventHandlerFactory, but doesn't delete it. 
        /// </summary>
        /// <param name="eventHandlerType">Search the factory by its type.</param>
        public void RemoveEventHandlerFactory(String eventHandlerType)
        {
            if (eventHandlerType == null || eventHandlerType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_RemoveEventHandlerFactory(NativePointer, eventHandlerType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a ParticleEventHandlerFactory given a certain type.
        /// </summary>
        /// <param name="eventHandlerType">Type of EventHandler to Destroy.</param>
        public void DestroyEventHandlerFactory(String eventHandlerType)
        {
            if (eventHandlerType == null || eventHandlerType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_DestroyEventHandlerFactory(NativePointer, eventHandlerType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a ParticleEventHandler given a certain type.
        /// </summary>
        /// <param name="eventHandlerType">Type of Event Handler to Create</param>
        /// <returns>A new ParticleEventHandler of specified type.</returns>
        public ParticleEventHandler CreateEventHandler(String eventHandlerType)
        {
            if (eventHandlerType == null || eventHandlerType.Length == 0)
                throw new ArgumentNullException("eventHandlerType cannot be null or empty!");
            try
            {
                return ParticleEventHandler.GetEventHandlerFromPtr(ParticleSystemManager_CreateEventHandler(NativePointer, eventHandlerType));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a ParticleEventHandler.
        /// </summary>
        /// <param name="eventHandler">Source ParticleEventHandler to Copy.</param>
        /// <returns>Duplicate of Source ParticleEventHandler.</returns>
        public ParticleEventHandler CloneEventHandler(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                throw new ArgumentNullException("eventHandler cannot be null!");
            try
            {
                return ParticleEventHandler.GetEventHandlerFromPtr(ParticleSystemManager_CloneEventHandler(NativePointer, eventHandler.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete a ParticleEventHandler.
        /// </summary>
        /// <param name="eventHandler">Event Handler to Destroy.</param>
        public void DestroyEventHandler(ParticleEventHandler eventHandler)
        {
            if (eventHandler == null)
                return;
            try
            {
                ParticleSystemManager_DestroyEventHandler(NativePointer, eventHandler.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add a ParticleRendererFactory to this ParticleSystemManager.
        /// </summary>
        /// <param name="factory">ParticleRendererFactory to add.</param>
        public void AddRendererFactory(ParticleRendererFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddRendererFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches a ParticleRendererFactory.
        /// </summary>
        /// <param name="rendererType">Renderer Type to Find.</param>
        /// <returns>ParticleRendererFactory of specified type if found.</returns>
        public ParticleRendererFactory GetRendererFactory(String rendererType)
        {
            if (rendererType == null || rendererType.Length == 0)
                throw new ArgumentNullException("rendererType cannot be null or empty!");
            try
            {
                IntPtr prf = ParticleSystemManager_GetRendererFactory(NativePointer, rendererType);
                if (prf == IntPtr.Zero)
                    return null;
                return new ParticleRendererFactory(prf);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleRendererFactory, but doesn't delete it.
        /// </summary>
        /// <param name="rendererType">Search the factory by its type.</param>
        public void RemoveRendererFactory(String rendererType)
        {
            if (rendererType == null || rendererType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_RemoveRendererFactory(NativePointer, rendererType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a ParticleRendererFactory given a certain type.
        /// </summary>
        /// <param name="rendererType">Renderer Type to to Destroy.</param>
        public void DestroyRendererFactory(String rendererType)
        {
            if (rendererType == null || rendererType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_DestroyRendererFactory(NativePointer, rendererType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a ParticleRenderer.
        /// </summary>
        /// <param name="rendererType">Renderer type to Create.</param>
        /// <returns></returns>
        public ParticleRenderer CreateRenderer(String rendererType)
        {
            if (rendererType == null || rendererType.Length == 0)
                throw new ArgumentNullException("rendererType cannot be null or empty!");
            try
            {
                return ParticleRendererHelper.GetParticleRenderer(ParticleSystemManager_CreateRenderer(NativePointer, rendererType));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a ParticleRenderer.
        /// </summary>
        /// <param name="renderer">Source Renderer to Copy.</param>
        /// <returns>Duplicate of Source ParticleRenderer.</returns>
        public ParticleRenderer CloneRenderer(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            try
            {
                return ParticleRendererHelper.GetParticleRenderer(ParticleSystemManager_CloneRenderer(NativePointer, renderer.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete a ParticleRenderer.
        /// </summary>
        /// <param name="renderer">Renderer to Destroy.</param>
        public void DestroyRenderer(ParticleRenderer renderer)
        {
            if (renderer == null)
                return;
            try
            {
                ParticleSystemManager_DestroyRenderer(NativePointer, renderer.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add a ExternFactory to this ParticleSystemManager.
        /// </summary>
        /// <param name="factory">ExternFactory to Add.</param>
        public void AddExternFactory(ExternFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddExternFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches an ExternFactory.
        /// </summary>
        /// <param name="externType">Type of ExternFactory to Find</param>
        /// <returns>ExternFactory if found.</returns>
        public ExternFactory GetExternFactory(String externType)
        {
            if (externType == null || externType.Length ==  0)
                throw new ArgumentNullException("externType cannot be null or empty!");
            try
            {
                IntPtr ef = ParticleSystemManager_GetExternFactory(NativePointer, externType);
                if (ef == IntPtr.Zero)
                    return null;
                return new ExternFactory(ef);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleExternFactory, but doesn't delete it.
        /// </summary>
        /// <param name="externType">Search the factory by its type.</param>
        public void RemoveExternFactory(String externType)
        {
            if (externType == null || externType.Length == 0)
                throw new ArgumentNullException("externType cannot be null or empty!");
            try
            {
                ParticleSystemManager_RemoveExternFactory(NativePointer, externType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a ExternFactory given a certain type.
        /// </summary>
        /// <param name="externType">Type of ExternFactory to Destroy.</param>
        public void DestroyExternFactory(String externType)
        {
            if (externType == null || externType.Length == 0)
                throw new ArgumentNullException("externType cannot be null or empty!");
            try
            {
                ParticleSystemManager_DestroyExternFactory(NativePointer, externType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create an Extern object.
        /// </summary>
        /// <param name="externType">Type of Extern Object to create</param>
        /// <returns>A new Extern if Valid.</returns>
        public Extern CreateExtern(String externType)
        {
            if (externType == null || externType.Length == 0)
                throw new ArgumentNullException("externType cannot be null or empty!");
            return ExternFactory.GetExternByType(externType);
        }

        /// <summary>
        /// Clone an Extern object.
        /// </summary>
        /// <param name="externObject">Source Extern to copy.</param>
        /// <returns>Duplicate of Source Extern.</returns>
        public Extern CloneExtern(Extern externObject)
        {
            if (externObject == null)
                return null;
            Extern copy = CreateExtern(externObject.Type);
            externObject.CopyAttributesTo(copy);
            externObject.CopyParentAttributesTo(copy);
            return copy;
        }

        /// <summary>
        /// Delete an Extern object.
        /// </summary>
        /// <param name="externObject">Extern to Destroy.</param>
        public void DestroyExtern(Extern externObject)
        {
            if (externObject == null)
                return;
            try
            {
                ParticleSystemManager_DestroyExtern(NativePointer, externObject.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add a BehaviourFactory to this ParticleSystemManager.
        /// </summary>
        /// <param name="factory">Factory to add.</param>
        public void AddBehaviourFactory(ParticleBehaviourFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory cannot be null!");
            try
            {
                ParticleSystemManager_AddBehaviourFactory(NativePointer, factory.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Searches a ParticleBehaviourFactory.
        /// </summary>
        /// <param name="behaviourType">Type of Behaviour to find.</param>
        /// <returns>ParticleBehaviourFactory if found.</returns>
        public ParticleBehaviourFactory GetBehaviourFactory(String behaviourType)
        {
            if (behaviourType == null || behaviourType.Length == 0)
                throw new ArgumentNullException("behaviourType cannot be null or empty!");
            try
            {
                IntPtr pbf = ParticleSystemManager_GetBehaviourFactory(NativePointer, behaviourType);
                if (pbf == IntPtr.Zero)
                    return null;
                return new ParticleBehaviourFactory(pbf);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove an ParticleBehaviourFactory, but doesn't delete it.
        /// </summary>
        /// <param name="behaviourType">Search the factory by its type.</param>
        public void RemoveBehaviourFactory(String behaviourType)
        {
            if (behaviourType == null || behaviourType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_RemoveBehaviourFactory(NativePointer, behaviourType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a BehaviourFactory given a certain type.
        /// </summary>
        /// <param name="behaviourType">Type of Behaviour Factory to Destroy.</param>
        public void DestroyBehaviourFactory(String behaviourType)
        {
            if (behaviourType == null || behaviourType.Length == 0)
                return;
            try
            {
                ParticleSystemManager_DestroyBehaviourFactory(NativePointer, behaviourType);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a Behaviour object.
        /// </summary>
        /// <param name="behaviourType">Type of Behaviour to create.</param>
        /// <returns>A new ParticleBehaviour if valid.</returns>
        public ParticleBehaviour CreateBehaviourFactory(String behaviourType)
        {
            if (behaviourType == null || behaviourType.Length == 0)
                throw new ArgumentNullException("behaviourType cannot be null or empty!");
            try
            {
                return ParticleBehaviourHelper.GetParticleBehaviour(ParticleSystemManager_CreateBehaviourFactory(NativePointer, behaviourType));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clone a Behaviour object.
        /// </summary>
        /// <param name="behaviour">Source Behaviour object to copy.</param>
        /// <returns>A Duplicate of Source ParticleBehaviour.</returns>
        public ParticleBehaviour CloneBehaviour(ParticleBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException("behaviourType cannot be null!");
            try
            {
                return ParticleBehaviourHelper.GetParticleBehaviour(ParticleSystemManager_CloneBehaviour(NativePointer, behaviour.NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete a Behaviour object.
        /// </summary>
        /// <param name="behaviour">Behaviour to destroy.</param>
        public void DestroyBehaviour(ParticleBehaviour behaviour)
        {
            if (behaviour == null)
                return;
            try
            {
                ParticleSystemManager_DestroyBehaviour(NativePointer, behaviour.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a ParticleSystemTemplate.
        /// 	ParticleSystemTemplates contain a ParticleSystem and form a blueprint for other ParticleSystems.
        /// 	Given the name of the template, a copy is made of the ParticleSystem. This copy can be used
        /// 	in your application. The ParticleSystem templates however are restricted to the 
        /// 	ParticleSystemManager.
        /// </summary>
        /// <param name="name">Name of Particle System to get.</param>
        /// <param name="resourceGroupName">Resource Group this Particle System Belongs to.</param>
        /// <returns>ParticleSystem if found.</returns>
        public ParticleSystem CreateParticleSystemTemplate(String name, String resourceGroupName)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            if (resourceGroupName == null || resourceGroupName.Length == 0)
                throw new ArgumentNullException("resourceGroupName cannot be null or empty!");
            try
            {
                return ParticleSystem.GetInstances(ParticleSystemManager_CreateParticleSystemTemplate(NativePointer, name, resourceGroupName));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Replace a ParticleSystemTemplate with an existing Particle System.
        ///	The existing Particle System is cloned and still exists after the call.
        /// </summary>
        /// <param name="name">Name of ParticleSystem to replace</param>
        /// <param name="system">The new ParticleSystem</param>
        public void ReplaceParticleSystemTemplate(String name, ParticleSystem system)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            if (system == null)
                throw new ArgumentNullException("system cannot be null!");
            try
            {
                ParticleSystemManager_ReplaceParticleSystemTemplate(NativePointer, name, system.nativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Returns the name of the last created template. Because the templates are often created by means of a script, it is not
        /// 	straightforward to get the template name. It is embedded in the script. Only afterwards it is possible to get the 
        /// 	name of the created template, by using this function.
        /// </summary>
        /// <returns>Returns the name of the last created template.</returns>
        public String GetLastCreatedParticleSystemTemplateName()
        {
            try
            {
                return Marshal.PtrToStringAnsi(ParticleSystemManager_GetLastCreatedParticleSystemTemplateName(NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Add a ParticleSystem template to this ParticleSystemManager.
        /// </summary>
        /// <param name="name">Name of Particle System</param>
        /// <param name="systemTemplate">ParticleSystem to Add.</param>
        public void AddParticleSystemTemplate(String name, ParticleSystem systemTemplate)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            if (systemTemplate == null)
                throw new ArgumentNullException("systemTemplate cannot be null!");
            try
            {
                ParticleSystemManager_AddParticleSystemTemplate(NativePointer, name, systemTemplate.nativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Retrieves a particle system template.
        /// </summary>
        /// <param name="templateName">Name of ParticleSystem to find.</param>
        /// <returns>ParticleSystem if found.</returns>
        public ParticleSystem GetParticleSystemTemplate(String templateName)
        {
            if (templateName == null || templateName.Length == 0)
                throw new ArgumentNullException("templateName cannot be null or empty!");
            try
            {
                return ParticleSystem.GetInstances(ParticleSystemManager_GetParticleSystemTemplate(NativePointer, templateName));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Remove a particle system template and delete it.
        /// </summary>
        /// <param name="templateName">Name of ParticleSystem to Destroy.</param>
        public void DestroyParticleSystemTemplate(String templateName)
        {
            if (templateName == null || templateName.Length == 0)
                return;
            try
            {
                ParticleSystemManager_DestroyParticleSystemTemplate(NativePointer, templateName);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Gets the number of Particle Systems registered in the Manager.
        /// This is used by the Wrapper for String[] ParticleSystemTemplateNames()
        /// </summary>
        /// <returns>Number of Particle Systems</returns>
        public int ParticleSystemTemplateNamesSize()
        {
            try
            {
                return ParticleSystemManager_ParticleSystemTemplateNamesSize(NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return -1;
        }

        /// <summary>
        /// Fill a list of template names registered in the Manager.
        /// </summary>
        /// <returns>String Array of Particle Systems Registered.</returns>
        public String[] ParticleSystemTemplateNames()
        {
            try
            {
                int bufSize = ParticleSystemManager_ParticleSystemTemplateNamesSize(NativePointer);
                char[] buf = new char[bufSize]; //[bufSize];

                String[] toReturn;

                fixed (char* bufHandle = buf)
                {
                    ParticleSystemManager_ParticleSystemTemplateNames(NativePointer, buf, buf.Length);

                    String strArr = new String(buf);
                    toReturn = strArr.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                }
                return toReturn;
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Creates a ParticleSystem, using a template as a blueprint.
        /// This is the function that must be used by the client application that wants to create a
        ///	ParticleSystem.
        /// </summary>
        /// <param name="name">Unique Name of Particle System</param>
        /// <param name="templateName">Name of Particle System Template Registered with Manager</param>
        /// <param name="sceneManager">SceneManager to Link With.</param>
        /// <returns>ParticleSystem if found.</returns>
        public ParticleSystem CreateParticleSystem(String name, String templateName, Mogre.SceneManager sceneManager)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            if (templateName == null || templateName.Length == 0)
                throw new ArgumentNullException("templateName cannot be null or empty!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                return ParticleSystem.GetInstances(ParticleSystemManager_CreateParticleSystem(NativePointer, name, templateName, (IntPtr)sceneManager.NativePtr));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Creates a default ParticleSystem.
        /// This is the function that must be used by the client application that wants to create a
        ///		ParticleSystem.
        /// </summary>
        /// <param name="name">Unique Name of Particle System</param>
        /// <param name="sceneManager">SceneManager to Link With.</param>
        /// <returns>A new ParticleSystem if valid.</returns>
        public ParticleSystem CreateParticleSystem(String name, Mogre.SceneManager sceneManager)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                return ParticleSystem.GetInstances(ParticleSystemManager_CreateParticleSystem2(NativePointer, name, (IntPtr)sceneManager.NativePtr));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Get a ParticleSystem by name.
        /// </summary>
        /// <param name="name">Unique name of Particle System to Find.</param>
        /// <returns>The Registered Particle System if found.</returns>
        public ParticleSystem GetParticleSystem(String name)
        {
            if (name == null || name.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            try
            {
                return ParticleSystem.GetInstances(ParticleSystemManager_GetParticleSystem(NativePointer, name));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// This is the function that must be used by the client application that wants to delete a ParticleSystem. 
        /// </summary>
        /// <param name="particleSystem">Unique name of Particle System to Destroy.</param>
        /// <param name="sceneManager">SceneManager to UnLink/Remove From.</param>
        public void DestroyParticleSystem(ParticleSystem particleSystem, Mogre.SceneManager sceneManager)
        {
            if (particleSystem == null)
                throw new ArgumentNullException("particleSystem cannot be null!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_DestroyParticleSystem(NativePointer, particleSystem.nativePtr, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete a ParticleSystem.
        /// This implementation deletes a ParticleSystem identified by its name. The function always
        ///		validates whether the name still exists.
        /// </summary>
        /// <param name="particleSystemName">Unique name of Particle System to Destroy.</param>
        /// <param name="sceneManager">SceneManager to UnLink/Remove From.</param>
        public void DestroyParticleSystem(String particleSystemName, Mogre.SceneManager sceneManager)
        {
            if (particleSystemName == null || particleSystemName.Length == 0)
                throw new ArgumentNullException("name cannot be null or empty!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_DestroyParticleSystemByName(NativePointer, particleSystemName, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Destroy all registered particle systems.
        /// Particle Systems are NOT automatically destroyed if the ParticleSystemManager is destroyed.
        ///		The reason is, that the ParticleSystemManager is destroyed when the plugin is unloaded, which is too late.
        ///		Before a scene is cleared, all particle systems must be deleted first (manually).
        /// </summary>
        /// <param name="sceneManager">SceneManager to Notify.</param>
        public void DestroyAllParticleSystems(Mogre.SceneManager sceneManager)
        {
            if (sceneManager == null)
                return;
            try
            {
                ParticleSystemManager_DestroyAllParticleSystems(NativePointer, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Add an alias to this ParticleSystemManager.
        /// An alias is a ParticleTechnique, ParticleEmitter, ParticleAffector, etc. and forms the
        ///		blueprint to create copies. These copies can be used again in ParticleSystems (templates).
        ///		An alias is similar to a template, but where templates are restricted to ParticleSystems, 
        ///		aliasses can be any type of object that inherits from IAlias.
        /// </summary>
        /// <param name="alias">Alias to Add.</param>
        public void AddAlias(IAlias alias)
        {
            if (alias == null)
                throw new ArgumentNullException("alias cannot be null!");
            try
            {
                ParticleSystemManager_AddAlias(NativePointer, alias.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Returns an alias.
        /// </summary>
        /// <param name="aliasName">name of Alias to Find.</param>
        /// <returns>Returns Alias if found.</returns>
        public IAlias GetAlias(String aliasName)
        {
            if (aliasName == null || aliasName.Length == 0)
                throw new ArgumentNullException("aliasName cannot be null!");
            try
            {
                IntPtr aliasPtr = ParticleSystemManager_GetAlias(NativePointer, aliasName);
                if (aliasPtr == IntPtr.Zero)
                    return null;
                switch (IAlias_GetAliasType(aliasPtr))
                {
                    case AliasType.AT_UNDEFINED:
                        return null; //We don't support this at this time ;-)
                    case AliasType.AT_TECHNIQUE:
                        return ParticleTechnique.GetInstances(aliasPtr);
                    case AliasType.AT_RENDERER:
                        return ParticleRendererHelper.GetParticleRenderer(aliasPtr);
                    case AliasType.AT_EMITTER:
                        return ParticleEmitter.GetParticleEmitterByPointer(aliasPtr);
                    case AliasType.AT_AFFECTOR:
                        return ParticleAffector.GetParticleAffectorByType(aliasPtr);
                    case AliasType.AT_OBSERVER:
                        return ParticleObserver.GetObserverByPtr(aliasPtr);
                    case AliasType.AT_EXTERN:
                        return ExternFactory.GetExternByPointer(aliasPtr);
                    case AliasType.AT_HANDLER:
                        return ParticleEventHandler.GetEventHandlerFromPtr(aliasPtr);
                    case AliasType.AT_BEHAVIOUR:
                        return ParticleBehaviourHelper.GetParticleBehaviour(aliasPtr);
                }

            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Delete an alias from the ParticleSystemManager.
        /// </summary>
        /// <param name="alias">Alias to Destroy.</param>
        public void DestroyAlias(IAlias alias)
        {
            if (alias == null)
                throw new ArgumentNullException("alias cannot be null!");
            try
            {
                ParticleSystemManager_DestroyAlias(NativePointer, alias.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Delete all aliasses.
        /// </summary>
        public void DestroyAllAliasses()
        {
            try
            {
                ParticleSystemManager_DestroyAllAliasses(NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        ///// <summary>
        ///// Returns the alias map. Not for general purposes; only use it if you really need it.
        ///// </summary>
        ////public AliasMap _GetAliasMap() {ParticleSystemManager__GetAliasMap(NativePointer);}
        ////{
        ////	return ptr->_getAliasMap();
        ////}

        /// <summary>
        /// Gets the ParticleSystemManager.
        /// </summary>
        /// <returns>ParticleSystemManager</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public static ParticleSystemManager Singleton
        {
            get
            {
                IntPtr psm = ParticleSystemManager_Singleton();
                return ParticleSystemManager.GetInstance(psm);
            }
        }

        /// <summary>
        /// Override standard Singleton retrieval.
        /// Use Singleton() instead.
        /// </summary>
        /// <returns>A Pointer to ParticleSystemManager</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        protected IntPtr SingletonPtr()
        {
            return ParticleSystemManager_SingletonPtr();
        }

        /// <summary>
        /// Writes a ParticleSystem to a file in the script format.
        /// </summary>
        /// <param name="particleSystem">ParticleSystem to Save.</param>
        /// <param name="fileName">Location of File to write.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public void WriteScript(ParticleSystem particleSystem, String fileName)
        {
            if (particleSystem == null)
                throw new ArgumentNullException("particleSystem cannot be null!");
            if (fileName == null || fileName.Length == 0)
                throw new ArgumentNullException("fileName cannot be null or empty!");
            
            ParticleSystemManager_WriteScript(NativePointer, particleSystem.nativePtr, fileName);
        }

        /// <summary>
        /// Writes a ParticleSystem to string in the script format.
        /// </summary>
        /// <param name="particleSystem">ParticleSystem to Save.</param>
        /// <returns>The ParticleSystem Script.</returns>
        public String WriteScript(ParticleSystem particleSystem)
        {
            if (particleSystem == null)
                throw new ArgumentNullException("particleSystem cannot be null!");
            try
            {
                return Marshal.PtrToStringAnsi(ParticleSystemManager_WriteScript2(NativePointer, particleSystem.nativePtr));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// The ParticleScriptSerializer is the main class responsible for serializing a Particle System to a script.
        /// </summary>
        /// <returns>Returns the ParticleScriptSerializer.</returns>
        public ParticleScriptSerializer GetParticleScriptSerializer()
        {
            try
            {
                return ParticleScriptSerializer.GetInstance(ParticleSystemManager_GetParticleScriptSerializer(NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// <see cref="ScriptWriter.Write"/>
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="element"></param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public void Write(ParticleScriptSerializer serializer, IElement element)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer cannot be null!");
            if (element == null)
                throw new ArgumentNullException("element cannot be null!");
            ParticleSystemManager_Write(NativePointer, serializer.NativePointer, element.NativePointer);
        }

        /// <summary>
        /// CameraDependency is used as a container that stores data parsed from a particle script.
        /// </summary>
        /// <returns>Returns Camera Dependency.</returns>
        public CameraDependency CreateCameraDependency()
        {
            try
            {
                return CameraDependency.GetInstance(ParticleSystemManager_CreateCameraDependency(NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Create a depth map (texture) of all visible scene objects, except overlays and particle systems that render soft particles.
        /// </summary>
        /// <param name="camera">Camera to Use for FOV</param>
        /// <param name="sceneManager">SceneManager to Use</param>
        public void CreateDepthMap(Mogre.Camera camera, Mogre.SceneManager sceneManager)
        {
            if (camera == null)
                throw new ArgumentNullException("camera cannot be null!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            try
            {
                ParticleSystemManager_CreateDepthMap(NativePointer, (IntPtr)camera.NativePtr, (IntPtr)sceneManager.NativePtr);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Perform cleanup activities.
        /// </summary>
        public void DestroyDepthMap()
        {
            try
            {
                ParticleSystemManager_DestroyDepthMap(NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Notify the Particle System Manager that is depthmap is needed.
        /// </summary>
        /// <param name="camera">Camera to Use for FOV</param>
        /// <param name="sceneManager">SceneManager to Use</param>
        /// <returns>True or False if DepthMap updated?</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public bool NotifyDepthMapNeeded(Mogre.Camera camera, Mogre.SceneManager sceneManager)
        {
            if (camera == null)
                throw new ArgumentNullException("camera cannot be null!");
            if (sceneManager == null)
                throw new ArgumentNullException("sceneManager cannot be null!");
            return ParticleSystemManager_NotifyDepthMapNeeded(NativePointer, (IntPtr)camera.NativePtr, (IntPtr)sceneManager.NativePtr);
        }

        /// <summary>
        /// Register the renderer, because it renders soft particles.
        /// Registration of the renderers is done by the ParticleSystemManager, because the depthmap that is used for soft particles
        ///		is rendered only once under management of the ParticleSystemManager. The reason why the renderers are registered and
        ///		not the complete ParticleSystems is to allow that ParticleSystem can contain a ParticleTechnique with soft particles and
        ///		one without (also in relation to LOD strategies).
        /// </summary>
        /// <param name="renderer">ParticleRenderer to Register.</param>
        public void RegisterSoftParticlesRenderer(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            try
            {
                ParticleSystemManager_RegisterSoftParticlesRenderer(NativePointer, renderer.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Unregister the renderer, because it will not use soft particles anymore.
        /// </summary>
        /// <param name="renderer">ParticleRenderer to Unregister.</param>
        public void UnregisterSoftParticlesRenderer(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            try
            {
                ParticleSystemManager_UnregisterSoftParticlesRenderer(NativePointer, renderer.NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Used to scale the values of the generated depth map.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public float DepthScale
        {
            get { return (float)ParticleSystemManager_GetDepthScale(NativePointer); }
            set { ParticleSystemManager_SetDepthScale(NativePointer, value); }
        }

        /// <summary>
        /// Returns the name of the depth texture.
        /// </summary>
        /// <returns>Returns the name of the depth texture.</returns>
        public String GetDepthTextureName()
        {
            try
            {
                return Marshal.PtrToStringAnsi(ParticleSystemManager_GetDepthTextureName(NativePointer));
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Set a depth texture name from a depthmap that is created outside Particle Universe. This means that the external source
        ///		also has to take into account that the Particle Universe particles are excluded from the depth map.
        /// </summary>
        /// <param name="depthTextureName"></param>
        public void SetExternDepthTextureName(String depthTextureName)
        {
            if (depthTextureName == null || depthTextureName.Length == 0)
                throw new ArgumentNullException("depthTextureName cannot be null!");
            try
            {
                ParticleSystemManager_SetExternDepthTextureName(NativePointer, depthTextureName);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Set a depth texture name from a depthmap that is created outside Particle Universe. This means that the external source
        ///		also has to take into account that the Particle Universe particles are excluded from the depth map.
        /// Returns the name of the depth texture.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public String DepthTextureName
        {
            get { return GetDepthTextureName(); }
            set { SetExternDepthTextureName(value); }
        }

        /// <summary>
        /// Reset the external depth texture name, so it is not used anymore.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public void ResetExternDepthTextureName()
        {
            try
            {
                ParticleSystemManager_resetExternDepthTextureName(NativePointer);
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
        }

        /// <summary>
        /// Create a DynamicAttribute.
        /// This can be called from outside the plugin, memory is allocated correctly.
        /// </summary>
        /// <param name="type">DynamicAttributeType to create</param>
        /// <returns>a new DynamicAttribute Object or Null if there was a problem.</returns>
        public DynamicAttribute CreateDynamicAttribute(DynamicAttributeType type)
        {
            if (type == null)
                throw new ArgumentNullException("type cannot be null!");
            try
            {
                IntPtr da = ParticleSystemManager_CreateDynamicAttribute(NativePointer, (int)type);
                if (da != IntPtr.Zero)
                {
                    switch (type)
                    {
                        case DynamicAttributeType.DAT_CURVED:
                            return new DynamicAttributeCurved(da);
                        case DynamicAttributeType.DAT_FIXED:
                            return new DynamicAttributeFixed(da);
                        case DynamicAttributeType.DAT_OSCILLATE:
                            return new DynamicAttributeOscillate(da);
                        case DynamicAttributeType.DAT_RANDOM:
                            return new DynamicAttributeRandom(da);
                    }
                }
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Determines whether materials must be loaded dynamically or not. If 'false', the materials must be loaded 
        ///	manually.
        ///	This gives more control over loading the materials.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an exception when something goes wrong.</exception>  
        public bool AutoLoadMaterials
        {
            get { return ParticleSystemManager_IsAutoLoadMaterials(NativePointer); }
            set { ParticleSystemManager_SetAutoLoadMaterials(NativePointer, value); }
        }

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
            ParticleSystemManager_Destroy(NativePointer);
            particleSystemManagerInstances.Remove(NativePointer);
        }

        #endregion

        //String puPluginPath = @"C:\Users\Ga'an\Documents\Visual Studio 2010\Projects\MParticleUniverse\ParticleUniverseTest\bin\Debug\";
        #region PINVOKE
        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern AliasType IAlias_GetAliasType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasType(IntPtr ptr, AliasType aliasType);


        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_GetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr IAlias_GetAliasName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "IAlias_SetAliasName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void IAlias_SetAliasName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string aliasName);


        //[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_New", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr ParticleSystem_New([In] [MarshalAs(UnmanagedType.LPStr);]String name);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveAndDestroyDanglingSceneNodes", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveAndDestroyDanglingSceneNodes(IntPtr ptr, IntPtr sceneNode);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyAllParticleSystemTemplates", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyAllParticleSystemTemplates(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateBoxSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateBoxSet(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr sceneManager, uint poolSize = 20);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyBoxSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyBoxSet(IntPtr ptr, IntPtr boxSet, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateSphereSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateSphereSet(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr sceneManager, uint poolSize = 20);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroySphereSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroySphereSet(IntPtr ptr, IntPtr sphereSet, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RegisterAttachable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RegisterAttachable(IntPtr ptr, IntPtr attachable, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_UnregisterAttachable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_UnregisterAttachable(IntPtr ptr, IntPtr attachable, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddEmitterFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddEmitterFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetEmitterFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetEmitterFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string emitterType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveEmitterFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveEmitterFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string emitterType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyEmitterFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyEmitterFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string emitterType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateEmitter(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string emitterType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneEmitter(IntPtr ptr, IntPtr emitter);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyEmitter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyEmitter(IntPtr ptr, IntPtr emitter);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddAffectorFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddAffectorFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetAffectorFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetAffectorFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string affectorType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveAffectorFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveAffectorFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string affectorType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyAffectorFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyAffectorFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string affectorType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateAffector(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string affectorType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneAffector(IntPtr ptr, IntPtr affector);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyAffector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyAffector(IntPtr ptr, IntPtr affector);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneTechnique(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyTechnique(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddObserverFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddObserverFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetObserverFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetObserverFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string observerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveObserverFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveObserverFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string observerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyObserverFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyObserverFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string observerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateObserver(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string observerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneObserver(IntPtr ptr, IntPtr observer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyObserver", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyObserver(IntPtr ptr, IntPtr observer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddEventHandlerFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddEventHandlerFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetEventHandlerFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetEventHandlerFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string eventHandlerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveEventHandlerFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveEventHandlerFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string eventHandlerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyEventHandlerFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyEventHandlerFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string eventHandlerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateEventHandler(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string eventHandlerType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneEventHandler(IntPtr ptr, IntPtr eventHandler);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyEventHandler", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyEventHandler(IntPtr ptr, IntPtr eventHandler);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddRendererFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddRendererFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetRendererFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetRendererFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string rendererType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveRendererFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveRendererFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string rendererType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyRendererFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyRendererFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string rendererType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateRenderer(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string rendererType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneRenderer(IntPtr ptr, IntPtr renderer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyRenderer(IntPtr ptr, IntPtr renderer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddExternFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddExternFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetExternFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetExternFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveExternFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveExternFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyExternFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyExternFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateExtern(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneExtern(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyExtern(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddBehaviourFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddBehaviourFactory(IntPtr ptr, IntPtr factory);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetBehaviourFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetBehaviourFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string behaviourType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RemoveBehaviourFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RemoveBehaviourFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string behaviourType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyBehaviourFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyBehaviourFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string behaviourType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateBehaviourFactory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateBehaviourFactory(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string behaviourType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CloneBehaviour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CloneBehaviour(IntPtr ptr, IntPtr behaviour);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyBehaviour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyBehaviour(IntPtr ptr, IntPtr behaviour);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateParticleSystemTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateParticleSystemTemplate(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, [In] [MarshalAs(UnmanagedType.LPStr)]string resourceGroupName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_ReplaceParticleSystemTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_ReplaceParticleSystemTemplate(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr system);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetLastCreatedParticleSystemTemplateName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetLastCreatedParticleSystemTemplateName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddParticleSystemTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddParticleSystemTemplate(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr systemTemplate);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetParticleSystemTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetParticleSystemTemplate(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string templateName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyParticleSystemTemplate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyParticleSystemTemplate(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string templateName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_ParticleSystemTemplateNamesSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystemManager_ParticleSystemTemplateNamesSize(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_ParticleSystemTemplateNames", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_ParticleSystemTemplateNames(IntPtr particleSystemPtr, [Out]  [MarshalAs(UnmanagedType.LPArray)] char[] templateNameBuffer, int bufferSize);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateParticleSystem(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, [In] [MarshalAs(UnmanagedType.LPStr)]string templateName, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateParticleSystem2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateParticleSystem2(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetParticleSystem(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyParticleSystem", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyParticleSystem(IntPtr ptr, IntPtr particleSystem, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyParticleSystemByName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyParticleSystemByName(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string particleSystemName, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyAllParticleSystems", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyAllParticleSystems(IntPtr ptr, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_AddAlias", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_AddAlias(IntPtr ptr, IntPtr alias);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetAlias", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetAlias(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string aliasName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyAlias", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyAlias(IntPtr ptr, IntPtr alias);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyAllAliasses", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyAllAliasses(IntPtr ptr);

        //[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_New", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern ParticleUniverse::ParticleSystemManager::AliasMap* ParticleSystemManager__GetAliasMap(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_Singleton", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_Singleton();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_SingletonPtr", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_SingletonPtr();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_WriteScript", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_WriteScript(IntPtr ptr, IntPtr particleSystem, [In] [MarshalAs(UnmanagedType.LPStr)]string fileName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_WriteScript2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_WriteScript2(IntPtr ptr, IntPtr particleSystem);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetParticleScriptSerializer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetParticleScriptSerializer(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_Write", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_Write(IntPtr ptr, IntPtr serializer, IntPtr element);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateCameraDependency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateCameraDependency(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateDepthMap", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_CreateDepthMap(IntPtr ptr, IntPtr camera, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_DestroyDepthMap", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_DestroyDepthMap(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_NotifyDepthMapNeeded", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystemManager_NotifyDepthMapNeeded(IntPtr ptr, IntPtr camera, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_RegisterSoftParticlesRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_RegisterSoftParticlesRenderer(IntPtr ptr, IntPtr renderer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_UnregisterSoftParticlesRenderer", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_UnregisterSoftParticlesRenderer(IntPtr ptr, IntPtr renderer);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetDepthScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystemManager_GetDepthScale(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_SetDepthScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_SetDepthScale(IntPtr ptr, float depthScale);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_GetDepthTextureName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_GetDepthTextureName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_SetExternDepthTextureName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_SetExternDepthTextureName(IntPtr ptr, [In] [MarshalAs(UnmanagedType.LPStr)]string depthTextureName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_resetExternDepthTextureName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_resetExternDepthTextureName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_CreateDynamicAttribute", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystemManager_CreateDynamicAttribute(IntPtr ptr, int type);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_IsAutoLoadMaterials", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystemManager_IsAutoLoadMaterials(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystemManager_SetAutoLoadMaterials", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystemManager_SetAutoLoadMaterials(IntPtr ptr, bool autoLoadMaterials);

        #endregion
    }
}