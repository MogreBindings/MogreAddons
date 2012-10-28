using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// A PhysicsActor is an element of the physical world and has no visual representation by itself. It only has physical properties. 
    //	The PhysicsActor is an abstraction class of the real underlying physics engine, such as PhysX, Havok or Bullet.
    /// </summary>
    public unsafe class PhysicsActor : IDisposable
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

        internal static Dictionary<IntPtr, PhysicsActor> physicsActorInstances;

        internal static PhysicsActor GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (physicsActorInstances == null)
                physicsActorInstances = new Dictionary<IntPtr, PhysicsActor>();

            PhysicsActor newvalue;

            if (physicsActorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new PhysicsActor(ptr);
            physicsActorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal PhysicsActor(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public PhysicsActor()
        {
            nativePtr = PhysicsActor_New();
            physicsActorInstances.Add(nativePtr, this);
        }

        // Public attributes
        public Mogre.Vector3 Position
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)PhysicsActor_GetPosition(nativePtr).ToPointer()));
                return vec;
            }
            set { PhysicsActor_SetPosition(nativePtr, value); }
        }
        public Mogre.Vector3 Direction
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)PhysicsActor_GetDirection(nativePtr).ToPointer()));
                return vec;
            }
            set { PhysicsActor_SetDirection(nativePtr, value); }
        }

        public Mogre.Quaternion Orientation
        {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)PhysicsActor_GetOrientation(nativePtr).ToPointer()));
                return vec;
            }
            set { PhysicsActor_SetOrientation(nativePtr, value); }
        }

        public float Mass
        {
            get
            {
                return PhysicsActor_GetMass(nativePtr);
            }
            set { PhysicsActor_SetMass(nativePtr, value); }
        }

        public ushort CollisionGroup
        {
            get
            {
                return PhysicsActor_GetCollisionGroup(nativePtr);
            }
            set { PhysicsActor_SetCollisionGroup(nativePtr, value); }
        }

        public Mogre.Vector3 ContactPoint
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)PhysicsActor_GetContactPoint(nativePtr).ToPointer()));
                return vec;
            }
            set { PhysicsActor_SetContactPoint(nativePtr, value); }
        }

        public Mogre.Vector3 ContactNormal
        {
            get
            {
                IntPtr cn = PhysicsActor_GetContactNormal(nativePtr);
                Mogre.Vector3 vec = *(((Mogre.Vector3*)cn.ToPointer()));
                return vec;
            }
            set { PhysicsActor_SetContactNormal(nativePtr, value); }
        }


        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PhysicsActor_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PhysicsActor_GetPosition(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetPosition(IntPtr ptr, Mogre.Vector3 newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PhysicsActor_GetDirection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetDirection(IntPtr ptr, Mogre.Vector3 newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PhysicsActor_GetOrientation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetOrientation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetOrientation(IntPtr ptr, Mogre.Quaternion newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float PhysicsActor_GetMass(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetMass", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetMass(IntPtr ptr, float newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetCollisionGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort PhysicsActor_GetCollisionGroup(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetCollisionGroup", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetCollisionGroup(IntPtr ptr, ushort newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetContactPoint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PhysicsActor_GetContactPoint(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetContactPoint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetContactPoint(IntPtr ptr, Mogre.Vector3 newVal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_GetContactNormal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PhysicsActor_GetContactNormal(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PhysicsActor_SetContactNormal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PhysicsActor_SetContactNormal(IntPtr ptr, Mogre.Vector3 newVal);
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
            PhysicsActor_Destroy(NativePointer);
            physicsActorInstances.Remove(nativePtr);
        }

        #endregion

    }
}
