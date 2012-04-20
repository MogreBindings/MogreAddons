using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The MeshSurfaceEmitter is a ParticleEmitter that emits particles on the surface of a mesh.
    /// <remarks>
    ///	There are several ways of emitting it on the surface, from the vertices, edges and faces of a mesh.
    ///	It is also possible to define whether more particles emit on larger faces.
    ///	</remarks>
    /// </summary>
    public unsafe class MeshSurfaceEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, MeshSurfaceEmitter> emitterInstances;

        internal static MeshSurfaceEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, MeshSurfaceEmitter>();

            MeshSurfaceEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new MeshSurfaceEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal MeshSurfaceEmitter(IntPtr ptr)
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
            MeshSurfaceEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public static Mogre.Vector3 DEFAULT_SCALE { get { return new Mogre.Vector3(1, 1, 1); } }
        public const MeshInfo.MeshSurfaceDistribution DEFAULT_DISTRIBUTION = MeshInfo.MeshSurfaceDistribution.MSD_HOMOGENEOUS;

        public MeshSurfaceEmitter()
            : base(MeshSurfaceEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }

        /** Returns the mesh name.
         * Sets the mesh name.
        */
        public String MeshName { get { return Marshal.PtrToStringAnsi(MeshSurfaceEmitter_GetMeshName(nativePtr)); } set { MeshSurfaceEmitter_SetMeshName(nativePtr, value); } }

        /** Returns true if normals are used for the particle direction.
         * Set indication whether normals are used for the particle direction.
        */
        public bool UseNormals 
        {
            get { 
                throw new NotImplementedException("The method 'UseNormals' has no implementation."); 
                //return MeshSurfaceEmitter_UseNormals(nativePtr);
            } 
            set {
                throw new NotImplementedException("The method 'UseNormals' has no implementation.");
                //MeshSurfaceEmitter_SetUseNormals(nativePtr, value); 
            }
        }

        /** Returns the type op distribution.
        @remarks
            There are several ways to emit particles on the surface of a mesh. This attribute indicates
            the type of distrubution on the surface.
        Set the type of particle distribution on the surface of a mesh.
         */
        public MeshInfo.MeshSurfaceDistribution Distribution { get { return MeshSurfaceEmitter_GetDistribution(nativePtr); } set { MeshSurfaceEmitter_SetDistribution(nativePtr, value); } }

        /** Returns the scale of the mesh.
        */
        /** Set the scale of the mesh.
        @remarks
            This options makes it possible to scale the mesh independently from the particle system scale as a whole.
        */
        public Mogre.Vector3 Scale
        {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)MeshSurfaceEmitter_GetScale(nativePtr).ToPointer()));
                return vec;
            }
            set { MeshSurfaceEmitter_SetScale(nativePtr, value); }
        }

        /** Build all the data needed to generate the particles.
        */
        public void Build()
        {
            MeshSurfaceEmitter_Build(nativePtr);
        }

        /** Build the data if the mesh name has been set.
        */
        public void _prepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            MeshSurfaceEmitter__prepare(nativePtr, particleTechnique.nativePtr);
        }

        /** Reverse it.
        */
        public void _unprepare(ParticleTechnique particleTechnique)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            MeshSurfaceEmitter__unprepare(nativePtr, particleTechnique.nativePtr);
        }

        /** Determine a particle position on the mesh surface.
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            MeshSurfaceEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** See ParticleEmitter.
        */
        public ushort _calculateRequestedParticles(float timeElapsed)
        {
            return MeshSurfaceEmitter__calculateRequestedParticles(nativePtr, timeElapsed);
        }

        /** Determine the particle direction.
        */
        public void _initParticleDirection(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            MeshSurfaceEmitter__initParticleDirection(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            MeshSurfaceEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }

        #region MeshSurfaceEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MeshSurfaceEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_GetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MeshSurfaceEmitter_GetMeshName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_SetMeshName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter_SetMeshName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string meshName, bool doBuild = true);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_UseNormals", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern bool MeshSurfaceEmitter_UseNormals(IntPtr ptr);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_SetUseNormals", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void MeshSurfaceEmitter_SetUseNormals(IntPtr ptr, bool useNormals);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceDistribution", CallingConvention = CallingConvention.Cdecl)]
        internal static extern MeshInfo.MeshSurfaceDistribution MeshSurfaceEmitter_GetDistribution(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_SetDistribution", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter_SetDistribution(IntPtr ptr, MeshInfo.MeshSurfaceDistribution distribution);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_GetScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr MeshSurfaceEmitter_GetScale(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_SetScale", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter_SetScale(IntPtr ptr, Mogre.Vector3 scale);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_Build", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter_Build(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter__prepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter__prepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter__unprepare", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter__unprepare(IntPtr ptr, IntPtr particleTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter__calculateRequestedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort MeshSurfaceEmitter__calculateRequestedParticles(IntPtr ptr, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter__initParticleDirection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter__initParticleDirection(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "MeshSurfaceEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void MeshSurfaceEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
    /// <summary>
    /// Class that constructs mesh information of a given mesh name
    /// </summary>
    public class MeshInfo
    {
        /* 
        */
        /// <summary>
        /// Defining several methods to emit particles on the mesh surface
        ///	<remarks>
        ///		Sometimes the difference is not always visible, for example if the mesh contains triangles with more or
        ///		less the same size. Only in case a mesh contains both small and large triangles the difference between
        ///		the various distribution methods is more obvious.
        /// </remarks>
        /// </summary>
        public enum MeshSurfaceDistribution
        {
            /// <summary>
            /// Distribute particles homogeneous (random) on the mesh surface
            /// </summary>
            MSD_HOMOGENEOUS,
            /// <summary>
            /// Distribute more particles on the smaller faces
            /// </summary>
            MSD_HETEROGENEOUS_1,
            /// <summary>
            /// Same as above, but now more particles are emitting from the larger faces
            /// </summary>
            MSD_HETEROGENEOUS_2,
            /// <summary>
            /// Particles only emit from the vertices
            /// </summary>
            MSD_VERTEX,
            /// <summary>
            /// Particles emit random on the edges
            /// </summary>
            MSD_EDGE
        };
    }
}
