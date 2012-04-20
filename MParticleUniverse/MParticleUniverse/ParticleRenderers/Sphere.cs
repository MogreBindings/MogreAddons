using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    public unsafe class Sphere : IDisposable
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

        internal static Dictionary<IntPtr, Sphere> rendererInstances;

        internal static Sphere GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, Sphere>();

            Sphere newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Sphere(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal Sphere(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public Sphere()
        {
            nativePtr = Sphere_New();
            rendererInstances.Add(nativePtr, this);
        }
        public Sphere(Mogre.Vector3 position, SphereSet owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner cannot be null!");
            if (position == null)
                throw new ArgumentNullException("position cannot be null!");
            nativePtr = Sphere_New(position, owner.nativePtr);
            rendererInstances.Add(nativePtr, this);
        }

        public Mogre.Vector3 Position
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)Sphere_GetmPosition(nativePtr).ToPointer()));
                return vec;
            }
            set { Sphere_SetmPosition(nativePtr, value); }
        }
        public Mogre.ColourValue Colour
        {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)Sphere_GetmColour(nativePtr).ToPointer()));
                return vec;
            }
            set { Sphere_SetmColour(nativePtr, ref value); }
        }
        public Mogre.Quaternion Orientation
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)Sphere_GetmOrientation(nativePtr).ToPointer()));
                return vec;
            }
            set { Sphere_SetmOrientation(nativePtr, value); }
        }
        public SphereSet ParentSet
        {
            get { return SphereSet.GetInstance(Sphere_GetmParentSet(nativePtr)); }
            set { 
                if (value == null)
                    Sphere_SetmParentSet(nativePtr, IntPtr.Zero); 
                else
                    Sphere_SetmParentSet(nativePtr, value.nativePtr); 
            }
        }

        public void SetPosition(float x, float y, float z)
        {
            Sphere_SetPosition(nativePtr, x, y, z);
        }

        public void ResetRadius()
        {
            Sphere_ResetRadius(nativePtr);
        }

        public void SetRadius(float radius)
        {
            Sphere_SetRadius(nativePtr, radius);
        }

        public bool HasOwnRadius()
        {
            return Sphere_HasOwnRadius(nativePtr);
        }

        public float GetOwnRadius()
        {
            return Sphere_GetOwnRadius(nativePtr);
        }

        public void _notifyOwner(SphereSet owner)
        {
            if (owner == null)
                throw new ArgumentNullException("owner cannot be null!");
            Sphere__notifyOwner(nativePtr, owner.nativePtr);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetmPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_GetmPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetmPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetmPosition(IntPtr ptr, Mogre.Vector3 newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetmColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_GetmColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetmColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetmColour(IntPtr ptr, ref Mogre.ColourValue newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetmOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_GetmOrientation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetmOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetmOrientation(IntPtr ptr, Mogre.Quaternion newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetmParentSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_GetmParentSet(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetmParentSet", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetmParentSet(IntPtr ptr, IntPtr newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_New(Mogre.Vector3 position, IntPtr owner);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetPosition(IntPtr ptr, Mogre.Vector3 position);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetPosition2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetPosition(IntPtr ptr, float x, float y, float z);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_GetPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetColour(IntPtr ptr, ref Mogre.ColourValue colour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Sphere_GetColour(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_ResetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_ResetRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_SetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere_SetRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_HasOwnRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool Sphere_HasOwnRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere_GetOwnRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float Sphere_GetOwnRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Sphere__notifyOwner", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Sphere__notifyOwner(IntPtr ptr, IntPtr owner);

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
            Sphere_Destroy(nativePtr);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

    }
}