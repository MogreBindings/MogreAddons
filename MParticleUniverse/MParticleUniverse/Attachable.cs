using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// The Attachable is a MovableObject that can be registered at a ParticleSystem as an Extern. By means
    ///	of this construction it is possible to make a dependency between the 2 MovableObjects.
    ///	This can be convenient if the Attachable is a Particle Affector wrapper, that affects a particle
    ///	based on their mutual distance.
    /// <remarks>
    ///	This MovableObject will not be registered at the SceneManager; its purpose is to take advantage of the
    ///	fact that it can be attached to a SceneNode. If an inherited class of Attachable uses visual (rendering)
    ///	elements, it should use the registerAttachable() and unregisterAttachable() functions of the 
    ///	ParticleSystemManager. This implies some additional coding, because this is not supported by the
    ///	scripting capabilities.
    /// </remarks>
    /// </summary>
    public unsafe class Attachable : Mogre.MovableObject, IAttachable, Extern, IDisposable
    {
        internal IntPtr nativePtr;
        /// <summary>
        /// The Particle System in unamanged memory this class represents.
        /// </summary>
        public IntPtr NativePointer
        {
            get { return nativePtr; }
            //set { nativePtr = value; }
        }

        internal static Dictionary<IntPtr, Attachable> attachableInstances;

        internal static Attachable GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (attachableInstances == null)
                attachableInstances = new Dictionary<IntPtr, Attachable>();

            Attachable newvalue;

            if (attachableInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Attachable(ptr);
            attachableInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal Attachable(IntPtr ptr)
            : base((CLRObject*)ptr)
        {
            nativePtr = ptr;
        }

        // Constants
        public static String PU_ATTACHABLE;

        public Attachable()
            : base((CLRObject*)Attachable_New())
        {
            nativePtr = (IntPtr)base.NativePtr;
            attachableInstances.Add(nativePtr, this);
        }

        /** Get the Distance Threshold
        */
        public float DistanceThreshold
        {
            get
            {
                return Attachable_GetDistanceThreshold(nativePtr);
            }

            /** Set the Distance Threshold. This threshold defines at which distance the Attachable doesn't influence 
                the particle anymore.
            */
            set
            {
                Attachable_SetDistanceThreshold(nativePtr, value);
            }
        }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            Attachable__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            Attachable__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        public String GetMovableType()
        {
            return Marshal.PtrToStringAnsi(Attachable_GetMovableType(nativePtr));
        }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        public Mogre.AxisAlignedBox GetBoundingBox()
        {
            try
            {
                IntPtr scalePtr = Attachable_GetBoundingBox(nativePtr);
                if (scalePtr == null || scalePtr == IntPtr.Zero)
                    return null;
                Mogre.AxisAlignedBox aabtype = new Mogre.AxisAlignedBox();
                Mogre.AxisAlignedBox vec = (Mogre.AxisAlignedBox)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
                return vec;
            }
            catch (Exception)
            {
                //if (printDebug) Console.WriteLine(e.Message + "@" + e.Source);
                //lastError = e;
            }
            return null;
        }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        public float GetBoundingRadius()
        {
            return Attachable_GetBoundingRadius(nativePtr);
        }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        public void _updateRenderQueue(Mogre.RenderQueue queue)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            Attachable__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr);
        }

        ///** @see MovableObject
        //*/
        //public void visitRenderables(Mogre.Renderable.Visitor visitor,
        //    bool debugRenderables = false) {/* No implementation */}

        /** Copy attributes to another Extern object.
        */
        public void CopyAttributesTo(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            Attachable_CopyAttributesTo(nativePtr, externObject.NativePointer);
        }

        /** Perform initialisation actions.
        */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            Attachable__prepare(nativePtr, technique.NativePointer);
        }

        /** Reverse the actions from the _prepare.
        */
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            Attachable__unprepare(nativePtr, particleTechnique.NativePointer);
        }

        /** Actually processes a particle.
        */
        public void _interface(ParticleTechnique technique,
            Particle particle,
            float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Attachable__interface(nativePtr, technique.NativePointer, particle.NativePointer, timeElapsed);
        }


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
        /// Perform activities when an Extern is started.
        /// </summary>
        public void _notifyStart()
        {
            Extern__notifyStart(nativePtr);
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
        /// Perform activities when an Extern is stopped.
        /// </summary>
        public void _notifyStop()
        {
            Extern__notifyStop(nativePtr);
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

        #region Attachable PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_Get_PU_ATTACHABLE", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Attachable_Get_PU_ATTACHABLE();

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_Set_PU_ATTACHABLE", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable_Set_PU_ATTACHABLE([MarshalAs(UnmanagedType.LPStr)]string value);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Attachable_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_GetDistanceThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Attachable_GetDistanceThreshold(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_SetDistanceThreshold", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable_SetDistanceThreshold(IntPtr ptr, float distanceThreshold);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable__notifyCurrentCamera(IntPtr ptr, IntPtr cam);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_GetMovableType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Attachable_GetMovableType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_GetBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Attachable_GetBoundingBox(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_GetBoundingRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Attachable_GetBoundingRadius(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable__updateRenderQueue(IntPtr ptr, IntPtr queue);

        //[DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_VisitRenderables", CallingConvention = CallingConvention.Cdecl)]
        //            internal static extern void Attachable_VisitRenderables(IntPtr ptr, Mogre.Renderable::Visitor visitor, bool debugRenderables = false);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable_CopyAttributesTo(IntPtr ptr, IntPtr externObject);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable__prepare(IntPtr ptr, IntPtr technique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable__unprepare(IntPtr ptr, IntPtr particleTechnique);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Attachable__interface", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Attachable__interface(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);

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
            Attachable_Destroy(NativePointer);
            attachableInstances.Remove(nativePtr);
        }

        #endregion

    }

    public interface IAttachable : Extern
    {
        /// <summary>
        /// The Particle System in unamanged memory this class represents.
        /// </summary>
        IntPtr NativePointer { get; }

        // Constants
        //String PU_ATTACHABLE { get; set; }

        /** Get the Distance Threshold
        */
        float DistanceThreshold { get; set; }

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        void _notifyAttached(Mogre.Node parent, bool isTagPoint = false);

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        void _notifyCurrentCamera(Mogre.Camera cam);

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        String GetMovableType();

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        Mogre.AxisAlignedBox GetBoundingBox();

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        float GetBoundingRadius();

        /** Overridden from MovableObject
        @see
            MovableObject
        */
        void _updateRenderQueue(Mogre.RenderQueue queue);

        ///** @see MovableObject
        //*/
        //public void visitRenderables(Mogre.Renderable.Visitor visitor,
        //    bool debugRenderables = false) {/* No implementation */}

        /** Copy attributes to another Extern object.
        */
        void CopyAttributesTo(Extern externObject);

        /** Perform initialisation actions.
        */
        void _prepare(ParticleTechnique technique);

        /** Reverse the actions from the _prepare.
        */
        void _unprepare(ParticleTechnique particleTechnique);

        /** Actually processes a particle.
        */
        void _interface(ParticleTechnique technique, Particle particle, float timeElapsed);


        #region Extern Implementation

        /// <summary>
        /// Name of the extern (optional)
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Type of extern
        /// </summary>
        String Type { get; set; }

        /// <summary>
        /// Parent
        /// </summary>
        ParticleTechnique ParentTechnique { get; set; }

        /// <summary>
        /// Notify that the Particle System is rescaled.
        /// </summary>
        /// <param name="scale">Scale Value</param>
        void _notifyRescaled(Mogre.Vector3 scale);



        /// <summary>
        /// Copy parent attributes to another extern object.
        /// </summary>
        /// <param name="externObject"></param>
        void CopyParentAttributesTo(Extern externObject);

        /// <summary>
        /// Perform activities when an Extern is started.
        /// </summary>
        void _notifyStart();

        /// <summary>
        /// Perform activities when an Extern is paused.
        /// </summary>
        void _notifyPause();

        /// <summary>
        /// Perform activities when an Extern is resumed.
        /// </summary>
        void _notifyResume();

        /// <summary>
        /// Perform activities when an Extern is stopped.
        /// </summary>
        void _notifyStop();

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
        void _preProcessParticles(ParticleTechnique technique, float timeElapsed);

        /// <summary>
        /// Initialise a newly emitted particle.
        /// </summary>
        /// <param name="particle">particle Pointer to a Particle to initialise.</param>
        void _initParticleForEmission(Particle particle);

        /// <summary>
        ///  Perform actions if a particle gets expired.
        /// </summary>
        /// <param name="particle"></param>
        void _initParticleForExpiration(Particle particle);

        /// <summary>
        /// Perform precalculations if the first Particle in the update-loop is processed.
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        void _firstParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed);

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
        void _processParticle(ParticleTechnique technique, Particle particle, float timeElapsed, bool firstParticle);


        /// <summary>
        /// Perform activities after the individual particles are processed.
        /// <remarks>
        /// This function is called after the ParticleTechnique update-loop where all particles are traversed.
        /// </remarks>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        void _postProcessParticles(ParticleTechnique technique, float timeElapsed);


        #endregion
    }
}