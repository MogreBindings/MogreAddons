using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// The ParticlePool is a container class that includes other pools. The ParticlePool acts as one pool
    ///	with different types of particles.
    /// </summary>
    public class ParticlePool : IDisposable
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

        internal static Dictionary<IntPtr, ParticlePool> particlePoolInstances;

        internal static ParticlePool GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (particlePoolInstances == null)
                particlePoolInstances = new Dictionary<IntPtr, ParticlePool>();

            ParticlePool newvalue;

            if (particlePoolInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ParticlePool(ptr);
            particlePoolInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ParticlePool(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public ParticlePool()
        {
            nativePtr = ParticlePool_New();
            particlePoolInstances.Add(nativePtr, this);
        }

        public void SetParentTechnique(ParticleTechnique parentTechnique)
        {
            if (parentTechnique == null)
                throw new ArgumentNullException("parentTechnique cannot be null!");
            ParticlePool_SetParentTechnique(nativePtr, parentTechnique.nativePtr);
        }

        /** 
        */
        public bool Empty()
        {
            return ParticlePool_IsEmpty(nativePtr);
        }

        /** 
        */
        public bool Empty(Particle.ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return ParticlePool_IsEmpty(nativePtr, particleType);
        }

        /** 
        */
        public uint GetSize()
        {
            return ParticlePool_GetSize(nativePtr);
        }

        /** 
        */
        public uint GetSize(Particle.ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return ParticlePool_GetSize(nativePtr, particleType);
        }

        /** 
        */
        public void InitialisePool()
        {
            ParticlePool_InitialisePool(nativePtr);
        }

        /// <summary>
        /// This is not Currently Implemented. 
        /// Don't use it will throw a Not Implemented Exception!
        /// </summary>
        /// <param name="particleType"></param>
        /// <param name="size"></param>
        /// <param name="behaviours"></param>
        /// <param name="technique"></param>
        public void IncreasePool(Particle.ParticleType particleType,
            uint size,
            ParticleBehaviour[] behaviours,
            ParticleTechnique technique)
        {
            throw new NotImplementedException("Not Currently Implemented!");
            //ParticlePool_IncreasePool
        }

        /// <summary>
        /// Destroy particles of a certain type
        /// </summary>
        /// <param name="particleType"></param>
        public void DestroyParticles(Particle.ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            ParticlePool_DestroyParticles(nativePtr, particleType);
        }
        public void DestroyAllVisualParticles()
        {
            ParticlePool_DestroyAllVisualParticles(nativePtr);
        }
        public void DestroyAllEmitterParticles()
        {
            ParticlePool_DestroyAllEmitterParticles(nativePtr);
        }
        public void DestroyAllTechniqueParticles()
        {
            ParticlePool_DestroyAllTechniqueParticles(nativePtr);
        }
        public void DestroyAllAffectorParticles()
        {
            ParticlePool_DestroyAllAffectorParticles(nativePtr);
        }
        public void DestroyAllSystemParticles()
        {
            ParticlePool_DestroyAllSystemParticles(nativePtr);
        }

        /// <summary>
        /// Releases a particle from the pool
        /// </summary>
        /// <param name="particleType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Particle ReleaseParticle(Particle.ParticleType particleType, String name)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return Particle.GetInstance(ParticlePool_ReleaseParticle(nativePtr, particleType, name));
        }

        /// <summary>
        /// Releases all particles from the pool
        /// </summary>
        public void ReleaseAllParticles()
        {
            ParticlePool_ReleaseAllParticles(nativePtr);
        }

        /** 
        */
        public void LockLatestParticle()
        {
            ParticlePool_LockLatestParticle(nativePtr);
        }

        /// <summary>
        /// Lock all particles in the pool
        /// </summary>
        public void LockAllParticles()
        {
            ParticlePool_LockAllParticles(nativePtr);
        }

        /** 
        */
        public void ResetIterator()
        {
            ParticlePool_ResetIterator(nativePtr);
        }

        /** 
        */
        public Particle GetFirst()
        {
            return Particle.GetInstance(ParticlePool_GetFirst(nativePtr));
        }


        /** 
        */
        public Particle GetNext()
        {
            return Particle.GetInstance(ParticlePool_GetNext(nativePtr));
        }

        /** 
        */
        public Particle GetFirst(Particle.ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return Particle.GetInstance(ParticlePool_GetFirst(nativePtr, particleType));
        }


        /** 
        */
        public Particle GetNext(Particle.ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return Particle.GetInstance(ParticlePool_GetNext(nativePtr, particleType));
        }

        /** 
        */
        public bool End()
        {
            return ParticlePool_End(nativePtr);
        }

        /** 
        */
        public bool End(Particle.ParticleType particleType)
        {
            if (particleType == null)
                throw new ArgumentNullException("particleType cannot be null!");
            return ParticlePool_End(nativePtr, particleType);
        }

        /** 
        */
        public VisualParticle[] GetVisualParticlesList()
        {
            try
            {
                int bufSize = ParticlePool_GetVisualParticlesListCount(nativePtr);
                if (bufSize == 0)
                    return null;
                IntPtr[] floats = new IntPtr[bufSize];
                VisualParticle[] toReturn = new VisualParticle[bufSize];

                ParticlePool_GetVisualParticlesList(nativePtr, floats, bufSize);

                for (int i = 0; i < floats.Length; i++)
                {
                    //IntPtr scalePtr = floats[i];
                    //Type type = typeof(VisualParticle);
                    //VisualParticle retVal = (VisualParticle)(Marshal.PtrToStructure(scalePtr, type));
                    VisualParticle retVal = new VisualParticle(floats[i]);
                    toReturn[i] = retVal;
                }
                return toReturn;
            }
            catch (Exception e) { Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }


        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticlePool_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_SetParentTechnique", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_SetParentTechnique(IntPtr ptr, IntPtr parentTechnique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_IsEmpty", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticlePool_IsEmpty(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_IsEmpty2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticlePool_IsEmpty(IntPtr ptr, Particle.ParticleType particleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetSize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticlePool_GetSize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetSize2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint ParticlePool_GetSize(IntPtr ptr, Particle.ParticleType particleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_InitialisePool", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_InitialisePool(IntPtr ptr);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_IncreasePool", CallingConvention = CallingConvention.Cdecl)]
        //internal static extern void ParticlePool_IncreasePool(IntPtr ptr, Particle.ParticleType particleType, uint size, Particle.ParticleBehaviourList behaviours, IntPtr technique);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_DestroyParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_DestroyParticles(IntPtr ptr, Particle.ParticleType particleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_DestroyAllVisualParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_DestroyAllVisualParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_DestroyAllEmitterParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_DestroyAllEmitterParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_DestroyAllTechniqueParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_DestroyAllTechniqueParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_DestroyAllAffectorParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_DestroyAllAffectorParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_DestroyAllSystemParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_DestroyAllSystemParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_ReleaseParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticlePool_ReleaseParticle(IntPtr ptr, Particle.ParticleType particleType, [MarshalAs(UnmanagedType.LPStr)]string name);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_ReleaseAllParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_ReleaseAllParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_LockLatestParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_LockLatestParticle(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_LockAllParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_LockAllParticles(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_ResetIterator", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_ResetIterator(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetFirst", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticlePool_GetFirst(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetNext", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticlePool_GetNext(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetFirs2t", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticlePool_GetFirst(IntPtr ptr, Particle.ParticleType particleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetNext2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticlePool_GetNext(IntPtr ptr, Particle.ParticleType particleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_End", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticlePool_End(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_End2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ParticlePool_End(IntPtr ptr, Particle.ParticleType particleType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetVisualParticlesListCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ParticlePool_GetVisualParticlesListCount(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticlePool_GetVisualParticlesList", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticlePool_GetVisualParticlesList(IntPtr ptr, [In, Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] arrLodDistances, int bufSize);
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
            ParticlePool_Destroy(NativePointer);
            particlePoolInstances.Remove(nativePtr);
        }

        #endregion

    }
}
