#include "OgreNewt_Body.h"
#include "OgreNewt_Tools.h"

namespace MogreNewt
{

void Body::_ctor(MogreNewt::World^ W, MogreNewt::Collision^ col, int bodytype, bool enableGravity)
{
	m_world = W;
	m_collision = col;
	m_type = bodytype;
	m_gravityEnabled = false;
	m_node = nullptr;
	m_matid = nullptr;

	m_userdata = nullptr;

	m_NewtonBodyDestructorDelegate = gcnew NewtonBodyDestructorDelegate( this, &Body::NewtonDestructor );
	m_funcptr_destructor = (NewtonBodyDestructor) Marshal::GetFunctionPointerForDelegate( m_NewtonBodyDestructorDelegate ).ToPointer();

	m_NewtonForceCallbackDelegate = gcnew NewtonForceCallbackDelegate( this, &Body::NewtonForceTorqueCallback );
	m_funcptr_forcecallback = (NewtonApplyForceAndTorque) Marshal::GetFunctionPointerForDelegate( m_NewtonForceCallbackDelegate ).ToPointer();

	m_NewtonTransformCallbackDelegate = gcnew NewtonTransformCallbackDelegate( this, &Body::NewtonTransformCallback );
	m_funcptr_transformcallback = (NewtonSetTransform) Marshal::GetFunctionPointerForDelegate( m_NewtonTransformCallbackDelegate ).ToPointer();

	m_NewtonBuoyancyPlaneCallbackDelegate = gcnew NewtonBuoyancyPlaneCallbackDelegate( this, &Body::NewtonBuoyancyCallback );
	m_funcptr_buoyancycallback = (NewtonGetBuoyancyPlane) Marshal::GetFunctionPointerForDelegate( m_NewtonBuoyancyPlaneCallbackDelegate ).ToPointer();



	m_body = NewtonCreateBody( m_world->NewtonWorld, col->NewtonCollision ); 

	m_nativeInfo = new BodyNativeInfo( this );

	NewtonBodySetUserData( m_body, m_nativeInfo );
	NewtonBodySetDestructorCallback( m_body, m_funcptr_destructor );

	SetupForceCallback();
	IsGravityEnabled = enableGravity;
	m_IsDisposed = false;
}
	
Body::Body( MogreNewt::World^ W, MogreNewt::Collision^ col ) 
{
	_ctor(W, col, 0, false);
}
Body::Body( MogreNewt::World^ W, MogreNewt::Collision^ col, bool enableGravity ) 
{
	_ctor(W, col, 0, enableGravity);
}
Body::Body( MogreNewt::World^ W, MogreNewt::Collision^ col, int bodytype ) 
{
	_ctor(W, col, bodytype, false);
}
Body::Body( MogreNewt::World^ W, MogreNewt::Collision^ col, int bodytype, bool enableGravity ) 
{
	_ctor(W, col, bodytype, enableGravity);
}

Body::~Body()
{
	if (m_nativeInfo)
	{
		delete m_nativeInfo;
		m_nativeInfo = NULL;
	}

	if (m_body)
	{
		if (NewtonBodyGetUserData(m_body))
		{
			NewtonBodySetDestructorCallback( m_body, NULL );
			NewtonDestroyBody( m_world->NewtonWorld, m_body );
		}

		m_body = NULL;
	}

	m_IsDisposed = true;
	Disposed(this);
}



// destructor callback
void Body::NewtonDestructor( const ::NewtonBody* body )
{
 	//newton wants to destroy the body..

	// remove destructor callback
	NewtonBodySetDestructorCallback( body, NULL );
	// remove the user data
	NewtonBodySetUserData( body, NULL );

	//now dispose the object.
	delete this;
}



// transform callback
void Body::NewtonTransformCallback( const ::NewtonBody* body, const float* matrix ,int threadIndex  )
{
	Mogre::Quaternion orient;
	Mogre::Vector3 pos;

	MogreNewt::Converter::MatrixToQuatPos( matrix, orient, pos );

	if (m_node)
	{
		Ogre::Node* ogreNode = (Ogre::Node*)m_node;
		ogreNode->setOrientation( orient );
		ogreNode->setPosition( pos );
	}

	Transformed( this, orient, pos,threadIndex  );
}

	
void Body::NewtonForceTorqueCallback( const ::NewtonBody* body ,float timeStep, int threadIndex )
{
	if (m_gravityEnabled)
		Body_standardForceCallback( body,timeStep,threadIndex );

	ForceCallback( this,timeStep,threadIndex );
}

void Body::ApplyGravityForce( MogreNewt::Body^ body )
{
	//apply a simple gravity force.
	Mogre::Real mass;
	Mogre::Vector3 inertia;

	body->GetMassMatrix(mass, inertia);
	Mogre::Vector3 force(0,-9.8,0);
	force *= mass;

	body->AddForce( force );

}



int Body::NewtonBuoyancyCallback(const int collisionID, void *context, const float* globalSpaceMatrix, float* globalSpacePlane)
{
	Mogre::Quaternion orient;
	Mogre::Vector3 pos;

	MogreNewt::Converter::MatrixToQuatPos( globalSpaceMatrix, orient, pos );

	// call our user' function to get the plane.
	Mogre::Plane theplane;
	
	if (m_buoyancyPlaneCallback(collisionID, this, orient, pos, theplane))
	{
		globalSpacePlane[0] = theplane.normal.x;
		globalSpacePlane[1] = theplane.normal.y;
		globalSpacePlane[2] = theplane.normal.z;

		globalSpacePlane[3] = theplane.d;

		return 1;
	}

	return 0;
}


// attachToNode
void Body::AttachNode( Mogre::Node^ node )
{
	m_node = node;
	m_nativeInfo->node = (Ogre::Node*)node;
	SetupTransformCallback();
}

void Body::SetPositionOrientation( Mogre::Vector3 pos, Mogre::Quaternion orient )
{
	if (m_body)
	{
		float matrix[16];

		MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );
		NewtonBodySetMatrix( m_body, &matrix[0] );

		if (m_node)
		{
			Ogre::Node* ogreNode = m_node;
			ogreNode->setOrientation( orient );
			ogreNode->setPosition( pos );
		}
	}
}

