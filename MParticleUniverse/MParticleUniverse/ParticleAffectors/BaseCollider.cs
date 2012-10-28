using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The BaseCollider is the abstract class for Particle Colliders. A collider is a shape that can collide with a particle.
	///	In fact, particles can also be considered colliders, which allows particle-particle collision.
    /// </summary>
    public abstract class BaseCollider : ParticleAffector, IDisposable
    {
        /// <summary>
        /// Determines how a particle collision should be determined. IT_POINT means that the position of 
		///		the particle will be validated against the Colliders' shape. IT_BOX means that the dimensions
		///		(width, height and depth) are used to determine whether the particle collides.
        /// </summary>
        public enum IntersectionTypes
        {
            IT_POINT,
            IT_BOX
        };

        /// <summary>
        /// Determines how a particle behaves after collision with this collider. The behaviour of the
        ///    particle is solved in the collider and only behaviour that needs the colliders´ data is taken
        ///    into account. The fact that a particle expires for example, can be achieved by using an 
        ///    Observer in combination with an EventHandler (DoExpireEventHandler).
        ///    CT_NONE means that the particle doesn´t do anything. This value should be set if the behaviour 
        ///    of the particle is determined outside the collider (for example, expiring the particle).
        ///    CT_BOUNCE means that the particle bounces off the collider.
        ///    CT_FLOW means that the particle flows around the contours of the collider.
        /// </summary>
        public enum CollisionTypes
        {
            CT_NONE,
            CT_BOUNCE,
            CT_FLOW,
        };

        // Constants
        public const float DEFAULT_BOUNCYNESS = 1.0f;
        public const float DEFAULT_FRICTION = 0.0f;
        public const IntersectionTypes DEFAULT_INTERSECTION_TYPE = IntersectionTypes.IT_POINT;
        public const CollisionTypes DEFAULT_COLLISION_TYPE = CollisionTypes.CT_BOUNCE;

        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal BaseCollider(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        /// <summary>
        /// <see cref="ParticleAffector._preProcessParticles"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="timeElapsed"></param>
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            BaseCollider__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// Returns the type of intersection.
        /// </summary>
        public IntersectionTypes IntersectionType { get { return BaseCollider_GetIntersectionType(nativePtr); } set { BaseCollider_SetIntersectionType(nativePtr, value); } }

        /// <summary>
        /// Returns the type of collision.
        /// </summary>
        public CollisionTypes CollisionType { get { return BaseCollider_GetCollisionType(nativePtr); } set { BaseCollider_SetCollisionType(nativePtr, value); } }

        /// <summary>
        /// Returns the friction value.
        /// </summary>
        public float Friction { get { return BaseCollider_GetBouncyness(nativePtr); } set { BaseCollider_SetBouncyness(nativePtr, value); } }

        /// <summary>
        /// Returns the bouncyness value.
        /// </summary>
        public float Bouncyness { get { return BaseCollider_GetBouncyness(nativePtr); } set { BaseCollider_SetBouncyness(nativePtr, value); } }

        /// <summary>
        /// Fill the AxisAlignedBox with data derived from the other arguments.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="position"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="depth"></param>
        public void PopulateAlignedBox(Mogre.AxisAlignedBox box,
            Mogre.Vector3 position,
            float width,
            float height,
            float depth)
        {
            if (box == null)
                throw new ArgumentNullException("box cannot be null!");
            if (position == null)
                throw new ArgumentNullException("position cannot be null!");
            IntPtr boxPtr = Marshal.AllocHGlobal(Marshal.SizeOf(box));
            Marshal.StructureToPtr(box, boxPtr, true);
            BaseCollider_PopulateAlignedBox(nativePtr, boxPtr, position, width, height, depth);
        }

        /// <summary>
        /// Recalculates the rotation speed after collision.
        ///    This function must be explicitly called in the _affect() function of the class that inherits from 
        ///    BaseCollider.
        /// </summary>
        /// <param name="particle"></param>
        public void CalculateRotationSpeedAfterCollision(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BaseCollider_CalculateRotationSpeedAfterCollision(nativePtr, particle.NativePointer);
        }

        /// <summary>
        /// <see cref="ParticleAffector.CopyAttributesTo"/>
        /// </summary>
        /// <param name="affector"></param>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            BaseCollider_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            BaseCollider_Destroy(NativePointer);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_GetIntersectionType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern BaseCollider.IntersectionTypes BaseCollider_GetIntersectionType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_SetIntersectionType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_SetIntersectionType(IntPtr ptr, BaseCollider.IntersectionTypes intersectionType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_GetCollisionType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern BaseCollider.CollisionTypes BaseCollider_GetCollisionType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_SetCollisionType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_SetCollisionType(IntPtr ptr, BaseCollider.CollisionTypes collisionType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_GetFriction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BaseCollider_GetFriction(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_SetFriction", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_SetFriction(IntPtr ptr, float friction);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_GetBouncyness", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BaseCollider_GetBouncyness(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_SetBouncyness", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_SetBouncyness(IntPtr ptr, float bouncyness);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_PopulateAlignedBox", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_PopulateAlignedBox(IntPtr ptr, IntPtr box,
                Mogre.Vector3 position,
                float width,
                float height,
                float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_CalculateRotationSpeedAfterCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_CalculateRotationSpeedAfterCollision(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BaseCollider_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BaseCollider_CopyAttributesTo(IntPtr ptr, IntPtr affector);

        #endregion
    }
}
