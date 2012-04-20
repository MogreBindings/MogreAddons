using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// The BoxSet and Box classes are comparable with the BillboardSet and Billboard classes in Ogre, instead
    /// 	Boxes are rendered.
    /// <remarks>
    /// 	The uv mapping is done like this:
    /// 
    /// |---|---|
    /// | 1 | 4 |
    /// |---|---|
    /// | 2 | 5 |
    /// |---|---|
    /// | 3 | 6 |
    /// |---|---|
    /// 
    /// </remarks>
    /// </summary>
    public unsafe class BoxSet : Mogre.MovableObject, Mogre.IRenderable, PrimitiveShapeSet, IDisposable
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

        internal static Dictionary<IntPtr, BoxSet> rendererInstances;

        internal static BoxSet GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, BoxSet>();

            BoxSet newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BoxSet(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal BoxSet(IntPtr ptr)
            : base((CLRObject*)ptr.ToPointer())
        {
            nativePtr = ptr;
        }

        public BoxSet()
            : base((CLRObject*)BoxSet_New())
        {
            nativePtr = (IntPtr)base.NativePtr;
            rendererInstances.Add(nativePtr, this);
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
            BoxSet_Destroy(nativePtr);
            rendererInstances.Remove(nativePtr);
        }

        #endregion


        #region PrimitiveShapeSet Functionality

        public bool ZRotated { get { return BoxSet_IsZRotated(nativePtr);} set { BoxSet_SetZRotated(nativePtr, value);} }
        public void _notifyZRotated() { BoxSet__notifyZRotated(nativePtr); }
        public String MaterialName { get { return Marshal.PtrToStringAnsi(BoxSet_GetMaterialName(nativePtr)); } set { BoxSet_SetMaterialName(nativePtr, value); } }
        public void _notifyResized() { BoxSet__notifyResized(nativePtr); }
        public void _notifyCurrentCamera(Mogre.Camera cam) { BoxSet__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr); }
        public Mogre.AxisAlignedBox BoundingBox
        {
            get
            {
                IntPtr scalePtr = BoxSet_GetBoundingBox(nativePtr);
                if (scalePtr == null || scalePtr == IntPtr.Zero)
                    return null;
                Mogre.AxisAlignedBox aabtype = new Mogre.AxisAlignedBox();
                Mogre.AxisAlignedBox vec = (Mogre.AxisAlignedBox)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
                return vec;
            }
        }
        public float BoundingRadius { get { return BoxSet_GetBoundingRadius(nativePtr); } }
        public Mogre.MaterialPtr Material
        {
            get
            {
                IntPtr mat = BoxSet_GetMaterial(nativePtr);
                if (mat == null || mat == IntPtr.Zero)
                    return null;
                return (Mogre.MaterialPtr)Marshal.PtrToStructure(mat, typeof(Mogre.MaterialPtr));
        } }
        public Mogre.Matrix4 WorldTransforms
        {
            get
            {
                IntPtr mat = BoxSet_GetWorldPosition(nativePtr);
                if (mat == null || mat == IntPtr.Zero)
                    return null;
                Mogre.Matrix4 m4 = new Mogre.Matrix4();
                Marshal.PtrToStructure(mat, m4);
                //Mogre.Matrix4 vec = *(((Mogre.Matrix4*)BoxSet_GetWorldPosition(nativePtr).ToPointer()));
                return m4;
            }
        }
        public Mogre.Quaternion WorldOrientation
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)BoxSet_GetWorldOrientation(nativePtr).ToPointer()));
                return vec;
            }
        }
        public Mogre.Vector3 WorldPosition
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)BoxSet_GetWorldPosition(nativePtr).ToPointer()));
                return vec;
            }
        }
        public bool CullIndividually { get { return BoxSet_IsCullIndividually(nativePtr); } set { BoxSet_SetCullIndividually(nativePtr, value); } }
        //public float GetSquaredViewDepth(Mogre.Camera cam) { return BoxSet_GetSquaredViewDepth(nativePtr, (IntPtr)cam.NativePtr); }
        public Mogre.LightList Lights
        {
            get
            {
                IntPtr lightptr = BoxSet_GetLights(nativePtr);
                if (lightptr == null || lightptr == IntPtr.Zero)
                    return null;
                return (Mogre.LightList)Marshal.PtrToStructure(lightptr, typeof(Mogre.LightList));
            }
        }
        public uint TypeFlags { get { return BoxSet_GetTypeFlags(nativePtr); } }
        public void RotateTexture(float speed) { BoxSet_RotateTexture(nativePtr, speed); }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetZRotated(IntPtr ptr, bool zRotated);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_IsZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_IsZRotated(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet__notifyZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet__notifyZRotated(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetMaterialName(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet__notifyResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet__notifyResized(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetBoundingBox(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetBoundingRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxSet_GetBoundingRadius(IntPtr ptr);
//[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetMaterial", CallingConvention = CallingConvention.Cdecl)]
//        internal static extern IntPtr BoxSet_GetMaterial(IntPtr ptr);
//[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetWorldTransforms", CallingConvention = CallingConvention.Cdecl)]
//        internal static extern void BoxSet_GetWorldTransforms(IntPtr ptr, IntPtr xform);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetWorldOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetWorldOrientation(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetWorldPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetWorldPosition(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_IsCullIndividually", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_IsCullIndividually(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetCullIndividually", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetCullIndividually(IntPtr ptr, bool cullIndividual);
//[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetSquaredViewDepth", CallingConvention = CallingConvention.Cdecl)]
//        internal static extern float BoxSet_GetSquaredViewDepth(IntPtr ptr, IntPtr cam);
//[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetLights", CallingConvention = CallingConvention.Cdecl)]
//        internal static extern IntPtr BoxSet_GetLights(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetTypeFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint BoxSet_GetTypeFlags(IntPtr ptr);
[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_RotateTexture", CallingConvention = CallingConvention.Cdecl)]
internal static extern void BoxSet_RotateTexture(IntPtr ptr, float speed);
//[DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_VisitRenderables", CallingConvention = CallingConvention.Cdecl)]
//        internal static extern void BoxSet_VisitRenderables(IntPtr ptr, IntPtr visitor, bool debugRenderables = false)
        #endregion

        #endregion


        #region IRenderable Functionality

        public bool CastsShadows { get { return BoxSet_GetCastShadows(nativePtr); } }
        public ushort NumWorldTransforms { get { return BoxSet_GetNumWorldTransforms(nativePtr); } }
        public bool PolygonModeOverrideable 
        {
            get { return BoxSet_GetPolygonModeOverrideable(nativePtr); }
            set { BoxSet_SetPolygonModeOverrideable(nativePtr, value); }
        }
        public Mogre.Technique Technique
        {
            get
            {
                IntPtr tecptr = BoxSet_GetTechnique(nativePtr);
                if (tecptr == null || tecptr == IntPtr.Zero)
                    return null;
                return (Mogre.Technique)Marshal.PtrToStructure(tecptr, typeof(Mogre.Technique));
            }
        }

        public Ogre.Renderable* _GetNativePtr() { return (Ogre.Renderable*)nativePtr.ToPointer(); }
        public void _updateCustomGpuParameter(Mogre.GpuProgramParameters.AutoConstantEntry_NativePtr constantEntry, Mogre.GpuProgramParameters @parameters)
        {
            IntPtr param = Marshal.AllocHGlobal(Marshal.SizeOf(parameters));
            Marshal.StructureToPtr(parameters, param, true);
            BoxSet__updateCustomGpuParameter(nativePtr, constantEntry.NativePtr, param);
        }
        public Mogre.Const_LightList GetLights()
        {
            IntPtr lightptr = BoxSet_GetLights(nativePtr);
            if (lightptr == null || lightptr == IntPtr.Zero)
                return null;
            return (Mogre.Const_LightList)Marshal.PtrToStructure(lightptr, typeof(Mogre.Const_LightList));
        }

        public Mogre.MaterialPtr GetMaterial() { return (Mogre.MaterialPtr)Marshal.PtrToStructure(BoxSet_GetMaterial(nativePtr), typeof(Mogre.MaterialPtr)); }
        public float GetSquaredViewDepth(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            return BoxSet_GetSquaredViewDepth(nativePtr, (IntPtr)cam.NativePtr); 
        }
        public void GetWorldTransforms(Mogre.Matrix4.NativeValue* xform) 
        {
            if (xform == null)
                throw new ArgumentNullException("xform cannot be null!");
            BoxSet_GetWorldTransforms(nativePtr, new IntPtr(xform));
        }
        
        public void GetRenderOperation(Mogre.RenderOperation rop)
        {
            if (rop == null)
                throw new ArgumentNullException("rop cannot be null!");
            IntPtr ropPtr = Marshal.AllocHGlobal(Marshal.SizeOf(rop));
            Marshal.StructureToPtr(rop, ropPtr, true);
            BoxSet_GetRenderOperation(nativePtr, ropPtr);
            Mogre.RenderOperation roptype = new Mogre.RenderOperation();
            rop = (Mogre.RenderOperation)(Marshal.PtrToStructure(ropPtr, roptype.GetType()));
        }

        public void PostRender(Mogre.SceneManager sm, Mogre.RenderSystem rsys)
        {
            if (sm == null)
                throw new ArgumentNullException("sm cannot be null!");
            if (rsys == null)
                throw new ArgumentNullException("rsys cannot be null!");
            BoxSet_PostRender(nativePtr, (IntPtr)sm.NativePtr, (IntPtr)rsys.NativePtr);
        }
        public bool PreRender(Mogre.SceneManager sm, Mogre.RenderSystem rsys)
        {
            if (sm == null)
                throw new ArgumentNullException("sm cannot be null!");
            if (rsys == null)
                throw new ArgumentNullException("rsys cannot be null!");
            return BoxSet_PreRender(nativePtr, (IntPtr)sm.NativePtr, (IntPtr)rsys.NativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetCastShadows", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_GetCastShadows(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetNumWorldTransforms", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort BoxSet_GetNumWorldTransforms(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetPolygonModeOverrideable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_GetPolygonModeOverrideable(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetPolygonModeOverrideable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetPolygonModeOverrideable(IntPtr ptr, bool overrideable);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet__updateCustomGpuParameter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet__updateCustomGpuParameter(IntPtr ptr, 
            IntPtr constantEntry,
            IntPtr paramiters);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetLights", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetLights(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetMaterial", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetMaterial(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetRenderOperation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetRenderOperation(IntPtr ptr, IntPtr op);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetSquaredViewDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxSet_GetSquaredViewDepth(IntPtr ptr, IntPtr cam);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetWorldTransforms", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_GetWorldTransforms(IntPtr ptr, IntPtr xform);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_PreRender", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_PreRender(IntPtr ptr, IntPtr sm, IntPtr rsys);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_PostRender", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_PostRender(IntPtr ptr, IntPtr sm, IntPtr rsys);

        #endregion

        #endregion


        #region BoxSet Functionality

        /// <summary>
        /// Creates a new box and adds it to this set.
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Box CreateBox(Mogre.Vector3 position)
        {
            if (position == null)
                throw new ArgumentNullException("position cannot be null!");
            return Box.GetInstance(BoxSet_CreateBox(nativePtr, position));
        }

        /// <summary>
        /// Creates a new box and adds it to this set.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public Box CreateBox(float x, float y, float z)
        {
            return Box.GetInstance(BoxSet_CreateBox2(nativePtr, x, y, z));
        }

        /// <summary>
        /// Returns the number of active boxes which currently make up this set.
        /// </summary>
        /// <returns></returns>
        public uint GetNumBoxes()
        {
            return BoxSet_GetNumBoxes(nativePtr);
        }

        /// <summary>
        /// Returns true if the box pool automatically extends.
        /// Tells the set whether to allow automatic extension of the pool of boxes.
        /// </summary>
        public bool Autoextend
        {
            get { return BoxSet_IsAutoextend(nativePtr); }
            set { BoxSet_SetAutoextend(nativePtr, value); }
        }

        /// <summary>
        /// Get - Returns the current size of the box pool.
        /// Set - Adjusts the size of the pool of boxes available in this set.
        /// </summary>
        public uint PoolSize
        {
            get { return BoxSet_GetPoolSize(nativePtr); }
            set { BoxSet_SetPoolSize(nativePtr, value); }
        }

        /// <summary>
        /// Empties this set of all boxes.
        /// </summary>
        public void Clear()
        {
            BoxSet_Clear(nativePtr);
        }

        /// <summary>
        /// Returns a pointer to the box at the supplied index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Box GetBox(uint index)
        {
            return Box.GetInstance(BoxSet_GetBox(nativePtr, index));
        }

        /// <summary>
        /// Removes the box at the supplied index.
        /// </summary>
        /// <param name="index"></param>
        public void RemoveBox(uint index)
        {
            BoxSet_RemoveBox(nativePtr, index);
        }

        /// <summary>
        /// Removes a box from the set.
        /// </summary>
        /// <param name="box"></param>
        public void RemoveBox(Box box)
        {
            if (box == null)
                return;
            BoxSet_RemoveBox2(nativePtr, box.NativePointer);
        }

        /// <summary>
        /// Sets the default dimensions of the boxes in this set.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        public void SetDefaultDimensions(float width, float height, float depth)
        {
            BoxSet_SetDefaultDimensions(nativePtr, width, height, depth);
        }

        /// <summary>
        /// Sets the default Width of the boxes in this set.
        /// </summary>
        public float DefaultWidth
        {
            get { return BoxSet_GetDefaultWidth(nativePtr); }
            set { BoxSet_SetDefaultWidth(nativePtr, value); }
        }

        /// <summary>
        /// Sets the default Height of the boxes in this set.
        /// </summary>
        public float DefaultHeight
        {
            get { return BoxSet_GetDefaultHeight(nativePtr); }
            set { BoxSet_SetDefaultHeight(nativePtr, value); }
        }

        /// <summary>
        /// Sets the default Depth of the boxes in this set.
        /// </summary>
        public float DefaultDepth
        {
            get { return BoxSet_GetDefaultDepth(nativePtr); }
            set { BoxSet_SetDefaultDepth(nativePtr, value); }
        }

        /// <summary>
        /// Begin injection of box data; applicable when constructing the BoxSet for external data use.
        /// </summary>
        /// <param name="numBoxes"></param>
        public void BeginBoxes(uint numBoxes = 0)
        {
            BoxSet_BeginBoxes(nativePtr, numBoxes);
        }

        /// <summary>
        /// Define a box.
        /// </summary>
        /// <param name="bb"></param>
        public void InjectBox(Box bb)
        {
            if (bb == null)
                throw new ArgumentNullException("bb cannot be null");
            BoxSet_InjectBox(nativePtr, bb.NativePointer);
        }

        /// <summary>
        /// Finish defining boxes.
        /// </summary>
        public void EndBoxes()
        {
            BoxSet_EndBoxes(nativePtr);
        }

        /// <summary>
        /// Overridden from MovableObject
        /// </summary>
        /// <param name="box"></param>
        /// <param name="radius"></param>
        public void SetBounds(Mogre.AxisAlignedBox box, float radius)
        {
            if (box == null)
                throw new ArgumentNullException("box cannot be null");
            
            IntPtr boxPtr = Marshal.AllocHGlobal(Marshal.SizeOf(box));
            Marshal.StructureToPtr(box, boxPtr, true);
            BoxSet_SetBounds(nativePtr, boxPtr, radius);
        }

        /// <summary>
        /// Overridden from MovableObject
        /// </summary>
        /// <param name="queue"></param>
        public void _updateRenderQueue(Mogre.RenderQueue queue)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null");
            BoxSet__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr);
        }

        /// <summary>
        /// Overridden from MovableObject 
        /// </summary>
        public String MovableType
        {
            get { return Marshal.PtrToStringAnsi(BoxSet_GetMovableType(nativePtr)); }
        }

        /// <summary>
        /// Update the bounds of the box set
        /// </summary>
        public void _updateBounds()
        {
            BoxSet__updateBounds(nativePtr);
        }

        /// <summary>
        /// Sets whether boxes should be treated as being in world space.
        /// </summary>
        /// <param name="ws"></param>
        public void SetBoxesInWorldSpace(bool ws)
        {
            BoxSet_SetBoxesInWorldSpace(nativePtr, ws);
        }


        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_CreateBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_CreateBox(IntPtr ptr, Mogre.Vector3 position);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_CreateBox2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_CreateBox2(IntPtr ptr, float x, float y, float z);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetNumBoxes", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint BoxSet_GetNumBoxes(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetAutoextend", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetAutoextend(IntPtr ptr, bool autoextend);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_IsAutoextend", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxSet_IsAutoextend(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetPoolSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetPoolSize(IntPtr ptr, uint size);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetPoolSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint BoxSet_GetPoolSize(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_Clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_Clear(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetBox(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_RemoveBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_RemoveBox(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_RemoveBox2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_RemoveBox2(IntPtr ptr, IntPtr box);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetDefaultDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetDefaultDimensions(IntPtr ptr, float width, float height, float depth);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetDefaultWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetDefaultWidth(IntPtr ptr, float width);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetDefaultWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxSet_GetDefaultWidth(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetDefaultHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetDefaultHeight(IntPtr ptr, float height);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetDefaultHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxSet_GetDefaultHeight(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetDefaultDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetDefaultDepth(IntPtr ptr, float depth);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetDefaultDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxSet_GetDefaultDepth(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_BeginBoxes", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_BeginBoxes(IntPtr ptr, uint numBoxes = 0);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_InjectBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_InjectBox(IntPtr ptr, IntPtr bb);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_EndBoxes", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_EndBoxes(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetBounds(IntPtr ptr, IntPtr box, float radius);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet__updateRenderQueue(IntPtr ptr, IntPtr queue);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_GetMovableType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSet_GetMovableType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet__updateBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet__updateBounds(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSet_SetBoxesInWorldSpace", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSet_SetBoxesInWorldSpace(IntPtr ptr, bool ws);

        #endregion
        #endregion


    }

    public unsafe class BoxSetFactory : IDisposable
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

        internal static Dictionary<IntPtr, BoxSetFactory> rendererInstances;

        internal static BoxSetFactory GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, BoxSetFactory>();

            BoxSetFactory newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BoxSetFactory(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal BoxSetFactory(IntPtr ptr)
        {
            nativePtr = ptr;
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
            BoxSetFactory_Destroy(nativePtr);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

        public BoxSetFactory()
        {
            nativePtr = BoxSetFactory_New();
        }
        public static String PU_FACTORY_TYPE_NAME
        {
            get { return Marshal.PtrToStringAnsi(BoxSetFactory_GetPU_FACTORY_TYPE_NAME()); }
            set { BoxSetFactory_SetPU_FACTORY_TYPE_NAME(value); }
        }
        public String GetType()
        {
            return Marshal.PtrToStringAnsi(BoxSetFactory_GetType(nativePtr));
        }
        public void DestroyInstance(Mogre.MovableObject obj)
        {
            if (obj == null)
                throw new ArgumentNullException("obj cannot be null");
            BoxSetFactory_DestroyInstance(nativePtr, (IntPtr)obj.NativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSetFactory_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSetFactory_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSetFactory_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSetFactory_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSetFactory_GetPU_FACTORY_TYPE_NAME", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSetFactory_GetPU_FACTORY_TYPE_NAME();

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSetFactory_SetPU_FACTORY_TYPE_NAME", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSetFactory_SetPU_FACTORY_TYPE_NAME([MarshalAs(UnmanagedType.LPStr)]string value);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSetFactory_GetType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxSetFactory_GetType(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxSetFactory_DestroyInstance", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxSetFactory_DestroyInstance(IntPtr ptr, IntPtr obj);
        #endregion

    }
}
