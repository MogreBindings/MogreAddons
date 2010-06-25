#include <ContactMaterial.h>
#include "OgreNewt_Body.h"

namespace MogreNewt
{

ContactMaterial::ContactMaterial(NewtonMaterial *material) {
  if (material == NULL)
    throw gcnew Exception("Cannot create a Material from a null NewtonMaterial");
  _mat = material;
}

unsigned ContactMaterial::GetBodyCollisionID( MogreNewt::Body^ body )
{
  return NewtonMaterialGetBodyCollisionID( _mat, body->NewtonBody );
}

unsigned ContactMaterial::ContactFaceAttribute::get()
{	
  return NewtonMaterialGetContactFaceAttribute(_mat);
}

dFloat ContactMaterial::ContactNormalSpeed::get()
{	
  return NewtonMaterialGetContactNormalSpeed(_mat);
}


Mogre::Vector3 ContactMaterial::ContactForce::get()
{
  Mogre::Vector3 force;
  NewtonMaterialGetContactForce( _mat, &force.x );
  return force;
}

Mogre::Vector3 ContactMaterial::ContactPosition::get()
{
  Mogre::Vector3 pos, norm;
  GetContactPositionAndNormal(pos, norm);
  return pos;
}

Mogre::Vector3 ContactMaterial::ContactNormal::get()
{
  Mogre::Vector3 pos, norm;
  GetContactPositionAndNormal(pos, norm);
  return norm;
}

Mogre::Vector3 ContactMaterial::ContactFirstTangent::get()
{
  Mogre::Vector3 t1, t2;
  GetContactTangentDirections(t1, t1);
  return t1;
}

Mogre::Vector3 ContactMaterial::ContactSecondTangent::get()
{
  Mogre::Vector3 t1, t2;
  GetContactTangentDirections(t1, t1);
  return t2;
}

void ContactMaterial::GetContactPositionAndNormal( Mogre::Vector3% pos, Mogre::Vector3% norm )
{
  pin_ptr<Mogre::Vector3> p_pos = &pos;
  pin_ptr<Mogre::Vector3> p_norm = &norm;
  NewtonMaterialGetContactPositionAndNormal( _mat, &p_pos->x, &p_norm->x );
}


void ContactMaterial::GetContactTangentDirections( Mogre::Vector3% dir0, Mogre::Vector3% dir1 )
{
  pin_ptr<Mogre::Vector3> p_dir0 = &dir0;
  pin_ptr<Mogre::Vector3> p_dir1 = &dir1;
  NewtonMaterialGetContactTangentDirections( _mat, &p_dir0->x, &p_dir1->x );
}

dFloat ContactMaterial::GetContactTangentSpeed(int index) {
  return NewtonMaterialGetContactTangentSpeed(_mat, index);
}

}	// end namespace MogreNewt


