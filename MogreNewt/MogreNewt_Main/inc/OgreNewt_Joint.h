/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_JOINT
#define _INCLUDE_OGRENEWT_JOINT

#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreNewt_Body.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

//! base class for all joints.
/*!
	this class is inherited by all other specific joint types.
*/
public ref class Joint abstract
{
public:

	//! constructor
	Joint();

	//! disposer
	virtual ~Joint();

	//! returns/sets collision state
	/*!
		The collision state determines whether collision should be calculated between the parent and child bodies of the joint.
		\return integer value. 1 = collision on, 0 = collision off.
	*/
	property int CollisionState
  { 
    int get() { return NewtonJointGetCollisionState( m_joint ); }
    void set(int state) { NewtonJointSetCollisionState(m_joint, state); }
  }

	//! get/set joint stiffness
	/*!
		Joint stiffness adjusts how much "play" the joint can have.  high stiffness = very small play, but more likely to become unstable.
		\return float representing joint stiffness in range [0,1]
	*/
	property Mogre::Real Stiffness 
  {
    Mogre::Real get() { return (Mogre::Real)NewtonJointGetStiffness( m_joint ); }
    void set(Mogre::Real stiffness) { NewtonJointSetStiffness( m_joint, stiffness ); }
  }

	//! get/set user data for this joint
	/*!
		user data can be used to connect this class to other user classes through the use of this general pointer.
	*/
	property Object^ UserData
  {
    Object^ get() { return m_userdata; }
    void set(Object^ ptr) { m_userdata = ptr; }
  }

	property ::MogreNewt::Body^ GetBody0
	{	
		::MogreNewt::Body^ get() 
		{ 
			return static_cast<BodyNativeInfo*>( 
				NewtonBodyGetUserData( NewtonJointGetBody0(m_joint) ) 
				)->managedBody;
		}
	}

	property ::MogreNewt::Body^ GetBody1
	{	
		::MogreNewt::Body^ get() 
		{ 
			return static_cast<BodyNativeInfo*>( 
				NewtonBodyGetUserData( NewtonJointGetBody1(m_joint) ) 
				)->managedBody;
		}
	}

protected:

	NewtonJoint* m_joint;
	MogreNewt::World^ m_world;

	Object^ m_userdata;

	GCHandle m_handle;

	void Destructor( const NewtonJoint* me );

//MOD now declared as public (because of a strange compiler error since Newton 2 dll)
public:
	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
  delegate void NewtonJointDestructorDelegate( const NewtonJoint* me );

protected:
	NewtonJointDestructorDelegate^			m_NewtonJointDestructorDelegate;

	NewtonConstraintDestructor 		m_funcptr_destructor;

};






//! CustomJoint 
/*!
	this class represents a basic class for creating user-defined joints.  this class must be inherited to create discreet joints.
*/
public ref class CustomJoint abstract : public Joint
{
 
public:

	//! constructor
	CustomJoint( unsigned int maxDOF, Body^ body0, Body^ body1 );

	//! must be over-written for a functioning joint.
	virtual void SubmitConstraint( Mogre::Real timeStep, int threadIndex ) = 0;


	//! find the local orientation and position of the joint with regards to the 2 bodies in the joint.
	void pinAndDirToLocal( const Mogre::Vector3 pinpt, const Mogre::Vector3 pindir, [Out] Mogre::Quaternion% localOrient0, [Out] Mogre::Vector3% localPos0, [Out] Mogre::Quaternion% localOrient1, [Out] Mogre::Vector3% localPos1 );

	//! find the global orientation and position of the joint with regards to the a body in the joint.
	void localToGlobal( const Mogre::Quaternion localOrient, const Mogre::Vector3 localPos, [Out] Mogre::Quaternion% globalOrient, [Out] Mogre::Vector3% globalPos, int bodyIndex );
	void localToGlobal( const Mogre::Quaternion localOrient, const Mogre::Vector3 localPos, [Out] Mogre::Quaternion% globalOrient, [Out] Mogre::Vector3% globalPos )
	{
		localToGlobal( localOrient, localPos, globalOrient, globalPos, 0 );
	}

	//! add a linear row to the constraint.
	void addLinearRow( const Mogre::Vector3 pt0, const Mogre::Vector3 pt1, const Mogre::Vector3 dir );

	//! add an angular row to the constraint.
	void addAngularRow( Mogre::Radian relativeAngleError, const Mogre::Vector3 dir );

	//! set the general jacobian rows directly.
	void addGeneralRow( const Mogre::Vector3 linear0, const Mogre::Vector3 angular0, const Mogre::Vector3 linear1, const Mogre::Vector3 angular1 );

	//! set row minimum friction
	void setRowMinimumFriction( Mogre::Real friction );

	//! set row maximum friction
	void setRowMaximumFriction( Mogre::Real friction );

	//! set row acceleration
	void setRowAcceleration( Mogre::Real accel );

	//! set row stiffness
	void setRowStiffness( Mogre::Real stiffness );

	//! apply a spring to this row, allowing for joints with spring behaviour in 1 or more DoF's
	/*!
		\param springK float spring constant.
		\param springD float natural rest state distance of the spring.
	*/		
	void setRowSpringDamper( Mogre::Real springK, Mogre::Real springD );

	//! retrieve the force acting on the current row.
	Mogre::Real getRowForce( int row ) { return NewtonUserJointGetRowForce( m_joint, row ); }

	//! pin vector to arbitrary quaternion utility function.
	Mogre::Quaternion grammSchmidt( const Mogre::Vector3 pin );

protected:

	unsigned int m_maxDOF;

	MogreNewt::Body^ m_body0;
	MogreNewt::Body^ m_body1;



private:

	//! newton callback.  used internally.
	void newtonSubmitConstraint( const NewtonJoint* me,float timeStep, int threadIndex );
	void newtonGetInfo(const NewtonJoint* me, NewtonJointRecord* info );

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonSubmitConstraintDelegate( const NewtonJoint* me,float timeStep, int threadIndex );

	NewtonSubmitConstraintDelegate^			m_NewtonSubmitConstraintDelegate;

	NewtonUserBilateralCallBack		m_funcptr_submitConstraint;
	NewtonUserBilateralGetInfoCallBack     m_funcptr_getInfo;

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonGetInfoDelegate( const NewtonJoint* me, NewtonJointRecord* info );

	NewtonGetInfoDelegate^		m_NewtonGetInfoDelegate;
};



}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_JOINT


