/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_VEHICLE
#define _INCLUDE_OGRENEWT_VEHICLE

#include <Newton.h>
#include "OgreNewt_Body.h"
#include "OgreNewt_Joint.h"
#include "OgreNewt_World.h"

namespace MogreNewt
{
#ifdef INCLUDE_VEHICLE
	ref class Vehicle;

	public ref class TireEventArgs : EventArgs
	{
		Vehicle^ mVehicle;

	public:
		TireEventArgs( Vehicle^ vehicle ) : mVehicle(vehicle)
		{
		}

		property MogreNewt::Vehicle^ Vehicle
		{
			MogreNewt::Vehicle^ get() { return mVehicle; }
		}
	};

//! Represents a wheeled vehicle
/*!
	this class represents a basic vehicle, meant to be inherited by the user, with functionality added.
*/
	public ref class Vehicle abstract
	{
	public:

		//! basic vehicle tire.
		/*!
			secondary class: Tire.  this represents a basic tire.  you add tires by simply creating new tire objects, they are attached to the vehicle object you pass to the constructor.
		*/
		ref class Tire
		{
			void _ctor( Mogre::Vector3 pin, Mogre::Real mass, Mogre::Real width, Mogre::Real radius, Mogre::Real susShock, Mogre::Real susSpring, Mogre::Real susLength, int colID);

		public:

			event EventHandler^ Attached;
			event EventHandler^ Detached;

			//! overloaded constructor.
			/*
				this is the all-important constructor.
				\param vehicle the parent vehicle to which you want to add this tire.
				\param localorient local orientation of the tire (in the space of the chassis rigid body)
				\param localpos local orientation of the tire (in the space of the chassis rigid body)
				\param pin direction of the pin (axle) in the space of the tire
				\param mass mass of the tire
				\param width width of the tire
				\param radius radis of the tire
				\param susShock spring damper coefficient
				\param susSpring spring strength
				\param susLength spring length
				\param colID collision ID used to determine collision in material callbacks.
			*/
			Tire( Mogre::Vector3 pin, Mogre::Real mass, Mogre::Real width, Mogre::Real radius, Mogre::Real susShock, Mogre::Real susSpring, Mogre::Real susLength )
			{
				_ctor( pin, mass, width, radius, susShock, susSpring, susLength, 0);
			}

			Tire( Mogre::Vector3 pin, Mogre::Real mass, Mogre::Real width, Mogre::Real radius, Mogre::Real susShock, Mogre::Real susSpring, Mogre::Real susLength, int colID)
			{
				_ctor( pin, mass, width, radius, susShock, susSpring, susLength, colID);
			}

			//! attach a scenenode to the tire!
			virtual void AttachToNode( Mogre::SceneNode^ node ) { m_node = node; }

			//! update the position of the tire.  this must be called to update the attached scene node to the position of the tire!
			virtual void UpdateNode();

			//! get the Newton ID for this tire.
			property const void* NewtonID
      { 
        const void* get() { return m_tireid; }
      }

			//! get the parent vehicle.
			property MogreNewt::Vehicle^ Vehicle
      { 
        MogreNewt::Vehicle^ get() { return m_vehicle; }
      }

			//! get Ogre::SceneNode.
			property Mogre::SceneNode^ OgreNode 
      { 
        Mogre::SceneNode^ get() { return m_node; }
      }

			//////////////////////////////////////////////////////////////////////
			// Newton functions
			
			//! is the tire airborne?
			property bool IsAirBorne
      { 
        bool get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return NewtonVehicleTireIsAirBorne( m_vehicle->NewtonVehicle, m_tireid ) == 1; 
        }
      }

			//! has the tire lost side grip?
			property bool LostSideGrip
      { 
        bool get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return NewtonVehicleTireLostSideGrip( m_vehicle->NewtonVehicle, m_tireid ) == 1; 
        }
      }

