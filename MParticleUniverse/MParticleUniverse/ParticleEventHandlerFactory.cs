using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public class ParticleEventHandlerFactory
    {
        internal IntPtr NativePointer;

        internal ParticleEventHandlerFactory(IntPtr ptr)
        {
            NativePointer = ptr;
        }
    }
}
