using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using MParticleUniverse.Externs;

namespace MParticleUniverse
{
    /// <summary>
    /// This is the base factory of all Extern implementations.
    /// You can't create an Extern. It will throw an Exception.
    /// </summary>
    public class ExternFactory : IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal ExternFactory(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        internal static Extern GetExternByPointer(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;

            String ClassName = Marshal.PtrToStringAnsi(ExternFactory_GetExternType(ptr));
            if (string.Compare(ClassName.ToString(), "BoxColliderExtern", true, CultureInfo.InvariantCulture) == 0)
                return BoxColliderExtern.GetInstance(ptr);
            if (string.Compare(ClassName.ToString(), "GravityExtern", true, CultureInfo.InvariantCulture) == 0)
                return GravityExtern.GetInstance(ptr);
            if (string.Compare(ClassName.ToString(), "SceneDecoratorExtern", true, CultureInfo.InvariantCulture) == 0)
                return SceneDecoratorExtern.GetInstance(ptr);
            if (string.Compare(ClassName.ToString(), "SphereColliderExtern", true, CultureInfo.InvariantCulture) == 0)
                return SphereColliderExtern.GetInstance(ptr);
            if (string.Compare(ClassName.ToString(), "VortexExtern", true, CultureInfo.InvariantCulture) == 0)
                return VortexExtern.GetInstance(ptr);
            return null;
        }
        
        internal static Extern GetExternByType(String type)
        {
            if (type == "BoxColliderExtern")
                return new BoxColliderExtern();
            if (type == "GravityExtern")
                return new GravityExtern();
            if (type == "SceneDecoratorExtern")
                return new SceneDecoratorExtern();
            if (type == "SphereColliderExtern")
                return new SphereColliderExtern();
            if (type == "VortexExtern")
                return new VortexExtern();
            return null;
        }

        /// <summary>
        /// Returns the type of the factory, which identifies the Extern type this factory creates.
        /// </summary>
        public String ExternType
        {
            get { return Marshal.PtrToStringAnsi(ExternFactory_GetExternType(NativePointer)); }
        }

        /// <summary>
        /// Creates a new Extern instance.
        /// Not Implemented Will throw an exception!
        /// </summary>
        /// <returns></returns>
        Extern CreateExtern()
        {
            throw new NotImplementedException("Externs aren't Implemented Here!");
            //return new Extern(ExternFactory_CreateExtern(NativePointer));
        }

        /// <summary>
        /// Delete an Extern.
        /// </summary>
        /// <param name="externObject"></param>
        void DestroyExtern(Extern externObject)
        {
            if (externObject == null)
                throw new ArgumentNullException("externObject cannot be null!");
            ExternFactory_DestroyExtern(NativePointer, externObject.NativePointer);
        }

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ExternFactory_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ExternFactory_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ExternFactory_GetExternType", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExternFactory_GetExternType(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ExternFactory_CreateExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ExternFactory_CreateExtern(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ExternFactory_DestroyExtern", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ExternFactory_DestroyExtern(IntPtr ptr, IntPtr externObject);
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
            ExternFactory_Destroy(nativePtr);
        }

        #endregion

    }
}
