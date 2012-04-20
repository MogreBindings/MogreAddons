/*
-----------------------------------------------------------------------------------------------
This source file is part of the Particle Universe product.

Copyright (c) 2010 Henry van Merode

Usage of this program is licensed under the terms of the Particle Universe Commercial License.
You can find a copy of the Commercial License in the Particle Universe package.
-----------------------------------------------------------------------------------------------
*/
        
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using MParticleUniverse.ParticleRenderers;



namespace MParticleUniverse
{
    /** A ParticleSystem is the most top level of a particle structure, that consists of Particles, ParticleEmitters, 
        ParticleAffectors, ParticleObservers, etc. 
    @remarks
        The ParticleSystem can be seen as the container that includes the components that are needed to Create, 
        display and move particles.
    */
    public unsafe class ParticleSystem : Mogre.MovableObject, IParticle, IElement, IDisposable
    {
        /// <summary>
        /// Used while Debugging
        /// </summary>
        private const bool printDebug = true;
        private Exception lastError = null;
        /// <summary>
        /// Returns the Last Exception thrown by this class if any.
        /// Not including any exceptions not caught (i.e. methods that throw an exception when called).
        /// </summary>
        public Exception LastError
        {
            get { return lastError; }
        }

        private IParticle iParticle;

        internal IntPtr nativePtr;
        /// <summary>
        /// The Particle System in unamanged memory this class represents.
        /// </summary>
        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        /// <summary>
        /// The running states of this Partivle System.
        /// </summary>
        public enum ParticleSystemState
        {
            PSS_PREPARED = 0,
            PSS_STARTED = 1,
            PSS_STOPPED = 2,
            PSS_PAUSED = 3
        };

        internal static Dictionary<IntPtr, ParticleSystem> particleSystemInstances;

