#pragma once

#include <Newton.h>
#include <Ogre.h>
#include "OgreNewt_Prerequisites.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

public value class Material
{
  NewtonMaterial *_mat;

internal:
  Material(NewtonMaterial *material);

public:
  property unsigned ContactFaceAttribute
  {	
    unsigned get();
  }

  property dFloat ContactNormalSpeed
  {	
    dFloat get();
  }

  property Mogre::Vector3 ContactForce
  {
    Mogre::Vector3 get();
  }

  void GetContactPositionAndNormal([Out] Mogre::Vector3 %position, [Out] Mogre::Vector3 %normal);
  void GetContactTangentDirections([Out] Mogre::Vector3 %dir0, [Out] Mogre::Vector3 %dir1);
  dFloat GetContactTangentSpeed(int index);
};


}	// end namespace MogreNewt
