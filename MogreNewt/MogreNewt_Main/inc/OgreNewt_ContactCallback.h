/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_CONTACTCALLBACK
#define _INCLUDE_OGRENEWT_CONTACTCALLBACK

#include <Newton.h>
#include "OgreNewt_Body.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

	value class ContactMaterial;
	//! custom contact behavior
/*!
	this class is for creating custom behavior between material GroupIDs.
	this class must be inherited, and the user functions created, and then
	added to a MaterialPair class.
*/
public ref class ContactCallback
{
public:
	//! constructor
	ContactCallback();

	// user-defined callback function.
	
	//! user defined Begin function
	/*!
		this function is called when 2 bodies AABB overlap.  they have not yet collided, but *may* do so this loop.
		at this point, m_body0 and m_body1 are defined, but the contact isn't yet valid, so none of the member functions
		can be called yet.  they must be called from the userProcess() function.
		return 0 to ignore the collision, 1 to allow it.
	*/
    virtual int UserAABBOverlap(ContactMaterial material, Body^ body0, Body^ body1, int threadIndex) { return 1; }

	//! user-defined Process function
	/*!
		user process function.  is called for each contact between the 2 bodies.  all member functions are valid from
		within this function, and will affect the current contact.  return 0 to ignore the collision, 1 to allow it.
	 */
    virtual void UserProcess(ContactJoint contact, dFloat timestep, int threadIndex) {}

internal:
	
  int  AABBOverlap (const NewtonMaterial* material, const NewtonBody* body0, const NewtonBody* body1, int threadIndex);
  void ContactProcess (const NewtonJoint* contact, dFloat timestep, int threadIndex);

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
    delegate int  NewtonOnAABBOverlapDelegate (const NewtonMaterial* material, const NewtonBody* body0, const NewtonBody* body1, int threadIndex);

    [UnmanagedFunctionPointer(CallingConvention::Cdecl)]
    delegate void NewtonContactProcessDelegate (const NewtonJoint* contact, dFloat timestep, int threadIndex);

	NewtonOnAABBOverlapDelegate^			m_NewtonOnAABBOverlapDelegate;
    NewtonContactProcessDelegate^		    m_NewtonContactProcessDelegate;

    NewtonOnAABBOverlap 		m_funcptr_AABBOverlap;
 	NewtonContactsProcess	m_funcptr_contactProcess;

};
}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_CONTACTCALLBACK


