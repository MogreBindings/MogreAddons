using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleAffectors
{
    /// <summary>
    /// This affector is typically used to change the colour of a particle during its lifetime.
    /// </summary>
    public class ColourAffector : ParticleAffector, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ColourAffector(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }


        public enum ColourOperations
        {
            CAO_MULTIPLY = 0,
            CAO_SET = 1
        };

        public const ColourOperations DEFAULT_COLOUR_OPERATION = ColourOperations.CAO_SET;


        internal static Dictionary<IntPtr, ColourAffector> affectorInstances;

        internal static ColourAffector GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (affectorInstances == null)
                affectorInstances = new Dictionary<IntPtr, ColourAffector>();

            ColourAffector newvalue;

            if (affectorInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ColourAffector(ptr);
            affectorInstances.Add(ptr, newvalue);
            return newvalue;
        }

        // Constructor
        public ColourAffector()
            : base(ColourAffector_New())
        {
            nativePtr = base.nativePtr;
            affectorInstances.Add(nativePtr, this);
        }

        /** 
        */
        public void AddColour(float timeFraction, Mogre.ColourValue colour)
        {
            IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(colour));
            Marshal.StructureToPtr(colour, colourPtr, true);

            ColourAffector_AddColour(nativePtr, timeFraction, colourPtr);
        }

        /** 
        */
        public SortedDictionary<float, Mogre.ColourValue> GetTimeAndColour()
        {
            int arrSize = ColourAffector_GetTimeAndColoursCount(nativePtr);
            if (arrSize == 0)
                return null;
            float[] times = new float[arrSize];
            Mogre.ColourValue[] colours = new Mogre.ColourValue[arrSize];

            IntPtr timeAndColourList = ColourAffector_GetTimeAndColours(nativePtr, times, colours, arrSize);

            SortedDictionary<float, Mogre.ColourValue> dict = new SortedDictionary<float, Mogre.ColourValue>();

            for (int i = 0; i < arrSize; i++)
            {
                dict[times[i]] = colours[i];
            }
            return dict;
        }

        /** 
        */
        public void ClearColourMap()
        {
            ColourAffector_ClearColourMap(nativePtr);
        }

        ///** 
        //*/
        //public ColourMapIterator _findNearestColourMapIterator(float timeFraction)
        //{
        //    return null;
        //}

        /** 
        */
        public ColourOperations ColourOperation 
        { 
            get { return ColourAffector_GetColourOperation(nativePtr); } 
            set { ColourAffector_SetColourOperation(nativePtr, value); } 
        }

        /** 
        */
        public void _affect(ParticleTechnique particleTechnique, Particle particle, float timeElapsed)
        {
            if (particleTechnique == null)
                throw new ArgumentNullException("particleTechnique cannot be null!");
            if (particle == null)
                throw new ArgumentNullException("particle cannot be null!");
            ColourAffector__affect(nativePtr, particleTechnique.nativePtr, particle.NativePointer, timeElapsed);
        }

        ///<see cref="ParticleAffector.CopyAttributesTo"/>
        public void CopyAttributesTo(ParticleAffector affector)
        {
            if (affector == null)
                throw new ArgumentNullException("affector cannot be null!");
            ColourAffector_CopyAttributesTo(nativePtr, affector.nativePtr);
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
            ColourAffector_Destroy(NativePointer);
            affectorInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ColourAffector_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ColourAffector_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_AddColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ColourAffector_AddColour(IntPtr ptr, float timeFraction, IntPtr colour);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_GetTimeAndColour", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ColourAffector_GetTimeAndColour(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_GetTimeAndColoursCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ColourAffector_GetTimeAndColoursCount(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_GetTimeAndColours", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ColourAffector_GetTimeAndColours(IntPtr ptr, [In, Out] [MarshalAs(UnmanagedType.LPArray)] float[] arrTimes, [In, Out] [MarshalAs(UnmanagedType.LPArray)] Mogre.ColourValue[] arrColours, int bufSize);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_ClearColourMap", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ColourAffector_ClearColourMap(IntPtr ptr);
        //[DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector__findNearestColourMapIterator", CallingConvention = CallingConvention.Cdecl)]
        //        internal static extern IntPtr ColourAffector__findNearestColourMapIterator(IntPtr ptr, float timeFraction);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_GetColourOperation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern ColourAffector.ColourOperations ColourAffector_GetColourOperation(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_SetColourOperation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ColourAffector_SetColourOperation(IntPtr ptr, ColourAffector.ColourOperations colourOperation);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector__affect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ColourAffector__affect(IntPtr ptr, IntPtr particleTechnique, IntPtr particle, float timeElapsed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ColourAffector_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ColourAffector_CopyAttributesTo(IntPtr ptr, IntPtr affector);
        #endregion

    }
}
