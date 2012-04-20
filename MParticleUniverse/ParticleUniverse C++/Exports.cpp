//Added By: Tyler Grusendorf
//Wrapper Exports.
#include "ParticleUniversePCH.h"
#include "Ogre.h"
#include "ParticleAffectors/ParticleUniverseAlignAffector.h"
#include "ParticleAffectors/ParticleUniverseBoxCollider.h"
#include "ParticleAffectors/ParticleUniverseCollisionAvoidanceAffector.h"
#include "ParticleAffectors/ParticleUniverseColourAffector.h"
#include "ParticleAffectors/ParticleUniverseFlockCenteringAffector.h"
#include "ParticleAffectors/ParticleUniverseForceFieldAffector.h"
#include "ParticleAffectors/ParticleUniverseGeometryRotator.h"
#include "ParticleAffectors/ParticleUniverseGravityAffector.h"
#include "ParticleAffectors/ParticleUniverseInterParticleCollider.h"
#include "ParticleAffectors/ParticleUniverseJetAffector.h"
#include "ParticleAffectors/ParticleUniverseLineAffector.h"
#include "ParticleAffectors/ParticleUniverseLinearForceAffector.h"
#include "ParticleAffectors/ParticleUniverseParticleFollower.h"
#include "ParticleAffectors/ParticleUniversePathFollower.h"
#include "ParticleAffectors/ParticleUniversePlaneCollider.h"
#include "ParticleAffectors/ParticleUniverseRandomiser.h"
#include "ParticleAffectors/ParticleUniverseScaleAffector.h"
#include "ParticleAffectors/ParticleUniverseScaleVelocityAffector.h"
#include "ParticleAffectors/ParticleUniverseSineForceAffector.h"
#include "ParticleAffectors/ParticleUniverseSphereCollider.h"
#include "ParticleAffectors/ParticleUniverseTextureAnimator.h"
#include "ParticleAffectors/ParticleUniverseTextureRotator.h"
#include "ParticleAffectors/ParticleUniverseVelocityMatchingAffector.h"
#include "ParticleAffectors/ParticleUniverseVortexAffector.h"

#include "ParticleBehaviours/ParticleUniverseSlaveBehaviour.h"

#include "ParticleRenderers/ParticleUniverseBeamRenderer.h"
#include "ParticleRenderers/ParticleUniverseBillboard.h"
#include "ParticleRenderers/ParticleUniverseBillboardRenderer.h"
#include "ParticleRenderers/ParticleUniverseBoxRenderer.h"
#include "ParticleRenderers/ParticleUniverseEntityRenderer.h"
#include "ParticleRenderers/ParticleUniverseLightRenderer.h"
#include "ParticleRenderers/ParticleUniverseRibbonTrailRenderer.h"
#include "ParticleRenderers/ParticleUniverseSphereRenderer.h"


#include "Externs/ParticleUniverseBoxColliderExtern.h"
#include "Externs/ParticleUniverseGravityExtern.h"
#include "Externs/ParticleUniverseSceneDecoratorExtern.h"
#include "Externs/ParticleUniverseSphereColliderExtern.h"
#include "Externs/ParticleUniverseVortexExtern.h"


#include "ParticleEmitters/ParticleUniverseBoxEmitter.h"
#include "ParticleEmitters/ParticleUniverseCircleEmitter.h"
#include "ParticleEmitters/ParticleUniverseLineEmitter.h"
#include "ParticleEmitters/ParticleUniverseMeshSurfaceEmitter.h"
#include "ParticleEmitters/ParticleUniversePointEmitter.h"
#include "ParticleEmitters/ParticleUniversePositionEmitter.h"
#include "ParticleEmitters/ParticleUniverseSlaveEmitter.h"
#include "ParticleEmitters/ParticleUniverseSphereSurfaceEmitter.h"
#include "ParticleEmitters/ParticleUniverseVertexEmitter.h"

#include "ParticleObservers/ParticleUniverseOnClearObserver.h"
#include "ParticleObservers/ParticleUniverseOnCollisionObserver.h"
#include "ParticleObservers/ParticleUniverseOnCountObserver.h"
#include "ParticleObservers/ParticleUniverseOnEmissionObserver.h"
#include "ParticleObservers/ParticleUniverseOnEventFlagObserver.h"
#include "ParticleObservers/ParticleUniverseOnExpireObserver.h"
#include "ParticleObservers/ParticleUniverseOnPositionObserver.h"
#include "ParticleObservers/ParticleUniverseOnQuotaObserver.h"
#include "ParticleObservers/ParticleUniverseOnRandomObserver.h"
#include "ParticleObservers/ParticleUniverseOnTimeObserver.h"
#include "ParticleObservers/ParticleUniverseOnVelocityObserver.h"


#include "ParticleEventHandlers/ParticleUniverseDoAffectorEventHandler.h"
#include "ParticleEventHandlers/ParticleUniverseDoEnableComponentEventHandler.h"
#include "ParticleEventHandlers/ParticleUniverseDoExpireEventHandler.h"
#include "ParticleEventHandlers/ParticleUniverseDoFreezeEventHandler.h"
#include "ParticleEventHandlers/ParticleUniverseDoPlacementParticleEventHandler.h"
#include "ParticleEventHandlers/ParticleUniverseDoScaleEventHandler.h"
#include "ParticleEventHandlers/ParticleUniverseDoStopSystemEventHandler.h"


//Used for making char** from vector<String>
//{
#include <string>
#include <vector>
#include <algorithm>
#include <functional>
#include <iterator>
#include <sstream>
#include <typeinfo.h>
//}

//EXPORT ParticleUniverse::ParticleSystem* ParticleSystemManager_New(ParticleUniverse::ParticleSystemManager* ptr, char* name,
//		char* templateName,
//		Ogre::SceneManager* sceneManager)
//{
//	return ptr->createParticleSystem(name, templateName, sceneManager);
//}

EXPORT const char* ParticleUniverse_GetObjectName(void* objectToCheck)
{
//	return ParticleUniverse_GetObjectName2(&objectToCheck);
//}
//const char* ParticleUniverse_GetObjectName2(ParticleUniverse::Any& objectToCheck)
//{
	//if(objectToCheck->getType() == ParticleUniverse::ParticleTechnique) {
	//	return "ParticleTechnique";
		//Box* box = static_cast<Box*>(old);
		// Do something box specific
	//}
	//return typeid(objectToCheck).name();
	//try {
	//ParticleUniverse::ParticleSystem *ps = static_cast<ParticleUniverse::ParticleSystem*>(objectToCheck);
	//if (objectToCheck is ParticleUniverse::ParticleSystem)
	//	return "ParticleSystem";
	//}catch(std::bad_cast) {}
	//try {
	//ParticleUniverse::ParticleTechnique *pt = static_cast<ParticleUniverse::ParticleTechnique*>(objectToCheck);
	////if (pt)
	//	return "ParticleTechnique";
	//}catch(std::bad_cast) {}
	//try {
	//ParticleUniverse::BoxEmitter *be = static_cast<ParticleUniverse::BoxEmitter*>(objectToCheck);
	////if (be)
	//	return "BoxEmitter";
	//}catch(std::bad_cast) {}
	//try {
	//ParticleUniverse::Any *any = static_cast<ParticleUniverse::Any*>(objectToCheck);
	////if (any)
	//	return "Any";
	//}catch(std::bad_cast) {}
	//
	return "null";

	//return typeid( *objectToCheck ).name();
	//std::type_info objInfo = objectToCheck->getType();
	//return objInfo.raw_name();
}

#pragma region ParticleTechnique Exports
EXPORT ParticleUniverse::ParticleTechnique* ParticleTechnique_New()
{
	return new ParticleUniverse::ParticleTechnique();
}
EXPORT void ParticleTechnique_Destroy(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->~ParticleTechnique();
}

EXPORT ParticleUniverse::ParticleSystem* ParticleTechnique_GetParentSystem(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getParentSystem();
}
EXPORT void ParticleTechnique_SetParentSystem(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleSystem* parentSystem)
{
	ptr->setParentSystem(parentSystem);
}
EXPORT const char* ParticleTechnique_GetName(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getName().c_str();
}
EXPORT void ParticleTechnique_SetName(ParticleUniverse::ParticleTechnique* ptr, char* name)
{
	ptr->setName(name);
}
EXPORT unsigned int ParticleTechnique_GetVisualParticleQuota(ParticleUniverse::ParticleTechnique* ptr) 
{
	return ptr->getVisualParticleQuota();
}
EXPORT void ParticleTechnique_SetVisualParticleQuota(ParticleUniverse::ParticleTechnique* ptr, unsigned int quota)
{
	ptr->setVisualParticleQuota(quota);
}
EXPORT unsigned int ParticleTechnique_GetEmittedEmitterQuota(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getEmittedEmitterQuota();
}
EXPORT void ParticleTechnique_SetEmittedEmitterQuota(ParticleUniverse::ParticleTechnique* ptr, unsigned int quota)
{
	ptr->setEmittedEmitterQuota(quota);
}
EXPORT unsigned int ParticleTechnique_GetEmittedTechniqueQuota(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getEmittedTechniqueQuota();
}
EXPORT void ParticleTechnique_SetEmittedTechniqueQuota(ParticleUniverse::ParticleTechnique* ptr, unsigned int quota)
{
	ptr->setEmittedTechniqueQuota(quota);
}
EXPORT unsigned int ParticleTechnique_GetEmittedAffectorQuota(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getEmittedAffectorQuota();
}
EXPORT void ParticleTechnique_SetEmittedAffectorQuota(ParticleUniverse::ParticleTechnique* ptr, unsigned int quota)
{
	ptr->setEmittedAffectorQuota(quota);
}
EXPORT unsigned int ParticleTechnique_GetEmittedSystemQuota(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getEmittedSystemQuota();
}
EXPORT void ParticleTechnique_SetEmittedSystemQuota(ParticleUniverse::ParticleTechnique* ptr, unsigned int quota)
{
	ptr->setEmittedSystemQuota(quota);
}
EXPORT const float ParticleTechnique_GetDefaultWidth(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getDefaultWidth();
}
EXPORT void ParticleTechnique_SetDefaultWidth(ParticleUniverse::ParticleTechnique* ptr, const float width)
{
	ptr->setDefaultWidth(width);
}
EXPORT const float ParticleTechnique_GetDefaultHeight(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getDefaultHeight();
}
EXPORT void ParticleTechnique_SetDefaultHeight(ParticleUniverse::ParticleTechnique* ptr, const float height)
{
	ptr->setDefaultHeight(height);
}
EXPORT const float ParticleTechnique_GetDefaultDepth(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getDefaultDepth();
}
EXPORT void ParticleTechnique_SetDefaultDepth(ParticleUniverse::ParticleTechnique* ptr, const float depth)
{
	ptr->setDefaultDepth(depth);
}
EXPORT const Ogre::Vector3* ParticleTechnique_GetDerivedPosition(ParticleUniverse::ParticleTechnique* ptr)
{
	return &ptr->getDerivedPosition();
}
EXPORT float ParticleTechnique_GetCameraSquareDistance(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getCameraSquareDistance();
}
EXPORT void ParticleTechnique_SetCameraSquareDistance(ParticleUniverse::ParticleTechnique* ptr, float cameraSquareDistance)
{
	ptr->setCameraSquareDistance(cameraSquareDistance);
}
EXPORT void ParticleTechnique_SuppressNotifyEmissionChange(ParticleUniverse::ParticleTechnique* ptr, bool suppress)
{
	ptr->suppressNotifyEmissionChange(suppress);
}
EXPORT const char* ParticleTechnique_GetMaterialName(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getMaterialName().c_str();
}
EXPORT const Ogre::MaterialPtr* ParticleTechnique_GetMaterial(ParticleUniverse::ParticleTechnique* ptr)
{
	return &ptr->getMaterial();
}
EXPORT void ParticleTechnique_SetMaterialName(ParticleUniverse::ParticleTechnique* ptr, char* materialName)
{
	ptr->setMaterialName(materialName);
}
EXPORT ParticleUniverse::ParticleEmitter* ParticleTechnique_CreateEmitter (ParticleUniverse::ParticleTechnique* ptr, char* emitterType)
{
	return ptr->createEmitter(emitterType);
}
EXPORT void ParticleTechnique_AddEmitter (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->addEmitter(emitter);
}
EXPORT void ParticleTechnique_RemoveEmitter(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->removeEmitter(emitter);
}
EXPORT ParticleUniverse::ParticleEmitter* ParticleTechnique_GetEmitter (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	return ptr->getEmitter(index);
}
EXPORT ParticleUniverse::ParticleEmitter* ParticleTechnique_GetEmitter2 (ParticleUniverse::ParticleTechnique* ptr, char* emitterName)
{
	return ptr->getEmitter(emitterName);
}
EXPORT unsigned int ParticleTechnique_GetNumEmitters (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumEmitters();
}
EXPORT void ParticleTechnique_DestroyEmitter (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	ptr->destroyEmitter(index);
}
EXPORT void ParticleTechnique_DestroyEmitter2 (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->destroyEmitter(emitter);
}
EXPORT void ParticleTechnique_DestroyAllEmitters (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->destroyAllEmitters();
}
EXPORT unsigned int ParticleTechnique_GetNumEmittedEmitters (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumEmittedEmitters();
}
EXPORT ParticleUniverse::ParticleAffector* ParticleTechnique_CreateAffector (ParticleUniverse::ParticleTechnique* ptr, char* affectorType)
{
	return ptr->createAffector(affectorType);
}
EXPORT void ParticleTechnique_AddAffector (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->addAffector(affector);
}
EXPORT void ParticleTechnique_RemoveAffector (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->removeAffector(affector);
}
EXPORT ParticleUniverse::ParticleAffector* ParticleTechnique_GetAffector (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	return ptr->getAffector(index);
}
EXPORT ParticleUniverse::ParticleAffector* ParticleTechnique_GetAffector2 (ParticleUniverse::ParticleTechnique* ptr, char* affectorName)
{
	return ptr->getAffector(affectorName);
}
EXPORT unsigned int ParticleTechnique_GetNumAffectors (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumAffectors();
}
EXPORT void ParticleTechnique_DestroyAffector (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	ptr->destroyAffector(index);
}
EXPORT void ParticleTechnique_DestroyAffector2 (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->destroyAffector(affector);
}
EXPORT void ParticleTechnique_DestroyAllAffectors (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->destroyAllAffectors();
}
EXPORT unsigned int ParticleTechnique_GetNumEmittedAffectors (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumEmittedAffectors();
}
EXPORT ParticleUniverse::ParticleObserver* ParticleTechnique_CreateObserver (ParticleUniverse::ParticleTechnique* ptr, char* observerType)
{
	return ptr->createObserver(observerType);
}
EXPORT void ParticleTechnique_AddObserver (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->addObserver(observer);
}
EXPORT void ParticleTechnique_RemoveObserver(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->removeObserver(observer);
}
EXPORT ParticleUniverse::ParticleObserver* ParticleTechnique_GetObserver (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	return ptr->getObserver(index);
}
EXPORT ParticleUniverse::ParticleObserver* ParticleTechnique_GetObserver2 (ParticleUniverse::ParticleTechnique* ptr, char* observerName)
{
	return ptr->getObserver(observerName);
}
EXPORT unsigned int ParticleTechnique_GetNumObservers (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumObservers();
}
EXPORT void ParticleTechnique_DestroyObserver (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	ptr->destroyObserver(index);
}
EXPORT void ParticleTechnique_DestroyObserver2 (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->destroyObserver(observer);
}
EXPORT void ParticleTechnique_DestroyAllObservers (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->destroyAllObservers();
}
EXPORT ParticleUniverse::ParticleRenderer* ParticleTechnique_GetRenderer(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getRenderer();
}
EXPORT void ParticleTechnique_SetRenderer(ParticleUniverse::ParticleTechnique* ptr, char* rendererType)
{
	ptr->setRenderer(rendererType);
}
EXPORT void ParticleTechnique_RemoveRenderer(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->removeRenderer(renderer);
}
EXPORT void ParticleTechnique_SetRenderer2(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->setRenderer(renderer);
}
EXPORT void ParticleTechnique_DestroyRenderer(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->destroyRenderer();
}
EXPORT void ParticleTechnique__addBehaviourTemplate (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleBehaviour* behaviourTemplate)
{
	ptr->_addBehaviourTemplate(behaviourTemplate);
}
EXPORT void ParticleTechnique__removeBehaviourTemplate(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleBehaviour* behaviourTemplate)
{
	ptr->_removeBehaviourTemplate(behaviourTemplate);
}
EXPORT ParticleUniverse::ParticleBehaviour* ParticleTechnique__getBehaviourTemplate (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	return ptr->_getBehaviourTemplate(index);
}
EXPORT ParticleUniverse::ParticleBehaviour* ParticleTechnique__getBehaviourTemplate2 (ParticleUniverse::ParticleTechnique* ptr, char* behaviourType)
{
	return ptr->_getBehaviourTemplate(behaviourType);
}
EXPORT unsigned int ParticleTechnique__getNumBehaviourTemplates (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->_getNumBehaviourTemplates();
}
EXPORT void ParticleTechnique__destroyBehaviourTemplate (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleBehaviour* behaviourTemplate)
{
	ptr->_destroyBehaviourTemplate(behaviourTemplate);
}
EXPORT void ParticleTechnique__destroyAllBehaviourTemplates (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_destroyAllBehaviourTemplates();
}
EXPORT ParticleUniverse::Extern* ParticleTechnique_CreateExtern (ParticleUniverse::ParticleTechnique* ptr, char* externType)
{
	return ptr->createExtern(externType);
}
EXPORT void ParticleTechnique_AddExtern (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->addExtern(externObject);
}
EXPORT void ParticleTechnique_RemoveExtern (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->removeExtern(externObject);
}
EXPORT ParticleUniverse::Extern* ParticleTechnique_GetExtern (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	return ptr->getExtern(index);
}
EXPORT ParticleUniverse::Extern* ParticleTechnique_GetExtern2 (ParticleUniverse::ParticleTechnique* ptr, char* externName)
{
	return ptr->getExtern(externName);
}
EXPORT ParticleUniverse::Extern* ParticleTechnique_GetExternType (ParticleUniverse::ParticleTechnique* ptr, char* externType)
{
	return ptr->getExternType(externType);
}
EXPORT unsigned int ParticleTechnique_GetNumExterns (ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumExterns();
}
EXPORT void ParticleTechnique_DestroyExtern (ParticleUniverse::ParticleTechnique* ptr, unsigned int index)
{
	ptr->destroyExtern(index);
}
EXPORT void ParticleTechnique_DestroyExtern2 (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->destroyExtern(externObject);
}
EXPORT void ParticleTechnique_DestroyAllExterns (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->destroyAllExterns();
}
EXPORT void ParticleTechnique__updateRenderQueue(ParticleUniverse::ParticleTechnique* ptr, Ogre::RenderQueue* queue)
{
	ptr->_updateRenderQueue(queue);
}
EXPORT void ParticleTechnique_SetRenderQueueGroup(ParticleUniverse::ParticleTechnique* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT void ParticleTechnique__prepare(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepare();
}
EXPORT void ParticleTechnique__prepareSystem(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareSystem();
}
EXPORT void ParticleTechnique__unprepareSystem(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareSystem();
}
EXPORT void ParticleTechnique__prepareTechnique(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareTechnique();
}
EXPORT void ParticleTechnique__unprepareTechnique(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareTechnique();
}
EXPORT void ParticleTechnique__prepareVisualParticles(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareVisualParticles();
}
EXPORT void ParticleTechnique__unprepareVisualParticles(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareVisualParticles();
}
EXPORT void ParticleTechnique__prepareRenderer(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareRenderer();
}
EXPORT void ParticleTechnique__unprepareRenderer(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareRenderer();
}
EXPORT void ParticleTechnique__prepareEmitters(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareEmitters();
}
EXPORT void ParticleTechnique__unprepareEmitters(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareEmitters();
}
EXPORT void ParticleTechnique__prepareAffectors(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareAffectors();
}
EXPORT void ParticleTechnique__unprepareAffectors(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareAffectors();
}
EXPORT void ParticleTechnique__prepareBehaviours(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareBehaviours();
}
EXPORT void ParticleTechnique__unprepareBehaviours(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareBehaviours();
}
EXPORT void ParticleTechnique__prepareExterns(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_prepareExterns();
}
EXPORT void ParticleTechnique__unprepareExterns(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_unprepareExterns();
}
EXPORT void ParticleTechnique__update(ParticleUniverse::ParticleTechnique* ptr, float timeElapsed)
{
	ptr->_update(timeElapsed);
}
EXPORT void ParticleTechnique__notifyEmissionChange(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyEmissionChange();
}
EXPORT void ParticleTechnique__notifyAttached(ParticleUniverse::ParticleTechnique* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void ParticleTechnique__notifyAttachedPooledTechniques(ParticleUniverse::ParticleTechnique* ptr, Ogre::Node* parent, bool isTagPoint)
{
	ptr->_notifyAttachedPooledTechniques(parent, isTagPoint);
}
EXPORT void ParticleTechnique__notifyCurrentCamera(ParticleUniverse::ParticleTechnique* ptr, Ogre::Camera* camera)
{
	ptr->_notifyCurrentCamera(camera);
}
EXPORT void ParticleTechnique__notifyCurrentCameraPooledTechniques(ParticleUniverse::ParticleTechnique* ptr, Ogre::Camera* camera)
{
	ptr->_notifyCurrentCameraPooledTechniques(camera);
}
EXPORT void ParticleTechnique__notifyParticleResized(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void ParticleTechnique__notifyStart (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyStart();
}
EXPORT void ParticleTechnique__notifyStop (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyStop();
}
EXPORT void ParticleTechnique__notifyPause (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyPause();
}
EXPORT void ParticleTechnique__notifyResume (ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyResume();
}
EXPORT bool ParticleTechnique__isExpired(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_isExpired(particle, timeElapsed);
}
EXPORT void ParticleTechnique_ForceEmission(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleEmitter* emitter, unsigned requested)
{
	ptr->forceEmission(emitter, requested);
}
EXPORT void ParticleTechnique_ForceEmission2(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Particle::ParticleType* particleType, unsigned requested)
{
	ptr->forceEmission(*particleType, requested);
}
EXPORT void ParticleTechnique_CopyAttributesTo (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->copyAttributesTo(technique);
}
EXPORT unsigned short ParticleTechnique_GetLodIndex(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getLodIndex();
}
EXPORT void ParticleTechnique_SetLodIndex(ParticleUniverse::ParticleTechnique* ptr, unsigned short lodIndex)
{
	ptr->setLodIndex(lodIndex);
}
EXPORT void ParticleTechnique__markForEmission(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_markForEmission();
}
EXPORT void ParticleTechnique__markForEmission2(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleEmitter* emitter, bool mark = true)
{
	ptr->_markForEmission(emitter, mark);
}
EXPORT void ParticleTechnique__notifyUpdateBounds(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_notifyUpdateBounds();
}
EXPORT void ParticleTechnique__resetBounds(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_resetBounds();
}
EXPORT void ParticleTechnique__notifyRescaled(ParticleUniverse::ParticleTechnique* ptr, const Ogre::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT void ParticleTechnique__notifyVelocityRescaled(ParticleUniverse::ParticleTechnique* ptr, float scaleVelocity)
{
	ptr->_notifyVelocityRescaled(scaleVelocity);
}
EXPORT const Ogre::AxisAlignedBox* ParticleTechnique_GetWorldBoundingBox(ParticleUniverse::ParticleTechnique* ptr)
{
	return &ptr->getWorldBoundingBox();
}
EXPORT void ParticleTechnique__sortVisualParticles(ParticleUniverse::ParticleTechnique* ptr, Ogre::Camera* camera)
{
	ptr->_sortVisualParticles(camera);
}
EXPORT void ParticleTechnique_SetWidthCameraDependency(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::CameraDependency* cameraDependency)
{
	ptr->setWidthCameraDependency(cameraDependency);
}
EXPORT void ParticleTechnique_SetWidthCameraDependency2(ParticleUniverse::ParticleTechnique* ptr, float squareDistance, bool inc)
{
	ptr->setWidthCameraDependency(squareDistance, inc);
}
EXPORT ParticleUniverse::CameraDependency* ParticleTechnique_GetWidthCameraDependency(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getWidthCameraDependency();
}
EXPORT void ParticleTechnique_SetHeightCameraDependency(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::CameraDependency* cameraDependncy)
{
	ptr->setHeightCameraDependency(cameraDependncy);
}
EXPORT void ParticleTechnique_SetHeightCameraDependency2(ParticleUniverse::ParticleTechnique* ptr, float squareDistance, bool inc)
{
	ptr->setHeightCameraDependency(squareDistance, inc);
}
EXPORT ParticleUniverse::CameraDependency* ParticleTechnique_GetHeightCameraDependency(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getHeightCameraDependency();
}
EXPORT void ParticleTechnique_SetDepthCameraDependency(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::CameraDependency* cameraDependency)
{
	ptr->setDepthCameraDependency(cameraDependency);
}
EXPORT void ParticleTechnique_SetDepthCameraDependency2(ParticleUniverse::ParticleTechnique* ptr, float squareDistance, bool inc)
{
	ptr->setDepthCameraDependency(squareDistance, inc);
}
EXPORT ParticleUniverse::CameraDependency* ParticleTechnique_GetDepthCameraDependency(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getDepthCameraDependency();
}
EXPORT unsigned int ParticleTechnique_GetNumberOfEmittedParticles(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getNumberOfEmittedParticles();
}
EXPORT unsigned int ParticleTechnique_GetNumberOfEmittedParticles2(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Particle::ParticleType* particleType)
{
	return ptr->getNumberOfEmittedParticles(*particleType);
}
EXPORT void ParticleTechnique__initAllParticlesForExpiration(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->_initAllParticlesForExpiration();
}
EXPORT void ParticleTechnique_LockAllParticles(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->lockAllParticles();
}
EXPORT void ParticleTechnique_InitVisualDataInPool(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->initVisualDataInPool();
}
EXPORT ParticleUniverse::ParticlePool* ParticleTechnique__getParticlePool(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->_getParticlePool();
}
EXPORT bool ParticleTechnique_IsKeepLocal(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->isKeepLocal();
}
EXPORT void ParticleTechnique_SetKeepLocal(ParticleUniverse::ParticleTechnique* ptr, bool keepLocal)
{
	ptr->setKeepLocal(keepLocal);
}
EXPORT bool ParticleTechnique_MakeParticleLocal(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::Particle* particle)
{
	return ptr->makeParticleLocal(particle);
}
EXPORT ParticleUniverse::SpatialHashTable<ParticleUniverse::Particle*>* ParticleTechnique_GetSpatialHashTable(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getSpatialHashTable();
}
EXPORT bool ParticleTechnique_IsSpatialHashingUsed(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->isSpatialHashingUsed();
}
EXPORT void ParticleTechnique_SetSpatialHashingUsed(ParticleUniverse::ParticleTechnique* ptr, bool spatialHashingUsed)
{
	ptr->setSpatialHashingUsed(spatialHashingUsed);
}
EXPORT unsigned short ParticleTechnique_GetSpatialHashingCellDimension(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getSpatialHashingCellDimension();
}
EXPORT void ParticleTechnique_SetSpatialHashingCellDimension(ParticleUniverse::ParticleTechnique* ptr, unsigned short spatialHashingCellDimension)		
{
	ptr->setSpatialHashingCellDimension(spatialHashingCellDimension);
}
EXPORT unsigned short ParticleTechnique_GetSpatialHashingCellOverlap(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getSpatialHashingCellOverlap();
}
EXPORT void ParticleTechnique_SetSpatialHashingCellOverlap(ParticleUniverse::ParticleTechnique* ptr, unsigned short spatialHashingCellOverlap)
{
	ptr->setSpatialHashingCellOverlap(spatialHashingCellOverlap);
}
EXPORT unsigned int ParticleTechnique_GetSpatialHashTableSize(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getSpatialHashTableSize();
}
EXPORT void ParticleTechnique_SetSpatialHashTableSize(ParticleUniverse::ParticleTechnique* ptr, unsigned int spatialHashTableSize)
{
	ptr->setSpatialHashTableSize(spatialHashTableSize);
}
EXPORT float ParticleTechnique_GetSpatialHashingInterval(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getSpatialHashingInterval();
}
EXPORT void ParticleTechnique_SetSpatialHashingInterval(ParticleUniverse::ParticleTechnique* ptr, float spatialHashingInterval)
{
	ptr->setSpatialHashingInterval(spatialHashingInterval);
}
//EXPORT bool ParticleTechnique_IsSpatialHashingParticleSizeUsed(ParticleUniverse::ParticleTechnique* ptr)
//{
//	return ptr->isSpatialHashingParticleSizeUsed();
//}
//EXPORT void ParticleTechnique_SetSpatialHashingParticleSizeUsed(ParticleUniverse::ParticleTechnique* ptr, bool spatialHashingParticleSizeUsed)
//{
//	ptr->setSpatialHashingParticleSizeUsed(spatialHashingParticleSizeUsed);
//}
EXPORT float ParticleTechnique_GetMaxVelocity(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getMaxVelocity();
}
EXPORT void ParticleTechnique_SetMaxVelocity(ParticleUniverse::ParticleTechnique* ptr, float maxVelocity)
{
	ptr->setMaxVelocity(maxVelocity);
}
EXPORT void ParticleTechnique_AddTechniqueListener (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::TechniqueListener* techniqueListener)
{
	ptr->addTechniqueListener(techniqueListener);
}
EXPORT void ParticleTechnique_RemoveTechniqueListener (ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::TechniqueListener* techniqueListener)
{
	ptr->removeTechniqueListener(techniqueListener);
}
EXPORT void ParticleTechnique_LogDebug(ParticleUniverse::ParticleTechnique* ptr)
{
	ptr->logDebug();
}
EXPORT float ParticleTechnique_GetParticleSystemScaleVelocity(ParticleUniverse::ParticleTechnique* ptr)
{
	return ptr->getParticleSystemScaleVelocity();
}
EXPORT void ParticleTechnique_PushEvent(ParticleUniverse::ParticleTechnique* ptr, ParticleUniverse::ParticleUniverseEvent* particleUniverseEvent)
{
	ptr->pushEvent(*particleUniverseEvent);
}

#pragma endregion

#pragma region SphereSet PrimitiveShapeSet Exports
	
EXPORT void SphereSet_SetZRotated(ParticleUniverse::SphereSet* ptr, bool zRotated)
{
	ptr->setZRotated(zRotated);
}
EXPORT bool SphereSet_IsZRotated(ParticleUniverse::SphereSet* ptr)
{
	return ptr->isZRotated();
}
EXPORT void SphereSet__notifyZRotated(ParticleUniverse::SphereSet* ptr)
{
	ptr->_notifyZRotated();
}
EXPORT void SphereSet_SetMaterialName(ParticleUniverse::SphereSet* ptr, char* name)
{
	ptr->setMaterialName(name);
}
EXPORT const char* SphereSet_GetMaterialName(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getMaterialName().c_str();
}
EXPORT void SphereSet__notifyResized(ParticleUniverse::SphereSet* ptr)
{
	ptr->_notifyResized();
}
EXPORT void SphereSet__notifyCurrentCamera(ParticleUniverse::SphereSet* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT const Ogre::AxisAlignedBox* SphereSet_GetBoundingBox(ParticleUniverse::SphereSet* ptr)
{
	return &ptr->getBoundingBox();
}
EXPORT float SphereSet_GetBoundingRadius(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getBoundingRadius();
}
EXPORT const Ogre::MaterialPtr* SphereSet_GetMaterial(ParticleUniverse::SphereSet* ptr)
{
	return &ptr->getMaterial();
}
EXPORT void SphereSet_GetWorldTransforms(ParticleUniverse::SphereSet* ptr, Ogre::Matrix4* xform)
{
	ptr->getWorldTransforms(xform);
}
EXPORT const Ogre::Quaternion* SphereSet_GetWorldOrientation(ParticleUniverse::SphereSet* ptr)
{
	return &ptr->getWorldOrientation();
}
EXPORT const Ogre::Vector3* SphereSet_GetWorldPosition(ParticleUniverse::SphereSet* ptr)
{
	return &ptr->getWorldPosition();
}
EXPORT bool SphereSet_IsCullIndividually(ParticleUniverse::SphereSet* ptr)
{
	return ptr->isCullIndividually();
}
EXPORT void SphereSet_SetCullIndividually(ParticleUniverse::SphereSet* ptr, bool cullIndividual)
{
	ptr->setCullIndividually(cullIndividual);
}
EXPORT float SphereSet_GetSquaredViewDepth(ParticleUniverse::SphereSet* ptr, const Ogre::Camera* cam)
{
	return ptr->getSquaredViewDepth(cam);
}
EXPORT const Ogre::LightList* SphereSet_GetLights(ParticleUniverse::SphereSet* ptr)
{
	return &ptr->getLights();
}
EXPORT unsigned int SphereSet_GetTypeFlags(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getTypeFlags();
}
EXPORT void SphereSet_RotateTexture(ParticleUniverse::SphereSet* ptr, float speed)
{
	ptr->rotateTexture(speed);
}
EXPORT void SphereSet_VisitRenderables(ParticleUniverse::SphereSet* ptr, Ogre::Renderable::Visitor* visitor, bool debugRenderables = false)
{
	ptr->visitRenderables(visitor, debugRenderables);
}

#pragma endregion
#pragma region SphereSet Renderable Exports
//Renderable Implementeation
EXPORT bool SphereSet_GetCastShadows(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getCastsShadows();
}

EXPORT unsigned short SphereSet_GetNumWorldTransforms(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getNumWorldTransforms();
}
EXPORT bool SphereSet_GetPolygonModeOverrideable(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getPolygonModeOverrideable();
}
EXPORT void SphereSet_SetPolygonModeOverrideable(ParticleUniverse::SphereSet* ptr, bool overrideable)
{
	ptr->setPolygonModeOverrideable(overrideable);
}
EXPORT Ogre::Technique* SphereSet_GetTechnique(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getTechnique();
}
EXPORT void SphereSet__updateCustomGpuParameter(ParticleUniverse::SphereSet* ptr, 
            const Ogre::GpuProgramParameters::AutoConstantEntry* constantEntry,
            Ogre::GpuProgramParameters* params)
{
	ptr->_updateCustomGpuParameter(*constantEntry, params);
}
EXPORT void SphereSet_GetRenderOperation(ParticleUniverse::SphereSet* ptr, Ogre::RenderOperation* op)
{
	return ptr->getRenderOperation(*op);
}
EXPORT bool SphereSet_PreRender(ParticleUniverse::SphereSet* ptr, Ogre::SceneManager* sm, Ogre::RenderSystem* rsys)
{
	return ptr->preRender(sm, rsys);
}
EXPORT void SphereSet_PostRender(ParticleUniverse::SphereSet* ptr, Ogre::SceneManager* sm, Ogre::RenderSystem* rsys)
{
	ptr->postRender(sm, rsys);
}
//End Renderable Implementation
#pragma endregion
#pragma region SphereSet Exports
EXPORT ParticleUniverse::SphereSet* SphereSet_New(const char* name, unsigned int poolSize = 20, bool externalData = false)
{
	return new ParticleUniverse::SphereSet(name, poolSize, externalData);
}
EXPORT ParticleUniverse::SphereSet* SphereSet_New2(void)
{
	return new ParticleUniverse::SphereSet();
}
EXPORT void SphereSet_Destroy(ParticleUniverse::SphereSet* ptr)
{
	ptr->~SphereSet();
}
EXPORT ParticleUniverse::Sphere* SphereSet_CreateSphere(ParticleUniverse::SphereSet* ptr, const Ogre::Vector3* position)
{
	return ptr->createSphere(*position);
}
EXPORT ParticleUniverse::Sphere* SphereSet_CreateSphere2(ParticleUniverse::SphereSet* ptr, float x, float y, float z)
{
	return ptr->createSphere(x, y, z);
}
EXPORT unsigned int SphereSet_GetNumSpheres(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getNumSpheres();
}
EXPORT void SphereSet_SetAutoextend(ParticleUniverse::SphereSet* ptr, bool autoextend)
{
	ptr->setAutoextend(autoextend);
}
EXPORT bool SphereSet_IsAutoextend(ParticleUniverse::SphereSet* ptr)
{
	return ptr->isAutoextend();
}
EXPORT void SphereSet_SetPoolSize(ParticleUniverse::SphereSet* ptr, unsigned int size)
{
	ptr->setPoolSize(size);
}
EXPORT unsigned int SphereSet_GetPoolSize(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getPoolSize();
}
EXPORT void SphereSet_Clear(ParticleUniverse::SphereSet* ptr)
{
	ptr->clear();
}
EXPORT ParticleUniverse::Sphere* SphereSet_GetSphere(ParticleUniverse::SphereSet* ptr, unsigned int index)
{
	return ptr->getSphere(index);
}
EXPORT void SphereSet_RemoveSphere(ParticleUniverse::SphereSet* ptr, unsigned int index)
{
	ptr->removeSphere(index);
}
EXPORT void SphereSet_RemoveSphere2(ParticleUniverse::SphereSet* ptr, ParticleUniverse::Sphere* sphere)
{
	ptr->removeSphere(sphere);
}
EXPORT void SphereSet_SetDefaultRadius(ParticleUniverse::SphereSet* ptr, float radius)
{
	ptr->setDefaultRadius(radius);
}
EXPORT float SphereSet_GetDefaultRadius(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getDefaultRadius();
}
EXPORT void SphereSet_SetNumberOfRings(ParticleUniverse::SphereSet* ptr, unsigned int numberOfRings)
{
	ptr->setNumberOfRings(numberOfRings);
}
EXPORT unsigned int SphereSet_GetNumberOfRings(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getNumberOfRings();
}
EXPORT void SphereSet_SetNumberOfSegments(ParticleUniverse::SphereSet* ptr, unsigned int numberOfSegments)
{
	ptr->setNumberOfSegments(numberOfSegments);
}
EXPORT unsigned int SphereSet_GetNumberOfSegments(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getNumberOfSegments();
}
EXPORT void SphereSet_BeginSpheres(ParticleUniverse::SphereSet* ptr, unsigned int numSpheres = 0)
{
	ptr->beginSpheres(numSpheres);
}
EXPORT void SphereSet_InjectSphere(ParticleUniverse::SphereSet* ptr, ParticleUniverse::Sphere* sphere)
{
	ptr->injectSphere(*sphere);
}
EXPORT void SphereSet_EndSpheres(ParticleUniverse::SphereSet* ptr)
{
	ptr->endSpheres();
}
EXPORT void SphereSet_SetBounds(ParticleUniverse::SphereSet* ptr, const Ogre::AxisAlignedBox* box, float radius)
{
	ptr->setBounds(*box, radius);
}
EXPORT void SphereSet__updateRenderQueue(ParticleUniverse::SphereSet* ptr, Ogre::RenderQueue* queue)
{
	ptr->_updateRenderQueue(queue);
}
EXPORT const char* SphereSet_GetMovableType(ParticleUniverse::SphereSet* ptr)
{
	return ptr->getMovableType().c_str();
}
EXPORT void SphereSet__updateBounds(ParticleUniverse::SphereSet* ptr)
{
	ptr->_updateBounds();
}
EXPORT void SphereSet_SetSpheresInWorldSpace(ParticleUniverse::SphereSet* ptr, bool ws)
{
	ptr->setSpheresInWorldSpace(ws);
}

#pragma endregion
#pragma region Sphere Exports
EXPORT Ogre::Vector3* Sphere_GetmPosition(ParticleUniverse::Sphere* ptr)
{
	return &ptr->mPosition;
}
EXPORT void Sphere_SetmPosition(ParticleUniverse::Sphere* ptr, Ogre::Vector3* newVal)
{
	ptr->mPosition = *newVal;
}
EXPORT Ogre::ColourValue* Sphere_GetmColour(ParticleUniverse::Sphere* ptr)
{
	return &ptr->mColour;
}
EXPORT void Sphere_SetmColour(ParticleUniverse::Sphere* ptr, Ogre::ColourValue* newVal)
{
	ptr->mColour = *newVal;
}
EXPORT Ogre::Quaternion* Sphere_GetmOrientation(ParticleUniverse::Sphere* ptr)
{
	return &ptr->mOrientation;
}
EXPORT void Sphere_SetmOrientation(ParticleUniverse::Sphere* ptr, Ogre::Quaternion* newVal)
{
	ptr->mOrientation = *newVal;
}
EXPORT ParticleUniverse::SphereSet* Sphere_GetmParentSet(ParticleUniverse::Sphere* ptr)
{
	return ptr->mParentSet;
}
EXPORT void Sphere_SetmParentSet(ParticleUniverse::Sphere* ptr, ParticleUniverse::SphereSet* newVal)
{
	ptr->mParentSet = newVal;
}

EXPORT ParticleUniverse::Sphere* Sphere_New(void)
{
	return new ParticleUniverse::Sphere();
}
EXPORT ParticleUniverse::Sphere* Sphere_New2(const Ogre::Vector3* position, ParticleUniverse::SphereSet* owner)
{
	return new ParticleUniverse::Sphere(*position, owner);
}
EXPORT void Sphere_Destroy(ParticleUniverse::Sphere* ptr)
{
	ptr->~Sphere();
}
EXPORT void Sphere_SetPosition(ParticleUniverse::Sphere* ptr, const Ogre::Vector3* position)
{
	ptr->setPosition(*position);
}
EXPORT void Sphere_SetPosition2(ParticleUniverse::Sphere* ptr, float x, float y, float z)
{
	ptr->setPosition(x, y, z);
}

EXPORT const Ogre::Vector3* Sphere_GetPosition(ParticleUniverse::Sphere* ptr)
{
	return &ptr->getPosition();
}

EXPORT void Sphere_SetColour(ParticleUniverse::Sphere* ptr, const Ogre::ColourValue* colour)
{
	ptr->setColour(*colour);
}

EXPORT const Ogre::ColourValue* Sphere_GetColour(ParticleUniverse::Sphere* ptr)
{
	return &ptr->getColour();
}

EXPORT void Sphere_ResetRadius(ParticleUniverse::Sphere* ptr)
{
	ptr->resetRadius();
}

EXPORT void Sphere_SetRadius(ParticleUniverse::Sphere* ptr, float radius)
{
	ptr->setRadius(radius);
}

EXPORT bool Sphere_HasOwnRadius(ParticleUniverse::Sphere* ptr)
{
	return ptr->hasOwnRadius();
}

EXPORT float Sphere_GetOwnRadius(ParticleUniverse::Sphere* ptr)
{
	return ptr->getOwnRadius();
}

EXPORT void Sphere__notifyOwner(ParticleUniverse::Sphere* ptr, ParticleUniverse::SphereSet* owner)
{
	ptr->_notifyOwner(owner);
}

#pragma endregion
#pragma region BoxSet PrimitiveShapeSet Exports
	
EXPORT void BoxSet_SetZRotated(ParticleUniverse::BoxSet* ptr, bool zRotated)
{
	ptr->setZRotated(zRotated);
}
EXPORT bool BoxSet_IsZRotated(ParticleUniverse::BoxSet* ptr)
{
	return ptr->isZRotated();
}
EXPORT void BoxSet__notifyZRotated(ParticleUniverse::BoxSet* ptr)
{
	ptr->_notifyZRotated();
}
EXPORT void BoxSet_SetMaterialName(ParticleUniverse::BoxSet* ptr, char* name)
{
	ptr->setMaterialName(name);
}
EXPORT const char* BoxSet_GetMaterialName(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getMaterialName().c_str();
}
EXPORT void BoxSet__notifyResized(ParticleUniverse::BoxSet* ptr)
{
	ptr->_notifyResized();
}
EXPORT void BoxSet__notifyCurrentCamera(ParticleUniverse::BoxSet* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT const Ogre::AxisAlignedBox* BoxSet_GetBoundingBox(ParticleUniverse::BoxSet* ptr)
{
	return &ptr->getBoundingBox();
}
EXPORT float BoxSet_GetBoundingRadius(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getBoundingRadius();
}
EXPORT const Ogre::MaterialPtr* BoxSet_GetMaterial(ParticleUniverse::BoxSet* ptr)
{
	return &ptr->getMaterial();
}
EXPORT void BoxSet_GetWorldTransforms(ParticleUniverse::BoxSet* ptr, Ogre::Matrix4* xform)
{
	ptr->getWorldTransforms(xform);
}
EXPORT const Ogre::Quaternion* BoxSet_GetWorldOrientation(ParticleUniverse::BoxSet* ptr)
{
	return &ptr->getWorldOrientation();
}
EXPORT const Ogre::Vector3* BoxSet_GetWorldPosition(ParticleUniverse::BoxSet* ptr)
{
	return &ptr->getWorldPosition();
}
EXPORT bool BoxSet_IsCullIndividually(ParticleUniverse::BoxSet* ptr)
{
	return ptr->isCullIndividually();
}
EXPORT void BoxSet_SetCullIndividually(ParticleUniverse::BoxSet* ptr, bool cullIndividual)
{
	ptr->setCullIndividually(cullIndividual);
}
EXPORT float BoxSet_GetSquaredViewDepth(ParticleUniverse::BoxSet* ptr, const Ogre::Camera* cam)
{
	return ptr->getSquaredViewDepth(cam);
}
EXPORT const Ogre::LightList* BoxSet_GetLights(ParticleUniverse::BoxSet* ptr)
{
	return &ptr->getLights();
}
EXPORT unsigned int BoxSet_GetTypeFlags(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getTypeFlags();
}
EXPORT void BoxSet_RotateTexture(ParticleUniverse::BoxSet* ptr, float speed)
{
	ptr->rotateTexture(speed);
}
EXPORT void BoxSet_VisitRenderables(ParticleUniverse::BoxSet* ptr, Ogre::Renderable::Visitor* visitor, bool debugRenderables = false)
{
	ptr->visitRenderables(visitor, debugRenderables);
}

#pragma endregion
#pragma region BoxSet Export


//Renderable Implementeation
EXPORT bool BoxSet_GetCastShadows(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getCastsShadows();
}

EXPORT unsigned short BoxSet_GetNumWorldTransforms(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getNumWorldTransforms();
}
EXPORT bool BoxSet_GetPolygonModeOverrideable(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getPolygonModeOverrideable();
}
EXPORT void BoxSet_SetPolygonModeOverrideable(ParticleUniverse::BoxSet* ptr, bool overrideable)
{
	ptr->setPolygonModeOverrideable(overrideable);
}
EXPORT Ogre::Technique* BoxSet_GetTechnique(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getTechnique();
}
EXPORT void BoxSet__updateCustomGpuParameter(ParticleUniverse::BoxSet* ptr, 
            const Ogre::GpuProgramParameters::AutoConstantEntry* constantEntry,
            Ogre::GpuProgramParameters* params)
{
	ptr->_updateCustomGpuParameter(*constantEntry, params);
}
EXPORT void BoxSet_GetRenderOperation(ParticleUniverse::BoxSet* ptr, Ogre::RenderOperation* op)
{
	return ptr->getRenderOperation(*op);
}
EXPORT bool BoxSet_PreRender(ParticleUniverse::BoxSet* ptr, Ogre::SceneManager* sm, Ogre::RenderSystem* rsys)
{
	return ptr->preRender(sm, rsys);
}
EXPORT void BoxSet_PostRender(ParticleUniverse::BoxSet* ptr, Ogre::SceneManager* sm, Ogre::RenderSystem* rsys)
{
	ptr->postRender(sm, rsys);
}
//End Renderable Implementation

EXPORT ParticleUniverse::BoxSet* BoxSet_New()
{
	return new ParticleUniverse::BoxSet();
}
EXPORT void BoxSet_Destroy(ParticleUniverse::BoxSet* ptr)
{
	ptr->~BoxSet();
}

EXPORT ParticleUniverse::Box* BoxSet_CreateBox(ParticleUniverse::BoxSet* ptr, const Ogre::Vector3& position)
{
	return ptr->createBox(position);
}

EXPORT ParticleUniverse::Box* BoxSet_CreateBox2(ParticleUniverse::BoxSet* ptr, float x, float y, float z)
{
	return ptr->createBox(x, y, z);
}

EXPORT unsigned int BoxSet_GetNumBoxes(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getNumBoxes();
}

EXPORT void BoxSet_SetAutoextend(ParticleUniverse::BoxSet* ptr, bool autoextend)
{
	ptr->setAutoextend(autoextend);
}

EXPORT bool BoxSet_IsAutoextend(ParticleUniverse::BoxSet* ptr)
{
	return ptr->isAutoextend();
}

EXPORT void BoxSet_SetPoolSize(ParticleUniverse::BoxSet* ptr, unsigned int size)
{
	ptr->setPoolSize(size);
}

EXPORT unsigned int BoxSet_GetPoolSize(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getPoolSize();
}

EXPORT void BoxSet_Clear(ParticleUniverse::BoxSet* ptr)
{
	ptr->clear();
}

EXPORT ParticleUniverse::Box* BoxSet_GetBox(ParticleUniverse::BoxSet* ptr, unsigned int index)
{
	return ptr->getBox(index);
}

EXPORT void BoxSet_RemoveBox(ParticleUniverse::BoxSet* ptr, unsigned int index)
{
	ptr->removeBox(index);
}

EXPORT void BoxSet_RemoveBox2(ParticleUniverse::BoxSet* ptr, ParticleUniverse::Box* box)
{
	ptr->removeBox(box);
}

EXPORT void BoxSet_SetDefaultDimensions(ParticleUniverse::BoxSet* ptr, float width, float height, float depth)
{
	ptr->setDefaultDimensions(width, height, depth);
}

EXPORT void BoxSet_SetDefaultWidth(ParticleUniverse::BoxSet* ptr, float width)
{
	ptr->setDefaultWidth(width);
}

EXPORT float BoxSet_GetDefaultWidth(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getDefaultWidth();
}

EXPORT void BoxSet_SetDefaultHeight(ParticleUniverse::BoxSet* ptr, float height)
{
	ptr->setDefaultHeight(height);
}

EXPORT float BoxSet_GetDefaultHeight(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getDefaultHeight();
}

EXPORT void BoxSet_SetDefaultDepth(ParticleUniverse::BoxSet* ptr, float depth)
{
	ptr->setDefaultDepth(depth);
}

EXPORT float BoxSet_GetDefaultDepth(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getDefaultDepth();
}

EXPORT void BoxSet_BeginBoxes(ParticleUniverse::BoxSet* ptr, size_t numBoxes = 0)
{
	ptr->beginBoxes(numBoxes);
}

EXPORT void BoxSet_InjectBox(ParticleUniverse::BoxSet* ptr, ParticleUniverse::Box& bb)
{
	ptr->injectBox(bb);
}

EXPORT void BoxSet_EndBoxes(ParticleUniverse::BoxSet* ptr)
{
	ptr->endBoxes();
}

EXPORT void BoxSet_SetBounds(ParticleUniverse::BoxSet* ptr, const ParticleUniverse::AxisAlignedBox* box, float radius)
{
	ptr->setBounds(*box, radius);
}

EXPORT void BoxSet__updateRenderQueue(ParticleUniverse::BoxSet* ptr, Ogre::RenderQueue* queue)
{
	ptr->_updateRenderQueue(queue);
}

EXPORT const char* BoxSet_GetMovableType(ParticleUniverse::BoxSet* ptr)
{
	return ptr->getMovableType().c_str();
}

EXPORT void BoxSet__updateBounds(ParticleUniverse::BoxSet* ptr)
{
	ptr->_updateBounds();
}

EXPORT void BoxSet_SetBoxesInWorldSpace(ParticleUniverse::BoxSet* ptr, bool ws)
{
	ptr->setBoxesInWorldSpace(ws);
}


EXPORT ParticleUniverse::BoxSetFactory* BoxSetFactory_New(void) 
{
	return new ParticleUniverse::BoxSetFactory();
}
EXPORT void BoxSetFactory_Destroy(ParticleUniverse::BoxSetFactory* ptr) 
{
	ptr->~BoxSetFactory();
}

EXPORT const char* BoxSetFactory_GetPU_FACTORY_TYPE_NAME(void)
{
	return ParticleUniverse::BoxSetFactory::PU_FACTORY_TYPE_NAME.c_str();
}

EXPORT void BoxSetFactory_SetPU_FACTORY_TYPE_NAME(char* value)
{
	ParticleUniverse::BoxSetFactory::PU_FACTORY_TYPE_NAME = value;
}

EXPORT const char* BoxSetFactory_GetType(ParticleUniverse::BoxSetFactory* ptr)
{
	return ptr->getType().c_str();
}
EXPORT void BoxSetFactory_DestroyInstance(ParticleUniverse::BoxSetFactory* ptr, Ogre::MovableObject* obj)
{
	ptr->destroyInstance(obj);
}

#pragma endregion
#pragma region Box Exports
EXPORT Ogre::Vector3* Box_GetPosition(ParticleUniverse::Box* ptr)
{
	return &ptr->mPosition;
}
EXPORT Ogre::ColourValue* Box_GetColour(ParticleUniverse::Box* ptr)
{
	return &ptr->mColour;
}
EXPORT Ogre::Quaternion* Box_GetOrientation(ParticleUniverse::Box* ptr)
{
	return &ptr->mOrientation;
}
EXPORT ParticleUniverse::BoxSet* Box_GetParentSet(ParticleUniverse::Box* ptr)
{
	return ptr->mParentSet;
}
EXPORT ParticleUniverse::Box* Box_New()
{
	return new ParticleUniverse::Box();
}

EXPORT void Box_Destroy(ParticleUniverse::Box* ptr)
{
	ptr->~Box();
}

EXPORT ParticleUniverse::Box* Box_New2(const Ogre::Vector3& position, ParticleUniverse::BoxSet* owner)
{
	return new ParticleUniverse::Box(position, owner);
}

EXPORT const Ogre::Vector3* Box_GetCorner(ParticleUniverse::Box* ptr, unsigned int  index)
{
	return &ptr->getCorner(index);
}

EXPORT const Ogre::Vector3* Box_GetWorldspaceCorner(ParticleUniverse::Box* ptr, unsigned int  index)
{
	return &ptr->getWorldspaceCorner(index);
}

EXPORT void Box_SetPosition(ParticleUniverse::Box* ptr, const Ogre::Vector3 position)
{
	ptr->setPosition(position);
}

EXPORT void Box_SetPosition2(ParticleUniverse::Box* ptr, float x, float y, float z)
{
	ptr->setPosition(x, y, z);
}

EXPORT const Ogre::Vector3* Box_GetPosition2(ParticleUniverse::Box* ptr)
{
	return &ptr->getPosition();
}

EXPORT void Box_SetColour(ParticleUniverse::Box* ptr, const Ogre::ColourValue& colour)
{
	ptr->setColour(colour);
}

EXPORT const Ogre::ColourValue* Box_GetColour2(ParticleUniverse::Box* ptr)
{
	return &ptr->getColour();
}

EXPORT void Box_ResetDimensions(ParticleUniverse::Box* ptr)
{
	ptr->resetDimensions();
}

EXPORT void Box_SetDimensions(ParticleUniverse::Box* ptr, float width, float height, float depth)
{
	ptr->setDimensions(width, height, depth);
}

EXPORT bool Box_HasOwnDimensions(ParticleUniverse::Box* ptr)
{
	return ptr->hasOwnDimensions();
}

EXPORT float Box_GetOwnWidth(ParticleUniverse::Box* ptr)
{
	return ptr->getOwnWidth();
}

EXPORT float Box_GetOwnHeight(ParticleUniverse::Box* ptr)
{
	return ptr->getOwnHeight();
}

EXPORT float Box_GetOwnDepth(ParticleUniverse::Box* ptr)
{
	return ptr->getOwnDepth();
}

EXPORT void Box__notifyOwner(ParticleUniverse::Box* ptr, ParticleUniverse::BoxSet* owner)
{
	ptr->_notifyOwner(owner);
}

#pragma endregion

#pragma region Attachable Exports
EXPORT const char* Attachable_Get_PU_ATTACHABLE()
{
	return ParticleUniverse::Attachable::PU_ATTACHABLE.c_str();
}

EXPORT void Attachable_Set_PU_ATTACHABLE(char* value)
{
	ParticleUniverse::Attachable::PU_ATTACHABLE = value;
}

EXPORT ParticleUniverse::Attachable* Attachable_New(void)
{
	return new ParticleUniverse::Attachable();
}

EXPORT void Attachable_Destroy(ParticleUniverse::Attachable ptr)
{
	ptr.~Attachable();
}

EXPORT float Attachable_GetDistanceThreshold(ParticleUniverse::Attachable ptr)
{
	return ptr.getDistanceThreshold();
}

EXPORT void Attachable_SetDistanceThreshold(ParticleUniverse::Attachable ptr, float distanceThreshold)
{
	ptr.setDistanceThreshold(distanceThreshold);
}

EXPORT void Attachable__notifyAttached(ParticleUniverse::Attachable ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr._notifyAttached(parent, isTagPoint);
}

EXPORT void Attachable__notifyCurrentCamera(ParticleUniverse::Attachable ptr, Ogre::Camera* cam)
{
	ptr._notifyCurrentCamera(cam);
}

EXPORT const char* Attachable_GetMovableType(ParticleUniverse::Attachable ptr)
{
	return ptr.getMovableType().c_str();
}

EXPORT const ParticleUniverse::AxisAlignedBox* Attachable_GetBoundingBox(ParticleUniverse::Attachable ptr)
{
	return &ptr.getBoundingBox();
}

EXPORT float Attachable_GetBoundingRadius(ParticleUniverse::Attachable ptr)
{
	return ptr.getBoundingRadius();
}

EXPORT void Attachable__updateRenderQueue(ParticleUniverse::Attachable ptr, Ogre::RenderQueue* queue)
{
	ptr._updateRenderQueue(queue);
}

EXPORT void Attachable_VisitRenderables(ParticleUniverse::Attachable ptr, Ogre::Renderable::Visitor* visitor, bool debugRenderables = false)
{
	ptr.visitRenderables(visitor, debugRenderables);
}

EXPORT void Attachable_CopyAttributesTo (ParticleUniverse::Attachable ptr, ParticleUniverse::Extern* externObject)
{
	ptr.copyAttributesTo(externObject);
}

EXPORT void Attachable__prepare(ParticleUniverse::Attachable ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr._prepare(technique);
}

EXPORT void Attachable__unprepare(ParticleUniverse::Attachable ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr._unprepare(particleTechnique);
}

EXPORT void Attachable__interface(ParticleUniverse::Attachable ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr._interface(technique, particle, timeElapsed);
}

#pragma endregion
#pragma region CameraDependancy

EXPORT ParticleUniverse::CameraDependency* CameraDependency_New()
{
	return new ParticleUniverse::CameraDependency();
}
EXPORT ParticleUniverse::CameraDependency* CameraDependency_New2(float threshold, bool inc)
{
	return new ParticleUniverse::CameraDependency(threshold, inc);
}
EXPORT void CameraDependency_Destroy(ParticleUniverse::CameraDependency* ptr)
{
	ptr->~CameraDependency();
}
EXPORT bool CameraDependency_Affect(ParticleUniverse::CameraDependency* ptr, float baseValue, float dependencyValue)
{
	return ptr->affect(baseValue, dependencyValue);
}
EXPORT float CameraDependency_GetThreshold(ParticleUniverse::CameraDependency* ptr)
{
	return ptr->getThreshold();
}
EXPORT void CameraDependency_SetThreshold(ParticleUniverse::CameraDependency* ptr, float threshold)
{
	ptr->setThreshold(threshold);
}
EXPORT bool CameraDependency_IsIncrease(ParticleUniverse::CameraDependency* ptr)
{
	return ptr->isIncrease();
}
EXPORT void CameraDependency_SetIncrease(ParticleUniverse::CameraDependency* ptr, bool increase)
{
	ptr->setIncrease(increase);
}
EXPORT void CameraDependency_CopyAttributesTo (ParticleUniverse::CameraDependency* ptr, ParticleUniverse::CameraDependency* cameraDependency)
{
	ptr->copyAttributesTo(cameraDependency);
}
#pragma endregion
#pragma region Context Exports
EXPORT ParticleUniverse::Context* Context_New()
{
	return new ParticleUniverse::Context();
}
EXPORT void Context_Destroy(ParticleUniverse::Context* ptr)
{
	ptr->~Context();
}

EXPORT void Context_BeginSection(ParticleUniverse::Context* ptr, 
	const char* sectionName, 
	ParticleUniverse::IElement* element,
	const char* elementName)
{
	ptr->beginSection(sectionName, element, elementName);
}
EXPORT void Context_EndSection(ParticleUniverse::Context* ptr)
{
	ptr->endSection();
}
EXPORT const char* Context_GetCurrentSectionName(ParticleUniverse::Context* ptr)
{
	return ptr->getCurrentSectionName().c_str();
}
EXPORT const char* Context_GetPreviousSectionName(ParticleUniverse::Context* ptr)
{
	return ptr->getPreviousSectionName().c_str();
}
EXPORT const char* Context_GetParentSectionName(ParticleUniverse::Context* ptr)
{
	return ptr->getParentSectionName().c_str();
}
EXPORT ParticleUniverse::IElement* Context_GetCurrentSectionElement(ParticleUniverse::Context* ptr)
{
	return ptr->getCurrentSectionElement();
}
EXPORT ParticleUniverse::IElement* Context_GetPreviousSectionElement(ParticleUniverse::Context* ptr)
{
	return ptr->getPreviousSectionElement();
}
EXPORT ParticleUniverse::IElement* Context_GetParentSectionElement(ParticleUniverse::Context* ptr)
{
	return ptr->getParentSectionElement();
}
EXPORT const char* Context_GetCurrentSectionElementName(ParticleUniverse::Context* ptr)
{
	return ptr->getCurrentSectionElementName().c_str();
}
EXPORT const char* Context_GetPreviousSectionElementName(ParticleUniverse::Context* ptr)
{
	return ptr->getPreviousSectionElementName().c_str();
}
EXPORT const char* Context_GetParentSectionElementName(ParticleUniverse::Context* ptr)
{
	return ptr->getParentSectionElementName().c_str();
}
EXPORT ParticleUniverse::IElement* Context_GetSectionElement(ParticleUniverse::Context* ptr, const char* sName)
{
	return ptr->getSectionElement(sName);
}
EXPORT void Context_ValidateCurrentSectionName(ParticleUniverse::Context* ptr, const char* sectionName, 
	const char* calledFromFunction)
{
	return ptr->validateCurrentSectionName(sectionName, calledFromFunction);
}
#pragma endregion
#pragma region DynamicAttribute Exports

EXPORT float DynamicAttribute_GetValue (ParticleUniverse::DynamicAttribute* ptr, float x = 0)
{
	return ptr->getValue(x);
}

EXPORT ParticleUniverse::DynamicAttribute::DynamicAttributeType DynamicAttribute_GetType (ParticleUniverse::DynamicAttribute* ptr)
{
	return ptr->getType();
}

EXPORT void DynamicAttribute_SetType (ParticleUniverse::DynamicAttribute* ptr, ParticleUniverse::DynamicAttribute::DynamicAttributeType type)
{
	ptr->setType(type);
}

EXPORT void DynamicAttribute_CopyAttributesTo(ParticleUniverse::DynamicAttribute* ptr, ParticleUniverse::DynamicAttribute* dynamicAttribute)
{
	ptr->copyAttributesTo(dynamicAttribute);
}

EXPORT ParticleUniverse::DynamicAttributeFixed* DynamicAttributeFixed_New(void)
{
	ParticleUniverse::DynamicAttributeFixed* psys = new ParticleUniverse::DynamicAttributeFixed();
	psys->setType(ParticleUniverse::DynamicAttributeFixed::DAT_FIXED);
	return psys;
}
EXPORT ParticleUniverse::DynamicAttributeFixed* DynamicAttributeFixed_New2(const ParticleUniverse::DynamicAttributeFixed* dynamicAttributeFixed)
{
	ParticleUniverse::DynamicAttributeFixed* psys = new ParticleUniverse::DynamicAttributeFixed(*dynamicAttributeFixed);
	psys->setType(ParticleUniverse::DynamicAttributeFixed::DAT_FIXED);
	return psys;
}

EXPORT void DynamicAttributeFixed_Destroy (ParticleUniverse::DynamicAttributeFixed* ptr)
{
	ptr->~DynamicAttributeFixed();
}

EXPORT float DynamicAttributeFixed_GetValue (ParticleUniverse::DynamicAttributeFixed* ptr, float x = 0)
{
	return ptr->getValue(x);
}

EXPORT void DynamicAttributeFixed_SetValue (ParticleUniverse::DynamicAttributeFixed* ptr, float value)
{
	ptr->setValue(value);
}

EXPORT void DynamicAttributeFixed_CopyAttributesTo(ParticleUniverse::DynamicAttributeFixed* ptr, ParticleUniverse::DynamicAttribute* dynamicAttribute)
{
	ptr->copyAttributesTo(dynamicAttribute);
}

EXPORT ParticleUniverse::DynamicAttributeRandom* DynamicAttributeRandom_New (void)
{
	ParticleUniverse::DynamicAttributeRandom* psys = new ParticleUniverse::DynamicAttributeRandom();
	psys->setType(ParticleUniverse::DynamicAttribute::DAT_RANDOM);
	return psys;
}

EXPORT ParticleUniverse::DynamicAttributeRandom* DynamicAttributeRandom_New2(const ParticleUniverse::DynamicAttributeRandom* dynamicAttributeRandom)
{
	ParticleUniverse::DynamicAttributeRandom* psys = new ParticleUniverse::DynamicAttributeRandom(*dynamicAttributeRandom);
	psys->setType(ParticleUniverse::DynamicAttribute::DAT_RANDOM);
	return psys;
}

EXPORT void DynamicAttributeRandom_Destroy(ParticleUniverse::DynamicAttributeRandom* ptr)
{
	ptr->~DynamicAttributeRandom();
}

EXPORT float DynamicAttributeRandom_GetValue (ParticleUniverse::DynamicAttributeRandom* ptr, float x = 0)
{
	return ptr->getValue(x);
}

EXPORT void DynamicAttributeRandom_SetMin (ParticleUniverse::DynamicAttributeRandom* ptr, float min)
{
	ptr->setMin(min);
}

EXPORT float DynamicAttributeRandom_GetMin (ParticleUniverse::DynamicAttributeRandom* ptr)
{
	return ptr->getMin();
}

EXPORT void DynamicAttributeRandom_SetMax (ParticleUniverse::DynamicAttributeRandom* ptr, float max)
{
	ptr->setMax(max);
}

EXPORT float DynamicAttributeRandom_GetMax (ParticleUniverse::DynamicAttributeRandom* ptr)
{
	return ptr->getMax();
}

EXPORT void DynamicAttributeRandom_SetMinMax (ParticleUniverse::DynamicAttributeRandom* ptr, float min, float max)
{
	ptr->setMinMax(min, max);
}

EXPORT void DynamicAttributeRandom_CopyAttributesTo(ParticleUniverse::DynamicAttributeRandom* ptr, ParticleUniverse::DynamicAttribute* dynamicAttribute)
{
	ptr->copyAttributesTo(dynamicAttribute);
}


EXPORT ParticleUniverse::DynamicAttributeCurved* DynamicAttributeCurved_New()
{
	ParticleUniverse::DynamicAttributeCurved* psys = new ParticleUniverse::DynamicAttributeCurved();
	psys->setType(ParticleUniverse::DynamicAttribute::DAT_CURVED);
	return psys;
}

EXPORT ParticleUniverse::DynamicAttributeCurved* DynamicAttributeCurved_New2(ParticleUniverse::InterpolationType* interpolationType)
{
	ParticleUniverse::DynamicAttributeCurved* psys = new ParticleUniverse::DynamicAttributeCurved(*interpolationType);
	psys->setType(ParticleUniverse::DynamicAttribute::DAT_CURVED);
	return psys;
}

EXPORT ParticleUniverse::DynamicAttributeCurved* DynamicAttributeCurved_New3(const ParticleUniverse::DynamicAttributeCurved* dynamicAttributeCurved)
{
	return new ParticleUniverse::DynamicAttributeCurved(*dynamicAttributeCurved);
}

EXPORT void DynamicAttributeCurved_Destroy(ParticleUniverse::DynamicAttributeCurved* ptr)
{
	ptr->~DynamicAttributeCurved();
}

EXPORT void DynamicAttributeCurved_SetInterpolationType (ParticleUniverse::DynamicAttributeCurved* ptr, ParticleUniverse::InterpolationType interpolationType)
{
	ptr->setInterpolationType(interpolationType);
}

EXPORT ParticleUniverse::InterpolationType DynamicAttributeCurved_GetInterpolationType (ParticleUniverse::DynamicAttributeCurved* ptr)
{
	return ptr->getInterpolationType();
}

EXPORT float DynamicAttributeCurved_GetValue (ParticleUniverse::DynamicAttributeCurved* ptr, float x = 0)
{
	return ptr->getValue(x);
}

EXPORT void DynamicAttributeCurved_AddControlPoint (ParticleUniverse::DynamicAttributeCurved* ptr, float x, float y)
{
	ptr->addControlPoint(x, y);
}

EXPORT int DynamicAttributeCurved_GetControlPointsCount(ParticleUniverse::DynamicAttributeCurved* ptr)
{
	ParticleUniverse::DynamicAttributeCurved::ControlPointList lines = ptr->getControlPoints();
	return lines.size();
}

EXPORT void DynamicAttributeCurved_GetControlPoints(ParticleUniverse::DynamicAttributeCurved* ptr, Ogre::Vector2* arrLodDistances, int bufSize)
{
	ParticleUniverse::DynamicAttributeCurved::ControlPointList lines = ptr->getControlPoints();
	
	if (lines.size() > (unsigned int)bufSize) //Avoid buffer over run.
		return;

	ParticleUniverse::vector<Ogre::Vector2>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      Ogre::Vector2 name = (Ogre::Vector2)*it;
	  *(arrLodDistances + i) = name;
		i++; // = sizeof(name);
    }
	//return arrLodDistances;
}


EXPORT void DynamicAttributeCurved_ProcessControlPoints (ParticleUniverse::DynamicAttributeCurved* ptr)
{
	ptr->processControlPoints();
}

EXPORT unsigned int DynamicAttributeCurved_GetNumControlPoints(ParticleUniverse::DynamicAttributeCurved* ptr)
{
	return ptr->getNumControlPoints();
}

EXPORT void DynamicAttributeCurved_RemoveAllControlPoints(ParticleUniverse::DynamicAttributeCurved* ptr)
{
	ptr->removeAllControlPoints();
}

EXPORT void DynamicAttributeCurved_CopyAttributesTo(ParticleUniverse::DynamicAttributeCurved* ptr, ParticleUniverse::DynamicAttribute* dynamicAttribute)
{
	ptr->copyAttributesTo(dynamicAttribute);
}


EXPORT ParticleUniverse::DynamicAttributeOscillate* DynamicAttributeOscillate_New(void)
{
	ParticleUniverse::DynamicAttributeOscillate* psys = new ParticleUniverse::DynamicAttributeOscillate();
	psys->setType(ParticleUniverse::DynamicAttribute::DAT_OSCILLATE);
	return psys;
}


EXPORT ParticleUniverse::DynamicAttributeOscillate* DynamicAttributeOscillate_New2(const ParticleUniverse::DynamicAttributeOscillate* dynamicAttributeOscillate)
{
	ParticleUniverse::DynamicAttributeOscillate* psys = new ParticleUniverse::DynamicAttributeOscillate(*dynamicAttributeOscillate);
	psys->setType(ParticleUniverse::DynamicAttribute::DAT_OSCILLATE);
	return psys;
}


EXPORT void DynamicAttributeOscillate_Destroy (ParticleUniverse::DynamicAttributeOscillate* ptr)
{
	ptr->~DynamicAttributeOscillate();
}


EXPORT float DynamicAttributeOscillate_GetValue (ParticleUniverse::DynamicAttributeOscillate* ptr, float x = 0)
{
	return ptr->getValue(x);
}


EXPORT ParticleUniverse::DynamicAttributeOscillate::OscillationType DynamicAttributeOscillate_GetOscillationType (ParticleUniverse::DynamicAttributeOscillate* ptr)
{
	return ptr->getOscillationType();
}

EXPORT void DynamicAttributeOscillate_SetOscillationType (ParticleUniverse::DynamicAttributeOscillate* ptr, ParticleUniverse::DynamicAttributeOscillate::OscillationType* oscillationType)
{
	ptr->setOscillationType(*oscillationType);
}


EXPORT float DynamicAttributeOscillate_GetFrequency (ParticleUniverse::DynamicAttributeOscillate* ptr)
{
	return ptr->getFrequency();
}

EXPORT void DynamicAttributeOscillate_SetFrequency (ParticleUniverse::DynamicAttributeOscillate* ptr, float frequency)
{
	ptr->setFrequency(frequency);
}

EXPORT float DynamicAttributeOscillate_GetPhase (ParticleUniverse::DynamicAttributeOscillate* ptr)
{
	return ptr->getPhase();
}

EXPORT void DynamicAttributeOscillate_SetPhase (ParticleUniverse::DynamicAttributeOscillate* ptr, float phase)
{
	ptr->setPhase(phase);
}

EXPORT float DynamicAttributeOscillate_GetBase (ParticleUniverse::DynamicAttributeOscillate* ptr)
{
	return ptr->getBase();
}

EXPORT void DynamicAttributeOscillate_SetBase (ParticleUniverse::DynamicAttributeOscillate* ptr, float base)
{
	ptr->setBase(base);
}

EXPORT float DynamicAttributeOscillate_GetAmplitude (ParticleUniverse::DynamicAttributeOscillate* ptr)
{
	return ptr->getAmplitude();
}

EXPORT void DynamicAttributeOscillate_SetAmplitude (ParticleUniverse::DynamicAttributeOscillate* ptr, float amplitude)
{
	ptr->setAmplitude(amplitude);
}

EXPORT void DynamicAttributeOscillate_CopyAttributesTo(ParticleUniverse::DynamicAttributeOscillate* ptr, ParticleUniverse::DynamicAttribute* dynamicAttribute)
{
	ptr->copyAttributesTo(dynamicAttribute);
}

//EXPORT bool DynamicAttribute_IsValueChangedExternally(ParticleUniverse::DynamicAttribute* ptr)
//{
////	return false;
//	return ptr->isValueChangedExternally();
//}

#pragma endregion
#pragma region ForceField
EXPORT ParticleUniverse::ForceField* ForceField_New()
{
	return new ParticleUniverse::ForceField();
}
EXPORT void ForceField_Destroy(ParticleUniverse::ForceField* ptr)
{
	ptr->~ForceField();
}
EXPORT void ForceField_Initialise(ParticleUniverse::ForceField* ptr, ParticleUniverse::ForceField::ForceFieldType type,
				const ParticleUniverse::Vector3* position,
				unsigned int forceFieldSize, 
				unsigned short octaves, 
				double frequency, 
				double amplitude, 
				double persistence, 
				const ParticleUniverse::Vector3* worldSize)
{
	ptr->initialise(type, *position, forceFieldSize, octaves, frequency, amplitude, persistence, *worldSize);
}
EXPORT void ForceField_Initialise2(ParticleUniverse::ForceField* ptr, ParticleUniverse::ForceField::ForceFieldType type,
				unsigned int forceFieldSize, 
				unsigned short octaves, 
				double frequency, 
				double amplitude, 
				double persistence, 
				const ParticleUniverse::Vector3* worldSize)
{
	ptr->initialise(type, forceFieldSize, octaves, frequency, amplitude, persistence, *worldSize);
}
EXPORT const ParticleUniverse::Vector3* ForceField_GetForceFieldPositionBase(ParticleUniverse::ForceField* ptr)
{
	return &ptr->getForceFieldPositionBase();
}
EXPORT void ForceField_SetForceFieldPositionBase(ParticleUniverse::ForceField* ptr, const ParticleUniverse::Vector3* position)
{
	ptr->setForceFieldPositionBase(*position);
}
EXPORT void ForceField_DetermineForce(ParticleUniverse::ForceField* ptr, const ParticleUniverse::Vector3* position, ParticleUniverse::Vector3* force, float delta)
{
	ptr->determineForce(*position, *force, delta);
}
EXPORT unsigned short ForceField_GetOctaves(ParticleUniverse::ForceField* ptr)
{
	return ptr->getOctaves();
}
EXPORT void ForceField_SetOctaves(ParticleUniverse::ForceField* ptr, unsigned short octaves)
{
	ptr->setOctaves(octaves);
}
EXPORT double ForceField_GetFrequency(ParticleUniverse::ForceField* ptr)
{
	return ptr->getFrequency();
}
EXPORT void ForceField_SetFrequency(ParticleUniverse::ForceField* ptr, double frequency)
{
	ptr->setFrequency(frequency);
}
EXPORT double ForceField_GetAmplitude(ParticleUniverse::ForceField* ptr)
{
	return ptr->getAmplitude();
}
EXPORT void ForceField_SetAmplitude(ParticleUniverse::ForceField* ptr, double amplitude)
{
	ptr->setAmplitude(amplitude);
}
EXPORT double ForceField_GetPersistence(ParticleUniverse::ForceField* ptr)
{
	return ptr->getPersistence();
}
EXPORT void ForceField_SetPersistence(ParticleUniverse::ForceField* ptr, double persistence)
{
	ptr->setPersistence(persistence);
}
EXPORT unsigned int ForceField_GetForceFieldSize(ParticleUniverse::ForceField* ptr)
{
	return ptr->getForceFieldSize();
}
EXPORT void ForceField_SetForceFieldSize(ParticleUniverse::ForceField* ptr, unsigned int forceFieldSize)
{
	ptr->setForceFieldSize(forceFieldSize);
}
EXPORT ParticleUniverse::Vector3* ForceField_GetWorldSize(ParticleUniverse::ForceField* ptr)
{
	return &ptr->getWorldSize();
}
EXPORT void ForceField_SetWorldSize(ParticleUniverse::ForceField* ptr, const ParticleUniverse::Vector3* worldSize)
{
	ptr->setWorldSize(*worldSize);
}
EXPORT ParticleUniverse::ForceField::ForceFieldType ForceField_GetForceFieldType(ParticleUniverse::ForceField* ptr)
{
	return ptr->getForceFieldType();
}
EXPORT void ForceField_SetForceFieldType(ParticleUniverse::ForceField* ptr, const ParticleUniverse::ForceField::ForceFieldType forceFieldType)
{
	ptr->setForceFieldType(forceFieldType);
}
#pragma endregion
#pragma region IAlias Exports

EXPORT ParticleUniverse::IAlias::AliasType IAlias_GetAliasType(ParticleUniverse::IAlias* ptr)
{
	return ptr->getAliasType();
}
EXPORT void IAlias_SetAliasType(ParticleUniverse::IAlias* ptr, ParticleUniverse::IAlias::AliasType aliasType)
{
	ptr->setAliasType(aliasType);
}

EXPORT const char* IAlias_GetAliasName(ParticleUniverse::IAlias* ptr)
{
	return ptr->getAliasName().c_str();
}
EXPORT void IAlias_SetAliasName(ParticleUniverse::IAlias* ptr, char* aliasName)
{
	ptr->setAliasName(aliasName);
}

#pragma endregion
#pragma region ScriptReader
EXPORT ParticleUniverse::ScriptReader* ScriptReader_New()
{
	return new ParticleUniverse::ScriptReader();
}
EXPORT void ScriptReader_Destroy(ParticleUniverse::ScriptReader* ptr)
{
	ptr->~ScriptReader();
}
EXPORT void ScriptReader_Translate(ParticleUniverse::ScriptReader* ptr, ParticleUniverse::ScriptCompiler* compiler, const ParticleUniverse::AbstractNodePtr* node)
{
	ptr->translate(compiler, *node);
}
EXPORT bool ScriptReader_TranslateChildProperty(ParticleUniverse::ScriptReader* ptr, ParticleUniverse::ScriptCompiler* compiler, const ParticleUniverse::AbstractNodePtr* node)
{
	return ptr->translateChildProperty(compiler, *node);
}
EXPORT bool ScriptReader_TranslateChildObject(ParticleUniverse::ScriptReader* ptr, ParticleUniverse::ScriptCompiler* compiler, const ParticleUniverse::AbstractNodePtr* node)
{
	return ptr->translateChildObject(compiler, *node);
}
#pragma endregion
#pragma region ParticleScriptSerializer Exports
EXPORT ParticleUniverse::Context* ParticleScriptSerializer_GetContext(ParticleUniverse::ParticleScriptSerializer* ptr)
{
	return &ptr->context;
}
EXPORT void ParticleScriptSerializer_SetContext(ParticleUniverse::ParticleScriptSerializer* ptr, ParticleUniverse::Context* context)
{
	ptr->context = *context;
}

EXPORT ParticleUniverse::ParticleScriptSerializer* ParticleScriptSerializer_New()
{
	return new ParticleUniverse::ParticleScriptSerializer();
}

EXPORT void ParticleScriptSerializer_Destroy(ParticleUniverse::ParticleScriptSerializer* ptr)
{
	ptr->~ParticleScriptSerializer();
}

EXPORT void ParticleScriptSerializer_WriteScript(ParticleUniverse::ParticleScriptSerializer* ptr, const ParticleUniverse::ParticleSystem* particleSystem, char* fileName)
{
	ptr->writeScript(particleSystem, fileName);
}


EXPORT const char* ParticleScriptSerializer_WriteScript2(ParticleUniverse::ParticleScriptSerializer* ptr, const ParticleUniverse::ParticleSystem* particleSystem)
{
	return ptr->writeScript(particleSystem).c_str();
}

EXPORT void ParticleScriptSerializer_WriteLine4(ParticleUniverse::ParticleScriptSerializer* ptr,
											   char* s0,char* s1, char* s2,char* s3,char* s4,
											   short indentation0 = -1,
											   short indentation1 = -1,
											   short indentation2 = -1,
											   short indentation3 = -1,
											   short indentation4 = -1)
{
	ptr->writeLine(s0, s1, s2,s3,s4,
				indentation0,indentation1,indentation2,indentation3,indentation4);
}
EXPORT void ParticleScriptSerializer_WriteLine3(ParticleUniverse::ParticleScriptSerializer* ptr,
											   char* s0,char* s1, char* s2,char* s3,
											   short indentation0 = -1,
											   short indentation1 = -1,
											   short indentation2 = -1,
											   short indentation3 = -1)
{
	ptr->writeLine(s0,s1, s2,s3,indentation0,indentation1,indentation2,indentation3);
}
EXPORT void ParticleScriptSerializer_WriteLine2(ParticleUniverse::ParticleScriptSerializer* ptr,
											   char* s0,char* s1, char* s2,
											   short indentation0 = -1,short indentation1 = -1,
											   short indentation2 = -1)
{
	ptr->writeLine(s0, s1, s2,indentation0,indentation1,indentation2);
}

EXPORT void ParticleScriptSerializer_WriteLine1(ParticleUniverse::ParticleScriptSerializer* ptr,
											   char* s0,char* s1, 
											   short indentation0 = -1,short indentation1 = -1)
{
	ptr->writeLine(s0, s1, indentation0,indentation1);
}

EXPORT void ParticleScriptSerializer_WriteLine(ParticleUniverse::ParticleScriptSerializer* ptr,
											   char* s0, short indentation0 = -1)
{
	ptr->writeLine(s0, indentation0);
}

EXPORT void ParticleScriptSerializer_SetDefaultTabs (ParticleUniverse::ParticleScriptSerializer* ptr,
				short tab0 = 0, short tab1 = 48, short tab2 = 52, short tab3 = 56, short tab4 = 60)
{
	ptr->setDefaultTabs(tab0, tab1, tab2, tab3, tab4);
}

EXPORT void ParticleScriptSerializer_SetPath(ParticleUniverse::ParticleScriptSerializer* ptr, char* path)
{
	ptr->setPath(path);
}

EXPORT const char* ParticleScriptSerializer_ToString(ParticleUniverse::Real* argv[], bool applySqrt = false)
{
	ParticleUniverse::vector<Ogre::Real> v;
	
	for (int i = 0; i < sizeof(argv); i++)
		v.push_back(*argv[i]);
	
	//ParticleUniverse::ParticleSystem::LodDistanceList lst(v);

	return ParticleUniverse::ParticleScriptSerializer::toString(v, applySqrt).c_str();
	//ptr->toString(v, applySqrt);
}


EXPORT const short ParticleScriptSerializer_GetIndentation(ParticleUniverse::ParticleScriptSerializer* ptr)
{
	return ptr->getIndentation();
}

EXPORT void ParticleScriptSerializer_SetIndentation(ParticleUniverse::ParticleScriptSerializer* ptr, short indentation)
{
	return ptr->setIndentation(indentation);
}

EXPORT const char* ParticleScriptSerializer_GetKeyword(ParticleUniverse::ParticleScriptSerializer* ptr)
{
	return ptr->getKeyword().c_str();
}
EXPORT void ParticleScriptSerializer_SetKeyword(ParticleUniverse::ParticleScriptSerializer* ptr, char* keyword)
{
	return ptr->setKeyword(keyword);
}

#pragma endregion
#pragma region ScriptWriter
EXPORT void ScriptWriter_Destroy(ParticleUniverse::ScriptWriter* ptr)
{
	ptr->~ScriptWriter();
}
EXPORT void ScriptWriter_Write(ParticleUniverse::ScriptWriter* ptr, ParticleUniverse::ParticleScriptSerializer* serializer, const ParticleUniverse::IElement* element)
{
	ptr->write(serializer, element);
}
#pragma endregion
#pragma region Particle Exports
EXPORT ParticleUniverse::Particle* Particle_New(void)
{
	ParticleUniverse::Particle* psys = new ParticleUniverse::Particle();
	//psys->particleType = ParticleUniverse::Particle::PT_VISUAL;
	return psys;
}

EXPORT void Particle_Destroy(ParticleUniverse::Particle* ptr)
{
	ptr->~Particle();
}

//EXPORT void Particle_ParticleBehaviourList(ParticleUniverse::Particle* ptr)
//{
//	vector<ParticleBehaviour*> ParticleBehaviourList;
//}
//
//EXPORT int Particle_ParticleBehaviousListSize(ParticleUniverse::Particle* ptr)
//{
//	return ptr->mBehaviours.size();
//}

EXPORT float Particle_GetDEFAULT_TTL()
{
	return ParticleUniverse::Particle::DEFAULT_TTL;
}
EXPORT void Particle_SetDEFAULT_TTL(float newTTL)
{
	ParticleUniverse::Particle::DEFAULT_TTL = newTTL;
}

EXPORT float Particle_GetDEFAULT_MASS()
{
	return ParticleUniverse::Particle::DEFAULT_MASS;
}
EXPORT void Particle_SetDEFAULT_MASS(float newDefaultMass)
{
	ParticleUniverse::Particle::DEFAULT_MASS = newDefaultMass;
}

EXPORT ParticleUniverse::ParticleEmitter* Particle_GetParentEmitter(ParticleUniverse::Particle* ptr)
{
	return ptr->parentEmitter;
}
EXPORT void Particle_SetParentEmitter(ParticleUniverse::Particle* ptr, ParticleUniverse::ParticleEmitter* newParent)
{
	ptr->parentEmitter = newParent;
}

EXPORT Ogre::Vector3* Particle_GetPosition(ParticleUniverse::Particle* ptr)
{
	return &ptr->position;
}
EXPORT void Particle_SetPosition(ParticleUniverse::Particle* ptr, Ogre::Vector3 newPos)
{
	ptr->position = newPos;
}

EXPORT Ogre::Vector3* Particle_GetDirection(ParticleUniverse::Particle* ptr)
{
	return &ptr->direction;
}
EXPORT void Particle_SetDirection(ParticleUniverse::Particle* ptr, Ogre::Vector3 newDir)
{
	ptr->direction = newDir;
}

EXPORT float Particle_GetMass(ParticleUniverse::Particle* ptr)
{
	return ptr->mass;
}
EXPORT void Particle_SetMass(ParticleUniverse::Particle* ptr, float newMass)
{
	ptr->mass = newMass;
}

EXPORT float Particle_GetTimeToLive(ParticleUniverse::Particle* ptr)
{
	return ptr->timeToLive;
}
EXPORT void Particle_SetTimeToLive(ParticleUniverse::Particle* ptr, float newTTL)
{
	ptr->timeToLive = newTTL;
}

EXPORT float Particle_GetTotalTimeToLive(ParticleUniverse::Particle* ptr)
{
	return ptr->totalTimeToLive;
}
EXPORT void Particle_SetTotalTimeToLive(ParticleUniverse::Particle* ptr, float newTotalTTL)
{
	ptr->totalTimeToLive = newTotalTTL;
}

EXPORT float Particle_GetTimeFraction(ParticleUniverse::Particle* ptr)
{
	return ptr->timeFraction;
}
EXPORT void Particle_SetTimeFraction(ParticleUniverse::Particle* ptr, float newTimeFrac)
{
	ptr->timeFraction = newTimeFrac;
}

EXPORT ParticleUniverse::Particle::ParticleType Particle_GetParticleType(ParticleUniverse::Particle* ptr)
{
	return ptr->particleType;
}
EXPORT void Particle_SetParticleType(ParticleUniverse::Particle* ptr, ParticleUniverse::Particle::ParticleType newType)
{
	ptr->particleType = newType;
}

EXPORT ParticleUniverse::Any* Particle_GetMUserDefinedObject(ParticleUniverse::Particle* ptr)
{
	return &ptr->mUserDefinedObject;
}
EXPORT void Particle_SetMUserDefinedObject(ParticleUniverse::Particle* ptr, ParticleUniverse::Any* newObj)
{
	ptr->mUserDefinedObject = newObj;
}

EXPORT ParticleUniverse::PhysicsActor* Particle_GetPhysicsActor(ParticleUniverse::Particle* ptr)
{
	return ptr->physicsActor;
}
EXPORT void Particle_SetPhysicsActor(ParticleUniverse::Particle* ptr, ParticleUniverse::PhysicsActor* newActor)
{
	ptr->physicsActor = newActor;
}

EXPORT ParticleUniverse::IVisualData* Particle_GetVisualData(ParticleUniverse::Particle* ptr)
{
	return ptr->visualData;
}
EXPORT void Particle_SetVisualData(ParticleUniverse::Particle* ptr, ParticleUniverse::IVisualData* newVisualData)
{
	ptr->visualData = newVisualData;
}

EXPORT Ogre::Vector3* Particle_GetOriginalPosition(ParticleUniverse::Particle* ptr)
{
	return &ptr->originalPosition;
}
EXPORT void Particle_SetOriginalPosition(ParticleUniverse::Particle* ptr, Ogre::Vector3* newOrigPos)
{
	ptr->originalPosition = *newOrigPos;
}

EXPORT Ogre::Vector3* Particle_GetOriginalDirection(ParticleUniverse::Particle* ptr)
{
	return &ptr->originalDirection;
}
EXPORT void Particle_SetOriginalDirection(ParticleUniverse::Particle* ptr, Ogre::Vector3* newOrigDir)
{
	ptr->originalDirection = *newOrigDir;
}

EXPORT float Particle_GetOriginalVelocity(ParticleUniverse::Particle* ptr)
{
	return ptr->originalVelocity;
}
EXPORT void Particle_SetOriginalVelocity(ParticleUniverse::Particle* ptr, float origVel)
{
	ptr->originalVelocity = origVel;
}

EXPORT float Particle_GetOriginalDirectionLength(ParticleUniverse::Particle* ptr)
{
	return ptr->originalDirectionLength;
}
EXPORT void Particle_SetOriginalDirectionLength(ParticleUniverse::Particle* ptr, float origLen)
{
	ptr->originalDirectionLength = origLen;
}

EXPORT float Particle_GetOriginalScaledDirectionLength(ParticleUniverse::Particle* ptr)
{
	return ptr->originalScaledDirectionLength;
}
EXPORT void Particle_SetOriginalScaledDirectionLength(ParticleUniverse::Particle* ptr, float origScaleLen)
{
	ptr->originalScaledDirectionLength = origScaleLen;
}

EXPORT Ogre::Vector3* Particle_GetLatestPosition(ParticleUniverse::Particle* ptr)
{
	return &ptr->latestPosition;
}
EXPORT void Particle_SetLatestPosition(ParticleUniverse::Particle* ptr, Ogre::Vector3* latestPos)
{
	ptr->latestPosition = *latestPos;
}

			
EXPORT bool Particle__isMarkedForEmission(ParticleUniverse::Particle* ptr)
{
	return ptr->_isMarkedForEmission();
}
EXPORT void Particle__setMarkedForEmission(ParticleUniverse::Particle* ptr, bool markedForEmission)
{
	ptr->_setMarkedForEmission(markedForEmission);
}


EXPORT  void Particle__initForEmission(ParticleUniverse::Particle* ptr)
{
	ptr->_initForEmission();
}

EXPORT  void Particle__initForExpiration(ParticleUniverse::Particle* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_initForExpiration(technique, timeElapsed);
}

EXPORT  bool Particle_IsEnabled(ParticleUniverse::Particle* ptr)
{
	return ptr->isEnabled();
}

EXPORT  void Particle_SetEnabled(ParticleUniverse::Particle* ptr, bool enabled)
{
	ptr->setEnabled(enabled);
}

EXPORT void Particle__setOriginalEnabled(ParticleUniverse::Particle* ptr, bool originalEnabled)
{
	ptr->_setOriginalEnabled(originalEnabled);
}

EXPORT bool Particle__getOriginalEnabled(ParticleUniverse::Particle* ptr)
{
	return ptr->_getOriginalEnabled();
}

EXPORT bool Particle_IsFreezed(ParticleUniverse::Particle* ptr)
{
	return ptr->isFreezed();
}

EXPORT void Particle_SetFreezed(ParticleUniverse::Particle* ptr, bool freezed)
{
	ptr->setFreezed(freezed);
}

EXPORT void Particle_SetEventFlags(ParticleUniverse::Particle* ptr, unsigned int flags)
{
	ptr->setEventFlags(flags);
}

EXPORT void Particle_AddEventFlags(ParticleUniverse::Particle* ptr, unsigned int flags)
{
	ptr->addEventFlags(flags);
}

EXPORT void Particle_RemoveEventFlags(ParticleUniverse::Particle* ptr, unsigned int flags)
{
	ptr->removeEventFlags(flags);
}

EXPORT unsigned int Particle_GetEventFlags(ParticleUniverse::Particle* ptr)
{
	return ptr->getEventFlags();
}

EXPORT bool Particle_HasEventFlags(ParticleUniverse::Particle* ptr, unsigned int flags)
{
	return ptr->hasEventFlags(flags);
}
//TODO: This should input an array to convert!
EXPORT void Particle_CopyBehaviours(ParticleUniverse::Particle* ptr, ParticleUniverse::Particle::ParticleBehaviourList* behaviours, int listSize)
{
	ptr->copyBehaviours(*behaviours);
}

EXPORT  void Particle__process(ParticleUniverse::Particle* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_process(technique, timeElapsed);
}

EXPORT ParticleUniverse::ParticleBehaviour* Particle_GetBehaviour(ParticleUniverse::Particle* ptr, char* behaviourType)
{
	return ptr->getBehaviour(behaviourType);
}

EXPORT float Particle_CalculateVelocity(ParticleUniverse::Particle* ptr)
{
	return ptr->calculateVelocity();
}

EXPORT void Particle_CopyAttributesTo (ParticleUniverse::Particle* ptr, ParticleUniverse::Particle* particle)
{
	ptr->copyAttributesTo(particle);
}

#pragma endregion
#pragma region ParticlePool
EXPORT ParticleUniverse::ParticlePool* ParticlePool_New()
{
	return new ParticleUniverse::ParticlePool();
}
EXPORT void ParticlePool_Destroy(ParticleUniverse::ParticlePool* ptr)
{
	ptr->~ParticlePool();
}
EXPORT void ParticlePool_SetParentTechnique(ParticleUniverse::ParticlePool* ptr, ParticleUniverse::ParticleTechnique* parentTechnique)
{
	ptr->setParentTechnique(parentTechnique);
}
EXPORT bool ParticlePool_IsEmpty(ParticleUniverse::ParticlePool* ptr)
{
	return ptr->isEmpty();
}
EXPORT bool ParticlePool_IsEmpty2(ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType)
{
	return ptr->isEmpty(particleType);
}
EXPORT size_t ParticlePool_GetSize(ParticleUniverse::ParticlePool* ptr)
{
	return ptr->getSize();
}
EXPORT size_t ParticlePool_GetSize2(ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType)
{
	return ptr->getSize(particleType);
}
EXPORT void ParticlePool_InitialisePool(ParticleUniverse::ParticlePool* ptr)
{
	ptr->initialisePool();
}
EXPORT void ParticlePool_IncreasePool (ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType, size_t size, ParticleUniverse::Particle::ParticleBehaviourList& behaviours, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->increasePool(particleType, size, behaviours, technique);
}
EXPORT void ParticlePool_DestroyParticles(ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType)
{
	ptr->destroyParticles(particleType);
}
EXPORT void ParticlePool_DestroyAllVisualParticles(ParticleUniverse::ParticlePool* ptr)
{
	ptr->destroyAllVisualParticles();
}
EXPORT void ParticlePool_DestroyAllEmitterParticles(ParticleUniverse::ParticlePool* ptr)
{
	ptr->destroyAllEmitterParticles();
}
EXPORT void ParticlePool_DestroyAllTechniqueParticles(ParticleUniverse::ParticlePool* ptr)
{
	ptr->destroyAllTechniqueParticles();
}
EXPORT void ParticlePool_DestroyAllAffectorParticles(ParticleUniverse::ParticlePool* ptr)
{
	ptr->destroyAllAffectorParticles();
}
EXPORT void ParticlePool_DestroyAllSystemParticles(ParticleUniverse::ParticlePool* ptr)
{
	ptr->destroyAllSystemParticles();
}
EXPORT ParticleUniverse::Particle* ParticlePool_ReleaseParticle (ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType, const char* name)
{
	return ptr->releaseParticle(particleType, name);
}
EXPORT void ParticlePool_ReleaseAllParticles (ParticleUniverse::ParticlePool* ptr)
{
	ptr->releaseAllParticles();
}
EXPORT void ParticlePool_LockLatestParticle (ParticleUniverse::ParticlePool* ptr)
{
	ptr->lockLatestParticle();
}
EXPORT void ParticlePool_LockAllParticles (ParticleUniverse::ParticlePool* ptr)
{
	ptr->lockAllParticles();
}
EXPORT void ParticlePool_ResetIterator(ParticleUniverse::ParticlePool* ptr)
{
	ptr->resetIterator();
}
EXPORT ParticleUniverse::Particle* ParticlePool_GetFirst(ParticleUniverse::ParticlePool* ptr)
{
	return ptr->getFirst();
}
EXPORT ParticleUniverse::Particle* ParticlePool_GetNext(ParticleUniverse::ParticlePool* ptr)
{
	return ptr->getNext();
}
EXPORT ParticleUniverse::Particle* ParticlePool_GetFirst2(ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType)
{
	return ptr->getFirst(particleType);
}
EXPORT ParticleUniverse::Particle* ParticlePool_GetNext2(ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType)
{
	return ptr->getNext(particleType);
}
EXPORT bool ParticlePool_End(ParticleUniverse::ParticlePool* ptr)
{
	return ptr->end();
}
EXPORT bool ParticlePool_End2(ParticleUniverse::ParticlePool* ptr, const ParticleUniverse::Particle::ParticleType particleType)
{
	return ptr->end(particleType);
}

EXPORT int ParticlePool_GetVisualParticlesListCount(ParticleUniverse::ParticlePool* ptr)
{
	ParticleUniverse::Pool<ParticleUniverse::VisualParticle>::PoolList lines = ptr->getVisualParticlesList();
	return lines.size();
}
EXPORT void ParticlePool_GetVisualParticlesList(ParticleUniverse::ParticlePool* ptr, ParticleUniverse::VisualParticle** arrLodDistances, int bufSize)
{
	ParticleUniverse::Pool<ParticleUniverse::VisualParticle>::PoolList lines = ptr->getVisualParticlesList();
	
	if (lines.size() > (unsigned int)bufSize) //Avoid buffer over run.
		return;

	ParticleUniverse::Pool<ParticleUniverse::VisualParticle>::PoolList::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      ParticleUniverse::VisualParticle* name = (ParticleUniverse::VisualParticle*)*it;
	  *(arrLodDistances + i) = name;
		i++; // = sizeof(name);
    }
	//return arrLodDistances;
}
#pragma endregion
#pragma region ParticleSystem Exports
EXPORT bool ParticleSystem_DEFAULT_KEEP_LOCAL()
{
	return ParticleUniverse::ParticleSystem::DEFAULT_KEEP_LOCAL;
}

EXPORT Ogre::Real ParticleSystem_DEFAULT_ITERATION_INTERVAL()
{
	return ParticleUniverse::ParticleSystem::DEFAULT_ITERATION_INTERVAL;
}
EXPORT Ogre::Real ParticleSystem_DEFAULT_FIXED_TIMEOUT()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_FIXED_TIMEOUT;
}
EXPORT Ogre::Real ParticleSystem_DEFAULT_NON_VISIBLE_UPDATE_TIMEOUT()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_NON_VISIBLE_UPDATE_TIMEOUT;
}
EXPORT bool ParticleSystem_DEFAULT_SMOOTH_LOD()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_SMOOTH_LOD;
}
EXPORT Ogre::Real ParticleSystem_DEFAULT_FAST_FORWARD_TIME()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_FAST_FORWARD_TIME;
}
EXPORT const char* ParticleSystem_DEFAULT_MAIN_CAMERA_NAME()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_MAIN_CAMERA_NAME;
}
EXPORT Ogre::Real ParticleSystem_DEFAULT_SCALE_VELOCITY()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_SCALE_VELOCITY;
}
EXPORT Ogre::Real ParticleSystem_DEFAULT_SCALE_TIME()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_SCALE_TIME;
}
EXPORT const ParticleUniverse::Vector3* ParticleSystem_DEFAULT_SCALE()
{
			return &ParticleUniverse::ParticleSystem::DEFAULT_SCALE;
}
EXPORT bool ParticleSystem_DEFAULT_TIGHT_BOUNDINGBOX()
{
			return ParticleUniverse::ParticleSystem::DEFAULT_TIGHT_BOUNDINGBOX;
}
			

EXPORT ParticleUniverse::ParticleSystem* ParticleSystem_New(char* name)
{
	ParticleUniverse::ParticleSystem* psys = new ParticleUniverse::ParticleSystem(name);
	psys->particleType = ParticleUniverse::Particle::PT_SYSTEM;
	return psys;
}

EXPORT ParticleUniverse::ParticleSystem* ParticleSystem_New2(char* name, char* resourceGroupName)
{
	ParticleUniverse::ParticleSystem* psys = new ParticleUniverse::ParticleSystem(name, resourceGroupName);
	psys->particleType = ParticleUniverse::Particle::PT_SYSTEM;
	return psys;
}

EXPORT void ParticleSystem_Destroy(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->~ParticleSystem();
}

EXPORT const ParticleUniverse::Vector3* ParticleSystem_GetDerivedPosition(ParticleUniverse::ParticleSystem* ptr)
{
	return &ptr->getDerivedPosition();
}

EXPORT const ParticleUniverse::Quaternion* ParticleSystem_GetDerivedOrientation(ParticleUniverse::ParticleSystem* ptr){
	return &ptr->getDerivedOrientation();
}
EXPORT const ParticleUniverse::Quaternion* ParticleSystem_GetLatestOrientation(ParticleUniverse::ParticleSystem* ptr){
	return &ptr->getLatestOrientation();
}
EXPORT bool ParticleSystem_HasRotatedBetweenUpdates(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->hasRotatedBetweenUpdates();
}
EXPORT void ParticleSystem_RotationOffset(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::Vector3& pos)
{
	ptr->rotationOffset(pos);
}
EXPORT bool ParticleSystem_IsSmoothLod (ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->isSmoothLod();
}
EXPORT void ParticleSystem_SetSmoothLod (ParticleUniverse::ParticleSystem* ptr, bool smoothLod)
{
	ptr->setSmoothLod(smoothLod);
}
EXPORT Ogre::Real ParticleSystem_GetTimeElapsedSinceStart(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getTimeElapsedSinceStart();
}
EXPORT const char* ParticleSystem_GetResourceGroupName(ParticleUniverse::ParticleSystem* ptr)
{
	Ogre::String strToReturn = ptr->getResourceGroupName();
	return strToReturn.c_str();
}
EXPORT void ParticleSystem_SetResourceGroupName(ParticleUniverse::ParticleSystem* ptr, char* resourceGroupName)
{
	ptr->setResourceGroupName(resourceGroupName);
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleSystem_CreateTechnique(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->createTechnique();
}
EXPORT void ParticleSystem_AddTechnique (ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->addTechnique(technique);
}
EXPORT void ParticleSystem_RemoveTechnique (ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->removeTechnique(technique);
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleSystem_GetTechnique (ParticleUniverse::ParticleSystem* ptr, size_t index)
{
	return ptr->getTechnique(index);
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleSystem_GetTechnique2 (ParticleUniverse::ParticleSystem* ptr, char* name)
{
	return ptr->getTechnique(name);
}
EXPORT int ParticleSystem_GetNumTechniques (ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getNumTechniques();
}
EXPORT void ParticleSystem_DestroyTechnique(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->destroyTechnique(particleTechnique);
}
EXPORT void ParticleSystem_DestroyTechnique2 (ParticleUniverse::ParticleSystem* ptr, size_t index)
{
	ptr->destroyTechnique(index);
}
EXPORT void ParticleSystem_DestroyAllTechniques (ParticleUniverse::ParticleSystem* ptr)
{
	ptr->destroyAllTechniques();
}
EXPORT void ParticleSystem__notifyAttached(ParticleUniverse::ParticleSystem* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void ParticleSystem__notifyCurrentCamera(ParticleUniverse::ParticleSystem* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT const char* ParticleSystem_GetMovableType(ParticleUniverse::ParticleSystem* ptr)
{
	Ogre::String strToReturn = ptr->getMovableType();
	return strToReturn.c_str();
}
EXPORT const ParticleUniverse::AxisAlignedBox* ParticleSystem_GetBoundingBox(ParticleUniverse::ParticleSystem* ptr)
{
	return &ptr->getBoundingBox();
}
EXPORT Ogre::Real ParticleSystem_GetBoundingRadius(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getBoundingRadius();
}
EXPORT void ParticleSystem__updateRenderQueue(ParticleUniverse::ParticleSystem* ptr, Ogre::RenderQueue* queue)
{
	ptr->_updateRenderQueue(queue);
}
EXPORT void ParticleSystem_SetRenderQueueGroup(ParticleUniverse::ParticleSystem* ptr, Ogre::uint8 queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT void ParticleSystem__update(ParticleUniverse::ParticleSystem* ptr, Ogre::Real timeElapsed)
{
	ptr->_update(timeElapsed);
}
EXPORT int ParticleSystem__updateTechniques(ParticleUniverse::ParticleSystem* ptr, Ogre::Real timeElapsed)
{
	return ptr->_updateTechniques(timeElapsed);
}
EXPORT Ogre::Real ParticleSystem_GetNonVisibleUpdateTimeout(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getNonVisibleUpdateTimeout();
}
EXPORT void ParticleSystem_SetNonVisibleUpdateTimeout(ParticleUniverse::ParticleSystem* ptr, Ogre::Real timeout)
{
	ptr->setNonVisibleUpdateTimeout(timeout);
}
EXPORT void ParticleSystem_Prepare (ParticleUniverse::ParticleSystem* ptr)
{
	ptr->prepare();
}
EXPORT void ParticleSystem_Start(ParticleUniverse::ParticleSystem* ptr) 
{
	ptr->start();
}
EXPORT void ParticleSystem_Start2(ParticleUniverse::ParticleSystem* ptr, Ogre::Real stopTime)
{
	ptr->start(stopTime);
}
EXPORT void ParticleSystem_StartAndStopFade(ParticleUniverse::ParticleSystem* ptr, Ogre::Real stopTime)
{
	ptr->startAndStopFade(stopTime);
}
EXPORT void ParticleSystem_Stop(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->stop();
}
EXPORT void ParticleSystem_Stop2(ParticleUniverse::ParticleSystem* ptr, Ogre::Real stopTime)
{
	ptr->stop(stopTime);
}
EXPORT void ParticleSystem_StopFade(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->stopFade();
}
EXPORT void ParticleSystem_StopFade2(ParticleUniverse::ParticleSystem* ptr, Ogre::Real stopTime)
{
	ptr->stopFade(stopTime);
}
EXPORT void ParticleSystem_Pause(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->pause();
}
EXPORT void ParticleSystem_Pause2(ParticleUniverse::ParticleSystem* ptr, Ogre::Real pauseTime)
{
	ptr->pause(pauseTime);
}
EXPORT void ParticleSystem_Resume(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->resume();
}
EXPORT int ParticleSystem_GetState (ParticleUniverse::ParticleSystem* ptr)
{
	//ParticleUniverse::ParticleSystem::ParticleSystemState pss = ;
	return int(ptr->getState());
}
EXPORT Ogre::Real ParticleSystem_GetFastForwardTime(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getFastForwardTime();
}
EXPORT Ogre::Real ParticleSystem_GetFastForwardInterval(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getFastForwardInterval();
}
EXPORT void ParticleSystem_SetFastForward(ParticleUniverse::ParticleSystem* ptr, Ogre::Real time, Ogre::Real interval)
{
	ptr->setFastForward(time, interval);
}
EXPORT const char* ParticleSystem_GetMainCameraName(ParticleUniverse::ParticleSystem* ptr)
{
	Ogre::String strToReturn = ptr->getMainCameraName();
	return strToReturn.c_str();
}
EXPORT Ogre::Camera* ParticleSystem_GetMainCamera(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getMainCamera();
}
EXPORT bool ParticleSystem_HasMainCamera(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->hasMainCamera();
}
EXPORT void ParticleSystem_SetMainCameraName(ParticleUniverse::ParticleSystem* ptr, char* cameraName)
{
	ptr->setMainCameraName(cameraName);
}
EXPORT void ParticleSystem_SetMainCamera(ParticleUniverse::ParticleSystem* ptr, Ogre::Camera* camera)
{
	ptr->setMainCamera(camera);
}
EXPORT Ogre::Camera* ParticleSystem_GetCurrentCamera(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getCurrentCamera();
}
EXPORT void ParticleSystem_FastForward(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->fastForward();
}
EXPORT bool ParticleSystem_IsParentIsTagPoint(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->isParentIsTagPoint();
}

EXPORT int ParticleSystem_GetLodDistancesCount(ParticleUniverse::ParticleSystem* ptr)
{
	ParticleUniverse::ParticleSystem::LodDistanceList lines = ptr->getLodDistances();
	return lines.size();
}

EXPORT void ParticleSystem_GetLodDistances(ParticleUniverse::ParticleSystem* ptr, float* arrLodDistances, int bufSize)
{
	ParticleUniverse::ParticleSystem::LodDistanceList lines = ptr->getLodDistances();
	
	if (lines.size() > (unsigned int)bufSize) //Avoid buffer over run.
		return;

	ParticleUniverse::vector<Ogre::Real>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      float name = (float)*it;
	  *(arrLodDistances + i) = name;
		i++; // = sizeof(name);
    }
	//return arrLodDistances;
}

EXPORT void ParticleSystem_ClearLodDistances(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->clearLodDistances();
}
EXPORT void ParticleSystem_AddLodDistance(ParticleUniverse::ParticleSystem* ptr, Ogre::Real distance)
{
	ptr->addLodDistance(distance);
}
EXPORT void ParticleSystem_SetLodDistances(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::Real* argv[])
{
	ParticleUniverse::vector<Ogre::Real> v;
	
	for (int i = 0; i < sizeof(argv); i++)
		v.push_back(*argv[i]);
	
	ParticleUniverse::ParticleSystem::LodDistanceList lst(v);
	ptr->setLodDistances(lst);
}
EXPORT int ParticleSystem_GetNumEmittedTechniques (ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getNumEmittedTechniques();
}
EXPORT void ParticleSystem__markForEmission(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_markForEmission();
}
EXPORT void ParticleSystem__resetMarkForEmission(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_resetMarkForEmission();
}
EXPORT void ParticleSystem_SuppressNotifyEmissionChange(ParticleUniverse::ParticleSystem* ptr, bool suppress)
{
	ptr->suppressNotifyEmissionChange(suppress);
}
EXPORT void ParticleSystem__notifyEmissionChange(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_notifyEmissionChange();
}
EXPORT Ogre::Real ParticleSystem_GetIterationInterval(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getIterationInterval();
}
EXPORT void ParticleSystem_SetIterationInterval(ParticleUniverse::ParticleSystem* ptr, Ogre::Real iterationInterval)
{
	ptr->setIterationInterval(iterationInterval);
}
EXPORT Ogre::Real ParticleSystem_GetFixedTimeout(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getFixedTimeout();
}
EXPORT void ParticleSystem_SetFixedTimeout(ParticleUniverse::ParticleSystem* ptr, Ogre::Real fixedTimeout)
{
	ptr->setFixedTimeout(fixedTimeout);
}
EXPORT void ParticleSystem_SetBoundsAutoUpdated(ParticleUniverse::ParticleSystem* ptr, bool autoUpdate, Ogre::Real stopIn = 0.0f)
{
	ptr->setBoundsAutoUpdated(autoUpdate, stopIn);
}
EXPORT void ParticleSystem__resetBounds(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_resetBounds();
}
EXPORT const ParticleUniverse::Vector3* ParticleSystem_GetScale(ParticleUniverse::ParticleSystem* ptr)
{
	return &ptr->getScale();
}
EXPORT void ParticleSystem_SetScale(ParticleUniverse::ParticleSystem* ptr, Ogre::Vector3& scale)
{
	ptr->setScale(scale);
}
EXPORT void ParticleSystem__notifyRescaled(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_notifyRescaled();
}
EXPORT const ParticleUniverse::Real ParticleSystem_GetScaleVelocity(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getScaleVelocity();
}
EXPORT void ParticleSystem_SetScaleVelocity(ParticleUniverse::ParticleSystem* ptr, Ogre::Real& scaleVelocity)
{
	ptr->setScaleVelocity(scaleVelocity);
}
EXPORT void ParticleSystem__notifyVelocityRescaled(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_notifyVelocityRescaled();
}
EXPORT const ParticleUniverse::Real ParticleSystem_GetScaleTime(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getScaleTime();
}
EXPORT void ParticleSystem_SetScaleTime(ParticleUniverse::ParticleSystem* ptr, Ogre::Real& scaleTime)
{
	ptr->setScaleTime(scaleTime);
}
EXPORT void ParticleSystem__initForEmission(ParticleUniverse::ParticleSystem* ptr)
{
	ptr->_initForEmission();
}
EXPORT void ParticleSystem__initForExpiration(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Real timeElapsed)
{
	ptr->_initForExpiration(technique, timeElapsed);
}
EXPORT void ParticleSystem__process(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Real timeElapsed)
{
	ptr->_process(technique, timeElapsed);
}
EXPORT bool ParticleSystem_IsKeepLocal(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->isKeepLocal();
}
EXPORT void ParticleSystem_SetKeepLocal(ParticleUniverse::ParticleSystem* ptr, bool keepLocal)
{
	ptr->setKeepLocal(keepLocal);
}
EXPORT bool ParticleSystem_MakeParticleLocal(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::Particle* particle)
{
	return ptr->makeParticleLocal(particle);
}
EXPORT bool ParticleSystem_HasTightBoundingBox(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->hasTightBoundingBox();
}
EXPORT void ParticleSystem_SetTightBoundingBox(ParticleUniverse::ParticleSystem* ptr, bool tightBoundingBox)
{
	ptr->setTightBoundingBox(tightBoundingBox);
}
EXPORT void ParticleSystem_AddParticleSystemListener (ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleSystemListener* particleSystemListener)
{
	ptr->addParticleSystemListener(particleSystemListener);
}
EXPORT void ParticleSystem_RemoveParticleSystemListener (ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleSystemListener* particleSystemListener)
{
	ptr->removeParticleSystemListener(particleSystemListener);
}
EXPORT void ParticleSystem_PushEvent(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::ParticleUniverseEvent& particleUniverseEvent)
{
	ptr->pushEvent(particleUniverseEvent);
}
EXPORT void ParticleSystem_VisitRenderables (ParticleUniverse::ParticleSystem* ptr, Ogre::Renderable::Visitor* visitor, Ogre::Real getPauseTime)
{
	ptr->visitRenderables(visitor, getPauseTime);
}
/** Returns the time of a pause (if set)
			*/
EXPORT Ogre::Real ParticleSystem_GetPauseTime (ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getPauseTime();
}
EXPORT void ParticleSystem_SetPauseTime (ParticleUniverse::ParticleSystem* ptr, Ogre::Real pauseTime)
{
	ptr->setPauseTime(pauseTime);
}
EXPORT const char* ParticleSystem_GetTemplateName(ParticleUniverse::ParticleSystem* ptr)
{
	Ogre::String strToReturn = ptr->getTemplateName();
	return strToReturn.c_str();
}
EXPORT void ParticleSystem_SetTemplateName(ParticleUniverse::ParticleSystem* ptr, Ogre::String& templateName)
{
	ptr->setTemplateName(templateName);
}
EXPORT void ParticleSystem_SetEnabled(ParticleUniverse::ParticleSystem* ptr, bool enabled)
{
	ptr->setEnabled(enabled);
}
EXPORT Ogre::SceneManager* ParticleSystem_GetSceneManager(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getSceneManager();
}
EXPORT void ParticleSystem_SetSceneManager(ParticleUniverse::ParticleSystem* ptr, Ogre::SceneManager* sceneManager)
{
	ptr->setSceneManager(sceneManager);
}
EXPORT void ParticleSystem_SetUseController(ParticleUniverse::ParticleSystem* ptr, bool useController)
{
	ptr->setUseController(useController);
}
EXPORT bool ParticleSystem_HasExternType(ParticleUniverse::ParticleSystem* ptr, char* externType)
{
	return ptr->hasExternType(externType);
}
EXPORT int ParticleSystem_GetNumberOfEmittedParticles(ParticleUniverse::ParticleSystem* ptr)
{
	return ptr->getNumberOfEmittedParticles();
}
EXPORT int ParticleSystem_GetNumberOfEmittedParticles2(ParticleUniverse::ParticleSystem* ptr, ParticleUniverse::Particle::ParticleType particleType)
{
	return ptr->getNumberOfEmittedParticles(particleType);
}
EXPORT const char* ParticleSystem_GetCategory(ParticleUniverse::ParticleSystem* ptr)
{
	Ogre::String strToReturn = ptr->getCategory();
	return strToReturn.c_str();
}
EXPORT void ParticleSystem_SetCategory(ParticleUniverse::ParticleSystem* ptr, char* category)
{
	ptr->setCategory(category);
}

#pragma endregion

#pragma region ParticleSystemManager Exports

EXPORT ParticleUniverse::ParticleSystemManager* ParticleSystemManager_New()
{
	return new ParticleUniverse::ParticleSystemManager();
}

EXPORT void ParticleSystemManager_Destroy(ParticleUniverse::ParticleSystemManager* ptr)
{
	ptr->~ParticleSystemManager();
}
EXPORT void ParticleSystemManager_RemoveAndDestroyDanglingSceneNodes(ParticleUniverse::ParticleSystemManager* ptr, Ogre::SceneNode* sceneNode)
{
	ptr->removeAndDestroyDanglingSceneNodes(sceneNode);
}

EXPORT void ParticleSystemManager_DestroyAllParticleSystemTemplates(ParticleUniverse::ParticleSystemManager* ptr)
{
	ptr->destroyAllParticleSystemTemplates();
}

EXPORT ParticleUniverse::BoxSet* ParticleSystemManager_CreateBoxSet(ParticleUniverse::ParticleSystemManager* ptr,
																	char* name,
																	Ogre::SceneManager* sceneManager,
																	unsigned int poolSize = 20)
{
	return ptr->createBoxSet(name, sceneManager, poolSize);
}

EXPORT void ParticleSystemManager_DestroyBoxSet(ParticleUniverse::ParticleSystemManager* ptr,
												ParticleUniverse::BoxSet* boxSet, Ogre::SceneManager* sceneManager)
{
	ptr->destroyBoxSet(boxSet, sceneManager);
}

EXPORT ParticleUniverse::SphereSet* ParticleSystemManager_CreateSphereSet(ParticleUniverse::ParticleSystemManager* ptr,
																	char* name,
																	Ogre::SceneManager* sceneManager,
																	unsigned int poolSize = 20)
{
	return ptr->createSphereSet(name, sceneManager, poolSize);
}
	
EXPORT void ParticleSystemManager_DestroySphereSet(ParticleUniverse::ParticleSystemManager* ptr,
												   ParticleUniverse::SphereSet* sphereSet, Ogre::SceneManager* sceneManager)
{
	ptr->destroySphereSet(sphereSet, sceneManager);
}

EXPORT void ParticleSystemManager_RegisterAttachable(ParticleUniverse::ParticleSystemManager* ptr,
												ParticleUniverse::Attachable* attachable, Ogre::SceneManager* sceneManager)
{
	ptr->registerAttachable(attachable, sceneManager);
}

EXPORT void ParticleSystemManager_UnregisterAttachable(ParticleUniverse::ParticleSystemManager* ptr,
												ParticleUniverse::Attachable* attachable, Ogre::SceneManager* sceneManager)
{
	ptr->unregisterAttachable(attachable, sceneManager);
}

EXPORT void ParticleSystemManager_AddEmitterFactory(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::ParticleEmitterFactory* factory)
{
	ptr->addEmitterFactory(factory);
}

EXPORT ParticleUniverse::ParticleEmitterFactory* ParticleSystemManager_GetEmitterFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* emitterType)
{
	return ptr->getEmitterFactory(emitterType);
}

EXPORT void ParticleSystemManager_RemoveEmitterFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* emitterType)
{
	ptr->removeEmitterFactory(emitterType);
}

EXPORT void ParticleSystemManager_DestroyEmitterFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* emitterType)
{
	ptr->destroyEmitterFactory(emitterType);
}

EXPORT ParticleUniverse::ParticleEmitter* ParticleSystemManager_CreateEmitter(ParticleUniverse::ParticleSystemManager* ptr,
													char* emitterType)
{
	return ptr->createEmitter(emitterType);
}

EXPORT ParticleUniverse::ParticleEmitter* ParticleSystemManager_CloneEmitter(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::ParticleEmitter* emitter)
{
	return ptr->cloneEmitter(emitter);
}

EXPORT void ParticleSystemManager_DestroyEmitter(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->destroyEmitter(emitter);
}

EXPORT void ParticleSystemManager_AddAffectorFactory(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::ParticleAffectorFactory* factory)
{
	ptr->addAffectorFactory(factory);
}

EXPORT ParticleUniverse::ParticleAffectorFactory* ParticleSystemManager_GetAffectorFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* affectorType)
{
	return ptr->getAffectorFactory(affectorType);
}

EXPORT void ParticleSystemManager_RemoveAffectorFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* affectorType)
{
	ptr->removeAffectorFactory(affectorType);
}

EXPORT void ParticleSystemManager_DestroyAffectorFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* affectorType)
{
	ptr->destroyAffectorFactory(affectorType);
}

EXPORT ParticleUniverse::ParticleAffector* ParticleSystemManager_CreateAffector(ParticleUniverse::ParticleSystemManager* ptr,
													char* affectorType)
{
	return ptr->createAffector(affectorType);
}

EXPORT ParticleUniverse::ParticleAffector* ParticleSystemManager_CloneAffector(ParticleUniverse::ParticleSystemManager* ptr,
																			   ParticleUniverse::ParticleAffector* affector)
{
	return ptr->cloneAffector(affector);
}

EXPORT void ParticleSystemManager_DestroyAffector(ParticleUniverse::ParticleSystemManager* ptr,
																			   ParticleUniverse::ParticleAffector* affector)
{
	ptr->destroyAffector(affector);
}

EXPORT ParticleUniverse::ParticleTechnique* ParticleSystemManager_CreateTechnique(ParticleUniverse::ParticleSystemManager* ptr)
{
	return ptr->createTechnique();
}

EXPORT ParticleUniverse::ParticleTechnique* ParticleSystemManager_CloneTechnique(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleTechnique* technique)
{
	return ptr->cloneTechnique(technique);
}

EXPORT void ParticleSystemManager_DestroyTechnique(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleTechnique* technique)
{
	ptr->destroyTechnique(technique);
}

EXPORT void ParticleSystemManager_AddObserverFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleObserverFactory* factory)
{
	ptr->addObserverFactory(factory);
}

EXPORT ParticleUniverse::ParticleObserverFactory* ParticleSystemManager_GetObserverFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* observerType)
{
	return ptr->getObserverFactory(observerType);
}

EXPORT void ParticleSystemManager_RemoveObserverFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* observerType)
{
	ptr->removeObserverFactory(observerType);
}

EXPORT void ParticleSystemManager_DestroyObserverFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* observerType)
{
	ptr->destroyObserverFactory(observerType);
}

EXPORT ParticleUniverse::ParticleObserver*  ParticleSystemManager_CreateObserver(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* observerType)
{
	return ptr->createObserver(observerType);
}

EXPORT ParticleUniverse::ParticleObserver*  ParticleSystemManager_CloneObserver(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleObserver* observer)
{
	return ptr->cloneObserver(observer);
}

EXPORT void ParticleSystemManager_DestroyObserver(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleObserver* observer)
{
	ptr->destroyObserver(observer);
}

EXPORT void ParticleSystemManager_AddEventHandlerFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleEventHandlerFactory* factory)
{
	ptr->addEventHandlerFactory(factory);
}

EXPORT ParticleUniverse::ParticleEventHandlerFactory* ParticleSystemManager_GetEventHandlerFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* eventHandlerType)
{
	return ptr->getEventHandlerFactory(eventHandlerType);
}

EXPORT void ParticleSystemManager_RemoveEventHandlerFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* eventHandlerType)
{
	ptr->removeEventHandlerFactory(eventHandlerType);
}

EXPORT void ParticleSystemManager_DestroyEventHandlerFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* eventHandlerType)
{
	ptr->destroyEventHandlerFactory(eventHandlerType);
}

EXPORT ParticleUniverse::ParticleEventHandler* ParticleSystemManager_CreateEventHandler(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* eventHandlerType)
{
	return ptr->createEventHandler(eventHandlerType);
}

EXPORT ParticleUniverse::ParticleEventHandler* ParticleSystemManager_CloneEventHandler(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleEventHandler* eventHandler)
{
	return ptr->cloneEventHandler(eventHandler);
}

EXPORT void ParticleSystemManager_DestroyEventHandler(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->destroyEventHandler(eventHandler);
}

EXPORT void ParticleSystemManager_AddRendererFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleRendererFactory* factory)
{
	ptr->addRendererFactory(factory);
}

EXPORT ParticleUniverse::ParticleRendererFactory* ParticleSystemManager_GetRendererFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* rendererType)
{
	return ptr->getRendererFactory(rendererType);
}

EXPORT void ParticleSystemManager_RemoveRendererFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* rendererType)
{
	ptr->removeRendererFactory(rendererType);
}

EXPORT void ParticleSystemManager_DestroyRendererFactory(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* rendererType)
{
	ptr->destroyRendererFactory(rendererType);
}

EXPORT ParticleUniverse::ParticleRenderer* ParticleSystemManager_CreateRenderer(ParticleUniverse::ParticleSystemManager* ptr,
																				 char* rendererType)
{
	return ptr->createRenderer(rendererType);
}

EXPORT ParticleUniverse::ParticleRenderer* ParticleSystemManager_CloneRenderer(ParticleUniverse::ParticleSystemManager* ptr,
																				 ParticleUniverse::ParticleRenderer* renderer)
{
	return ptr->cloneRenderer(renderer);
}

EXPORT void ParticleSystemManager_DestroyRenderer(ParticleUniverse::ParticleSystemManager* ptr,
												ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->destroyRenderer(renderer);
}

EXPORT void ParticleSystemManager_AddExternFactory(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::ExternFactory* factory)
{
	ptr->addExternFactory(factory);
}

EXPORT ParticleUniverse::ExternFactory* ParticleSystemManager_GetExternFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* externType)
{
	return ptr->getExternFactory(externType);
}

EXPORT void ParticleSystemManager_RemoveExternFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* externType)
{
	ptr->removeExternFactory(externType);
}

EXPORT void ParticleSystemManager_DestroyExternFactory(ParticleUniverse::ParticleSystemManager* ptr,
													char* externType)
{
	ptr->destroyExternFactory(externType);
}

EXPORT ParticleUniverse::Extern* ParticleSystemManager_CreateExtern(ParticleUniverse::ParticleSystemManager* ptr,
													char* externType)
{
	return ptr->createExtern(externType);
}

EXPORT ParticleUniverse::Extern* ParticleSystemManager_CloneExtern(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::Extern* externObject)
{
	return ptr->cloneExtern(externObject);
}

EXPORT void ParticleSystemManager_DestroyExtern(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::Extern* externObject)
{
	ptr->destroyExtern(externObject);
}

EXPORT void ParticleSystemManager_AddBehaviourFactory(ParticleUniverse::ParticleSystemManager* ptr,
													ParticleUniverse::ParticleBehaviourFactory* factory)
{
	ptr->addBehaviourFactory(factory);
}

EXPORT ParticleUniverse::ParticleBehaviourFactory* ParticleSystemManager_GetBehaviourFactory(ParticleUniverse::ParticleSystemManager* ptr,
												char* behaviourType	)
{
	return ptr->getBehaviourFactory(behaviourType);
}

EXPORT void ParticleSystemManager_RemoveBehaviourFactory(ParticleUniverse::ParticleSystemManager* ptr,
												char* behaviourType	)
{
	ptr->removeBehaviourFactory(behaviourType);
}

EXPORT void ParticleSystemManager_DestroyBehaviourFactory(ParticleUniverse::ParticleSystemManager* ptr,
												char* behaviourType	)
{
	ptr->destroyBehaviourFactory(behaviourType);
}

EXPORT ParticleUniverse::ParticleBehaviour* ParticleSystemManager_CreateBehaviourFactory(ParticleUniverse::ParticleSystemManager* ptr,
												char* behaviourType	)
{
	return ptr->createBehaviour(behaviourType);
}

EXPORT ParticleUniverse::ParticleBehaviour* ParticleSystemManager_CloneBehaviour(ParticleUniverse::ParticleSystemManager* ptr,
												ParticleUniverse::ParticleBehaviour* behaviour	)
{
	return ptr->cloneBehaviour(behaviour);
}

EXPORT void ParticleSystemManager_DestroyBehaviour(ParticleUniverse::ParticleSystemManager* ptr,
												ParticleUniverse::ParticleBehaviour* behaviour	)
{
	ptr->destroyBehaviour(behaviour);
}

EXPORT ParticleUniverse::ParticleSystem* ParticleSystemManager_CreateParticleSystemTemplate(ParticleUniverse::ParticleSystemManager* ptr,
												char* name,
												char* resourceGroupName)
{
	return ptr->createParticleSystemTemplate(name, resourceGroupName);
}

EXPORT void ParticleSystemManager_ReplaceParticleSystemTemplate(ParticleUniverse::ParticleSystemManager* ptr,
												char* name,
												ParticleUniverse::ParticleSystem* system)
{
	ptr->replaceParticleSystemTemplate(name, system);
}

EXPORT const char* ParticleSystemManager_GetLastCreatedParticleSystemTemplateName(ParticleUniverse::ParticleSystemManager* ptr)
{
	Ogre::String strToReturn = ptr->getLastCreatedParticleSystemTemplateName();
	
	return strToReturn.c_str();
}

EXPORT void ParticleSystemManager_AddParticleSystemTemplate(ParticleUniverse::ParticleSystemManager* ptr,
															char* name,
															ParticleUniverse::ParticleSystem* systemTemplate)
{
	ptr->addParticleSystemTemplate(name, systemTemplate);
}

EXPORT ParticleUniverse::ParticleSystem* ParticleSystemManager_GetParticleSystemTemplate(ParticleUniverse::ParticleSystemManager* ptr,
															char* templateName)
{
	return ptr->getParticleSystemTemplate(templateName);
}

EXPORT void ParticleSystemManager_DestroyParticleSystemTemplate(ParticleUniverse::ParticleSystemManager* ptr,
															char* templateName)
{
	ptr->destroyParticleSystemTemplate(templateName);
}

EXPORT int ParticleSystemManager_ParticleSystemTemplateNamesSize(ParticleUniverse::ParticleSystemManager* ptr)
{
	ParticleUniverse::vector<Ogre::String> lines;
	ptr->particleSystemTemplateNames(lines);

	int templateMemSize = 0;

	ParticleUniverse::vector<Ogre::String>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it, i++)
    {
      Ogre::String name = *it;
	  templateMemSize += sizeof(name);
	}
	return templateMemSize;
}

EXPORT void ParticleSystemManager_ParticleSystemTemplateNames(ParticleUniverse::ParticleSystemManager* ptr,
															   char* arrTemplateNames, int bufSize)
{
	ParticleUniverse::vector<Ogre::String> lines;
	ptr->particleSystemTemplateNames(lines);
	if (sizeof(lines) > bufSize)
		return;

	ParticleUniverse::vector<Ogre::String>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      Ogre::String name = *it;
	  strcpy((arrTemplateNames + i), (char*)name.c_str());
		i += sizeof(name);
    }
}

EXPORT ParticleUniverse::ParticleSystem* ParticleSystemManager_CreateParticleSystem(ParticleUniverse::ParticleSystemManager* ptr, char* name,
		char* templateName,
		Ogre::SceneManager* sceneManager)
{
	return ptr->createParticleSystem(name, templateName, sceneManager);
}

EXPORT ParticleUniverse::ParticleSystem* ParticleSystemManager_CreateParticleSystem2(ParticleUniverse::ParticleSystemManager* ptr, char* name, 
				Ogre::SceneManager* sceneManager)
{
	return ptr->createParticleSystem(name, sceneManager);
}

EXPORT ParticleUniverse::ParticleSystem* ParticleSystemManager_GetParticleSystem(ParticleUniverse::ParticleSystemManager* ptr, const Ogre::String name)
{
	return ptr->getParticleSystem(name);
}

EXPORT void ParticleSystemManager_DestroyParticleSystem(ParticleUniverse::ParticleSystemManager* ptr, 
														ParticleUniverse::ParticleSystem* particleSystem, 
														Ogre::SceneManager* sceneManager)
{
	ptr->destroyParticleSystem(particleSystem, sceneManager);
}

EXPORT void ParticleSystemManager_DestroyParticleSystemByName(ParticleUniverse::ParticleSystemManager* ptr, 
														char* particleSystemName, 
														Ogre::SceneManager* sceneManager)
{
	ptr->destroyParticleSystem(particleSystemName, sceneManager);
}

EXPORT void ParticleSystemManager_DestroyAllParticleSystems(ParticleUniverse::ParticleSystemManager* ptr, 
														Ogre::SceneManager* sceneManager)
{
	ptr->destroyAllParticleSystems(sceneManager);
}

EXPORT void ParticleSystemManager_AddAlias(ParticleUniverse::ParticleSystemManager* ptr, 
														ParticleUniverse::IAlias* alias)
{
	ptr->addAlias(alias);
}

EXPORT ParticleUniverse::IAlias* ParticleSystemManager_GetAlias(ParticleUniverse::ParticleSystemManager* ptr, 
														char* aliasName)
{
	return ptr->getAlias(aliasName);
}

EXPORT void ParticleSystemManager_DestroyAlias(ParticleUniverse::ParticleSystemManager* ptr, 
														ParticleUniverse::IAlias* alias)
{
	ptr->destroyAlias(alias);
}

EXPORT void ParticleSystemManager_DestroyAllAliasses(ParticleUniverse::ParticleSystemManager* ptr)
{
	ptr->destroyAllAliasses();
}

//EXPORT ParticleUniverse::ParticleSystemManager::AliasMap* ParticleSystemManager__GetAliasMap(ParticleUniverse::ParticleSystemManager* ptr)
//{
//	return ptr->_getAliasMap();
//}

EXPORT ParticleUniverse::ParticleSystemManager* ParticleSystemManager_Singleton()
{
	return ParticleUniverse::ParticleSystemManager::getSingleton().getSingletonPtr();
}

EXPORT ParticleUniverse::ParticleSystemManager* ParticleSystemManager_SingletonPtr()
{
	return ParticleUniverse::ParticleSystemManager::getSingleton().getSingletonPtr();
}

EXPORT void ParticleSystemManager_WriteScript(ParticleUniverse::ParticleSystemManager* ptr, 
											  ParticleUniverse::ParticleSystem* particleSystem, 
														char* fileName)
{
	ptr->writeScript(particleSystem, fileName);
}

EXPORT const char* ParticleSystemManager_WriteScript2(ParticleUniverse::ParticleSystemManager* ptr, 
											  ParticleUniverse::ParticleSystem* particleSystem)
{
	Ogre::String strToReturn = ptr->writeScript(particleSystem);
	return strToReturn.c_str();
}

EXPORT ParticleUniverse::ParticleScriptSerializer* ParticleSystemManager_GetParticleScriptSerializer(ParticleUniverse::ParticleSystemManager* ptr)
{
	return ptr->getParticleScriptSerializer();
}

EXPORT void ParticleSystemManager_Write(ParticleUniverse::ParticleSystemManager* ptr,
										ParticleUniverse::ParticleScriptSerializer* serializer, 
										ParticleUniverse::IElement* element)
{
	ptr->write(serializer, element);
}

EXPORT ParticleUniverse::CameraDependency* ParticleSystemManager_CreateCameraDependency(ParticleUniverse::ParticleSystemManager* ptr)
{
	return ptr->createCameraDependency();
}

EXPORT void ParticleSystemManager_CreateDepthMap(ParticleUniverse::ParticleSystemManager* ptr,
												 Ogre::Camera* camera, Ogre::SceneManager* sceneManager)
{
	ptr->createDepthMap(camera, sceneManager);
}

EXPORT void ParticleSystemManager_DestroyDepthMap(ParticleUniverse::ParticleSystemManager* ptr)
{
	ptr->destroyDepthMap();
}

EXPORT bool ParticleSystemManager_NotifyDepthMapNeeded(ParticleUniverse::ParticleSystemManager* ptr,
													   Ogre::Camera* camera, Ogre::SceneManager* sceneManager)
{
	return ptr->notifyDepthMapNeeded(camera, sceneManager);
}

EXPORT void ParticleSystemManager_RegisterSoftParticlesRenderer(ParticleUniverse::ParticleSystemManager* ptr,
													   ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->registerSoftParticlesRenderer(renderer);
}

EXPORT void ParticleSystemManager_UnregisterSoftParticlesRenderer(ParticleUniverse::ParticleSystemManager* ptr,
													   ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->unregisterSoftParticlesRenderer(renderer);
}

EXPORT Ogre::Real ParticleSystemManager_GetDepthScale(ParticleUniverse::ParticleSystemManager* ptr)
{
	return ptr->getDepthScale();
}

EXPORT void ParticleSystemManager_SetDepthScale(ParticleUniverse::ParticleSystemManager* ptr,
												Ogre::Real depthScale)
{
	ptr->setDepthScale(depthScale);
}

EXPORT const char* ParticleSystemManager_GetDepthTextureName(ParticleUniverse::ParticleSystemManager* ptr)
{
	Ogre::String toReturn = ptr->getDepthTextureName();
	return toReturn.c_str();
}

EXPORT void ParticleSystemManager_SetExternDepthTextureName(ParticleUniverse::ParticleSystemManager* ptr,
												char* depthTextureName)
{
	ptr->setExternDepthTextureName(depthTextureName);
}

EXPORT void ParticleSystemManager_ResetExternDepthTextureName(ParticleUniverse::ParticleSystemManager* ptr)
{
	ptr->resetExternDepthTextureName();
}

EXPORT ParticleUniverse::DynamicAttribute* ParticleSystemManager_CreateDynamicAttribute(ParticleUniverse::ParticleSystemManager* ptr,
																	  ParticleUniverse::DynamicAttribute::DynamicAttributeType* type)
{
	return ptr->createDynamicAttribute(*type);
}

EXPORT bool ParticleSystemManager_IsAutoLoadMaterials(ParticleUniverse::ParticleSystemManager* ptr)
{
	return ptr->isAutoLoadMaterials();
}

EXPORT void ParticleSystemManager_SetAutoLoadMaterials(ParticleUniverse::ParticleSystemManager* ptr,
													   bool autoLoadMaterials)
{
	ptr->setAutoLoadMaterials(autoLoadMaterials);
}

#pragma endregion
#pragma region PhysicsActor
EXPORT ParticleUniverse::PhysicsActor* PhysicsActor_New()
{
	return new ParticleUniverse::PhysicsActor();
}
EXPORT void PhysicsActor_Destroy(ParticleUniverse::PhysicsActor* ptr)
{
	ptr->~PhysicsActor();
}
EXPORT ParticleUniverse::Vector3* PhysicsActor_GetPosition(ParticleUniverse::PhysicsActor* ptr)
{
	return &ptr->position;
}
EXPORT void PhysicsActor_SetPosition(ParticleUniverse::PhysicsActor* ptr, ParticleUniverse::Vector3* newVal)
{
	ptr->position = *newVal;
}
EXPORT ParticleUniverse::Vector3* PhysicsActor_GetDirection(ParticleUniverse::PhysicsActor* ptr)
{
	return &ptr->direction;
}
EXPORT void PhysicsActor_SetDirection(ParticleUniverse::PhysicsActor* ptr, ParticleUniverse::Vector3*  newVal)
{
	ptr->direction = *newVal;
}
EXPORT ParticleUniverse::Quaternion* PhysicsActor_GetOrientation(ParticleUniverse::PhysicsActor* ptr)
{
	return &ptr->orientation;
}
EXPORT void PhysicsActor_SetOrientation(ParticleUniverse::PhysicsActor* ptr, ParticleUniverse::Quaternion*  newVal)
{
	ptr->orientation = *newVal;
}
EXPORT float PhysicsActor_GetMass(ParticleUniverse::PhysicsActor* ptr)
{
	return ptr->mass;
}
EXPORT void PhysicsActor_SetMass(ParticleUniverse::PhysicsActor* ptr, float newVal)
{
	ptr->mass = newVal;
}
EXPORT unsigned short PhysicsActor_GetCollisionGroup(ParticleUniverse::PhysicsActor* ptr)
{
	return ptr->collisionGroup;
}
EXPORT void PhysicsActor_SetCollisionGroup(ParticleUniverse::PhysicsActor* ptr, unsigned short newVal)
{
	ptr->collisionGroup = newVal;
}
EXPORT ParticleUniverse::Vector3* PhysicsActor_GetContactPoint(ParticleUniverse::PhysicsActor* ptr)
{
	return &ptr->contactPoint;
}
EXPORT void PhysicsActor_SetContactPoint(ParticleUniverse::PhysicsActor* ptr, ParticleUniverse::Vector3* newVal)
{
	ptr->contactPoint = *newVal;
}
EXPORT ParticleUniverse::Vector3* PhysicsActor_GetContactNormal(ParticleUniverse::PhysicsActor* ptr)
{
	return &ptr->contactNormal;
}
EXPORT void PhysicsActor_SetContactNormal(ParticleUniverse::PhysicsActor* ptr, ParticleUniverse::Vector3* newVal)
{
	ptr->contactNormal = *newVal;
}
#pragma endregion
#pragma region IVisualData
EXPORT void IVisualData_SetVisible (ParticleUniverse::IVisualData* ptr, bool isVisible)
{
	ptr->setVisible(isVisible);
}
#pragma endregion

#pragma region Extern Exports

EXPORT void Extern_Destroy(ParticleUniverse::Extern* ptr)
{
	ptr->~Extern();
}

EXPORT const char* Extern_GetName(ParticleUniverse::Extern* ptr)
{
	return ptr->getName().c_str();
}

EXPORT void Extern_SetName(ParticleUniverse::Extern* ptr, const char*  name)
{
	ptr->setName(name);
}

EXPORT const char* Extern_GetExternType(ParticleUniverse::Extern* ptr)
{
	return ptr->getExternType().c_str();
}
EXPORT void Extern_SetExternType(ParticleUniverse::Extern* ptr, const char*  externType)
{
	ptr->setExternType(externType);
}

EXPORT ParticleUniverse::ParticleTechnique* Extern_GetParentTechnique(ParticleUniverse::Extern* ptr) 
{
	return ptr->getParentTechnique();
}
EXPORT void Extern_SetParentTechnique(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* parentTechnique) 
{
	ptr->setParentTechnique(parentTechnique);
}

EXPORT void Extern__notifyRescaled(ParticleUniverse::Extern* ptr, const Ogre::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}

EXPORT void Extern_CopyAttributesTo (ParticleUniverse::Extern* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->copyAttributesTo(externObject);
}

EXPORT void Extern_CopyParentAttributesTo (ParticleUniverse::Extern* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->copyParentAttributesTo(externObject);
}

EXPORT void Extern__prepare(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}

EXPORT void Extern__unprepare(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}

EXPORT void Extern__notifyStart (ParticleUniverse::Extern* ptr)
{
	ptr->_notifyStart();
}

EXPORT void Extern__notifyPause (ParticleUniverse::Extern* ptr)
{
	ptr->_notifyPause();
}

EXPORT void Extern__notifyResume (ParticleUniverse::Extern* ptr)
{
	ptr->_notifyResume();
}

EXPORT void Extern__notifyStop (ParticleUniverse::Extern* ptr)
{
	ptr->_notifyStop();
}

EXPORT void Extern__preProcessParticles(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}

EXPORT void Extern__initParticleForEmission(ParticleUniverse::Extern* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}

EXPORT void Extern__initParticleForExpiration(ParticleUniverse::Extern* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForExpiration(particle);
}

EXPORT void Extern__firstParticle(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, 
				ParticleUniverse::Particle* particle, 
				float timeElapsed)
{
	ptr->_firstParticle(particleTechnique, particle, timeElapsed);
}

EXPORT void Extern__processParticle(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed, bool firstParticle)
{
	ptr->_processParticle(technique, particle, timeElapsed, firstParticle);
}

EXPORT void Extern__interface(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* technique, 
				ParticleUniverse::Particle* particle, 
				float timeElapsed)
{
	ptr->_interface(technique, particle, timeElapsed);
}

EXPORT void Extern__postProcessParticles(ParticleUniverse::Extern* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_postProcessParticles(technique, timeElapsed);
}

#pragma endregion
#pragma region ExternFactory Exports
EXPORT void ExternFactory_Destroy(ParticleUniverse::ExternFactory* ptr)
{
	ptr->~ExternFactory();
}

EXPORT const char* ExternFactory_GetExternType(ParticleUniverse::ExternFactory* ptr)
{
	return ptr->getExternType().c_str();
}
EXPORT ParticleUniverse::Extern* ExternFactory_CreateExtern(ParticleUniverse::ExternFactory* ptr)
{
	return ptr->createExtern();
}
EXPORT void ExternFactory_DestroyExtern (ParticleUniverse::ExternFactory* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->destroyExtern(externObject);
}

#pragma endregion
#pragma region VortexExtern Exports
EXPORT ParticleUniverse::VortexExtern* VortexExtern_New()
{
	ParticleUniverse::VortexExtern* pextern = new ParticleUniverse::VortexExtern();
	pextern->setExternType("Vortex");
	return pextern;
}
EXPORT void VortexExtern_Destroy(ParticleUniverse::VortexExtern* ptr)
{
	ptr->~VortexExtern();
}
EXPORT void VortexExtern__preProcessParticles(ParticleUniverse::VortexExtern* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void VortexExtern__interface(ParticleUniverse::VortexExtern* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_interface(technique, particle, timeElapsed);
}
EXPORT void VortexExtern_CopyAttributesTo(ParticleUniverse::VortexExtern* ptr, ParticleUniverse::Extern* externCopy)
{
	ptr->copyAttributesTo(externCopy);
}
#pragma endregion
#pragma region SphereColliderExtern
EXPORT ParticleUniverse::SphereColliderExtern* SphereColliderExtern_New()
{
	ParticleUniverse::SphereColliderExtern* pextern = new ParticleUniverse::SphereColliderExtern();
	pextern->setExternType("SphereCollider");
	return pextern;
}
EXPORT void SphereColliderExtern_Destroy(ParticleUniverse::SphereColliderExtern* ptr)
{
	ptr->~SphereColliderExtern();
}
EXPORT void SphereColliderExtern__preProcessParticles(ParticleUniverse::SphereColliderExtern* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void SphereColliderExtern__interface(ParticleUniverse::SphereColliderExtern* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_interface(technique, particle, timeElapsed);
}
EXPORT void SphereColliderExtern_CopyAttributesTo (ParticleUniverse::SphereColliderExtern* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->copyAttributesTo(externObject);
}
#pragma endregion
#pragma region BoxColliderExtern
EXPORT ParticleUniverse::BoxColliderExtern* BoxColliderExtern_New()
{
	ParticleUniverse::BoxColliderExtern* pextern = new ParticleUniverse::BoxColliderExtern();
	pextern->setExternType("BoxCollider");
	return pextern;
}
EXPORT void BoxColliderExtern_Destroy(ParticleUniverse::BoxColliderExtern* ptr)
{
	ptr->~BoxColliderExtern();
}
EXPORT void BoxColliderExtern__preProcessParticles(ParticleUniverse::BoxColliderExtern* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void BoxColliderExtern__interface(ParticleUniverse::BoxColliderExtern* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_interface(technique, particle, timeElapsed);
}
EXPORT void BoxColliderExtern_CopyAttributesTo (ParticleUniverse::BoxColliderExtern* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->copyAttributesTo(externObject);
}
#pragma endregion
#pragma region GravityExtern
EXPORT ParticleUniverse::GravityExtern* GravityExtern_New()
{
	ParticleUniverse::GravityExtern* pextern = new ParticleUniverse::GravityExtern();
	pextern->setExternType("Gravity");
	return pextern;
}
EXPORT void GravityExtern_Destroy(ParticleUniverse::GravityExtern* ptr)
{
	ptr->~GravityExtern();
}
EXPORT void GravityExtern__preProcessParticles(ParticleUniverse::GravityExtern* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void GravityExtern__interface(ParticleUniverse::GravityExtern* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_interface(technique, particle, timeElapsed);
}
EXPORT void GravityExtern_CopyAttributesTo (ParticleUniverse::GravityExtern* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->copyAttributesTo(externObject);
}
#pragma endregion
#pragma region SceneDecoratorExtern
EXPORT ParticleUniverse::SceneDecoratorExtern* SceneDecoratorExtern_New()
{
	ParticleUniverse::SceneDecoratorExtern* pextern = new ParticleUniverse::SceneDecoratorExtern();
	pextern->setExternType("SceneDecorator");
	return pextern;
}
EXPORT void SceneDecoratorExtern_Destroy(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	ptr->~SceneDecoratorExtern();
}
EXPORT void SceneDecoratorExtern__prepare(ParticleUniverse::SceneDecoratorExtern* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void SceneDecoratorExtern__unprepare(ParticleUniverse::SceneDecoratorExtern* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void SceneDecoratorExtern_CopyAttributesTo (ParticleUniverse::SceneDecoratorExtern* ptr, ParticleUniverse::Extern* externObject)
{
	ptr->copyAttributesTo(externObject);
}
EXPORT void SceneDecoratorExtern__interface(ParticleUniverse::SceneDecoratorExtern* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_interface(technique, particle, timeElapsed);
}
EXPORT void SceneDecoratorExtern_CreateEntity(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	ptr->createEntity();
}
EXPORT void SceneDecoratorExtern_DestroyEntity(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	ptr->destroyEntity();
}
EXPORT const char* SceneDecoratorExtern_GetMeshName(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	return ptr->getMeshName().c_str();
}
EXPORT void SceneDecoratorExtern_SetMeshName(ParticleUniverse::SceneDecoratorExtern* ptr, const char* meshName)
{
	ptr->setMeshName(meshName);
}
EXPORT const char* SceneDecoratorExtern_GetMaterialName(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	return ptr->getMaterialName().c_str();
}
EXPORT void SceneDecoratorExtern_SetMaterialName(ParticleUniverse::SceneDecoratorExtern* ptr, const char* materialName)
{
	ptr->setMaterialName(materialName);
}
EXPORT const Ogre::Vector3* SceneDecoratorExtern_GetScale(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	return &ptr->getScale();
}
EXPORT void SceneDecoratorExtern_SetScale(ParticleUniverse::SceneDecoratorExtern* ptr, Ogre::Vector3* scale)
{
	ptr->setScale(*scale);
}
EXPORT const Ogre::Vector3* SceneDecoratorExtern_GetPosition(ParticleUniverse::SceneDecoratorExtern* ptr)
{
	return &ptr->getPosition();
}
EXPORT void SceneDecoratorExtern_SetPosition(ParticleUniverse::SceneDecoratorExtern* ptr, Ogre::Vector3* position)
{
	ptr->setPosition(*position);
}
EXPORT void SceneDecoratorExtern__notifyStart (ParticleUniverse::SceneDecoratorExtern* ptr)
{
	ptr->_notifyStart();
}
EXPORT void SceneDecoratorExtern__notifyStop (ParticleUniverse::SceneDecoratorExtern* ptr)
{
	ptr->_notifyStop();
}
#pragma endregion

#pragma region ParticleAffector Exports
//EXPORT ParticleUniverse::ParticleAffector* ParticleAffector_New(void)
//{
//	return new ParticleUniverse::ParticleAffector();
//}
EXPORT void ParticleAffector_Destroy(ParticleUniverse::ParticleAffector* ptr)
{
	ptr->~ParticleAffector();
}
EXPORT const ParticleUniverse::ParticleAffector::AffectSpecialisation* ParticleAffector_GetAffectSpecialisation(ParticleUniverse::ParticleAffector* ptr)
{
	return &ptr->getAffectSpecialisation();
}
EXPORT void ParticleAffector_SetAffectSpecialisation(ParticleUniverse::ParticleAffector* ptr, const ParticleUniverse::ParticleAffector::AffectSpecialisation* affectSpecialisation)
{
	ptr->setAffectSpecialisation(*affectSpecialisation);
}
EXPORT const char* ParticleAffector_GetAffectorType(ParticleUniverse::ParticleAffector* ptr)
{
	return ptr->getAffectorType().c_str();
}
EXPORT void ParticleAffector_SetAffectorType(ParticleUniverse::ParticleAffector* ptr, char* affectorType)
{
	ptr->setAffectorType(affectorType);
}
EXPORT const char* ParticleAffector_GetName(ParticleUniverse::ParticleAffector* ptr)
{
	return ptr->getName().c_str();
}
EXPORT void ParticleAffector_SetName(ParticleUniverse::ParticleAffector* ptr, char* name)
{
	ptr->setName(name);
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleAffector_GetParentTechnique(ParticleUniverse::ParticleAffector* ptr)
{
	return ptr->getParentTechnique();
}
EXPORT void ParticleAffector_SetParentTechnique(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* parentTechnique)
{
	ptr->setParentTechnique(parentTechnique);
}
EXPORT void ParticleAffector__prepare(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void ParticleAffector__unprepare(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void ParticleAffector__notifyStart (ParticleUniverse::ParticleAffector* ptr)
{
	ptr->_notifyStart();
}
EXPORT void ParticleAffector__notifyStop (ParticleUniverse::ParticleAffector* ptr)
{
	ptr->_notifyStop();
}
EXPORT void ParticleAffector__notifyPause (ParticleUniverse::ParticleAffector* ptr)
{
	ptr->_notifyPause();
}
EXPORT void ParticleAffector__notifyResume (ParticleUniverse::ParticleAffector* ptr)
{
	ptr->_notifyResume();
}
EXPORT void ParticleAffector__notifyRescaled(ParticleUniverse::ParticleAffector* ptr, Ogre::Vector3 scale)
{
	ptr->_notifyRescaled(scale);
}
EXPORT void ParticleAffector__preProcessParticles(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void ParticleAffector__firstParticle(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_firstParticle(particleTechnique, particle, timeElapsed);
}
EXPORT void ParticleAffector__initForEmission(ParticleUniverse::ParticleAffector* ptr)
{
	ptr->_initForEmission();
}
EXPORT void ParticleAffector__initForExpiration(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_initForExpiration(technique, timeElapsed);
}
EXPORT void ParticleAffector__initParticleForEmission(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}
EXPORT void ParticleAffector__processParticle(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed, bool firstParticle)
{
	ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT void ParticleAffector__postProcessParticles(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_postProcessParticles(technique, timeElapsed);
}
EXPORT void ParticleAffector__affect(ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void ParticleAffector_AddEmitterToExclude(ParticleUniverse::ParticleAffector* ptr, char* emitterName)
{
	ptr->addEmitterToExclude(emitterName);
}
EXPORT void ParticleAffector_RemoveEmitterToExclude(ParticleUniverse::ParticleAffector* ptr, char* emitterName)
{
	ptr->removeEmitterToExclude(emitterName);
}
EXPORT void ParticleAffector_RemoveAllEmittersToExclude(ParticleUniverse::ParticleAffector* ptr)
{
	ptr->removeAllEmittersToExclude();
}

EXPORT int ParticleAffector_GetEmittersToExcludeSize(ParticleUniverse::ParticleAffector* ptr)
{
	ParticleUniverse::list<std::string> lines = ptr->getEmittersToExclude();

	int templateMemSize = 0;

	ParticleUniverse::list<std::string>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it, i++)
    {
      Ogre::String name = *it;
	  templateMemSize += sizeof(name);
	}
	return templateMemSize;
}


EXPORT void ParticleAffector_GetEmittersToExclude(ParticleUniverse::ParticleAffector* ptr, char* arrTemplateNames, int bufSize)
{
	ParticleUniverse::list<std::string> lines = ptr->getEmittersToExclude();

		if (sizeof(lines) > bufSize)
		return;

	ParticleUniverse::list<std::string>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      Ogre::String name = *it;
	  strcpy((arrTemplateNames + i), (char*)name.c_str());
		i += sizeof(name);
    }
}
EXPORT bool ParticleAffector_HasEmitterToExclude(ParticleUniverse::ParticleAffector* ptr, char* emitterName)
{
	return ptr->hasEmitterToExclude(emitterName);
}
EXPORT void ParticleAffector_CopyAttributesTo (ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void ParticleAffector_CopyParentAttributesTo (ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyParentAttributesTo(affector);
}
EXPORT const ParticleUniverse::Vector3* ParticleAffector_GetDerivedPosition(ParticleUniverse::ParticleAffector* ptr)
{
	return &ptr->getDerivedPosition();
}
EXPORT float ParticleAffector__calculateAffectSpecialisationFactor (ParticleUniverse::ParticleAffector* ptr, ParticleUniverse::Particle* particle)
{
	return ptr->_calculateAffectSpecialisationFactor(particle);
}
#pragma endregion
#pragma region AlignAffector
EXPORT ParticleUniverse::AlignAffector* AlignAffector_New(void)
{
	ParticleUniverse::AlignAffector* affector = new ParticleUniverse::AlignAffector();
	affector->setAffectorType("Align");
	return affector;
}

EXPORT void AlignAffector_Destroy(ParticleUniverse::AlignAffector* ptr)
{
	ptr->~AlignAffector();
}

EXPORT void AlignAffector_CopyAttributesTo (ParticleUniverse::AlignAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}

EXPORT bool AlignAffector_IsResize(ParticleUniverse::AlignAffector* ptr)
{
	return ptr->isResize();
}

EXPORT void AlignAffector_SetResize(ParticleUniverse::AlignAffector* ptr, bool resize)
{
	ptr->setResize(resize);
}

EXPORT void AlignAffector__firstParticle(ParticleUniverse::AlignAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, 
				ParticleUniverse::Particle* particle, 
				float timeElapsed)
{
	ptr->_firstParticle(particleTechnique, particle, timeElapsed);
}

EXPORT void AlignAffector__affect(ParticleUniverse::AlignAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}

#pragma endregion
#pragma region BaseCollider Exports
EXPORT void BaseCollider_Destroy(ParticleUniverse::BaseCollider* ptr)
{
	ptr->~BaseCollider();
}
EXPORT void BaseCollider__preProcessParticles(ParticleUniverse::BaseCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT const ParticleUniverse::BaseCollider::IntersectionType BaseCollider_GetIntersectionType(ParticleUniverse::BaseCollider* ptr)
{
	return ptr->getIntersectionType();
}
EXPORT void BaseCollider_SetIntersectionType(ParticleUniverse::BaseCollider* ptr, ParticleUniverse::BaseCollider::IntersectionType* intersectionType)
{
	ptr->setIntersectionType(*intersectionType);
}
EXPORT const ParticleUniverse::BaseCollider::CollisionType BaseCollider_GetCollisionType(ParticleUniverse::BaseCollider* ptr)
{
	return ptr->getCollisionType();
}
EXPORT void BaseCollider_SetCollisionType(ParticleUniverse::BaseCollider* ptr, ParticleUniverse::BaseCollider::CollisionType* collisionType)
{
	ptr->setCollisionType(*collisionType);
}
EXPORT float BaseCollider_GetFriction(ParticleUniverse::BaseCollider* ptr)
{
	return ptr->getFriction();
}
EXPORT void BaseCollider_SetFriction(ParticleUniverse::BaseCollider* ptr, float friction)
{
	ptr->setFriction(friction);
}
EXPORT float BaseCollider_GetBouncyness(ParticleUniverse::BaseCollider* ptr)
{
	return ptr->getBouncyness();
}
EXPORT void BaseCollider_SetBouncyness(ParticleUniverse::BaseCollider* ptr, float bouncyness)
{
	ptr->setBouncyness(bouncyness);
}
EXPORT void BaseCollider_PopulateAlignedBox(ParticleUniverse::BaseCollider* ptr, Ogre::AxisAlignedBox* box,
				Ogre::Vector3* position, 
				float width,
				float height,
				float depth)
{
	ptr->populateAlignedBox(*box, *position, width, height, depth);
}
EXPORT void BaseCollider_CalculateRotationSpeedAfterCollision(ParticleUniverse::BaseCollider* ptr, ParticleUniverse::Particle* particle)
{
	ptr->calculateRotationSpeedAfterCollision(particle);
}
EXPORT void BaseCollider_CopyAttributesTo (ParticleUniverse::BaseCollider* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion
#pragma region BaseForceAffector Exports
EXPORT void BaseForceAffector_Destroy(ParticleUniverse::BaseForceAffector* ptr)
{
	ptr->~BaseForceAffector();
}
EXPORT void BaseForceAffector_CopyAttributesTo (ParticleUniverse::BaseForceAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT const Ogre::Vector3* BaseForceAffector_GetForceVector(ParticleUniverse::BaseForceAffector* ptr)
{
	return &ptr->getForceVector();
}
EXPORT void BaseForceAffector_SetForceVector(ParticleUniverse::BaseForceAffector* ptr, const Ogre::Vector3* forceVector)
{
	ptr->setForceVector(*forceVector);
}
EXPORT ParticleUniverse::BaseForceAffector::ForceApplication BaseForceAffector_GetForceApplication(ParticleUniverse::BaseForceAffector* ptr)
{
	return ptr->getForceApplication();
}
EXPORT void BaseForceAffector_SetForceApplication(ParticleUniverse::BaseForceAffector* ptr, ParticleUniverse::BaseForceAffector::ForceApplication* forceApplication)
{
	ptr->setForceApplication(*forceApplication);
}
#pragma endregion
#pragma region BoxCollider Exports
EXPORT ParticleUniverse::BoxCollider* BoxCollider_New(void)
{
	ParticleUniverse::BoxCollider* affector = new ParticleUniverse::BoxCollider();
	affector->setAffectorType("Box");
	return affector;
}
EXPORT void BoxCollider_Destroy(ParticleUniverse::BoxCollider* ptr)
{
	ptr->~BoxCollider();
}
EXPORT float BoxCollider_GetWidth(ParticleUniverse::BoxCollider* ptr)
{
	return ptr->getWidth();
}
EXPORT void BoxCollider_SetWidth(ParticleUniverse::BoxCollider* ptr, const float width)
{
	ptr->setWidth(width);
}
EXPORT float BoxCollider_GetHeight(ParticleUniverse::BoxCollider* ptr)
{
	return ptr->getHeight();
}
EXPORT void BoxCollider_SetHeight(ParticleUniverse::BoxCollider* ptr, const float height)
{
	return ptr->setHeight(height);
}
EXPORT float BoxCollider_GetDepth(ParticleUniverse::BoxCollider* ptr)
{
	return ptr->getDepth();
}
EXPORT void BoxCollider_SetDepth(ParticleUniverse::BoxCollider* ptr, const float depth)
{
	ptr->setDepth(depth);
}
EXPORT bool BoxCollider_IsInnerCollision(ParticleUniverse::BoxCollider* ptr)
{
	return ptr->isInnerCollision();
}
EXPORT void BoxCollider_SetInnerCollision(ParticleUniverse::BoxCollider* ptr, bool innerCollision)
{
	ptr->setInnerCollision(innerCollision);
}
EXPORT void BoxCollider_CalculateDirectionAfterCollision(ParticleUniverse::BoxCollider* ptr, ParticleUniverse::Particle* particle)
{
	ptr->calculateDirectionAfterCollision(particle);
}
EXPORT void BoxCollider__preProcessParticles(ParticleUniverse::BoxCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void BoxCollider__affect(ParticleUniverse::BoxCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void BoxCollider_CopyAttributesTo (ParticleUniverse::BoxCollider* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo (affector);
}
#pragma endregion
#pragma region CollisionAvoidance Exports
EXPORT ParticleUniverse::CollisionAvoidanceAffector* CollisionAvoidanceAffector_New(void)
{
	ParticleUniverse::CollisionAvoidanceAffector* affector = new ParticleUniverse::CollisionAvoidanceAffector();
	affector->setAffectorType("CollisionAvoidance");
	return affector;
}
EXPORT void CollisionAvoidanceAffector_Destroy(ParticleUniverse::CollisionAvoidanceAffector* ptr)
{
	ptr->~CollisionAvoidanceAffector();
}
EXPORT float CollisionAvoidanceAffector_GetRadius(ParticleUniverse::CollisionAvoidanceAffector* ptr)
{
	return ptr->getRadius();
}
EXPORT void CollisionAvoidanceAffector_SetRadius(ParticleUniverse::CollisionAvoidanceAffector* ptr, float radius)
{
	ptr->setRadius(radius);
}
EXPORT void CollisionAvoidanceAffector__prepare(ParticleUniverse::CollisionAvoidanceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void CollisionAvoidanceAffector__unprepare(ParticleUniverse::CollisionAvoidanceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void CollisionAvoidanceAffector__affect(ParticleUniverse::CollisionAvoidanceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void CollisionAvoidanceAffector_CopyAttributesTo (ParticleUniverse::CollisionAvoidanceAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo (affector);
}
#pragma endregion
#pragma region ColourAffector
EXPORT ParticleUniverse::ColourAffector* ColourAffector_New(void)
{
	ParticleUniverse::ColourAffector* affector = new ParticleUniverse::ColourAffector();
	affector->setAffectorType("Colour");
	return affector;
}

EXPORT void ColourAffector_Destroy(ParticleUniverse::ColourAffector* ptr)
{
	return ptr->~ColourAffector();
}

EXPORT void ColourAffector_AddColour(ParticleUniverse::ColourAffector* ptr, float timeFraction, Ogre::ColourValue* colour)
{
	ptr->addColour(timeFraction, *colour);
}

EXPORT const ParticleUniverse::ColourAffector::ColourMap* ColourAffector_GetTimeAndColour
	(ParticleUniverse::ColourAffector* ptr)
{
	return &ptr->getTimeAndColour();
}

EXPORT int ColourAffector_GetTimeAndColoursCount(ParticleUniverse::ColourAffector* ptr)
{
	ParticleUniverse::ColourAffector::ColourMap lines = ptr->getTimeAndColour();
	return lines.size();
}

EXPORT void ColourAffector_GetTimeAndColours (ParticleUniverse::ColourAffector* ptr, float* arrTimes, Ogre::ColourValue* arrColours, int bufSize)
{
	ParticleUniverse::ColourAffector::ColourMap lines = ptr->getTimeAndColour();
	
	if (lines.size() > (unsigned int)bufSize) //Avoid buffer over run.
		return;

	ParticleUniverse::ColourAffector::ColourMap::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      float timeVal = (float)it->first;
      Ogre::ColourValue colVal = (Ogre::ColourValue)it->second;
	  *(arrTimes + i) = timeVal;
	  *(arrColours + i) = colVal;
		i++; // = sizeof(name);
    }

}

EXPORT void ColourAffector_ClearColourMap(ParticleUniverse::ColourAffector* ptr)
{
	ptr->clearColourMap();
}

//EXPORT ParticleUniverse::ColourAffector::ColourMapIterator* ColourAffector__findNearestColourMapIterator(ParticleUniverse::ColourAffector* ptr, float timeFraction)
//{
//	return ptr->_findNearestColourMapIterator(timeFraction);
//}

EXPORT const ParticleUniverse::ColourAffector::ColourOperation ColourAffector_GetColourOperation (ParticleUniverse::ColourAffector* ptr)
{
	return ptr->getColourOperation();
}

EXPORT void ColourAffector_SetColourOperation (ParticleUniverse::ColourAffector* ptr, ParticleUniverse::ColourAffector::ColourOperation colourOperation)
{
	ptr->setColourOperation(colourOperation);
}

EXPORT void ColourAffector__affect(ParticleUniverse::ColourAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void ColourAffector_CopyAttributesTo (ParticleUniverse::ColourAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo (affector);
}
#pragma endregion
#pragma region FlockCenteringAffector
EXPORT ParticleUniverse::FlockCenteringAffector* FlockCenteringAffector_New(void)
{
	ParticleUniverse::FlockCenteringAffector* affector = new ParticleUniverse::FlockCenteringAffector();
	affector->setAffectorType("FlockCentering");
	return affector;
}	
EXPORT void FlockCenteringAffector_Destroy(ParticleUniverse::FlockCenteringAffector* ptr)
{
	ptr->~FlockCenteringAffector();
}
EXPORT void FlockCenteringAffector__preProcessParticles(ParticleUniverse::FlockCenteringAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void FlockCenteringAffector__affect(ParticleUniverse::FlockCenteringAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void FlockCenteringAffector_CopyAttributesTo (ParticleUniverse::FlockCenteringAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo (affector);
}
#pragma endregion
#pragma region ForceFieldAffector
EXPORT ParticleUniverse::ForceFieldAffector* ForceFieldAffector_New(void)
{
	ParticleUniverse::ForceFieldAffector* affector = new ParticleUniverse::ForceFieldAffector();
	affector->setAffectorType("ForceField");
	return affector;
}
EXPORT void ForceFieldAffector_Destroy(ParticleUniverse::ForceFieldAffector* ptr)
{
	ptr->~ForceFieldAffector();
}
EXPORT void ForceFieldAffector__prepare(ParticleUniverse::ForceFieldAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void ForceFieldAffector__preProcessParticles(ParticleUniverse::ForceFieldAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void ForceFieldAffector__notifyStart(ParticleUniverse::ForceFieldAffector* ptr)
{
	ptr->_notifyStart();
}
EXPORT void ForceFieldAffector__affect(ParticleUniverse::ForceFieldAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT const ParticleUniverse::ForceField::ForceFieldType ForceFieldAffector_GetForceFieldType(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getForceFieldType();
}
EXPORT void ForceFieldAffector_SetForceFieldType(ParticleUniverse::ForceFieldAffector* ptr, const ParticleUniverse::ForceField::ForceFieldType forceFieldType)
{
	ptr->setForceFieldType(forceFieldType);
}

EXPORT float ForceFieldAffector_GetDelta(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getDelta();
}
EXPORT void ForceFieldAffector_SetDelta(ParticleUniverse::ForceFieldAffector* ptr, float delta)
{
	ptr->setDelta(delta);
}

EXPORT float ForceFieldAffector_GetScaleForce(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getScaleForce();
}
EXPORT void ForceFieldAffector_SetScaleForce(ParticleUniverse::ForceFieldAffector* ptr, float scaleForce)
{
	ptr->setScaleForce(scaleForce);
}

EXPORT unsigned short ForceFieldAffector_GetOctaves(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getOctaves();
}
EXPORT void ForceFieldAffector_SetOctaves(ParticleUniverse::ForceFieldAffector* ptr, unsigned short octaves)
{
	ptr->setOctaves(octaves);
}

EXPORT double ForceFieldAffector_GetFrequency(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getFrequency();
}
EXPORT void ForceFieldAffector_SetFrequency(ParticleUniverse::ForceFieldAffector* ptr, double frequency)
{
	ptr->setFrequency(frequency);
}

EXPORT double ForceFieldAffector_GetAmplitude(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getAmplitude();
}
EXPORT void ForceFieldAffector_SetAmplitude(ParticleUniverse::ForceFieldAffector* ptr, double amplitude)
{
	ptr->setAmplitude(amplitude);
}

EXPORT double ForceFieldAffector_GetPersistence(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getPersistence();
}
EXPORT void ForceFieldAffector_SetPersistence(ParticleUniverse::ForceFieldAffector* ptr, double persistence)
{
	ptr->setPersistence(persistence);
}

EXPORT unsigned int ForceFieldAffector_GetForceFieldSize(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getForceFieldSize();
}
EXPORT void ForceFieldAffector_SetForceFieldSize(ParticleUniverse::ForceFieldAffector* ptr, unsigned int forceFieldSize)
{
	ptr->setForceFieldSize(forceFieldSize);
}

EXPORT ParticleUniverse::Vector3* ForceFieldAffector_GetWorldSize(ParticleUniverse::ForceFieldAffector* ptr)
{
	return &ptr->getWorldSize();
}
EXPORT void ForceFieldAffector_SetWorldSize(ParticleUniverse::ForceFieldAffector* ptr, const ParticleUniverse::Vector3* worldSize)
{
	ptr->setWorldSize(*worldSize);
}

EXPORT bool ForceFieldAffector_GetIgnoreNegativeX(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getIgnoreNegativeX();
}
EXPORT void ForceFieldAffector_SetIgnoreNegativeX(ParticleUniverse::ForceFieldAffector* ptr, bool ignoreNegativeX)
{
	ptr->setIgnoreNegativeX(ignoreNegativeX);
}
EXPORT bool ForceFieldAffector_GetIgnoreNegativeY(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getIgnoreNegativeY();
}
EXPORT void ForceFieldAffector_SetIgnoreNegativeY(ParticleUniverse::ForceFieldAffector* ptr, bool ignoreNegativeY)
{
	ptr->setIgnoreNegativeY(ignoreNegativeY);
}
EXPORT bool ForceFieldAffector_GetIgnoreNegativeZ(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getIgnoreNegativeZ();
}
EXPORT void ForceFieldAffector_SetIgnoreNegativeZ(ParticleUniverse::ForceFieldAffector* ptr, bool ignoreNegativeZ)
{
	ptr->setIgnoreNegativeZ(ignoreNegativeZ);
}
EXPORT const ParticleUniverse::Vector3* ForceFieldAffector_GetMovement(ParticleUniverse::ForceFieldAffector* ptr)
{
	return &ptr->getMovement();
}
EXPORT void ForceFieldAffector_SetMovement(ParticleUniverse::ForceFieldAffector* ptr, const ParticleUniverse::Vector3* movement)
{
	ptr->setMovement(*movement);
}

EXPORT float ForceFieldAffector_GetMovementFrequency(ParticleUniverse::ForceFieldAffector* ptr)
{
	return ptr->getMovementFrequency();
}
EXPORT void ForceFieldAffector_SetMovementFrequency(ParticleUniverse::ForceFieldAffector* ptr, float movementFrequency)
{
	ptr->setMovementFrequency(movementFrequency);
}
EXPORT void ForceFieldAffector_SuppressGeneration(ParticleUniverse::ForceFieldAffector* ptr, bool suppress)
{
	ptr->suppressGeneration(suppress);
}
EXPORT void ForceFieldAffector_CopyAttributesTo (ParticleUniverse::ForceFieldAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion
#pragma region GeometryRotator
EXPORT ParticleUniverse::GeometryRotator* GeometryRotator_New()
{
	ParticleUniverse::GeometryRotator* affector = new ParticleUniverse::GeometryRotator();
	affector->setAffectorType("GeometryRotator");
	return affector;
}
EXPORT void GeometryRotator_Destroy(ParticleUniverse::GeometryRotator* ptr)
{
	ptr->~GeometryRotator();
}
EXPORT ParticleUniverse::DynamicAttribute* GeometryRotator_GetRotationSpeed(ParticleUniverse::GeometryRotator* ptr)
{
	return ptr->getRotationSpeed();
}
EXPORT void GeometryRotator_SetRotationSpeed(ParticleUniverse::GeometryRotator* ptr, ParticleUniverse::DynamicAttribute* dynRotationSpeed)
{
	ptr->setRotationSpeed(dynRotationSpeed);
}
EXPORT bool GeometryRotator_UseOwnRotationSpeed (ParticleUniverse::GeometryRotator* ptr)
{
	return ptr->useOwnRotationSpeed();
}
EXPORT void GeometryRotator_SetUseOwnRotationSpeed (ParticleUniverse::GeometryRotator* ptr, bool useOwnRotationSpeed)
{
	ptr->setUseOwnRotationSpeed(useOwnRotationSpeed);
}
EXPORT const ParticleUniverse::Vector3* GeometryRotator_GetRotationAxis(ParticleUniverse::GeometryRotator* ptr)
{
	return &ptr->getRotationAxis();
}
EXPORT void GeometryRotator_SetRotationAxis(ParticleUniverse::GeometryRotator* ptr, const ParticleUniverse::Vector3* rotationAxis)
{
	ptr->setRotationAxis(*rotationAxis);
}
EXPORT void GeometryRotator_ResetRotationAxis(ParticleUniverse::GeometryRotator* ptr)
{
	ptr->resetRotationAxis();
}
EXPORT float GeometryRotator__calculateRotationSpeed (ParticleUniverse::GeometryRotator* ptr, ParticleUniverse::Particle* particle)
{
	return ptr->_calculateRotationSpeed(particle);
}
EXPORT void GeometryRotator_CopyAttributesTo (ParticleUniverse::GeometryRotator* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo (affector);
}
EXPORT void GeometryRotator_initParticleForEmission(ParticleUniverse::GeometryRotator* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}
EXPORT void GeometryRotator__affect(ParticleUniverse::GeometryRotator* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region GravityAffector
EXPORT ParticleUniverse::GravityAffector* GravityAffector_New()
{
	ParticleUniverse::GravityAffector* affector = new ParticleUniverse::GravityAffector();
	affector->setAffectorType("Gravity");
	return affector;
}
EXPORT void GravityAffector_Destroy(ParticleUniverse::GravityAffector* ptr)
{
	ptr->~GravityAffector();
}
EXPORT void GravityAffector__CopyAttributesTo (ParticleUniverse::GravityAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo (affector);
}
EXPORT void GravityAffector__preProcessParticles(ParticleUniverse::GravityAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void GravityAffector__affect(ParticleUniverse::GravityAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT float GravityAffector_GetGravity(ParticleUniverse::GravityAffector* ptr)
{
	return ptr->getGravity();
}
EXPORT void GravityAffector_SetGravity(ParticleUniverse::GravityAffector* ptr, float gravity)
{
	ptr->setGravity(gravity);
}

#pragma endregion
#pragma region InterParticleCollider
EXPORT ParticleUniverse::InterParticleCollider* InterParticleCollider_New()
{
	ParticleUniverse::InterParticleCollider* affector = new ParticleUniverse::InterParticleCollider();
	affector->setAffectorType("InterParticleCollider");
	return affector;
}
EXPORT void InterParticleCollider_Destroy(ParticleUniverse::InterParticleCollider* ptr)
{
	ptr->~InterParticleCollider();
}
EXPORT float InterParticleCollider_GetAdjustment(ParticleUniverse::InterParticleCollider* ptr)
{
	return ptr->getAdjustment();
}
EXPORT void InterParticleCollider_SetAdjustment(ParticleUniverse::InterParticleCollider* ptr, float adjustment)
{
	ptr->setAdjustment(adjustment);
}
EXPORT ParticleUniverse::InterParticleCollider::InterParticleCollisionResponse InterParticleCollider_GetInterParticleCollisionResponse(ParticleUniverse::InterParticleCollider* ptr)
{
	return ptr->getInterParticleCollisionResponse();
}
EXPORT void InterParticleCollider_SetInterParticleCollisionResponse(ParticleUniverse::InterParticleCollider* ptr, ParticleUniverse::InterParticleCollider::InterParticleCollisionResponse* interParticleCollisionResponse)
{
	ptr->setInterParticleCollisionResponse(*interParticleCollisionResponse);
}
EXPORT void InterParticleCollider__prepare(ParticleUniverse::InterParticleCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void InterParticleCollider__unprepare(ParticleUniverse::InterParticleCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void InterParticleCollider__affect(ParticleUniverse::InterParticleCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void InterParticleCollider_CopyAttributesTo (ParticleUniverse::InterParticleCollider* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion
#pragma region JetAffector
EXPORT ParticleUniverse::JetAffector* JetAffector_New()
{
	ParticleUniverse::JetAffector* affector = new ParticleUniverse::JetAffector();
	affector->setAffectorType("Jet");
	return affector;
}
EXPORT void JetAffector_Destroy(ParticleUniverse::JetAffector* ptr)
{
	ptr->~JetAffector();
}
EXPORT ParticleUniverse::DynamicAttribute* JetAffector_GetDynAcceleration(ParticleUniverse::JetAffector* ptr)
{
	return ptr->getDynAcceleration();
}
EXPORT void JetAffector_SetDynAcceleration(ParticleUniverse::JetAffector* ptr, ParticleUniverse::DynamicAttribute* dynAcceleration)
{
	ptr->setDynAcceleration(dynAcceleration);
}
EXPORT void JetAffector__affect(ParticleUniverse::JetAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void JetAffector_CopyAttributesTo (ParticleUniverse::JetAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion
#pragma region LineAffector
EXPORT ParticleUniverse::LineAffector* LineAffector_New()
{
	ParticleUniverse::LineAffector* affector = new ParticleUniverse::LineAffector();
	affector->setAffectorType("Line");
	return affector;
}

EXPORT void LineAffector_Destroy(ParticleUniverse::LineAffector* ptr)
{
	ptr->~LineAffector();
}

EXPORT float LineAffector_GetMaxDeviation(ParticleUniverse::LineAffector* ptr)
{
	return ptr->getMaxDeviation();
}
EXPORT void LineAffector_SetMaxDeviation(ParticleUniverse::LineAffector* ptr, float maxDeviation)
{
	ptr->setMaxDeviation(maxDeviation);
}

EXPORT const Ogre::Vector3* LineAffector_GetEnd(ParticleUniverse::LineAffector* ptr)
{
	return &ptr->getEnd();
}
EXPORT void LineAffector_SetEnd(ParticleUniverse::LineAffector* ptr, const Ogre::Vector3* end)
{
	ptr->setEnd(*end);
}

EXPORT float LineAffector_GetTimeStep(ParticleUniverse::LineAffector* ptr)
{
	return ptr->getTimeStep();
}
EXPORT void LineAffector_SetTimeStep(ParticleUniverse::LineAffector* ptr, float timeStep)
{
	ptr->setTimeStep(timeStep);
}

EXPORT float LineAffector_GetDrift(ParticleUniverse::LineAffector* ptr)
{
	return ptr->getDrift();
}
EXPORT void LineAffector_SetDrift(ParticleUniverse::LineAffector* ptr, float drift)
{
	ptr->setDrift(drift);
}

EXPORT void LineAffector__notifyRescaled(ParticleUniverse::LineAffector* ptr, ParticleUniverse::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT void LineAffector__firstParticle(ParticleUniverse::LineAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_firstParticle(particleTechnique, particle, timeElapsed);
}
EXPORT void LineAffector__preProcessParticles(ParticleUniverse::LineAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void LineAffector__postProcessParticles(ParticleUniverse::LineAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_postProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void LineAffector__affect(ParticleUniverse::LineAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void LineAffector_CopyAttributesTo (ParticleUniverse::LineAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}

#pragma endregion
#pragma region LinearForceAffector
EXPORT ParticleUniverse::LinearForceAffector* LinearForceAffector_New()
{
	ParticleUniverse::LinearForceAffector* affector = new ParticleUniverse::LinearForceAffector();
	affector->setAffectorType("LinearForce");
	return affector;
}
EXPORT void LinearForceAffector_Destroy(ParticleUniverse::LinearForceAffector* ptr)
{
	ptr->~LinearForceAffector();
}
EXPORT void LinearForceAffector_CopyAttributesTo (ParticleUniverse::LinearForceAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void LinearForceAffector__preProcessParticles(ParticleUniverse::LinearForceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void LinearForceAffector__affect(ParticleUniverse::LinearForceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region ParticleFollower
EXPORT ParticleUniverse::ParticleFollower* ParticleFollower_New()
{
	ParticleUniverse::ParticleFollower* affector = new ParticleUniverse::ParticleFollower();
	affector->setAffectorType("ParticleFollower");
	return affector;
}
EXPORT void ParticleFollower_Destroy(ParticleUniverse::ParticleFollower* ptr)
{
	ptr->~ParticleFollower();
}
EXPORT void ParticleFollower_CopyAttributesTo (ParticleUniverse::ParticleFollower* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void ParticleFollower__firstParticle(ParticleUniverse::ParticleFollower* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_firstParticle(particleTechnique, particle, timeElapsed);
}
EXPORT void ParticleFollower__affect(ParticleUniverse::ParticleFollower* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT float ParticleFollower_GetMaxDistance(ParticleUniverse::ParticleFollower* ptr)
{
	return ptr->getMaxDistance();
}
EXPORT void ParticleFollower_SetMaxDistance(ParticleUniverse::ParticleFollower* ptr, float maxDistance)
{
	ptr->setMaxDistance(maxDistance);
}
EXPORT float ParticleFollower_GetMinDistance(ParticleUniverse::ParticleFollower* ptr)
{
	return ptr->getMinDistance();
}
EXPORT void ParticleFollower_SetMinDistance(ParticleUniverse::ParticleFollower* ptr, float minDistance)
{
	ptr->setMinDistance(minDistance);
}
#pragma endregion
#pragma region PathFollower
EXPORT ParticleUniverse::PathFollower* PathFollower_New()
{
	ParticleUniverse::PathFollower* affector = new ParticleUniverse::PathFollower();
	affector->setAffectorType("PathFollower");
	return affector;
}
EXPORT void PathFollower_Destroy(ParticleUniverse::PathFollower* ptr)
{
	ptr->~PathFollower();
}
EXPORT void PathFollower_AddPoint(ParticleUniverse::PathFollower* ptr,const Ogre::Vector3* point)
{
	ptr->addPoint(*point);
}
EXPORT void PathFollower_ClearPoints (ParticleUniverse::PathFollower* ptr)
{
	ptr->clearPoints();
}
EXPORT unsigned short PathFollower_GetNumPoints(ParticleUniverse::PathFollower* ptr)
{
	return ptr->getNumPoints();
}
EXPORT const Ogre::Vector3& PathFollower_GetPoint(ParticleUniverse::PathFollower* ptr, unsigned short index)
{
	return ptr->getPoint(index);
}	
EXPORT void PathFollower__affect(ParticleUniverse::PathFollower* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void PathFollower_CopyAttributesTo (ParticleUniverse::PathFollower* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion
#pragma region PlaneCollider
EXPORT ParticleUniverse::PlaneCollider* PlaneCollider_New()
{
	ParticleUniverse::PlaneCollider* affector = new ParticleUniverse::PlaneCollider();
	affector->setAffectorType("PlaneCollider");
	return affector;
}
EXPORT void PlaneCollider_Destroy(ParticleUniverse::PlaneCollider* ptr)
{
	ptr->~PlaneCollider();
}
EXPORT const Ogre::Vector3* PlaneCollider_GetNormal(ParticleUniverse::PlaneCollider* ptr)
{
	return &ptr->getNormal();
}		
EXPORT void PlaneCollider_SetNormal(ParticleUniverse::PlaneCollider* ptr, const Ogre::Vector3* normal)
{
	ptr->setNormal(*normal);
}		
EXPORT void PlaneCollider__notifyRescaled(ParticleUniverse::PlaneCollider* ptr, const Ogre::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}		
EXPORT void PlaneCollider_CalculateDirectionAfterCollision(ParticleUniverse::PlaneCollider* ptr, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->calculateDirectionAfterCollision(particle, timeElapsed);
}		
EXPORT void PlaneCollider__affect(ParticleUniverse::PlaneCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void PlaneCollider_CopyAttributesTo (ParticleUniverse::PlaneCollider* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion
#pragma region Randomiser
EXPORT ParticleUniverse::Randomiser* Randomiser_New()
{
	ParticleUniverse::Randomiser* affector = new ParticleUniverse::Randomiser();
	affector->setAffectorType("Randomiser");
	return affector;
}
EXPORT void Randomiser_Destroy(ParticleUniverse::Randomiser* ptr)
{
	ptr->~Randomiser();
}
EXPORT float Randomiser_GetMaxDeviationX(ParticleUniverse::Randomiser* ptr)
{
	return ptr->getMaxDeviationX();
}
EXPORT void Randomiser_SetMaxDeviationX(ParticleUniverse::Randomiser* ptr,  float maxDeviationX)
{
	ptr->setMaxDeviationX(maxDeviationX);
}
EXPORT float Randomiser_GetMaxDeviationY(ParticleUniverse::Randomiser* ptr)
{
	return ptr->getMaxDeviationY();
}
EXPORT void Randomiser_SetMaxDeviationY(ParticleUniverse::Randomiser* ptr,  float maxDeviationZ)
{
	ptr->setMaxDeviationY(maxDeviationZ);
}
EXPORT float Randomiser_GetMaxDeviationZ(ParticleUniverse::Randomiser* ptr)
{
	return ptr->getMaxDeviationZ();
}
EXPORT void Randomiser_SetMaxDeviationZ(ParticleUniverse::Randomiser* ptr,  float maxDeviationZ)
{
	ptr->setMaxDeviationZ(maxDeviationZ);
}
EXPORT float Randomiser_GetTimeStep(ParticleUniverse::Randomiser* ptr)
{
	return ptr->getTimeStep();
}
EXPORT void Randomiser_SetTimeStep(ParticleUniverse::Randomiser* ptr,  float timeStep)
{
	ptr->setTimeStep(timeStep);
}
EXPORT bool Randomiser_IsRandomDirection(ParticleUniverse::Randomiser* ptr)
{
	return ptr->isRandomDirection();
}
EXPORT void Randomiser_SetRandomDirection(ParticleUniverse::Randomiser* ptr, bool randomDirection)
{
	ptr->setRandomDirection(randomDirection);
}
EXPORT void Randomiser__affect(ParticleUniverse::Randomiser* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void Randomiser_CopyAttributesTo (ParticleUniverse::Randomiser* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void Randomiser__preProcessParticles(ParticleUniverse::Randomiser* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT void Randomiser__postProcessParticles(ParticleUniverse::Randomiser* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_postProcessParticles(particleTechnique, timeElapsed);
}
#pragma endregion
#pragma region ScaleAffector
EXPORT ParticleUniverse::ScaleAffector* ScaleAffector_New()
{
	ParticleUniverse::ScaleAffector* affector = new ParticleUniverse::ScaleAffector();
	affector->setAffectorType("Scale");
	return affector;
}
EXPORT void ScaleAffector_Destroy(ParticleUniverse::ScaleAffector* ptr)
{
	ptr->~ScaleAffector();
}
EXPORT void ScaleAffector__affect(ParticleUniverse::ScaleAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void ScaleAffector_CopyAttributesTo (ParticleUniverse::ScaleAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT ParticleUniverse::DynamicAttribute* ScaleAffector_GetDynScaleX(ParticleUniverse::ScaleAffector* ptr)
{
	return ptr->getDynScaleX();
}
EXPORT void ScaleAffector_SetDynScaleX(ParticleUniverse::ScaleAffector* ptr, ParticleUniverse::DynamicAttribute* dynScaleX)
{
	ptr->setDynScaleX(dynScaleX);
}
EXPORT void ScaleAffector_ResetDynScaleX(ParticleUniverse::ScaleAffector* ptr, bool resetToDefault = true)
{
	ptr->resetDynScaleX(resetToDefault);
}
EXPORT ParticleUniverse::DynamicAttribute* ScaleAffector_GetDynScaleY(ParticleUniverse::ScaleAffector* ptr)
{
	return ptr->getDynScaleY();
}
EXPORT void ScaleAffector_SetDynScaleY(ParticleUniverse::ScaleAffector* ptr, ParticleUniverse::DynamicAttribute* dynScaleY)
{
	ptr->setDynScaleY(dynScaleY);
}
EXPORT void ScaleAffector_ResetDynScaleY(ParticleUniverse::ScaleAffector* ptr, bool resetToDefault = true)
{
	ptr->resetDynScaleY(resetToDefault);
}
EXPORT ParticleUniverse::DynamicAttribute* ScaleAffector_GetDynScaleZ(ParticleUniverse::ScaleAffector* ptr)
{
	return ptr->getDynScaleZ();
}
EXPORT void ScaleAffector_SetDynScaleZ(ParticleUniverse::ScaleAffector* ptr, ParticleUniverse::DynamicAttribute* dynScaleZ)
{
	ptr->setDynScaleZ(dynScaleZ);
}
EXPORT void ScaleAffector_ResetDynScaleZ(ParticleUniverse::ScaleAffector* ptr, bool resetToDefault = true)
{
	ptr->resetDynScaleZ(resetToDefault);
}
EXPORT ParticleUniverse::DynamicAttribute* ScaleAffector_GetDynScaleXYZ(ParticleUniverse::ScaleAffector* ptr)
{
	return ptr->getDynScaleXYZ();
}
EXPORT void ScaleAffector_SetDynScaleXYZ(ParticleUniverse::ScaleAffector* ptr, ParticleUniverse::DynamicAttribute* dynScaleXYZ)
{
	ptr->setDynScaleXYZ(dynScaleXYZ);
}
EXPORT void ScaleAffector_ResetDynScaleXYZ(ParticleUniverse::ScaleAffector* ptr, bool resetToDefault = true)
{
	ptr->resetDynScaleXYZ(resetToDefault);
}
EXPORT bool ScaleAffector_IsSinceStartSystem(ParticleUniverse::ScaleAffector* ptr)
{
	return ptr->isSinceStartSystem();
}
EXPORT void ScaleAffector_SetSinceStartSystem(ParticleUniverse::ScaleAffector* ptr, bool sinceStartSystem)
{
	ptr->setSinceStartSystem(sinceStartSystem);
}
#pragma endregion
#pragma region ScaleVelocityAffector
EXPORT ParticleUniverse::ScaleVelocityAffector* ScaleVelocityAffector_New()
{
	ParticleUniverse::ScaleVelocityAffector* affector = new ParticleUniverse::ScaleVelocityAffector();
	affector->setAffectorType("ScaleVelocity");
	return affector;
}
EXPORT void ScaleVelocityAffector_Destroy(ParticleUniverse::ScaleVelocityAffector* ptr)
{
	ptr->~ScaleVelocityAffector();
}
EXPORT void ScaleVelocityAffector__affect(ParticleUniverse::ScaleVelocityAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void ScaleVelocityAffector_CopyAttributesTo (ParticleUniverse::ScaleVelocityAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT ParticleUniverse::DynamicAttribute* ScaleVelocityAffector_GetDynScaleVelocity(ParticleUniverse::ScaleVelocityAffector* ptr)
{
	return ptr->getDynScaleVelocity();
}
EXPORT void ScaleVelocityAffector_SetDynScaleVelocity(ParticleUniverse::ScaleVelocityAffector* ptr, ParticleUniverse::DynamicAttribute* dynScaleVelocity)
{
	ptr->setDynScaleVelocity(dynScaleVelocity);
}
EXPORT void ScaleVelocityAffector_ResetDynScaleVelocity(ParticleUniverse::ScaleVelocityAffector* ptr, bool resetToDefault = true)
{
	ptr->resetDynScaleVelocity(resetToDefault);
}
EXPORT bool ScaleVelocityAffector_IsSinceStartSystem(ParticleUniverse::ScaleVelocityAffector* ptr)
{
	return ptr->isSinceStartSystem();
}
EXPORT void ScaleVelocityAffector_SetSinceStartSystem(ParticleUniverse::ScaleVelocityAffector* ptr, bool sinceStartSystem)
{
	ptr->setSinceStartSystem(sinceStartSystem);
}
EXPORT bool ScaleVelocityAffector_IsStopAtFlip(ParticleUniverse::ScaleVelocityAffector* ptr)
{
	return ptr->isStopAtFlip();
}
EXPORT void ScaleVelocityAffector_SetStopAtFlip(ParticleUniverse::ScaleVelocityAffector* ptr, bool stopAtFlip)
{
	ptr->setStopAtFlip(stopAtFlip);
}
#pragma endregion
#pragma region SineForceAffector
EXPORT ParticleUniverse::SineForceAffector* SineForceAffector_New()
{
	ParticleUniverse::SineForceAffector* affector = new ParticleUniverse::SineForceAffector();
	affector->setAffectorType("SineForce");
	return affector;
}
EXPORT void SineForceAffector_Destroy(ParticleUniverse::SineForceAffector* ptr)
{
	ptr->~SineForceAffector();
}
EXPORT void SineForceAffector__affect(ParticleUniverse::SineForceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void SineForceAffector_CopyAttributesTo (ParticleUniverse::SineForceAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void SineForceAffector__preProcessParticles(ParticleUniverse::SineForceAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
EXPORT float SineForceAffector_GetFrequencyMin(ParticleUniverse::SineForceAffector* ptr)
{
	return ptr->getFrequencyMin();
}
EXPORT void SineForceAffector_SetFrequencyMin(ParticleUniverse::SineForceAffector* ptr, float frequencyMin)
{
	ptr->setFrequencyMin(frequencyMin);
}
EXPORT float SineForceAffector_GetFrequencyMax(ParticleUniverse::SineForceAffector* ptr)
{
	return ptr->getFrequencyMax();
}
EXPORT void SineForceAffector_SetFrequencyMax(ParticleUniverse::SineForceAffector* ptr, float frequencyMax)
{
	ptr->setFrequencyMax(frequencyMax);
}
#pragma endregion
#pragma region SphereCollider
EXPORT ParticleUniverse::SphereCollider* SphereCollider_New()
{
	ParticleUniverse::SphereCollider* affector = new ParticleUniverse::SphereCollider();
	affector->setAffectorType("SphereCollider");
	return affector;
}
EXPORT void SphereCollider_Destroy(ParticleUniverse::SphereCollider* ptr)
{
	ptr->~SphereCollider();
}
EXPORT float SphereCollider_GetRadius(ParticleUniverse::SphereCollider* ptr)
{
	return ptr->getRadius();
}
EXPORT void SphereCollider_SetRadius(ParticleUniverse::SphereCollider* ptr, float radius)
{
	ptr->setRadius(radius);
}
EXPORT bool SphereCollider_IsInnerCollision(ParticleUniverse::SphereCollider* ptr)
{
	return ptr->isInnerCollision();
}
EXPORT void SphereCollider_SetInnerCollision(ParticleUniverse::SphereCollider* ptr, bool innerCollision)
{
	ptr->setInnerCollision(innerCollision);
}
EXPORT void SphereCollider_CalculateDirectionAfterCollision(ParticleUniverse::SphereCollider* ptr, ParticleUniverse::Particle* particle, Ogre::Vector3* distance, float distanceLength)
{
	ptr->calculateDirectionAfterCollision(particle, *distance, distanceLength);
}
EXPORT void SphereCollider__affect(ParticleUniverse::SphereCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void SphereCollider_CopyAttributesTo (ParticleUniverse::SphereCollider* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void SphereCollider__preProcessParticles(ParticleUniverse::SphereCollider* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
#pragma endregion
#pragma region TextureAnimator
EXPORT ParticleUniverse::TextureAnimator* TextureAnimator_New()
{
	ParticleUniverse::TextureAnimator* affector = new ParticleUniverse::TextureAnimator();
	affector->setAffectorType("TextureAnimator");
	return affector;
}
EXPORT void TextureAnimator_Destroy(ParticleUniverse::TextureAnimator* ptr)
{
	ptr->~TextureAnimator();
}
EXPORT float TextureAnimator_GetAnimationTimeStep(ParticleUniverse::TextureAnimator* ptr)
{
	return ptr->getAnimationTimeStep();
}
EXPORT void TextureAnimator_SetAnimationTimeStep(ParticleUniverse::TextureAnimator* ptr, float animationTimeStep)
{
	ptr->setAnimationTimeStep(animationTimeStep);
}
EXPORT ParticleUniverse::TextureAnimator::TextureAnimationType TextureAnimator_GetTextureAnimationType(ParticleUniverse::TextureAnimator* ptr)
{
	return ptr->getTextureAnimationType();
}
EXPORT void TextureAnimator_SetTextureAnimationType(ParticleUniverse::TextureAnimator* ptr, ParticleUniverse::TextureAnimator::TextureAnimationType textureAnimationType)
{
	ptr->setTextureAnimationType(textureAnimationType);
}
EXPORT unsigned short TextureAnimator_GetTextureCoordsStart(ParticleUniverse::TextureAnimator* ptr)
{
	return ptr->getTextureCoordsStart();
}
EXPORT void TextureAnimator_SetTextureCoordsStart(ParticleUniverse::TextureAnimator* ptr, unsigned short textureCoordsStart)
{
	ptr->setTextureCoordsStart(textureCoordsStart);
}
EXPORT unsigned short TextureAnimator_GetTextureCoordsEnd(ParticleUniverse::TextureAnimator* ptr)
{
	return ptr->getTextureCoordsEnd();
}
EXPORT void TextureAnimator_SetTextureCoordsEnd(ParticleUniverse::TextureAnimator* ptr, unsigned short textureCoordsEnd)
{
	ptr->setTextureCoordsEnd(textureCoordsEnd);
}
EXPORT bool TextureAnimator_IsStartRandom(ParticleUniverse::TextureAnimator* ptr)
{
	return ptr->isStartRandom();
}
EXPORT void TextureAnimator_SetStartRandom(ParticleUniverse::TextureAnimator* ptr, bool startRandom)
{
	ptr->setStartRandom(startRandom);
}
EXPORT void TextureAnimator__initParticleForEmission(ParticleUniverse::TextureAnimator* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}
EXPORT void TextureAnimator__affect(ParticleUniverse::TextureAnimator* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void TextureAnimator_CopyAttributesTo (ParticleUniverse::TextureAnimator* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void TextureAnimator__preProcessParticles(ParticleUniverse::TextureAnimator* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
#pragma endregion
#pragma region TextureRotator
EXPORT ParticleUniverse::TextureRotator* TextureRotator_New()
{
	ParticleUniverse::TextureRotator* affector = new ParticleUniverse::TextureRotator();
	affector->setAffectorType("TextureRotator");
	return affector;
}
EXPORT void TextureRotator_Destroy(ParticleUniverse::TextureRotator* ptr)
{
	ptr->~TextureRotator();
}
EXPORT bool TextureRotator_UseOwnRotationSpeed (ParticleUniverse::TextureRotator* ptr)
{
	return ptr->useOwnRotationSpeed();
}
EXPORT void TextureRotator_SetUseOwnRotationSpeed (ParticleUniverse::TextureRotator* ptr, bool useOwnRotationSpeed)
{
	ptr->setUseOwnRotationSpeed(useOwnRotationSpeed);
}
EXPORT ParticleUniverse::DynamicAttribute* TextureRotator_GetRotationSpeed(ParticleUniverse::TextureRotator* ptr)
{
	return ptr->getRotationSpeed();
}
EXPORT void TextureRotator_SetRotationSpeed(ParticleUniverse::TextureRotator* ptr, ParticleUniverse::DynamicAttribute* dynRotationSpeed)
{
	ptr->setRotationSpeed(dynRotationSpeed);
}
EXPORT ParticleUniverse::DynamicAttribute* TextureRotator_GetRotation(ParticleUniverse::TextureRotator* ptr)
{
	return ptr->getRotation();
}
EXPORT void TextureRotator_SetRotation(ParticleUniverse::TextureRotator* ptr, ParticleUniverse::DynamicAttribute* dynRotation)
{
	ptr->setRotation(dynRotation);
}
EXPORT Ogre::Radian* TextureRotator__calculateRotation (ParticleUniverse::TextureRotator* ptr)
{
	return &ptr->_calculateRotation();
}
EXPORT Ogre::Radian* TextureRotator__calculateRotationSpeed (ParticleUniverse::TextureRotator* ptr, ParticleUniverse::Particle* particle)
{
	return &ptr->_calculateRotationSpeed(particle);
}
EXPORT void TextureRotator_CopyAttributesTo (ParticleUniverse::TextureRotator* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void TextureRotator__initParticleForEmission(ParticleUniverse::TextureRotator* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}
EXPORT void TextureRotator__affect(ParticleUniverse::TextureRotator* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region VelocityMatchingAffector
EXPORT ParticleUniverse::VelocityMatchingAffector* VelocityMatchingAffector_New()
{
	ParticleUniverse::VelocityMatchingAffector* affector = new ParticleUniverse::VelocityMatchingAffector();
	affector->setAffectorType("VelocityMatching");
	return affector;
}
EXPORT void VelocityMatchingAffector_Destroy(ParticleUniverse::VelocityMatchingAffector* ptr)
{
	ptr->~VelocityMatchingAffector();
}
EXPORT float VelocityMatchingAffector_GetRadius(ParticleUniverse::VelocityMatchingAffector* ptr)
{
	return ptr->getRadius();
}
EXPORT void VelocityMatchingAffector_SetRadius(ParticleUniverse::VelocityMatchingAffector* ptr, float radius)
{
	ptr->setRadius(radius);
}
EXPORT void VelocityMatchingAffector__affect(ParticleUniverse::VelocityMatchingAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void VelocityMatchingAffector_CopyAttributesTo (ParticleUniverse::VelocityMatchingAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void VelocityMatchingAffector__prepare(ParticleUniverse::VelocityMatchingAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void VelocityMatchingAffector__unprepare(ParticleUniverse::VelocityMatchingAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
#pragma endregion
#pragma region VisualParticle
EXPORT ParticleUniverse::VisualParticle* VisualParticle_New()
{
	ParticleUniverse::VisualParticle* affector = new ParticleUniverse::VisualParticle();
	affector->particleType = ParticleUniverse::Particle::PT_VISUAL;
	return affector;
}
EXPORT void VisualParticle_Destroy(ParticleUniverse::VisualParticle* ptr)
{
	ptr->~VisualParticle();
}
EXPORT Ogre::ColourValue* VisualParticle_GetColour(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->colour;
}
EXPORT void VisualParticle_SetColour(ParticleUniverse::VisualParticle* ptr, Ogre::ColourValue* newValue)
{
	ptr->colour = *newValue;
}
EXPORT Ogre::ColourValue* VisualParticle_GetOriginalColour(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->originalColour;
}
EXPORT void VisualParticle_SetOriginalColour(ParticleUniverse::VisualParticle* ptr, Ogre::ColourValue* newValue)
{
	ptr->originalColour = *newValue;
}
EXPORT Ogre::Radian* VisualParticle_GetZRotation(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->zRotation;
}
EXPORT void VisualParticle_SetZRotation(ParticleUniverse::VisualParticle* ptr, Ogre::Radian* newValue)
{
	ptr->zRotation = *newValue;
}
EXPORT Ogre::Radian* VisualParticle_GetZRotationSpeed(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->zRotationSpeed;
}
EXPORT void VisualParticle_SetZRotationSpeed(ParticleUniverse::VisualParticle* ptr, Ogre::Radian* newValue)
{
	ptr->zRotationSpeed = *newValue;
}
EXPORT Ogre::Quaternion* VisualParticle_GetOrientation(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->orientation;
}
EXPORT void VisualParticle_SetOrientation(ParticleUniverse::VisualParticle* ptr, Ogre::Quaternion* newValue)
{
	ptr->orientation = *newValue;
}
EXPORT Ogre::Quaternion* VisualParticle_GetOriginalOrientation(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->originalOrientation;
}
EXPORT void VisualParticle_SetOriginalOrientation(ParticleUniverse::VisualParticle*  ptr, Ogre::Quaternion* newValue)
{
	ptr->originalOrientation = *newValue;
}
EXPORT float VisualParticle_GetRotationSpeed(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->rotationSpeed;
}
EXPORT void VisualParticle_SetRotationSpeed(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->rotationSpeed = newValue;
}
EXPORT Ogre::Vector3* VisualParticle_GetRotationAxis(ParticleUniverse::VisualParticle* ptr)
{
	return &ptr->rotationAxis;
}
EXPORT void VisualParticle_SetRotationAxis(ParticleUniverse::VisualParticle* ptr, Ogre::Vector3* newValue)
{
	ptr->rotationAxis = *newValue;
}
EXPORT bool VisualParticle_GetOwnDimensions(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->ownDimensions;
}
EXPORT void VisualParticle_SetOwnDimensions(ParticleUniverse::VisualParticle* ptr, bool newValue)
{
	ptr->ownDimensions = newValue;
}
EXPORT float VisualParticle_GetWidth(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->width;
}
EXPORT void VisualParticle_SetWidth(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->width = newValue;
}
EXPORT float VisualParticle_GetHeight(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->height;
}
EXPORT void VisualParticle_SetHeight(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->height = newValue;
}
EXPORT float VisualParticle_GetDepth(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->depth;
}
EXPORT void VisualParticle_SetDepth(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->depth = newValue;
}
EXPORT float VisualParticle_GetRadius(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->radius;
}
EXPORT void VisualParticle_SetRadius(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->radius = newValue;
}
EXPORT float VisualParticle_GetTextureAnimationTimeStep(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->textureAnimationTimeStep;
}
EXPORT void VisualParticle_SetTextureAnimationTimeStep(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->textureAnimationTimeStep = newValue;
}
EXPORT float VisualParticle_GetTextureAnimationTimeStepCount(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->textureAnimationTimeStepCount;
}
EXPORT void VisualParticle_SetTextureAnimationTimeStepCount(ParticleUniverse::VisualParticle* ptr, float newValue)
{
	ptr->textureAnimationTimeStepCount = newValue;
}
EXPORT unsigned short VisualParticle_GetTextureCoordsCurrent(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->textureCoordsCurrent;
}
EXPORT void VisualParticle_SetTextureCoordsCurrent(ParticleUniverse::VisualParticle* ptr, unsigned short newValue)
{
	ptr->textureCoordsCurrent = newValue;
}
EXPORT bool VisualParticle_GetTextureAnimationDirectionUp(ParticleUniverse::VisualParticle* ptr)
{
	return ptr->textureAnimationDirectionUp;
}
EXPORT void VisualParticle_SetTextureAnimationDirectionUp(ParticleUniverse::VisualParticle* ptr, bool newValue)
{
	ptr->textureAnimationDirectionUp = newValue;
}
EXPORT void VisualParticle_SetOwnDimensions2(ParticleUniverse::VisualParticle* ptr, float newWidth, float newHeight, float newDepth)
{
	ptr->setOwnDimensions(newWidth, newHeight, newDepth);
}
EXPORT void VisualParticle__initForEmission(ParticleUniverse::VisualParticle* ptr)
{
	ptr->_initForEmission();
}
EXPORT void VisualParticle__initForExpiration(ParticleUniverse::VisualParticle* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_initForExpiration(technique, timeElapsed);
}
EXPORT void VisualParticle__calculateBoundingSphereRadius(ParticleUniverse::VisualParticle* ptr)
{
	ptr->_calculateBoundingSphereRadius();
}
#pragma endregion
#pragma region VortexAffector
EXPORT ParticleUniverse::VortexAffector* VortexAffector_New()
{
	ParticleUniverse::VortexAffector* affector = new ParticleUniverse::VortexAffector();
	affector->setAffectorType("Vortex");
	return affector;
}
EXPORT void VortexAffector_Destroy(ParticleUniverse::VortexAffector* ptr)
{
	ptr->~VortexAffector();
}
EXPORT const Ogre::Vector3* VortexAffector_GetRotationVector(ParticleUniverse::VortexAffector* ptr)
{
	return &ptr->getRotationVector();
}
EXPORT void VortexAffector_SetRotationVector(ParticleUniverse::VortexAffector* ptr, Ogre::Vector3* rotationVector)
{
	return ptr->setRotationVector(*rotationVector);
}
EXPORT ParticleUniverse::DynamicAttribute* VortexAffector_GetRotationSpeed(ParticleUniverse::VortexAffector* ptr)
{
	return ptr->getRotationSpeed();
}
EXPORT void VortexAffector_SetRotationSpeed(ParticleUniverse::VortexAffector* ptr, ParticleUniverse::DynamicAttribute* dynRotationSpeed)
{
	return ptr->setRotationSpeed(dynRotationSpeed);
}
EXPORT void VortexAffector__affect(ParticleUniverse::VortexAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_affect(particleTechnique, particle, timeElapsed);
}
EXPORT void VortexAffector_CopyAttributesTo (ParticleUniverse::VortexAffector* ptr, ParticleUniverse::ParticleAffector* affector)
{
	ptr->copyAttributesTo(affector);
}
EXPORT void VortexAffector__preProcessParticles(ParticleUniverse::VortexAffector* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_preProcessParticles(particleTechnique, timeElapsed);
}
#pragma endregion

#pragma region ParticleBehavior Exports
EXPORT void ParticleBehaviour_Destroy(ParticleUniverse::ParticleBehaviour* ptr)
{
	ptr->~ParticleBehaviour();
}
EXPORT const char* ParticleBehaviour_GetBehaviourType(ParticleUniverse::ParticleBehaviour* ptr)
{
	return ptr->getBehaviourType().c_str();
}
EXPORT void ParticleBehaviour_SetBehaviourType(ParticleUniverse::ParticleBehaviour* ptr, char* behaviourType)
{
	ptr->setBehaviourType(behaviourType);
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleBehaviour_GetParentTechnique(ParticleUniverse::ParticleBehaviour* ptr)
{
	return ptr->getParentTechnique();
}
EXPORT void ParticleBehaviour_SetParentTechnique(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleTechnique* parentTechnique)
{
	ptr->setParentTechnique(parentTechnique);
}
EXPORT void ParticleBehaviour__notifyRescaled(ParticleUniverse::ParticleBehaviour* ptr, Ogre::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT void ParticleBehaviour__prepare(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void ParticleBehaviour__unprepare(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void ParticleBehaviour__initParticleForEmission(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}
EXPORT void ParticleBehaviour__processParticle(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_processParticle(technique, particle, timeElapsed);
}
EXPORT void ParticleBehaviour__initParticleForExpiration(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_initParticleForExpiration(technique, particle, timeElapsed);
}
EXPORT void ParticleBehaviour_CopyAttributesTo (ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleBehaviour* behaviour)
{
	ptr->copyAttributesTo(behaviour);
}
EXPORT void ParticleBehaviour_CopyParentAttributesTo(ParticleUniverse::ParticleBehaviour* ptr, ParticleUniverse::ParticleBehaviour* behaviour)
{
	ptr->copyParentAttributesTo(behaviour);
}
#pragma endregion
#pragma region SlaveBehavior Exports
EXPORT void SlaveBehaviour_SetMasterParticle(ParticleUniverse::SlaveBehaviour* ptr, ParticleUniverse::Particle* particle)
{
	ptr->masterParticle = particle;
}
EXPORT ParticleUniverse::Particle* SlaveBehaviour_GetMasterParticle(ParticleUniverse::SlaveBehaviour* ptr)
{
	return ptr->masterParticle;
}
EXPORT ParticleUniverse::SlaveBehaviour* SlaveBehaviour_New()
{
	ParticleUniverse::SlaveBehaviour* behaviour = new ParticleUniverse::SlaveBehaviour();
	behaviour->setBehaviourType("Slave");
	return behaviour;
}
EXPORT void SlaveBehaviour_Destroy(ParticleUniverse::SlaveBehaviour* ptr)
{
	ptr->~SlaveBehaviour();
}
EXPORT void SlaveBehaviour__processParticle(ParticleUniverse::SlaveBehaviour* ptr, ParticleUniverse::ParticleTechnique* technique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_processParticle(technique, particle, timeElapsed);
}
EXPORT void SlaveBehaviour_CopyAttributesTo (ParticleUniverse::SlaveBehaviour* ptr, ParticleUniverse::ParticleBehaviour* affector)
{
	ptr->copyAttributesTo(affector);
}
#pragma endregion

#pragma region ParticleRenderer Exports
EXPORT void ParticleRenderer_Destroy(ParticleUniverse::ParticleRenderer* ptr)
{
	ptr->~ParticleRenderer();
}
EXPORT bool ParticleRenderer_GetAutoRotate(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->autoRotate;
}
EXPORT void ParticleRenderer_SetAutoRotate(ParticleUniverse::ParticleRenderer* ptr, bool newVal)
{
	ptr->autoRotate = newVal;
}
EXPORT const char* ParticleRenderer_GetRendererType(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getRendererType().c_str();
}
EXPORT void ParticleRenderer_SetRendererType(ParticleUniverse::ParticleRenderer* ptr, char* rendererType)
{
	return ptr->setRendererType(rendererType);
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleRenderer_GetParentTechnique(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getParentTechnique();
}
EXPORT void ParticleRenderer_SetParentTechnique(ParticleUniverse::ParticleRenderer* ptr, ParticleUniverse::ParticleTechnique* parentTechnique)
{
	return ptr->setParentTechnique(parentTechnique);
}
EXPORT bool ParticleRenderer_IsRendererInitialised(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->isRendererInitialised();
}
EXPORT void ParticleRenderer_SetRendererInitialised(ParticleUniverse::ParticleRenderer* ptr, bool rendererInitialised)
{
	return ptr->setRendererInitialised(rendererInitialised);
}
EXPORT void ParticleRenderer__notifyStart(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->_notifyStart();
}
EXPORT void ParticleRenderer__notifyStop(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->_notifyStop();
}
EXPORT void ParticleRenderer__notifyRescaled(ParticleUniverse::ParticleRenderer* ptr, const Ogre::Vector3* scale)
{
	return ptr->_notifyRescaled(*scale);
}
EXPORT void ParticleRenderer_SetVisible(ParticleUniverse::ParticleRenderer* ptr, bool visible = true)
{
	return ptr->setVisible(visible);
}
EXPORT void ParticleRenderer__prepare(ParticleUniverse::ParticleRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	return ptr->_prepare(technique);
}
EXPORT void ParticleRenderer__unprepare(ParticleUniverse::ParticleRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	return ptr->_unprepare(particleTechnique);
}
EXPORT void ParticleRenderer__processParticle(ParticleUniverse::ParticleRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed, bool firstParticle)
{
	return ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT bool ParticleRenderer_GetUseSoftParticles(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getUseSoftParticles();
}
EXPORT void ParticleRenderer_SetUseSoftParticles(ParticleUniverse::ParticleRenderer* ptr, bool useSoftParticles)
{
	return ptr->setUseSoftParticles(useSoftParticles);
}
EXPORT float ParticleRenderer_GetSoftParticlesContrastPower(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getSoftParticlesContrastPower();
}
EXPORT float ParticleRenderer_GetSoftParticlesScale(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getSoftParticlesScale();
}
EXPORT float ParticleRenderer_GetSoftParticlesDelta(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getSoftParticlesDelta();
}
EXPORT void ParticleRenderer_SetSoftParticlesContrastPower(ParticleUniverse::ParticleRenderer* ptr, float softParticlesContrastPower)
{
	return ptr->setSoftParticlesContrastPower(softParticlesContrastPower);
}
EXPORT void ParticleRenderer_SetSoftParticlesScale(ParticleUniverse::ParticleRenderer* ptr, float softParticlesScale)
{
	return ptr->setSoftParticlesScale(softParticlesScale);
}
EXPORT void ParticleRenderer_SetSoftParticlesDelta(ParticleUniverse::ParticleRenderer* ptr, float softParticlesDelta)
{
	return ptr->setSoftParticlesDelta(softParticlesDelta);
}
EXPORT void ParticleRenderer__updateRenderQueue(ParticleUniverse::ParticleRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	return ptr->_updateRenderQueue(queue, pool);
}
EXPORT void ParticleRenderer__setMaterialName(ParticleUniverse::ParticleRenderer* ptr, char* materialName)
{
	return ptr->_setMaterialName(materialName);
}
EXPORT void ParticleRenderer__notifyCurrentCamera(ParticleUniverse::ParticleRenderer* ptr, Ogre::Camera* cam)
{
	return ptr->_notifyCurrentCamera(cam);
}
EXPORT void ParticleRenderer__notifyAttached(ParticleUniverse::ParticleRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	return ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void ParticleRenderer__notifyParticleQuota(ParticleUniverse::ParticleRenderer* ptr, size_t quota)
{
	return ptr->_notifyParticleQuota(quota);
}
EXPORT void ParticleRenderer__notifyDefaultDimensions(ParticleUniverse::ParticleRenderer* ptr, float width, float height, float depth)
{
	return ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void ParticleRenderer__notifyParticleResized(ParticleUniverse::ParticleRenderer* ptr) 
{
	return ptr->_notifyParticleResized();
}
EXPORT void ParticleRenderer__notifyParticleZRotated(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->_notifyParticleZRotated();
}
EXPORT void ParticleRenderer_SetRenderQueueGroup(ParticleUniverse::ParticleRenderer* ptr, unsigned char queueId)
{
	return ptr->setRenderQueueGroup(queueId);
}
EXPORT unsigned char ParticleRenderer_GetRenderQueueGroup(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getRenderQueueGroup();
}
EXPORT ParticleUniverse::SortMode ParticleRenderer__getSortMode(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT bool ParticleRenderer_IsSorted(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->isSorted();
}
EXPORT void ParticleRenderer_SetSorted(ParticleUniverse::ParticleRenderer* ptr, bool sorted)
{
	return ptr->setSorted(sorted);
}
EXPORT unsigned char ParticleRenderer_GetTextureCoordsRows(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getTextureCoordsRows();
}
EXPORT void ParticleRenderer_SetTextureCoordsRows(ParticleUniverse::ParticleRenderer* ptr, unsigned char const textureCoordsRows)
{
	return ptr->setTextureCoordsRows(textureCoordsRows);
}
EXPORT const unsigned char ParticleRenderer_GetTextureCoordsColumns(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getTextureCoordsColumns();
}
EXPORT void ParticleRenderer_SetTextureCoordsColumns(ParticleUniverse::ParticleRenderer* ptr, unsigned char const textureCoordsColumns)
{
	return ptr->setTextureCoordsColumns(textureCoordsColumns);
}
EXPORT size_t ParticleRenderer_GetNumTextureCoords(ParticleUniverse::ParticleRenderer* ptr)
{
	return ptr->getNumTextureCoords();
}
EXPORT void ParticleRenderer_CopyAttributesTo(ParticleUniverse::ParticleRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	return ptr->copyAttributesTo(renderer);
}
EXPORT void ParticleRenderer_CopyParentAttributesTo(ParticleUniverse::ParticleRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	return ptr->copyParentAttributesTo(renderer);
}
EXPORT void ParticleRenderer_AddTextureCoords(ParticleUniverse::ParticleRenderer* ptr, float u, float v, float width, float height)
{
	return ptr->addTextureCoords(u, v, width, height);
}
EXPORT int ParticleRenderer_GetTextureCoordsCount(ParticleUniverse::ParticleRenderer* ptr)
{
	ParticleUniverse::vector<Ogre::FloatRect*> lines = ptr->getTextureCoords();
	return lines.size();
}
EXPORT void ParticleRenderer_GetTextureCoords(ParticleUniverse::ParticleRenderer* ptr, Ogre::FloatRect** arrLodDistances, int bufSize)
{
	ParticleUniverse::vector<Ogre::FloatRect*> lines = ptr->getTextureCoords();
	
	if (lines.size() > (unsigned int)bufSize) //Avoid buffer over run.
		return;

	ParticleUniverse::vector<Ogre::FloatRect*>::iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      Ogre::FloatRect* name = (Ogre::FloatRect*)*it;
	  *(arrLodDistances + i) = name;
		i++; // = sizeof(name);
    }
	//return arrLodDistances;
}

#pragma endregion
#pragma region Billboard
EXPORT ParticleUniverse::Billboard* Billboard_New()
{
	ParticleUniverse::Billboard* renderer = new ParticleUniverse::Billboard();
	//renderer->("Billboard");
	return renderer;
}
EXPORT void Billboard_Destroy(ParticleUniverse::Billboard* ptr)
{
	ptr->~Billboard();
}
#pragma endregion
#pragma region BeamRenderer
EXPORT ParticleUniverse::BeamRenderer* BeamRenderer_New()
{
	ParticleUniverse::BeamRenderer* renderer = new ParticleUniverse::BeamRenderer();
	renderer->setRendererType("Beam");
	return renderer;
}
EXPORT void BeamRenderer_Destroy(ParticleUniverse::BeamRenderer* ptr)
{
	ptr->~BeamRenderer();
}
EXPORT bool BeamRenderer_IsUseVertexColours(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->isUseVertexColours();
}
EXPORT void BeamRenderer_SetUseVertexColours(ParticleUniverse::BeamRenderer* ptr, bool useVertexColours)
{
	ptr->setUseVertexColours(useVertexColours);
}
EXPORT size_t BeamRenderer_GetMaxChainElements(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->getMaxChainElements();
}
EXPORT void BeamRenderer_SetMaxChainElements(ParticleUniverse::BeamRenderer* ptr, size_t maxChainElements)
{
	ptr->setMaxChainElements(maxChainElements);
}
EXPORT float BeamRenderer_GetUpdateInterval(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->getUpdateInterval();
}
EXPORT void BeamRenderer_SetUpdateInterval(ParticleUniverse::BeamRenderer* ptr, float updateInterval)
{
	ptr->setUpdateInterval(updateInterval);
}
EXPORT float BeamRenderer_GetDeviation(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->getDeviation();
}
EXPORT void BeamRenderer_SetDeviation(ParticleUniverse::BeamRenderer* ptr, float deviation)
{
	ptr->setDeviation(deviation);
}
EXPORT size_t BeamRenderer_GetNumberOfSegments(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->getNumberOfSegments();
}
EXPORT void BeamRenderer_SetNumberOfSegments(ParticleUniverse::BeamRenderer* ptr, size_t numberOfSegments)
{
	ptr->setNumberOfSegments(numberOfSegments);
}
EXPORT bool BeamRenderer_IsJump(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->isJump();
}
EXPORT void BeamRenderer_SetJump(ParticleUniverse::BeamRenderer* ptr, bool jump)
{
	ptr->setJump(jump);
}
EXPORT Ogre::BillboardChain::TexCoordDirection BeamRenderer_GetTexCoordDirection(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->getTexCoordDirection();
}
EXPORT void BeamRenderer_SetTexCoordDirection(ParticleUniverse::BeamRenderer* ptr, Ogre::BillboardChain::TexCoordDirection texCoordDirection)
{
	ptr->setTexCoordDirection(texCoordDirection);
}
EXPORT void BeamRenderer__destroyAll(ParticleUniverse::BeamRenderer* ptr)
{
	ptr->_destroyAll();
}
EXPORT void BeamRenderer__processParticle(ParticleUniverse::BeamRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, 
				ParticleUniverse::Particle* particle, 
				float timeElapsed, 
				bool firstParticle)
{
	ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT void BeamRenderer__prepare(ParticleUniverse::BeamRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void BeamRenderer__unprepare(ParticleUniverse::BeamRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void BeamRenderer__updateRenderQueue(ParticleUniverse::BeamRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	ptr->_updateRenderQueue(queue, pool);
}
EXPORT void BeamRenderer__notifyAttached(ParticleUniverse::BeamRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void BeamRenderer__setMaterialName(ParticleUniverse::BeamRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void BeamRenderer__notifyCurrentCamera(ParticleUniverse::BeamRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void BeamRenderer__notifyParticleQuota(ParticleUniverse::BeamRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void BeamRenderer__notifyDefaultDimensions(ParticleUniverse::BeamRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void BeamRenderer__notifyParticleResized(ParticleUniverse::BeamRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void BeamRenderer__notifyParticleZRotated(ParticleUniverse::BeamRenderer* ptr)
{
	ptr->_notifyParticleZRotated();
}
EXPORT void BeamRenderer_SetRenderQueueGroup(ParticleUniverse::BeamRenderer* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT ParticleUniverse::SortMode BeamRenderer__GetSortMode(ParticleUniverse::BeamRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void BeamRenderer_CopyAttributesTo (ParticleUniverse::BeamRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
EXPORT void BeamRenderer_SetVisible(ParticleUniverse::BeamRenderer* ptr, bool visible)
{
	ptr->setVisible(visible);
}
EXPORT void BeamRenderer_ParticleEmitted(ParticleUniverse::BeamRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleEmitted(particleTechnique, particle);
}
EXPORT void BeamRenderer_ParticleExpired(ParticleUniverse::BeamRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleExpired(particleTechnique, particle);
}
#pragma endregion
#pragma region BillboardRenderer
EXPORT ParticleUniverse::BillboardRenderer* BillboardRenderer_New()
{
	ParticleUniverse::BillboardRenderer* renderer = new ParticleUniverse::BillboardRenderer();
	renderer->setRendererType("Billboard");
	return renderer;
}
EXPORT void BillboardRenderer_Destroy(ParticleUniverse::BillboardRenderer* ptr)
{
	ptr->~BillboardRenderer();
}
EXPORT void BillboardRenderer__prepare(ParticleUniverse::BillboardRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void BillboardRenderer__unprepare(ParticleUniverse::BillboardRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void BillboardRenderer_SetBillboardType(ParticleUniverse::BillboardRenderer* ptr, ParticleUniverse::BillboardRenderer::BillboardType bbt)
{
	ptr->setBillboardType(bbt);
}
EXPORT ParticleUniverse::BillboardRenderer::BillboardType BillboardRenderer_GetBillboardType(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->getBillboardType();
}
EXPORT void BillboardRenderer_SetUseAccurateFacing(ParticleUniverse::BillboardRenderer* ptr, bool acc)
{
	ptr->setUseAccurateFacing(acc);
}
EXPORT bool BillboardRenderer_IsUseAccurateFacing(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->isUseAccurateFacing();
}
EXPORT void BillboardRenderer_SetBillboardOrigin(ParticleUniverse::BillboardRenderer* ptr, Ogre::BillboardOrigin origin)
{
	ptr->setBillboardOrigin(origin);
}
EXPORT Ogre::BillboardOrigin BillboardRenderer_GetBillboardOrigin(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->getBillboardOrigin();
}
EXPORT void BillboardRenderer_SetBillboardRotationType(ParticleUniverse::BillboardRenderer* ptr, Ogre::BillboardRotationType rotationType)
{
	ptr->setBillboardRotationType(rotationType);
}
EXPORT Ogre::BillboardRotationType BillboardRenderer_GetBillboardRotationType(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->getBillboardRotationType();
}
EXPORT void BillboardRenderer_SetCommonDirection(ParticleUniverse::BillboardRenderer* ptr, ParticleUniverse::Vector3* vec)
{
	ptr->setCommonDirection(*vec);
}
EXPORT const ParticleUniverse::Vector3* BillboardRenderer_GetCommonDirection(ParticleUniverse::BillboardRenderer* ptr)
{
	return &ptr->getCommonDirection();
}
EXPORT void BillboardRenderer_SetCommonUpVector(ParticleUniverse::BillboardRenderer* ptr, ParticleUniverse::Vector3* vec)
{
	ptr->setCommonUpVector(*vec);
}
EXPORT const ParticleUniverse::Vector3* BillboardRenderer_GetCommonUpVector(ParticleUniverse::BillboardRenderer* ptr)
{
	return &ptr->getCommonUpVector();
}
EXPORT void BillboardRenderer_SetPointRenderingEnabled(ParticleUniverse::BillboardRenderer* ptr, bool enabled)
{
	ptr->setPointRenderingEnabled(enabled);
}
EXPORT bool BillboardRenderer_IsPointRenderingEnabled(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->isPointRenderingEnabled();
}
EXPORT Ogre::BillboardSet* BillboardRenderer_GetBillboardSet(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->getBillboardSet();
}

EXPORT void BillboardRenderer__updateRenderQueue(ParticleUniverse::BillboardRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	ptr->_updateRenderQueue(queue, pool);
}
EXPORT void BillboardRenderer__notifyAttached(ParticleUniverse::BillboardRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void BillboardRenderer__setMaterialName(ParticleUniverse::BillboardRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void BillboardRenderer__notifyCurrentCamera(ParticleUniverse::BillboardRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void BillboardRenderer__notifyParticleQuota(ParticleUniverse::BillboardRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void BillboardRenderer__notifyDefaultDimensions(ParticleUniverse::BillboardRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void BillboardRenderer__notifyParticleResized(ParticleUniverse::BillboardRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void BillboardRenderer__notifyParticleZRotated(ParticleUniverse::BillboardRenderer* ptr)
{
	ptr->_notifyParticleZRotated();
}
EXPORT void BillboardRenderer_SetRenderQueueGroup(ParticleUniverse::BillboardRenderer* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT ParticleUniverse::SortMode BillboardRenderer__GetSortMode(ParticleUniverse::BillboardRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void BillboardRenderer_CopyAttributesTo (ParticleUniverse::BillboardRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
EXPORT void BillboardRenderer_SetVisible(ParticleUniverse::BillboardRenderer* ptr, bool visible)
{
	ptr->setVisible(visible);
}
#pragma endregion
#pragma region BoxRenderer
EXPORT ParticleUniverse::BoxRenderer* BoxRenderer_New()
{
	ParticleUniverse::BoxRenderer* renderer = new ParticleUniverse::BoxRenderer();
	renderer->setRendererType("Box");
	return renderer;
}
EXPORT void BoxRenderer_Destroy(ParticleUniverse::BoxRenderer* ptr)
{
	ptr->~BoxRenderer();
}
EXPORT void BoxRenderer__prepare(ParticleUniverse::BoxRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void BoxRenderer__unprepare(ParticleUniverse::BoxRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void BoxRenderer__updateRenderQueue(ParticleUniverse::BoxRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	ptr->_updateRenderQueue(queue, pool);
}
EXPORT void BoxRenderer__notifyAttached(ParticleUniverse::BoxRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void BoxRenderer__setMaterialName(ParticleUniverse::BoxRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void BoxRenderer__notifyCurrentCamera(ParticleUniverse::BoxRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void BoxRenderer__notifyParticleQuota(ParticleUniverse::BoxRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void BoxRenderer__notifyDefaultDimensions(ParticleUniverse::BoxRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void BoxRenderer__notifyParticleResized(ParticleUniverse::BoxRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void BoxRenderer__notifyParticleZRotated(ParticleUniverse::BoxRenderer* ptr)
{
	ptr->_notifyParticleZRotated();
}
EXPORT void BoxRenderer_SetRenderQueueGroup(ParticleUniverse::BoxRenderer* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT ParticleUniverse::SortMode BoxRenderer__GetSortMode(ParticleUniverse::BoxRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void BoxRenderer_CopyAttributesTo (ParticleUniverse::BoxRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
EXPORT void BoxRenderer_SetVisible(ParticleUniverse::BoxRenderer* ptr, bool visible)
{
	ptr->setVisible(visible);
}
EXPORT ParticleUniverse::BoxSet* BoxRenderer_GetBoxSet(ParticleUniverse::BoxRenderer* ptr)
{
	return ptr->getBoxSet();
}
#pragma endregion
#pragma region EntityRenderer
EXPORT ParticleUniverse::EntityRenderer* EntityRenderer_New()
{
	ParticleUniverse::EntityRenderer* renderer = new ParticleUniverse::EntityRenderer();
	renderer->setRendererType("Entity");
	return renderer;
}
EXPORT void EntityRenderer_Destroy(ParticleUniverse::EntityRenderer* ptr)
{
	ptr->~EntityRenderer();
}
EXPORT const char* EntityRenderer_GetMeshName(ParticleUniverse::EntityRenderer* ptr)
{
	return ptr->getMeshName().c_str();
}
EXPORT void EntityRenderer_SetMeshName(ParticleUniverse::EntityRenderer* ptr, char* meshName)
{
	ptr->setMeshName(meshName);
}
EXPORT void EntityRenderer__destroyAll(ParticleUniverse::EntityRenderer* ptr)
{
	ptr->_destroyAll();
}
EXPORT void EntityRenderer__notifyStop(ParticleUniverse::EntityRenderer* ptr)
{
	ptr->_notifyStop();
}
EXPORT void EntityRenderer__rotateTexture(ParticleUniverse::EntityRenderer* ptr, ParticleUniverse::VisualParticle* particle, Ogre::Entity* entity)
{
	ptr->_rotateTexture(particle, entity);
}
EXPORT const ParticleUniverse::EntityRenderer::EntityOrientationType EntityRenderer_GetEntityOrientationType(ParticleUniverse::EntityRenderer* ptr)
{
	return ptr->getEntityOrientationType();
}
EXPORT void EntityRenderer_SetEntityOrientationType(ParticleUniverse::EntityRenderer* ptr, ParticleUniverse::EntityRenderer::EntityOrientationType entityOrientationType)
{
	ptr->setEntityOrientationType(entityOrientationType);
}
EXPORT void EntityRenderer_SetVisible(ParticleUniverse::EntityRenderer* ptr, bool visible)
{
	ptr->setVisible(visible);
}
EXPORT void EntityRenderer__prepare(ParticleUniverse::EntityRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void EntityRenderer__unprepare(ParticleUniverse::EntityRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void EntityRenderer__updateRenderQueue(ParticleUniverse::EntityRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	ptr->_updateRenderQueue(queue, pool);
}
EXPORT void EntityRenderer__notifyAttached(ParticleUniverse::EntityRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void EntityRenderer__setMaterialName(ParticleUniverse::EntityRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void EntityRenderer__notifyCurrentCamera(ParticleUniverse::EntityRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void EntityRenderer__notifyParticleQuota(ParticleUniverse::EntityRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void EntityRenderer__notifyDefaultDimensions(ParticleUniverse::EntityRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void EntityRenderer__notifyParticleResized(ParticleUniverse::EntityRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void EntityRenderer__notifyParticleZRotated(ParticleUniverse::EntityRenderer* ptr)
{
	ptr->_notifyParticleZRotated();
}
EXPORT void EntityRenderer_SetRenderQueueGroup(ParticleUniverse::EntityRenderer* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT ParticleUniverse::SortMode EntityRenderer__GetSortMode(ParticleUniverse::EntityRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void EntityRenderer_CopyAttributesTo (ParticleUniverse::EntityRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
#pragma endregion
#pragma region LightRenderer
EXPORT ParticleUniverse::LightRenderer* LightRenderer_New()
{
	ParticleUniverse::LightRenderer* renderer = new ParticleUniverse::LightRenderer();
	renderer->setRendererType("Light");
	return renderer;
}
EXPORT void LightRenderer_Destroy(ParticleUniverse::LightRenderer* ptr)
{
	ptr->~LightRenderer();
}
EXPORT Ogre::Light::LightTypes LightRenderer_GetLightType(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getLightType();
}
EXPORT void LightRenderer_SetLightType(ParticleUniverse::LightRenderer* ptr, Ogre::Light::LightTypes lightType)
{
	ptr->setLightType(lightType);
}
EXPORT const Ogre::ColourValue* LightRenderer_GetSpecularColour(ParticleUniverse::LightRenderer* ptr)
{
	return &ptr->getSpecularColour();
}
EXPORT void LightRenderer_SetSpecularColour(ParticleUniverse::LightRenderer* ptr, const Ogre::ColourValue* specularColour)
{
	ptr->setSpecularColour(*specularColour);
}
EXPORT float LightRenderer_GetAttenuationRange(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getAttenuationRange();
}
EXPORT void LightRenderer_SetAttenuationRange(ParticleUniverse::LightRenderer* ptr, float attenuationRange)
{
	ptr->setAttenuationRange(attenuationRange);
}
EXPORT float LightRenderer_GetAttenuationConstant(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getAttenuationConstant();
}
EXPORT void LightRenderer_SetAttenuationConstant(ParticleUniverse::LightRenderer* ptr, float attenuationConstant)
{
	ptr->setAttenuationConstant(attenuationConstant);
}
EXPORT float LightRenderer_GetAttenuationLinear(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getAttenuationLinear();
}
EXPORT void LightRenderer_SetAttenuationLinear(ParticleUniverse::LightRenderer* ptr, float attenuationLinear)
{
	ptr->setAttenuationLinear(attenuationLinear);
}
EXPORT float LightRenderer_GetAttenuationQuadratic(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getAttenuationQuadratic();
}
EXPORT void LightRenderer_SetAttenuationQuadratic(ParticleUniverse::LightRenderer* ptr, float attenuationQuadratic)
{
	ptr->setAttenuationQuadratic(attenuationQuadratic);
}
EXPORT const ParticleUniverse::Radian* LightRenderer_GetSpotlightInnerAngle(ParticleUniverse::LightRenderer* ptr)
{
	return &ptr->getSpotlightInnerAngle();
}
EXPORT void LightRenderer_SetSpotlightInnerAngle(ParticleUniverse::LightRenderer* ptr, const ParticleUniverse::Radian* spotlightInnerAngle)
{
	ptr->setSpotlightInnerAngle(*spotlightInnerAngle);
}
EXPORT const ParticleUniverse::Radian* LightRenderer_GetSpotlightOuterAngle(ParticleUniverse::LightRenderer* ptr)
{
	return &ptr->getSpotlightOuterAngle();
}
EXPORT void LightRenderer_SetSpotlightOuterAngle(ParticleUniverse::LightRenderer* ptr, ParticleUniverse::Radian* spotlightOuterAngle)
{
	ptr->setSpotlightOuterAngle(*spotlightOuterAngle);
}
EXPORT float LightRenderer_GetSpotlightFalloff(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getSpotlightFalloff();
}
EXPORT void LightRenderer_SetSpotlightFalloff(ParticleUniverse::LightRenderer* ptr, float spotlightFalloff)
{
	ptr->setSpotlightFalloff(spotlightFalloff);
}
EXPORT float LightRenderer_GetPowerScale(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getPowerScale();
}
EXPORT void LightRenderer_SetPowerScale(ParticleUniverse::LightRenderer* ptr, float powerScale)
{
	ptr->setPowerScale(powerScale);
}
EXPORT float LightRenderer_GetFlashFrequency(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getFlashFrequency();
}
EXPORT void LightRenderer_SetFlashFrequency(ParticleUniverse::LightRenderer* ptr, float flashFrequency)
{
	ptr->setFlashFrequency(flashFrequency);
}
EXPORT float LightRenderer_GetFlashLength(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->getFlashLength();
}
EXPORT void LightRenderer_SetFlashLength(ParticleUniverse::LightRenderer* ptr, float flashLength)
{
	ptr->setFlashLength(flashLength);
}
EXPORT bool LightRenderer_IsFlashRandom(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->isFlashRandom();
}
EXPORT void LightRenderer_SetFlashRandom(ParticleUniverse::LightRenderer* ptr, bool flashRandom)
{
	ptr->setFlashRandom(flashRandom);
}
EXPORT void LightRenderer__destroyAll(ParticleUniverse::LightRenderer* ptr)
{
	ptr->_destroyAll();
}
EXPORT void LightRenderer__processParticle(ParticleUniverse::LightRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, 
				ParticleUniverse::Particle* particle, 
				float timeElapsed, 
				bool firstParticle)
{
	ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT void LightRenderer__prepare(ParticleUniverse::LightRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void LightRenderer__unprepare(ParticleUniverse::LightRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void LightRenderer__setMaterialName(ParticleUniverse::LightRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void LightRenderer__notifyCurrentCamera(ParticleUniverse::LightRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void LightRenderer__notifyAttached(ParticleUniverse::LightRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void LightRenderer__notifyParticleQuota(ParticleUniverse::LightRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void LightRenderer__notifyDefaultDimensions(ParticleUniverse::LightRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void LightRenderer__notifyParticleResized(ParticleUniverse::LightRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT ParticleUniverse::SortMode LightRenderer__GetSortMode(ParticleUniverse::LightRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void LightRenderer_CopyAttributesTo (ParticleUniverse::LightRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
#pragma endregion
#pragma region RibbonTrailRenderer
EXPORT ParticleUniverse::RibbonTrailRenderer* RibbonTrailRenderer_New()
{
	ParticleUniverse::RibbonTrailRenderer* renderer = new ParticleUniverse::RibbonTrailRenderer();
	renderer->setRendererType("RibbonTrail");
	return renderer;
}
EXPORT void RibbonTrailRenderer_Destroy(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	ptr->~RibbonTrailRenderer();
}
EXPORT void RibbonTrailRenderer__notifyRescaled(ParticleUniverse::RibbonTrailRenderer* ptr, const ParticleUniverse::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT bool RibbonTrailRenderer_IsUseVertexColours(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return ptr->isUseVertexColours();
}
EXPORT void RibbonTrailRenderer_SetUseVertexColours(ParticleUniverse::RibbonTrailRenderer* ptr, bool useVertexColours)
{
	ptr->setUseVertexColours(useVertexColours);
}
EXPORT size_t RibbonTrailRenderer_GetMaxChainElements(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return ptr->getMaxChainElements();
}
EXPORT void RibbonTrailRenderer_SetMaxChainElements(ParticleUniverse::RibbonTrailRenderer* ptr, size_t maxChainElements)
{
	ptr->setMaxChainElements(maxChainElements);
}
EXPORT float RibbonTrailRenderer_GetTrailLength(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return ptr->getTrailLength();
}
EXPORT void RibbonTrailRenderer_SetTrailLength(ParticleUniverse::RibbonTrailRenderer* ptr, float trailLength)
{
	ptr->setTrailLength(trailLength);
}
EXPORT float RibbonTrailRenderer_GetTrailWidth(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return ptr->getTrailWidth();
}
EXPORT void RibbonTrailRenderer_SetTrailWidth(ParticleUniverse::RibbonTrailRenderer* ptr, float trailWidth)
{
	ptr->setTrailWidth(trailWidth);
}
EXPORT bool RibbonTrailRenderer_IsRandomInitialColour(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return ptr->isRandomInitialColour();
}
EXPORT void RibbonTrailRenderer_SetRandomInitialColour(ParticleUniverse::RibbonTrailRenderer* ptr, bool randomInitialColour)
{
	ptr->setRandomInitialColour(randomInitialColour);
}
EXPORT const ParticleUniverse::ColourValue* RibbonTrailRenderer_GetInitialColour(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return &ptr->getInitialColour();
}
EXPORT void RibbonTrailRenderer_SetInitialColour(ParticleUniverse::RibbonTrailRenderer* ptr, Ogre::ColourValue* initialColour)
{
	ptr->setInitialColour(*initialColour);
}
EXPORT const ParticleUniverse::ColourValue* RibbonTrailRenderer_GetColourChange(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return &ptr->getColourChange();
}
EXPORT void RibbonTrailRenderer_SetColourChange(ParticleUniverse::RibbonTrailRenderer* ptr, const Ogre::ColourValue* colourChange)
{
	ptr->setColourChange(*colourChange);
}
EXPORT void RibbonTrailRenderer__destroyAll(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	ptr->_destroyAll();
}
EXPORT void RibbonTrailRenderer_SetVisible(ParticleUniverse::RibbonTrailRenderer* ptr, bool visible)
{
	ptr->setVisible(visible);
}
EXPORT void RibbonTrailRenderer__prepare(ParticleUniverse::RibbonTrailRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void RibbonTrailRenderer__unprepare(ParticleUniverse::RibbonTrailRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void RibbonTrailRenderer__updateRenderQueue(ParticleUniverse::RibbonTrailRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	ptr->_updateRenderQueue(queue, pool);
}
EXPORT void RibbonTrailRenderer__notifyAttached(ParticleUniverse::RibbonTrailRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void RibbonTrailRenderer__setMaterialName(ParticleUniverse::RibbonTrailRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void RibbonTrailRenderer__notifyCurrentCamera(ParticleUniverse::RibbonTrailRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void RibbonTrailRenderer__notifyParticleQuota(ParticleUniverse::RibbonTrailRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void RibbonTrailRenderer__notifyDefaultDimensions(ParticleUniverse::RibbonTrailRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void RibbonTrailRenderer__notifyParticleResized(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void RibbonTrailRenderer__notifyParticleZRotated(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	ptr->_notifyParticleZRotated();
}
EXPORT void RibbonTrailRenderer_SetRenderQueueGroup(ParticleUniverse::RibbonTrailRenderer* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT ParticleUniverse::SortMode RibbonTrailRenderer__GetSortMode(ParticleUniverse::RibbonTrailRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void RibbonTrailRenderer_CopyAttributesTo (ParticleUniverse::RibbonTrailRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
EXPORT void RibbonTrailRenderer_ParticleEmitted(ParticleUniverse::RibbonTrailRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleEmitted(particleTechnique, particle);
}
EXPORT void RibbonTrailRenderer_ParticleExpired(ParticleUniverse::RibbonTrailRenderer* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleExpired(particleTechnique, particle);
}
#pragma endregion
#pragma region SphereRenderer
EXPORT ParticleUniverse::SphereRenderer* SphereRenderer_New()
{
	ParticleUniverse::SphereRenderer* renderer = new ParticleUniverse::SphereRenderer();
	renderer->setRendererType("Sphere");
	return renderer;
}
EXPORT void SphereRenderer_Destroy(ParticleUniverse::SphereRenderer* ptr)
{
	ptr->~SphereRenderer();
}
EXPORT void SphereRenderer__prepare(ParticleUniverse::SphereRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_prepare(technique);
}
EXPORT void SphereRenderer__unprepare(ParticleUniverse::SphereRenderer* ptr, ParticleUniverse::ParticleTechnique* technique)
{
	ptr->_unprepare(technique);
}
EXPORT void SphereRenderer__updateRenderQueue(ParticleUniverse::SphereRenderer* ptr, Ogre::RenderQueue* queue, ParticleUniverse::ParticlePool* pool)
{
	ptr->_updateRenderQueue(queue, pool);
}
EXPORT void SphereRenderer__notifyAttached(ParticleUniverse::SphereRenderer* ptr, Ogre::Node* parent, bool isTagPoint = false)
{
	ptr->_notifyAttached(parent, isTagPoint);
}
EXPORT void SphereRenderer__setMaterialName(ParticleUniverse::SphereRenderer* ptr, char* materialName)
{
	ptr->_setMaterialName(materialName);
}
EXPORT void SphereRenderer__notifyCurrentCamera(ParticleUniverse::SphereRenderer* ptr, Ogre::Camera* cam)
{
	ptr->_notifyCurrentCamera(cam);
}
EXPORT void SphereRenderer__notifyParticleQuota(ParticleUniverse::SphereRenderer* ptr, size_t quota)
{
	ptr->_notifyParticleQuota(quota);
}
EXPORT void SphereRenderer__notifyDefaultDimensions(ParticleUniverse::SphereRenderer* ptr, float width, float height, float depth)
{
	ptr->_notifyDefaultDimensions(width, height, depth);
}
EXPORT void SphereRenderer__notifyParticleResized(ParticleUniverse::SphereRenderer* ptr)
{
	ptr->_notifyParticleResized();
}
EXPORT void SphereRenderer__notifyParticleZRotated(ParticleUniverse::SphereRenderer* ptr)
{
	ptr->_notifyParticleZRotated();
}
EXPORT void SphereRenderer_SetRenderQueueGroup(ParticleUniverse::SphereRenderer* ptr, char queueId)
{
	ptr->setRenderQueueGroup(queueId);
}
EXPORT ParticleUniverse::SortMode SphereRenderer__GetSortMode(ParticleUniverse::SphereRenderer* ptr)
{
	return ptr->_getSortMode();
}
EXPORT void SphereRenderer_CopyAttributesTo (ParticleUniverse::SphereRenderer* ptr, ParticleUniverse::ParticleRenderer* renderer)
{
	ptr->copyAttributesTo(renderer);
}
EXPORT ParticleUniverse::SphereSet* SphereRenderer_GetSphereSet(ParticleUniverse::SphereRenderer* ptr)
{
	return ptr->getSphereSet();
}
EXPORT void SphereRenderer_SetVisible(ParticleUniverse::SphereRenderer* ptr, bool visible)
{
	ptr->setVisible(visible);
}
#pragma endregion


#pragma region Emitter Exports
EXPORT ParticleUniverse::ParticleEmitter* ParticleEmitter_New()
{
	return new ParticleUniverse::ParticleEmitter();
}
EXPORT void ParticleEmitter_Destroy(ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->~ParticleEmitter();
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleEmitter_GetParentTechnique(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getParentTechnique();
}
EXPORT void ParticleEmitter_SetParentTechnique(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleTechnique* parentTechnique)
{
	ptr->setParentTechnique(parentTechnique);
}
EXPORT const char* ParticleEmitter_GetEmitterType(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getEmitterType().c_str();
}
EXPORT void ParticleEmitter_SetEmitterType(ParticleUniverse::ParticleEmitter* ptr, const char* emitterType)
{
	ptr->setEmitterType(emitterType);
}
EXPORT const char* ParticleEmitter_GetName(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getName().c_str();
}
EXPORT void ParticleEmitter_SetName(ParticleUniverse::ParticleEmitter* ptr, const char* name)
{
	ptr->setName(name);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynAngle(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynAngle();
}
EXPORT void ParticleEmitter_SetDynAngle(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynAngle)
{
	ptr->setDynAngle(dynAngle);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynEmissionRate(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynEmissionRate();
}
EXPORT void ParticleEmitter_SetDynEmissionRate(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynEmissionRate)
{
	ptr->setDynEmissionRate(dynEmissionRate);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynTotalTimeToLive(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynTotalTimeToLive();
}
EXPORT void ParticleEmitter_SetDynTotalTimeToLive(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynTotalTimeToLive)
{
	ptr->setDynTotalTimeToLive(dynTotalTimeToLive);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynParticleMass(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynParticleMass();
}
EXPORT void ParticleEmitter_SetDynParticleMass(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynParticleMass)
{
	ptr->setDynParticleMass(dynParticleMass);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynVelocity(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynVelocity();
}
EXPORT void ParticleEmitter_SetDynVelocity(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynVelocity)
{
	ptr->setDynVelocity(dynVelocity);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynDuration(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynDuration();
}
EXPORT void ParticleEmitter_SetDynDuration(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynDuration)
{
	ptr->setDynDuration(dynDuration);
}
EXPORT void ParticleEmitter_SetDynDurationSet(ParticleUniverse::ParticleEmitter* ptr, bool durationSet)
{
	ptr->setDynDurationSet(durationSet);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynRepeatDelay(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynRepeatDelay();
}
EXPORT void ParticleEmitter_SetDynRepeatDelay(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynRepeatDelay)
{
	ptr->setDynRepeatDelay(dynRepeatDelay);
}
EXPORT void ParticleEmitter_SetDynRepeatDelaySet(ParticleUniverse::ParticleEmitter* ptr, bool repeatDelaySet)
{
	ptr->setDynRepeatDelaySet(repeatDelaySet);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynParticleAllDimensions(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynParticleAllDimensions();
}
EXPORT void ParticleEmitter_SetDynParticleAllDimensions(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynParticleAllDimensions)
{
	ptr->setDynParticleAllDimensions(dynParticleAllDimensions);
}
EXPORT void ParticleEmitter_SetDynParticleAllDimensionsSet(ParticleUniverse::ParticleEmitter* ptr, bool particleAllDimensionsSet)
{
	ptr->setDynParticleAllDimensionsSet(particleAllDimensionsSet);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynParticleWidth(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynParticleWidth();
}
EXPORT void ParticleEmitter_SetDynParticleWidth(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynParticleWidth)
{
	ptr->setDynParticleWidth(dynParticleWidth);
}
EXPORT void ParticleEmitter_SetDynParticleWidthSet(ParticleUniverse::ParticleEmitter* ptr, bool particleWidthSet)
{
	ptr->setDynParticleWidthSet(particleWidthSet);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynParticleHeight(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynParticleHeight();
}
EXPORT void ParticleEmitter_SetDynParticleHeight(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynParticleHeight)
{
	ptr->setDynParticleHeight(dynParticleHeight);
}
EXPORT void ParticleEmitter_SetDynParticleHeightSet(ParticleUniverse::ParticleEmitter* ptr, bool particleHeightSet)
{
	ptr->setDynParticleHeightSet(particleHeightSet);
}
EXPORT ParticleUniverse::DynamicAttribute* ParticleEmitter_GetDynParticleDepth(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getDynParticleDepth();
}
EXPORT void ParticleEmitter_SetDynParticleDepth(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::DynamicAttribute* dynParticleDepth)
{
	ptr->setDynParticleDepth(dynParticleDepth);
}
EXPORT void ParticleEmitter_SetDynParticleDepthSet(ParticleUniverse::ParticleEmitter* ptr, bool particleDepthSet)
{
	ptr->setDynParticleDepthSet(particleDepthSet);
}
EXPORT ParticleUniverse::Particle::ParticleType ParticleEmitter_GetEmitsType(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getEmitsType();
}
EXPORT void ParticleEmitter_SetEmitsType(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle::ParticleType emitsType)
{
	ptr->setEmitsType(emitsType);
}
EXPORT const char* ParticleEmitter_GetEmitsName(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getEmitsName().c_str();
}
EXPORT void ParticleEmitter_SetEmitsName(ParticleUniverse::ParticleEmitter* ptr, const char* emitsName)
{
	ptr->setEmitsName(emitsName);
}
EXPORT void ParticleEmitter_SetEmissionRateCameraDependency(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::CameraDependency* cameraDependency)
{
	ptr->setEmissionRateCameraDependency(cameraDependency);
}
EXPORT void ParticleEmitter_SetEmissionRateCameraDependency2(ParticleUniverse::ParticleEmitter* ptr, float squareDistance, bool inc = false)
{
	ptr->setEmissionRateCameraDependency(squareDistance, inc);
}
EXPORT ParticleUniverse::CameraDependency* ParticleEmitter_GetEmissionRateCameraDependency(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getEmissionRateCameraDependency();
}
EXPORT const ParticleUniverse::Vector3* ParticleEmitter_GetParticleDirection(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleDirection();
}
EXPORT const ParticleUniverse::Vector3* ParticleEmitter_GetOriginalParticleDirection(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getOriginalParticleDirection();
}
EXPORT const ParticleUniverse::Quaternion* ParticleEmitter_GetParticleOrientation(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleOrientation();
}
EXPORT void ParticleEmitter_SetParticleOrientation(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Quaternion* orientation)
{
	ptr->setParticleOrientation(*orientation);
}
EXPORT const ParticleUniverse::Quaternion* ParticleEmitter_GetParticleOrientationRangeStart(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleOrientationRangeStart();
}
EXPORT void ParticleEmitter_SetParticleOrientationRangeStart(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Quaternion* orientationRangeStart)
{
	ptr->setParticleOrientationRangeStart(*orientationRangeStart);
}
EXPORT const ParticleUniverse::Quaternion* ParticleEmitter_GetParticleOrientationRangeEnd(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleOrientationRangeEnd();
}
EXPORT void ParticleEmitter_SetParticleOrientationRangeEnd(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Quaternion* orientationRangeEnd)
{
	ptr->setParticleOrientationRangeEnd(*orientationRangeEnd);
}
EXPORT void ParticleEmitter_SetEnabled (ParticleUniverse::ParticleEmitter* ptr, bool enabled)
{
	ptr->setEnabled(enabled);
}
EXPORT void ParticleEmitter_SetParticleDirection(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Vector3* direction)
{
	ptr->setParticleDirection(*direction);
}
EXPORT bool ParticleEmitter_IsAutoDirection(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->isAutoDirection();
}
EXPORT void ParticleEmitter_SetAutoDirection(ParticleUniverse::ParticleEmitter* ptr, bool autoDirection)
{
	ptr->setAutoDirection(autoDirection);
}
EXPORT bool ParticleEmitter_IsForceEmission(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->isForceEmission();
}
EXPORT void ParticleEmitter_SetForceEmission(ParticleUniverse::ParticleEmitter* ptr, bool forceEmission)
{
	ptr->setForceEmission(forceEmission);
}
EXPORT void ParticleEmitter__prepare(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void ParticleEmitter__unprepare(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void ParticleEmitter__preProcessParticles(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void ParticleEmitter__postProcessParticles(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_postProcessParticles(technique, timeElapsed);
}
EXPORT void ParticleEmitter__initForEmission(ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->_initForEmission();
}
EXPORT void ParticleEmitter__initForExpiration(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_initForExpiration(technique, timeElapsed);
}
EXPORT void ParticleEmitter__initParticlePosition(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void ParticleEmitter__initParticleForEmission(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleForEmission(particle);
}
EXPORT void ParticleEmitter__initParticleDirection(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDirection(particle);
}
EXPORT void ParticleEmitter__initParticleOrientation(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleOrientation(particle);
}
EXPORT void ParticleEmitter__generateAngle(ParticleUniverse::ParticleEmitter* ptr, Ogre::Radian* angle)
{
	ptr->_generateAngle(*angle);
}
EXPORT void ParticleEmitter__initParticleVelocity(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleVelocity(particle);
}
EXPORT void ParticleEmitter__initParticleMass(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleMass(particle);
}
EXPORT void ParticleEmitter__initParticleColour(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleColour(particle);
}
EXPORT void ParticleEmitter__initParticleTextureCoords(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleTextureCoords(particle);
}
EXPORT float ParticleEmitter__initParticleTimeToLive(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->_initParticleTimeToLive();
}
EXPORT unsigned short ParticleEmitter__calculateRequestedParticles(ParticleUniverse::ParticleEmitter* ptr, float timeElapsed)
{
	return ptr->_calculateRequestedParticles(timeElapsed);
}
EXPORT void ParticleEmitter__initParticleDimensions(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDimensions(particle);
}
EXPORT void ParticleEmitter__initTimeBased(ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->_initTimeBased();
}
EXPORT const ParticleUniverse::Vector3* ParticleEmitter_GetDerivedPosition(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getDerivedPosition();
}
EXPORT void ParticleEmitter__notifyStart (ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->_notifyStart();
}
EXPORT void ParticleEmitter__notifyStop (ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->_notifyStop();
}
EXPORT void ParticleEmitter__notifyPause (ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->_notifyPause();
}
EXPORT void ParticleEmitter__notifyResume (ParticleUniverse::ParticleEmitter* ptr)
{
	ptr->_notifyResume();
}
EXPORT void ParticleEmitter__notifyRescaled(ParticleUniverse::ParticleEmitter* ptr, Ogre::Vector3& scale)
{
	ptr->_notifyRescaled(scale);
}
EXPORT void ParticleEmitter_CopyAttributesTo (ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
EXPORT void ParticleEmitter_CopyParentAttributesTo (ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyParentAttributesTo(emitter);
}
EXPORT const ParticleUniverse::ColourValue* ParticleEmitter_GetParticleColour(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleColour();
}
EXPORT void ParticleEmitter_SetParticleColour(ParticleUniverse::ParticleEmitter* ptr, Ogre::ColourValue* particleColour)
{
	ptr->setParticleColour(*particleColour);
}
EXPORT const ParticleUniverse::ColourValue* ParticleEmitter_GetParticleColourRangeStart(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleColourRangeStart();
}
EXPORT void ParticleEmitter_SetParticleColourRangeStart(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ColourValue* particleColourRangeStart)
{
	ptr->setParticleColourRangeStart(*particleColourRangeStart);
}
EXPORT const ParticleUniverse::ColourValue* ParticleEmitter_GetParticleColourRangeEnd(ParticleUniverse::ParticleEmitter* ptr)
{
	return &ptr->getParticleColourRangeEnd();
}
EXPORT void ParticleEmitter_SetParticleColourRangeEnd(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ColourValue* particleColourRangeEnd)
{
	ptr->setParticleColourRangeEnd(*particleColourRangeEnd);
}
EXPORT const unsigned short ParticleEmitter_GetParticleTextureCoords(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getParticleTextureCoords();
}
EXPORT void ParticleEmitter_SetParticleTextureCoords(ParticleUniverse::ParticleEmitter* ptr, unsigned short particleTextureCoords)
{
	ptr->setParticleTextureCoords(particleTextureCoords);
}
EXPORT const unsigned short ParticleEmitter_GetParticleTextureCoordsRangeStart(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getParticleTextureCoordsRangeStart();
}
EXPORT void ParticleEmitter_SetParticleTextureCoordsRangeStart(ParticleUniverse::ParticleEmitter* ptr, unsigned short particleTextureCoordsRangeStart)
{
	ptr->setParticleTextureCoordsRangeStart(particleTextureCoordsRangeStart);
}
EXPORT const unsigned short ParticleEmitter_GetParticleTextureCoordsRangeEnd(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->getParticleTextureCoordsRangeEnd();
}
EXPORT void ParticleEmitter_SetParticleTextureCoordsRangeEnd(ParticleUniverse::ParticleEmitter* ptr, unsigned short particleTextureCoordsRangeEnd)
{
	ptr->setParticleTextureCoordsRangeEnd(particleTextureCoordsRangeEnd);
}
EXPORT bool ParticleEmitter_IsKeepLocal(ParticleUniverse::ParticleEmitter* ptr)
{
	return ptr->isKeepLocal();
}
EXPORT void ParticleEmitter_SetKeepLocal(ParticleUniverse::ParticleEmitter* ptr, bool keepLocal)
{
	ptr->setKeepLocal(keepLocal);
}
EXPORT bool ParticleEmitter_MakeParticleLocal(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	return ptr->makeParticleLocal(particle);
}
EXPORT void ParticleEmitter_PushEvent(ParticleUniverse::ParticleEmitter* ptr, ParticleUniverse::ParticleUniverseEvent* particleUniverseEvent)
{
	ptr->pushEvent(*particleUniverseEvent);
}
#pragma endregion
#pragma region BoxEmitter Exports
EXPORT ParticleUniverse::BoxEmitter* BoxEmitter_New()
{
	ParticleUniverse::BoxEmitter* be = new ParticleUniverse::BoxEmitter();
	be->setEmitterType("Box");
	return be;
	//return new ParticleUniverse::();
}
EXPORT void BoxEmitter_Destroy(ParticleUniverse::BoxEmitter* ptr)
{
	ptr->~BoxEmitter();
}
EXPORT float BoxEmitter_GetHeight(ParticleUniverse::BoxEmitter* ptr)
{
	return ptr->getHeight();
}
EXPORT void BoxEmitter_SetHeight(ParticleUniverse::BoxEmitter* ptr, float height)
{
	ptr->setHeight(height);
}
EXPORT float BoxEmitter_GetWidth(ParticleUniverse::BoxEmitter* ptr)
{
	return ptr->getWidth();
}
EXPORT void BoxEmitter_SetWidth(ParticleUniverse::BoxEmitter* ptr, float width)
{
	ptr->setWidth(width);
}
EXPORT float BoxEmitter_GetDepth(ParticleUniverse::BoxEmitter* ptr)
{
	return ptr->getDepth();
}
EXPORT void BoxEmitter_SetDepth(ParticleUniverse::BoxEmitter* ptr, float depth)
{
	ptr->setDepth(depth);
}
EXPORT void BoxEmitter__initParticlePosition(ParticleUniverse::BoxEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void BoxEmitter_CopyAttributesTo (ParticleUniverse::BoxEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region CircleEmitter Exports
EXPORT ParticleUniverse::CircleEmitter* CircleEmitter_New()
{
	ParticleUniverse::CircleEmitter* ce = new ParticleUniverse::CircleEmitter();
	ce->setEmitterType("Circle");
	return ce;
	//return new ParticleUniverse::CircleEmitter();
}
EXPORT void CircleEmitter_Destroy(ParticleUniverse::CircleEmitter* ptr)
{
	ptr->~CircleEmitter();
}
EXPORT float CircleEmitter_GetRadius(ParticleUniverse::CircleEmitter* ptr)
{
	return ptr->getRadius();
}
EXPORT void CircleEmitter_SetRadius(ParticleUniverse::CircleEmitter* ptr, float radius)
{
	ptr->setRadius(radius);
}
EXPORT float CircleEmitter_GetCircleAngle(ParticleUniverse::CircleEmitter* ptr)
{
	return ptr->getCircleAngle();
}
EXPORT void CircleEmitter_SetCircleAngle(ParticleUniverse::CircleEmitter* ptr, float circleAngle)
{
	ptr->setCircleAngle(circleAngle);
}
EXPORT float CircleEmitter_GetStep(ParticleUniverse::CircleEmitter* ptr)
{
	return ptr->getStep();
}
EXPORT void CircleEmitter_SetStep(ParticleUniverse::CircleEmitter* ptr, float step)
{
	ptr->setStep(step);
}
EXPORT bool CircleEmitter_IsRandom(ParticleUniverse::CircleEmitter* ptr) 
{
	return ptr->isRandom();
}
EXPORT void CircleEmitter_SetRandom(ParticleUniverse::CircleEmitter* ptr, bool random)
{
	ptr->setRandom(random);
}
EXPORT const ParticleUniverse::Quaternion* CircleEmitter_GetOrientation(ParticleUniverse::CircleEmitter* ptr)
{
	return &ptr->getOrientation();
}
EXPORT const ParticleUniverse::Vector3* CircleEmitter_GetNormal(ParticleUniverse::CircleEmitter* ptr)
{
	return &ptr->getNormal();
}
EXPORT void CircleEmitter_SetNormal(ParticleUniverse::CircleEmitter* ptr, ParticleUniverse::Vector3* normal)
{
	ptr->setNormal(*normal);
}
EXPORT void CircleEmitter__notifyStart(ParticleUniverse::CircleEmitter* ptr)
{
	ptr->_notifyStart();
}
EXPORT void CircleEmitter__initParticlePosition(ParticleUniverse::CircleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void CircleEmitter__initParticleDirection(ParticleUniverse::CircleEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDirection(particle);
}
EXPORT void CircleEmitter_CopyAttributesTo (ParticleUniverse::CircleEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region LineEmitter Exports
EXPORT ParticleUniverse::LineEmitter* LineEmitter_New()
{
	ParticleUniverse::LineEmitter* le = new ParticleUniverse::LineEmitter();
	le->setEmitterType("Line");
	return le;
	//return new ParticleUniverse::LineEmitter();
}
EXPORT void LineEmitter_Destroy(ParticleUniverse::LineEmitter* ptr)
{
	ptr->~LineEmitter();
}
EXPORT void LineEmitter__notifyStart (ParticleUniverse::LineEmitter* ptr)
{
	ptr->_notifyStart();
}
EXPORT unsigned short LineEmitter__calculateRequestedParticles(ParticleUniverse::LineEmitter* ptr, float timeElapsed)
{
	return ptr->_calculateRequestedParticles(timeElapsed);
}
EXPORT float LineEmitter_GetMaxDeviation(ParticleUniverse::LineEmitter* ptr)
{
	return ptr->getMaxDeviation();
}
EXPORT void LineEmitter_SetMaxDeviation(ParticleUniverse::LineEmitter* ptr, float maxDeviation)
{
	ptr->setMaxDeviation(maxDeviation);
}
EXPORT float LineEmitter_GetMaxIncrement(ParticleUniverse::LineEmitter* ptr)
{
	return ptr->getMaxIncrement();
}
EXPORT void LineEmitter_SetMaxIncrement(ParticleUniverse::LineEmitter* ptr, float maxIncrement)
{
	ptr->setMaxIncrement(maxIncrement);
}
EXPORT float LineEmitter_GetMinIncrement(ParticleUniverse::LineEmitter* ptr)
{
	return ptr->getMinIncrement();
}
EXPORT void LineEmitter_SetMinIncrement(ParticleUniverse::LineEmitter* ptr, float minIncrement)
{
	ptr->setMinIncrement(minIncrement);
}
EXPORT const ParticleUniverse::Vector3* LineEmitter_GetEnd(ParticleUniverse::LineEmitter* ptr)
{
	return &ptr->getEnd();
}
EXPORT void LineEmitter_SetEnd(ParticleUniverse::LineEmitter* ptr, ParticleUniverse::Vector3* end)
{
	ptr->setEnd(*end);
}
EXPORT void LineEmitter__notifyRescaled(ParticleUniverse::LineEmitter* ptr, ParticleUniverse::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT void LineEmitter__initParticlePosition(ParticleUniverse::LineEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void LineEmitter__initParticleDirection(ParticleUniverse::LineEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDirection(particle);
}
EXPORT void LineEmitter_CopyAttributesTo (ParticleUniverse::LineEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region MeshSurfaceEmitter Exports
EXPORT ParticleUniverse::MeshSurfaceEmitter* MeshSurfaceEmitter_New()
{
	ParticleUniverse::MeshSurfaceEmitter* mse = new ParticleUniverse::MeshSurfaceEmitter();
	mse->setEmitterType("MeshSurface");
	return mse;
	//return new ParticleUniverse::MeshSurfaceEmitter();
}
EXPORT void MeshSurfaceEmitter_Destroy(ParticleUniverse::MeshSurfaceEmitter* ptr)
{
	ptr->~MeshSurfaceEmitter();
}
EXPORT const char* MeshSurfaceEmitter_GetMeshName(ParticleUniverse::MeshSurfaceEmitter* ptr)
{
	return ptr->getMeshName().c_str();
}
EXPORT void MeshSurfaceEmitter_SetMeshName(ParticleUniverse::MeshSurfaceEmitter* ptr, char* meshName, bool doBuild = true)
{
	ptr->setMeshName(meshName, doBuild);
}
//EXPORT bool MeshSurfaceEmitter_UseNormals (ParticleUniverse::MeshSurfaceEmitter* ptr)
//{
//	return ptr->useNormals();
//}
//EXPORT void MeshSurfaceEmitter_SetUseNormals (ParticleUniverse::MeshSurfaceEmitter* ptr, bool useNormals)
//{
//	ptr->setUseNormals(useNormals);
//}
EXPORT const ParticleUniverse::MeshInfo::MeshSurfaceDistribution MeshSurfaceEmitter_GetDistribution (ParticleUniverse::MeshSurfaceEmitter* ptr)
{
	return ptr->getDistribution();
}
EXPORT void MeshSurfaceEmitter_SetDistribution(ParticleUniverse::MeshSurfaceEmitter* ptr, ParticleUniverse::MeshInfo::MeshSurfaceDistribution distribution)
{
	ptr->setDistribution(distribution);
}
EXPORT const ParticleUniverse::Vector3* MeshSurfaceEmitter_GetScale (ParticleUniverse::MeshSurfaceEmitter* ptr)
{
	return &ptr->getScale();
}
EXPORT void MeshSurfaceEmitter_SetScale (ParticleUniverse::MeshSurfaceEmitter* ptr, const ParticleUniverse::Vector3* scale)
{
	ptr->setScale(*scale);
}
EXPORT void MeshSurfaceEmitter_Build(ParticleUniverse::MeshSurfaceEmitter* ptr)
{
	ptr->build();
}
EXPORT void MeshSurfaceEmitter__prepare(ParticleUniverse::MeshSurfaceEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void MeshSurfaceEmitter__unprepare(ParticleUniverse::MeshSurfaceEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void MeshSurfaceEmitter__initParticlePosition(ParticleUniverse::MeshSurfaceEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT unsigned short MeshSurfaceEmitter__calculateRequestedParticles(ParticleUniverse::MeshSurfaceEmitter* ptr, float timeElapsed)
{
	return ptr->_calculateRequestedParticles(timeElapsed);
}
EXPORT void MeshSurfaceEmitter__initParticleDirection(ParticleUniverse::MeshSurfaceEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDirection(particle);
}
EXPORT void MeshSurfaceEmitter_CopyAttributesTo (ParticleUniverse::MeshSurfaceEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region PointEmitter Exports
EXPORT ParticleUniverse::PointEmitter* PointEmitter_New()
{
	ParticleUniverse::PointEmitter* pe = new ParticleUniverse::PointEmitter();
	pe->setEmitterType("Point");
	return pe;
	//return new ParticleUniverse::PointEmitter();
}
EXPORT void PointEmitter_Destroy(ParticleUniverse::PointEmitter* ptr)
{
	ptr->~PointEmitter();
}
EXPORT void PointEmitter_CopyAttributesTo (ParticleUniverse::PointEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region PositionEmitter Exports
EXPORT ParticleUniverse::PositionEmitter* PositionEmitter_New()
{
	ParticleUniverse::PositionEmitter* pe = new ParticleUniverse::PositionEmitter();
	pe->setEmitterType("Position");
	return pe;
	//return new ParticleUniverse::PositionEmitter();
}
EXPORT void PositionEmitter_Destroy(ParticleUniverse::PositionEmitter* ptr)
{
	ptr->~PositionEmitter();
}
EXPORT bool PositionEmitter_IsRandomized(ParticleUniverse::PositionEmitter* ptr)
{
	return ptr->isRandomized();
}
EXPORT void PositionEmitter_SetRandomized(ParticleUniverse::PositionEmitter* ptr, bool randomized)
{
	ptr->setRandomized(randomized);
}
EXPORT int PositionEmitter_GetPositionsCount(ParticleUniverse::PositionEmitter* ptr)
{
	const ParticleUniverse::vector<ParticleUniverse::Vector3> lines = ptr->getPositions();
	return lines.size();
}
EXPORT void PositionEmitter_GetPositionsCoords(ParticleUniverse::PositionEmitter* ptr, Ogre::Vector3** arrLodDistances, int bufSize)
{
	const ParticleUniverse::vector<ParticleUniverse::Vector3> lines = ptr->getPositions();
	
	if (lines.size() > (unsigned int)bufSize) //Avoid buffer over run.
		return;

	ParticleUniverse::vector<Ogre::Vector3>::const_iterator it;
    int i = 0;
    for ( it = lines.begin(); it != lines.end(); ++it)
    {
      Ogre::Vector3 name = (Ogre::Vector3)*it;
	  *(arrLodDistances + i) = &name;
		i++; // = sizeof(name);
    }
	//return arrLodDistances;
}

EXPORT void PositionEmitter_AddPosition(ParticleUniverse::PositionEmitter* ptr, const ParticleUniverse::Vector3* position)
{
	ptr->addPosition(*position);
}
EXPORT void PositionEmitter_RemoveAllPositions(ParticleUniverse::PositionEmitter* ptr)
{
	ptr->removeAllPositions();
}
EXPORT void PositionEmitter__notifyStart(ParticleUniverse::PositionEmitter* ptr)
{
	ptr->_notifyStart();
}
EXPORT unsigned short PositionEmitter__calculateRequestedParticles(ParticleUniverse::PositionEmitter* ptr, float timeElapsed)
{
	return ptr->_calculateRequestedParticles(timeElapsed);
}
EXPORT void PositionEmitter__initParticlePosition(ParticleUniverse::PositionEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void PositionEmitter_CopyAttributesTo (ParticleUniverse::PositionEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region SlaveEmitter Exports
EXPORT ParticleUniverse::SlaveEmitter* SlaveEmitter_New()
{
	ParticleUniverse::SlaveEmitter* se = new ParticleUniverse::SlaveEmitter();
	se->setEmitterType("Slave");
	return se;
	//return new ParticleUniverse::SlaveEmitter();
}
EXPORT void SlaveEmitter_Destroy(ParticleUniverse::SlaveEmitter* ptr)
{
	ptr->~SlaveEmitter();
}
EXPORT const char* SlaveEmitter_GetMasterTechniqueName(ParticleUniverse::SlaveEmitter* ptr)
{
	return ptr->getMasterTechniqueName().c_str();
}
EXPORT void SlaveEmitter_SetMasterTechniqueName(ParticleUniverse::SlaveEmitter* ptr, char* masterTechniqueName)
{
	ptr->setMasterTechniqueName(masterTechniqueName);
}
EXPORT const char* SlaveEmitter_GetMasterEmitterName(ParticleUniverse::SlaveEmitter* ptr)
{
	return ptr->getMasterEmitterName().c_str();
}
EXPORT void SlaveEmitter__initParticlePosition(ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void SlaveEmitter__initParticleDirection(ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDirection(particle);
}
EXPORT void SlaveEmitter__prepare(ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_prepare(particleTechnique);
}
EXPORT void SlaveEmitter__unprepare(ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique)
{
	ptr->_unprepare(particleTechnique);
}
EXPORT void SlaveEmitter__notifyStart (ParticleUniverse::SlaveEmitter* ptr)
{
	ptr->_notifyStart();
}
EXPORT void SlaveEmitter_ParticleEmitted(ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleEmitted(particleTechnique, particle);
}
EXPORT void SlaveEmitter_ParticleExpired(ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleExpired(particleTechnique, particle);
}
EXPORT void SlaveEmitter_SetMasterEmitterName(ParticleUniverse::SlaveEmitter* ptr, char* masterEmitterName)
{
	ptr->setMasterEmitterName(masterEmitterName);
}
EXPORT void SlaveEmitter_CopyAttributesTo (ParticleUniverse::SlaveEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region SphereSurfaceEmitter Exports
EXPORT ParticleUniverse::SphereSurfaceEmitter* SphereSurfaceEmitter_New()
{
	ParticleUniverse::SphereSurfaceEmitter* se = new ParticleUniverse::SphereSurfaceEmitter();
	se->setEmitterType("SphereSurface");
	return se;
	//return new ParticleUniverse::SphereSurfaceEmitter();
}
EXPORT void SphereSurfaceEmitter_Destroy(ParticleUniverse::SphereSurfaceEmitter* ptr)
{
	ptr->~SphereSurfaceEmitter();
}
EXPORT float SphereSurfaceEmitter_GetRadius(ParticleUniverse::SphereSurfaceEmitter* ptr)
{
	return ptr->getRadius();
}
EXPORT void SphereSurfaceEmitter_SetRadius(ParticleUniverse::SphereSurfaceEmitter* ptr, float radius)
{
	ptr->setRadius(radius);
}
EXPORT void SphereSurfaceEmitter__initParticlePosition(ParticleUniverse::SphereSurfaceEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT void SphereSurfaceEmitter__initParticleDirection(ParticleUniverse::SphereSurfaceEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticleDirection(particle);
}
EXPORT void SphereSurfaceEmitter_CopyAttributesTo (ParticleUniverse::SphereSurfaceEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion
#pragma region VertexEmitter Exports
EXPORT ParticleUniverse::VertexEmitter* VertexEmitter_New()
{
	ParticleUniverse::VertexEmitter* ve = new ParticleUniverse::VertexEmitter();
	ve->setEmitterType("Vertex");
	return ve;
}
EXPORT void VertexEmitter_Destroy(ParticleUniverse::VertexEmitter* ptr)
{
	ptr->~VertexEmitter();
}
EXPORT unsigned short VertexEmitter_GetIterations(ParticleUniverse::VertexEmitter* ptr)
{
	return ptr->getIterations();
}
EXPORT void VertexEmitter_SetIterations(ParticleUniverse::VertexEmitter* ptr, unsigned short iterations)
{
	ptr->setIterations(iterations);
}
EXPORT unsigned short VertexEmitter_GetSegments(ParticleUniverse::VertexEmitter* ptr)
{
	return ptr->getSegments();
}
EXPORT void VertexEmitter_SetSegments(ParticleUniverse::VertexEmitter* ptr, unsigned short segments)
{
	ptr->setSegments(segments);
}
EXPORT unsigned short VertexEmitter_GetStep(ParticleUniverse::VertexEmitter* ptr)
{
	return ptr->getStep();
}
EXPORT void VertexEmitter_SetStep(ParticleUniverse::VertexEmitter* ptr, unsigned short step)
{
	ptr->setStep(step);
}
EXPORT const char* VertexEmitter_GetMeshName(ParticleUniverse::VertexEmitter* ptr)
{
	return ptr->getMeshName().c_str();
}
EXPORT void VertexEmitter__notifyStart (ParticleUniverse::VertexEmitter* ptr)
{
	ptr->_notifyStart();
}
EXPORT void VertexEmitter_SetMeshName(ParticleUniverse::VertexEmitter* ptr, char* meshName)
{
	ptr->setMeshName(meshName);
}
EXPORT void VertexEmitter__initParticlePosition(ParticleUniverse::VertexEmitter* ptr, ParticleUniverse::Particle* particle)
{
	ptr->_initParticlePosition(particle);
}
EXPORT unsigned short VertexEmitter__calculateRequestedParticles(ParticleUniverse::VertexEmitter* ptr, float timeElapsed)
{
	return ptr->_calculateRequestedParticles(timeElapsed);
}
EXPORT void VertexEmitter_CopyAttributesTo (ParticleUniverse::VertexEmitter* ptr, ParticleUniverse::ParticleEmitter* emitter)
{
	ptr->copyAttributesTo(emitter);
}
#pragma endregion

#pragma region ParticleObserver
EXPORT void ParticleObserver_Destroy(ParticleUniverse::ParticleObserver* ptr)
{
	ptr->~ParticleObserver();
}
EXPORT const char* ParticleObserver_GetObserverType(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getObserverType().c_str();
}
EXPORT void ParticleObserver_SetObserverType(ParticleUniverse::ParticleObserver* ptr, const char* observerType) 
{
	ptr->setObserverType(observerType);
}
EXPORT bool ParticleObserver_IsEnabled(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->isEnabled();
}
EXPORT bool ParticleObserver__getOriginalEnabled(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->_getOriginalEnabled();
}
EXPORT void ParticleObserver_SetEnabled(ParticleUniverse::ParticleObserver* ptr, bool enabled)
{
	ptr->setEnabled(enabled);
}
EXPORT void ParticleObserver__resetEnabled(ParticleUniverse::ParticleObserver* ptr)
{
	ptr->_resetEnabled();
}
EXPORT ParticleUniverse::ParticleTechnique* ParticleObserver_GetParentTechnique(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getParentTechnique();
}
EXPORT void ParticleObserver_SetParentTechnique(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleTechnique* parentTechnique)
{
	ptr->setParentTechnique(parentTechnique);
}
EXPORT const char* ParticleObserver_GetName(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getName().c_str();
}
EXPORT void ParticleObserver_SetName(ParticleUniverse::ParticleObserver* ptr, const char* name)
{
	ptr->setName(name);
}
EXPORT const ParticleUniverse::Particle::ParticleType ParticleObserver_GetParticleTypeToObserve(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getParticleTypeToObserve();
}
EXPORT void ParticleObserver_SetParticleTypeToObserve(ParticleUniverse::ParticleObserver* ptr, const ParticleUniverse::Particle::ParticleType particleTypeToObserve)
{
	ptr->setParticleTypeToObserve(particleTypeToObserve);
}
EXPORT void ParticleObserver__notifyStart (ParticleUniverse::ParticleObserver* ptr)
{
	ptr->_notifyStart();
}
EXPORT void ParticleObserver__notifyStop (ParticleUniverse::ParticleObserver* ptr)
{
	ptr->_notifyStop();
}
EXPORT void ParticleObserver__notifyRescaled(ParticleUniverse::ParticleObserver* ptr, const ParticleUniverse::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT void ParticleObserver__preProcessParticles(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void ParticleObserver__processParticle(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed, bool firstParticle)
{
	ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT void ParticleObserver__firstParticle(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, 
				ParticleUniverse::Particle* particle, 
				float timeElapsed)
{
	ptr->_firstParticle(particleTechnique, particle, timeElapsed);
}
EXPORT void ParticleObserver__postProcessParticles(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_postProcessParticles(technique, timeElapsed);
}
EXPORT bool ParticleObserver__observe (ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT ParticleUniverse::ParticleEventHandler* ParticleObserver_CreateEventHandler(ParticleUniverse::ParticleObserver* ptr, const char* eventHandlerType)
{
	return ptr->createEventHandler(eventHandlerType);
}
EXPORT void ParticleObserver_AddEventHandler(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->addEventHandler(eventHandler);
}
EXPORT void ParticleObserver_RemoveEventHandler(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->removeEventHandler(eventHandler);
}
EXPORT ParticleUniverse::ParticleEventHandler* ParticleObserver_GetEventHandler (ParticleUniverse::ParticleObserver* ptr, size_t index)
{
	return ptr->getEventHandler(index);
}
EXPORT ParticleUniverse::ParticleEventHandler* ParticleObserver_GetEventHandler2 (ParticleUniverse::ParticleObserver* ptr, const char* eventHandlerName)
{
	return ptr->getEventHandler(eventHandlerName);
}
EXPORT size_t ParticleObserver_GetNumEventHandlers (ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getNumEventHandlers();
}
EXPORT void ParticleObserver_DestroyEventHandler(ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->destroyEventHandler(eventHandler);
}
EXPORT void ParticleObserver_DestroyEventHandler2 (ParticleUniverse::ParticleObserver* ptr, size_t index)
{
	ptr->destroyEventHandler(index);
}
EXPORT void ParticleObserver_DestroyAllEventHandlers (ParticleUniverse::ParticleObserver* ptr)
{
	ptr->destroyAllEventHandlers();
}
EXPORT void ParticleObserver_CopyAttributesTo (ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
EXPORT void ParticleObserver_CopyParentAttributesTo (ParticleUniverse::ParticleObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyParentAttributesTo(observer);
}
EXPORT float ParticleObserver_GetObserverInterval(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getObserverInterval();
}
EXPORT void ParticleObserver_SetObserverInterval(ParticleUniverse::ParticleObserver* ptr, float observerInterval)
{
	ptr->setObserverInterval(observerInterval);
}
EXPORT bool ParticleObserver_GetObserveUntilEvent(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->getObserveUntilEvent();
}
EXPORT void ParticleObserver_SetObserveUntilEvent(ParticleUniverse::ParticleObserver* ptr, bool observeUntilEvent)
{
	ptr->setObserveUntilEvent(observeUntilEvent);
}
EXPORT bool ParticleObserver_IsParticleTypeToObserveSet(ParticleUniverse::ParticleObserver* ptr)
{
	return ptr->isParticleTypeToObserveSet();
}
#pragma endregion
#pragma region OnClearObserver
EXPORT ParticleUniverse::OnClearObserver* OnClearObserver_New()
{
	ParticleUniverse::OnClearObserver* toReturn = new ParticleUniverse::OnClearObserver();
	toReturn->setObserverType("OnClear");
	return toReturn;
}
EXPORT void OnClearObserver_Destroy(ParticleUniverse::OnClearObserver* ptr)
{
	ptr->~OnClearObserver();
}
EXPORT void OnClearObserver__notifyStart(ParticleUniverse::OnClearObserver* ptr)
{
	ptr->_notifyStart();
}
EXPORT bool OnClearObserver__observe (ParticleUniverse::OnClearObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle,  float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT void OnClearObserver__processParticle(ParticleUniverse::OnClearObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed, bool firstParticle)
{
	ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT void OnClearObserver__postProcessParticles(ParticleUniverse::OnClearObserver* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_postProcessParticles(technique, timeElapsed);
}
#pragma endregion
#pragma region OnCollisionObserver
EXPORT ParticleUniverse::OnCollisionObserver* OnCollisionObserver_New()
{
	ParticleUniverse::OnCollisionObserver* toReturn = new ParticleUniverse::OnCollisionObserver();
	toReturn->setObserverType("OnCollision");
	return toReturn;
}
EXPORT void OnCollisionObserver_Destroy(ParticleUniverse::OnCollisionObserver* ptr)
{
	ptr->~OnCollisionObserver();
}
EXPORT bool OnCollisionObserver__observe (ParticleUniverse::OnCollisionObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region OnCountObserver
EXPORT ParticleUniverse::OnCountObserver* OnCountObserver_New()
{
	ParticleUniverse::OnCountObserver* toReturn = new ParticleUniverse::OnCountObserver();
	toReturn->setObserverType("OnCount");
	return toReturn;
}
EXPORT void OnCountObserver_Destroy(ParticleUniverse::OnCountObserver* ptr)
{
	ptr->~OnCountObserver();
}
EXPORT void OnCountObserver__notifyStart (ParticleUniverse::OnCountObserver* ptr)
{
	ptr->_notifyStart();
}
EXPORT bool OnCountObserver__observe (ParticleUniverse::OnCountObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT unsigned int OnCountObserver_GetThreshold(ParticleUniverse::OnCountObserver* ptr)
{
	return ptr->getThreshold();
}
EXPORT void OnCountObserver_SetThreshold(ParticleUniverse::OnCountObserver* ptr, unsigned int threshold)
{
	ptr->setThreshold(threshold);
}
EXPORT const ParticleUniverse::ComparisionOperator OnCountObserver_GetCompare(ParticleUniverse::OnCountObserver* ptr)
{
	return ptr->getCompare();
}
EXPORT void OnCountObserver_SetCompare(ParticleUniverse::OnCountObserver* ptr, ParticleUniverse::ComparisionOperator op)
{
	ptr->setCompare(op);
}
EXPORT void OnCountObserver_CopyAttributesTo (ParticleUniverse::OnCountObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
#pragma endregion
#pragma region OnEmissionObserver
EXPORT ParticleUniverse::OnEmissionObserver* OnEmissionObserver_New()
{
	ParticleUniverse::OnEmissionObserver* toReturn = new ParticleUniverse::OnEmissionObserver();
	toReturn->setObserverType("OnEmission");
	return toReturn;
}
EXPORT void OnEmissionObserver_Destroy(ParticleUniverse::OnEmissionObserver* ptr)
{
	ptr->~OnEmissionObserver();
}
EXPORT bool OnEmissionObserver__observe (ParticleUniverse::OnEmissionObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region OnEventFlagObserver
EXPORT ParticleUniverse::OnEventFlagObserver* OnEventFlagObserver_New()
{
	ParticleUniverse::OnEventFlagObserver* toReturn = new ParticleUniverse::OnEventFlagObserver();
	toReturn->setObserverType("OnEventFlag");
	return toReturn;
}
EXPORT void OnEventFlagObserver_Destroy(ParticleUniverse::OnEventFlagObserver* ptr)
{
	ptr->~OnEventFlagObserver();
}
EXPORT bool OnEventFlagObserver__observe (ParticleUniverse::OnEventFlagObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT unsigned int OnEventFlagObserver_GetEventFlag(ParticleUniverse::OnEventFlagObserver* ptr)
{
	return ptr->getEventFlag();
}
EXPORT void OnEventFlagObserver_SetEventFlag(ParticleUniverse::OnEventFlagObserver* ptr, unsigned int eventFlag)
{
	ptr->setEventFlag(eventFlag);
}
EXPORT void OnEventFlagObserver_CopyAttributesTo (ParticleUniverse::OnEventFlagObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
#pragma endregion
#pragma region OnExpireObserver
EXPORT ParticleUniverse::OnExpireObserver* OnExpireObserver_New()
{
	ParticleUniverse::OnExpireObserver* toReturn = new ParticleUniverse::OnExpireObserver();
	toReturn->setObserverType("OnExpire");
	return toReturn;
}
EXPORT void OnExpireObserver_Destroy(ParticleUniverse::OnExpireObserver* ptr)
{
	ptr->~OnExpireObserver();
}
EXPORT bool OnExpireObserver__observe (ParticleUniverse::OnExpireObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region OnPositionObserver
EXPORT ParticleUniverse::OnPositionObserver* OnPositionObserver_New()
{
	ParticleUniverse::OnPositionObserver* toReturn = new ParticleUniverse::OnPositionObserver();
	toReturn->setObserverType("OnPosition");
	return toReturn;
}
EXPORT void OnPositionObserver_Destroy(ParticleUniverse::OnPositionObserver* ptr)
{
	ptr->~OnPositionObserver();
}
EXPORT bool OnPositionObserver__observe (ParticleUniverse::OnPositionObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT void OnPositionObserver_SetPositionXThreshold(ParticleUniverse::OnPositionObserver* ptr, float threshold)
{
	ptr->setPositionXThreshold(threshold);
}
EXPORT void OnPositionObserver_SetPositionYThreshold(ParticleUniverse::OnPositionObserver* ptr, float threshold)
{
	ptr->setPositionYThreshold(threshold);
}
EXPORT void OnPositionObserver_SetPositionZThreshold(ParticleUniverse::OnPositionObserver* ptr, float threshold)
{
	ptr->setPositionZThreshold(threshold);
}
EXPORT float OnPositionObserver_GetPositionXThreshold(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->getPositionXThreshold();
}
EXPORT float OnPositionObserver_GetPositionYThreshold(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->getPositionYThreshold();
}
EXPORT float OnPositionObserver_GetPositionZThreshold(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->getPositionZThreshold();
}
EXPORT bool OnPositionObserver_IsPositionXThresholdSet(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->isPositionXThresholdSet();
}
EXPORT bool OnPositionObserver_IsPositionYThresholdSet(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->isPositionYThresholdSet();
}
EXPORT bool OnPositionObserver_IsPositionZThresholdSet(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->isPositionZThresholdSet();
}
EXPORT void OnPositionObserver_ResetPositionXThreshold(ParticleUniverse::OnPositionObserver* ptr)
{
	ptr->resetPositionXThreshold();
}
EXPORT void OnPositionObserver_ResetPositionYThreshold(ParticleUniverse::OnPositionObserver* ptr)
{
	ptr->resetPositionYThreshold();
}
EXPORT void OnPositionObserver_ResetPositionZThreshold(ParticleUniverse::OnPositionObserver* ptr)
{
	ptr->resetPositionZThreshold();
}
EXPORT void OnPositionObserver_SetComparePositionX(ParticleUniverse::OnPositionObserver* ptr, ParticleUniverse::ComparisionOperator op)
{
	ptr->setComparePositionX(op);
}
EXPORT void OnPositionObserver_SetComparePositionY(ParticleUniverse::OnPositionObserver* ptr, ParticleUniverse::ComparisionOperator op)
{
	ptr->setComparePositionY(op);
}
EXPORT void OnPositionObserver_SetComparePositionZ(ParticleUniverse::OnPositionObserver* ptr, ParticleUniverse::ComparisionOperator op)
{
	ptr->setComparePositionZ(op);
}
EXPORT const ParticleUniverse::ComparisionOperator OnPositionObserver_GetComparePositionX(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->getComparePositionX();
}
EXPORT const ParticleUniverse::ComparisionOperator OnPositionObserver_GetComparePositionY(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->getComparePositionY();
}
EXPORT const ParticleUniverse::ComparisionOperator OnPositionObserver_GetComparePositionZ(ParticleUniverse::OnPositionObserver* ptr)
{
	return ptr->getComparePositionZ();
}
EXPORT void OnPositionObserver_CopyAttributesTo (ParticleUniverse::OnPositionObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
#pragma endregion
#pragma region OnQuotaObserver
EXPORT ParticleUniverse::OnQuotaObserver* OnQuotaObserver_New()
{
	ParticleUniverse::OnQuotaObserver* toReturn = new ParticleUniverse::OnQuotaObserver();
	toReturn->setObserverType("OnQuota");
	return toReturn;
}
EXPORT void OnQuotaObserver_Destroy(ParticleUniverse::OnQuotaObserver* ptr)
{
	ptr->~OnQuotaObserver();
}
EXPORT bool OnQuotaObserver__observe (ParticleUniverse::OnQuotaObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT void OnQuotaObserver__postProcessParticles(ParticleUniverse::OnQuotaObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, float timeElapsed)
{
	ptr->_postProcessParticles(particleTechnique, timeElapsed);
}
#pragma endregion
#pragma region OnRandomObserver
EXPORT ParticleUniverse::OnRandomObserver* OnRandomObserver_New()
{
	ParticleUniverse::OnRandomObserver* toReturn = new ParticleUniverse::OnRandomObserver();
	toReturn->setObserverType("OnRandom");
	return toReturn;
}
EXPORT void OnRandomObserver_Destroy(ParticleUniverse::OnRandomObserver* ptr)
{
	ptr->~OnRandomObserver();
}
EXPORT void OnRandomObserver__preProcessParticles(ParticleUniverse::OnRandomObserver* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT void OnRandomObserver__processParticle(ParticleUniverse::OnRandomObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed, bool firstParticle)
{
	ptr->_processParticle(particleTechnique, particle, timeElapsed, firstParticle);
}
EXPORT bool OnRandomObserver__observe (ParticleUniverse::OnRandomObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT float OnRandomObserver_GetThreshold(ParticleUniverse::OnRandomObserver* ptr)
{
	return ptr->getThreshold();
}
EXPORT void OnRandomObserver_SetThreshold(ParticleUniverse::OnRandomObserver* ptr, float threshold)
{
	ptr->setThreshold(threshold);
}
EXPORT void OnRandomObserver_CopyAttributesTo (ParticleUniverse::OnRandomObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
#pragma endregion
#pragma region OnTimeObserver
EXPORT ParticleUniverse::OnTimeObserver* OnTimeObserver_New()
{
	ParticleUniverse::OnTimeObserver* toReturn = new ParticleUniverse::OnTimeObserver();
	toReturn->setObserverType("OnTime");
	return toReturn;
}
EXPORT void OnTimeObserver_Destroy(ParticleUniverse::OnTimeObserver* ptr)
{
	ptr->~OnTimeObserver();
}
EXPORT void OnTimeObserver__preProcessParticles(ParticleUniverse::OnTimeObserver* ptr, ParticleUniverse::ParticleTechnique* technique, float timeElapsed)
{
	ptr->_preProcessParticles(technique, timeElapsed);
}
EXPORT bool OnTimeObserver__observe (ParticleUniverse::OnTimeObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT float OnTimeObserver_GetThreshold(ParticleUniverse::OnTimeObserver* ptr)
{
	return ptr->getThreshold();
}
EXPORT void OnTimeObserver_SetThreshold(ParticleUniverse::OnTimeObserver* ptr, float threshold)
{
	ptr->setThreshold(threshold);
}
EXPORT const ParticleUniverse::ComparisionOperator OnTimeObserver_GetCompare(ParticleUniverse::OnTimeObserver* ptr)
{
	return ptr->getCompare();
}
EXPORT void OnTimeObserver_SetCompare(ParticleUniverse::OnTimeObserver* ptr, ParticleUniverse::ComparisionOperator op)
{
	ptr->setCompare(op);
}
EXPORT bool OnTimeObserver_IsSinceStartSystem(ParticleUniverse::OnTimeObserver* ptr)
{
	return ptr->isSinceStartSystem();
}
EXPORT void OnTimeObserver_SetSinceStartSystem(ParticleUniverse::OnTimeObserver* ptr, bool sinceStartSystem)
{
	ptr->setSinceStartSystem(sinceStartSystem);
}
EXPORT void OnTimeObserver_CopyAttributesTo (ParticleUniverse::OnTimeObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
#pragma endregion
#pragma region OnVelocityObserver
EXPORT ParticleUniverse::OnVelocityObserver* OnVelocityObserver_New()
{
	ParticleUniverse::OnVelocityObserver* toReturn = new ParticleUniverse::OnVelocityObserver();
	toReturn->setObserverType("OnVelocity");
	return toReturn;
}
EXPORT void OnVelocityObserver_Destroy(ParticleUniverse::OnVelocityObserver* ptr)
{
	ptr->~OnVelocityObserver();
}
EXPORT bool OnVelocityObserver__observe (ParticleUniverse::OnVelocityObserver* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	return ptr->_observe(particleTechnique, particle, timeElapsed);
}
EXPORT float OnVelocityObserver_GetThreshold(ParticleUniverse::OnVelocityObserver* ptr)
{
	return ptr->getThreshold();
}
EXPORT void OnVelocityObserver_SetThreshold(ParticleUniverse::OnVelocityObserver* ptr, float threshold)
{
	ptr->setThreshold(threshold);
}
EXPORT const ParticleUniverse::ComparisionOperator OnVelocityObserver_GetCompare(ParticleUniverse::OnVelocityObserver* ptr)
{
	return ptr->getCompare();
}
EXPORT void OnVelocityObserver_SetCompare(ParticleUniverse::OnVelocityObserver* ptr, ParticleUniverse::ComparisionOperator op)
{
	ptr->setCompare(op);
}
EXPORT void OnVelocityObserver_CopyAttributesTo (ParticleUniverse::OnVelocityObserver* ptr, ParticleUniverse::ParticleObserver* observer)
{
	ptr->copyAttributesTo(observer);
}
#pragma endregion

#pragma region ParticleEventHandler
EXPORT void ParticleEventHandler_Destroy(ParticleUniverse::ParticleEventHandler* ptr)
{
	ptr->~ParticleEventHandler();
}
EXPORT const char* ParticleEventHandler_GetName(ParticleUniverse::ParticleEventHandler* ptr)
{
	return ptr->getName().c_str();
}
EXPORT void ParticleEventHandler_SetName(ParticleUniverse::ParticleEventHandler* ptr, const char* name)
{
	ptr->setName(name);
}
EXPORT ParticleUniverse::ParticleObserver* ParticleEventHandler_GetParentObserver(ParticleUniverse::ParticleEventHandler* ptr)
{
	return ptr->getParentObserver();
}
EXPORT void ParticleEventHandler_SetParentObserver(ParticleUniverse::ParticleEventHandler* ptr, ParticleUniverse::ParticleObserver* parentObserver)
{
	ptr->setParentObserver(parentObserver);
}
EXPORT const char* ParticleEventHandler_GetEventHandlerType(ParticleUniverse::ParticleEventHandler* ptr)
{
	return ptr->getEventHandlerType().c_str();
}
EXPORT void ParticleEventHandler_SetEventHandlerType(ParticleUniverse::ParticleEventHandler* ptr, const char* eventHandlerType)
{
	ptr->setEventHandlerType(eventHandlerType);
}
EXPORT void ParticleEventHandler__notifyRescaled(ParticleUniverse::ParticleEventHandler* ptr, const ParticleUniverse::Vector3* scale)
{
	ptr->_notifyRescaled(*scale);
}
EXPORT void ParticleEventHandler__handle(ParticleUniverse::ParticleEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
EXPORT void ParticleEventHandler_CopyAttributesTo (ParticleUniverse::ParticleEventHandler* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->copyAttributesTo(eventHandler);
}
EXPORT void ParticleEventHandler_CopyParentAttributesTo (ParticleUniverse::ParticleEventHandler* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->copyParentAttributesTo(eventHandler);
}
#pragma endregion

#pragma region DoStopSystemEventHandler
EXPORT ParticleUniverse::DoStopSystemEventHandler* DoStopSystemEventHandler_New()
{
	ParticleUniverse::DoStopSystemEventHandler* toReturn = new ParticleUniverse::DoStopSystemEventHandler();
	toReturn->setEventHandlerType("DoStopSystem");
	return toReturn;
}
EXPORT void DoStopSystemEventHandler_Destroy(ParticleUniverse::DoStopSystemEventHandler* ptr)
{
	ptr->~DoStopSystemEventHandler();
}
EXPORT void DoStopSystemEventHandler__handle (ParticleUniverse::DoStopSystemEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region DoStopSystemEventHandler
EXPORT ParticleUniverse::DoScaleEventHandler* DoScaleEventHandler_New()
{
	ParticleUniverse::DoScaleEventHandler* toReturn = new ParticleUniverse::DoScaleEventHandler();
	toReturn->setEventHandlerType("DoScale");
	return toReturn;
}
EXPORT void DoScaleEventHandler_Destroy(ParticleUniverse::DoScaleEventHandler* ptr)
{
	ptr->~DoScaleEventHandler();
}
EXPORT const ParticleUniverse::DoScaleEventHandler::ScaleType DoScaleEventHandler_GetScaleType(ParticleUniverse::DoScaleEventHandler* ptr)
{
	return ptr->getScaleType();
}
EXPORT void DoScaleEventHandler_SetScaleType(ParticleUniverse::DoScaleEventHandler* ptr, const ParticleUniverse::DoScaleEventHandler::ScaleType scaleType)
{
	ptr->setScaleType(scaleType);
}
EXPORT const float DoScaleEventHandler_GetScaleFraction(ParticleUniverse::DoScaleEventHandler* ptr)
{
	return ptr->getScaleFraction();
}
EXPORT void DoScaleEventHandler_SetScaleFraction(ParticleUniverse::DoScaleEventHandler* ptr, const float scaleFraction)
{
	ptr->setScaleFraction(scaleFraction);
}
EXPORT void DoScaleEventHandler__handle (ParticleUniverse::DoScaleEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
EXPORT void DoScaleEventHandler_CopyAttributesTo (ParticleUniverse::DoScaleEventHandler* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->copyAttributesTo(eventHandler);
}
#pragma endregion
#pragma region DoPlacementParticleEventHandler
EXPORT ParticleUniverse::DoPlacementParticleEventHandler* DoPlacementParticleEventHandler_New()
{
	ParticleUniverse::DoPlacementParticleEventHandler* toReturn = new ParticleUniverse::DoPlacementParticleEventHandler();
	toReturn->setEventHandlerType("DoPlacementParticle");
	return toReturn;
}
EXPORT void DoPlacementParticleEventHandler_Destroy(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	ptr->~DoPlacementParticleEventHandler();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritPosition(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritPosition();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritDirection(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritDirection();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritOrientation(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritOrientation();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritTimeToLive(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritTimeToLive();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritMass(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritMass();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritTextureCoordinate(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritTextureCoordinate();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritColour(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritColour();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritParticleWidth(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritParticleWidth();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritParticleHeight(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritParticleHeight();
}
EXPORT bool DoPlacementParticleEventHandler_IsInheritParticleDepth(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->isInheritParticleDepth();
}
EXPORT void DoPlacementParticleEventHandler_SetInheritPosition(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritPosition)
{
	ptr->setInheritPosition(inheritPosition);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritDirection(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritDirection)
{
	ptr->setInheritDirection(inheritDirection);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritOrientation(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritOrientation)
{
	ptr->setInheritOrientation(inheritOrientation);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritTimeToLive(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritTimeToLive)
{
	ptr->setInheritTimeToLive(inheritTimeToLive);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritMass(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritMass)
{
	ptr->setInheritMass(inheritMass);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritTextureCoordinate(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritTextureCoordinate)
{
	ptr->setInheritTextureCoordinate(inheritTextureCoordinate);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritColour(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritColour)
{
	ptr->setInheritColour(inheritColour);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritParticleWidth(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritParticleWidth)
{
	ptr->setInheritParticleWidth(inheritParticleWidth);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritParticleHeight(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritParticleHeight)
{
	ptr->setInheritParticleHeight(inheritParticleHeight);
}
EXPORT void DoPlacementParticleEventHandler_SetInheritParticleDepth(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool inheritParticleDepth)
{
	ptr->setInheritParticleDepth(inheritParticleDepth);
}
EXPORT const char* DoPlacementParticleEventHandler_GetForceEmitterName(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->getForceEmitterName().c_str();
}
EXPORT void DoPlacementParticleEventHandler_SetForceEmitterName(ParticleUniverse::DoPlacementParticleEventHandler* ptr, const char* forceEmitterName)
{
	ptr->setForceEmitterName(forceEmitterName);
}
EXPORT ParticleUniverse::ParticleEmitter* DoPlacementParticleEventHandler_GetForceEmitter(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->getForceEmitter();
}
EXPORT void DoPlacementParticleEventHandler_RemoveAsListener(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	ptr->removeAsListener();
}
EXPORT unsigned int DoPlacementParticleEventHandler_GetNumberOfParticles(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->getNumberOfParticles();
}
EXPORT void DoPlacementParticleEventHandler_SetNumberOfParticles(ParticleUniverse::DoPlacementParticleEventHandler* ptr, unsigned int numberOfParticles)
{
	ptr->setNumberOfParticles(numberOfParticles);
}
EXPORT bool DoPlacementParticleEventHandler_AlwaysUsePosition(ParticleUniverse::DoPlacementParticleEventHandler* ptr)
{
	return ptr->alwaysUsePosition();
}
EXPORT void DoPlacementParticleEventHandler_SetAlwaysUsePosition(ParticleUniverse::DoPlacementParticleEventHandler* ptr, bool alwaysUsePosition)
{
	ptr->setAlwaysUsePosition(alwaysUsePosition);
}
EXPORT void DoPlacementParticleEventHandler__handle (ParticleUniverse::DoPlacementParticleEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
EXPORT void DoPlacementParticleEventHandler_ParticleEmitted(ParticleUniverse::DoPlacementParticleEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleEmitted(particleTechnique, particle);
}
EXPORT void DoPlacementParticleEventHandler_ParticleExpired(ParticleUniverse::DoPlacementParticleEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle)
{
	ptr->particleExpired(particleTechnique, particle);
}
EXPORT void DoPlacementParticleEventHandler_CopyAttributesTo (ParticleUniverse::DoPlacementParticleEventHandler* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->copyAttributesTo(eventHandler);
}
#pragma endregion
#pragma region DoFreezeEventHandler
EXPORT ParticleUniverse::DoFreezeEventHandler* DoFreezeEventHandler_New()
{
	ParticleUniverse::DoFreezeEventHandler* toReturn = new ParticleUniverse::DoFreezeEventHandler();
	toReturn->setEventHandlerType("DoFreeze");
	return toReturn;
}
EXPORT void DoFreezeEventHandler_Destroy(ParticleUniverse::DoFreezeEventHandler* ptr)
{
	ptr->~DoFreezeEventHandler();
}
EXPORT void DoFreezeEventHandler__handle (ParticleUniverse::DoFreezeEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}


EXPORT ParticleUniverse::DoExpireEventHandler* DoExpireEventHandler_New()
{
	return new ParticleUniverse::DoExpireEventHandler();
}
EXPORT void DoExpireEventHandler_Destroy(ParticleUniverse::DoExpireEventHandler* ptr)
{
	ptr->~DoExpireEventHandler();
}
//EXPORT bool DoExpireEventHandler_GetExpireAll(ParticleUniverse::DoExpireEventHandler* ptr)
//{
//	return ptr->getExpireAll();
//}
//EXPORT void DoExpireEventHandler_SetExpireAll(ParticleUniverse::DoExpireEventHandler* ptr, bool expifloatl)
//{
//	ptr->setExpireAll(expifloatl);
//}
EXPORT void DoExpireEventHandler__handle (ParticleUniverse::DoExpireEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
#pragma endregion
#pragma region DoEnableComponentEventHandler
EXPORT ParticleUniverse::DoEnableComponentEventHandler* DoEnableComponentEventHandler_New()
{
	ParticleUniverse::DoEnableComponentEventHandler* toReturn = new ParticleUniverse::DoEnableComponentEventHandler();
	toReturn->setEventHandlerType("DoEnableComponent");
	return toReturn;
}
EXPORT void DoEnableComponentEventHandler_Destroy(ParticleUniverse::DoEnableComponentEventHandler* ptr)
{
	ptr->~DoEnableComponentEventHandler();
}
EXPORT const char* DoEnableComponentEventHandler_GetComponentName(ParticleUniverse::DoEnableComponentEventHandler* ptr)
{
	return ptr->getComponentName().c_str();
}
EXPORT void DoEnableComponentEventHandler_SetComponentName(ParticleUniverse::DoEnableComponentEventHandler* ptr, const char* componentName)
{
	ptr->setComponentName(componentName);
}
EXPORT bool DoEnableComponentEventHandler_IsComponentEnabled(ParticleUniverse::DoEnableComponentEventHandler* ptr)
{
	return ptr->isComponentEnabled();
}
EXPORT void DoEnableComponentEventHandler_SetComponentEnabled(ParticleUniverse::DoEnableComponentEventHandler* ptr, bool enabled)
{
	ptr->setComponentEnabled(enabled);
}
EXPORT ParticleUniverse::ComponentType DoEnableComponentEventHandler_GetComponentType(ParticleUniverse::DoEnableComponentEventHandler* ptr)
{
	return ptr->getComponentType();
}
EXPORT void DoEnableComponentEventHandler_SetComponentType(ParticleUniverse::DoEnableComponentEventHandler* ptr, ParticleUniverse::ComponentType componentType)
{
	ptr->setComponentType(componentType);
}
EXPORT void DoEnableComponentEventHandler__handle (ParticleUniverse::DoEnableComponentEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
EXPORT void DoEnableComponentEventHandler_CopyAttributesTo (ParticleUniverse::DoEnableComponentEventHandler* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->copyAttributesTo(eventHandler);
}
#pragma endregion
#pragma region DoAffectorEventHandler
EXPORT ParticleUniverse::DoAffectorEventHandler* DoAffectorEventHandler_New()
{
	ParticleUniverse::DoAffectorEventHandler* toReturn = new ParticleUniverse::DoAffectorEventHandler();
	toReturn->setEventHandlerType("DoAffector");
	return toReturn;
}
EXPORT void DoAffectorEventHandler_Destroy(ParticleUniverse::DoAffectorEventHandler* ptr)
{
	ptr->~DoAffectorEventHandler();
}
EXPORT const bool DoAffectorEventHandler_GetPrePost(ParticleUniverse::DoAffectorEventHandler* ptr)
{
	return ptr->getPrePost();
}
EXPORT void DoAffectorEventHandler_SetPrePost(ParticleUniverse::DoAffectorEventHandler* ptr, const bool prePost)
{
	ptr->setPrePost(prePost);
}
EXPORT const char* DoAffectorEventHandler_GetAffectorName(ParticleUniverse::DoAffectorEventHandler* ptr)
{
	return ptr->getAffectorName().c_str();
}
EXPORT void DoAffectorEventHandler_SetAffectorName(ParticleUniverse::DoAffectorEventHandler* ptr, const char* affectorName)
{
	ptr->setAffectorName(affectorName);
}
EXPORT void DoAffectorEventHandler__handle (ParticleUniverse::DoAffectorEventHandler* ptr, ParticleUniverse::ParticleTechnique* particleTechnique, ParticleUniverse::Particle* particle, float timeElapsed)
{
	ptr->_handle(particleTechnique, particle, timeElapsed);
}
EXPORT void DoAffectorEventHandler_CopyAttributesTo (ParticleUniverse::DoAffectorEventHandler* ptr, ParticleUniverse::ParticleEventHandler* eventHandler)
{
	ptr->copyAttributesTo(eventHandler);
}
#pragma endregion
