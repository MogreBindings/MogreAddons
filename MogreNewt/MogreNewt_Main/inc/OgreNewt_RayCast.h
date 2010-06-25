/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/

#ifndef _INCLUDE_OGRENEWT_RAYCAST
#define _INCLUDE_OGRENEWT_RAYCAST


#include <Ogre.h>
#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreNewt_Body.h"
#include "OgreNewt_CollisionPrimitives.h"


// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

//! general raycast
/*!
	General class representing a raycast query in the Newton world.  this class should be inherited to create specific raycast behavior.
*/
public ref class Raycast abstract
{
public:

	Raycast();

	//! performs the raycast.  
	/*!
		call after creating the object.
		\param world pointer to the MogreNewt::World
		\param startpt starting point of ray in global space
		\param endpt ending point of ray in global space
	*/
	void Go( MogreNewt::World^ world, const Mogre::Vector3 startpt, const Mogre::Vector3 endpt );

	//! user callback pre-filter function.
	/*!
		This function is an optional pre-filter to completely ignore specific bodies during the raycast.
		return false from this function to ignore this body, return true (default) to accept it.
	*/
	virtual bool UserPreFilterCallback( MogreNewt::Body^ body ) { return true; }

	//! user callback filter function
	/*! user callback function.  
		This function must be implemented by the user.
		Newton calls this function for each body intersected by the ray.  however it doesn't
		necessarily go perfect cloest-to-farthest order.
		return true and the callback will only be called for bodies closer to the start point than the current body.
		return false and the callback will call for any other bodies, even those farther than the current one.
	*/
	virtual bool UserCallback( MogreNewt::Body^ body, Mogre::Real distance, const Mogre::Vector3 normal, int collisionID ) = 0;

protected:
	//! save the last OgreNewt::Body from the newtonRaycastPreFilter to use this for example the TreeCollisionRayCallback
	MogreNewt::Body^ m_lastbody;

	//! save if this body was already added by RayCastCallback from TreeCollision
	bool bodyalreadyadded;

private:

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate float NewtonWorldRayFilterCallbackDelegate( const NewtonBody* body, const float* hitNormal, int collisionID, void* userData, float intersetParam );

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate unsigned NewtonWorldRayPrefilterCallbackDelegate( const NewtonBody* body, const NewtonCollision* collision, void* userData );

	NewtonWorldRayFilterCallbackDelegate^			m_NewtonWorldRayFilterCallbackDelegate;
	NewtonWorldRayPrefilterCallbackDelegate^		m_NewtonWorldRayPrefilterCallbackDelegate;

	NewtonWorldRayFilterCallback 		 m_funcptr_newtonRaycastFilter;
	NewtonWorldRayPrefilterCallback 	 m_funcptr_newtonRaycastPreFilter;

	//! callback used for running the raycast itself... used internally
	float NewtonRaycastFilter(const NewtonBody* body, const float* hitNormal, int collisionID, void* userData, float intersetParam);

	//! callback used for running the raycast prefilder... used internally
	unsigned NewtonRaycastPreFilter( const NewtonBody* body, const NewtonCollision* collision, void* userData );
};


public ref class ConvexCast abstract
{
public:
	  ConvexCast();

	  
    //! performs the convexcast.
    /*!
        call after creating the object.
        \param world pointer to the MogreNewt::World
        \param col reference to a convex collision shape used for the cast
        \param startpt starting point of ray in global space
        \param colori orientation of the collision in global space
        \param endpt ending point of ray in global space
        \param maxcontactscount maximum number of contacts that should be saved,
               set to 0 if you only need the distance to the first intersection
    */
	  void  Go(MogreNewt::World^ world,MogreNewt::ConvexCollision^ col,const Mogre::Vector3 startpt,const Mogre::Quaternion colori,const Mogre::Vector3 endpt, int MaxContactsCount, int ThreadIndex);

	//! user callback pre-filter function.
	/*!
		This function is an optional pre-filter to completely ignore specific bodies during the raycast.
		return false from this function to ignore this body, return true (default) to accept it.
	*/
	virtual bool UserPreFilterCallback( MogreNewt::Body^ body ) { return true; }

protected:
	 //! list that stores the results of the convex-cast
        NewtonWorldConvexCastReturnInfo *mReturnInfoList;

        //! the real length of the list
        int mReturnInfoListLength;

        //! the actual maximum length of the list (number of elements memory has been reserved for)
        int mReturnInfoListSize;

		//! distance in [0,1] to first contact
		Mogre::Real mFirstContactDistance;
private:
	
	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate unsigned NewtonConvexcastPreFilterCallbackDelegate( const NewtonBody* body, const NewtonCollision* collision, void* userData );

	NewtonConvexcastPreFilterCallbackDelegate^ m_NewtonConvexcastPreFilterCallbackDelegate;

	NewtonWorldRayPrefilterCallback 	 m_funcptr_newtonConvexCastPreFilter;
 //! callback used for running the raycast prefilter... used internally
    unsigned NewtonConvexcastPreFilter( const NewtonBody* body, const NewtonCollision* collision, void* userData );

};




