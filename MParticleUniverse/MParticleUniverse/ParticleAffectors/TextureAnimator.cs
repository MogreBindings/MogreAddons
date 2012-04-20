using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The TextureAnimator makes it possible to have an animated texture for each individual particle. It relies on the uv coordinate 
	///    settings in the ParticleRenderer.
    /// </summary>
    public class TextureAnimator : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal TextureAnimator(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public enum TextureAnimationTypes
        {
            TAT_LOOP,
            TAT_UP_DOWN,
            TAT_RANDOM
        };

        public const float DEFAULT_TIME_STEP = 0.0f;
        public const ushort DEFAULT_TEXCOORDS_START = 0;
        public const ushort DEFAULT_TEXCOORDS_END = 0;
        public const TextureAnimationTypes DEFAULT_ANIMATION_TYPE = TextureAnimationTypes.TAT_LOOP;
        public const bool DEFAULT_START_RANDOM = true;


        internal static Dictionary<IntPtr, TextureAnimator> affectorInstances;

        internal static TextureAnimator GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, TextureAnimator>();

            TextureAnimator newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new TextureAnimator(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public TextureAnimator()
            : base(TextureAnimator_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }



        /** Returns the AnimationTimeStep. The AnimationTimeStep defines the time between each animation frame. */
        float AnimationTimeStep { get { return TextureAnimator_GetAnimationTimeStep(nativePtr); } set { TextureAnimator_SetAnimationTimeStep(nativePtr, value); } }

        /** Returns the type of texture animation. */
        TextureAnimationTypes TextureAnimationType { get { return TextureAnimator_GetTextureAnimationType(nativePtr); } set { TextureAnimator_SetTextureAnimationType(nativePtr, value); } }

        /** Todo */
        ushort TextureCoordsStart { get { return TextureAnimator_GetTextureCoordsStart(nativePtr); } set { TextureAnimator_SetTextureCoordsStart(nativePtr, value); } }

        /** Todo */
        ushort TextureCoordsEnd { get { return TextureAnimator_GetTextureCoordsEnd(nativePtr); } set { TextureAnimator_SetTextureCoordsEnd(nativePtr, value); } }

        /** Todo */
        bool IsStartRandom { get { return TextureAnimator_IsStartRandom(nativePtr); } set { TextureAnimator_SetStartRandom(nativePtr, value); } }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            TextureAnimator_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /** @copydoc ParticleAffector::_preProcessParticles */
        void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            TextureAnimator__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /** @copydoc ParticleAffector::_initParticleForEmission */
        void _initParticleForEmission(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            TextureAnimator__initParticleForEmission(nativePtr, particle.NativePointer);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            TextureAnimator__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
        protected void Dispose(bool disposing)
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
            TextureAnimator_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr TextureAnimator_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_GetAnimationTimeStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float TextureAnimator_GetAnimationTimeStep(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_SetAnimationTimeStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_SetAnimationTimeStep(IntPtr ptr, float animationTimeStep);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_GetTextureAnimationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern TextureAnimator.TextureAnimationTypes TextureAnimator_GetTextureAnimationType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_SetTextureAnimationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_SetTextureAnimationType(IntPtr ptr, TextureAnimator.TextureAnimationTypes textureAnimationType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_GetTextureCoordsStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort TextureAnimator_GetTextureCoordsStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_SetTextureCoordsStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_SetTextureCoordsStart(IntPtr ptr, ushort textureCoordsStart);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_GetTextureCoordsEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort TextureAnimator_GetTextureCoordsEnd(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_SetTextureCoordsEnd", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_SetTextureCoordsEnd(IntPtr ptr, ushort textureCoordsEnd);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_IsStartRandom", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool TextureAnimator_IsStartRandom(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_SetStartRandom", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_SetStartRandom(IntPtr ptr, bool startRandom);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator__initParticleForEmission", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator__initParticleForEmission(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "TextureAnimator__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void TextureAnimator__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        #endregion

    }
}
