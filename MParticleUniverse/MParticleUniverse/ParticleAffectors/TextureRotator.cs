using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The TextureRotator rotates the texture(s) of a particle. In general it is possible to support individual
	///	rotation of each particle texture-set - the same as in the geometry rotator, setting 
	///	mUseOwnRotationSpeed to true -, but in practice this isn't really usable, because usually all particles
	///	share the same material.
    /// </summary>
    public class TextureRotator : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal TextureRotator(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        public const bool DEFAULT_USE_OWN_SPEED = false;
        public const float DEFAULT_ROTATION_SPEED = 10.0f;
        public const float DEFAULT_ROTATION = 0.0f;


        internal static Dictionary<IntPtr, TextureRotator> affectorInstances;

        internal static TextureRotator GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, TextureRotator>();

            TextureRotator newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new TextureRotator(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public TextureRotator()
            : base(TextureRotator_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /** Returns an indication whether the 2D rotation speed is the same for all particles in this 
            particle technique, or whether the 2D rotation speed of the particle itself is used.
        */
        public bool UseOwnRotationSpeed { get { return TextureRotator_UseOwnRotationSpeed(nativePtr); } set { TextureRotator_SetUseOwnRotationSpeed(nativePtr, value); } }

        /** Returns the rotation speed. This is the speed controlled by the affector.
        */
        public DynamicAttribute RotationSpeed { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(TextureRotator_GetRotationSpeed(nativePtr));} 
            set { if (value == null)
                TextureRotator_SetRotationSpeed(nativePtr, IntPtr.Zero);
                else
                    TextureRotator_SetRotationSpeed(nativePtr, value.NativePointer); } }

        /** Returns the rotation defined in the the affector.
        */
        public DynamicAttribute Rotation { 
            get { return DynamicAttributeTypeHelper.GetDynamicAttribute(TextureRotator_GetRotation(nativePtr)); }
            set
            {
                if (value == null)
                    TextureRotator_SetRotation(nativePtr, IntPtr.Zero);
                else
                    TextureRotator_SetRotation(nativePtr, value.NativePointer);
            }
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            TextureRotator_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** Returns a rotation set in the affector, depending on the type of dynamic attribute.
        */
        public Mogre.Radian _calculateRotation()
        {
            Mogre.Radian rad = new Mogre.Radian();
            IntPtr calcdRad = TextureRotator__calculateRotation(nativePtr);
            Marshal.PtrToStructure(calcdRad, rad);
            return rad;

        }

        /** Returns a rotation speed value, depending on the type of dynamic attribute.
        */
        public Mogre.Radian _calculateRotationSpeed(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            Mogre.Radian rad = new Mogre.Radian();
            Marshal.PtrToStructure(TextureRotator__calculateRotationSpeed(nativePtr, particle.NativePointer), rad);
            return rad;
        }

        /** @copydoc ParticleAffector::_initParticleForEmission */
        public void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            TextureRotator__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            TextureRotator__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            TextureRotator_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TextureRotator_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_UseOwnRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool TextureRotator_UseOwnRotationSpeed(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_SetUseOwnRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator_SetUseOwnRotationSpeed(IntPtr ptr, bool useOwnRotationSpeed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_GetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TextureRotator_GetRotationSpeed(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_SetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator_SetRotationSpeed(IntPtr ptr, IntPtr dynRotationSpeed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_GetRotation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TextureRotator_GetRotation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_SetRotation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator_SetRotation(IntPtr ptr, IntPtr dynRotation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator__calculateRotation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TextureRotator__calculateRotation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator__calculateRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TextureRotator__calculateRotationSpeed(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator__initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator__initParticleForEmission(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureRotator__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureRotator__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion

    }
}
