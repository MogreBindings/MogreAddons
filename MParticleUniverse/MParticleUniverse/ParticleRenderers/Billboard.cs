using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse.ParticleRenderers
{
    /// <summary>
    /// This is a child of the Ogre Billboard class, with the exception that it has new friends ;-)
    /// </summary>
    public unsafe class Billboard : Mogre.Billboard, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, Billboard> rendererInstances;

        internal static Billboard GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (rendererInstances == null)
                rendererInstances = new Dictionary<IntPtr, Billboard>();

            Billboard newvalue;

            if (rendererInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new Billboard(ptr);
            rendererInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal Billboard(IntPtr ptr)
            : base()
        {
            nativePtr = ptr;
        }
        public Billboard()
            : base()
        {
            nativePtr = Billboard_New();
            rendererInstances.Add(nativePtr, this);
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
            Billboard_Destroy(nativePtr);
            rendererInstances.Remove(nativePtr);
        }

        #endregion

        #region Billboard
[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetRendererType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr Billboard_New();
[DllImport("ParticleUniverse.dll", EntryPoint = "ParticleRenderer_GetRendererType", CallingConvention = CallingConvention.Cdecl)]
internal static extern void Billboard_Destroy(IntPtr ptr);
#endregion
    }
}
