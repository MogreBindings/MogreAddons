using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public class ParticleEmitterFactory
    {
        internal IntPtr NativePointer;

        internal ParticleEmitterFactory(IntPtr ptr)
        {
            NativePointer = ptr;
        }

    }
}
