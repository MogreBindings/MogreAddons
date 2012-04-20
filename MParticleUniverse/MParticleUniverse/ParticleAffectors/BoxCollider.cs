using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The BoxCollider is a box shape that collides with the particles. The BoxCollider can only collide with
	///	particles that are created within the same ParticleTechnique as where the BoxCollider is registered.
    /// </summary>
    public class BoxCollider : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal BoxCollider(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        public const float DEFAULT_WIDTH = 100.0f;
        public const float DEFAULT_HEIGHT = 100.0f;
        public const float DEFAULT_DEPTH = 100.0f;

        internal static Dictionary<IntPtr, BoxCollider> affectorInstances;

        internal static BoxCollider GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, BoxCollider>();

            BoxCollider newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new BoxCollider(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public BoxCollider()
            : base(BoxCollider_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /// <summary>
        /// Returns the width of the box
        /// </summary>
        public float Width { get { return BoxCollider_GetWidth(nativePtr); } set { BoxCollider_SetWidth(nativePtr, value); } }

        /// <summary>
        /// Returns the height of the box
        /// </summary>
        public float Height { get { return BoxCollider_GetHeight(nativePtr); } set { BoxCollider_SetHeight(nativePtr, value); } }

        /// <summary>
        /// Returns the depth of the box
        /// </summary>
        public float Depth { get { return BoxCollider_GetDepth(nativePtr); } set { BoxCollider_SetDepth(nativePtr, value); } }

        /// <summary>
        /// Returns indication whether the collision is inside or outside of the box
        ///  Set indication whether the collision is inside or outside of the box
        /// <remarks>
        ///    If value is true, the collision is inside of the box.</remarks>
        /// </summary>
        public bool InnerCollision { get { return BoxCollider_IsInnerCollision(nativePtr); } set { BoxCollider_SetInnerCollision(nativePtr, value); } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="particle"></param>
        public void CalculateDirectionAfterCollision(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BoxCollider_CalculateDirectionAfterCollision(nativePtr, particle.NativePointer);
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
            BoxCollider__preProcessParticles(nativePtr, particleTechnique.nativePtr, timeElapsed);
        }

        /// <summary>
        /// <see cref="ParticleAffector._affect"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            BoxCollider__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        /// <summary>
        /// <see cref="ParticleAffector.CopyAttributesTo"/>
        /// </summary>
        /// <param name="affector"></param>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            BoxCollider_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            BoxCollider_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr BoxCollider_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_GetWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxCollider_GetWidth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_SetWidth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_SetWidth(IntPtr ptr, float width);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_GetHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxCollider_GetHeight(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_SetHeight", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_SetHeight(IntPtr ptr, float height);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_GetDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float BoxCollider_GetDepth(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_SetDepth", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_SetDepth(IntPtr ptr, float depth);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_IsInnerCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool BoxCollider_IsInnerCollision(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_SetInnerCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_SetInnerCollision(IntPtr ptr, bool innerCollision);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_CalculateDirectionAfterCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_CalculateDirectionAfterCollision(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider__preProcessParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider__preProcessParticles(IntPtr ptr, IntPtr particleTechnique, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "BoxCollider_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void BoxCollider_CopyAttributesTo(IntPtr ptr, IntPtr affector);

        #endregion

    }
}