        internal static ParticleSystem GetInstances(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;

            if (particleSystemInstances == null)
                particleSystemInstances = new Dictionary<IntPtr, ParticleSystem>();

            ParticleSystem newvalue;

            if (particleSystemInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ParticleSystem(ptr);
            particleSystemInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ParticleSystem(IntPtr ptr)
            : base((CLRObject*)ptr)
        {
            nativePtr = ptr;
            iParticle = Particle.GetInstance(ptr);
            UpdateTechniqueList();
        }

        /// <summary>
        /// Use this to create a ParticleSystem rather than the constructor due do the limitation of passing a pointer 
        /// to the Mogre.MoveableObject base from within the constructor in C#. 
        /// TODO: Fix this to use a constructor rather than a static method. 
        /// </summary>
        /// <param name="name">Name of the Particle System</param>
        /// <returns>New ParticleSystem Object</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public ParticleSystem(String name)
            : base((CLRObject*)ParticleSystem_New(name))
        {
            nativePtr = (IntPtr)base.NativePtr;
            iParticle = Particle.GetInstance((IntPtr)base.NativePtr);
            UpdateTechniqueList();
            particleSystemInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// Use this to create a ParticleSystem rather than the constructor due do the limitation of passing a pointer 
        /// to the Mogre.MoveableObject from within the constructor in C#. 
        /// TODO: Fix this to use a constructor rather than a static method. 
        /// </summary>
        /// <param name="name">Name of the Particle System</param>
        /// <param name="resourceGroupName">The resource group this Particle System should belong to.</param>
        /// <returns>New ParticleSystem Object</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public ParticleSystem(String name, String resourceGroupName)
            : base((CLRObject*)ParticleSystem_New(name, resourceGroupName))
        {
            nativePtr = (IntPtr)base.NativePtr;
            iParticle = Particle.GetInstance((IntPtr)base.NativePtr);
            UpdateTechniqueList();
            particleSystemInstances.Add(nativePtr, this);
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
            ParticleSystem_Destroy(nativePtr);
            particleSystemInstances.Remove(nativePtr);
        }

        #endregion


        #region The Meat
        /// <summary>
        /// The default Keep local setting
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static bool DEFAULT_KEEP_LOCAL
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_KEEP_LOCAL();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static float DEFAULT_ITERATION_INTERVAL
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_ITERATION_INTERVAL();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static float DEFAULT_FIXED_TIMEOUT
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_FIXED_TIMEOUT();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static float DEFAULT_NON_VISIBLE_UPDATE_TIMEOUT
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_NON_VISIBLE_UPDATE_TIMEOUT();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static bool DEFAULT_SMOOTH_LOD
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_SMOOTH_LOD();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static float DEFAULT_FAST_FORWARD_TIME
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_FAST_FORWARD_TIME();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static String DEFAULT_MAIN_CAMERA_NAME
        {
            get
            {
                //try{
                return Marshal.PtrToStringAnsi(ParticleSystem_DEFAULT_MAIN_CAMERA_NAME());
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static float DEFAULT_SCALE_VELOCITY
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_SCALE_VELOCITY();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static float DEFAULT_SCALE_TIME
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_SCALE_TIME();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static Mogre.Vector3 DEFAULT_SCALE
        {
            get
            {
                //try{
                IntPtr scalePtr = ParticleSystem_DEFAULT_SCALE();
                //if (scalePtr != null && scalePtr != IntPtr.Zero)
                Mogre.Vector3 vec = *(((Mogre.Vector3*)scalePtr.ToPointer()));
                return vec;
                //    }
                //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
                //    return null;
            }
        }
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public static bool DEFAULT_TIGHT_BOUNDINGBOX
        {
            get
            {
                //try{
                return ParticleSystem_DEFAULT_TIGHT_BOUNDINGBOX();
            }
            //catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            //    return false;
            //}
        }

        /// <summary>
        /// Gets the derived position of the particle system (i.e derived from the parent node, if available).
        /// </summary>
        /// <returns>Position in the World as a Vector3.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public Mogre.Vector3 GetDerivedPosition()
        {
            Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleSystem_GetDerivedPosition(nativePtr).ToPointer()));
            return vec;
        }

        /// <summary>
        /// Gets the derived orientation of the particle system (i.e derived from the parent node, if available).
        /// </summary>
        /// <returns>Position in the World as a Quaternion.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public Mogre.Quaternion GetDerivedOrientation()
        {
            Mogre.Quaternion quat = *(((Mogre.Quaternion*)ParticleSystem_GetDerivedOrientation(nativePtr).ToPointer()));
            return quat;
        }

        /// <summary>
        ///  Gets the latest orientation of the particle system before update.
        /// </summary>
        /// <returns>The Last Orientation.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public Mogre.Quaternion GetLatestOrientation()
        {
            Mogre.Quaternion quat = *(((Mogre.Quaternion*)ParticleSystem_GetLatestOrientation(nativePtr).ToPointer()));
            return quat;
        }

        /// <summary>
        /// Returns true if the particle system has rotated between updates.
        /// </summary>
        /// <returns>Returns true if the particle system has rotated between updates.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool HasRotatedBetweenUpdates()
        {
            return ParticleSystem_HasRotatedBetweenUpdates(nativePtr);
        }

        /// <summary>
        ///  If the orientation of the particle system has been changed since the last update, the passed vector
        ///	is rotated accordingly.
        /// </summary>
        /// <param name="pos">Vector to Rotate to match Particle System Orientation.</param>
        public void RotationOffset(Mogre.Vector3 pos)
        {
            ParticleSystem_RotationOffset(nativePtr, pos);
        }

        /// <summary>
        /// Is smooth Lod Set?
        /// Enable or disable Smooth Lod.
        /// True if the Lod Set is smooth.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool SmoothLod
        {
            get { return isSmoothLod(); }
            set { SetSmoothLod(value); }
        }

        /// <summary>
        /// Is smooth Lod Set?.
        /// </summary>
        /// <returns>True if the Lod Set is smooth.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool isSmoothLod()
        {
            return ParticleSystem_IsSmoothLod(nativePtr);
        }

        /// <summary>
        /// Set smooth Lod.
        /// </summary>
        /// <param name="smoothLod">Enable or disable Smooth Lod</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetSmoothLod(bool smoothLod)
        {
            ParticleSystem_SetSmoothLod(nativePtr, smoothLod);
        }

        /// <summary>
        /// Returns the time since the ParticleSystem was started.
        /// </summary>
        /// <returns>Returns the time in seconds. Or -1 if there was a problem.</returns>
        public float GetTimeElapsedSinceStart()
        {
            try{
                return ParticleSystem_GetTimeElapsedSinceStart(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Get the name of the resource group that is assigned to this ParticleSystem.
        /// Set the name of the resource group for this ParticleSystem.
        /// </summary>
        /// <returns>The Resource Group Name this Particle System Belongs to. Or null if there is a problem.</returns>
        public String ResourceGroupName
        {
            get
            {
                try
                {
                    return Marshal.PtrToStringAnsi(ParticleSystem_GetResourceGroupName(nativePtr));
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return null;
            }
            set
            {
                ParticleSystem_SetResourceGroupName(nativePtr, value);
            }
        }

        List<ParticleTechnique> childTechniques;
        //Use this method to make sure what we have is the same as what the Particule Universe native DLL has.
        private void UpdateTechniqueList()
        {
            int  numT = ParticleSystem_GetNumTechniques(nativePtr);
            List<ParticleTechnique> childTechniquesNew = new List<ParticleTechnique>();

            for (int i = 0; i < numT; i++)
            {
                IntPtr ptr = ParticleSystem_GetTechnique(nativePtr, i);
                bool found = false;
                if (childTechniques != null && childTechniques.Count > 0)
                    foreach (ParticleTechnique part in childTechniques)
                    {
                        if (part != null && part.nativePtr == ptr)
                        {
                            childTechniquesNew.Add(part);
                            found = true;
                            break;
                        }
                    }
                if (!found)
                    childTechniquesNew.Add(ParticleTechnique.GetInstances(ptr));
            }
            childTechniques = childTechniquesNew;
        }

        /// <summary>
        /// Create a ParticleTechnique and add it to this ParticleSystem.
        /// </summary>
        /// <returns>A New empty Technique attached to this Particle System. Or null if there was a problem.</returns>
        public ParticleTechnique CreateTechnique()
        {
            if (childTechniques == null)
                childTechniques = new List<ParticleTechnique>();
            try
            {
                IntPtr ptr = ParticleSystem_CreateTechnique(nativePtr);
                if (ptr == IntPtr.Zero)
                    return null;
                ParticleTechnique t = ParticleTechnique.GetInstances(ptr);
                childTechniques.Add(t);
                return t;
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return null;
        }

        /// <summary>
        /// Add a technique to the list.
        /// <remarks>
        ///	It must be possible to add a previously Created technique to the list. This is the case with 
        ///	techniques that were Created outside the particle system. An example is the creation of 
        ///	techniques by means of a script. The technique will be placed under control of the particle 
        ///	system.	The Particle System Manager however, deletes the techniques (since they are also 
        ///	created by the Particle System Manager).
        ///	</remarks>
        /// </summary>
        /// <param name="technique">Previously Created technique to add.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if technique is null.</exception>
        public void AddTechnique(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null");

            ParticleSystem_AddTechnique(nativePtr, technique.NativePointer);
            UpdateTechniqueList();
        }

        /// <summary>
        ///  Remove a technique from the system, but don't Destroy it.
        /// </summary>
        /// <param name="technique">Technique to remove.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if technique is null.</exception>
        public void RemoveTechnique(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null");
            ParticleSystem_RemoveTechnique(nativePtr, technique.NativePointer);
            UpdateTechniqueList();
        }

        /// <summary>
        /// Get a ParticleTechnique given a certain index.
        /// </summary>
        /// <param name="index">Index of ParticleTechnique to get.</param>
        /// <returns>ParticleTechnique at index or null if not found or there was a problem..</returns>
        public ParticleTechnique GetTechnique(int index)
        {
            try
            {
                IntPtr ptr = ParticleSystem_GetTechnique(nativePtr, index);
                if (childTechniques != null)
                foreach (ParticleTechnique part in childTechniques)
                {
                    if (part != null && part.nativePtr == ptr)
                        return part;
                }
                UpdateTechniqueList();
                if (ptr != IntPtr.Zero)
                    return ParticleTechnique.GetInstances(ptr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return null;
        }

        /// <summary>
        /// Get a ParticleTechnique. Search by ParticleTechnique name.
        /// </summary>
        /// <param name="name">Name of ParticleTechnique to get.</param>
        /// <returns>ParticleTechnique at index or null if not found.</returns>
        public ParticleTechnique GetTechnique(String name)
        {
            try
            {
                IntPtr ptr = ParticleSystem_GetTechnique(nativePtr, name);
                if (childTechniques != null)
                foreach (ParticleTechnique part in childTechniques)
                {
                    if (part != null && part.nativePtr == ptr)
                        return part;
                }
                UpdateTechniqueList();
                if (ptr != IntPtr.Zero)
                    return ParticleTechnique.GetInstances(ptr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return null;
        }

        /// <summary>
        /// Get the number of ParticleTechniques of this ParticleSystem.
        /// </summary>
        /// <returns>The Number of Techniques in ParticleSystem. Or -1 if there was a problem.</returns>
        public int GetNumTechniques()
        {
            try{
            return ParticleSystem_GetNumTechniques(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Destroy a ParticleTechnique.
        /// </summary>
        /// <param name="particleTechnique">ParticleTechnique to destroy.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if particleTechnique is null.</exception>
        public void DestroyTechnique(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null");
            ParticleSystem_DestroyTechnique(nativePtr, particleTechnique.NativePointer);
            UpdateTechniqueList();
        }

        /// <summary>
        /// Destroy a ParticleTechnique identified by means of an index.
        /// </summary>
        /// <param name="index">Index of ParticleTechnique to destroy.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void DestroyTechnique(int index)
        {
            if (ParticleSystem_GetNumTechniques(nativePtr) < index)
                return;
            ParticleSystem_DestroyTechnique(nativePtr, index);
            UpdateTechniqueList();
        }

        /// <summary>
        /// Destroy all ParticleTechniques of this ParticleSystem.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void DestroyAllTechniques()
        {
            ParticleSystem_DestroyAllTechniques(nativePtr);
            childTechniques.Clear();
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <param name="isTagPoint">TODO: Not Sure?</param>
        /// <param name="parent">Parent Node to Notify.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if parent is null.</exception>
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null");
            ParticleSystem__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <param name="cam">Camera to Notify</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if cam is null.</exception>
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null");
            ParticleSystem__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <returns>The Type name of this Object. Or null if there was a problem.</returns>
        public String GetMovableType()
        {
            try{
            return Marshal.PtrToStringAnsi(ParticleSystem_GetMovableType(nativePtr));
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return null;
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <returns>The Bounding box of this Object. Or null if there was a problem.</returns>
        public Mogre.AxisAlignedBox GetBoundingBox()
        {
            try
            {
                IntPtr scalePtr = ParticleSystem_GetBoundingBox(nativePtr);
                if (scalePtr == IntPtr.Zero)
                    return null;
                Mogre.AxisAlignedBox aabtype = new Mogre.AxisAlignedBox();
                Mogre.AxisAlignedBox vec = (Mogre.AxisAlignedBox)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
                return vec;
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return null;
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <returns>The size of the bounding box. Or -1 if there was a problem.</returns>
        public float GetBoundingRadius()
        {
            try
            {
                return ParticleSystem_GetBoundingRadius(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <param name="queue">RenderQueue to Notify.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if queue is null.</exception>
        public void _updateRenderQueue(Mogre.RenderQueue queue)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null");
            ParticleSystem__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr);
        }

        /// <summary>
        /// Overridden from MovableObject
        /// <see cref="Mogre.MovableObject"/>
        /// </summary>
        /// <param name="queueId">The ID Of the Queue group to set this Particle System to.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetRenderQueueGroup(byte queueId)
        {
            ParticleSystem_SetRenderQueueGroup(nativePtr, queueId);
        }

        /// <summary>
        /// Updates the particle system based on time elapsed.
        /// <remarks>
        ///	This is called automatically every frame by OGRE.
        ///	</remarks>
        /// </summary>
        /// <param name="timeElapsed">timeElapsed The amount of time, in seconds, since the last frame.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _update(float timeElapsed)
        {
            ParticleSystem__update(nativePtr, timeElapsed);
        }

        /// <summary>
        /// Update all techniques and return the number of emitted particles for any function who is interested.
        /// </summary>
        /// <param name="timeElapsed">timeElapsed The amount of time, in seconds, since the last frame. Or -1 if there was a problem.</param>
        public int _updateTechniques(float timeElapsed)
        {
            try{
                return ParticleSystem__updateTechniques(nativePtr, timeElapsed);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Update timeout when nonvisible (0 for no timeout)
        /// </summary>
        /// <returns>Update timeout when nonvisible (0 for no timeout). Or -1 if there was a problem.</returns>
        public float GetNonVisibleUpdateTimeout()
        {
            try{
                return ParticleSystem_GetNonVisibleUpdateTimeout(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Update timeout when nonvisible (0 for no timeout)
        /// </summary>
        /// <param name="timeout">Update timeout when nonvisible (0 for no timeout)</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetNonVisibleUpdateTimeout(float timeout)
        {
            ParticleSystem_SetNonVisibleUpdateTimeout(nativePtr, timeout);
        }

        /// <summary>
        /// Prepares the particle system.
        /// <remarks>
        ///	This optional state can be used to prepare the particle system, so that initial actions are
        ///	preformed before the particle system is started.
        ///	</remarks>
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Prepare()
        {
            ParticleSystem_Prepare(nativePtr);
        }

        /// <summary>
        ///  Starts the particle system.
        /// <remarks>
        ///	Only if a particle system has been attached to a SceneNode it can be started.
        ///	</remarks>
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Start()
        {
            //try{
            ParticleSystem_Start(nativePtr);
            //}
            //catch (Exception e)
            //{
            //    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
            //    lastError = e;
            //}
        }

        /// <summary>
        /// Starts the particle system and stops after a period of time.
        /// </summary>
        /// <param name="stopTime">Length of time for Particle System to run before stopping.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Start(float stopTime)
        {
            ParticleSystem_Start(nativePtr, stopTime);
        }

        /// <summary>
        /// Convenient function to start and stop (gradually) in one step.
        /// <remarks>
        ///	This function uses the stopFade() funtion; <see cref="stopFade()"/>.
        ///	</remarks>
        /// </summary>
        /// <param name="stopTime">Length of time for Particle System to run before stopping.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void StartAndStopFade(float stopTime)
        {
            ParticleSystem_StartAndStopFade(nativePtr, stopTime);
        }

        /// <summary>
        /// Stops the particle system.
        /// <remarks>
        ///	Only if a particle system has been attached to a SceneNode and started it can be stopped.
        ///	</remarks>
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Stop()
        {
            ParticleSystem_Stop(nativePtr);
        }


        /// <summary>
        /// Stops the particle system after a period of time.
        /// <remarks>
        ///	This is basicly the same as calling SetFixedTimeout().
        ///	</remarks>
        /// </summary>
        /// <param name="stopTime">Length of time for Particle System to run before stopping.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Stop(float stopTime)
        {
            ParticleSystem_Stop(nativePtr, stopTime);
        }

        /// <summary>
        /// Stops emission of all particle and really stops the particle system when all particles are expired.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void StopFade()
        {
            ParticleSystem_StopFade(nativePtr);
        }

        /// <summary>
        /// Stops emission of all particle after a period of time and really stops the particle system when all particles are expired.
        /// </summary>
        /// <param name="stopTime">Length of time for Particle System to run before stopping.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void StopFade(float stopTime)
        {
            ParticleSystem_StopFade(nativePtr, stopTime);
        }

        /// <summary>
        /// Pauses the particle system.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Pause()
        {
            ParticleSystem_Pause(nativePtr);
        }

        /// <summary>
        /// Pauses the particle system for a period of time. After this time, the Particle System automatically resumes.
        /// </summary>
        /// <returns>Length of time to pause particle system before restarting.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Pause(float pauseTime)
        {
            ParticleSystem_Pause(nativePtr, pauseTime);
        }

        /// <summary>
        /// Resumes the particle system.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void Resume()
        {
            ParticleSystem_Resume(nativePtr);
        }

        /// <summary>
        /// Returns the state of the particle system.
        /// </summary>
        /// <returns>State of the particle system.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public ParticleSystemState GetState()
        {
            int val = ParticleSystem_GetState(nativePtr);
            switch (val)
            {
                case (int)ParticleSystemState.PSS_PAUSED:
                    return ParticleSystemState.PSS_PAUSED;
                case (int)ParticleSystemState.PSS_PREPARED:
                    return ParticleSystemState.PSS_PREPARED;
                case (int)ParticleSystemState.PSS_STARTED:
                    return ParticleSystemState.PSS_STARTED;
                case (int)ParticleSystemState.PSS_STOPPED:
                    return ParticleSystemState.PSS_STOPPED;
            }
            return ParticleSystemState.PSS_STOPPED;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The fast forward time or -1 if there was a problem.</returns>
        public float GetFastForwardTime()
        {
            try
            {
                return ParticleSystem_GetFastForwardTime(nativePtr);
            }

            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The Time Step the Particle System Fast Forwards in. Or -1 if there was a problem.</returns>
        public float GetFastForwardInterval()
        {
            try{
                return ParticleSystem_GetFastForwardInterval(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="time"></param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetFastForward(float time, float interval)
        {
            ParticleSystem_SetFastForward(nativePtr, time, interval);
        }

        /// <summary>
        ///  Get the main camera (name).
        /// Set the main camera (Object).
        /// <remarks>
        ///	By Setting the main camera, the number of cameras for which un update is performed,
        ///	is narrowed. Particle System LOD for example only really works with a 1-camera Setup, 
        ///	so Setting a camera (Object) basicly prevents that LODding isn't screwed up if the scene 
        ///	contains multiple cameras.
        ///	</remarks>
        /// </summary>
        /// <returns>The name of the Camera. Or null if there was a problem.</returns>
        public String MainCameraName
        {
            get
            {
                try
                {
                    return Marshal.PtrToStringAnsi(ParticleSystem_GetMainCameraName(nativePtr));
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return null;
            }
            set
            {
                ParticleSystem_SetMainCameraName(nativePtr, value);
            }
        }

        /// <summary>
        ///  Get the main camera (Object).
        /// </summary>
        /// <returns>The Camera Object. Or null if there was a problem.</returns>
        public Mogre.Camera MainCamera
        {
            get
            {
                try
                {
                    IntPtr scalePtr = ParticleSystem_GetMainCamera(nativePtr);
                    if (scalePtr == IntPtr.Zero)
                        return null;
                    Type type = typeof(Mogre.Camera);
                    Mogre.Camera retVal = (Mogre.Camera)(Marshal.PtrToStructure(scalePtr, type));
                    return retVal;
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return null;
            }
            set
            {
                if (value == null)
                    ParticleSystem_SetMainCamera(nativePtr, IntPtr.Zero);
                else
                    ParticleSystem_SetMainCamera(nativePtr, (IntPtr)value.NativePtr);
            }
        }

        /// <summary>
        ///  True if the main camera has been Set.
        /// </summary>
        /// <returns>True if the main camera has been Set.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool HasMainCamera()
        {
            return ParticleSystem_HasMainCamera(nativePtr);
        }

        /// <summary>
        /// Returns the current camera (the one that Gets updated/notified).
        /// </summary>
        /// <returns>Returns the current camera (the one that Gets updated/notified). Or null if there wa a problem.</returns>
        public Mogre.Camera GetCurrentCamera()
        {
            try
            {
                IntPtr scalePtr = ParticleSystem_GetCurrentCamera(nativePtr);
                if (scalePtr == IntPtr.Zero)
                    return null;
                Type type = typeof(Mogre.Camera);
                Mogre.Camera retVal = (Mogre.Camera)(Marshal.PtrToStructure(scalePtr, type));
                return retVal;
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void FastForward()
        {
            ParticleSystem_FastForward(nativePtr);
        }


        /// <summary>
        /// Determines whether the parent is a TagPoint or a SceneNode.
        /// </summary>
        /// <returns>True if a TagPoint?</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool ParentIsTagPoint()
        {
            return ParticleSystem_IsParentIsTagPoint(nativePtr);
        }

        /// <summary>
        /// Returns the distances at which level-of-detail (LOD) levels come into effect.
        /// </summary>
        /// <returns>List of Lod Values.</returns>
        public float[] GetLodDistances()
        {
            try
            {
                int bufSize = ParticleSystem_GetLodDistancesCount(nativePtr);
                if (bufSize <= 0)
                    return null;
                float[] floats = new float[bufSize];
                ParticleSystem_GetLodDistances(nativePtr, floats, bufSize);
                return floats;
            }
            catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /// <summary>
        /// Clears the list with distances.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void ClearLodDistances()
        {
            ParticleSystem_ClearLodDistances(nativePtr);
        }

        /// <summary>
        /// Adds a distance at which level-of-detail (LOD) levels come into effect.
        /// The Lod Distance is added as distance * distance (So when you get lod distances they won't be the same as what you put in.)
        /// </summary>
        /// <param name="distance">Lod level distance to add.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void AddLodDistance(float distance)
        {
            ParticleSystem_AddLodDistance(nativePtr, distance);
        }

        /// <summary>
        /// Sets the distance at which level-of-detail (LOD) levels come into effect.
        /// <remarks>
        ///	You should only use this if you have assigned LOD indexes to the ParticleTechnique
        ///	instances attached to this ParticleSystem.
        ///	</remarks>
        /// </summary>
        /// <param name="lodDistances">An array of floats which indicate the distance at which to switch to 
        ///	another ParticleTechnique.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetLodDistances(float[] lodDistances)
        {
            if (lodDistances == null)
                throw new ArgumentNullException("lodDistances cannot be null!");
            ParticleSystem_SetLodDistances(nativePtr, lodDistances);
        }

        /// <summary>
        /// Returns the number of emitted ParticleTechniques.
        /// <remarks>
        ///	Emitted ParticleTechniques are determined by means of the _markForEmission() function.
        ///	</remarks>
        /// </summary>
        /// <returns>The number of emitted ParticleTechniques. Or -1 if there was a problem.</returns>
        public int GetNumEmittedTechniques()
        {
            try
            {
                return ParticleSystem_GetNumEmittedTechniques(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Mark all emitted objects (ParticleTechniques and all the objects registered 
        ///	at each ParticleTechnique).
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _markForEmission()
        {
            ParticleSystem__markForEmission(nativePtr);
        }

        /// <summary>
        /// Reset mark for emission indication for all techniques.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _resetMarkForEmission()
        {
            ParticleSystem__resetMarkForEmission(nativePtr);
        }

        /// <summary>
        /// Function to suppress notification of an emission change.
        /// <remarks>
        ///	This function is typically used when notification isn´t needed anymore. An example for this 
        ///	situation is for instance in case the ParticleTechnique is Destroyed, which also causes the
        ///	emitters to be Destroyed.
        ///	</remarks>
        /// </summary>
        /// <param name="suppress">true to suppress notification of an emission change.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SuppressNotifyEmissionChange(bool suppress)
        {
            ParticleSystem_SuppressNotifyEmissionChange(nativePtr, suppress);
        }

        /// <summary>
        /// Is called as soon as a new technique is added or deleted, which leads to a re-evaluation of the 
        ///	emitted objects.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _notifyEmissionChange()
        {
            ParticleSystem__notifyEmissionChange(nativePtr);
        }

        /// <summary>
        /// Value that determines in which intervals the system must be the updated.
        /// </summary>
        /// <returns>Value that determines in which intervals the system must be the updated. Or -1 if there was a problem.</returns>
        public float IterationInterval
        {
            get
            {
                try
                {
                    return ParticleSystem_GetIterationInterval(nativePtr);
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return -1;
            }
            set
            {
                ParticleSystem_SetIterationInterval(nativePtr, value);
            }
        }

        /// <summary>
        /// If set, the ParticleSystem automatically stops after set values seconds.
        /// </summary>
        /// <returns>Length of time in Seconds before ParticleSystem automatically stops. Or -1 if there was a problem.</returns>
        public float FixedTimeout
        {
            get
            {
                try
                {
                    return ParticleSystem_GetFixedTimeout(nativePtr);
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return -1;
            }
            set
            {
                ParticleSystem_SetFixedTimeout(nativePtr, value);
            }
        }

        /// <summary>
        /// Sets whether the bounds will be automatically updated for the life of the particle system.
        /// </summary>
        /// <param name="autoUpdate">Determines whether the recalculation of the bounding box is performed automatically.</param>
        /// <param name="stopIn">Time that indicates how long the AABB must be calculated before it is fixed.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetBoundsAutoUpdated(bool autoUpdate, float stopIn = 0.0f)
        {
            ParticleSystem_SetBoundsAutoUpdated(nativePtr, autoUpdate, stopIn);
        }

        /// <summary>
        /// Reset the bounding box of the particle system to the most minimal value.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _resetBounds()
        {
            ParticleSystem__resetBounds(nativePtr);
        }

        /// <summary>
        /// Scale the Particle Systems´ relative positions and size.
        /// Set the scale independant from the node to which it is attached.
        /// <remarks>Scaling is done on a Particle System level and is independent of the scaling of the SceneNode</remarks>
        /// </summary>
        /// <returns>Particle Systems´ relative positions and size.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public Mogre.Vector3 Scale
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)ParticleSystem_GetScale(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                ParticleSystem_SetScale(nativePtr, ref value);
            }
        }

        /// <summary>
        /// Notify registered ParticleTechniques.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _notifyRescaled()
        {
            ParticleSystem__notifyRescaled(nativePtr);
        }

        /// <summary>
        /// Returns the velocity scale.
        /// Set the velocity scale.
        /// Scaling of the particle velocity is independant from scaling the positions and size.
        /// </summary>
        /// <returns>Scaling of the particle velocity is independant from scaling the positions and size. Or -1 if there was a problem.</returns>
        public float ScaleVelocity
        {
            get
            {
                try
                {
                    return ParticleSystem_GetScaleVelocity(nativePtr);
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return -1;
            }
            set
            {
                ParticleSystem_SetScaleVelocity(nativePtr, value);
            }
        }

        /// <summary>
        /// Notify registered ParticleTechniques.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _notifyVelocityRescaled()
        {
            ParticleSystem__notifyRescaled(nativePtr);
        }

        /// <summary>
        ///  Get the time scale.
        /// Set the time scale. 
        /// Makes the Particle System speed up or slow down.
        /// </summary>
        /// <returns>Particle System speed. Or -1 if there was a problem.</returns>
        public float ScaleTime
        {
            get
            {
                try
                {
                    return ParticleSystem_GetScaleTime(nativePtr);
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return -1;
            }
            set
            {
                ParticleSystem_SetScaleTime(nativePtr, value);
            }
        }

        /// <summary>
        /// Perform initialising activities as soon as the particle is emitted.
        /// </summary>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _initForEmission()
        {
            ParticleSystem__initForEmission(nativePtr);
        }

        /// <summary>
        /// Perform some action if the particle expires.
        /// <remarks>
        /// Note, that this function applies to all particle types (including Particle Techniques, Emitters and
        ///	Affectors).</remarks>
        /// </summary>
        /// <param name="technique">ParticleTechnique to call on Event.</param>
        /// <param name="timeElapsed">Duration.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _initForExpiration(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleSystem__initForExpiration(nativePtr, technique.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Perform actions on the particle itself during the update loop of a ParticleTechnique.
        /// <remarks>
        ///	Active particles may want to do some processing themselves each time the ParticleTechnique is updated.
        ///	One example is to perform actions by means of the registered ParticleBehaviour objects. 
        ///	ParticleBehaviour objects apply internal behaviour of each particle individually. They add both 
        ///	data and behaviour to a particle, which means that each particle can be extended with functionality.</remarks>
        /// </summary>
        /// <param name="technique">ParticleTechnique to call on Event.</param>
        /// <param name="timeElapsed">Duration.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void _process(ParticleTechnique technique, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleSystem__process(nativePtr, technique.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Determines whether particle positions should be kept local in relation to the system.
        /// If this attribute is Set to 'true', the particles are emitted relative to the system 
        /// </summary>
        /// <returns>True if particle positions should be kept local in relation to the system.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool KeepLocal
        {
            get
            {
                return ParticleSystem_IsKeepLocal(nativePtr);
            }
            set
            {
                ParticleSystem_SetKeepLocal(nativePtr, value);
            }
        }

        /// <summary>
        /// Transforms the particle position in a local position relative to the system
        /// </summary>
        /// <param name="particle">Particle to make local.</param>
        /// <returns>False if particle is expired or Particle System not set to keep local.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool MakeParticleLocal(IParticle particle)
        {
            if (particle == null)
                return false;
            return ParticleSystem_MakeParticleLocal(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Is 'true' when the bounding box is wrapped tight around the particle system or 'false' when the
        ///	bounding box is only increasing and doesn´t shrink when the particle system shrinks.
        /// Determines whether the bounding box is tight around the particle system or whether the bounding
        ///    is growing and never shrinks.
        /// </summary>
        /// <returns>Is 'true' when the bounding box is wrapped tight around the particle system or 'false' when the
        ///	bounding box is only increasing and doesn´t shrink when the particle system shrinks.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public bool TightBoundingBox
        {
            get
            {
                return ParticleSystem_HasTightBoundingBox(nativePtr);
            }
            set
            {
                ParticleSystem_SetTightBoundingBox(nativePtr, value);
            }
        }

        /// <summary>
        /// Add a ParticleSystemListener, which Gets called as soon as a ParticleSystem is started or stopped.
        /// </summary>
        /// <param name="particleSystemListener">ParticleSystemListener to notify on start and stop.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        /// <exception cref="ArgumentNullException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void AddParticleSystemListener(ParticleSystemListener particleSystemListener)
        {
            if (particleSystemListener == null)
                throw new ArgumentNullException("particleSystemListener cannot be null");
            ParticleSystem_AddParticleSystemListener(nativePtr, particleSystemListener.NativePointer);
        }

        /// <summary>
        /// Removes the ParticleSystemListener, but it the ParticleSystemListener isn't Destroyed.
        /// </summary>
        /// <param name="particleSystemListener">ParticleSystemListener to remove.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void RemoveParticleSystemListener(ParticleSystemListener particleSystemListener)
        {
            if (particleSystemListener == null)
                return;
            ParticleSystem_RemoveParticleSystemListener(nativePtr, particleSystemListener.NativePointer);
        }

        /// <summary>
        /// This function is called as soon as an event withing the Particle System occurs.
        /// </summary>
        /// <param name="particleUniverseEvent"></param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void PushEvent(ParticleUniverseEvent particleUniverseEvent)
        {
            if (particleUniverseEvent == null)
                throw new ArgumentNullException("particleSystemListener cannot be null");
            ParticleSystem_PushEvent(nativePtr, particleUniverseEvent.NativePointer);
        }

        //** @see MovableObject
        //*/
        //Not Implemented
        //public void VisitRenderables (Mogre.Renderable.Visitor visitor,
        //    bool debugRenderables = false)
        //{
        //}

        /// <summary>
        /// Returns the time of a pause (if Set)
        /// Set the pause time. This is used to pause the ParticleSystem for a certain amount of time.
        /// </summary>
        /// <returns>How long to pause. Or -1 if there was a problem.</returns>
        public float PauseTime
        {
            get
            {
                try
                {
                    return ParticleSystem_GetPauseTime(nativePtr);
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return -1;
            }
            set
            {
                ParticleSystem_SetPauseTime(nativePtr, value);
            }
        }

        /// <summary>
        ///  Returns the name of the template which acted as a blueprint for creation of this Particle System.
        /// Set the name of the template which acts as a blueprint for creation of this Particle System.
        /// </summary>
        /// <returns>Particle System Template Name. Or null if there was a problem.</returns>
        public String TemplateName
        {
            get
            {
                try
                {
                    return Marshal.PtrToStringAnsi(ParticleSystem_GetTemplateName(nativePtr));
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return null;
            }
            set
            {
                ParticleSystem_SetTemplateName(nativePtr, value);
            }
        }

        /// <summary>
        /// Set to True to enable this Particle System.
        /// </summary>
        /// <param name="enabled">Set to True to enable this Particle System.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetEnabled(bool enabled)
        {
            ParticleSystem_SetEnabled(nativePtr, enabled);
        }

        /// <summary>
        /// Get the SceneManager with which the ParticleSystem is Created
        /// Set the SceneManager with which the ParticleSystem is Created
        /// </summary>
        /// <returns>The SceneManager that created this Particle System. Or null if there was a problem.</returns>
        public Mogre.SceneManager SceneManager
        {
            get
            {
                try
                {
                    IntPtr scalePtr = ParticleSystem_GetSceneManager(nativePtr);
                    if (scalePtr == IntPtr.Zero)
                        return null;
                    Type type = typeof(Mogre.SceneManager);
                    Mogre.SceneManager retVal = (Mogre.SceneManager)(Marshal.PtrToStructure(scalePtr, type));
                    return retVal;
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return null;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("SceneManager cannot be null");
                ParticleSystem_SetSceneManager(nativePtr, (IntPtr)value.NativePtr);
            }
        }

        /// <summary>
        /// Set the indication to false if you want to update the particle system yourself.
        /// </summary>
        /// <param name="useController">Set the indication to false if you want to update the particle system yourself.</param>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws if there was a problem talking to the Particle Universe DLL.</exception>
        public void SetUseController(bool useController)
        {
            ParticleSystem_SetUseController(nativePtr, useController);
        }

        /// <summary>
        /// Returns 'true' if one of externs is of a certain type.
        ///
        ///	All techniques are searched.
        /// </summary>
        /// <param name="externType">Extern Type to Search for.</param>
        /// <returns>Returns 'true' if one of externs is of a certain type.</returns>
        /// <exception cref="System.Runtime.InteropServices.SEHException">Throws an Exception if there is a problem accessing the DLL.</exception>
        public bool HasExternType(String externType)
        {
            if (externType == null)
                return false;
            return ParticleSystem_HasExternType(nativePtr, externType);
        }

        /// <summary>
        /// Return number of all emitted particles.
        /// </summary>
        /// <returns>The number of all emitted particles. Or -1 if there was a problem.</returns>
        public int GetNumberOfEmittedParticles()
        {
            try{
                return ParticleSystem_GetNumberOfEmittedParticles(nativePtr);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Return number of emitted particles of a certain type
        /// </summary>
        /// <param name="particleType">Particle Type to count</param>
        /// <returns>The number of emitted particles of the specified type. Or -1 if there was a problem.</returns>
        public int GetNumberOfEmittedParticles(Particle.ParticleType particleType)
        {
            try{
            return ParticleSystem_GetNumberOfEmittedParticles(nativePtr, particleType);
            }
            catch (Exception e)
            {
                if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                lastError = e;
            }
            return -1;
        }

        /// <summary>
        /// Used to define to which category the particle system belongs.
        ///	This is pure for administration and can be used in an editor. There is no technical reason to set this attribute.
        /// </summary>
        /// <returns>Category Name or Null if there was a problem.</returns>
        public String Category
        {
            get
            {
                try
                {
                    return Marshal.PtrToStringAnsi(ParticleSystem_GetCategory(nativePtr));
                }
                catch (Exception e)
                {
                    if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                    lastError = e;
                }
                return null;
            }
            set
            {
                ParticleSystem_SetCategory(nativePtr, value);
            }
        }
        #endregion

        #region IParticle Implementation
        /// <summary>
        /// Emitter that has emitted the particle.
        /// <remarks>Since the particle can be reused by several emitters, this values can change.</remarks>
        /// </summary>
        public ParticleEmitter ParentEmitter {
            get
            {
                try
                {
                    return iParticle.ParentEmitter;
                }
                catch (System.AccessViolationException) //The Particle System doesn't have a Parent Emitter!
                {
                    return null;
                }
            }
            set { iParticle.ParentEmitter = value; } 
        }

        /// <summary>Position.
        /// <remarks>Unlike Ogre's particle plugin, the ParticleUniverse plugin doesn't distinguish between local and worldspace.</remarks>
        /// </summary>
        public Mogre.Vector3 Position { get { return iParticle.Position; } set { iParticle.Position = value; } }

        /// <summary>
        /// Direction (and speed)
        /// </summary>
        public Mogre.Vector3 Direction { get { return iParticle.Direction; } set { iParticle.Direction = value; } }

        /// <summary>
        /// Mass of a particle.
        /// <remarks>In case of simulations where mass of a particle is needed (i.e. exploding particles of different mass) this attribute can be used.</remarks>
        /// </summary>
        public float Mass { get { return iParticle.Mass; } set { iParticle.Mass = value; } }

        /// <summary>
        /// Time to live, number of seconds left of particles natural life
        /// </summary>
        public float TimeToLive { get { return iParticle.TimeToLive; } set { iParticle.TimeToLive = value; } }

        /// <summary>
        /// Total Time to live, number of seconds of particles natural life
        /// </summary>
        public float TotalTimeToLive { get { return iParticle.TotalTimeToLive; } set { iParticle.TotalTimeToLive = value; } }

        /// <summary>
        /// The timeFraction is calculated every update. It is used in other observers, affectors, etc. so it is
        /// better to calculate it once at the Particle level.
        /// </summary>
        public float TimeFraction { get { return iParticle.TimeFraction; } set { iParticle.TimeFraction = value; } }

        /// <summary>
        /// Determine type of particle, to prevent Realtime type checking
        /// </summary>
        public Particle.ParticleType Type { get { return iParticle.Type; } set { iParticle.Type = value; } }

        /// <summary>
        /// Keep the posibility to attach some custom data. This is additional to the Behaviour data. The
        /// advantage of a UserDefinedObject in favour of a ParticleBehaviour is, that no search is
        /// needed.
        /// <remarks>The UserDefinedObject is not managed by the Particle itself, so assigned UserDefinedObjects must
        ///	be deleted outside the Particle.</remarks>
        /// </summary>
        public object UserDefinedObject { get { return iParticle.UserDefinedObject; } set { iParticle.UserDefinedObject = value; } }

        /// <summary>
        ///If a physics engine is used, this attribute is set as soon as a particle is emitted.
        /// </summary>
        public PhysicsActor PhysicsActor { get { return iParticle.PhysicsActor; } set { iParticle.PhysicsActor = value; } }

        /// <summary>
        /// For some renderers it is needed to relate a particle to some visual data
        /// <remarks>The visual data is set into the Particle instead of the VisualParticle, to enable other
        ///	particle types do use visual data (if needed). The IVisualData is not managed by the Particle 
        ///	itself, so assigned IVisualData must be deleted outside the Particle.</remarks>
        /// </summary>
        public IVisualData VisualData { get { return iParticle.VisualData; } set { iParticle.VisualData = value; } }

        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        public Mogre.Vector3 OriginalPosition { get { return iParticle.OriginalPosition; } set { iParticle.OriginalPosition = value; } }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        public Mogre.Vector3 OriginalDirection { get { return iParticle.OriginalDirection; } set { iParticle.OriginalDirection = value; } }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// </summary>
        public float OriginalVelocity { get { return iParticle.OriginalVelocity; } set { iParticle.OriginalVelocity = value; } }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// Length of the direction that has been set
        /// </summary>
        public float OriginalDirectionLength { get { return iParticle.OriginalDirectionLength; } set { iParticle.OriginalDirectionLength = value; } }
        /// <summary>
        /// Value that are assigned as soon as the particle is emitted (non-transformed)
        /// Length of the direction after multiplication with the velocity
        /// </summary>
        public float OriginalScaledDirectionLength { get { return iParticle.OriginalScaledDirectionLength; } set { iParticle.OriginalScaledDirectionLength = value; } }

        /// <summary>
        /// Keeps latest position
        /// </summary>
        public Mogre.Vector3 LatestPosition { get { return iParticle.LatestPosition; } set { iParticle.LatestPosition = value; } }

        /// <summary>
        /// Todo
        /// </summary>
        public bool _IsMarkedForEmission { get { return iParticle._IsMarkedForEmission; } set { iParticle._IsMarkedForEmission = value; } }

        //bool _isMarkedForEmission();
        //void _setMarkedForEmission(bool markedForEmission);

        ///// <summary>
        ///// Perform initialising activities as soon as the particle is emitted.
        ///// </summary>
        //public void _initForEmission()
        //{
        //    iParticle._initForEmission();
        //}

        ///// <summary>
        ///// Perform some action if the particle expires.
        ///// <remarks>Note, that this function applies to all particle types (including Particle Techniques, Emitters and
        /////	Affectors).</remarks>
        ///// </summary>
        //public void _initForExpiration(ParticleTechnique technique, float timeElapsed)
        //{
        //    iParticle._initForExpiration(technique, timeElapsed);
        //}

        /// <summary>
        /// Sets weither or not this Particle is enabled.
        /// </summary>
        public bool Enabled { get { return iParticle.Enabled; } set { iParticle.Enabled = value; } }

        //bool isEnabled();
        //void SetEnabled(bool enabled);

        /// <summary>
        /// This function sets the original 'enabled' value of the particle.
        /// Returns the original 'enabled' value of the particle
        /// <remarks>Only use this function if you really know what you're doing. Otherwise it shouldn't be used for regular usage.</remarks>
        /// </summary>
        public bool _OriginalEnabled { get { return iParticle._OriginalEnabled; } set { iParticle._OriginalEnabled = value; } }

        //void _setOriginalEnabled(bool originalEnabled);
        //bool _getOriginalEnabled();


        /// <summary>
        /// Freeze the particle, so it doesn't move anymore.
        /// Returns true if the particle is freezed and doesn't move anymore.
        /// <remarks>Although it is freezed, repositioning the particle is still possible.</remarks>
        /// </summary>
        public bool Freezed { get { return iParticle.Freezed; } set { iParticle.Freezed = value; } }

        //bool isFreezed();
        //void SetFreezed(bool freezed);

        /// <summary>
        /// Sets the event flags.
        /// </summary>
        public void SetEventFlags(uint flags)
        {
            iParticle.SetEventFlags(flags);
        }

        /// <summary>
        /// As setEventFlags, except the flags passed as parameters are appended to the
        ///	existing flags on this object.
        /// </summary>
        public void AddEventFlags(uint flags)
        {
            iParticle.AddEventFlags(flags);
        }

        /// <summary>
        /// The flags passed as parameters are removed from the existing flags.
        /// </summary>
        public void RemoveEventFlags(uint flags)
        {
            iParticle.RemoveEventFlags(flags);
        }

        /// <summary>
        /// Return the event flags.
        /// </summary>
        public uint GetEventFlags()
        {
            return iParticle.GetEventFlags();
        }

        /// <summary>
        /// Determines whether it has certain flags set.
        /// </summary>
        public bool HasEventFlags(uint flags)
        {
            return iParticle.HasEventFlags(flags);
        }

        /// <summary>
        /// Copy a vector of ParticleBehaviour objects to this particle.
        /// </summary>
        public void CopyBehaviours(ParticleBehaviour[] behaviours)
        {
            iParticle.CopyBehaviours(behaviours);
        }


        ///// <summary>
        ///// Perform actions on the particle itself during the update loop of a ParticleTechnique.
        ///// <remarks>Active particles may want to do some processing themselves each time the ParticleTechnique is updated.
        /////	One example is to perform actions by means of the registered ParticleBehaviour objects. 
        /////	ParticleBehaviour objects apply internal behaviour of each particle individually. They add both 
        /////	data and behaviour to a particle, which means that each particle can be extended with functionality.</remarks>
        ///// </summary>
        //public void _process(ParticleTechnique technique, float timeElapsed)
        //{
        //    iParticle._process(technique, timeElapsed);
        //}

        /// <summary>
        /// Returns the first occurence of the ParticleBehaviour specified by its type.
        /// </summary>
        public ParticleBehaviour GetBehaviour(String behaviourType)
        {
            return iParticle.GetBehaviour(behaviourType);
        }

        /// <summary>
        /// Calculates the velocity, based on the direction vector.
        /// </summary>
        public float CalculateVelocity()
        {
            return iParticle.CalculateVelocity();
        }

        /// <summary>
        /// Copy the data of this particle to another particle.
        /// </summary>
        public void CopyAttributesTo(IParticle particle)
        {
            iParticle.CopyAttributesTo(particle);
        }
        #endregion

        #region P/Invoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_KEEP_LOCAL", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_DEFAULT_KEEP_LOCAL();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_ITERATION_INTERVAL", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_DEFAULT_ITERATION_INTERVAL();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_FIXED_TIMEOUT", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_DEFAULT_FIXED_TIMEOUT();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_NON_VISIBLE_UPDATE_TIMEOUT", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_DEFAULT_NON_VISIBLE_UPDATE_TIMEOUT();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_SMOOTH_LOD", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_DEFAULT_SMOOTH_LOD();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_FAST_FORWARD_TIME", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_DEFAULT_FAST_FORWARD_TIME();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_MAIN_CAMERA_NAME", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_DEFAULT_MAIN_CAMERA_NAME();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_SCALE_VELOCITY", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_DEFAULT_SCALE_VELOCITY();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_SCALE_TIME", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_DEFAULT_SCALE_TIME();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_SCALE", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_DEFAULT_SCALE();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DEFAULT_TIGHT_BOUNDINGBOX", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_DEFAULT_TIGHT_BOUNDINGBOX();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_New([MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_New([MarshalAs(UnmanagedType.LPStr)]string name, [MarshalAs(UnmanagedType.LPStr)]string resourceGroupName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetDerivedPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetDerivedPosition(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetDerivedOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetDerivedOrientation(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetLatestOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetLatestOrientation(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_HasRotatedBetweenUpdates", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_HasRotatedBetweenUpdates(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_RotationOffset", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_RotationOffset(IntPtr ptr, Mogre.Vector3 pos);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_IsSmoothLod", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_IsSmoothLod(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetSmoothLod", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetSmoothLod(IntPtr ptr, bool smoothLod);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetTimeElapsedSinceStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetTimeElapsedSinceStart(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetResourceGroupName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetResourceGroupName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetResourceGroupName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetResourceGroupName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string resourceGroupName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_CreateTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_CreateTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_AddTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_AddTechnique(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_RemoveTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_RemoveTechnique(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetTechnique(IntPtr ptr, int index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetTechnique2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetTechnique(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetNumTechniques", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem_GetNumTechniques(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DestroyTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_DestroyTechnique(IntPtr ptr, IntPtr particleTechnique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DestroyTechnique2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_DestroyTechnique(IntPtr ptr, int index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_DestroyAllTechniques", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_DestroyAllTechniques(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__notifyCurrentCamera(IntPtr ptr, IntPtr cam);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetMovableType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetMovableType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetBoundingBox(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetBoundingRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetBoundingRadius(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__updateRenderQueue(IntPtr ptr, IntPtr queue);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetRenderQueueGroup(IntPtr ptr, byte queueId);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__update", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__update(IntPtr ptr, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__updateTechniques", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem__updateTechniques(IntPtr ptr, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetNonVisibleUpdateTimeout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetNonVisibleUpdateTimeout(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetNonVisibleUpdateTimeout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetNonVisibleUpdateTimeout(IntPtr ptr, float timeout);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Prepare(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Start", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Start(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Start2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Start(IntPtr ptr, float stopTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_StartAndStopFade", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_StartAndStopFade(IntPtr ptr, float stopTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Stop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Stop(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Stop2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Stop(IntPtr ptr, float stopTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_StopFade", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_StopFade(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_StopFade2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_StopFade(IntPtr ptr, float stopTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Pause", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Pause(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Pause2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Pause(IntPtr ptr, float pauseTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_Resume", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_Resume(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetState", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem_GetState(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetFastForwardTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetFastForwardTime(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetFastForwardInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetFastForwardInterval(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetFastForward", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetFastForward(IntPtr ptr, float time, float interval);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetMainCameraName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetMainCameraName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetMainCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetMainCamera(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_HasMainCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_HasMainCamera(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetMainCameraName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetMainCameraName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string cameraName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetMainCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetMainCamera(IntPtr ptr, IntPtr camera);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetCurrentCamera(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_FastForward", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_FastForward(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_IsParentIsTagPoint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_IsParentIsTagPoint(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetLodDistances", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_GetLodDistances(IntPtr ptr, [In, Out] [MarshalAs(UnmanagedType.LPArray)] float[] arrLodDistances, int bufSize);

        //[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetLodDistances2", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern IntPtr ParticleSystem_GetLodDistances(IntPtr ptr);
        
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetLodDistancesCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem_GetLodDistancesCount(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_ClearLodDistances", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_ClearLodDistances(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_AddLodDistance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_AddLodDistance(IntPtr ptr, float distance);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetLodDistances", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetLodDistances(IntPtr ptr, float[] argv);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetNumEmittedTechniques", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem_GetNumEmittedTechniques(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__markForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__markForEmission(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__resetMarkForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__resetMarkForEmission(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SuppressNotifyEmissionChange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SuppressNotifyEmissionChange(IntPtr ptr, bool suppress);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__notifyEmissionChange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__notifyEmissionChange(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetIterationInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetIterationInterval(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetIterationInterval", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetIterationInterval(IntPtr ptr, float iterationInterval);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetFixedTimeout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetFixedTimeout(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetFixedTimeout", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetFixedTimeout(IntPtr ptr, float fixedTimeout);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetBoundsAutoUpdated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetBoundsAutoUpdated(IntPtr ptr, bool autoUpdate, float stopIn = 0.0f);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__resetBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__resetBounds(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetScale(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetScale(IntPtr ptr, ref Mogre.Vector3 scale);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__notifyRescaled(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetScaleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetScaleVelocity(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetScaleVelocity", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetScaleVelocity(IntPtr ptr, float scaleVelocity);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__notifyVelocityRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__notifyVelocityRescaled(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetScaleTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetScaleTime(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetScaleTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetScaleTime(IntPtr ptr, float scaleTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__initForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__initForEmission(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__initForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__initForExpiration(IntPtr ptr, IntPtr technique, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem__process", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem__process(IntPtr ptr, IntPtr technique, float timeElapsed);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_IsKeepLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_IsKeepLocal(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetKeepLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetKeepLocal(IntPtr ptr, bool keepLocal);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_MakeParticleLocal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_MakeParticleLocal(IntPtr ptr, IntPtr particle);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_HasTightBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_HasTightBoundingBox(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetTightBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetTightBoundingBox(IntPtr ptr, bool tightBoundingBox);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_AddParticleSystemListener", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_AddParticleSystemListener(IntPtr ptr, IntPtr particleSystemListener);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_RemoveParticleSystemListener", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_RemoveParticleSystemListener(IntPtr ptr, IntPtr particleSystemListener);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_PushEvent", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_PushEvent(IntPtr ptr, IntPtr particleUniverseEvent);

        //[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_VisitRenderables", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern void ParticleSystem_VisitRenderables (IntPtr ptr, IntPtr  visitor, float getPauseTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetPauseTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleSystem_GetPauseTime(IntPtr ptr);


        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetPauseTime", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetPauseTime(IntPtr ptr, float pauseTime);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetTemplateName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetTemplateName(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetTemplateName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetTemplateName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string templateName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetEnabled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetEnabled(IntPtr ptr, bool enabled);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetSceneManager", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetSceneManager(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetSceneManager", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetSceneManager(IntPtr ptr, IntPtr sceneManager);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetUseController", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetUseController(IntPtr ptr, bool useController);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_HasExternType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleSystem_HasExternType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string externType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetNumberOfEmittedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem_GetNumberOfEmittedParticles(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetNumberOfEmittedParticles2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleSystem_GetNumberOfEmittedParticles(IntPtr ptr, MParticleUniverse.Particle.ParticleType particleType);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_GetCategory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleSystem_GetCategory(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleSystem_SetCategory", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleSystem_SetCategory(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string category);

        #endregion
    }
}