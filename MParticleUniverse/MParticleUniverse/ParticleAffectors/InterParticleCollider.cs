using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The InterParticleCollider is used to perform particle-particle collision.
    /// </summary>
    public class InterParticleCollider : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal InterParticleCollider(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public enum InterParticleCollisionResponses
        {
            IPCR_AVERAGE_VELOCITY,
            IPCR_ANGLE_BASED_VELOCITY
        };

        public const float DEFAULT_ADJUSTMENT = 1.0f;
        public const InterParticleCollisionResponses DEFAULT_COLLISION_RESPONSE = InterParticleCollisionResponses.IPCR_AVERAGE_VELOCITY;


        internal static Dictionary<IntPtr, InterParticleCollider> affectorInstances;

        internal static InterParticleCollider GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, InterParticleCollider>();

            InterParticleCollider newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new InterParticleCollider(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public InterParticleCollider()
            : base(InterParticleCollider_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }



        public float Adjustment { get { return InterParticleCollider_GetAdjustment(nativePtr); } set { InterParticleCollider_SetAdjustment(nativePtr, value); } }

        /** Todo
        */
        public InterParticleCollisionResponses InterParticleCollisionResponse
        {
            get { return InterParticleCollider_GetInterParticleCollisionResponse(nativePtr); }
            set { InterParticleCollider_SetInterParticleCollisionResponse(nativePtr, value); }
        }

        /// <summary>
        /// <see cref="ParticleAffector._prepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            InterParticleCollider__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /// <summary>
        /// <see cref="ParticleAffector._unprepare"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            InterParticleCollider__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            InterParticleCollider__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            InterParticleCollider_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            InterParticleCollider_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr InterParticleCollider_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_GetAdjustment", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float InterParticleCollider_GetAdjustment(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_SetAdjustment", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider_SetAdjustment(IntPtr ptr, float adjustment);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_GetInterParticleCollisionResponse", CallingConvention = CallingConvention.Cdecl)]
        internal static extern InterParticleCollider.InterParticleCollisionResponses InterParticleCollider_GetInterParticleCollisionResponse(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_SetInterParticleCollisionResponse", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider_SetInterParticleCollisionResponse(IntPtr ptr, InterParticleCollider.InterParticleCollisionResponses interParticleCollisionResponse);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "InterParticleCollider_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void InterParticleCollider_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
