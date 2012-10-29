#pragma once

#pragma managed(push, off)
#include <Newton.h>
#include <Ogre.h>
#pragma managed(pop)
#include "OgreNewt_Prerequisites.h"
#include "OgreNewt_Body.h"

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

  Mogre::Vector3 GetContactForce( MogreNewt::Body^ body );

  void GetContactPositionAndNormal(
	  MogreNewt::Body^ body,
		[Out] Mogre::Vector3 %position, [Out] Mogre::Vector3 %normal
	);
  void GetContactTangentDirections(
	  MogreNewt::Body^ body,
	  [Out] Mogre::Vector3 %dir0, [Out] Mogre::Vector3 %dir1
	);
  dFloat GetContactTangentSpeed(int index);
};


}	// end namespace MogreNewt
