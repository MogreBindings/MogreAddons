using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public class ParticleRendererFactory
    {
        internal IntPtr NativePointer;

        internal ParticleRendererFactory(IntPtr ptr)
        {
            NativePointer = ptr;
        }
    }
}