// set mass matrix
void Body::SetMassMatrix( Mogre::Real mass, const Mogre::Vector3 inertia )
{
	if (m_body)
		NewtonBodySetMassMatrix( m_body, (float)mass, (float)inertia.x, (float)inertia.y, (float)inertia.z );
}


void Body::ForceCallback::add(ForceCallbackHandler^ hnd)
{
	bool wasEmpty = (m_forceCallback == nullptr);

	m_forceCallback += hnd;

	if (wasEmpty)
		SetupForceCallback();
}

void Body::ForceCallback::remove(ForceCallbackHandler^ hnd)
{
	m_forceCallback -= hnd;

	if (m_forceCallback == nullptr)
	{
		SetupForceCallback();
	}
}


void Body::Transformed::add(TransformEventHandler^ hnd)
{
	bool wasEmpty = (m_transformed == nullptr);

	m_transformed += hnd;

	if (wasEmpty)
		SetupForceCallback();
}

void Body::Transformed::remove(TransformEventHandler^ hnd)
{
	m_transformed -= hnd;

	if (m_transformed == nullptr)
	{
		SetupTransformCallback();
	}
}


void Body::Disposed::add(DisposeEventHandler^ hnd)
{
	m_dispose += hnd;
}

void Body::Disposed::remove(DisposeEventHandler^ hnd)
{
	m_dispose -= hnd;
}


void Body::IsGravityEnabled::set(bool enableGravity)
{
	m_gravityEnabled = enableGravity;
	SetupForceCallback();
}


void Body::SetupTransformCallback()
{
	if (m_transformed)
	{
		NewtonBodySetTransformCallback( m_body, m_funcptr_transformcallback );
	}
	else if (m_node)
	{
		NewtonBodySetTransformCallback( m_body, Body_standardTransformCallback );
	}
	else
	{
		NewtonBodySetTransformCallback( m_body, NULL );
	}
}

