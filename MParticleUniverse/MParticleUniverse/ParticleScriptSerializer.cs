using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    ///<summary>
    ///The ParticleScriptSerializer class is responsible for writing objects and attributes to a particle system script.
    ///</summary>
    public class ParticleScriptSerializer : IDisposable 
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer { get { return nativePtr; } }

        internal static Dictionary<IntPtr, ParticleScriptSerializer> particleScriptSerializerInstances;

        internal static ParticleScriptSerializer GetInstance(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            if (particleScriptSerializerInstances == null)
                particleScriptSerializerInstances = new Dictionary<IntPtr, ParticleScriptSerializer>();

            ParticleScriptSerializer newvalue;

            if (particleScriptSerializerInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = new ParticleScriptSerializer(ptr);
            particleScriptSerializerInstances.Add(ptr, newvalue);
            return newvalue;
        }

        internal ParticleScriptSerializer(IntPtr ptr)
        {
            nativePtr = ptr;
        }

        public Context context
        {
            get { return Context.GetInstance(ParticleScriptSerializer_GetContext(nativePtr)); }
            set { 
                if (value == null)
                    ParticleScriptSerializer_SetContext(nativePtr, IntPtr.Zero);
                else
                    ParticleScriptSerializer_SetContext(nativePtr, value.NativePointer); 
            }
        }

        /// <summary>
        /// The ParticleScriptSerializer class is responsible for writing objects and attributes to a particle system script.
        /// </summary>
        public ParticleScriptSerializer()
        {
            nativePtr = ParticleScriptSerializer_New();
            particleScriptSerializerInstances.Add(nativePtr, this);
        }

        /// <summary>
        /// Writes a ParticleSystem to a file in the script format.
        /// </summary>
        public void WriteScript(ParticleSystem particleSystem, String fileName)
        {
            if (particleSystem == null)
                throw new ArgumentNullException("particleSystem cannot be null!");
            if (fileName == null || fileName.Length == 0)
                throw new ArgumentNullException("fileName cannot be null or Empty!");
            ParticleScriptSerializer_WriteScript(nativePtr, particleSystem.nativePtr, fileName);
        }

        /// <summary>
        /// Writes a line to a script file.
        /// </summary>
        public String WriteScript(ParticleSystem particleSystem)
        {
            if (particleSystem == null)
                throw new ArgumentNullException("particleSystem cannot be null!");
            return Marshal.PtrToStringAnsi(ParticleScriptSerializer_WriteScript(nativePtr, particleSystem.nativePtr));
        }

        /// <summary>
        /// Writes a line to a script file.
        /// </summary>
        public void writeLine(
            String s0,
            String s1,
            String s2,
            String s3,
            String s4,
            short indentation0 = -1,
            short indentation1 = -1,
            short indentation2 = -1,
            short indentation3 = -1,
            short indentation4 = -1)
        {
            ParticleScriptSerializer_WriteLine(nativePtr, s0, s1, s2, s3, s4, indentation0, indentation1, indentation2, indentation3, indentation4);
        }

        /// <summary>
        /// Writes a line to a script file.
        /// </summary>
        public void writeLine(
            String s0,
            String s1,
            String s2,
            String s3,
            short indentation0 = -1,
            short indentation1 = -1,
            short indentation2 = -1,
            short indentation3 = -1)
        {
            ParticleScriptSerializer_WriteLine(nativePtr, s0, s1, s2, s3, indentation0, indentation1, indentation2, indentation3);
        }

        /// <summary>
        /// Writes a line to a script file.
        /// </summary>
        public void writeLine(
            String s0,
            String s1,
            String s2,
            short indentation0 = -1,
            short indentation1 = -1,
            short indentation2 = -1)
        {
            ParticleScriptSerializer_WriteLine(nativePtr, s0, s1, s2, indentation0, indentation1, indentation2);
        }

        /// <summary>
        /// Writes a line to a script file.
        /// </summary>
        public void writeLine(
            String s0,
            String s1,
            short indentation0 = -1,
            short indentation1 = -1)
        {
            ParticleScriptSerializer_WriteLine(nativePtr, s0, s1, indentation0, indentation1);
        }

        /// <summary>
        /// Writes a line to a script file.
        /// </summary>
        public void writeLine(
            String s0,
            short indentation0 = -1)
        {
            ParticleScriptSerializer_WriteLine(nativePtr, s0, indentation0);
        }

        /// <summary>
        /// Set the default tab values. If a writeline doesn't contain any indentation value, the default tab values are used.
        /// </summary>
        public void SetDefaultTabs(
            short tab0 = 0,
            short tab1 = 48,
            short tab2 = 52,
            short tab3 = 56,
            short tab4 = 60)
        {
            ParticleScriptSerializer_SetDefaultTabs(nativePtr, tab0, tab1, tab2, tab3, tab4);
        }

        /// <summary>
        /// Set the directory to which files are serialized.
        /// </summary>
        public void SetPath(String path)
        {
            if (path == null || path.Length == 0)
                throw new ArgumentNullException("fileName cannot be null or Empty!");
            ParticleScriptSerializer_SetPath(nativePtr, path);
        }

        /// <summary>
        /// Creates a string from a vector with Reals. This function contains an additional argument. If set to true, the
        ///    square root is applied to all real values (don't ask!).
        /// </summary>
        public String ToString(float[] vector, bool applySqrt = false)
        {
            return Marshal.PtrToStringAnsi(ParticleScriptSerializer_ToString(vector, applySqrt));
        }

        /// <summary>
        /// Get/set the indentation. To be used in cases where it is unkown what the indentation is.
        /// </summary>
        public short Indentation
        {
            get { return ParticleScriptSerializer_GetIndentation(nativePtr); }
            set { ParticleScriptSerializer_SetIndentation(nativePtr, value); }
        }
        //public const short getIndentation(void) const;
        //public void setIndentation(const short indentation);

        
        /// <summary>
        /// Get/set the keyword. To be used in cases where it is unkown what the keyword is or in case serialisation is spread across multiple objects.
        /// </summary>
        public String Keyword
        {
            get { return Marshal.PtrToStringAnsi(ParticleScriptSerializer_GetKeyword(nativePtr)); }
            set { ParticleScriptSerializer_SetKeyword(nativePtr, value); }
        }
        //public const String& getKeyword(void) const;
        //public void setKeyword(const String& keyword);

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
            ParticleScriptSerializer_Destroy(nativePtr);
            particleScriptSerializerInstances.Remove(nativePtr);
        }

        #endregion

        #region PInvoke
        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_GetContext", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleScriptSerializer_GetContext(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_SetContext", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_SetContext(IntPtr ptr, IntPtr context);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_New", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleScriptSerializer_New();

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_Destroy", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_Destroy(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteScript", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_WriteScript(IntPtr ptr, IntPtr particleSystem, [MarshalAs(UnmanagedType.LPStr)]string fileName);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteScript2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleScriptSerializer_WriteScript(IntPtr ptr, IntPtr particleSystem);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteLine4", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_WriteLine(IntPtr ptr,
                                               String s0, [MarshalAs(UnmanagedType.LPStr)]string s1, [MarshalAs(UnmanagedType.LPStr)]string s2, [MarshalAs(UnmanagedType.LPStr)]string s3, [MarshalAs(UnmanagedType.LPStr)]string s4,
                                               short indentation0 = -1,
                                               short indentation1 = -1,
                                               short indentation2 = -1,
                                               short indentation3 = -1,
                                               short indentation4 = -1);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteLine3", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_WriteLine(IntPtr ptr,
                                               String s0, [MarshalAs(UnmanagedType.LPStr)]string s1, [MarshalAs(UnmanagedType.LPStr)]string s2, [MarshalAs(UnmanagedType.LPStr)]string s3,
                                               short indentation0 = -1,
                                               short indentation1 = -1,
                                               short indentation2 = -1,
                                               short indentation3 = -1);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteLine2", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_WriteLine(IntPtr ptr,
                                               String s0, [MarshalAs(UnmanagedType.LPStr)]string s1, [MarshalAs(UnmanagedType.LPStr)]string s2,
                                               short indentation0 = -1, short indentation1 = -1,
                                               short indentation2 = -1);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteLine1", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_WriteLine(IntPtr ptr,
                                               String s0, [MarshalAs(UnmanagedType.LPStr)]string s1,
                                               short indentation0 = -1, short indentation1 = -1);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_WriteLine", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_WriteLine(IntPtr ptr,
                                               String s0, short indentation0 = -1);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_SetDefaultTabs", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_SetDefaultTabs(IntPtr ptr,
                short tab0 = 0, short tab1 = 48, short tab2 = 52, short tab3 = 56, short tab4 = 60);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_SetPath", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_SetPath(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string path);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_ToString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleScriptSerializer_ToString([In, Out] [MarshalAs(UnmanagedType.LPArray)] float[] vector, bool applySqrt = false);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_GetIndentation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern short ParticleScriptSerializer_GetIndentation(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_SetIndentation", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_SetIndentation(IntPtr ptr, short indentation);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_GetKeyword", CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ParticleScriptSerializer_GetKeyword(IntPtr ptr);

        [DllImport("ParticleUniverse.dll", EntryPoint = "ParticleScriptSerializer_SetKeyword", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ParticleScriptSerializer_SetKeyword(IntPtr ptr, [MarshalAs(UnmanagedType.LPStr)]string keyword);

        #endregion
    }
}