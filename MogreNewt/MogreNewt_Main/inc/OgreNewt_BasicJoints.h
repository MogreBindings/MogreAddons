/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_BASICJOINTS
#define _INCLUDE_OGRENEWT_BASICJOINTS

#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreNewt_Body.h"
#include "OgreNewt_Joint.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{


//! Namespace for ready-made joints
namespace BasicJoints
{

//! Ball and Socket joint.
/*!
	simple ball and socket joint, with limits.
*/
public ref class BallAndSocket : public Joint
{
 
public:

	//! constructor
	/*!
		\param world pointer to the MogreNewt::World
		\param child pointer to the child rigid body.
		\param parent pointer to the parent rigid body. pass NULL to make the world itself the parent (aka a rigid joint)
		\param pos position of the joint in global space
	*/
	BallAndSocket( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos );

	//! retrieve the current joint angle
  property Mogre::Vector3 JointAngle { Mogre::Vector3 get(); }

	//! retrieve the current joint omega
	property Mogre::Vector3 JointOmega { Mogre::Vector3 get(); }

	//! retrieve the current joint force.
	/*!
		This can be used to find the "stress" on the joint.  you can do special effects like break the joint if the force exceedes some value, etc.
	*/
	property Mogre::Vector3 JointForce { Mogre::Vector3 get(); }

	//! set limits for the joints rotation
	/*!
		\param pin pin direction in global space
		\param maxCone max angle for "swing" (in radians)
		\param maxTwist max angle for "twist"  (in radians)
	*/
	void SetLimits( const Mogre::Vector3 pin, Mogre::Radian maxCone, Mogre::Radian maxTwist ) { NewtonBallSetConeLimits( m_joint, &pin.x, (float)maxCone.ValueRadians, (float)maxTwist.ValueRadians ); }

	event System::EventHandler^ WorldUpdated
	{
		void add(System::EventHandler^ hnd);
		void remove(System::EventHandler^ hnd);

	private:
		void raise( Object^ sender, EventArgs^ args )
		{
			if (m_worldUpdated)
				m_worldUpdated->Invoke( sender, args );
		}
	}


protected:
	virtual void SetNewtonCallback(NewtonBallCallBack callback);
	System::EventHandler^ m_worldUpdated;

private:
	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonBallCallBackDelegate( const NewtonJoint* joint, dFloat timestep );

	NewtonBallCallBackDelegate^			m_NewtonBallCallBackDelegate;

	NewtonBallCallBack		 m_funcptr_newtonCallback;

	void NewtonCallback( const NewtonJoint* joint, dFloat timestep );
};



public ref class HingeSliderBase abstract : public Joint
{
public:

	HingeSliderBase();

	event System::EventHandler^ WorldUpdated
	{
		void add(System::EventHandler^ hnd);
		void remove(System::EventHandler^ hnd);

	private:
		void raise( Object^ sender, EventArgs^ args )
		{
			if (m_worldUpdated)
				m_worldUpdated->Invoke( sender, args );
		}
	}


protected:

	virtual void SetNewtonCallback( NewtonHingeCallBack callback ) abstract;

	System::EventHandler^ m_worldUpdated;
	NewtonHingeSliderUpdateDesc* m_desc;

	unsigned m_retval;

private:

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate unsigned NewtonHingeCallBackDelegate( const NewtonJoint* joint, NewtonHingeSliderUpdateDesc* desc );

	NewtonHingeCallBackDelegate^			m_NewtonHingeCallBackDelegate;

	NewtonHingeCallBack		 m_funcptr_newtonCallback;

	unsigned NewtonCallback( const NewtonJoint* joint, NewtonHingeSliderUpdateDesc* desc );

};



//! hinge joint.
/*!
	simple hinge joint.  implement motors/limits through a callback.
*/
public ref class Hinge : public HingeSliderBase
{
 
public:

	//! constructor
	/*!
		\param world pointer to the MogreNewt::World
		\param child pointer to the child rigid body.
		\param parent pointer to the parent rigid body. pass NULL to make the world itself the parent (aka a rigid joint)
		\param pin direction of the joint pin in global space
	*/
	Hinge( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos, const Mogre::Vector3 pin );

	//! destructor
	~Hinge();	

	//! retrieve the angle around the pin.
  property Mogre::Radian JointAngle { Mogre::Radian get() { return Mogre::Radian(NewtonHingeGetJointAngle( m_joint )); } }

	//! retrieve the rotational velocity around the pin.
  property Mogre::Real JointOmega { Mogre::Real get() { return (Mogre::Real)NewtonHingeGetJointOmega( m_joint ); } }

	//! get the force on the joint.
  property Mogre::Vector3 JointForce { Mogre::Vector3 get(); }


	////////// CALLBACK COMMANDS ///////////
	// the following commands are only valid from inside a hinge callback function

	//! set acceleration around the joint pin
	/*!
		This command is only valid when used inside a custom Hinge callback.
	*/
	void SetCallbackAccel( Mogre::Real accel );

	//! set minimum joint friction.
	/*!
		This command is only valid when used inside a custom Hinge callback.
	*/
	void SetCallbackFrictionMin( Mogre::Real min );

	//! set maximum joint friction
	/*!
		This command is only valid when used inside a custom Hinge callback.
	*/
	void SetCallbackFrictionMax( Mogre::Real max );

	//! get the current physics timestep.
	/*!
		This command is only valid when used inside a custom Hinge callback.
	*/
  property Mogre::Real CallbackTimestep { Mogre::Real get(); }

	//! calculate the acceleration neccesary to stop the joint at the specified angle.
	/*!
		For implementing joint limits.
		This command is only valid when used inside a custom Hinge callback.
	*/
	Mogre::Real CalculateStopAlpha( Mogre::Radian angle );

protected:

	virtual void SetNewtonCallback( NewtonHingeCallBack callback ) override;

};


//! slider joint.
/*!
	simple slider joint.  implement motors/limits through a callback.
*/
public ref class Slider : public HingeSliderBase
{
 
public:

	//! constructor
	/*!
		\param world pointer to the MogreNewt::World
		\param child pointer to the child rigid body.
		\param parent pointer to the parent rigid body. pass NULL to make the world itself the parent (aka a rigid joint)
		\param pin direction of the joint pin in global space
	*/
	Slider( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos, const Mogre::Vector3 pin );

	//! destructor.
	~Slider();

	//! get position of child along the pin
  property Mogre::Real JointPosition { Mogre::Real get() { return (Mogre::Real)NewtonSliderGetJointPosit( m_joint ); } }

	//! get rotational velocity along the pin
  property Mogre::Real JointVeloc { Mogre::Real get() { return (Mogre::Real)NewtonSliderGetJointVeloc( m_joint ); } }

	//! get force on the joint.
  property Mogre::Vector3 JointForce { Mogre::Vector3 get(); }

	////////// CALLBACK COMMANDS ///////////
	// the following commands are only valid from inside a hinge callback function

	//! set the acceleration along the pin.
	/*!
		This command is only valid when used inside a custom Slider callback.
	*/
	void SetCallbackAccel( Mogre::Real accel );

	//! set minimum friction for the joint
	/*!
		This command is only valid when used inside a custom Slider callback.
	*/
	void SetCallbackFrictionMin( Mogre::Real min );

	//! set maximum friction for the joint.
	/*!
		This command is only valid when used inside a custom Slider callback.
	*/
	void SetCallbackFrictionMax( Mogre::Real max );

	//! get current physics timestep.
	/*!
		This command is only valid when used inside a custom Slider callback.
	*/
  property Mogre::Real CallbackTimestep { Mogre::Real get(); }

	//! calculate the acceleration neccesary to stop the joint at the specified distance.
	/*!
		For implementing joint limits.
		This command is only valid when used inside a custom Slider callback.
	*/
	Mogre::Real CalculateStopAccel( Mogre::Real dist );

protected:

	virtual void SetNewtonCallback( NewtonHingeCallBack callback ) override;

};



//!	this class represents a Universal joint.
/*!
	simple universal joint.  implement motors/limits through a callback.
*/
public ref class Universal : public HingeSliderBase
{
 
public:
	
	//! constructor
	/*!
		\param world pointer to the MogreNewt::World
		\param child pointer to the child rigid body.
		\param parent pointer to the parent rigid body. pass NULL to make the world itself the parent (aka a rigid joint)
		\param pos position of the joint in global space
		\param pin0 direction of the first axis of rotation in global space
		\param pin1 direction of the second axis of rotation in global space
	*/
	Universal( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos, const Mogre::Vector3 pin0, const Mogre::Vector3 pin1 );

	//! destructor
	~Universal();

	//! get the angle around pin0.
  property Mogre::Radian JointAngle0 { Mogre::Radian get() { return Mogre::Radian(NewtonUniversalGetJointAngle0( m_joint )); } }

	//! get the angle around pin1.
  property Mogre::Radian JointAngle1 { Mogre::Radian get() { return Mogre::Radian(NewtonUniversalGetJointAngle1( m_joint )); } }

	//! get the rotational velocity around pin0.
  property Mogre::Real JointOmega0 { Mogre::Real get() { return (Mogre::Real)NewtonUniversalGetJointOmega0( m_joint ); } }

	//! get the rotational velocity around pin1.
  property Mogre::Real JointOmega1 { Mogre::Real get() { return (Mogre::Real)NewtonUniversalGetJointOmega1( m_joint ); } }

	//! get the force on the joint.
  property Mogre::Vector3 JointForce { Mogre::Vector3 get(); }

	////////// CALLBACK COMMANDS ///////////
	// the following commands are only valid from inside a hinge callback function

	//! set the acceleration around a particular pin.
	/*
		this function can only be called from within a custom callback.
		\param accel desired acceleration
		\param axis which pin to use (0 or 1)
	*/
	void SetCallbackAccel( Mogre::Real accel, unsigned axis );

	//! set the minimum friction around a particular pin
	/*
		this function can only be called from within a custom callback.
		\param min minimum friction
		\param axis which pin to use (0 or 1)
	*/
	void SetCallbackFrictionMin( Mogre::Real min, unsigned axis );

	//! set the maximum friction around a particular pin.
	/*
		this function can only be called from within a custom callback.
		\param max maximum friction
		\param axis which pin to use (0 or 1)
	*/
	void SetCallbackFrictionMax( Mogre::Real max, unsigned axis );

	//! get the current phsics timestep.
	/*
		this function can only be called from within a custom callback.
	*/
  property Mogre::Real CallbackTimestep { Mogre::Real get(); };

	//! calculate the acceleration neccesary to stop the joint at the specified angle on pin 0.
	/*!
		For implementing joint limits.
		This command is only valid when used inside a custom  callback.
	*/
	Mogre::Real CalculateStopAlpha0( Mogre::Real angle );

	//! calculate the acceleration neccesary to stop the joint at the specified angle on pin 1.
	/*!
		For implementing joint limits.
		This command is only valid when used inside a custom  callback.
	*/
	Mogre::Real CalculateStopAlpha1( Mogre::Real angle );

protected:
	
	virtual void SetNewtonCallback( NewtonHingeCallBack callback ) override;

};



//! UpVector joint.
/*!
	simple upvector joint.  upvectors remove all rotation except for a single pin.  useful for character controllers, etc.
*/
public ref class UpVector : public Joint
{
 
public:
	//! constructor
	/*
		\param world pointer to the MogreNewt::World.
		\param body pointer to the body to apply the upvector to.
		\param pin direction of the upvector in global space.
	*/
	UpVector( World^ world, Body^ body, const Mogre::Vector3 pin );

	//! destructor
	~UpVector();

	//! get/set the pin direction.
	/*
		by calling this function in realtime, you can effectively "animate" the pin.
	*/
  property Mogre::Vector3 Pin
  {
    Mogre::Vector3 get();
    void set( const Mogre::Vector3 pin ) { NewtonUpVectorSetPin( m_joint, &pin.x ); }
  }

};


}	// end NAMESPACE BasicJoints


