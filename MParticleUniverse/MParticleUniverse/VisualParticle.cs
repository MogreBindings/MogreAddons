using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MParticleUniverse
{
    /// <summary>
    /// A VisualParticle is the most obvious implementation of a particle. It represents that particles that can be
	///	visualised on the screen.
    /// </summary>
    public unsafe class VisualParticle : Particle, IParticle, IDisposable
    {
        internal IntPtr nativePtr;

        public IntPtr NativePointer
        {
            get { return nativePtr; }
        }
        internal VisualParticle(IntPtr ptr)
            : base(ptr)
        {
            nativePtr = ptr;
        }

        internal static Dictionary<IntPtr, VisualParticle> visualParticleInstances;

        internal static VisualParticle GetInstances(IntPtr ptr)
        {
            if (visualParticleInstances == null)
                visualParticleInstances = new Dictionary<IntPtr, VisualParticle>();

            VisualParticle newvalue;

            if (visualParticleInstances.TryGetValue(ptr, out newvalue))
            {
                return newvalue;
            }
            newvalue = VisualParticle.GetInstances(ptr);
            visualParticleInstances.Add(ptr, newvalue);
            return newvalue;
        }

        public VisualParticle() : base(VisualParticle_New())
        {
            nativePtr = base.NativePointer;
            visualParticleInstances.Add(nativePtr, this);
        }
           

			/** Current and original colour */
        public Mogre.ColourValue Colour {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)VisualParticle_GetColour(nativePtr).ToPointer()));
                return vec; 
            }
            set
            {
                IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, colourPtr, true);
                VisualParticle_SetColour(nativePtr, colourPtr);
            }
        }
        public Mogre.ColourValue OriginalColour {
            get
            {
                Mogre.ColourValue vec = *(((Mogre.ColourValue*)VisualParticle_GetOriginalColour(nativePtr).ToPointer()));
                return vec; 
            }
            set
            {
                IntPtr colourPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, colourPtr, true);
                VisualParticle_SetOriginalColour(nativePtr, colourPtr);
            }
        }

			/** zRotation is used to rotate the particle in 2D (around the Z-axis)
			@remarks
				There is no relation between zRotation and orientation.
				rotationSpeed in combination with orientation are used for 3D rotation of the particle, while
				zRotation means the rotation around the Z-axis. This type of rotation is typically used for 
				rotating textures. This also means that both types of rotation can be used together.
			*/
        public Mogre.Radian ZRotation {
            get
            {
                Mogre.Radian vec = *(((Mogre.Radian*)VisualParticle_GetZRotation(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                IntPtr radianPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, radianPtr, true);
                VisualParticle_SetZRotation(nativePtr, radianPtr); 
            }
        }

			/** The zRotationSpeed is used in combination with zRotation and defines tha actual rotationspeed
				in 2D. */
        public Mogre.Radian ZRotationSpeed {
            get
            {
                Mogre.Radian vec = *(((Mogre.Radian*)VisualParticle_GetZRotationSpeed(nativePtr).ToPointer()));
                return vec;
            }
            set
            {
                IntPtr radianPtr = Marshal.AllocHGlobal(Marshal.SizeOf(value));
                Marshal.StructureToPtr(value, radianPtr, true);
                VisualParticle_SetZRotationSpeed(nativePtr, radianPtr); 
            }
        }

			/*  Orientation of the particle.
			@remarks
				The orientation of the particle is only visible if the Particle Renderer - such as the Box renderer - 
				supports orientation.
			*/
        public Mogre.Quaternion Orientation {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)VisualParticle_GetOrientation(nativePtr).ToPointer()));
                return vec; 
            } 
            set { VisualParticle_SetOrientation(nativePtr, value); } 
        }
        public Mogre.Quaternion OriginalOrientation {
            get
            {
                Mogre.Quaternion vec = *(((Mogre.Quaternion*)VisualParticle_GetOriginalOrientation(nativePtr).ToPointer()));
                return vec; 
            } 
            set { VisualParticle_SetOriginalOrientation(nativePtr, value); } 
        }

			/** The rotation is used in combination with orientation. Because the rotation speed is part
				of the particle itself, it can be changed independently. */
        public float RotationSpeed { get { return VisualParticle_GetRotationSpeed(nativePtr); } set { VisualParticle_SetRotationSpeed(nativePtr, value); } }

			/** The rotation axis is used in combination with orientation. Because the rotation axis is part
				of the particle itself, it can be changed independently. */
        public Mogre.Vector3 RotationAxis {
            get
            {
                Mogre.Vector3 vec = *(((Mogre.Vector3*)VisualParticle_GetRotationAxis(nativePtr).ToPointer()));
                return vec;
            } 
            set { VisualParticle_SetRotationAxis(nativePtr, value); } 
        }

			/** Does this particle have it's own dimensions? */
        public bool OwnDimensions { get { return VisualParticle_GetOwnDimensions(nativePtr); } set { VisualParticle_SetOwnDimensions(nativePtr, value); } }

			/** Own width
			*/
        public float Width { get { return VisualParticle_GetWidth(nativePtr); } set { VisualParticle_SetWidth(nativePtr, value); } }
        
			/** Own height
			*/
        public float Height { get { return VisualParticle_GetHeight(nativePtr); } set { VisualParticle_SetHeight(nativePtr, value); } }

			/** Own depth
			*/
        public float Depth { get { return VisualParticle_GetDepth(nativePtr); } set { VisualParticle_SetDepth(nativePtr, value); } }

			/** Radius of the particle, to be used for inter-particle collision and such.
			*/
        public float Radius { get { return VisualParticle_GetRadius(nativePtr); } set { VisualParticle_SetRadius(nativePtr, value); } }

			/** Animation attributes
			*/
        public float TextureAnimationTimeStep { get { return VisualParticle_GetTextureAnimationTimeStep(nativePtr); } set { VisualParticle_SetTextureAnimationTimeStep(nativePtr, value); } }
        public float TextureAnimationTimeStepCount { get { return VisualParticle_GetTextureAnimationTimeStepCount(nativePtr); } set { VisualParticle_SetTextureAnimationTimeStepCount(nativePtr, value); } }
        public ushort TextureCoordsCurrent { get { return VisualParticle_GetTextureCoordsCurrent(nativePtr); } set { VisualParticle_SetTextureCoordsCurrent(nativePtr, value); } }
        public bool TextureAnimationDirectionUp { get { return VisualParticle_GetTextureAnimationDirectionUp(nativePtr); } set { VisualParticle_SetTextureAnimationDirectionUp(nativePtr, value); } }

			/** Set own dimensions
			*/
            public void SetOwnDimensions(float newWidth, float newHeight, float newDepth)
            {
                VisualParticle_SetOwnDimensions(nativePtr, newWidth, newHeight, newDepth);
            }

			/** @see Particle::_initForEmission()
			*/
            public void _initForEmission()
            {
                VisualParticle__initForEmission(nativePtr);
            }

			/** @see Particle::_initForExpiration()
			*/
            public void _initForExpiration(ParticleTechnique technique, float timeElapsed)
            {
                if (technique == null)
                    throw new ArgumentNullException("technique cannot be null!");
                VisualParticle__initForExpiration(nativePtr, technique.nativePtr, timeElapsed);
            }

			/** Calculate the bounding sphere radius
			*/
            public void _calculateBoundingSphereRadius()
            {
                VisualParticle__calculateBoundingSphereRadius(nativePtr);
            }
        #region PInvoke
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_New", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_New();
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_Destroy", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_Destroy(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetColour", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetColour(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetColour", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetColour(IntPtr ptr, IntPtr newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetOriginalColour", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetOriginalColour(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetOriginalColour", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetOriginalColour(IntPtr ptr, IntPtr newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetZRotation", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetZRotation(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetZRotation", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetZRotation(IntPtr ptr, IntPtr newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetZRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetZRotationSpeed(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetZRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetZRotationSpeed(IntPtr ptr, IntPtr newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetOrientation", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetOrientation(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetOrientation", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetOrientation(IntPtr ptr, Mogre.Quaternion newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetOriginalOrientation", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetOriginalOrientation(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetOriginalOrientation", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetOriginalOrientation(IntPtr ptr, Mogre.Quaternion newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetRotationSpeed(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetRotationSpeed", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetRotationSpeed(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetRotationAxis", CallingConvention = CallingConvention.Cdecl)]
            internal static extern IntPtr VisualParticle_GetRotationAxis(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetRotationAxis", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetRotationAxis(IntPtr ptr, Mogre.Vector3 newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetOwnDimensions", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool VisualParticle_GetOwnDimensions(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetOwnDimensions", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetOwnDimensions(IntPtr ptr, bool newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetWidth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetWidth(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetWidth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetWidth(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetHeight", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetHeight(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetHeight", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetHeight(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetDepth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetDepth(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetDepth", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetDepth(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetRadius", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetRadius(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetRadius", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetRadius(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetTextureAnimationTimeStep", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetTextureAnimationTimeStep(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetTextureAnimationTimeStep", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetTextureAnimationTimeStep(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetTextureAnimationTimeStepCount", CallingConvention = CallingConvention.Cdecl)]
            internal static extern float VisualParticle_GetTextureAnimationTimeStepCount(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetTextureAnimationTimeStepCount", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetTextureAnimationTimeStepCount(IntPtr ptr, float newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetTextureCoordsCurrent", CallingConvention = CallingConvention.Cdecl)]
            internal static extern ushort VisualParticle_GetTextureCoordsCurrent(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetTextureCoordsCurrent", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetTextureCoordsCurrent(IntPtr ptr, ushort newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_GetTextureAnimationDirectionUp", CallingConvention = CallingConvention.Cdecl)]
            internal static extern bool VisualParticle_GetTextureAnimationDirectionUp(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetTextureAnimationDirectionUp", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetTextureAnimationDirectionUp(IntPtr ptr, bool newValue);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle_SetOwnDimensions2", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle_SetOwnDimensions(IntPtr ptr, float newWidth, float newHeight, float newDepth);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle__initForEmission", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle__initForEmission(IntPtr ptr);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle__initForExpiration", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle__initForExpiration(IntPtr ptr, IntPtr technique, float timeElapsed);
            [DllImport("ParticleUniverse.dll", EntryPoint = "VisualParticle__calculateBoundingSphereRadius", CallingConvention = CallingConvention.Cdecl)]
            internal static extern void VisualParticle__calculateBoundingSphereRadius(IntPtr ptr);
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
                VisualParticle_Destroy(NativePointer);
                visualParticleInstances.Remove(nativePtr);
            }

            #endregion

    }
}
