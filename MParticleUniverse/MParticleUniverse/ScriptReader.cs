using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// A class that implements the ScriptReader, is responsible for writing (serialization) to a particle script, but it is not a ScriptTranslator
    ///	itself. It can only delegate the translation to underlying components (Translators).
    /// </summary>
    internal class ScriptReader : IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }

        internal static Dictionary<IntPtr, ScriptReader> scriptReaderInstances;

        internal static ScriptReader GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (scriptReaderInstances == null)
                scriptReaderInstances = new Dictionary<IntPtr, ScriptReader>();

            ScriptReader newvalue;

            if (scriptReaderInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ScriptReader(ptr);
            scriptReaderInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ScriptReader(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public ScriptReader()
        {
            scriptReaderInstances.Add(nativePtr, this);
            throw new NotImplementedException("ScriptReader is not Implemented!");
        }

        ///** Child classes must implement this pure virtual function, which must be used to write an object or attribute to
        //    a particle script.
        //**/
        //void translate(ScriptCompiler compiler, AbstractNodePtr node)
        //{

        //}

        ///** Only parses a certain child property
        //*/
        //bool translateChildProperty(Mogre.Serializer.ScriptCompiler compiler, Mogre.AbstractNodePtr node)
        //{

        //    return false;
        //}

        ///** Only parses a certain child objec
        //*/
        //bool translateChildObject(Mogre.ScriptCompiler compiler, Mogre.AbstractNodePtr node)
        //{

        //    return false;
        //}

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptReader_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ScriptReader_New();
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptReader_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScriptReader_Destroy(IntPtr ptr);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptReader_Translate", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ScriptReader_Translate(IntPtr ptr, IntPtr compiler, IntPtr node);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptReader_TranslateChildProperty", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ScriptReader_TranslateChildProperty(IntPtr ptr, IntPtr compiler, IntPtr node);
        [DllImport("ParticleUniverse.dll", EntryPoint = "ScriptReader_TranslateChildObject", CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ScriptReader_TranslateChildObject(IntPtr ptr, IntPtr compiler, IntPtr node);
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
            ScriptReader_Destroy(NativePointer);
            scriptReaderInstances.Remove(nativePtr);
        }

        #endregion

    }
}
