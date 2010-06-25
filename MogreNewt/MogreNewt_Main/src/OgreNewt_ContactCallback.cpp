#include <OgreNewt_ContactCallback.h>
#include <OgreNewt_Body.h>

namespace MogreNewt
{


ContactCallback::ContactCallback()
{
  m_funcptr_AABBOverlap = NULL;
  m_funcptr_contactProcess = NULL;
  System::Type^ thisType = this->GetType();
  if (thisType == ContactCallback::typeid)
    return;

  System::Reflection::MethodInfo^ method_AABBOverlap = thisType->GetMethod("UserAABBOverlap",
      gcnew array<Type^>{ContactMaterial::typeid, Body::typeid, Body::typeid, int::typeid});
  System::Reflection::MethodInfo^ method_contactProcess = thisType->GetMethod("UserProcess",
      gcnew array<Type^>{ContactJoint::typeid, dFloat::typeid, int::typeid});

  if (method_AABBOverlap->DeclaringType != ContactCallback::typeid) {
    m_NewtonOnAABBOverlapDelegate = gcnew NewtonOnAABBOverlapDelegate( this, &ContactCallback::AABBOverlap );
	m_funcptr_AABBOverlap = (NewtonOnAABBOverlap) Marshal::GetFunctionPointerForDelegate( m_NewtonOnAABBOverlapDelegate ).ToPointer();
  }

  if (method_contactProcess->DeclaringType != ContactCallback::typeid) {
	m_NewtonContactProcessDelegate = gcnew NewtonContactProcessDelegate( this, &ContactCallback::ContactProcess );
	m_funcptr_contactProcess = (NewtonContactsProcess) Marshal::GetFunctionPointerForDelegate( m_NewtonContactProcessDelegate ).ToPointer();
  }
}

int ContactCallback::AABBOverlap(const NewtonMaterial* material, const NewtonBody* body0, const NewtonBody* body1, int threadIndex)
{
  return UserAABBOverlap(ContactMaterial(const_cast<NewtonMaterial*>(material)),
                         static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body0 ) )->managedBody,
                         static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body1 ) )->managedBody,
                         threadIndex);
}

void ContactCallback::ContactProcess(const NewtonJoint* contact, dFloat timestep, int threadIndex)
{
  UserProcess(ContactJoint(const_cast<NewtonJoint*>(contact)), timestep, threadIndex);
}

}
