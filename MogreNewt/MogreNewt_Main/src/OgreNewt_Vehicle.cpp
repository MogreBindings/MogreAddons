#include <OgreNewt_Vehicle.h>
#include <OgreNewt_Tools.h>

namespace MogreNewt
{
#ifdef INCLUDE_VEHICLE

////////////////////////////////////////////////////////
// TIRE FUNCTIONS

	// constructor
	void Vehicle::Tire::_ctor( Mogre::Vector3 pin, Mogre::Real mass, Mogre::Real width, Mogre::Real radius, Mogre::Real susShock, Mogre::Real susSpring, Mogre::Real susLength, int colID)
	{
		m_vehicle = nullptr;
		m_node = nullptr;
		m_tireid = NULL;
		
		m_pin = pin;
		m_mass = mass;
		m_width = width;
		m_radius = radius;
		m_susShock = susShock;
		m_susSpring = susSpring;
		m_susLength = susLength;
		m_colID = colID;
	}


	void Vehicle::Tire::UpdateNode()
	{
		if (!m_node)
			return;

		Mogre::Quaternion orient;
		Mogre::Vector3 pos;

		GetPositionOrientation( orient, pos );
		m_node->Orientation = orient;
		m_node->Position = pos;
	}

	void Vehicle::Tire::GetPositionOrientation( Mogre::Quaternion% orient, Mogre::Vector3% pos )
	{
		if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");

		float matrix[16];

		NewtonVehicleGetTireMatrix( m_vehicle->NewtonVehicle, m_tireid, matrix );
		MogreNewt::Converter::MatrixToQuatPos( matrix, orient, pos );
	}



///////////////////////////////////////////////////////////////////
// VEHICLE FUNCTIONS

	Vehicle::Vehicle()
	{
		m_vehicle = NULL;
		m_tires = gcnew System::Collections::Generic::List<Tire^>();

		m_NewtonJointDestructorDelegate = gcnew NewtonJointDestructorDelegate( this, &Vehicle::NewtonDestructor );
		m_funcptr_destructor = (NewtonConstraintDestructor) Marshal::GetFunctionPointerForDelegate( m_NewtonJointDestructorDelegate ).ToPointer();
		m_NewtonVehicleUpdateDelegate = gcnew NewtonVehicleUpdateDelegate( this, &Vehicle::NewtonCallback );
		m_funcptr_update = (NewtonVehicleTireUpdate) Marshal::GetFunctionPointerForDelegate( m_NewtonVehicleUpdateDelegate ).ToPointer();
	}

	Vehicle::~Vehicle()
	{
		OnDisposing();

		DetachAllTires();

		if (m_handle.IsAllocated)
			m_handle.Free();

		// destroy the joint!
		if(m_vehicle)
		{
			if (NewtonJointGetUserData(m_vehicle))
			{
				NewtonJointSetDestructor( m_vehicle, NULL );
				NewtonDestroyJoint( m_chassis->World->NewtonWorld, m_vehicle );
			}

			m_vehicle = NULL;
		}
		
	}



	void Vehicle::Init( MogreNewt::Body^ chassis, const Mogre::Vector3 updir )
	{
		// setup the basic vehicle.
		m_chassis = chassis;

		m_vehicle = NewtonConstraintCreateVehicle (chassis->World->NewtonWorld, &updir.x, chassis->NewtonBody );

		
		if (m_handle.IsAllocated)
			m_handle.Free();

		m_handle = GCHandle::Alloc( this );


		// set the user data
		NewtonJointSetUserData( m_vehicle, GCHandle::ToIntPtr(m_handle).ToPointer() );
		NewtonJointSetDestructor( m_vehicle, m_funcptr_destructor );

		//set the tire callback.
		NewtonVehicleSetTireCallback( m_vehicle, m_funcptr_update );

		//now call the user setup function
		Setup();

	}

	// Newton callback.
	void Vehicle::NewtonCallback( const NewtonJoint* me )
	{
		this->UserCallback();
	}


#include "OgreNoMemoryMacros.h"


	void Vehicle::Destroy()
	{
		DetachAllTires();

		// don't let newton call the destructor.
		NewtonJointSetDestructor( m_vehicle, NULL );

		// destroy the chassis.
		if (m_chassis)
		{
			delete m_chassis;
			m_chassis = nullptr;
		}

		// joint is now destroyed.
		m_vehicle = NULL;
	}

	void Vehicle::NewtonDestructor( const NewtonJoint* me )
	{
		NewtonJointSetDestructor( me, NULL );
		NewtonJointSetUserData( me, NULL );

		delete this;
	}

#include "OgreMemoryMacros.h"


	void Vehicle::AttachTire( Tire^ tire, Mogre::Quaternion localorient, Mogre::Vector3 localpos )
	{
		if (tire->m_vehicle != nullptr)
		{
			throw gcnew ArgumentException("The tire is already attached to a vehicle.");
		}

		tire->m_vehicle = this;

		float matrix[16];

		MogreNewt::Converter::QuatPosToMatrix( localorient, localpos, matrix );

		Mogre::Vector3 pin = tire->m_pin;
		
		tire->m_tireid = NewtonVehicleAddTire ( this->NewtonVehicle, matrix, &pin.x, tire->m_mass, tire->m_width, tire->m_radius, 
		       tire->m_susShock, tire->m_susSpring, tire->m_susLength, NULL, tire->m_colID );

		m_tires->Add( tire );

		tire->OnAttached( this );
	}

	void Vehicle::DetachTire( Tire^ tire )
	{
		if ( m_tires->Contains(tire) )
		{
			// remove the tire from the vehicle.
			NewtonVehicleRemoveTire( this->NewtonVehicle, tire->m_tireid );

			tire->m_vehicle = nullptr;
			tire->m_tireid = NULL;

			m_tires->Remove( tire );
		}

		tire->OnDetached( this );
	}

	void Vehicle::DetachAllTires()
	{
		while ( m_tires->Count > 0 )
			DetachTire( m_tires[0] );
	}















#endif


}	// end namespace MogreNewt