//! namespace for pre-built custom joints
namespace PrebuiltCustomJoints
{

	//! Custom2DJoint class
	/*!
		This class represents a joint that limits movement to a plane, and rotation only around the normal of that
		plane.  This can be used to create simple 2D simulations.  it also supports limits and acceleration for spinning.
		This joint has been used in a few projects, but is not 100% fully-tested.
	*/
	public ref class Custom2DJoint : public MogreNewt::CustomJoint
	{
	public:
		//! constructor
		Custom2DJoint( MogreNewt::Body^ body, const Mogre::Vector3 pin );

		//! destructor
		~Custom2DJoint() {}

		//! overloaded function that applies the actual constraint.
		virtual void SubmitConstraint(Mogre::Real timeStep, int threadIndex) override;

		//! get the current angle of the joint.
    property Mogre::Radian Angle { Mogre::Radian get() { return mAngle; } }

		//! set rotational limits for the joint.
		void SetLimits( Mogre::Degree min, Mogre::Degree max ) { mMin = min, mMax = max; }
		
		//! gets/sets whether to enable limits or not for the joint.
    property bool LimitsOn
    {
      bool get() { return mLimitsOn; }
      void set( bool onoff ) { mLimitsOn = onoff; }
    }

		//! adds rotational acceleration to the joint (like a motor)
		void AddAccel( Mogre::Real accel ) { mAccel = accel; }

		//! resets the joint angle to 0.  this simply sets the internal variable to zero.
		//! you might want to call this for example after resetting a body.
		void ResetAngle() { mAngle = Mogre::Radian(0.0f); }

		//! get the pin.
    property Mogre::Vector3 Pin { Mogre::Vector3 get() { return mPin; } }

	private:
		Mogre::Vector3 mPin;
		Mogre::Quaternion mLocalOrient0, mLocalOrient1;
		Mogre::Vector3 mLocalPos0, mLocalPos1;

		Mogre::Radian mAngle;

		Mogre::Radian mMin;
		Mogre::Radian mMax;

		bool mLimitsOn;

		Mogre::Real mAccel;
	};

