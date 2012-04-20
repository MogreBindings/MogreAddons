using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// Aligns the orientation of a particle towards the previous particle and adjusts the height of
    ///	the particle, which becomes the length between the two particles. And how do we benefit from 
    ///	this? Well, with the right renderer settings you could get a chain of particles, each connected
    ///	to the previous, making use of the particle orientation.
    ///	
    ///	We get good results with a billboard renderer (which - btw - doesn't take the particle 
    ///	orientation into account by default). Use the billboard type 'oriented shape', which is a type
    ///	that isn't a standard billboard type of Ogre. It has been added to allow the billboard renderer
    ///	take advantage of the particles' orientation. Also use for instance 'bottom center' as the 
    ///	billboard origin, which neatly connects the billboards.
    ///
    ///	Using the AlignAffector is a step in the direction to generate electric beams.
    /// </summary>
    public class AlignAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal AlignAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        // Constants
        public const bool DEFAULT_RESIZE = false;

        internal static Dictionary<IntPtr, AlignAffector> affectorInstances;

        internal static AlignAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, AlignAffector>();

            AlignAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new AlignAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public AlignAffector()
            : base(AlignAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// <see cref="ParticleAffector.CopyAttributesTo"/>
        /// </summary>
        /// <param name="affector"></param>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            AlignAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
        }

        /// <summary>
        /// Set resize. This attribute determines whether the size of the particle must be changed
        ///	according to its alignment with the previous particle.
        /// </summary>
        public bool Resize
        {
            get { return AlignAffector_IsResize(nativePtr); }
            set { AlignAffector_SetResize(nativePtr, value); }
        }


        /// <summary>
        /// <see cref="ParticleAffector._firstParticle"/>
        /// </summary>
        /// <param name="particleTechnique"></param>
        /// <param name="particle"></param>
        /// <param name="timeElapsed"></param>
        public void _firstParticle(ParticleTechnique particleTechnique,
            Particle particle,
            float timeElapsed)
        {

            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            AlignAffector__firstParticle(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            AlignAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
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
            AlignAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr AlignAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void AlignAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void AlignAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector_IsResize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool AlignAffector_IsResize(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector_SetResize", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void AlignAffector_SetResize(IntPtr ptr, bool resize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector__firstParticle", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void AlignAffector__firstParticle(IntPtr ptr, IntPtr particleTechnique,
                IntPtr particle,
                float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "AlignAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void AlignAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        #endregion
    }
}
