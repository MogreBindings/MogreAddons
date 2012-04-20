using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The CollisionAvoidanceAffector is used to prevent particles from colliding with each other.
	/// <remarks>
	///	The current implementation doesn´t take avoidance of colliders (box, sphere, plane) into account (yet).</remarks>
    /// </summary>
    public class CollisionAvoidanceAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal CollisionAvoidanceAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        const float DEFAULT_RADIUS = 100.0f;


        internal static Dictionary<IntPtr, CollisionAvoidanceAffector> affectorInstances;

        internal static CollisionAvoidanceAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, CollisionAvoidanceAffector>();

            CollisionAvoidanceAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new CollisionAvoidanceAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public CollisionAvoidanceAffector()
            : base(CollisionAvoidanceAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /// <summary>
        /// Todo
        /// </summary>
        public float Radius { get { return CollisionAvoidanceAffector_GetRadius(nativePtr); } set { CollisionAvoidanceAffector_SetRadius(nativePtr, value); } }

        /// <summary>
        /// <see cref="ParticleAffector._prepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            CollisionAvoidanceAffector__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /// <summary>
        /// <see cref="ParticleAffector._unprepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>

        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            CollisionAvoidanceAffector__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            CollisionAvoidanceAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            CollisionAvoidanceAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            CollisionAvoidanceAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr CollisionAvoidanceAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CollisionAvoidanceAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector_GetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float CollisionAvoidanceAffector_GetRadius(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector_SetRadius", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CollisionAvoidanceAffector_SetRadius(IntPtr ptr, float radius);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CollisionAvoidanceAffector__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CollisionAvoidanceAffector__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CollisionAvoidanceAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "CollisionAvoidanceAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void CollisionAvoidanceAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);

        #endregion

    }
}