void Body::SetupForceCallback()
{
	if (m_forceCallback)
	{
		NewtonBodySetForceAndTorqueCallback( m_body, m_funcptr_forcecallback );
	}
	else if (m_gravityEnabled)
	{
		NewtonBodySetForceAndTorqueCallback( m_body, Body_standardForceCallback );
	}
	else
	{
		NewtonBodySetForceAndTorqueCallback( m_body, NULL );
	}
}


//set collision
void Body::Collision::set( MogreNewt::Collision^ col )
{
	NewtonBodySetCollision( m_body, col->NewtonCollision );

	m_collision = col;
}

//get collision
MogreNewt::Collision^ Body::Collision::get()
{
	return m_collision;
}

Mogre::Vector3 Body::Position::get()
{
  Mogre::Vector3 pos;
  Mogre::Quaternion orient;
  GetPositionOrientation(pos, orient);
  return pos;
}

Mogre::Quaternion Body::Orientation::get()
{
  Mogre::Vector3 pos;
  Mogre::Quaternion orient;
  GetPositionOrientation(pos, orient);
  return orient;
}


Mogre::AxisAlignedBox^ Body::BoundingBox::get()
{
	Mogre::Vector3 min, max;
	NewtonBodyGetAABB( m_body, &min.x, &max.x );

	Mogre::AxisAlignedBox^ ret = gcnew Mogre::AxisAlignedBox(min,max);
	return ret;
}

Mogre::Real Body::Mass::get()
{
  Mogre::Real mass;
  Mogre::Vector3 inertia;
  GetMassMatrix(mass, inertia);
  return mass;
}

Mogre::Vector3 Body::Intertia::get()
{
  Mogre::Real mass;
  Mogre::Vector3 inertia;
  GetMassMatrix(mass, inertia);
  return inertia;
}

Mogre::Real Body::InvertMass::get()
{
  Mogre::Real mass;
  Mogre::Vector3 inertia;
  GetInvMass(mass, inertia);
  return mass;
}

Mogre::Vector3 Body::InvertIntertia::get()
{
  Mogre::Real mass;
  Mogre::Vector3 inertia;
  GetInvMass(mass, inertia);
  return inertia;
}

// get position and orientation
void Body::GetPositionOrientation( Mogre::Vector3% pos, Mogre::Quaternion% orient )
{
	float matrix[16];

	NewtonBodyGetMatrix( m_body, matrix );
	MogreNewt::Converter::MatrixToQuatPos( matrix, orient, pos );
}

void Body::GetMassMatrix( Mogre::Real% mass, Mogre::Vector3% inertia )
{
	pin_ptr<Mogre::Real> p_mass = &mass;
	pin_ptr<Mogre::Vector3> p_inertia = &inertia;
	NewtonBodyGetMassMatrix( m_body, p_mass, &p_inertia->x, &p_inertia->y, &p_inertia->z );
}

void Body::GetInvMass( Mogre::Real% mass, Mogre::Vector3% inertia )
{
	pin_ptr<Mogre::Real> p_mass = &mass;
	pin_ptr<Mogre::Vector3> p_inertia = &inertia;
	NewtonBodyGetInvMass( m_body, p_mass, &p_inertia->x, &p_inertia->y, &p_inertia->z );
}

Mogre::Vector3 Body::Omega::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetOmega( m_body, &ret.x );
	return ret;
}

Mogre::Vector3 Body::Velocity::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetVelocity( m_body, &ret.x );
	return ret;
}

Mogre::Vector3 Body::Force::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetForce( m_body, &ret.x );
	return ret;
}

Mogre::Vector3 Body::Torque::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetTorque( m_body, &ret.x );
	return ret;
}


Mogre::Vector3 Body::ForceAcceleration::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetForceAcc( m_body, &ret.x );
	return ret;
}

Mogre::Vector3 Body::TorqueAcceleration::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetTorqueAcc( m_body, &ret.x );
	return ret;
}

Mogre::Vector3 Body::CalculateInverseDynamicsForce(Mogre::Real timestep,Mogre::Vector3 desiredVelocity)
{
	Mogre::Vector3 ret;
    NewtonBodyCalculateInverseDynamicsForce(m_body, timestep, &desiredVelocity.x, &ret.x);
    return ret;
}


Mogre::Vector3 Body::AngularDamping::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetAngularDamping( m_body, &ret.x );
	return ret;
}

