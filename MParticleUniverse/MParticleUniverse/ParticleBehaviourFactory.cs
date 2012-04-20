using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public class ParticleBehaviourFactory 
    {
        internal IntPtr NativePointer;

        internal ParticleBehaviourFactory(IntPtr ptr)
        {
            NativePointer = ptr;
        }
    }
}
