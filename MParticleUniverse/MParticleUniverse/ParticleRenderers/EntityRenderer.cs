using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// Visual data specific for this type of renderer.
    /// </summary>
    public unsafe class EntityRenderer : ParticleRenderer, IDisposable 
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, EntityRenderer> rendererInstances;

        internal static EntityRenderer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, EntityRenderer>();

            EntityRenderer newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new EntityRenderer(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal EntityRenderer(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public enum EntityOrientationTypes
        {
            ENT_ORIENTED_SELF,
            ENT_ORIENTED_SELF_MIRRORED,
            ENT_ORIENTED_SHAPE
        }

        public const EntityRenderer.EntityOrientationTypes DEFAULT_ORIENTATION_TYPE = EntityRenderer.EntityOrientationTypes.ENT_ORIENTED_SHAPE;

        public EntityRenderer()
            : base(EntityRenderer_New())
        {
            nativePtr = base.nativePtr;
            rendererInstances.Add(nativePtr, this);
        }

        /** Get the mesh name.
        */
        public String MeshName { get { return Marshal.PtrToStringAnsi(EntityRenderer_GetMeshName(nativePtr)); } set { EntityRenderer_SetMeshName(nativePtr, value); } }

        /** Deletes all ChildSceneNodes en Entities.
        */
        public void _destroyAll()
        {
            EntityRenderer__destroyAll(nativePtr);
        }

        /** Disable all ChildSceneNodes.
        */
        //			virtual void _notifyStop(void);

        /** Make all objects visible or invisible.
        */
        public void setVisible(bool visible)
        {
            EntityRenderer_SetVisible(nativePtr, visible);
        }

        /** @copydoc ParticleRenderer::_prepare */
        public void _prepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            EntityRenderer__prepare(nativePtr, technique.nativePtr);
        }

        /** @copydoc ParticleRenderer::_unprepare */
        public void _unprepare(ParticleTechnique technique)
        {
            if (technique == null)
                throw new ArgumentNullException("technique cannot be null!");
            EntityRenderer__unprepare(nativePtr, technique.nativePtr);
        }

        /** 
        */
        public void _updateRenderQueue(Mogre.RenderQueue queue, ParticlePool pool)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            if (pool == null)
                throw new ArgumentNullException("pool cannot be null!");
            EntityRenderer__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr, pool.nativePtr);
        }

        /** 
        */
        public void _notifyAttached(Mogre.Node parent, bool isTagPoint = false)
        {
            if (parent == null)
                throw new ArgumentNullException("parent cannot be null!");
            EntityRenderer__notifyAttached(nativePtr, (IntPtr)parent.NativePtr, isTagPoint);
        }

        /** @copydoc ParticleRenderer::_setMaterialName */
        public void _setMaterialName(String materialName)
        {
            EntityRenderer__setMaterialName(nativePtr, materialName);
        }

        /** 
        */
        public void _notifyCurrentCamera(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            EntityRenderer__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr);
        }

        /** 
        */
        public void _notifyParticleQuota(uint quota)
        {
            EntityRenderer__notifyParticleQuota(nativePtr, quota);
        }

        /** 
        */
        public void _notifyDefaultDimensions(float width, float height, float depth)
        {
            EntityRenderer__notifyDefaultDimensions(nativePtr, width, height, depth);
        }

        /** 
        */
        public void _notifyParticleResized()
        {
            EntityRenderer__notifyParticleResized(nativePtr);
        }

        /** 
        */
        public void _notifyParticleZRotated()
        {
            EntityRenderer__notifyParticleZRotated(nativePtr);
        }

        /** Rotate the textures of eacht entity.
        */
        public void _rotateTexture(VisualParticle particle, Mogre.Entity entity)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            if (entity == null)
                throw new ArgumentNullException("entity cannot be null!");
            EntityRenderer__rotateTexture(nativePtr, particle.nativePtr, (IntPtr)entity.NativePtr);
        }

        /** 
        */
        public void setRenderQueueGroup(byte queueId)
        {
            EntityRenderer_SetRenderQueueGroup(nativePtr, queueId);
        }

        /** 
        */
        public Mogre.SortMode _getSortMode()
        {
            return EntityRenderer__GetSortMode(nativePtr);
        }

        /** 
        */
        public EntityOrientationTypes EntityOrientationType { get { return EntityRenderer_GetEntityOrientationType(nativePtr); } set { EntityRenderer_SetEntityOrientationType(nativePtr, value); } }

        /** 
        */
        public void CopyAttributesTo(ParticleRenderer renderer)
        {
            if (renderer == null)
                throw new ArgumentNullException("renderer cannot be null!");
            EntityRenderer_CopyAttributesTo(nativePtr, renderer.nativePtr);
        }


        #region EntityRenderer
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr EntityRenderer_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_GetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr EntityRenderer_GetMeshName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_SetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer_SetMeshName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string meshName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__destroyAll", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__destroyAll(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyStop", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyStop(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__rotateTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__rotateTexture(IntPtr ptr, IntPtr particle, IntPtr entity);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_GetEntityOrientationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern EntityRenderer.EntityOrientationTypes EntityRenderer_GetEntityOrientationType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_SetEntityOrientationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer_SetEntityOrientationType(IntPtr ptr, EntityRenderer.EntityOrientationTypes entityOrientationType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer_SetVisible(IntPtr ptr, bool visible);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__prepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__unprepare(IntPtr ptr, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__updateRenderQueue(IntPtr ptr, IntPtr queue, IntPtr pool);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyAttached", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyAttached(IntPtr ptr, IntPtr parent, bool isTagPoint = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__setMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__setMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string materialName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyParticleQuota", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyParticleQuota(IntPtr ptr, uint quota);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyDefaultDimensions(IntPtr ptr, float width, float height, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyParticleResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyParticleResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__notifyParticleZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer__notifyParticleZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_SetRenderQueueGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer_SetRenderQueueGroup(IntPtr ptr, byte queueId);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer__GetSortMode", CallingConvention = CallingConvention.Cdecl)]
        internal static extern Mogre.SortMode EntityRenderer__GetSortMode(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "EntityRenderer_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void EntityRenderer_CopyAttributesTo(IntPtr ptr, IntPtr renderer);
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
            EntityRenderer_Destroy(NativePointer);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
