using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The SphereCollider is a sphere shape that collides with the particles. The SphereCollider can only collide 
	///	with particles that are created within the same ParticleTechnique as where the SphereCollider is registered.
    /// </summary>
    public class SphereCollider : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal SphereCollider(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_RADIUS = 100.0f;


        internal static Dictionary<IntPtr, SphereCollider> affectorInstances;

        internal static SphereCollider GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, SphereCollider>();

            SphereCollider newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new SphereCollider(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public SphereCollider()
            : base(SphereCollider_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /** Returns the radius of the sphere
        */
        public float Radius { get { return SphereCollider_GetRadius(nativePtr); } set { SphereCollider_SetRadius(nativePtr, value); } }

        /** Returns indication whether the collision is inside or outside of the box
        @remarks
            If value is true, the collision is inside of the box.
        */
        public bool InnerCollision { get { return SphereCollider_IsInnerCollision(nativePtr); } set { SphereCollider_SetInnerCollision(nativePtr, value); } }

        /** 
        */
        public void CalculateDirectionAfterCollision(Particle particle, Mogre.Vector3 distance, float distanceLength)
        {
            if (distance == null)
                throw new ArgumentNullException("distance cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SphereCollider_CalculateDirectionAfterCollision(nativePtr, particle.NativePointer, distance, distanceLength);
        }

        /** @copydoc ParticleAffector::_preProcessParticles */
        public void _preProcessParticles(ParticleTechnique particleTechnique, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            SphereCollider__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            SphereCollider__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            SphereCollider_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            SphereCollider_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr SphereCollider_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_GetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float SphereCollider_GetRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_SetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider_SetRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_IsInnerCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool SphereCollider_IsInnerCollision(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_SetInnerCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider_SetInnerCollision(IntPtr ptr, bool innerCollision);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_CalculateDirectionAfterCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider_CalculateDirectionAfterCollision(IntPtr ptr, IntPtr particle, Mogre.Vector3 distance, float distanceLength);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "SphereCollider__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void SphereCollider__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        #endregion

    }
}
