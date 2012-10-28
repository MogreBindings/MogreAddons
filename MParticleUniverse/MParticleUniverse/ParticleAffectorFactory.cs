using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public class ParticleAffectorFactory
    {
        internal IntPtr NativePointer;

        internal ParticleAffectorFactory(IntPtr ptr)
        {
            NativePointer = ptr;
        }

    }
}
