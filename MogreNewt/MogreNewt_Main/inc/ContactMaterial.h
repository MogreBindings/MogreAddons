#pragma once

#pragma managed(push, off)
#include <Newton.h>
#include <Ogre.h>
#pragma managed(pop)
#include "OgreNewt_Prerequisites.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

  ref class Body;

public value class ContactMaterial
{
  NewtonMaterial *_mat;

internal:
  ContactMaterial(NewtonMaterial *material);

public:
  property unsigned ContactFaceAttribute
  {	
    unsigned get();
  }

// NOTE: This function was removed in Newton 2.34
//  unsigned GetBodyCollisionID( MogreNewt::Body^ body );

  property dFloat ContactNormalSpeed
  {	
    dFloat get();
  }

  Mogre::Vector3 GetContactForce( MogreNewt::Body^ body );

  Mogre::Vector3 GetContactPosition( MogreNewt::Body^ body );

  Mogre::Vector3 GetContactNormal( MogreNewt::Body^ body );

  Mogre::Vector3 GetContactFirstTangent( MogreNewt::Body^ body );

  Mogre::Vector3 GetContactSecondTangent( MogreNewt::Body^ body );

  void GetContactPositionAndNormal(
		MogreNewt::Body^ body,
	  [Out] Mogre::Vector3 %position, [Out] Mogre::Vector3 %normal
	);
  void GetContactTangentDirections(
	  MogreNewt::Body^ body,
		[Out] Mogre::Vector3 %dir0, [Out] Mogre::Vector3 %dir1
	);
  dFloat GetContactTangentSpeed(int index);


  //! set softness of the current contact
  void SetContactSoftness( Mogre::Real softness ) { NewtonMaterialSetContactSoftness( _mat, (float)softness ); }

  //! set elasticity of the current contact
  void SetContactElasticity( Mogre::Real elasticity ) { NewtonMaterialSetContactElasticity( _mat, (float)elasticity ); }

  //! set friction state of current contact
  void SetContactFrictionState( int state, int index ) { NewtonMaterialSetContactFrictionState( _mat, state, index ); }

  //! set tangent acceleration for contact
  void SetContactTangentAcceleration( Mogre::Real accel, int index ) { NewtonMaterialSetContactTangentAcceleration( _mat, (float)accel, index ); }

  //! align tangent vectors with a user supplied direction
  void RotateTangentDirections( Mogre::Vector3 dir ) { NewtonMaterialContactRotateTangentDirections( _mat, &dir.x ); }

  //! manually set the normal for the collision.
  void SetContactNormalDirection( Mogre::Vector3 dir ) { NewtonMaterialSetContactNormalDirection( _mat, &dir.x ); }

  //! manually set the acceleration along the collision normal.
  void SetContactNormalAcceleration( Mogre::Real accel ) { NewtonMaterialSetContactNormalAcceleration( _mat, accel ); }

  //! Set friction for contact
  void SetContactFrictionCoef(dFloat staticFrictionCoef, dFloat kineticFrictionCoef, int index)	
  {NewtonMaterialSetContactFrictionCoef(_mat,staticFrictionCoef,kineticFrictionCoef,index);}
};


}	// end namespace MogreNewt