	//! CustomFixedJoint
	/*!
		This joint implements a fully fixed joint, which removes all DOF, creating a completely fixed connection between bodies.
		This is probably the most expensive kind of joint, and should only be used when really needed.
	*/
	public ref class CustomRigidJoint : public MogreNewt::CustomJoint
	{
	public:
		CustomRigidJoint( MogreNewt::Body^ child, MogreNewt::Body^ parent, Mogre::Vector3 dir, Mogre::Vector3 pos);
		~CustomRigidJoint();

		virtual void SubmitConstraint(Mogre::Real timeStep, int threadIndex) override;

	private:
		Mogre::Vector3 mLocalPos0;
		Mogre::Vector3 mLocalPos1;

		Mogre::Quaternion mLocalOrient0;
		Mogre::Quaternion mLocalOrient1;
	};


	//! CutomPulleyJoint
	/*!
		This joint implements a pulley system.  note that this joint only works with 2 bodies attached!
	*/

	//
	//public ref class CustomPulleyJoint : public MogreNewt::CustomJoint
	//{
	//public:
	//	//! constructor
	//	/*!
	//		\param gearRatio float value representing the ratio of movement between parent and child.
	//		\param parent pointer to MogreNewt::Body to be the parent body.
	//		\param child pointer to the MogreNewt::Body to be the child body.
	//		\param parentPin direction vector for movement of parent.
	//		\param childPin direction vector for movement of child.
	//	*/
	//	CustomPulleyJoint( Mogre::Real gearRatio, Body^ parent, Body^ child, const Mogre::Vector3 parentPin, const Mogre::Vector3 childPin );
	//	~CustomPulleyJoint() {}

