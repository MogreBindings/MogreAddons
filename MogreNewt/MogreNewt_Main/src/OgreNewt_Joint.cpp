#include <assert.h>
#include <OgreNewt_Joint.h>


namespace MogreNewt
{

	
Joint::Joint()
{
	m_NewtonJointDestructorDelegate = gcnew NewtonJointDestructorDelegate( this, &Joint::Destructor );
	m_funcptr_destructor = (NewtonConstraintDestructor) Marshal::GetFunctionPointerForDelegate( m_NewtonJointDestructorDelegate ).ToPointer();
}

Joint::~Joint()
{
	if (m_handle.IsAllocated)
		m_handle.Free();

	if(m_joint)
	{
		if (NewtonJointGetUserData(m_joint))
		{
			NewtonJointSetDestructor( m_joint, NULL );
			NewtonDestroyJoint( m_world->NewtonWorld, m_joint );
		}

		m_joint = NULL;
	}

}


void Joint::Destructor( const NewtonJoint* me )
{
	NewtonJointSetDestructor( me, NULL );
	NewtonJointSetUserData( me, NULL );

	delete this;
}




CustomJoint::CustomJoint( unsigned int maxDOF, Body^ body0, Body^ body1 ) : Joint()
{
	// Keep GC from collecting it until the user or Newton disposes it
	m_handle = GCHandle::Alloc(this);

	m_NewtonSubmitConstraintDelegate = gcnew NewtonSubmitConstraintDelegate( this, &CustomJoint::newtonSubmitConstraint );
	m_NewtonGetInfoDelegate = gcnew NewtonGetInfoDelegate( this, &CustomJoint::newtonGetInfo );
	m_funcptr_submitConstraint = (NewtonUserBilateralCallBack) Marshal::GetFunctionPointerForDelegate( m_NewtonSubmitConstraintDelegate ).ToPointer();
	m_funcptr_getInfo = (NewtonUserBilateralGetInfoCallBack) Marshal::GetFunctionPointerForDelegate( m_NewtonGetInfoDelegate ).ToPointer();

	m_maxDOF = maxDOF;

	m_body0 = body0;
	m_body1 = body1;

	m_world = m_body0->World;

	if (body1)
		m_joint = NewtonConstraintCreateUserJoint( m_world->NewtonWorld, m_maxDOF, m_funcptr_submitConstraint,m_funcptr_getInfo, 
													m_body0->NewtonBody, m_body1->NewtonBody );
	else
		m_joint = NewtonConstraintCreateUserJoint( m_world->NewtonWorld, m_maxDOF, m_funcptr_submitConstraint,m_funcptr_getInfo,
													m_body0->NewtonBody, NULL );

	NewtonJointSetUserData (m_joint, GCHandle::ToIntPtr(m_handle).ToPointer());
	NewtonJointSetDestructor (m_joint, m_funcptr_destructor);

}


void CustomJoint::pinAndDirToLocal( const Mogre::Vector3 pinpt, const Mogre::Vector3 pindir, 
								   Mogre::Quaternion% localOrient0, Mogre::Vector3% localPos0, Mogre::Quaternion% localOrient1, Mogre::Vector3% localPos1 )
{
	localOrient0 = localOrient1 =  Mogre::Quaternion::IDENTITY;
	localPos0 = localPos1 = Mogre::Vector3::ZERO;

	Mogre::Quaternion bodyOrient0 = Mogre::Quaternion::IDENTITY;
	Mogre::Quaternion bodyOrient1 = Mogre::Quaternion::IDENTITY;
	Mogre::Vector3 bodyPos0 = Mogre::Vector3::ZERO;
	Mogre::Vector3 bodyPos1 = Mogre::Vector3::ZERO;

	Mogre::Quaternion pinOrient = grammSchmidt(pindir);

	m_body0->GetPositionOrientation( bodyPos0, bodyOrient0 );

	if (m_body1)
		m_body1->GetPositionOrientation( bodyPos1, bodyOrient1 );

	localPos0 = bodyOrient0.Inverse() * (pinpt - bodyPos0);
	localOrient0 = pinOrient * bodyOrient0.Inverse();

	localPos1 = bodyOrient1.Inverse() * (pinpt - bodyPos1);
	localOrient1 = pinOrient * bodyOrient1.Inverse();

}


void CustomJoint::localToGlobal( const Mogre::Quaternion localOrient, const Mogre::Vector3 localPos, Mogre::Quaternion% globalOrient, Mogre::Vector3% globalPos, int bodyIndex )
{
	globalOrient = Mogre::Quaternion::IDENTITY;
	globalPos= Mogre::Vector3::ZERO;

	Body^ bdy;
	if (bodyIndex == 0)
		bdy = m_body0;
	else if (m_body1)
		bdy = m_body1;

	Mogre::Quaternion bodyOrient = Mogre::Quaternion::IDENTITY;
	Mogre::Vector3 bodyPos = Mogre::Vector3::ZERO;

	if (bdy)
		bdy->GetPositionOrientation( bodyPos, bodyOrient );

	globalPos = (bodyOrient * localPos) + bodyPos;
	globalOrient = bodyOrient * localOrient;
}

	

void CustomJoint::addLinearRow( const Mogre::Vector3 pt0, const Mogre::Vector3 pt1, const Mogre::Vector3 dir )
{
	NewtonUserJointAddLinearRow( m_joint, &pt0.x, &pt1.x, &dir.x );
}

void CustomJoint::addAngularRow( Mogre::Radian relativeAngleError, const Mogre::Vector3 dir )
{
	NewtonUserJointAddAngularRow( m_joint, relativeAngleError.ValueRadians, &dir.x );
}

void CustomJoint::addGeneralRow(const Mogre::Vector3 linear0, const Mogre::Vector3 angular0, const Mogre::Vector3 linear1, const Mogre::Vector3 angular1)
{
	float jacobian0[6], jacobian1[6];

	jacobian0[0] = linear0.x;
	jacobian0[1] = linear0.y;
	jacobian0[2] = linear0.z;
	jacobian0[3] = angular0.x;
	jacobian0[4] = angular0.y;
	jacobian0[5] = angular0.z;

	jacobian1[0] = linear1.x;
	jacobian1[1] = linear1.y;
	jacobian1[2] = linear1.z;
	jacobian1[3] = angular1.x;
	jacobian1[4] = angular1.y;
	jacobian1[5] = angular1.z;

	NewtonUserJointAddGeneralRow( m_joint, jacobian0, jacobian1 );
}


void CustomJoint::setRowMinimumFriction( Mogre::Real friction )
{
	NewtonUserJointSetRowMinimumFriction( m_joint, friction );
}


void CustomJoint::setRowMaximumFriction( Mogre::Real friction )
{
	NewtonUserJointSetRowMaximumFriction( m_joint, friction );
}


void CustomJoint::setRowAcceleration( Mogre::Real accel )
{
	NewtonUserJointSetRowAcceleration( m_joint, accel );
}

void CustomJoint::setRowStiffness( Mogre::Real stiffness )
{
	NewtonUserJointSetRowStiffness( m_joint, stiffness );
}

void CustomJoint::setRowSpringDamper(Mogre::Real springK, Mogre::Real springD)
{
	NewtonUserJointSetRowSpringDamperAcceleration( m_joint, springK, springD );
}


void CustomJoint::newtonSubmitConstraint( const NewtonJoint* me ,float timeStep, int threadIndex )
{
	this->SubmitConstraint((Mogre::Real)timeStep, threadIndex);
}


void CustomJoint::newtonGetInfo(const NewtonJoint *me, NewtonJointRecord *info)
{
}


Mogre::Quaternion CustomJoint::grammSchmidt( const Mogre::Vector3 pin )
{
	Mogre::Vector3 front, up, right;
	front = pin;

	front.Normalise();
	if (System::Math::Abs( front.z ) > 0.577f)
		right = front.CrossProduct( Mogre::Vector3(-front.y, front.z, 0.0f) );
	else
		right = front.CrossProduct( Mogre::Vector3(-front.y, front.x, 0.0f) );
	right.Normalise();
	up = right.CrossProduct( front );

	Mogre::Matrix3^ ret = gcnew Mogre::Matrix3();
	ret->FromAxes( front, up, right );

	Mogre::Quaternion quat;
	quat.FromRotationMatrix( ret );

	return quat;
}




}	// end namespace MogreNewt


