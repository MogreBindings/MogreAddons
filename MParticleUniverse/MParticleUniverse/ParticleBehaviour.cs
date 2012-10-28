using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    public enum ParticleBehaviourType
    {
        SlaveBehaviour
    };

    /// <summary>
    /// Defines the behaviour of a particle.
    /// <remarks>
    ///	While a ParticleAffector acts as an external 'force' on the particles, the ParticleBehaviour defines 
    ///	the internal behaviour of each particle individually. For example, 'wind' is typically something that is 
    ///	defined as a ParticleAffector, while 'elasticity' is behaviour of the particle itself.
    ///	</remarks>
    /// par
    ///	A particle can have multiple Behaviours. Each ParticleBehaviour is accompanied with a specific subclass of 
    ///	the ParticleBehaviourFactory. Similar to ParticleEmitters and ParticleAffectors, each factory is 
    ///	identified by means of a type name.
    /// par
    ///	The main reason to introduce the ParticleBehaviour class is to be able to expand the attributes and 
    ///	functions of a particle dynamically and to assign behaviour by means of a particle script.
    /// </summary>
    public abstract class ParticleBehaviour : IAlias, IElement, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
            set { nativePtr = value; }
        }

        internal ParticleBehaviour(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        /* Todo
        */
        public String BehaviourType { get { return Marshal.PtrToStringAnsi(ParticleBehaviour_GetBehaviourType(nativePtr)); } set { ParticleBehaviour_SetBehaviourType(nativePtr, value); } }

        /* Todo
        */
        public ParticleTechnique ParentTechnique { 
            get { return ParticleTechnique.GetInstances(ParticleBehaviour_GetParentTechnique(nativePtr)); }
            set
            {
                if (value == null)
                    ParticleBehaviour_SetParentTechnique(nativePtr, IntPtr.Zero);
                else
                    ParticleBehaviour_SetParentTechnique(nativePtr, value.nativePtr);
            }
        }

        /* Notify that the Behaviour is rescaled.
        */
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            ParticleBehaviour__notifyRescaled(nativePtr, scale);
        }

        /* Perform initialisation actions.
        @remarks
            The _prepare() function is automatically called during initialisation (prepare) activities of a 
            ParticleTechnique. A subclass could implement this function to perform initialisation 
            actions.
        */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleBehaviour__prepare(nativePtr, technique.nativePtr);
        }

        /* Reverse the actions from the _prepare.
        */
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            ParticleBehaviour__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        /* Perform initialising activities as soon as the particle with which the ParticleBehaviour is
            associated, is emitted.
        */
        public void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleBehaviour__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        /* Process a particle.
        */
        public void _processParticle(ParticleTechnique technique, Particle particle, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleBehaviour__processParticle(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /* Perform some action if a particle expires.
        */
        public void _initParticleForExpiration(ParticleTechnique technique, Particle particle, float timeElapsed)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleBehaviour__initParticleForExpiration(nativePtr, technique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /* Copy attributes to another ParticleBehaviour.
        */
        public void CopyAttributesTo(ParticleBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException("behaviour cannot be null!");
            ParticleBehaviour_CopyAttributesTo(nativePtr, behaviour.nativePtr);
        }

        /* Copy parent attributes to another behaviour.
        */
        public void CopyParentAttributesTo(ParticleBehaviour behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException("behaviour cannot be null!");
            ParticleBehaviour_CopyParentAttributesTo(nativePtr, behaviour.nativePtr);
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

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_GetBehaviourType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleBehaviour_GetBehaviourType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_SetBehaviourType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour_SetBehaviourType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string behaviourType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_GetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleBehaviour_GetParentTechnique(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour__initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour__initParticleForEmission(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour__processParticle(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour__initParticleForExpiration", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour__initParticleForExpiration(IntPtr ptr, IntPtr technique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour_CopyAttributesTo(IntPtr ptr, IntPtr behaviour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleBehaviour_CopyParentAttributesTo(IntPtr ptr, IntPtr behaviour);

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
            ParticleBehaviour_Destroy(NativePointer);
        }

        #endregion

    }

    internal class ParticleBehaviourHelper
    {
        public static ParticleBehaviour GetParticleBehaviour(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            String daType = Marshal.PtrToStringAnsi(ParticleBehaviour_GetBehaviourType(ptr));
            switch (daType)
            {
                case "Slave":
                    return ParticleBehaviours.SlaveBehaviour.GetInstance(ptr);
                default:
                    return null;
            }
        }

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleBehaviour_GetBehaviourType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleBehaviour_GetBehaviourType(IntPtr ptr);
    }
}
