using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// The LightRenderer class is responsible to render particles as a light.
    /// <remarks>
    ///	Note, that the diffuse colour cannot be set. This is, because the light inherits its diffuse colour from the particle. This makes
    ///	it possible to manipulate the colour (for instance, using a Colour Affector).
    ///	</remarks>
    /// </summary>
    public unsafe class LightRenderer : ParticleRenderer, IDisposable 
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, LightRenderer> rendererInstances;

        internal static LightRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, LightRenderer>();

            LightRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new LightRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal LightRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const Mogre.Light.LightTypes DEFAULT_LIGHT_TYPE = Mogre.Light.LightTypes.LT_POINT;
        public static Mogre.ColourValue DEFAULT_DIFFUSE { get { return new Mogre.ColourValue(0, 0, 0); } }
        public static Mogre.ColourValue DEFAULT_SPECULAR { get { return new Mogre.ColourValue(0, 0, 0); } }
        public const float DEFAULT_ATT_RANGE = 100000;
        public const float DEFAULT_ATT_CONSTANT = 1.0f;
        public const float DEFAULT_ATT_LINEAR = 0.0f;
        public const float DEFAULT_ATT_QUADRATIC = 0.0f;
        public static Mogre.Radian DEFAULT_SPOT_INNER_ANGLE { get { return new Mogre.Degree(30.0f); } }
        public static Mogre.Radian DEFAULT_SPOT_OUTER_ANGLE { get { return new Mogre.Degree(40.0f); } }
        public const float DEFAULT_FALLOFF = 1.0f;
        public const float DEFAULT_POWER_SCALE = 1.0f;


        public LightRenderer()
            : base(LightRenderer_New())
        {
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }

        /** Return the type of light that is emitted.
        */
        public Mogre.Light.LightTypes LightType { get { return LightRenderer_GetLightType(nativePtr); } set { LightRenderer_SetLightType(nativePtr, value); } }

        /** 
        */
        public Mogre.ColourValue SpecularColour
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)LightRenderer_GetSpecularColour(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                if (value == null)
                    LightRenderer_SetSpecularColour(nativePtr, IntPtr.Zero);
                else
                {
                    IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                    Marshal.StructureToPtr(value, colourPtr, true);
                    LightRenderer_SetSpecularColour(nativePtr, colourPtr);
                }
            }
        }

        /** 
        */
        public float AttenuationRange { get { return LightRenderer_GetAttenuationRange(nativePtr); } set { LightRenderer_SetAttenuationRange(nativePtr, value); } }

        /** 
        */
        public float AttenuationConstant { get { return LightRenderer_GetAttenuationConstant(nativePtr); } set { LightRenderer_SetAttenuationConstant(nativePtr, value); } }

        /** 
        */
        public float AttenuationLinear { get { return LightRenderer_GetAttenuationLinear(nativePtr); } set { LightRenderer_SetAttenuationLinear(nativePtr, value); } }

        /** 
        */
        public float AttenuationQuadratic { get { return LightRenderer_GetAttenuationQuadratic(nativePtr); } set { LightRenderer_SetAttenuationQuadratic(nativePtr, value); } }

        /** 
        */
        public Mogre.Radian SpotlightInnerAngle
        {
            get
            {
                Mogre.Radian vec = *(((Mogre.Radian*)LightRenderer_GetSpotlightInnerAngle(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                if (value == null)
                    LightRenderer_SetSpotlightInnerAngle(nativePtr, IntPtr.Zero);
                else
                {
                    IntPtr radianPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                    Marshal.StructureToPtr(value, radianPtr, true);
                    LightRenderer_SetSpotlightInnerAngle(nativePtr, radianPtr);
                }
            }
        }


        /** 
        */
        public Mogre.Radian SpotlightOuterAngle
        {
            get
            {
                Mogre.Radian vec = *(((Mogre.Radian*)LightRenderer_GetSpotlightOuterAngle(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                if (value == null)
                    LightRenderer_SetSpotlightOuterAngle(nativePtr, IntPtr.Zero);
                else
                {
                    IntPtr radianPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                    Marshal.StructureToPtr(value, radianPtr, true);
                    LightRenderer_SetSpotlightOuterAngle(nativePtr, radianPtr);
                }
            }
        }

        /** 
        */
        public float SpotlightFalloff { get { return LightRenderer_GetSpotlightFalloff(nativePtr); } set { LightRenderer_SetSpotlightFalloff(nativePtr, value); } }

        /** 
        */
        public float PowerScale { get { return LightRenderer_GetPowerScale(nativePtr); } set { LightRenderer_SetPowerScale(nativePtr, value); } }

        /** 
        */
        public float FlashFrequency { get { return LightRenderer_GetFlashFrequency(nativePtr); } set { LightRenderer_SetFlashFrequency(nativePtr, value); } }

        /** 
        */
        public float FlashLength { get { return LightRenderer_GetFlashLength(nativePtr); } set { LightRenderer_SetFlashLength(nativePtr, value); } }

        /** 
        */
        public bool FlashRandom { get { return LightRenderer_IsFlashRandom(nativePtr); } set { LightRenderer_SetFlashRandom(nativePtr, value); } }

        /** Deletes all ChildSceneNodes en Lights.
        */
        public void _destroyAll()
        {
            LightRenderer__destroyAll(nativePtr);
        }

        /** @copydoc ParticleRenderer::setVisible */
        //virtual void setVisible(bool visible = true);

        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            LightRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            LightRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_updateRenderQueue */
        //virtual void _updateRenderQueue(Ogre::RenderQueue* queue, DllImport("ParticleUniverse.dll"* pool);

        /** @copydoc ParticleRenderer::_processParticle */
        public virtual void _processParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed,
            bool firstParticle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            LightRenderer__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            LightRenderer__setMaterialName(nativePtr, materialName);
        }

        /** @copydoc ParticleRenderer::_notifyCurrentCamera */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            LightRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** @copydoc ParticleRenderer::_notifyAttached */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            LightRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_notifyParticleQuota */
        public void _notifyParticleQuota(uint quota)
        {
            LightRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** @copydoc ParticleRenderer::_notifyDefaultDimensions */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            LightRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** @copydoc ParticleRenderer::_notifyParticleResized */
        public void _notifyParticleResized()
        {
            LightRenderer__notifyParticleResized(nativePtr);
        }

        /** @copydoc ParticleRenderer::_getSortMode */
        public Mogre.SortMode _getSortMode()
        {
            return LightRenderer__GetSortMode(nativePtr);
        }

        /** @copydoc ParticleRenderer::copyAttributesTo */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            LightRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }


        #region LightRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LightRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetLightType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.Light.LightTypes LightRenderer_GetLightType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetLightType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetLightType(IntPtr ptr, Mogre.Light.LightTypes lightType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetSpecularColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LightRenderer_GetSpecularColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetSpecularColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetSpecularColour(IntPtr ptr, IntPtr specularColour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetAttenuationRange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetAttenuationRange(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetAttenuationRange", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetAttenuationRange(IntPtr ptr, float attenuationRange);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetAttenuationConstant", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetAttenuationConstant(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetAttenuationConstant", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetAttenuationConstant(IntPtr ptr, float attenuationConstant);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetAttenuationLinear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetAttenuationLinear(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetAttenuationLinear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetAttenuationLinear(IntPtr ptr, float attenuationLinear);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetAttenuationQuadratic", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetAttenuationQuadratic(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetAttenuationQuadratic", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetAttenuationQuadratic(IntPtr ptr, float attenuationQuadratic);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetSpotlightInnerAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LightRenderer_GetSpotlightInnerAngle(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetSpotlightInnerAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetSpotlightInnerAngle(IntPtr ptr, IntPtr spotlightInnerAngle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetSpotlightOuterAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr LightRenderer_GetSpotlightOuterAngle(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetSpotlightOuterAngle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetSpotlightOuterAngle(IntPtr ptr, IntPtr spotlightOuterAngle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetSpotlightFalloff", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetSpotlightFalloff(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetSpotlightFalloff", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetSpotlightFalloff(IntPtr ptr, float spotlightFalloff);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetPowerScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetPowerScale(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetPowerScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetPowerScale(IntPtr ptr, float powerScale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetFlashFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetFlashFrequency(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetFlashFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetFlashFrequency(IntPtr ptr, float flashFrequency);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_GetFlashLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float LightRenderer_GetFlashLength(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetFlashLength", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetFlashLength(IntPtr ptr, float flashLength);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_IsFlashRandom", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool LightRenderer_IsFlashRandom(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_SetFlashRandom", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_SetFlashRandom(IntPtr ptr, bool flashRandom);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__destroyAll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__destroyAll(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__processParticle(IntPtr ptr, IntPtr particleTechnique,
                        IntPtr particle,
                        float timeElapsed,
                        bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode LightRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "LightRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void LightRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
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
            LightRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
