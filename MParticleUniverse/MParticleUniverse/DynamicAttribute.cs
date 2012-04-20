using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    public enum DynamicAttributeType
    {
        DAT_FIXED = 0,
        DAT_RANDOM = 1,
        DAT_CURVED = 2,
        DAT_OSCILLATE = 3
    };

    public interface DynamicAttribute : IElement
    {
        IntPtr NativePointer { get; }

        /// <summary>
        /// Virtual function that needs to be implemented by its childs.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        float GetValue(float x);

        DynamicAttributeType Type { get; set; }

        void CopyAttributesTo(DynamicAttribute dynamicAttribute);

        //bool isValueChangedExternally();
    }

    internal class DynamicAttributeTypeHelper
    {
        public static DynamicAttribute GetDynamicAttribute(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;

            DynamicAttributeType daType = DynamicAttribute_GetType(ptr);
            switch (daType)
            {
                case DynamicAttributeType.DAT_CURVED:
                    return new DynamicAttributeCurved(ptr);
                case DynamicAttributeType.DAT_FIXED:
                    return new DynamicAttributeFixed(ptr);
                case DynamicAttributeType.DAT_OSCILLATE:
                    return new DynamicAttributeOscillate(ptr);
                case DynamicAttributeType.DAT_RANDOM:
                    return new DynamicAttributeRandom(ptr);
                default:
                    return null;
            }
        }

        public static float GetValue(IntPtr ptr, float x = 0)
        {
            return DynamicAttribute_GetValue(ptr, x);
        }

        public static DynamicAttributeType GetType(IntPtr ptr)
        {
            return DynamicAttribute_GetType(ptr);
        }

        public static void SetType(IntPtr ptr, DynamicAttributeType type)
        {
            DynamicAttribute_SetType(ptr, type);
        }
        public static void CopyAttributesTo(IntPtr ptr, IntPtr dynamicAttribute)
        {
            DynamicAttribute_CopyAttributesTo(ptr, dynamicAttribute);
        }

        #region DynamicAttribute PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttribute_GetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttribute_GetValue(IntPtr ptr, float x = 0);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttribute_GetType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern DynamicAttributeType DynamicAttribute_GetType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttribute_SetType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttribute_SetType(IntPtr ptr, DynamicAttributeType type);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttribute_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttribute_CopyAttributesTo(IntPtr ptr, IntPtr dynamicAttribute);
        #endregion

    }

    /// <summary>
    /// This class is an implementation of the DynamicAttribute class in its most simple form. It just returns a value
    ///	that has previously been set.
    ///<remarks>
    ///	Although use of a regular attribute within the class that needs it is preferred, its benefit is that it makes
    ///	use of the generic 'getValue' mechanism of a DynamicAttribute.</remarks>
    /// </summary>
    public class DynamicAttributeFixed : DynamicAttribute, IDisposable
    {
        internal IntPtr nativePointer;

        public IntPtr NativePointer
        {
            get { return nativePointer; }
            set { nativePointer = value; }
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
            DynamicAttributeFixed_Destroy(NativePointer);
        }

        #endregion


        internal DynamicAttributeFixed(IntPtr ptr)
        {
            nativePointer = ptr;
        }

        /** Constructor
			*/
        public DynamicAttributeFixed()
        {
            nativePointer = DynamicAttributeFixed_New();
        }

        /** Copy constructor
        */
        public DynamicAttributeFixed(DynamicAttributeFixed dynamicAttributeFixed)
        {
            nativePointer = DynamicAttributeFixed_New(dynamicAttributeFixed.NativePointer);
        }

        /** Todo
        */
        public float GetValue(float x = 0)
        {
            return DynamicAttributeFixed_GetValue(nativePointer, x);
        }

        /** Todo
        */
        public void SetValue(float value)
        {
            DynamicAttributeFixed_SetValue(nativePointer, value);
        }

        /** Todo
        */
        public void CopyAttributesTo(DynamicAttribute dynamicAttribute)
        {
            DynamicAttributeFixed_CopyAttributesTo(nativePointer, dynamicAttribute.NativePointer);
        }

        public DynamicAttributeType Type
        {
            get { return DynamicAttributeTypeHelper.GetType(nativePointer); }
            set { DynamicAttributeTypeHelper.SetType(nativePointer, value); }
        }


        #region DynamicAttributeFixed PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeFixed_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeFixed_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeFixed_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeFixed_New(IntPtr dynamicAttributeFixed);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeFixed_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeFixed_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeFixed_GetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeFixed_GetValue(IntPtr ptr, float x = 0);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeFixed_SetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeFixed_SetValue(IntPtr ptr, float value);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeFixed_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeFixed_CopyAttributesTo(IntPtr ptr, IntPtr dynamicAttribute);
        #endregion
    }

    public class DynamicAttributeRandom : DynamicAttribute, IDisposable
    {
        internal IntPtr nativePointer;

        public IntPtr NativePointer
        {
            get { return nativePointer; }
            set { nativePointer = value; }
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
            DynamicAttributeRandom_Destroy(NativePointer);
        }

        #endregion


        internal DynamicAttributeRandom(IntPtr ptr)
        {
            nativePointer = ptr;
        }

        public DynamicAttributeType Type
        {
            get { return DynamicAttributeTypeHelper.GetType(nativePointer); }
            set { DynamicAttributeTypeHelper.SetType(nativePointer, value); }
        }

        /** Constructor
			*/
        public DynamicAttributeRandom()
        {
            nativePointer = DynamicAttributeRandom_New();
        }

        /** Copy constructor
        */
        public DynamicAttributeRandom(DynamicAttributeRandom dynamicAttributeRandom)
        {
            nativePointer = DynamicAttributeRandom_New(dynamicAttributeRandom.NativePointer);
        }


        /** Todo
        */
        public float GetValue(float x = 0)
        {
            return DynamicAttributeRandom_GetValue(nativePointer, x);
        }

        /** Todo
        */
        public float Min
        {
            set { DynamicAttributeRandom_SetMin(nativePointer, value); }
            get { return DynamicAttributeRandom_GetMin(nativePointer); }
        }
        public float Max
        {
            set { DynamicAttributeRandom_SetMax(nativePointer, value); }
            get { return DynamicAttributeRandom_GetMax(nativePointer); }
        }

        public void SetMinMax(float min, float max)
        {
            DynamicAttributeRandom_SetMinMax(nativePointer, min, max);
        }

        /** Todo
        */
        public void CopyAttributesTo(DynamicAttribute dynamicAttribute)
        {
            DynamicAttributeRandom_CopyAttributesTo(nativePointer, dynamicAttribute.NativePointer);
        }


        #region DynamicAttributeRandom PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeRandom_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeRandom_New(IntPtr dynamicAttributeRandom);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeRandom_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_GetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeRandom_GetValue(IntPtr ptr, float x = 0);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_SetMin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeRandom_SetMin(IntPtr ptr, float min);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_GetMin", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeRandom_GetMin(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_SetMax", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeRandom_SetMax(IntPtr ptr, float max);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_GetMax", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeRandom_GetMax(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_SetMinMax", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeRandom_SetMinMax(IntPtr ptr, float min, float max);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeRandom_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeRandom_CopyAttributesTo(IntPtr ptr, IntPtr dynamicAttribute);
        #endregion
    }

    public class DynamicAttributeCurved : DynamicAttribute, IDisposable
    {
        public const bool printDebug = true;

        internal IntPtr nativePointer;

        public IntPtr NativePointer
        {
            get { return nativePointer; }
            set { nativePointer = value; }
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
            DynamicAttributeCurved_Destroy(NativePointer);
        }

        #endregion


        internal DynamicAttributeCurved(IntPtr ptr)
        {
            nativePointer = ptr;
        }
                public DynamicAttributeType Type
        {
            get { return DynamicAttributeTypeHelper.GetType(nativePointer); }
            set { DynamicAttributeTypeHelper.SetType(nativePointer, value); }
        }

        /** Constructor
			*/
                public DynamicAttributeCurved()
            {
                nativePointer = DynamicAttributeCurved_New();
            }

            public DynamicAttributeCurved(InterpolationType interpolationType)
            {
                nativePointer = DynamicAttributeCurved_New(interpolationType);
            }

			/** Copy constructor
			*/
            public DynamicAttributeCurved(DynamicAttributeCurved dynamicAttributeCurved)
            {
                nativePointer = DynamicAttributeCurved_New(dynamicAttributeCurved.nativePointer);
            }

			/** Get and set the curve type
			*/
            public InterpolationType InterpolationType
            {
                get { return DynamicAttributeCurved_GetInterpolationType(nativePointer); }
                set { DynamicAttributeCurved_SetInterpolationType(nativePointer, value); }
            }


			/** Todo
			*/
            public float GetValue(float x = 0)
            {
                return DynamicAttributeCurved_GetValue(nativePointer, x);
            }

			/** Todo
			*/
            public void AddControlPoint(float x, float y)
            {
                DynamicAttributeCurved_AddControlPoint(nativePointer, x, y);
            }

			/** Todo
			*/
			public Mogre.Vector2[] GetControlPoints ()
            {
                try
                {
                    int bufSize = DynamicAttributeCurved_GetControlPointsCount(nativePointer);
                    Mogre.Vector2[] floats = new Mogre.Vector2[bufSize];
                    DynamicAttributeCurved_GetControlPoints(nativePointer, floats, bufSize);
                    return floats;
                }
                catch (Exception e) { if (printDebug) Console.WriteLine(e.Message + "@" + e.Source); }
                return null;

    }

			/** Todo
			*/
            public void ProcessControlPoints()
            {
                DynamicAttributeCurved_ProcessControlPoints(nativePointer);
            }

			/** Todo
			*/
            public uint GetNumControlPoints()
            {
                return DynamicAttributeCurved_GetNumControlPoints(nativePointer);
            }

			/** Todo
			*/
            public void RemoveAllControlPoints()
            {
                DynamicAttributeCurved_RemoveAllControlPoints(nativePointer);
            }

			/** Todo
			*/
            public void CopyAttributesTo(DynamicAttribute dynamicAttribute)
            {
                DynamicAttributeCurved_CopyAttributesTo(nativePointer, dynamicAttribute.NativePointer);
            }


        #region DynamicAttributeCurved PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeCurved_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeCurved_New(InterpolationType interpolationType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_New3", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeCurved_New(IntPtr dynamicAttributeCurved);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_SetInterpolationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_SetInterpolationType(IntPtr ptr, InterpolationType interpolationType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_GetInterpolationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern InterpolationType DynamicAttributeCurved_GetInterpolationType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_GetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeCurved_GetValue(IntPtr ptr, float x = 0);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_AddControlPoint", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_AddControlPoint(IntPtr ptr, float x, float y);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_GetControlPointsCount", CallingConvention = CallingConvention.Cdecl)]
        internal static extern int DynamicAttributeCurved_GetControlPointsCount(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_GetControlPoints", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_GetControlPoints(IntPtr ptr, [In, Out] [MarshalAs(UnmanagedType.LPArray)] Mogre.Vector2[] arrLodDistances, int bufSize);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_ProcessControlPoints", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_ProcessControlPoints(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_GetNumControlPoints", CallingConvention = CallingConvention.Cdecl)]
        internal static extern uint DynamicAttributeCurved_GetNumControlPoints(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_RemoveAllControlPoints", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_RemoveAllControlPoints(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeCurved_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeCurved_CopyAttributesTo(IntPtr ptr, IntPtr dynamicAttribute);
        #endregion
    }

    public class DynamicAttributeOscillate : DynamicAttribute, IDisposable
    {
        public enum OscillationType
        {
            OSCT_SINE = 0,
            OSCT_SQUARE = 1
        };

        internal IntPtr nativePointer;

        public IntPtr NativePointer
        {
            get { return nativePointer; }
            set { nativePointer = value; }
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
            DynamicAttributeOscillate_Destroy(NativePointer);
        }

        #endregion


        internal DynamicAttributeOscillate(IntPtr ptr)
        {
            nativePointer = ptr;
        }

        public DynamicAttributeType Type
        {
            get { return DynamicAttributeTypeHelper.GetType(nativePointer); }
            set { DynamicAttributeTypeHelper.SetType(nativePointer, value); }
        }

        /** Constructor
			*/
        public DynamicAttributeOscillate()
                            {
                nativePointer = DynamicAttributeOscillate_New();
            }

			/** Copy constructor
			*/
        public DynamicAttributeOscillate(DynamicAttributeOscillate dynamicAttributeOscillate)
            {
                nativePointer = DynamicAttributeOscillate_New(dynamicAttributeOscillate.nativePointer);
            }

			/** Todo
			*/
        public float GetValue(float x = 0)
            {
                return DynamicAttributeOscillate_GetValue(nativePointer, x);
            }

			/** Get and set the OscillationType
			*/
        public OscillationType Oscillation_Type  
            {
                get{return DynamicAttributeOscillate_GetOscillationType(nativePointer);}
                set{DynamicAttributeOscillate_SetOscillationType(nativePointer, value);}
            }

			/** Get and set the Frequency
			*/
        public float Frequency  
            {
                get{return DynamicAttributeOscillate_GetFrequency(nativePointer);}
                set{DynamicAttributeOscillate_SetFrequency(nativePointer, value);}
            }

			/** Get and set the Phase
			*/
        public float Phase  
            {
                get{return DynamicAttributeOscillate_GetPhase(nativePointer);}
                set{DynamicAttributeOscillate_SetPhase(nativePointer, value);}
            }

			/** Get and set the Base
			*/
        public float Base  
            {
                get{return DynamicAttributeOscillate_GetBase(nativePointer);}
                set{DynamicAttributeOscillate_SetBase(nativePointer, value);}
            }


			/** Get and set the Amplitude
			*/
        public float Amplitude
            {
                get{return DynamicAttributeOscillate_GetAmplitude(nativePointer);}
                set{DynamicAttributeOscillate_SetAmplitude(nativePointer, value);}
            }

			/** Todo
			*/
        public void CopyAttributesTo(DynamicAttribute dynamicAttribute)
            {
                DynamicAttributeOscillate_CopyAttributesTo(nativePointer, dynamicAttribute.NativePointer);
            }


        #region DynamicAttributeCurved PInvoke

        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeOscillate_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_New2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr DynamicAttributeOscillate_New(IntPtr dynamicAttributeOscillate);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_GetValue", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeOscillate_GetValue(IntPtr ptr, float x = 0);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_GetOscillationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern DynamicAttributeOscillate.OscillationType DynamicAttributeOscillate_GetOscillationType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_SetOscillationType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_SetOscillationType(IntPtr ptr, DynamicAttributeOscillate.OscillationType oscillationType);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_GetFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeOscillate_GetFrequency(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_SetFrequency", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_SetFrequency(IntPtr ptr, float frequency);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_GetPhase", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeOscillate_GetPhase(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_SetPhase", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_SetPhase(IntPtr ptr, float phase);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_GetBase", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeOscillate_GetBase(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_SetBase", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_SetBase(IntPtr ptr, float baseValue);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_GetAmplitude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern float DynamicAttributeOscillate_GetAmplitude(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_SetAmplitude", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_SetAmplitude(IntPtr ptr, float amplitude);
        [DllImport("ParticleUniverse.dll", EntryPoint = "DynamicAttributeOscillate_CopyAttributesTo", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void DynamicAttributeOscillate_CopyAttributesTo(IntPtr ptr, IntPtr dynamicAttribute);
        #endregion
    }
}