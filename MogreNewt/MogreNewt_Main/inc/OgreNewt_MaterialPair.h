/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_MATERIALPAIR
#define _INCLUDE_OGRENEWT_MATERIALPAIR

#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreNewt_ContactCallback.h"
#include "OgreNewt_MaterialID.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{


//! define interaction between materials
/*!
	this class represents a pair of Newton MaterialGroupIDs, which is 
	used to define interaction bewteen materials.
*/
public ref class MaterialPair
{
public:

	//! constructor
	/*!
		creates an object representing a pair of materials, and defining how they will interact.
		\param world pointer to the MogreNewt::World
		\param mat1 pointer to first materialID
		\param mat2 pointer to second materialID
	*/
	MaterialPair( World^ world, MaterialID^ mat1, MaterialID^ mat2 );


	// set the default behavior for this material pair.

	//! set default softness for the material pair.
	void SetDefaultSoftness( Mogre::Real softness ) { NewtonMaterialSetDefaultSoftness( m_world->NewtonWorld, id0->ID, id1->ID, (float)softness ); }

	//! set default elasticity for the material pair.
	void SetDefaultElasticity( Mogre::Real elasticity ) { NewtonMaterialSetDefaultElasticity( m_world->NewtonWorld, id0->ID, id1->ID, (float)elasticity ); }

	//! set default collision for the material pair.
	void SetDefaultCollidable( int state ) { NewtonMaterialSetDefaultCollidable( m_world->NewtonWorld, id0->ID, id1->ID, state ); }

	//! set the default thickness for this material pair
	void SetDefaultSurfaceThickness( float thickness ) { NewtonMaterialSetSurfaceThickness( m_world->NewtonWorld, id0->ID, id1->ID, thickness ); }


	//! set default friction for the material pair.
	void SetDefaultFriction( Mogre::Real stat, Mogre::Real kinetic ) { NewtonMaterialSetDefaultFriction( m_world->NewtonWorld, id0->ID, id1->ID, (float)stat, (float)kinetic ); }

	//! set continuos collision on/off for this material pair
	/*!
		continuous collision mode is an advanced system used to prevent "tunelling", or objects passing through one an other when traveling at high velocities.  
		there is a performance hit involved, so this sould only be used when it is deemed necessary.
	*/
	void SetContinuousCollisionMode( int state ) { NewtonMaterialSetContinuousCollisionMode( m_world->NewtonWorld, id0->ID, id1->ID, state ); }


	//! assign a custom collision callback.
	/*!
		ContactCallbacks allow for custom interaction between bodies of specific materials.
		\param callback pointer to a user-created ContactCallback object
	*/
	void SetContactCallback( MogreNewt::ContactCallback^ callback );



protected:

	MaterialID^	id0;
	MaterialID^	id1;
	World^		m_world;

	ContactCallback^	m_contactCallback;

};


}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_MATERIALPAIR


