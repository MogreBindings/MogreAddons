#include <Newton.h>
#include <OgreNewt_BasicJoints.h>
#include <OgreNewt_World.h>
#include <OgreNewt_Body.h>

#include "OgreNewt_Math.h"

namespace MogreNewt
{

namespace BasicJoints
{
	
BallAndSocket::BallAndSocket( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos ) : Joint()
{
	m_world = world;

	if (parent)
		m_joint = NewtonConstraintCreateBall( world->NewtonWorld, &pos.x, child->NewtonBody, parent->NewtonBody );
	else
		m_joint = NewtonConstraintCreateBall( world->NewtonWorld, &pos.x, child->NewtonBody, NULL );


	m_handle = GCHandle::Alloc(this);
	// all constructors inherited from Joint MUST call these 2 functions to make the joint function properly.
	NewtonJointSetUserData( m_joint, GCHandle::ToIntPtr(m_handle).ToPointer() );
	NewtonJointSetDestructor( m_joint, m_funcptr_destructor );

	m_NewtonBallCallBackDelegate = gcnew NewtonBallCallBackDelegate( this, &BallAndSocket::NewtonCallback );
	m_funcptr_newtonCallback = (NewtonBallCallBack) Marshal::GetFunctionPointerForDelegate( m_NewtonBallCallBackDelegate ).ToPointer();
										
}

Mogre::Vector3 BallAndSocket::JointAngle::get()
{
	Mogre::Vector3 ret;

	NewtonBallGetJointAngle( m_joint, &ret.x );

	return ret;
}


Mogre::Vector3 BallAndSocket::JointOmega::get()
{
	Mogre::Vector3 ret;

	NewtonBallGetJointOmega( m_joint, &ret.x );

	return ret;
}


Mogre::Vector3 BallAndSocket::JointForce::get()
{
	Mogre::Vector3 ret;

	NewtonBallGetJointForce( m_joint, &ret.x );

	return ret;
}

void BallAndSocket::WorldUpdated::add(System::EventHandler^ hnd)
{
	if (m_worldUpdated == nullptr)
	{
		SetNewtonCallback( m_funcptr_newtonCallback );
	}

	m_worldUpdated += hnd;
}

void BallAndSocket::WorldUpdated::remove(System::EventHandler^ hnd)
{
	m_worldUpdated -= hnd;

	if (m_worldUpdated == nullptr)
	{
		SetNewtonCallback( NULL );
	}
}



void BallAndSocket::SetNewtonCallback( NewtonBallCallBack callback )
{
	NewtonBallSetUserCallback(m_joint,callback);
}

void BallAndSocket::NewtonCallback( const NewtonJoint* ball, dFloat timestep )
{
	WorldUpdated( this, EventArgs::Empty );
}

///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////


HingeSliderBase::HingeSliderBase() : Joint()
{
	m_NewtonHingeCallBackDelegate = gcnew NewtonHingeCallBackDelegate( this, &HingeSliderBase::NewtonCallback );
	m_funcptr_newtonCallback = (NewtonHingeCallBack) Marshal::GetFunctionPointerForDelegate( m_NewtonHingeCallBackDelegate ).ToPointer();
}

void HingeSliderBase::WorldUpdated::add(System::EventHandler^ hnd)
{
	if (m_worldUpdated == nullptr)
	{
		SetNewtonCallback( m_funcptr_newtonCallback );
	}

	m_worldUpdated += hnd;
}

void HingeSliderBase::WorldUpdated::remove(System::EventHandler^ hnd)
{
	m_worldUpdated -= hnd;

	if (m_worldUpdated == nullptr)
	{
		SetNewtonCallback( NULL );
	}
}

unsigned HingeSliderBase::NewtonCallback( const NewtonJoint* hinge, NewtonHingeSliderUpdateDesc* desc )
{
	m_desc = desc;
	m_retval = 0;

	WorldUpdated( this, EventArgs::Empty );

	m_desc = NULL;

	return m_retval;
}



///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////


void Hinge::SetNewtonCallback( NewtonHingeCallBack callback )
{
	NewtonHingeSetUserCallback( m_joint, callback );
}


Hinge::Hinge( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos, const Mogre::Vector3 pin ) : HingeSliderBase()
{
	m_world = world;

	if (parent)
	{
		m_joint = NewtonConstraintCreateHinge( world->NewtonWorld, &pos.x, &pin.x,
												child->NewtonBody, parent->NewtonBody );
	}
	else
	{
		m_joint = NewtonConstraintCreateHinge( world->NewtonWorld, &pos.x, &pin.x,
												child->NewtonBody, NULL );
	}

	m_handle = GCHandle::Alloc(this);
	NewtonJointSetUserData( m_joint, GCHandle::ToIntPtr(m_handle).ToPointer() );
	NewtonJointSetDestructor( m_joint, m_funcptr_destructor );
}

Hinge::~Hinge()
{
}


Mogre::Vector3 Hinge::JointForce::get()
{
	Mogre::Vector3 ret;

	NewtonHingeGetJointForce( m_joint, &ret.x );

	return ret;
}

/////// CALLBACK FUNCTIONS ///////
void Hinge::SetCallbackAccel( Mogre::Real accel )
{
	if (m_desc)
	{
		m_retval = 1;
		m_desc->m_accel = (float)accel;
	}
}

void Hinge::SetCallbackFrictionMin( Mogre::Real min )
{
	if (m_desc)
	{
		m_retval = 1;
		m_desc->m_minFriction = (float)min;
	}
}

void Hinge::SetCallbackFrictionMax( Mogre::Real max )
{
	if (m_desc)
	{
		m_retval = 1;
		m_desc->m_maxFriction = (float)max;
	}
}

Mogre::Real Hinge::CallbackTimestep::get()
{
	if (m_desc)
		return (Mogre::Real)m_desc->m_timestep;
	else
		return 0.0;
}

Mogre::Real Hinge::CalculateStopAlpha( Mogre::Radian angle )
{
	if (m_desc)
		return (Mogre::Real)NewtonHingeCalculateStopAlpha( m_joint, m_desc, (float)angle.ValueRadians );
	else
		return 0.0;
}



///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////



void Slider::SetNewtonCallback( NewtonHingeCallBack callback )
{
	NewtonSliderSetUserCallback( m_joint, callback );
}


Slider::Slider( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos, const Mogre::Vector3 pin ) : HingeSliderBase()
{
	m_world = world;

	if (parent)
	{
		m_joint = NewtonConstraintCreateSlider( world->NewtonWorld, &pos.x, &pin.x,
												child->NewtonBody, parent->NewtonBody );
	}
	else
	{
		m_joint = NewtonConstraintCreateSlider( world->NewtonWorld, &pos.x, &pin.x,
												child->NewtonBody, NULL );
	}

	m_handle = GCHandle::Alloc(this);
	NewtonJointSetUserData( m_joint, GCHandle::ToIntPtr(m_handle).ToPointer() );
	NewtonJointSetDestructor( m_joint, m_funcptr_destructor );
}

Slider::~Slider()
{
}

Mogre::Vector3 Slider::JointForce::get()
{
	Mogre::Vector3 ret;

	NewtonSliderGetJointForce( m_joint, &ret.x );

	return ret;
}


/////// CALLBACK FUNCTIONS ///////
void Slider::SetCallbackAccel( Mogre::Real accel )
{
	if (m_desc)
	{
		m_retval = 1;
		m_desc->m_accel = (float)accel;
	}
}

void Slider::SetCallbackFrictionMin( Mogre::Real min )
{
	if (m_desc)
	{
		m_retval = 1;
		m_desc->m_minFriction = (float)min;
	}
}

void Slider::SetCallbackFrictionMax( Mogre::Real max )
{
	if (m_desc)
	{
		m_retval = 1;
		m_desc->m_maxFriction = (float)max;
	}
}

Mogre::Real Slider::CallbackTimestep::get()
{
	if (m_desc)
		return (Mogre::Real)m_desc->m_timestep;
	else
		return 0.0;
}

Mogre::Real Slider::CalculateStopAccel( Mogre::Real dist )
{
	if (m_desc)
		return (Mogre::Real)NewtonSliderCalculateStopAccel( m_joint, m_desc, (float)dist );
	else
		return 0.0;
}



///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////



void Universal::SetNewtonCallback( NewtonHingeCallBack callback )
{
	NewtonUniversalSetUserCallback( m_joint, callback );
}


Universal::Universal( World^ world, MogreNewt::Body^ child, MogreNewt::Body^ parent, const Mogre::Vector3 pos, const Mogre::Vector3 pin0, const Mogre::Vector3 pin1 ) : HingeSliderBase()
{
	m_world = world;

	if (parent)
	{
		m_joint = NewtonConstraintCreateUniversal( world->NewtonWorld, &pos.x, &pin0.x, &pin1.x,
												child->NewtonBody, parent->NewtonBody );
	}
	else
	{
		m_joint = NewtonConstraintCreateUniversal( world->NewtonWorld, &pos.x, &pin0.x, &pin1.x,
												child->NewtonBody, NULL );
	}

	m_handle = GCHandle::Alloc(this);
	NewtonJointSetUserData( m_joint, GCHandle::ToIntPtr(m_handle).ToPointer() );
	NewtonJointSetDestructor( m_joint, m_funcptr_destructor );
}

Universal::~Universal()
{
}

Mogre::Vector3 Universal::JointForce::get()
{
	Mogre::Vector3 ret;

	NewtonUniversalGetJointForce( m_joint, &ret.x );

	return ret;
}


/////// CALLBACK FUNCTIONS ///////
void Universal::SetCallbackAccel( Mogre::Real accel, unsigned int axis )
{
	if (axis > 1) { return; }

	if (m_desc)
	{
		m_retval |= axis;
		m_desc[axis].m_accel = (float)accel;
	}
}

void Universal::SetCallbackFrictionMax( Mogre::Real max, unsigned int axis )
{
	if (axis > 1) { return; }

	if (m_desc)
	{
		m_retval |= axis;
		m_desc[axis].m_maxFriction = (float)max;
	}
}

void Universal::SetCallbackFrictionMin( Mogre::Real min, unsigned int axis )
{
	if (axis > 1) { return; }

	if (m_desc)
	{
		m_retval |= axis;
		m_desc[axis].m_minFriction = (float)min;
	}
}

Mogre::Real Universal::CallbackTimestep::get()
{
	if (m_desc)
		return (Mogre::Real)m_desc->m_timestep;
	else
		return 0.0;
}

Mogre::Real Universal::CalculateStopAlpha0( Mogre::Real angle )
{
	if (m_desc)
		return (Mogre::Real)NewtonUniversalCalculateStopAlpha0( m_joint, m_desc, (float)angle );
	else
		return 0.0;
}

Mogre::Real Universal::CalculateStopAlpha1( Mogre::Real angle )
{
	if (m_desc)
		return (Mogre::Real)NewtonUniversalCalculateStopAlpha1( m_joint, m_desc, (float)angle );
	else
		return 0.0;
}


///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////



UpVector::UpVector( World^ world, Body^ body, const Mogre::Vector3 pin )
{
	m_world = world;

	m_joint = NewtonConstraintCreateUpVector( world->NewtonWorld, &pin.x, body->NewtonBody );

	m_handle = GCHandle::Alloc(this);
	NewtonJointSetUserData( m_joint, GCHandle::ToIntPtr(m_handle).ToPointer() );
	NewtonJointSetDestructor( m_joint, m_funcptr_destructor );

}

UpVector::~UpVector()
{
}

Mogre::Vector3 UpVector::Pin::get()
{
	Mogre::Vector3 ret;

	NewtonUpVectorGetPin( m_joint, &ret.x );

	return ret;
}





}	// end NAMESPACE BasicJoints




///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////



///////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////


namespace PrebuiltCustomJoints
{

Custom2DJoint::Custom2DJoint(MogreNewt::Body^ body, const Mogre::Vector3 pin ) : CustomJoint( 4, body, nullptr )
{
	mPin = pin;
	Mogre::Quaternion bodyorient;
	Mogre::Vector3 bodypos;

	body->GetPositionOrientation( bodypos, bodyorient );

	pinAndDirToLocal( bodypos, pin, mLocalOrient0, mLocalPos0, mLocalOrient1, mLocalPos1 );

	// initialize variables
	mMin = mMax = Mogre::Degree(0);
	mLimitsOn = false;
	mAccel = 0.0f;

}


void Custom2DJoint::SubmitConstraint(Mogre::Real timeStep, int threadIndex)
{
	// get the global orientations.
	Mogre::Quaternion globalOrient0, globalOrient1;
	Mogre::Vector3 globalPos0, globalPos1;

	localToGlobal( mLocalOrient0, mLocalPos0, globalOrient0, globalPos0, 0 );
	localToGlobal( mLocalOrient1, mLocalPos1, globalOrient1, globalPos1, 1 );

	// calculate all main 6 vectors.
	Mogre::Vector3 bod0X = globalOrient0 * Mogre::Vector3( Mogre::Vector3::UNIT_X );
	Mogre::Vector3 bod0Y = globalOrient0 * Mogre::Vector3( Mogre::Vector3::UNIT_Y );
	Mogre::Vector3 bod0Z = globalOrient0 * Mogre::Vector3( Mogre::Vector3::UNIT_Z );
	
	Mogre::Vector3 bod1X = globalOrient1 * Mogre::Vector3( Mogre::Vector3::UNIT_X );
	Mogre::Vector3 bod1Y = globalOrient1 * Mogre::Vector3( Mogre::Vector3::UNIT_Y );
	Mogre::Vector3 bod1Z = globalOrient1 * Mogre::Vector3( Mogre::Vector3::UNIT_Z );

	//Mogre::LogManager::Singleton->LogMessage(" Submit Constraint   bod0X:"+ bod0X +
	//	"   bod1X:"+ bod1X );
	
	// ---------------------------------------------------------------
	// first add a linear row to keep the body on the plane.
	addLinearRow( globalPos0, globalPos1, bod0X );

	// have we strayed from the plane along the normal?
	Mogre::Plane thePlane( bod0X, globalPos0 );
	Mogre::Real stray = thePlane.GetDistance( globalPos1 );
	if (stray > 0.0001f)
	{
		// we have strayed, apply acceleration to move back to 0 in one timestep.
		Mogre::Real accel = (stray / timeStep);
		if (thePlane.GetSide( globalPos1 ) == Mogre::Plane::Side::NEGATIVE_SIDE) { accel = -accel; }

		setRowAcceleration( accel );
	}

	// see if the main axis (pin) has wandered off.
	Mogre::Vector3 latDir = bod0X.CrossProduct( bod1X );
	Mogre::Real latMag = latDir.SquaredLength;

	if (latMag > 1.0e-6f)
	{
		// has wandered a bit, we need to correct.  first find the angle off.
		latMag = System::Math::Sqrt( latMag );
		latDir.Normalise();

		Mogre::Radian angle = MogreNewt::Math::ASin( latMag );

		// ---------------------------------------------------------------
		addAngularRow( angle, latDir );

		// ---------------------------------------------------------------
		// secondary correction for stability.
		Mogre::Vector3 latDir2 = latDir.CrossProduct( bod1X );
		addAngularRow( Mogre::Radian(0.0f), latDir2 );
	}
	else
	{
		// ---------------------------------------------------------------
		// no major change, just add 2 simple constraints.
		addAngularRow( Mogre::Radian(0.0f), bod1Y );
		addAngularRow( Mogre::Radian(0.0f), bod1Z );
	}

	// calculate the current angle.
	Mogre::Real cos = bod0Y.DotProduct( bod1Y );
	Mogre::Real sin = (bod0Y.CrossProduct( bod1Y )).DotProduct( bod0X );

	mAngle = Mogre::Radian(System::Math::Atan2( sin, cos));

	if (mLimitsOn)
	{
		if (mAngle > mMax)
		{
			Mogre::Radian diff = mAngle - mMax;

			addAngularRow( diff, bod0X );
			setRowStiffness( 1.0f );
		}
		else if (mAngle < mMin)
		{
			Mogre::Radian diff = mAngle - mMin;

			addAngularRow( diff, bod0X );
			setRowStiffness( 1.0f );
		}
	}
	else
	{
		if (mAccel != 0.0f)
		{
			addAngularRow( Mogre::Radian(0.0f), bod0X );
			setRowAcceleration( mAccel );

			mAccel = 0.0f;
		}
	}
	
}


CustomRigidJoint::CustomRigidJoint(MogreNewt::Body^ child, MogreNewt::Body^ parent, Mogre::Vector3 dir, Mogre::Vector3 pos) : MogreNewt::CustomJoint(6,child,parent)
{
	// calculate local offsets.
	pinAndDirToLocal( pos, dir, mLocalOrient0, mLocalPos0, mLocalOrient1, mLocalPos1 );
}

CustomRigidJoint::~CustomRigidJoint()
{
}

void CustomRigidJoint::SubmitConstraint(Mogre::Real timeStep, int threadIndex)
{
	// get globals.
	Mogre::Vector3 globalPos0, globalPos1;
	Mogre::Quaternion globalOrient0, globalOrient1;

	localToGlobal( mLocalOrient0, mLocalPos0, globalOrient0, globalPos0, 0 );
	localToGlobal( mLocalOrient1, mLocalPos1, globalOrient1, globalPos1, 1 );

	// apply the constraints!
	addLinearRow( globalPos0, globalPos1, globalOrient0 * Mogre::Vector3::UNIT_X );
	addLinearRow( globalPos0, globalPos1, globalOrient0 * Mogre::Vector3::UNIT_Y );
	addLinearRow( globalPos0, globalPos1, globalOrient0 * Mogre::Vector3::UNIT_Z );

	// now find a point off 10 units away.
	globalPos0 = globalPos0 + (globalOrient0 * (Mogre::Vector3::UNIT_X * 10.0f));
	globalPos1 = globalPos1 + (globalOrient1 * (Mogre::Vector3::UNIT_X * 10.0f));

	// apply the constraints!
	addLinearRow( globalPos0, globalPos1, globalOrient0 * Mogre::Vector3::UNIT_Y );
	addLinearRow( globalPos0, globalPos1, globalOrient0 * Mogre::Vector3::UNIT_Z );

	Mogre::Vector3 xdir0 = globalOrient0 * Mogre::Vector3::UNIT_X;
	Mogre::Vector3 xdir1 = globalOrient1 * Mogre::Vector3::UNIT_X;

	Mogre::Radian angle = MogreNewt::Math::ACos( xdir0.DotProduct( xdir1 ) );
	addAngularRow( angle, globalOrient0 * Mogre::Vector3::UNIT_X );
}

/*
CustomPulleyJoint::CustomPulleyJoint( Mogre::Real gearRatio, Body^ parent, Body^ child, const Mogre::Vector3 parentPin, const Mogre::Vector3 childPin ) : MogreNewt::CustomJoint(1, parent, child)
{
	mGearRatio = gearRatio;

	Mogre::Vector3 dummyPos;
	Mogre::Quaternion dummyOrient;
	Mogre::Vector3 pivot(0.0f, 0.0f, 0.0f);

	// calculate local matrices.  in this case we have 2 pins, so we call twice, once for each body.
	pinAndDirToLocal( pivot, parentPin, mLocalOrient0, mLocalPos0, dummyOrient, dummyPos );
	pinAndDirToLocal( pivot, childPin, dummyOrient, dummyPos, mLocalOrient1, mLocalPos1 );	
}

void CustomPulleyJoint::SubmitConstraint()
{
	Mogre::Real w0, w1;
	Mogre::Real deltat;
	Mogre::Real relAccel, relVel;
	Mogre::Vector3 vel0, vel1;

	Mogre::Vector3 globalPos0, globalPos1;
	Mogre::Quaternion globalOrient0, globalOrient1;

	Mogre::Vector3 xdir0, xdir1;

	// get global matrices.
	localToGlobal( mLocalOrient0, mLocalPos0, globalOrient0, globalPos0, 0);
	localToGlobal( mLocalOrient1, mLocalPos1, globalOrient1, globalPos1, 1);

	xdir0 = globalOrient0 * Mogre::Vector3(Mogre::Vector3::UNIT_X);
	xdir1 = globalOrient1 * Mogre::Vector3(Mogre::Vector3::UNIT_X);

	// velocities for both bodies.
	vel0 = m_body0->Velocity;
	vel1 = m_body1->Velocity;

	// relative angular velocity.
	w0 = vel0.DotProduct( xdir0 );
	w1 = vel1.DotProduct( xdir1 );

	// relative velocity.
	relVel = w0 + (mGearRatio * w1);

	deltat = m_body0->World->TimeStep;
	relAccel = relVel / deltat;

	addGeneralRow( xdir0, Mogre::Vector3::ZERO, xdir1, Mogre::Vector3::ZERO );
	setRowAcceleration( relAccel );
}


CustomGearJoint::CustomGearJoint(Mogre::Real gearRatio, MogreNewt::Body^ parent, MogreNewt::Body^ child, const Mogre::Vector3 parentPin, const Mogre::Vector3 childPin) : MogreNewt::CustomJoint(1, parent, child)
{
	mGearRatio = gearRatio;

	Mogre::Vector3 dummyPos;
	Mogre::Quaternion dummyOrient;
	Mogre::Vector3 pivot(0.0f, 0.0f, 0.0f);

	// calculate local matrices.  in this case we have 2 pins, so we call twice, once for each body.
	pinAndDirToLocal( pivot, parentPin, mLocalOrient0, mLocalPos0, dummyOrient, dummyPos );
	pinAndDirToLocal( pivot, childPin, dummyOrient, dummyPos, mLocalOrient1, mLocalPos1 );	
}

void CustomGearJoint::SubmitConstraint()
{
	Mogre::Real w0, w1;
	Mogre::Real deltat;
	Mogre::Real relAccel, relOmega;
	Mogre::Vector3 omega0, omega1;

	Mogre::Vector3 globalPos0, globalPos1;
	Mogre::Quaternion globalOrient0, globalOrient1;

	Mogre::Vector3 xdir0, xdir1;

	// get global matrices.
	localToGlobal( mLocalOrient0, mLocalPos0, globalOrient0, globalPos0, 0);
	localToGlobal( mLocalOrient1, mLocalPos1, globalOrient1, globalPos1, 1);

	xdir0 = globalOrient0 * Mogre::Vector3(Mogre::Vector3::UNIT_X);
	xdir1 = globalOrient1 * Mogre::Vector3(Mogre::Vector3::UNIT_X);

	// velocities for both bodies.
	omega0 = m_body0->Omega;
	omega1 = m_body1->Omega;

	// relative angular velocity.
	w0 = omega0.DotProduct( xdir0 );
	w1 = omega1.DotProduct( xdir1 );

	// relative velocity.
	relOmega = w0 + (mGearRatio * w1);

	deltat = m_body0->World->TimeStep;
	relAccel =  - relOmega / deltat;

	addGeneralRow( Mogre::Vector3::ZERO, xdir0, Mogre::Vector3::ZERO, xdir1 );
	setRowAcceleration( relAccel );
}

*/



}	// end NAMESPACE PrebuiltCustomJoints




}	// end namespace MogreNewt
