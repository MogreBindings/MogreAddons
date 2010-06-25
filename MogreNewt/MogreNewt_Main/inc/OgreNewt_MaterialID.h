/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_MATERIALID
#define _INCLUDE_OGRENEWT_MATERIALID

#include <Newton.h>
#include "OgreNewt_World.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

	ref class World;

//! represents a material
public ref class MaterialID
{
public:

	//! constructor
	/*!
		\param world pointer to the OgreNewt;;World
	*/
	MaterialID( World^ world );

	/*!
		Overloaded constructor, sets the internal ID manually.  should not be used by the end-user.
	*/
	MaterialID( World^ world, int ID );

	//! get Newton-assigned material ID.
  property int ID { int get() { return id; } }

protected:

	int id;
	MogreNewt::World^ m_world;

};



}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_MATERIALID


