using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    public unsafe class SphereSet : Mogre.MovableObject, Mogre.IRenderable, PrimitiveShapeSet, IDisposable
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

        internal static Dictionary<IntPtr, SphereSet> rendererInstances;

        internal static SphereSet GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, SphereSet>();

            SphereSet newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SphereSet(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal SphereSet(IntPtr ptr)
            : base((CLRObject*)ptr.ToPointer())
        {
            nativePtr = ptr;
        }

        public SphereSet(String name, uint poolSize = 20, bool externalData = false)
            : base((CLRObject*)SphereSet_New(name, poolSize, externalData))
        {
            nativePtr = (IntPtr)base.NativePtr;
            rendererInstances.Add(nativePtr, this);
        }
        public SphereSet()
            : base((CLRObject*)SphereSet_New())
        {
            nativePtr = (IntPtr)base.NativePtr;
            rendererInstances.Add(nativePtr, this);
        }

        public Sphere CreateSphere(Mogre.Vector3 position)
        {
            if (position == null)
                throw new ArgumentNullException("position cannot be null!");
            return Sphere.GetInstance(SphereSet_CreateSphere(nativePtr, position));
        }
        public Sphere CreateSphere(float x, float y, float z)
        {
            return Sphere.GetInstance(SphereSet_CreateSphere(nativePtr, x, y, z));
        }
        public uint GetNumSpheres()
        {
            return SphereSet_GetNumSpheres(nativePtr);
        }
        public bool Autoextend
        {
            get { return SphereSet_IsAutoextend(nativePtr); }
            set { SphereSet_SetAutoextend(nativePtr, value); }
        }
        public uint PoolSize
        {
            get { return SphereSet_GetPoolSize(nativePtr); }
            set { SphereSet_SetPoolSize(nativePtr, value); }
        }
        public void Clear()
        {
            SphereSet_Clear(nativePtr);
        }
        public Sphere GetSphere(uint index)
        {
            return Sphere.GetInstance(SphereSet_GetSphere(nativePtr, index));
        }
        public void RemoveSphere(uint index)
        {
            SphereSet_RemoveSphere(nativePtr, index);
        }
        public void RemoveSphere(Sphere sphere)
        {
            if (sphere == null)
                return;
            SphereSet_RemoveSphere(nativePtr, sphere.NativePointer);
        }
        public float DefaultRadius
        {
            get { return SphereSet_GetDefaultRadius(nativePtr); }
            set { SphereSet_SetDefaultRadius(nativePtr, value); }
        }
        public uint NumberOfRings
        {
            get { return SphereSet_GetNumberOfRings(nativePtr); }
            set { SphereSet_SetNumberOfRings(nativePtr, value); }
        }
        public uint NumberOfSegments
        {
            get { return SphereSet_GetNumberOfSegments(nativePtr); }
            set { SphereSet_SetNumberOfSegments(nativePtr, value); }
        }
        public void BeginSpheres(uint numSpheres = 0)
        {
            SphereSet_BeginSpheres(nativePtr, numSpheres);
        }
        public void InjectSphere(Sphere sphere)
        {
            if (sphere == null)
                throw new ArgumentNullException("sphere cannot be null!");
            SphereSet_InjectSphere(nativePtr, sphere.NativePointer);
        }
        public void EndSpheres()
        {
            SphereSet_EndSpheres(nativePtr);
        }
        public void SetBounds(Mogre.AxisAlignedBox box, float radius)
        {
            if (box == null)
                throw new ArgumentNullException("box cannot be null!");
            IntPtr boxPtr = Marshal.AllocHGlobal(Marshal.SizeOf(box));
            Marshal.StructureToPtr(box, boxPtr, true);
            SphereSet_SetBounds(nativePtr, boxPtr, radius);
        }
        public void _updateRenderQueue(Mogre.RenderQueue queue)
        {
            if (queue == null)
                throw new ArgumentNullException("queue cannot be null!");
            SphereSet__updateRenderQueue(nativePtr, (IntPtr)queue.NativePtr);
        }
        public String MovableType
        {
            get { return Marshal.PtrToStringAnsi(SphereSet_GetMovableType(nativePtr)); }
        }
        public void _updateBounds()
        {
            SphereSet__updateBounds(nativePtr);
        }
        public void SetSpheresInWorldSpace(bool ws)
        {
            SphereSet_SetSpheresInWorldSpace(nativePtr, ws);
        }



        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_New([MarshalAs(UnmanagedType.LPStr)]string name, uint poolSize = 20, bool externalData = false);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_CreateSphere", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_CreateSphere(IntPtr ptr, Mogre.Vector3 position);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_CreateSphere", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_CreateSphere(IntPtr ptr, float x, float y, float z);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetNumSpheres", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint SphereSet_GetNumSpheres(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetAutoextend", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetAutoextend(IntPtr ptr, bool autoextend);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_IsAutoextend", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_IsAutoextend(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetPoolSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetPoolSize(IntPtr ptr, uint size);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetPoolSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint SphereSet_GetPoolSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_Clear", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_Clear(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetSphere", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetSphere(IntPtr ptr, uint index);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_RemoveSphere", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_RemoveSphere(IntPtr ptr, uint index);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_RemoveSphere", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_RemoveSphere(IntPtr ptr, IntPtr sphere);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetDefaultRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetDefaultRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetDefaultRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SphereSet_GetDefaultRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetNumberOfRings", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetNumberOfRings(IntPtr ptr, uint numberOfRings);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetNumberOfRings", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint SphereSet_GetNumberOfRings(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetNumberOfSegments", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetNumberOfSegments(IntPtr ptr, uint numberOfSegments);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetNumberOfSegments", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint SphereSet_GetNumberOfSegments(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_BeginSpheres", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_BeginSpheres(IntPtr ptr, uint numSpheres = 0);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_InjectSphere", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_InjectSphere(IntPtr ptr, IntPtr sphere);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_EndSpheres", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_EndSpheres(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetBounds(IntPtr ptr, IntPtr box, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet__updateRenderQueue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet__updateRenderQueue(IntPtr ptr, IntPtr queue);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetRenderOperation", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void SphereSet_GetRenderOperation(IntPtr ptr, IntPtr op);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetMovableType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetMovableType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet__updateBounds", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet__updateBounds(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetSpheresInWorldSpace", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetSpheresInWorldSpace(IntPtr ptr, bool ws);

        #endregion

        #region PrimitiveShapeSet Functionality

        public bool ZRotated { get { return SphereSet_IsZRotated(nativePtr); } set { SphereSet_SetZRotated(nativePtr, value); } }
        public void _notifyZRotated() { SphereSet__notifyZRotated(nativePtr); }
        public String MaterialName { get { return Marshal.PtrToStringAnsi(SphereSet_GetMaterialName(nativePtr)); } set { SphereSet_SetMaterialName(nativePtr, value); } }
        public void _notifyResized() { SphereSet__notifyResized(nativePtr); }
        public void _notifyCurrentCamera(Mogre.Camera cam) { SphereSet__notifyCurrentCamera(nativePtr, (IntPtr)cam.NativePtr); }
        public Mogre.AxisAlignedBox BoundingBox
        {
            get
            {
                IntPtr scalePtr = SphereSet_GetBoundingBox(nativePtr);
                if (scalePtr == null || scalePtr == IntPtr.Zero)
                    return null;
                Mogre.AxisAlignedBox aabtype = new Mogre.AxisAlignedBox();
                Mogre.AxisAlignedBox vec = (Mogre.AxisAlignedBox)(Marshal.PtrToStructure(scalePtr, aabtype.GetType()));
                return vec;
            }
        }
        public float BoundingRadius { get { return SphereSet_GetBoundingRadius(nativePtr); } }
        public Mogre.MaterialPtr Material
        {
            get
            {
                return (Mogre.MaterialPtr)Marshal.PtrToStructure(SphereSet_GetMaterial(nativePtr), typeof(Mogre.MaterialPtr));
            }
        }
        public Mogre.Matrix4 WorldTransforms
        {
            get
            {
                Mogre.Matrix4 m4 = new Mogre.Matrix4();
                Marshal.PtrToStructure(SphereSet_GetWorldPosition(nativePtr), m4);
                //Mogre.Matrix4 vec = *(((Mogre.Matrix4*)SphereSet_GetWorldPosition(nativePtr).ToPointer()));
                return m4;
            }
        }
        public Mogre.Quaternion WorldOrientation
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)SphereSet_GetWorldOrientation(nativePtr).ToPointer()));
                return vec;
            }
        }
        public Mogre.Vector3 WorldPosition
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)SphereSet_GetWorldPosition(nativePtr).ToPointer()));
                return vec;
            }
        }
        public bool CullIndividually { get { return SphereSet_IsCullIndividually(nativePtr); } set { SphereSet_SetCullIndividually(nativePtr, value); } }
        //public float GetSquaredViewDepth(Mogre.Camera cam) { return SphereSet_GetSquaredViewDepth(nativePtr, (IntPtr)cam.NativePtr); }
        public Mogre.LightList Lights { get { return (Mogre.LightList)Marshal.PtrToStructure(SphereSet_GetLights(nativePtr), typeof(Mogre.LightList)); } }
        public uint TypeFlags { get { return SphereSet_GetTypeFlags(nativePtr); } }
        public void RotateTexture(float speed) { SphereSet_RotateTexture(nativePtr, speed); }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetZRotated(IntPtr ptr, bool zRotated);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_IsZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_IsZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet__notifyZRotated", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet__notifyZRotated(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetMaterialName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string name);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetMaterialName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetMaterialName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet__notifyResized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet__notifyResized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet__notifyCurrentCamera", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet__notifyCurrentCamera(IntPtr ptr, IntPtr cam);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetBoundingBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetBoundingBox(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetBoundingRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SphereSet_GetBoundingRadius(IntPtr ptr);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetMaterial", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern IntPtr SphereSet_GetMaterial(IntPtr ptr);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetWorldTransforms", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern void SphereSet_GetWorldTransforms(IntPtr ptr, IntPtr xform);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetWorldOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetWorldOrientation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetWorldPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetWorldPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_IsCullIndividually", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_IsCullIndividually(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetCullIndividually", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetCullIndividually(IntPtr ptr, bool cullIndividual);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetSquaredViewDepth", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern float SphereSet_GetSquaredViewDepth(IntPtr ptr, IntPtr cam);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetLights", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern IntPtr SphereSet_GetLights(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetTypeFlags", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint SphereSet_GetTypeFlags(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_RotateTexture", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_RotateTexture(IntPtr ptr, float speed);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_VisitRenderables", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern void SphereSet_VisitRenderables(IntPtr ptr, IntPtr visitor, bool debugRenderables = false)
        #endregion

        #endregion


        #region IRenderable Functionality

        public bool CastsShadows { get { return SphereSet_GetCastShadows(nativePtr); } }
        public ushort NumWorldTransforms { get { return SphereSet_GetNumWorldTransforms(nativePtr); } }
        public bool PolygonModeOverrideable
        {
            get { return SphereSet_GetPolygonModeOverrideable(nativePtr); }
            set { SphereSet_SetPolygonModeOverrideable(nativePtr, value); }
        }
        public Mogre.Technique Technique
        {
            get
            {
                return (Mogre.Technique)Marshal.PtrToStructure(SphereSet_GetTechnique(nativePtr), typeof(Mogre.Technique));
            }
        }

        public Ogre.Renderable* _GetNativePtr() { return (Ogre.Renderable*)nativePtr.ToPointer(); }
        public void _updateCustomGpuParameter(Mogre.GpuProgramParameters.AutoConstantEntry_NativePtr constantEntry, Mogre.GpuProgramParameters @parameters)
        {
            IntPtr param = Marshal.AllocHGlobal(Marshal.SizeOf(parameters));
            Marshal.StructureToPtr(parameters, param, true);
            SphereSet__updateCustomGpuParameter(nativePtr, constantEntry.NativePtr, param);
        }
        public Mogre.Const_LightList GetLights()
        {
            return (Mogre.Const_LightList)Marshal.PtrToStructure(SphereSet_GetLights(nativePtr), typeof(Mogre.Const_LightList));
        }

        public Mogre.MaterialPtr GetMaterial() { return (Mogre.MaterialPtr)Marshal.PtrToStructure(SphereSet_GetMaterial(nativePtr), typeof(Mogre.MaterialPtr)); }
        public float GetSquaredViewDepth(Mogre.Camera cam)
        {
            if (cam == null)
                throw new ArgumentNullException("cam cannot be null!");
            return SphereSet_GetSquaredViewDepth(nativePtr, (IntPtr)cam.NativePtr);
        }
        public void GetWorldTransforms(Mogre.Matrix4.NativeValue* xform)
        {
            if (xform == null)
                throw new ArgumentNullException("xform cannot be null!");
            SphereSet_GetWorldTransforms(nativePtr, new IntPtr(xform));
        }

        public void GetRenderOperation(Mogre.RenderOperation rop)
        {
            if (rop == null)
                throw new ArgumentNullException("rop cannot be null!");
            IntPtr ropPtr = Marshal.AllocHGlobal(Marshal.SizeOf(rop));
            Marshal.StructureToPtr(rop, ropPtr, true);
            SphereSet_GetRenderOperation(nativePtr, ropPtr);
            Mogre.RenderOperation roptype = new Mogre.RenderOperation();
            rop = (Mogre.RenderOperation)(Marshal.PtrToStructure(ropPtr, roptype.GetType()));
        }

        public void PostRender(Mogre.SceneManager sm, Mogre.RenderSystem rsys)
        {
            if (sm == null)
                throw new ArgumentNullException("sm cannot be null!");
            if (rsys == null)
                throw new ArgumentNullException("rsys cannot be null!");
            SphereSet_PostRender(nativePtr, (IntPtr)sm.NativePtr, (IntPtr)rsys.NativePtr);
        }
        public bool PreRender(Mogre.SceneManager sm, Mogre.RenderSystem rsys)
        {
            if (sm == null)
                throw new ArgumentNullException("sm cannot be null!");
            if (rsys == null)
                throw new ArgumentNullException("rsys cannot be null!");
            return SphereSet_PreRender(nativePtr, (IntPtr)sm.NativePtr, (IntPtr)rsys.NativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetCastShadows", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_GetCastShadows(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetNumWorldTransforms", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort SphereSet_GetNumWorldTransforms(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetPolygonModeOverrideable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_GetPolygonModeOverrideable(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_SetPolygonModeOverrideable", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_SetPolygonModeOverrideable(IntPtr ptr, bool overrideable);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetTechnique(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet__updateCustomGpuParameter", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet__updateCustomGpuParameter(IntPtr ptr,
            IntPtr constantEntry,
            IntPtr paramiters);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetLights", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetLights(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetMaterial", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetMaterial(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetRenderOperation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereSet_GetRenderOperation(IntPtr ptr, IntPtr op);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetSquaredViewDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SphereSet_GetSquaredViewDepth(IntPtr ptr, IntPtr cam);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_GetWorldTransforms", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereSet_GetWorldTransforms(IntPtr ptr, IntPtr xform);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_PreRender", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_PreRender(IntPtr ptr, IntPtr sm, IntPtr rsys);

        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereSet_PostRender", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereSet_PostRender(IntPtr ptr, IntPtr sm, IntPtr rsys);

        #endregion

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
            SphereSet_Destroy(nativePtr);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}
