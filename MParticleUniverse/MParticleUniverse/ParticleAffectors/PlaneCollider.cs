using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// The PlaneCollider is a plane shape that collides with the particles. The PlaneCollider can only collide 
	///	with particles that are created within the same ParticleTechnique as where the PlaneCollider is registered.
    /// </summary>
    public unsafe class PlaneCollider : BaseCollider, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal PlaneCollider(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        public static Mogre.Vector3 DEFAULT_NORMAL { get { return new Mogre.Vector3(0, 0, 0); } }


        internal static Dictionary<IntPtr, PlaneCollider> affectorInstances;

        internal static PlaneCollider GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, PlaneCollider>();

            PlaneCollider newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new PlaneCollider(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public PlaneCollider()
            : base(PlaneCollider_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /** Returns the normal of the plane
        */
        public Mogre.Vector3 Normal
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)PlaneCollider_GetNormal(nativePtr).ToPointer()));
                return vec; ;
            }
            set { PlaneCollider_SetNormal(nativePtr, value); }
        }

        /** Notify that the Affector is rescaled.
        */
        public void _notifyRescaled(Mogre.Vector3 scale)
        {
            if (scale == null)
                throw new ArgumentNullException("scale cannot be null!");
            PlaneCollider__notifyRescaled(nativePtr, scale);
        }
        /** 
        */
        public void CalculateDirectionAfterCollision(Particle particle, float timeElapsed)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            PlaneCollider_CalculateDirectionAfterCollision(nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector._unprepare"/>
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            PlaneCollider__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            PlaneCollider_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            PlaneCollider_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PlaneCollider_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PlaneCollider_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider_GetNormal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PlaneCollider_GetNormal(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider_SetNormal", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PlaneCollider_SetNormal(IntPtr ptr, Mogre.Vector3 normal);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider__notifyRescaled", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PlaneCollider__notifyRescaled(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider_CalculateDirectionAfterCollision", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PlaneCollider_CalculateDirectionAfterCollision(IntPtr ptr, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PlaneCollider__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PlaneCollider_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PlaneCollider_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
