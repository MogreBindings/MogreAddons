This is an my first C++ Wrapper so bare with me.

Please see "Building Particle Universe For Mogre.pdf" about compiling the Particle Universe Plugin for Mogre.

The Wrapper can create, add, modify, and remove particle systems from mogre space.
See my test project in the SVN codebase for an idea of what it can do. 

There are a few things it can't do or I haven't implemented yet and I can imagine quite a few things that I don't know that don't work. 
It's mostly untested but I have manually created a few particle systems and am able to manipulate them in runtime. 

A few things I need to check are:
ParticleTechnique.WorldBoundingBox
ParticleSystem.GetBoundingBox
ParticleSystem.GetMainCamera
Attachable.GetBoundingBox
Check ParticleScriptSerializer and Strings being passed as null


ScriptReader not Implemented.
ParticleRendererFactory not Implemented.
ParticlePool.IncreasePool not Implemented.
ParticleObserverFactory not Implemented.
ParticleEventHandlerFactory not Implemented.
ParticleEmitterFactory not Implemented.
ParticleBehaviourFactory not Implemented.
ParticleAffectorFactory not Implemented.
ParticleFactory not Implemented.
Particle.UserDefinedObject not Implemented.
ExternFactory.CreateExtern not Implemented.


Classes to add?:

ParticleUniverseAtlasImage.h
ParticleUniverseNoise.h
ParticleUniversePhysicsExtern.h
ParticleUniversePhysicsShape.h
ParticleUniversePlane.h
ParticleUniversePoolMap.h
ParticleUniverseSpatialHashTable.h
Externs/ParticleUniversePhysXActorExtern.h
Externs/ParticleUniversePhysXBridge.h
Externs/ParticleUniversePhysXExtern.h
Externs/ParticleUniversePhysXFluidExtern.h
Externs/ParticleUniversePhysXLogging.h
Externs/ParticleUniversePhysxMath.h
