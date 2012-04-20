using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Globalization;
using MParticleUniverse.ParticleAffectors;
using MParticleUniverse.ParticleBehaviours;

namespace MParticleUniverse
{
    /// <summary>
    /// The IElement is used to identify classes that must be set as part of a section in case a script is parsed.
	///     I.e. If the parses encounters a ´technique´ section in a particle universe script, a ParticleTechnique
	/// 	object is created and must be set in the current section of the Context object. the
    /// 	ParticleTechnique object must be of type IElement to be stored in the Context object.
    /// </summary>
    public interface IElement
    {
        IntPtr NativePointer { get; set; }
    }

    public unsafe class IElementHelper
    {
        public static IElement GetIElementByPointer(IntPtr ptr)
        {
            if (ptr == null || ptr == IntPtr.Zero)
                return null;
            try
            {
                Particle.ParticleType partype = Particle.Particle_GetParticleType(ptr);
                
                if (partype == Particle.ParticleType.PT_TECHNIQUE)
                    return ParticleTechnique.GetInstances(ptr);
                else if (partype == Particle.ParticleType.PT_SYSTEM)
                    return ParticleSystem.GetInstances(ptr);
                else if (partype == Particle.ParticleType.PT_AFFECTOR)
                    return ParticleAffector.GetParticleAffectorByType(ptr);
                else if (partype == Particle.ParticleType.PT_EMITTER)
                    return ParticleEmitter.GetParticleEmitterByPointer(ptr);
                //else if (partype == Particle.ParticleType.PT_VISUAL)
                //    return new Particle(ptr);

                //For Some reason Particle.Particle_GetParticleType(ptr) doesn't return a valid type for PT_SYSTEM.
                //So test for ParticleSystem_GetCategory if it throws an exception then we know it really isn't one! ;)
                ParticleSystem.ParticleSystem_GetCategory(ptr);
                return ParticleSystem.GetInstances(ptr);
            }
            catch (Exception) { }

            try
            {
                String behaviourType = Marshal.PtrToStringAnsi(ParticleBehaviour.ParticleBehaviour_GetBehaviourType(ptr));
                //ParticleBehaviourFactory
                if (behaviourType == "SlaveBehaviour")
                    return SlaveBehaviour.GetInstance(ptr);
            }
            catch (Exception)
            {
                return null;
            }

            try
            {
                DynamicAttributeType type = DynamicAttributeTypeHelper.DynamicAttribute_GetType(ptr);
                
                if (type == DynamicAttributeType.DAT_CURVED ||
                    type == DynamicAttributeType.DAT_FIXED ||
                    type == DynamicAttributeType.DAT_OSCILLATE ||
                    type == DynamicAttributeType.DAT_RANDOM)
                    return DynamicAttributeTypeHelper.GetDynamicAttribute(ptr);
            }
            catch (Exception)
            {
            }

            return null;

        }
    }
}
