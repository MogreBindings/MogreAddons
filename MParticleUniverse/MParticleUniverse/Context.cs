using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{

    public class Context : IDisposable
    {
        // String representation of the Particle Universe contexts. Numeric values are used to keep the string small.
        public const String ALIAS = "1";
        public const String SYSTEM = "2";
        public const String TECHNIQUE = "3";
        public const String RENDERER = "4";
        public const String EMITTER = "5";
        public const String AFFECTOR = "6";
        public const String OBSERVER = "7";
        public const String HANDLER = "8";
        public const String BEHAVIOUR = "9";
        public const String EXTERN = "10";
        public const String DYNAMIC_ATTRIBUTE = "11";
        public const String DEPENDENCY = "12";
        public const String INNER_CONTEXT = "13"; // To be used for deeper sections

        internal IntPtr nativePtr;

        public IntPtr NativePointer { get { return nativePtr; } }

        internal static Dictionary<IntPtr, Context> contextInstances;

        internal static Context GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (contextInstances == null)
                contextInstances = new Dictionary<IntPtr, Context>();

            Context newvalue;

            if (contextInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Context(ptr);
            contextInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal Context(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public Context()
        {
            nativePtr = Context_New();
            contextInstances.Add(nativePtr, this);
        }

        /** 
        */
        public void BeginSection(
                String sectionName,
                IElement element = null,
                String elementName = "")
        {
            if (element == null)
                Context_BeginSection(nativePtr, sectionName, IntPtr.Zero, elementName);
            else
                Context_BeginSection(nativePtr, sectionName, element.NativePointer, elementName);
        }

        /** 
        */
        public void EndSection()
        {
            Context_EndSection(nativePtr);
        }

        /** 
        */
        public String GetCurrentSectionName()
        {
            return Marshal.PtrToStringAnsi(Context_GetCurrentSectionName(nativePtr));
        }

        /** 
        */
        public String GetPreviousSectionName()
        {
            return Marshal.PtrToStringAnsi(Context_GetPreviousSectionName(nativePtr));
        }

        /** 
        */
        public String GetParentSectionName()
        {
            return Marshal.PtrToStringAnsi(Context_GetParentSectionName(nativePtr));
        }

        /** 
        */
        public IElement GetCurrentSectionElement()
        {
            return IElementHelper.GetIElementByPointer(Context_GetCurrentSectionElement(nativePtr));
        }

        /** 
        */
        public IElement GetPreviousSectionElement()
        {
            return IElementHelper.GetIElementByPointer(Context_GetPreviousSectionElement(nativePtr));
        }

        /** 
        */
        public IElement GetParentSectionElement()
        {
            return IElementHelper.GetIElementByPointer(Context_GetParentSectionElement(nativePtr));
        }

        /** 
        */
        public String GetCurrentSectionElementName()
        {
            return Marshal.PtrToStringAnsi(Context_GetCurrentSectionElementName(nativePtr));
        }

        /** 
        */
        public String GetPreviousSectionElementName()
        {
            return Marshal.PtrToStringAnsi(Context_GetPreviousSectionElementName(nativePtr));
        }

        /** 
        */
        public String GetParentSectionElementName()
        {
            return Marshal.PtrToStringAnsi(Context_GetParentSectionElementName(nativePtr));
        }

        /** 
        */
        public IElement GetSectionElement(String sName)
        {
            return IElementHelper.GetIElementByPointer(Context_GetSectionElement(nativePtr, sName));
        }

        /** 
        */
        public void ValidateCurrentSectionName(String sectionName,
                String calledFromFunction = "")
        {
            Context_ValidateCurrentSectionName(nativePtr, sectionName, calledFromFunction);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Context_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_BeginSection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Context_BeginSection(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string sectionName, IntPtr element, [MarshalAs(UnmanagedType.LPStr)]string elementName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_EndSection", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Context_EndSection(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetCurrentSectionName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetCurrentSectionName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetPreviousSectionName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetPreviousSectionName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetParentSectionName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetParentSectionName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetCurrentSectionElement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetCurrentSectionElement(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetPreviousSectionElement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetPreviousSectionElement(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetParentSectionElement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetParentSectionElement(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetCurrentSectionElementName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetCurrentSectionElementName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetPreviousSectionElementName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetPreviousSectionElementName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetParentSectionElementName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetParentSectionElementName(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_GetSectionElement", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Context_GetSectionElement(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string sName);
        [DllImport("ParticleUniverse.dll", EntryPoint = "Context_ValidateCurrentSectionName", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Context_ValidateCurrentSectionName(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string sectionName, [MarshalAs(UnmanagedType.LPStr)]string calledFromFunction);
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
            Context_Destroy(NativePointer);
            contextInstances.Remove(nativePtr);
        }

        #endregion

    }
}
