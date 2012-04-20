using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public class ParticleObserverFactory
    {
        internal IntPtr NativePointer;

        internal ParticleObserverFactory(IntPtr ptr)
        {
            NativePointer = ptr;
        }

    }
}