	//	//! overloaded function to submit the constraint.
	//	virtual void SubmitConstraint(Mogre::Real timeStep, int threadIndex) override;

	//private:
	//	Mogre::Real			mGearRatio;

	//	Mogre::Vector3		mLocalPos0, mLocalPos1;
	//	Mogre::Quaternion	mLocalOrient0, mLocalOrient1;
	//};


	////! CustomGearJoint
	///*!
	//	This class works like 2 gears that mesh, retaining a specific ration between the rotational velocity of the bodies.
	//	The gears don't have to rotate around the same axis, that is why 2 pins are supplied.
	//*/
	//public ref class CustomGearJoint : public MogreNewt::CustomJoint
	//{
	//public:
	//	/*!
	//		\param gearRatio float value representing the ratio of movement between parent and child.
	//		\param parent pointer to MogreNewt::Body to be the parent body.
	//		\param child pointer to the MogreNewt::Body to be the child body.
	//		\param parentPin pin around which the parent's rotation should be tracked.
	//		\param childPin pin around which the child's rotation should be tracked.
	//	*/
	//	CustomGearJoint( Mogre::Real gearRatio, Body^ parent, Body^ child, const Mogre::Vector3 parentPin, const Mogre::Vector3 childPin );
	//	~CustomGearJoint() {}

	//	//! overloaded function to submit the constraint.
	//	virtual void SubmitConstraint(Mogre::Real timeStep, int threadIndex) override;

	//private:
	//	Mogre::Real			mGearRatio;

	//	Mogre::Vector3		mLocalPos0, mLocalPos1;
	//	Mogre::Quaternion	mLocalOrient0, mLocalOrient1;
	//};




}	// end NAMESPACE PrebuiltCustomJoints


}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_BASICJOINTS