			//! has the tire lost traction?
			property bool LostTraction
      { 
        bool get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return NewtonVehicleTireLostTraction( m_vehicle->NewtonVehicle, m_tireid ) == 1; 
        }
      }

			//! get the rotational velocity of the tire
			property Mogre::Real Omega
      { 
        Mogre::Real get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return (Mogre::Real)NewtonVehicleGetTireOmega( m_vehicle->NewtonVehicle, m_tireid ); 
        }
      }

			//! get the load on the tire (along the suspension normal )
			property Mogre::Real NormalLoad
      { 
        Mogre::Real get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return (Mogre::Real)NewtonVehicleGetTireNormalLoad( m_vehicle->NewtonVehicle, m_tireid ); 
        }
      }

			//! get the current steering angle for this tire
			property Mogre::Radian SteeringAngle 
      { 
        Mogre::Radian get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return Mogre::Radian( NewtonVehicleGetTireSteerAngle( m_vehicle->NewtonVehicle, m_tireid ) ); 
        }
      }

			//! get the lateral speed of the tire (sideways)
			property Mogre::Real LateralSpeed 
      { 
        Mogre::Real get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return NewtonVehicleGetTireLateralSpeed( m_vehicle->NewtonVehicle, m_tireid ); 
        }
      }

			//! get the longitudinal speed of the tire (forward/backward)
			property Mogre::Real LongitudinalSpeed 
      { 
        Mogre::Real get()
        {
          if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  
          return NewtonVehicleGetTireLongitudinalSpeed( m_vehicle->NewtonVehicle, m_tireid ); 
        }
      }

			//! get the position of the tire 
      property Mogre::Vector3 Position
      {
        Mogre::Vector3 get()
        {
          Mogre::Vector3    pos;
          Mogre::Quaternion orient;

          // Lame...
          GetPositionOrientation(orient, pos);
          return pos;
        }
      }

			//! get the orientation of the tire 
      property Mogre::Quaternion Orientation
      {
        Mogre::Quaternion get()
        {
          Mogre::Vector3    pos;
          Mogre::Quaternion orient;

          // Lame...
          GetPositionOrientation(orient, pos);
          return orient;
        }
      }

			//! get the location and orientation of the tire (in global space).
			void GetPositionOrientation( [Out] Mogre::Quaternion% orient, [Out] Mogre::Vector3% pos );

			//! set the torque for this tire.  this must be called in the tire callback!
			void SetTorque( Mogre::Real torque ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleSetTireTorque( m_vehicle->NewtonVehicle, m_tireid, torque ); }

			//! set the steering angle for the tire.  this must be called in the tire callback.
			void SetSteeringAngle( Mogre::Radian angle ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleSetTireSteerAngle( m_vehicle->NewtonVehicle, m_tireid, angle.ValueRadians ); }

			//! calculate the max brake acceleration to stop the tires.
			Mogre::Real CalculateMaxBrakeAcceleration() { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  return (Mogre::Real)NewtonVehicleTireCalculateMaxBrakeAcceleration( m_vehicle->NewtonVehicle, m_tireid ); }

			//! set the brake acceleration
			void SetBrakeAcceleration( Mogre::Real accel, Mogre::Real limit ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleTireSetBrakeAcceleration( m_vehicle->NewtonVehicle, m_tireid, (float)accel, (float)limit ); }

			//! max side slip speed
			void SetMaxSideSlipSpeed( Mogre::Real speed ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleSetTireMaxSideSleepSpeed( m_vehicle->NewtonVehicle, m_tireid, (float)speed ); }

			//! set side slip coefficient
			void SetSideSlipCoefficient( Mogre::Real coefficient ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleSetTireSideSleepCoeficient( m_vehicle->NewtonVehicle, m_tireid, (float)coefficient ); }

			//! max longitudinal slip speed
			void SetMaxLongitudinalSlipSpeed( Mogre::Real speed ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleSetTireMaxLongitudinalSlideSpeed( m_vehicle->NewtonVehicle, m_tireid, (float)speed ); }

			//! set longitudinal slip coefficient
			void SetLongitudinalSlipCoefficient( Mogre::Real coefficient ) { if (!m_vehicle) throw gcnew Exception("The tire is not attached to a vehicle.");  NewtonVehicleSetTireLongitudinalSlideCoeficient( m_vehicle->NewtonVehicle, m_tireid, (float)coefficient ); }

		protected public:

      virtual void OnAttached( MogreNewt::Vehicle^ vehicle )
			{
				Attached( this, gcnew TireEventArgs( vehicle ) );
			}

			virtual void OnDetached( MogreNewt::Vehicle^ vehicle )
			{
				Detached( this, gcnew TireEventArgs( vehicle ) );
			}


			MogreNewt::Vehicle^ m_vehicle;
			void* m_tireid;

			Mogre::SceneNode^ m_node;

			Mogre::Vector3 m_pin;
			Mogre::Real m_mass;
			Mogre::Real m_width;
			Mogre::Real m_radius;
			Mogre::Real m_susShock;
			Mogre::Real m_susSpring;
			Mogre::Real m_susLength;
			int m_colID;
		};


		event EventHandler^ Disposing;

		//! constructor
		Vehicle();

		//! destructor
		virtual ~Vehicle();

		//! initialize the vehicle
		/*!
			this function should be called to initialize the vehicle, assigning it's main chassis rigid body.  the function
			also calls the virtual function "setup" which the user should implement to add tires to the vehicle.
			\param chassis pointer to the MogreNewt::Body to use as the chassis.
			\param updir unit vector signifying which direction is "up" in your world.  used for suspension.
		*/
		void Init( MogreNewt::Body^ chassis, const Mogre::Vector3 updir );

		//! destroy the vehicle, including chassis Body.
		void Destroy();	

		//! setup the tires.
		/*!
			this is a virtual function that must be implemented by the user.  you should add and balance all tires in this function.
		*/
		virtual void Setup() = 0;

		//! user callback for controlling the vehicle
		/*!
			callback called each frame.  inside this callback you can add torque and steering to the tires to control the vehicle.
		*/
		virtual void UserCallback() {}

		//! get the chassis body.
		property MogreNewt::Body^ ChassisBody 
    { 
      MogreNewt::Body^ get() { return m_chassis; }
    }

		//! get the NewtonJoint for the vehicle.
		property NewtonJoint* NewtonVehicle 
    { 
      NewtonJoint* get() { return m_vehicle; }
    }

		//////////////////////////////////////////////////////////////
		// Newton Vehicle functions.

		//! reset the vehicle (stop all tires)
		void Reset() { NewtonVehicleReset( m_vehicle ); }

		virtual void AttachTire( Tire^ tire, Mogre::Quaternion localorient, Mogre::Vector3 localpos );

		virtual void DetachTire( Tire^ tire );

		void DetachAllTires();

		property System::Collections::Generic::IEnumerable<Tire^>^ Tires
		{
			System::Collections::Generic::IEnumerable<Tire^>^ get()
			{
				return m_tires;
			}
		}

		property int TiresNum
		{
			int get() { return m_tires->Count; }
		}

		Tire^ GetTireAt( int index )
		{
			return m_tires[index];
		}


	protected:

		virtual void OnDisposing()
		{
			Disposing( this, EventArgs::Empty );
		}


		MogreNewt::Body^ m_chassis;

		System::Collections::Generic::List<Tire^>^ m_tires;

	private:

		NewtonJoint* m_vehicle;


		GCHandle m_handle;

		[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
		delegate void NewtonJointDestructorDelegate( const NewtonJoint* me );

		[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
		delegate void NewtonVehicleUpdateDelegate( const NewtonJoint* me );

		NewtonJointDestructorDelegate^			m_NewtonJointDestructorDelegate;
		NewtonVehicleUpdateDelegate^			m_NewtonVehicleUpdateDelegate;

		NewtonConstraintDestructor 		m_funcptr_destructor;
		NewtonVehicleTireUpdate 		m_funcptr_update;


		//! callback for newton... it calls the userCallback() function for you.
		void NewtonCallback( const NewtonJoint* me );

		void NewtonDestructor( const NewtonJoint* me );

	};


#endif

}	// end namespace MogreNewt


#endif	// _INCLUDE_OGRENEWT_VEHICLE


