using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// The Extern class is the abstract class for all extern components. Each subclass of Extern wraps a
	///	particular external component and forms a bridge between the external component and the
	///	ParticleUniverse (or to be more specific, the ParticleTechnique).
	/// <remarks>
	///	External components can be used to add functionality to the ParticleUniverse plugin. An example is
	///	the addition of a physics library. Settings up the library is typically done within a dedicated
	///	subclass of Extern.
    ///	</remarks>
    /// </summary>
    public interface Extern : IAlias
    {
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
        /// Copy attributes to another extern object.
        /// </summary>
        /// <param name="externObject"></param>
        void CopyAttributesTo(Extern externObject);

        /// <summary>
        /// Copy parent attributes to another extern object.
        /// </summary>
        /// <param name="externObject"></param>
        void CopyParentAttributesTo(Extern externObject);

        /// <summary>
        /// Perform initialisation actions.
        /// The _prepare() function is automatically called during initialisation activities of a ParticleTechnique.
        ///    Each subclass should implement this function to perform initialisation actions needed for the external
        ///    component.
        /// </summary>
        /// <param name="technique">
        ///     This is a pure virtual function, to be sure that developers of an extern component don't forget to
        ///    override this functions and perform setup/initialisation of the component. This is to prevent that 
        ///    unexplainable errors occur because initialisation tasks where forgotten.
        /// </param>
        void _prepare(ParticleTechnique technique);

        /// <summary>
        /// Reverse the actions from the _prepare.
        /// </summary>
        /// <param name="particleTechnique"></param>
        void _unprepare(ParticleTechnique particleTechnique); //{/* No implementation */};

        /// <summary>
        /// Perform activities when an Extern is started.
        /// </summary>
        void _notifyStart(); //{/* Do nothing */};

        /// <summary>
        /// Perform activities when an Extern is paused.
        /// </summary>
        void _notifyPause(); //{/* Do nothing */};

        /// <summary>
        /// Perform activities when an Extern is resumed.
        /// </summary>
        void _notifyResume(); //{/* Do nothing */};

        /// <summary>
        /// Perform activities when an Extern is stopped.
        /// </summary>
        void _notifyStop(); //{/* Do nothing */};

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
        void _preProcessParticles(ParticleTechnique technique, float timeElapsed); //{/* Do nothing */};

        /// <summary>
        /// Initialise a newly emitted particle.
        /// </summary>
        /// <param name="particle">particle Pointer to a Particle to initialise.</param>
        void _initParticleForEmission(Particle particle); // { /* by default do nothing */ }

        /// <summary>
        ///  Perform actions if a particle gets expired.
        /// </summary>
        /// <param name="particle"></param>
        void _initParticleForExpiration(Particle particle); // { /* by default do nothing */ }

        /// <summary>
        /// Perform precalculations if the first Particle in the update-loop is processed.
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        void _firstParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed); // { /* by default do nothing */ }

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
        /// Actually processes a particle.
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        void _interface(ParticleTechnique technique, Particle particle, float timeElapsed);

        /// <summary>
        /// Perform activities after the individual particles are processed.
        /// <remarks>
        /// This function is called after the ParticleTechnique update-loop where all particles are traversed.
        /// </remarks>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        void _postProcessParticles(ParticleTechnique technique, float timeElapsed); //{/* Do nothing */};

        //{
        //    get { return nativePtr; }
        //}
        //internal Extern(IntPtr ptr)
        //{
        //    nativePtr = ptr;
        //}


    }

    /*
            #region Extern Implementation

        /// <summary>
        /// Name of the extern (optional)
        /// </summary>
        String Name
        { get { return Extern_GetName(nativePtr); } set { Extern_SetName(nativePtr, value); } }

        /// <summary>
        /// Type of extern
        /// </summary>
        String Type
        { get { return Extern_GetExternType(nativePtr); } set { Extern_SetExternType(nativePtr, value); } }

        /// <summary>
        /// Parent
        /// </summary>
        ParticleTechnique ParentTechnique
        { get { return new ParticleTechnique(Extern_GetParentTechnique(nativePtr)); } set { Extern_SetParentTechnique(nativePtr, value.NativePointer); } }


        /// <summary>
        /// Notify that the Particle System is rescaled.
        /// </summary>
        /// <param name="scale">Scale Value</param>
        void _notifyRescaled(Mogre.Vector3 scale)
        {
            Extern__notifyRescaled(nativePtr, ref scale);
        }


        /// <summary>
        /// Copy attributes to another extern object.
        /// </summary>
        /// <param name="externObject"></param>
        void CopyAttributesTo(Extern externObject)
        {
            Extern_CopyAttributesTo(nativePtr, externObject.NativePointer);
        }

        /// <summary>
        /// Copy parent attributes to another extern object.
        /// </summary>
        /// <param name="externObject"></param>
        void CopyParentAttributesTo(Extern externObject)
        {
            Extern_CopyParentAttributesTo(nativePtr, externObject.NativePointer);
        }

        /// <summary>
        /// Perform initialisation actions.
        /// The _prepare() function is automatically called during initialisation activities of a ParticleTechnique.
        ///    Each subclass should implement this function to perform initialisation actions needed for the external
        ///    component.
        /// </summary>
        /// <param name="technique">
        ///     This is a pure virtual function, to be sure that developers of an extern component don't forget to
        ///    override this functions and perform setup/initialisation of the component. This is to prevent that 
        ///    unexplainable errors occur because initialisation tasks where forgotten.
        /// </param>
        void _prepare(ParticleTechnique technique)
        {
            Extern__prepare(nativePtr, technique.NativePointer);
        }

        /// <summary>
        /// Reverse the actions from the _prepare.
        /// </summary>
        /// <param name="particleTechnique"></param>
        void _unprepare(ParticleTechnique particleTechnique)
        {
            Extern__unprepare(nativePtr, particleTechnique.NativePointer);
        }

        /// <summary>
        /// Perform activities when an Extern is started.
        /// </summary>
        void _notifyStart()
        {
            Extern__notifyStart(nativePtr);
        }

        /// <summary>
        /// Perform activities when an Extern is paused.
        /// </summary>
        void _notifyPause()
        {
            Extern__notifyPause(nativePtr);
        }

        /// <summary>
        /// Perform activities when an Extern is resumed.
        /// </summary>
        void _notifyResume()
        {
            Extern__notifyResume(nativePtr);
        }

        /// <summary>
        /// Perform activities when an Extern is stopped.
        /// </summary>
        void _notifyStop()
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
        void _preProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
            Extern__preProcessParticles(nativePtr, technique.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Initialise a newly emitted particle.
        /// </summary>
        /// <param name="particle">particle Pointer to a Particle to initialise.</param>
        void _initParticleForEmission(Particle particle)
        {
            Extern__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        /// <summary>
        ///  Perform actions if a particle gets expired.
        /// </summary>
        /// <param name="particle"></param>
        void _initParticleForExpiration(Particle particle)
        {
            Extern__initParticleForExpiration(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// Perform precalculations if the first Particle in the update-loop is processed.
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        void _firstParticle(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
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
        void _processParticle(ParticleTechnique technique, Particle particle, float timeElapsed, bool firstParticle)
        {
            Extern__processParticle(nativePtr, technique.NativePointer, particle.NativePointer, timeElapsed, firstParticle);
        }

        /// <summary>
        /// Actually processes a particle.
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        void _interface(ParticleTechnique technique, Particle particle, float timeElapsed)
        {
            Extern__interface(nativePtr, technique.NativePointer, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// Perform activities after the individual particles are processed.
        /// <remarks>
        /// This function is called after the ParticleTechnique update-loop where all particles are traversed.
        /// </remarks>
        /// </summary>
        /// <param name="technique"></param>
        /// <param name="timeElapsed"></param>
        void _postProcessParticles(ParticleTechnique technique, float timeElapsed)
        {
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
    */
}
