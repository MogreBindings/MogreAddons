using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The VertexEmitter gradually generates spawn points, based on the edges of a mesh. 
    ///	This means that in every ParticleTechnique-update, a few points are generated, 
    ///	until all vertices of the mesh have been processed.
    /// <remarks>
    ///	Beware, there is no intermediate validation whether the mesh still exist (performance reasons), 
    ///	so destroying the mesh before all vertices are processed will result in an exception.
    /// </remarks>
    ///	Todo:
    ///	- Add normals to the SpawnPositionList
    /// </summary>
    public class VertexEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, VertexEmitter> emitterInstances;

        internal static VertexEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, VertexEmitter>();

            VertexEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new VertexEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal VertexEmitter(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
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
            VertexEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public const ushort DEFAULT_STEP = 1;
        public const ushort DEFAULT_SEGMENTS = 1;
        public const ushort DEFAULT_ITERATIONS = 1;

        public VertexEmitter()
            : base(VertexEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }

        /** 
        */
        public ushort Iterations { get { return VertexEmitter_GetIterations(nativePtr); } set { VertexEmitter_SetIterations(nativePtr, value); } }

        /** 
        */
        public ushort Segments { get { return VertexEmitter_GetSegments(nativePtr); } set { VertexEmitter_SetSegments(nativePtr, value); } }

        /** 
        */
        public ushort Step { get { return VertexEmitter_GetStep(nativePtr); } set { VertexEmitter_SetStep(nativePtr, value); } }

        /** 
        */
        public String MeshName { get { return Marshal.PtrToStringAnsi(VertexEmitter_GetMeshName(nativePtr)); } set { VertexEmitter_SetMeshName(nativePtr, value); } }

        /** 
        */
        public void _notifyStart()
        {
            VertexEmitter__notifyStart(nativePtr);
        }

        /** 
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            VertexEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public ushort _calculateRequestedParticles(float timeElapsed)
        {
            return VertexEmitter__calculateRequestedParticles(nativePtr, timeElapsed);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            VertexEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }

        #region VertexEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VertexEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_GetIterations", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort VertexEmitter_GetIterations(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_SetIterations", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter_SetIterations(IntPtr ptr, ushort iterations);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_GetSegments", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort VertexEmitter_GetSegments(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_SetSegments", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter_SetSegments(IntPtr ptr, ushort segments);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_GetStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort VertexEmitter_GetStep(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_SetStep", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter_SetStep(IntPtr ptr, ushort step);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_GetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr VertexEmitter_GetMeshName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_SetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter_SetMeshName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string meshName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter__calculateRequestedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort VertexEmitter__calculateRequestedParticles(IntPtr ptr, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "VertexEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void VertexEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
}