Mogre::Vector3 Body::CenterOfMass::get()
{
	Mogre::Vector3 ret;
	NewtonBodyGetCentreOfMass( m_body, &ret.x );
	return ret;
}

void Body::AddBouyancyForce( Mogre::Real fluidDensity, Mogre::Real fluidLinearViscosity, Mogre::Real fluisAngularViscosity, const Mogre::Vector3 gravity, BuoyancyPlaneCallbackHandler^ callback )
{
	m_buoyancyPlaneCallback = callback;
	NewtonGetBuoyancyPlane newtonCall = (callback) ? m_funcptr_buoyancycallback : NULL;

	NewtonBodyAddBuoyancyForce( m_body, fluidDensity, fluidLinearViscosity, fluisAngularViscosity,
		&gravity.x, newtonCall, m_nativeInfo );

	m_buoyancyPlaneCallback = nullptr;
}

void Body::AddGlobalForce( const Mogre::Vector3 force, const Mogre::Vector3 pos )
{
	Mogre::Vector3 bodypos;
	Mogre::Quaternion bodyorient;
	GetPositionOrientation( bodypos, bodyorient );

	Mogre::Vector3 topoint = pos - bodypos;
	Mogre::Vector3 torque = topoint.CrossProduct( force );

	AddForce( force );
	AddTorque( torque );
}

void Body::AddLocalForce( const Mogre::Vector3 force, const Mogre::Vector3 pos )
{
	Mogre::Vector3 bodypos;
	Mogre::Quaternion bodyorient;

	GetPositionOrientation( bodypos, bodyorient );

	Mogre::Vector3 globalforce = bodyorient * force;
	Mogre::Vector3 globalpoint = (bodyorient * pos) + bodypos;

	AddGlobalForce( globalforce, globalpoint );
}



Body::ContactJointEnumerator::ContactJointEnumerator(Body^ body) {
  _body = body->NewtonBody;
  Reset();
}

bool Body::ContactJointEnumerator::MoveNext() {
  if (_current == (NewtonJoint*)-1) {
    _current = NewtonBodyGetFirstContactJoint(_body);
  }
  else {
    if (_current != NULL)
      _current = NewtonBodyGetNextContactJoint( _body, _current);
  }

  return (_current != NULL);
}

ContactJoint Body::ContactJointEnumerator::Current::get()
{
  return ContactJoint(_current);
}	

void Body::RequestNodeUpdate( int threadIndex, bool forceNodeUpdate )
{
    if( !m_node )
        return;

    if( m_nodeupdateneeded && !forceNodeUpdate )
        return;

    m_nodeupdateneeded = true;
    m_world->AddBodyUpdateNodeRequest( threadIndex, this );
}

void Body::UpdateNode()
{
    m_nodeupdateneeded = false;

    if( !m_node )
        return;

    Mogre::Vector3 pos;
    Mogre::Quaternion orient;
    GetPositionOrientation(pos, orient);

	if(m_node)
	{
		Ogre::Node* ogreNode = m_node;
		ogreNode->setOrientation( orient );
		ogreNode->setPosition( pos );
	}

}


// --------------------------------------------------------------------------------------


#pragma unmanaged

//Add standard callbacks as native code for better performance

// standard gravity force callback.
void _CDECL Body_standardForceCallback( const ::NewtonBody* body ,float timeStep, int threadIndex)
{
	//apply a simple gravity force.
	Mogre::Real mass;
	Ogre::Vector3 inertia;

	NewtonBodyGetMassMatrix( body, &mass, &inertia.x, &inertia.y, &inertia.z );
	Ogre::Vector3 force(0,-9.8,0);
	force *= mass;

 	NewtonBodyAddForce( body, &force.x );
}

// standard transform callback.
void _CDECL Body_standardTransformCallback( const ::NewtonBody* body, const float* matrix , int threadIndex )
{
	Ogre::Quaternion orient;
	Ogre::Vector3 pos;
	Ogre::Node* node = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body ) )->node;

	MogreNewt::NativeConverters::MatrixToQuatPos( matrix, orient, pos );

	node->setOrientation( orient );
	node->setPosition( pos );
}



}
