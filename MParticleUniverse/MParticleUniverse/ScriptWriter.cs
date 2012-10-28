using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// A class that implements the ScriptWriter, is responsible for writing (serialization) to a particle script.
    /// </summary>
    internal abstract class ScriptWriter : IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ScriptWriter(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        /// <summary>
        /// Child classes must implement this pure virtual function, which must be used to write an object or attribute to
        ///		a particle script.
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="element"></param>
        public void write(ParticleScriptSerializer serializer, IElement element)
        {
            if (serializer == null)
                throw new ArgumentNullException("serializer cannot be null!");
            if (element == null)
                throw new ArgumentNullException("element cannot be null!");
            ScriptWriter_Write(nativePtr, serializer.nativePtr, element.NativePointer);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptWriter_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScriptWriter_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptWriter_Write", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScriptWriter_Write(IntPtr ptr, IntPtr serializer, IntPtr element);
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
            ScriptWriter_Destroy(NativePointer);
        }

        #endregion

    }
}
