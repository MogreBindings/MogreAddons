using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    /// <summary>
    /// ParticleSystemListener establishes a channel between a ParticleSystem and some other object, which is a 
	///    type of ParticleSystemListener.
    /// </summary>
    public interface ParticleSystemListener
    {
        IntPtr NativePointer { get; }

        void HandleParticleSystemEvent(ParticleSystem particleSystem, ParticleUniverseEvent particleUniverseEvent);

    }
}
