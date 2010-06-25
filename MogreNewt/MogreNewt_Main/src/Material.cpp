#include <Material.h>
#include "OgreNewt_Body.h"

namespace MogreNewt
{

Material::Material(NewtonMaterial *material) {
  if (material == NULL)
    throw gcnew Exception("Cannot create a Material from a null NewtonMaterial");
  _mat = material;
}

unsigned Material::ContactFaceAttribute::get()
{	
  return NewtonMaterialGetContactFaceAttribute(_mat);
}

dFloat Material::ContactNormalSpeed::get()
{	
  return NewtonMaterialGetContactNormalSpeed(_mat);
}

Mogre::Vector3 Material::ContactForce::get()
{
  dFloat force[3];
  NewtonMaterialGetContactForce(_mat, force);
  return Mogre::Vector3(force);
}

void Material::GetContactPositionAndNormal([Out] Mogre::Vector3 %position, [Out] Mogre::Vector3 %normal) {
  dFloat pos[3], norm[3];
  NewtonMaterialGetContactPositionAndNormal(_mat, pos, norm);
  position.x = pos[0];
  position.y = pos[1];
  position.z = pos[2];
  normal.x = norm[0];
  normal.y = norm[1];
  normal.z = norm[2];
}

void Material::GetContactTangentDirections([Out] Mogre::Vector3 %dir0, [Out] Mogre::Vector3 %dir1) {
  dFloat ndir0[3], ndir1[3];
  NewtonMaterialGetContactTangentDirections(_mat, ndir0, ndir1);
  dir0.x = ndir0[0];
  dir0.y = ndir0[1];
  dir0.z = ndir0[2];
  dir1.x = ndir1[0];
  dir1.y = ndir1[1];
  dir1.z = ndir1[2];
}

dFloat Material::GetContactTangentSpeed(int index) {
  return NewtonMaterialGetContactTangentSpeed(_mat, index);
}

}	// end namespace MogreNewt


