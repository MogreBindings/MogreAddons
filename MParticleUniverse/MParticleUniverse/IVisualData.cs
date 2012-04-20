using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    public class IVisualData
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

        internal static Dictionary<IntPtr, IVisualData> iVisualDataInstances;

        internal static IVisualData GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (iVisualDataInstances == null)
                iVisualDataInstances = new Dictionary<IntPtr, IVisualData>();

            IVisualData newvalue;

            if (iVisualDataInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new IVisualData(ptr);
            iVisualDataInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal IVisualData(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public void SetVisible(bool isVisible)
        {
            IVisualData_SetVisible(nativePtr, isVisible);
        }

        [DllImport("ParticleUniverse.dll", EntryPoint = "IVisualData_SetVisible", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr IVisualData_SetVisible(IntPtr ptr, bool isVisible);
    }
}
