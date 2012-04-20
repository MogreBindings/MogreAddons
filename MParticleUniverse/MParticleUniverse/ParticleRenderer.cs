using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    public enum ParticleRendererType
    {
        BeamRenderer,
        BillboardRenderer,
        BoxRenderer,
        EntityRenderer,
        LightRenderer,
        RibbonTrailRenderer,
        SphereRenderer
    };

    /// <summary>
    /// ParticleRenderer is a virtual class and must be subclassed. A subclass of ParticleRenderer is
    ///	responsible for rendering the visual particles. 
    /// <remarks>
    ///	Several types of renderers are possible. A billboard- and a mesh- renderer are examples.
    ///	</remarks>
    /// </summary>
    public unsafe abstract class ParticleRenderer : IAlias, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal ParticleRenderer(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        #region IAlias Implementation
        public AliasType AliasType
        {
            get { return IAlias_GetAliasType(NativePointer); }
            set {
                if (value == null)
                    throw new ArgumentNullException("AliasType cannot be null!");
                else
                    IAlias_SetAliasType(NativePointer, value);
            }
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

        public const byte DEFAULT_RENDER_QUEUE_GROUP = 50; // Mogre.RENDER_QUEUE_MAIN;
        public const bool DEFAULT_SORTED = false;
        public const byte DEFAULT_TEXTURECOORDS_ROWS = 1;
        public const byte DEFAULT_TEXTURECOORDS_COLUMNS = 1;
        public const bool DEFAULT_USE_SOFT_PARTICLES = false;
        public const float DEFAULT_SOFT_PARTICLES_CONTRAST_POWER = 0.8f;
        public const float DEFAULT_SOFT_PARTICLES_SCALE = 1.0f;
        public const float DEFAULT_SOFT_PARTICLES_DELTA = -1.0f;
        public const String SOFT_PREFIX = "NewSoft_";

        /// <summary>
        /// Means that the objects to render automatically rotate if the node to which the particle system is attached, rotates.
        /// </summary>
        public bool AutoRotate { get { return ParticleRenderer_GetAutoRotate(nativePtr); } set { ParticleRenderer_SetAutoRotate(nativePtr, value); } }




        /** 
        */
        public String RendererType { get { return Marshal.PtrToStringAnsi(ParticleRenderer_GetRendererType(nativePtr)); } set { ParticleRenderer_SetRendererType(nativePtr, value); } }

        /** Get / set the parent.
        */
        public ParticleTechnique ParentTechnique 
        { 
            get { return ParticleTechnique.GetInstances(ParticleRenderer_GetParentTechnique(nativePtr)); } 
            set 
            {
                if (value == null)
                    ParticleRenderer_SetParentTechnique(nativePtr, IntPtr.Zero); 
                else
                    ParticleRenderer_SetParentTechnique(nativePtr, value.nativePtr); 
            } 
        }

        /** Get / set whether the renderer is initialised.
        */
        public bool RendererInitialised { get { return ParticleRenderer_IsRendererInitialised(nativePtr); } set { ParticleRenderer_SetRendererInitialised(nativePtr, value); } }

        /** Perform activities when a Renderer is started.
        */
        public void _notifyStart()
        {
            ParticleRenderer__notifyStart(nativePtr);
        }

        /** Perform activities when a Renderer is stopped.
        */
        public void _notifyStop()
        {
            ParticleRenderer__notifyStop(nativePtr);
        }

        /** Notify that the Particle System is rescaled.
        */
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            ParticleRenderer__notifyRescaled(nativePtr, scale);
        }

        /** To make currently displayed objects visible or not.
        */
        public void SetVisible(bool isVisible)
        {
            ParticleRenderer_SetVisible(nativePtr, isVisible);
        }

        /** Prepare the renderer before it can be used.
        */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            ParticleRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** Reverse the actions from the _prepare.
        */
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            ParticleRenderer__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        /** Entry point for processing an individual particle.
        @remarks
            Some renderers provide additional functionality besides rendering particles only. This function
            makes it possible to perform activities that cannot be done in the updateRenderQueue() function.
            There is no default behaviour.
        */
        public void _processParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed,
            bool firstParticle)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ParticleRenderer__processParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed, firstParticle);
        }

        /** Returns value that indicates whether usage of soft particles is on or off.
        */
        public bool UseSoftParticles { get { return ParticleRenderer_GetUseSoftParticles(nativePtr); } set { ParticleRenderer_SetUseSoftParticles(nativePtr, value); } }


        /** Contrast Power determines the strength of the alpha that is applied to the particles.
        */
        public float SoftParticlesContrastPower { get { return ParticleRenderer_GetSoftParticlesContrastPower(nativePtr); } set { ParticleRenderer_SetSoftParticlesContrastPower(nativePtr, value); } }


        /** Scale determines the 'velocity' of alpha fading.
        */
        public float SoftParticlesScale { get { return ParticleRenderer_GetSoftParticlesScale(nativePtr); } set { ParticleRenderer_SetSoftParticlesScale(nativePtr, value); } }

        /** The delta is a threshold value that determines at what 'depth distance' alpha fading is applied.
        */
        public float SoftParticlesDelta { get { return ParticleRenderer_GetSoftParticlesDelta(nativePtr); } set { ParticleRenderer_SetSoftParticlesDelta(nativePtr, value); } }



        /** Updates the renderqueue
        @remarks
            The subclass must update the render queue using whichever Renderable instance(s) it wishes.
        */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            ParticleRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** Sets the material this renderer must use; called by ParticleTechnique. */
        public void _setMaterialName(String materialName)
        {
            if (materialName == null || materialName.Length == 0)
                throw new ArgumentNullException("materialName cannot be null!");
            ParticleRenderer__setMaterialName(nativePtr, materialName);
        }

        /** Delegated to by ParticleTechnique::_notifyCurrentCamera */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            ParticleRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** Delegated to by ParticleTechnique::_notifyAttached */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            ParticleRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** The particle quota has changed */
        public void _notifyParticleQuota(uint quota)
        {
            ParticleRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** The particle default size has changed */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            ParticleRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** Callback when particles are resized */
        public void _notifyParticleResized()
        {
            ParticleRenderer__notifyParticleResized(nativePtr);
        }

        /** Callback when particles are rotated */
        public void _notifyParticleZRotated()
        {
            ParticleRenderer__notifyParticleZRotated(nativePtr);
        }

        /** Get renderqueue group */
        public byte RenderQueueGroup { get { return ParticleRenderer_GetRenderQueueGroup(nativePtr); } set { ParticleRenderer_SetRenderQueueGroup(nativePtr, value); } }

        /** Gets the particles sort mode */
        public Mogre.SortMode _getSortMode()
        {
            return ParticleRenderer__getSortMode(nativePtr);
        }

        /** Returns whether the particles are sorted */
        public bool Sorted { get { return ParticleRenderer_IsSorted(nativePtr); } set { ParticleRenderer_SetSorted(nativePtr, value); } }

        /** Get the number of textureCoords rows (stacks) of an atlas texture. */
        public byte TextureCoordsRows { get { return ParticleRenderer_GetTextureCoordsRows(nativePtr); } set { ParticleRenderer_SetTextureCoordsRows(nativePtr, value); } }

        /** Get the number of textureCoords colums (slices) of an atlas texture. */
        public byte TextureCoordsColumns { get { return ParticleRenderer_GetTextureCoordsColumns(nativePtr); } set { ParticleRenderer_SetTextureCoordsColumns(nativePtr, value); } }

        /** Return the number of texture coordinates.
        */
        public uint NumTextureCoords { get { return ParticleRenderer_GetNumTextureCoords(nativePtr); } }

        /** Copy attributes to another renderer.
        */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            ParticleRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }

        /** Copy parent attributes to another renderer.
        */
        public void CopyParentAttributesTo(ParticleRenderer renderer)
        {
            ParticleRenderer_CopyParentAttributesTo(nativePtr, renderer.nativePtr);
        }

        /** Add a texture coordinate set, which consist of a the texel position (u, v), width and height
        */
        public void AddTextureCoords(float u, float v, float width, float height)
        {
            ParticleRenderer_AddTextureCoords(nativePtr, u, v, width, height);
        }

        /** Returns the list with texture coordinates.
        */
        public Mogre.FloatRect[] GetTextureCoords()
        {
            try
            {

                int bufSize = ParticleRenderer_GetTextureCoordsCount(nativePtr);
                if (bufSize == 0)
                    return null;
                IntPtr[] floats = new IntPtr[bufSize];
                Mogre.FloatRect[] toReturn = new Mogre.FloatRect[bufSize];

                ParticleRenderer_GetTextureCoords(nativePtr, floats, bufSize);

                for (int i = 0; i < floats.Length; i++)
                {
                    IntPtr scalePtr = floats[i];
                    Type type = typeof(Mogre.FloatRect);
                    Mogre.FloatRect retVal = (Mogre.FloatRect)(Marshal.PtrToStructure(scalePtr, type));

                    toReturn[i] = retVal;
                }
                return toReturn;
            }
            catch (Exception e) { Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetAutoRotate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleRenderer_GetAutoRotate(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetAutoRotate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetAutoRotate(IntPtr ptr, bool val);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetRendererType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleRenderer_GetRendererType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetRendererType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetRendererType(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string rendererType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleRenderer_GetParentTechnique(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_IsRendererInitialised", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleRenderer_IsRendererInitialised(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetRendererInitialised", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetRendererInitialised(IntPtr ptr, bool rendererInitialised);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyStop(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetVisible(IntPtr ptr, bool visible = true);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__processParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__processParticle(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed, bool firstParticle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetUseSoftParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleRenderer_GetUseSoftParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetUseSoftParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetUseSoftParticles(IntPtr ptr, bool useSoftParticles);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetSoftParticlesContrastPower", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleRenderer_GetSoftParticlesContrastPower(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetSoftParticlesScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleRenderer_GetSoftParticlesScale(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetSoftParticlesDelta", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float ParticleRenderer_GetSoftParticlesDelta(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetSoftParticlesContrastPower", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetSoftParticlesContrastPower(IntPtr ptr, float softParticlesContrastPower);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetSoftParticlesScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetSoftParticlesScale(IntPtr ptr, float softParticlesScale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetSoftParticlesDelta", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetSoftParticlesDelta(IntPtr ptr, float softParticlesDelta);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte ParticleRenderer_GetRenderQueueGroup(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer__getSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode ParticleRenderer__getSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_IsSorted", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticleRenderer_IsSorted(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetSorted", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetSorted(IntPtr ptr, bool sorted);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetTextureCoordsRows", CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte ParticleRenderer_GetTextureCoordsRows(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetTextureCoordsRows", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetTextureCoordsRows(IntPtr ptr, byte textureCoordsRows);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetTextureCoordsColumns", CallingConvention = CallingConvention.Cdecl)]
        internal static extern byte ParticleRenderer_GetTextureCoordsColumns(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_SetTextureCoordsColumns", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_SetTextureCoordsColumns(IntPtr ptr, byte textureCoordsColumns);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetNumTextureCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticleRenderer_GetNumTextureCoords(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_CopyParentAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_CopyParentAttributesTo(IntPtr ptr, IntPtr renderer);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_AddTextureCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_AddTextureCoords(IntPtr ptr, float u, float v, float width, float height);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetTextureCoordsCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticleRenderer_GetTextureCoordsCount(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetTextureCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleRenderer_GetTextureCoords(IntPtr ptr, [In, Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] arrLodDistances, int bufSize);

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
            ParticleRenderer_Destroy(NativePointer);
        }

        #endregion

    }

    internal class ParticleRendererHelper
    {
        public static ParticleRenderer GetParticleRenderer(IntPtr ptr)
        {
            String daType = Marshal.PtrToStringAnsi(ParticleRenderer_GetRendererType(ptr));
            switch (daType)
            {
                case "Beam":
                    return ParticleRenderers.BeamRenderer.GetInstance(ptr);
                case "Billboard":
                    return ParticleRenderers.BillboardRenderer.GetInstance(ptr);
                case "Box":
                    return ParticleRenderers.BoxRenderer.GetInstance(ptr);
                case "Entity":
                    return ParticleRenderers.EntityRenderer.GetInstance(ptr);
                case "Light":
                    return ParticleRenderers.LightRenderer.GetInstance(ptr);
                case "RibbonTrail":
                    return ParticleRenderers.RibbonTrailRenderer.GetInstance(ptr);
                case "Sphere":
                    return ParticleRenderers.SphereRenderer.GetInstance(ptr);
                default:
                    return null;
            }
        }

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetRendererType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleRenderer_GetRendererType(IntPtr ptr);

    }
}
