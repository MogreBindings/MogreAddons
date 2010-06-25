/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.


	please note that the "boost" library files included here are not of my creation, refer to those files and boost.org for information.


		by Walaber

*/

#ifndef _INCLUDE_OGRENEWT_BODY
#define _INCLUDE_OGRENEWT_BODY


#include <Ogre.h>
#include <Newton.h>
#include "OgreNewt_Prerequisites.h"
#include "OgreNewt_MaterialID.h"
#include "OgreNewt_Collision.h"
#include "ContactJoint.h"

#include <gcroot.h>


// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

ref class Body;
ref class World;
ref class MaterialID;
class BodyNativeInfo;

public delegate void ForceCallbackHandler( MogreNewt::Body^ body ,float timeStep, int threadIndex);
public delegate void TransformEventHandler( MogreNewt::Body^ sender, Mogre::Quaternion orientation, Mogre::Vector3 position ,int threadIndex);
public delegate bool BuoyancyPlaneCallbackHandler( int collisionID, MogreNewt::Body^ body, Mogre::Quaternion orientation, Mogre::Vector3 position, Mogre::Plane% retPlane );
public delegate void AutoactivateEventHandler( MogreNewt::Body^ sender, unsigned int state );

/*
	CLASS DEFINITION:

		Body

	USE:
		this class represents a NewtonBody rigid body!
*/
//! main class for all Rigid Bodies in the system.
public ref class Body
{
public:
	//! custom force callbacFk.
	/*!
		this function is called from within the MogreNewt::World::update() command when applying forces to Rigid Bodies, such as
		gravity, etc.

		You can set this as the custom force callback for a body by using the setCustomForceCallback() function.
		Using boost::function means OgreNewt can now accept pointers to member functions of specific classes.
	*/
	event ForceCallbackHandler^ ForceCallback
	{
		void add(ForceCallbackHandler^ hnd);
		void remove(ForceCallbackHandler^ hnd);

	private:
		void raise( MogreNewt::Body^ body ,float timeStep, int threadIndex)
		{
			if (m_forceCallback)
				m_forceCallback->Invoke( body,timeStep,threadIndex );
		}
	}

	//! custom transform callback.
	/*!
		This function is called from within the MogreNewt::World::update() command for all Rigid Bodies, after all collision and
		forces have been resolved.  this command is intended to allow the user to align a visual object with the new position and
		orientation of the rigid body.  OgreNewt has a general transform callback built-in for use with Ogre.  however you can 
		create your own for special cases.  you are passed a quaternion (orientation) and vector (position) of the rigid body
		in world space.
	*/
	event TransformEventHandler^ Transformed
	{
		void add(TransformEventHandler^ hnd);
		void remove(TransformEventHandler^ hnd);

	private:
		void raise( MogreNewt::Body^ sender, Mogre::Quaternion orientation, Mogre::Vector3 position ,int threadIndex)
		{
			if (m_transformed)
				m_transformed->Invoke( sender, orientation, position,threadIndex );
		}
	}
	
	delegate void DisposeEventHandler(MogreNewt::Body^ sender);

	event DisposeEventHandler^ Disposed
	{
		void add(DisposeEventHandler^ hnd);
		void remove(DisposeEventHandler^ hnd);

	private:
		void raise(MogreNewt::Body^ sender)
		{
			if(m_dispose)
				m_dispose->Invoke( sender );
		}
	}

	property bool IsDisposed
	{
		bool get() { return m_IsDisposed; }
	}

	//! buoyancy plane callback
	/*!
		this function is a user-defined function that supplies Newton with the plane equation for the surface of the liquid when
		applying buoyancy forces.  the user should create their own function for this, that returns an Ogre::Plane based on their
		own criteria.  you get a pointer to the MogreNewt::Body, and it's current orientation and location to boot.  note that this
		callback will be called for each collision primitive in the body (if it is a compound collision).  if you want to
		ignore buoyancy for this collision primitive, just return false from the function.  otherwise, fill the "retPlane" with your
		liquid surface plane, and return true to apply buoyancy to the primitive.
	*/


	property bool IsGravityEnabled
	{
		bool get() { return m_gravityEnabled; }
		void set(bool enableGravity);
	}

	//! constructor.
	/*!
		creates a Rigid Body in an MogreNewt::World, based on a specific collision shape.
		\param W pointer to the MogreNewt::World/
		\param col pointer to an MogreNewt::Collision object that represents the shape of the rigid body.
		\param bodytype simple integer value used to identify the type of rigid body, useful for determining bodies in callbacks.
	*/
	Body( World^ W, MogreNewt::Collision^ col );
	Body( World^ W, MogreNewt::Collision^ col, bool enableGravity );
	Body( World^ W, MogreNewt::Collision^ col, int bodytype );
	Body( World^ W, MogreNewt::Collision^ col, int bodytype, bool enableGravity );

	//! Dispose
	// Body will not get garbage collected until user calls Dispose method or the World that it belongs to is destroyed
	~Body();


	//! get/set user data to connect this class to another.
	/*!
		you can use this to store a pointer to a parent class, etc.  then inside one of the many callbacks, you can get the pointer
		using this "userData" system.
	*/
	property Object^ UserData
  { 
    Object^ get() { return m_userdata; }
    void set(Object^ data ) { m_userdata = data; }
  }

	//! get a pointer to the NewtonBody object
	/*!
		this is the NewtonBody used by the Newton SDK.  in most cases you shouldn't need to access this.
	*/
  property ::NewtonBody* NewtonBody { ::NewtonBody* get() { return m_body; } }

	//! get a pointer to the attached Node.
	/*!
		if you have "attached" this body to an Ogre::Node, this retrieves the node.
	*/
  property Mogre::Node^ OgreNode { Mogre::Node^ get() { return m_node; } }

	//! get a pointer to the MogreNewt::World this body belongs to.
  property MogreNewt::World^ World { MogreNewt::World^ get() { return m_world; } }

	//! get/set the type for this body.
	/*!
		this sets the "type" for the body, which can also be set in the constructor.
		\param type integer value to represent the type of body, e.g. "FLOOR" or "CANON BALL", etc. used for differentation in material callbacks.
	*/
	property int Type 
  { 
    int get() { return m_type; }
    void set( int type ) { m_type = type; }
  }

	//! attach this body to an Ogre::Node*
	/*!
		This is an easy way to connect a Rigid Body with an Ogre::Node.  this automatically sets up a standard Transform callback when you call this.  after calling this, the Ogre::Node will have its position orientation updated to that of the Rigid Body each time you call World::update(), and the body has moved during the update.
	*/
	void AttachNode( Mogre::Node^ node );

	//! position and orient the body arbitrarily.
	/*!
		generally in a physics engine you shouldn't directly set the location/rotation of a Body, because this defies physics laws.  this command exists to set up bodies initially.
		\param orient quaternion representing body orientation in world space.
		\param pos vector representing body position in world space. 
	*/
	void SetPositionOrientation( const Mogre::Vector3 pos, const Mogre::Quaternion orient );

	//! set the mass and inertia for the body.
	/*!
		Set the mass of the Rigid Body.  Inertia is also set here.  Inertia represents a body's "resistance" to rotation around the 3 primary axis.  OgreNewt has a few utility functions that can help you calculate these values based on several primitive shapes.
		\param mass real value for the body mass
		\param inertia vector representing body moment of inertia
	*/
	void SetMassMatrix( Mogre::Real mass, const Mogre::Vector3 inertia );

	//! get/set the body's center of mass
	/*!
		Set a new center of mass for the body that is different than the current, without offsetting the body.
		You can use this to adjust the center of mass of a body at runtime.
	*/
	property Mogre::Vector3 CenterOfMass
	{
	Mogre::Vector3 get();
	void set(const Mogre::Vector3 centerOfMass) { NewtonBodySetCentreOfMass( m_body, &centerOfMass.x ); }
	}

	//! get/set the material for the body
	/*!
		Materials are an extremely powerful way to control body behavior. first create a new MaterialID object, and then pass a pointer
		to apply that material to the body. 
		\param ID pointer to an MogreNewt::MaterialID object to use as the material for the body.
	*/
	property MaterialID^ MaterialGroupID 
	{ 
	MaterialID^ get() { return m_matid; }
	void set( MaterialID^ ID ) { m_matid = ID; NewtonBodySetMaterialGroupID( m_body, m_matid->ID ); }
	}

	//! prevents fast moving bodies from "tunneling" through other bodies.
	/*!
		continuous collision is an advanced feature that prevents fast moving bodies from "tunneling" (missing collision) with other bodies.  there 
		is a performance hit envolved, so this feature should only be used for bodies that have a high likelyhood of tunneling.
		
		note that continuous collision can also be set on a per-material basis via the MaterialPair class.
	*/
	property unsigned ContinuousCollisionMode
  { 
    unsigned get() { return NewtonBodyGetContinuousCollisionMode( m_body ); }
    void set(unsigned state) { NewtonBodySetContinuousCollisionMode( m_body, state ); }
  }

	//! get/set whether all parent/children pairs connected to this body should be allowed to collide.
	property unsigned JointRecursiveCollision
  {
    unsigned get() { return NewtonBodyGetJointRecursiveCollision( m_body ); }
    void set( unsigned state ) { NewtonBodySetJointRecursiveCollision( m_body, state ); } 
  }

	//! get/set an arbitrary omega for the body.
	/*!
		again, setting velocity/omega directly for a body in realtime is not recommended for proper physics behavior.  this function is intended to be used to setup a Body initially.
		\param omega vector representing the desired omega (rotational velocity)
	*/
	property Mogre::Vector3 Omega
  {
    Mogre::Vector3 get();
    void set ( const Mogre::Vector3 omega ) { NewtonBodySetOmega( m_body, &omega.x ); }
  }

	//! set an arbitrary velocity for the body.
	/*!
		again, setting velocity/omega directly for a body in realtime is not recommended for proper physics behavior.  this function is intended to be used to setup a Body initially.
		\param vel vector representing the desired velocity.
	*/
	property Mogre::Vector3 Velocity 
  { 
    Mogre::Vector3 get();
    void set( const Mogre::Vector3 vel ) { NewtonBodySetVelocity( m_body, &vel.x ); }
  }

  property Mogre::Vector3 Force
  {
    Mogre::Vector3 get();
    void set(const Mogre::Vector3 force) { NewtonBodySetForce( m_body, &force.x ); }
  }

  property Mogre::Vector3 Torque
  {
    Mogre::Vector3 get();
    void set(const Mogre::Vector3 torque) { NewtonBodySetTorque( m_body, &torque.x ); }
  }

	//! set the linear damping for the body.
	property Mogre::Real LinearDamping
  {
    Mogre::Real get() { return (Mogre::Real)NewtonBodyGetLinearDamping( m_body ); }
    void set( Mogre::Real damp ) { NewtonBodySetLinearDamping( m_body, (float)damp ); }
  }

	//! get/set the angular damping for the body.
	property Mogre::Vector3 AngularDamping
  { 
	Mogre::Vector3 get();
	void set( const Mogre::Vector3 damp ) { NewtonBodySetAngularDamping( m_body, &damp.x ); }
  }




	//! get/set the collision that represents the shape of the body
	/*!
		This can be used to change the collision shape of a body mid-simulation.  for example making the collision for a character smaller when crouching, etc.
		\param col pointer to the new MogreNewt::Collision shape.
	*/
	  property MogreNewt::Collision^ Collision
	  {
		MogreNewt::Collision^ get();
		void set( MogreNewt::Collision^ col );
	  }

		//! set whether the body should "freeze" when equilibruim is reached.
		/*!
			user-controlled bodies should disable freezing, because frozen bodies' callbacks are not called... so a callback that implements motion based on user input will not be called!
		*/
	  property bool AutoSleep
	  {
		bool get() { return NewtonBodyGetAutoSleep(m_body) == 1; }
		void set( bool flag ) { NewtonBodySetAutoSleep ( m_body, flag ? 1 : 0 ); }
	  }
	  
	  property bool IsFreezed
	  {
		  bool get() { return NewtonBodyGetFreezeState( m_body ) == 1;}
		  void set(bool flag) { NewtonBodySetFreezeState(m_body, flag ? 1:0); }
	  }


	  property Mogre::Vector3 Position { Mogre::Vector3 get(); }

	  property Mogre::Quaternion Orientation { Mogre::Quaternion get(); }

	  property Mogre::AxisAlignedBox^ BoundingBox { Mogre::AxisAlignedBox^ get(); }

	  property Mogre::Real Mass { Mogre::Real get(); }

	  property Mogre::Vector3 Intertia { Mogre::Vector3 get(); }

	  property Mogre::Real InvertMass { Mogre::Real get(); }

	  property Mogre::Vector3 InvertIntertia { Mogre::Vector3 get(); }

	//! get position and orientation in form of an Ogre::Vector(position) and Ogre::Quaternion(orientation)
	void GetPositionOrientation( [Out] Mogre::Vector3% pos, [Out] Mogre::Quaternion% orient );

	//! get Mogre::Real(mass) and Ogre::Vector3(inertia) of the body.
	void GetMassMatrix( [Out] Mogre::Real% mass, [Out] Mogre::Vector3% inertia );

	//! get invert mass + inertia for the body.
	void GetInvMass( [Out] Mogre::Real% mass, [Out] Mogre::Vector3% inertia );

	//! add an impulse (relative change in velocity) to a body.  values are in world coordinates.
	void AddImpulse( const Mogre::Vector3 deltav, const Mogre::Vector3 posit ) { NewtonBodyAddImpulse( m_body, &deltav.x, &posit.x ); }

	//! add force to the body.  
	/*!
		this function is only valid inside a ForceCallback function!
	*/
	void AddForce( const Mogre::Vector3 force ) { NewtonBodyAddForce( m_body, &force.x ); }

	//! add torque to the body.
	/*!
		this function is only valid inside a ForceCallback function!
	*/
	void AddTorque( const Mogre::Vector3 torque ) { NewtonBodyAddTorque( m_body, &torque.x ); }

		//! get the force acting on the body.
	property Mogre::Vector3 ForceAcceleration { Mogre::Vector3 get(); }
	property Mogre::Vector3 TorqueAcceleration { Mogre::Vector3 get(); }

	Mogre::Vector3 CalculateInverseDynamicsForce(Mogre::Real timestep, Mogre::Vector3 desiredVelocity);
	//! set the force for a body.
	/*!
		this function is only valid inside a ForceCallback function!
	*/
	void SetForce( const Mogre::Vector3 force ) { NewtonBodySetForce( m_body, &force.x ); }

	//! set the torque for a body.
	/*!
		this function is only valid inside a ForceCallback function!
	*/
	void SetTorque( const Mogre::Vector3 torque ) { NewtonBodySetTorque( m_body, &torque.x ); }


	//! apply a buoyancy force to the body.
	/*!
		buoyancy is one of the more powerful and overlooked features of the Newton physics system.  you can of course
		simulate floating objects, and even lighter-than-air objects like balloons, etc.
		\param fluidDensity density of the fluid.
		\param fluidLinearViscosity how much the fluid slows linear motion
		\param fluidAngularViscosity how much the fluid slows rotational motion
		\param gravity vector representing world gravity.
		\param buoyancyPlaneCallback user function that returns the plane equation for the fluid at the current location. pass NULL to assume the body is fully immersed in fluid.  see the setCustomForceAndTorqueCallback() docs to info on how to bind class member functions.
	*/
	void AddBouyancyForce( Mogre::Real fluidDensity, Mogre::Real fluidLinearViscosity , Mogre::Real fluidAngularViscosity , const Mogre::Vector3 gravity, BuoyancyPlaneCallbackHandler^ callback );



	//! helper function that adds a force (and resulting torque) to an object in global cordinates.
	/*!
		this function is only valid inside a ForceCallback function!
		\param force vector representing force, in global space
		\param pos vector representing location of force, in global space
	*/
	void AddGlobalForce( const Mogre::Vector3 force, const Mogre::Vector3 pos );

	// helper function that adds a force (and resulting torque) to an object in local coordinates.
	/*!
		this function is only valid inside a ForceCallback function!
		\param force vector representing force, in local space of the body
		\param pos vector representing locatino of force, in local space of the body
	*/
	void AddLocalForce( const Mogre::Vector3 force, const Mogre::Vector3 pos );


	static void ApplyGravityForce( Body^ body );

	void RequestNodeUpdate(int threadIndex, bool forceNodeUpdate);

    //! Return if an node update was requested
    property bool IsNodeUpdateNeeded 
	{ 
		bool get() {return m_nodeupdateneeded;}
	}

    //! update the position of the node (if attached) and sets m_nodeupdateneeded to false
    void UpdateNode();
	
	

	value class ContactJointEnumerator sealed : Collections::Generic::IEnumerator<ContactJoint>
    {
      const ::NewtonBody* _body;
      NewtonJoint* _current;

    public:																				
      ContactJointEnumerator(Body^ body);

      virtual bool MoveNext();														

      property ContactJoint Current																
      {																				
        virtual ContactJoint get();
      }																				
      property Object^ NonGenericCurrent												
      {
      private: virtual Object^ get() sealed = Collections::IEnumerator::Current::get
               {																			
                 return Current;														
               }																			
      }																				

      virtual void Reset() {
        _current = (NewtonJoint*)-1;
      }
    };


    value class ContactJointsEnumerable : Collections::Generic::IEnumerable<ContactJoint>
    {
      Body^ m_body;

    public:
      ContactJointsEnumerable(Body^ body) : m_body(body) {}

    private: virtual Collections::IEnumerator^ NonGenericGetEnumerator() sealed = Collections::IEnumerable::GetEnumerator
             {
               return ContactJointEnumerator(m_body);
             }
    public: virtual Collections::Generic::IEnumerator<ContactJoint>^ GetEnumerator()
            {
              return ContactJointEnumerator(m_body);
            }
    };


    property ContactJointsEnumerable ContactJoints {
      ContactJointsEnumerable get() {
        return ContactJointsEnumerable(this);
      }
    }

 protected:

	 void SetupForceCallback();
	 void SetupTransformCallback();

	 BodyNativeInfo* 	m_nativeInfo;
	 bool				m_gravityEnabled;
	 bool				m_nodeupdateneeded;
	 bool				m_IsDisposed;

	::NewtonBody*			m_body;
	MogreNewt::Collision^	m_collision;
	MogreNewt::MaterialID^	m_matid;
	MogreNewt::World^		m_world;
	
	
	Object^				m_userdata;
	
	int					m_type;
	Mogre::Node^	m_node;

	ForceCallbackHandler^			m_forceCallback;
	TransformEventHandler^			m_transformed;
	BuoyancyPlaneCallbackHandler^	m_buoyancyPlaneCallback;
	AutoactivateEventHandler^		m_autoactivated;
	DisposeEventHandler^			m_dispose;


private:

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
  delegate void NewtonBodyDestructorDelegate( const ::NewtonBody* body );

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonForceCallbackDelegate( const ::NewtonBody* body , float timeStep, int threadIndex);

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonTransformCallbackDelegate( const ::NewtonBody* body, const float* matrix ,int threadIndex);

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate int NewtonBuoyancyPlaneCallbackDelegate( const int collisionID, void* context, const float* globalSpaceMatrix, float* globalSpacePlane );


	NewtonBodyDestructorDelegate^			m_NewtonBodyDestructorDelegate;
	NewtonForceCallbackDelegate^			m_NewtonForceCallbackDelegate;
	NewtonTransformCallbackDelegate^		m_NewtonTransformCallbackDelegate;
	NewtonBuoyancyPlaneCallbackDelegate^	m_NewtonBuoyancyPlaneCallbackDelegate;

	NewtonBodyDestructor		m_funcptr_destructor;
	NewtonApplyForceAndTorque	m_funcptr_forcecallback;
	NewtonSetTransform			m_funcptr_transformcallback;
	NewtonGetBuoyancyPlane		m_funcptr_buoyancycallback;

  void _ctor(MogreNewt::World^ W, MogreNewt::Collision^ col, int bodytype, bool enableGravity);

  void NewtonDestructor( const ::NewtonBody* body );

	void NewtonTransformCallback( const ::NewtonBody* body, const float* matrix,int threadIndex  );
	void NewtonForceTorqueCallback( const ::NewtonBody* body,float timeStep, int threadIndex );

	int NewtonBuoyancyCallback( const int collisionID, void* context, const float* globalSpaceMatrix, float* globalSpacePlane );
};


// BodyNativeInfo is basically used so Body_standardTransformCallback can get the attached Ogre::SceneNode of the Body
// without entering into managed context.
class BodyNativeInfo
{
public:

	gcroot<MogreNewt::Body^> managedBody;
	Ogre::Node* node;

	BodyNativeInfo( MogreNewt::Body^ body )
		:	managedBody( body ),
			node(0)
	{
	}
};




#pragma managed(push, off)


//Add standard callbacks as native code for better performance

// standard gravity force callback.
void _CDECL Body_standardForceCallback( const NewtonBody* body ,float timestep, int threadIndex);

// standard transform callback.
void _CDECL Body_standardTransformCallback( const NewtonBody* body, const float* matrix , int threadIndex  );


#pragma managed(pop)


}

#endif
// _INCLUDE_OGRENEWT_BODY


