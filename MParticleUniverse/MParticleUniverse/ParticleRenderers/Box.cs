using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    public unsafe class Box : IDisposable
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
            Box_Destroy(nativePtr);
            rendererInstances.Remove(nativePtr);
        }

        #endregion


        internal static Dictionary<IntPtr, Box> rendererInstances;

        internal static Box GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, Box>();

            Box newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Box(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal Box(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public Mogre.Quaternion GetOrientation()
        {
            Mogre.Quaternion vec = *(((Mogre.Quaternion*)Box_GetOrientation(nativePtr).ToPointer()));
            return vec;
        }

        public BoxSet GetParentSet()
        {
            return BoxSet.GetInstance(Box_GetParentSet(nativePtr));
        }

        public Box()
        {
            nativePtr = Box_New();
            rendererInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// Constructor as called by BoxSet.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="owner"></param>
        public Box(Mogre.Vector3 position, BoxSet owner)
        {
            if (position == null)
                throw new ArgumentNullException("position cannot be null!");
            if (owner == null)
                nativePtr = Box_New2(position, IntPtr.Zero);
            else
                nativePtr = Box_New2(position, owner.NativePointer);
            rendererInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// Return the boxcorner in local space of a specified corner.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Mogre.Vector3 GetCorner(uint index)
        {
            Mogre.Vector3 vec = *(((Mogre.Vector3*)Box_GetCorner(nativePtr, index).ToPointer()));
            return vec;
        }

        /// <summary>
        /// Return the boxcorner in world space of a specified corner.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Mogre.Vector3 GetWorldspaceCorner(uint index)
        {
            Mogre.Vector3 vec = *(((Mogre.Vector3*)Box_GetWorldspaceCorner(nativePtr, index).ToPointer()));
            return vec;
        }

        public void SetPosition(float x, float y, float z)
        {
            Box_SetPosition2(nativePtr, x, y, z);
        }

        public Mogre.Vector3 Position
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Box_GetPosition(nativePtr).ToPointer()));
                return vec;
            }
            set { Box_SetPosition(nativePtr, value);}
        }

        public Mogre.ColourValue Colour
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)Box_GetColour(nativePtr).ToPointer()));
                return vec;
            }
            set { Box_SetColour(nativePtr, ref value);}
        }

        public void ResetDimensions()
        {
            Box_ResetDimensions(nativePtr);
        }

        public void SetDimensions(float width, float height, float depth)
        {
            Box_SetDimensions(nativePtr, width, height, depth);
        }

        public bool HasOwnDimensions()
        {
            return Box_HasOwnDimensions(nativePtr);
        }

        public float GetOwnWidth()
        {
            return Box_GetOwnWidth(nativePtr);
        }

        public float GetOwnHeight()
        {
            return Box_GetOwnHeight(nativePtr);
        }

        public float GetOwnDepth()
        {
            return Box_GetOwnDepth(nativePtr);
        }

        public void _notifyOwner(BoxSet owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner cannot be null!");
            Box__notifyOwner(nativePtr, owner.NativePointer);
        }

        #region P/Invoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetPosition(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetColour(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetOrientation(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetParentSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetParentSet(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_New2(Mogre.Vector3 position, IntPtr owner);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetCorner", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetCorner(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetWorldspaceCorner", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetWorldspaceCorner(IntPtr ptr, uint index);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_SetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box_SetPosition(IntPtr ptr, Mogre.Vector3 position);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_SetPosition2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box_SetPosition2(IntPtr ptr, float x, float y, float z);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetPosition2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetPosition2(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_SetColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box_SetColour(IntPtr ptr, ref Mogre.ColourValue colour);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetColour2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Box_GetColour2(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_ResetDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box_ResetDimensions(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_SetDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box_SetDimensions(IntPtr ptr, float width, float height, float depth);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_HasOwnDimensions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Box_HasOwnDimensions(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetOwnWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Box_GetOwnWidth(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetOwnHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Box_GetOwnHeight(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box_GetOwnDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Box_GetOwnDepth(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "Box__notifyOwner", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Box__notifyOwner(IntPtr ptr, IntPtr owner);

        #endregion
    }
}
