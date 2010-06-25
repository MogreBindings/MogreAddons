#include <ContactJoint.h>
#include "OgreNewt_Body.h"

namespace MogreNewt
{

ContactJoint::ContactJoint(NewtonJoint *joint) {
  if (joint == NULL)
    throw gcnew Exception("Cannot create a ContactJoint from a null NewtonJoint");
  _joint = joint;
}

Body^ ContactJoint::Body0::get() {
  return static_cast<BodyNativeInfo*>( 
    NewtonBodyGetUserData( NewtonJointGetBody0(_joint) ) 
    )->managedBody;
}


Body^ ContactJoint::Body1::get() {
  return static_cast<BodyNativeInfo*>( 
    NewtonBodyGetUserData( NewtonJointGetBody1(_joint) ) 
    )->managedBody;
}

ContactJoint::ContactEnumerator::ContactEnumerator(ContactJoint joint) {
  _joint = joint._joint;
  Reset();
}

bool ContactJoint::ContactEnumerator::MoveNext() {
  if (_current == (void*)-1) {
    _current = NewtonContactJointGetFirstContact(_joint);
  }
  else {
    if (_current != NULL)
      _current = NewtonContactJointGetNextContact( _joint, _current);
  }

  return (_current != NULL);
}

ContactMaterial ContactJoint::ContactEnumerator::Current::get()
{
  return ContactMaterial(NewtonContactGetMaterial(_current));
}																				

ContactJoint::ContactsEnumerable::ContactsEnumerable(ContactJoint joint) {
  _joint = joint._joint;
}

}	// end namespace MogreNewt
