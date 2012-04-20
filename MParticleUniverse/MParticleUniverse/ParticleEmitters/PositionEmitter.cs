using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleEmitters
{
    /// <summary>
    /// The PositionEmitter is an emitter that emits particles from one or more given positions.
    ///	The PositionEmitter enables building up a predefined structure out of particles. Imaging building up a
    ///	wall that is made up from stone blocks. The particles are emitted on the positions that are added to this
    ///	emitter. Adding some physical behaviour to the particles and you have your stone wall that collapses if a 
    ///	force is applied to it.
    /// </summary>
    public class PositionEmitter : ParticleEmitter, IDisposable
    {
        internal IntPtr nativePtr;

        internal static Dictionary<IntPtr, PositionEmitter> emitterInstances;

        internal static PositionEmitter GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (emitterInstances == null)
                emitterInstances = new Dictionary<IntPtr, PositionEmitter>();

            PositionEmitter newvalue;

            if (emitterInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new PositionEmitter(ptr);
            emitterInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal PositionEmitter(IntPtr ptr)
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
            PositionEmitter_Destroy(NativePointer);
            emitterInstances.Remove(nativePtr);
        }

        #endregion

        public const bool DEFAULT_RANDOMIZE = true;


        public PositionEmitter()
            : base(PositionEmitter_New())
        {
            nativePtr = base.nativePtr;
            emitterInstances.Add(nativePtr, this);
        }
        /** 
        */
        public bool Randomized { get { return PositionEmitter_IsRandomized(nativePtr); } set { PositionEmitter_SetRandomized(nativePtr, value); } }

        /** Returns a list with positions
        */
        public Mogre.Vector3[] GetTextureCoords()
        {
            try
            {

                int bufSize = PositionEmitter_GetPositionsCount(nativePtr);
                if (bufSize == 0)
                    return null;
                IntPtr[] floats = new IntPtr[bufSize];
                Mogre.Vector3[] toReturn = new Mogre.Vector3[bufSize];

                PositionEmitter_GetPositionsCoords(nativePtr, floats, bufSize);

                for (int i = 0; i < floats.Length; i++)
                {
                    IntPtr scalePtr = floats[i];
                    Type type = typeof(Mogre.Vector3);
                    Mogre.Vector3 retVal = (Mogre.Vector3)(Marshal.PtrToStructure(scalePtr, type));

                    toReturn[i] = retVal;
                }
                return toReturn;
            }
            catch (Exception e) { Console.WriteLine(e.Message + "@" + e.Source); }
            return null;
        }

        /** Add a new position to this emitter
        */
        public void AddPosition(Mogre.Vector3 position)
        {
            if (position == null)
                throw new ArgumentNullException("position cannot be null!");
            PositionEmitter_AddPosition(nativePtr, position);
        }

        /** Remove all positions from this emitter
        */
        public void RemoveAllPositions()
        {
            PositionEmitter_RemoveAllPositions(nativePtr);
        }

        /** See ParticleEmitter
        */
        public void _notifyStart()
        {
            PositionEmitter__notifyStart(nativePtr);
        }

        /** See ParticleEmitter
        */
        public ushort _calculateRequestedParticles(float timeElapsed)
        {
            return PositionEmitter__calculateRequestedParticles(nativePtr, timeElapsed);
        }

        /** Generate a particle position based on the added positions.
        */
        public void _initParticlePosition(Particle particle)
        {
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            PositionEmitter__initParticlePosition(nativePtr, particle.NativePointer);
        }

        /** 
        */
        public void CopyAttributesTo(ParticleEmitter emitter)
        {
            if (emitter == null)
                throw new ArgumentNullException("emitter cannot be null!");
            PositionEmitter_CopyAttributesTo(nativePtr, emitter.nativePtr);
        }


        #region PositionEmitter Exports
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr PositionEmitter_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_IsRandomized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool PositionEmitter_IsRandomized(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_SetRandomized", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter_SetRandomized(IntPtr ptr, bool randomized);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_GetPositionsCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int PositionEmitter_GetPositionsCount(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_GetPositionsCoords", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter_GetPositionsCoords(IntPtr ptr, [In, Out] [MarshalAs(UnmanagedType.LPArray)] IntPtr[] arrLodDistances, int bufSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_AddPosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter_AddPosition(IntPtr ptr, Mogre.Vector3 position);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_RemoveAllPositions", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter_RemoveAllPositions(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter__notifyStart", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter__notifyStart(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter__calculateRequestedParticles", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ushort PositionEmitter__calculateRequestedParticles(IntPtr ptr, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter__initParticlePosition", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter__initParticlePosition(IntPtr ptr, IntPtr particle);
        [DllImport("ParticleUniverse.dll", EntryPoint = "PositionEmitter_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void PositionEmitter_CopyAttributesTo(IntPtr ptr, IntPtr emitter);
        #endregion
    }
}
