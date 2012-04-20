using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MParticleUniverse
{
    public interface TechniqueListener
    {
        /// <summary>
        /// The Particle System in unamanged memory this class represents.
        /// </summary>
        IntPtr NativePointer { get; }

        void ParticleEmitted(ParticleTechnique particleTechnique, Particle particle);
        void ParticleExpired(ParticleTechnique particleTechnique, Particle particle);


    }
}
