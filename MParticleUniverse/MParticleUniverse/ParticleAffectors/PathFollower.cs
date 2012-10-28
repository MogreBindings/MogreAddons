using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// This affector let particles move along a line. The line is derived from a number of points in 3D space.
	///	Using a spline interpolation, the line becomes smooth.
    /// </summary>
    public unsafe class PathFollower : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal PathFollower(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        internal static Dictionary<IntPtr, PathFollower> affectorInstances;

        internal static PathFollower GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, PathFollower>();

            PathFollower newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new PathFollower(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public PathFollower()
            : base(PathFollower_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }


        /** 
        */
        public void AddPoint(Mogre.Vector3 point)
        {
            if (point == null)
                throw new ArgumentNullException("point cannot be null!");
            PathFollower_AddPoint(nativePtr, point);
        }

        /** Clear all points
        */
        public void ClearPoints()
        {
            PathFollower_ClearPoints(nativePtr);
        }

        /** 
        */
        public ushort GetNumPoints()
        {
            return PathFollower_GetNumPoints(nativePtr);
        }

        /** 
        */
        public Mogre.Vector3 GetPoint(ushort index)
        {
            Mogre.Vector3 vec = *(((Mogre.Vector3*)PathFollower_GetPoint(nativePtr, index).ToPointer()));
            return vec;
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            PathFollower__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            PathFollower_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            PathFollower_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PathFollower_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PathFollower_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_AddPoint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PathFollower_AddPoint(IntPtr ptr, Mogre.Vector3 point);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_ClearPoints", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PathFollower_ClearPoints(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_GetNumPoints", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort PathFollower_GetNumPoints(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_GetPoint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PathFollower_GetPoint(IntPtr ptr, ushort index);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PathFollower__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PathFollower_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PathFollower_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