//! Basic implementation of the raycast
/*!
	This class is provided for general raycast use.  it returns information about all bodies hit by the ray.
*/
public ref class BasicRaycast : public Raycast
{
public:
	//! simple class that represents a single raycast rigid body intersection.
	ref class BasicRaycastInfo
	{
	public:
		Mogre::Real					mDistance;	//!< dist from point1 of the raycast, in range [0,1].
		MogreNewt::Body^				mBody;	//!< pointer to body intersected with
		int							mCollisionID;		//!< collision ID of the primitive hit by the ray (for compound collision bodies)
		Mogre::Vector3				mNormal;	//!< normal of intersection.

		BasicRaycastInfo();
	};

	//! constructor
	/*!
		performs a raycast, then the results can be queried from the object after creation.
		\param world pointer to the MogreNewt::World
		\param startpt starting point of the ray in global space
		\param endpt ending point of the ray in global space
	*/
	BasicRaycast();

	BasicRaycast( MogreNewt::World^ world, const Mogre::Vector3 startpt, const Mogre::Vector3 endpt );

	//! the all-important custom callback function.
	virtual bool UserCallback( Body^ body, Mogre::Real distance, const Mogre::Vector3 normal, int collisionID ) override;

	// ------------------------------------------------------
	// the following functions can be used to retrieve information about the bodies collided by the ray.
	
	//! how many bodies did we hit?
	property int HitCount { int get(); }

	//! retrieve the raycast info for a specific hit.
	BasicRaycastInfo^ GetInfoAt( int hitnum );

	//! get the closest body hit by the ray.
  property BasicRaycastInfo^ FirstHit { BasicRaycastInfo^ get(); }


private:


	// container for results.
	typedef System::Collections::Generic::List<BasicRaycastInfo^> RaycastInfoList;

	RaycastInfoList^ mRayList;

};


public ref class BasicConvexCast : public ConvexCast
{

public:
	ref class ConvexcastContactInfo
	{
	public:
		MogreNewt::Body^         mBody;                      //!< pointer to body intersected with
        int                     mCollisionID;               //!< collision ID of the primitive hit by the ray (for compound collision bodies)
        Mogre::Vector3           mContactNormal;             //!< normal of intersection.
        Mogre::Vector3           mContactNormalOnHitPoint;   //!< surface normal at the surface of the hit body
        Mogre::Vector3           mContactPoint;              //!< point of the contact in global space
        Mogre::Real              mContactPenetration;        //!< contact penetration at collision point

        ConvexcastContactInfo();
	};

	BasicConvexCast();

	BasicConvexCast( MogreNewt::World^ world, MogreNewt::ConvexCollision^ col, const  Mogre::Vector3 startpt, const Mogre::Quaternion colori, const Mogre::Vector3 endpt, int maxcontactscount, int threadIndex);

	 // ------------------------------------------------------
    // the following functions can be used to retrieve information about the bodies collided by the convexcast.
    
    //! how many bodies did we hit? if maxcontactscount is to small, this value will be smaller too!
	property int HitCount { int get(); }

	//! how many contacts do we have
	property int ContactsCount { int get(); }

    //! retrieve the raycast info for a specific hit.
    ConvexcastContactInfo^ GetInfoAt( int hitnum );

    //! retrieve the distance to the first contact (in range [0,1] from startpt to endpt)
	property Mogre::Real NormalizedDistanceToFirstHit{ Mogre::Real get();}
};



}	// end namespace MogreNewt
	




#endif	// _INCLUDE_OGRENEWT_RAYCAST


