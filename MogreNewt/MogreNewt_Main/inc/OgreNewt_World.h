/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber
*/
#ifndef _INCLUDE_OGRENEWT_WORLD
#define _INCLUDE_OGRENEWT_WORLD

#include <Newton.h>
#include <Ogre.h>
#include "OgreNewt_Prerequisites.h"
#include "OgreNewt_Debugger.h"
#include "OgreNewt_MaterialID.h"


#include <gcroot.h>



//! main namespace.
/*!
	This is the main namespace for the OgreNewt library.  all classes are included in this namespace.
*/
namespace MogreNewt
{


ref class MaterialID;
ref class Body;
ref class World;
ref class BodyInAABBIterator;
class WorldNativeInfo;


public delegate void LeaveWorldEventHandler(MogreNewt::Body^ body,int threadIndex );

//! represents a physics world.
/*!
	this class represents a NewtonWorld, which is the basic space in which physics elements can exist.  It can have various Rigid Bodies, connected by joints, and other constraints.
*/
public ref class World
{

public:

	//! physics solver mode.
	/*!
		you can adjust the accuracy of the solver (and therefore the speed of the solver) using these, or a simple int >= 2.  a value >= 2 represents the number of passes you want the engine to take when attempting to reconcile joints.
	*/
	enum class SolverModelMode 
	{ 
		SM_EXACT    = 0,	/*!< the most accurate simulation. */
		SM_ADAPTIVE = 1,	/*!< still accurate, but faster. */
		SM_2_PASS   = 2,
		SM_3_PASS   = 3,
		SM_4_PASS   = 4,
		SM_5_PASS   = 5,
		SM_6_PASS   = 6,
		SM_7_PASS   = 7,
		SM_8_PASS   = 8,
		SM_9_PASS   = 9,
		SM_10_PASS  = 10,
	};

	//! friction solver mode.
	/*!
		like the physics solver mode, these options allow you to reduce the accuracy of the friction model in exchange for speed.
	*/
	enum class FrictionModelMode 
	{ 
		FM_EXACT = 0,	/*!< exact friction model (default). */
		FM_ADAPTIVE = 1	/*!< adaptive friction mode. (faster but less accurate) */
	};

  enum class PlatformArchitecture
  {
    PA_COMMON_DENOMINATOR = 0,
    PA_FLOAT_ENHANCEMENT  = 1,
    PA_BEST_HARDWARE      = 2
  };

	//! leave world callback.
	/*!
		this function is called when a body leaves the MogreNewt::World.  you can use this to destroy bodies that have left the scene,
		or re-position them, reflect them, do whatever you want.

		callback binding to member classes is exactly the same as the various callbacks for the Body class.
	*/
	event LeaveWorldEventHandler^ LeaveWorld
	{
		void add(LeaveWorldEventHandler^ hnd);
		void remove(LeaveWorldEventHandler^ hnd);

	private:
		void raise( MogreNewt::Body^ body ,int threadIndex)
		{
			if (m_leaveWorld)
				m_leaveWorld->Invoke( body ,threadIndex);
		}
	}

	delegate void DisposeEventHandler(MogreNewt::World^ sender);

	event DisposeEventHandler^ Disposed
	{
		void add(DisposeEventHandler^ hnd);
		void remove(DisposeEventHandler^ hnd);

	private:
		void raise(MogreNewt::World^ sender)
		{
			if(m_dispose)
				m_dispose->Invoke( sender );
		}
	}

	property bool IsDisposed
	{
		bool get() { return m_IsDisposed; }
	}

	Mogre::Real m_step;

public:
	//! Standard Constructor, creates the world.
	World();

	//! Standard Destructor, destroys the world.
	!World();

	~World()
	{
		this->!World();
	}

	//! update the world by the specified time_step.
	/*!
		this function is clamped between values representing fps [60,600].  if you pass a smaller value, it is internally clamped to 60fps.  likewise a value higher than 600fps is treated as 600fs.

		\param t_step Real value representing the time elapsed in seconds.
	*/
	void Update( Mogre::Real t_step );	

	//adding body enumerator for the world, TODO : move it to a specific class like ogrenewt
	value class BodyEnumerator : Collections::Generic::IEnumerator<Body^>
    {
		const ::NewtonWorld* _world;
      NewtonBody* _current;

    internal:
      BodyEnumerator(const NewtonWorld* world) : _world(world) {
        Reset();
      }

    public:                                                            
      BodyEnumerator(World^ world);

      virtual bool MoveNext();                                          

      property Body^ Current                                                
      {                                                            
        virtual Body^ get();
      }                                                            
      property Object^ NonGenericCurrent                                    
      {
      private: virtual Object^ get() sealed = Collections::IEnumerator::Current::get
         {                                                         
           return Current;                                          
         }                                                         
      }                                                            

      virtual void Reset() {
        _current = (NewtonBody*)-1;
      }
    };

    value class BodiesEnumerable : Collections::Generic::IEnumerable<Body^>
    {
      World^ m_World;

    public:
      BodiesEnumerable(World^ world) : m_World(world) {}

    private: virtual Collections::IEnumerator^ NonGenericGetEnumerator() sealed = Collections::IEnumerable::GetEnumerator
             {
               return BodyEnumerator(m_World);
             }
    public: virtual Collections::Generic::IEnumerator<Body^>^ GetEnumerator()
            {
              return BodyEnumerator(m_World);
            }
    };


    property BodiesEnumerable Bodies {
      BodiesEnumerable get() {
        return BodiesEnumerable(this);
      }
    }

	void InvalidateCache() { NewtonInvalidateCache( m_world ); }

	//! retrieves a pointer to the NewtonWorld
	/*!
		in most cases you shouldn't need this... but in case you want to implement something not yet in the library, you can use this to get a pointer to the NewtonWorld object.
		\return pointer to NewtonWorld
	*/
	property const ::NewtonWorld* NewtonWorld
  { 
    const ::NewtonWorld* get() { return m_world; }
  }

	property Mogre::Real TimeStep
	{
		virtual Mogre::Real get()
		{
			return m_step;
		}
	}

	//! get the default materialID object.
	/*!
		when you create a world, a default material is created, which is by default applied to all new rigid bodies.  you might need this pointer when assigning material callbacks, etc.
		\return pointer to a MaterialID^ representing the default material.
	*/
	property MaterialID^ DefaultMaterialID
  { 
    MaterialID^ get() { return m_defaultMatID; } // get pointer to default material ID object.
  }	

	//! remove all bodies from the world.
	/*!
		destroys all Rigid Bodies and Joints in the world. the bodies are properly deleted, so don't try and access any pointers you have laying around!
	*/
	void DestroyAllBodies() { NewtonDestroyAllBodies( m_world ); }

	//! set the physics solver model
	/*!
		setting the solver model allows sacrificing accuracy and realism for speed, good for games, etc.  for a more detailed description of how to use this function, see the Newton documentation.
		\param model int representing the physics model.  you can also pass the enum values I've included.
		\sa SolverModelMode
	*/
	void SetSolverModel(SolverModelMode model) { NewtonSetSolverModel( m_world, (int)model ); }
  
	//! set the physics friction model
	/*!
		setting the friction model allows sacrificing accuracy and realism for speed, good for games, etc. for a more detailed description of how to use this function, see the Newton documentation.
		\param model int representing friction model.  you can also pass the enum values I've included.
		\sa FrictionModelMode
	*/
  void SetFrictionModel(FrictionModelMode model) { NewtonSetFrictionModel( m_world, (int)model ); }
  
	//! specify a specific architecture to use for physics calculations.
	/*!
		Setting to a specific architecture can allow for deterministic physics calculations on systems with different cpus,
		which is particularly useful for multiplayer systems where deterministic physics are absolutely vital.
	*/
	void SetPlatformArchitecture( PlatformArchitecture mode )  { NewtonSetPlatformArchitecture( m_world, (int)mode ); }

	PlatformArchitecture GetPlatformArchitecture([Out] System::String^ %description) {char desc[265]; int mode = NewtonGetPlatformArchitecture(m_world,desc); description = gcnew System::String(desc); return (PlatformArchitecture)System::Enum::ToObject(PlatformArchitecture::typeid,mode);}

	//! get the number of bodies in the simulation.
	/*!
		returns the number of bodies in the simulation.
	*/
	int GetBodyCount() { return NewtonWorldGetBodyCount( m_world ); }

	int GetConstraintCount() { return NewtonWorldGetConstraintCount(m_world); }

	//! multithread settings
	void SetMultithreadSolverOnSingleIsland( int mode ) { NewtonSetMultiThreadSolverOnSingleIsland( m_world, mode ); }

	 //! get multithread settings
    int GetMultithreadSolverOnSingleIsland( ) { return NewtonGetMultiThreadSolverOnSingleIsland( m_world ); }

	//! set the number of threads for the physics simulation to use.
	void SetThreadCount(int threads) { NewtonSetThreadsCount( m_world, threads ); }

	//! get the number of threads the simulation is using.
	int GetThreadCount() { return NewtonGetThreadsCount( m_world ); }

	//! notify an entrance to a critical section of code.
	void CriticalSectionLock() { NewtonWorldCriticalSectionLock( m_world ); }

	//! notify the exit of a critical section of code.
	void CricicalSectionUnlock() { NewtonWorldCriticalSectionUnlock( m_world ); }



	//! set minimum framerate
  void SetMinimumFrameRate(Mogre::Real frame) { NewtonSetMinimumFrameRate( m_world, frame ); }

	//! set the newton world size
	/*!
		setting the world size is very important for a efficient simulation.  although basic collisions will work outside the world limits, other things like raycasting will not work outside the world limits.
		\param min minimum point of the world.
		\param max maximum point of the world.
	*/
	void SetWorldSize( const Mogre::Vector3 min, const Mogre::Vector3 max );

	/*!
		\param box bos describing the size of the world.
	*/
	void SetWorldSize( Mogre::AxisAlignedBox^ box );

	/*!
		get the world limits.
	*/
	property Mogre::AxisAlignedBox^ WorldSize
	{ 
		Mogre::AxisAlignedBox^ get(){return m_limits; }
	}

	//! get the Newton SDK version.
	property int Version 
  { 
    int get() { return NewtonWorldGetVersion( m_world ); }
  }

	//! updates only the collision of the world and call the callback functions if necessary, can be used for an collision only system
	void CollisionUpdate() { NewtonCollisionUpdate( m_world ); }

	property MogreNewt::BodyInAABBIterator^ BodyInAABB
	{
		MogreNewt::BodyInAABBIterator^ get() { return m_bodyInAABBIterator; }
	}

	 //! get the debugger for this world
    /*!
     * the debugger needs to be initialized (Debugger::init(...) ) in order to work correctly
    */
	property MogreNewt::Debugger^ DebuggerInstance
	{
		MogreNewt::Debugger^ get() {  return m_debugger;}
	}

	 //! adds an update request for the body, this means that after the next world update the function body->updateNode will be called, if the bodie needs updating
	void AddBodyUpdateNodeRequest( int threadIndex, MogreNewt::Body^ body ) ;


protected:
	
  ::NewtonWorld* m_world;
	MaterialID^ m_defaultMatID;

	LeaveWorldEventHandler^ m_leaveWorld;

	cli::array<System::Collections::Generic::List<MogreNewt::Body^>^>^ m_bodyUpdateNodeRequests;
	MogreNewt::Debugger^ m_debugger;
	bool m_IsDisposed;

	MogreNewt::BodyInAABBIterator^ m_bodyInAABBIterator;

	WorldNativeInfo* 	m_nativeInfo;


private:

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonLeaveWorldDelegate( const NewtonBody* body,int threadIndex );

	NewtonLeaveWorldDelegate^ m_newtonLeaveWorld;
	DisposeEventHandler^	  m_dispose;
	NewtonBodyLeaveWorld m_fptr_newtonLeaveWorld;

	void NewtonLeaveWorld( const NewtonBody* body,int threadIndex );
	Mogre::AxisAlignedBox^ m_limits;
};


class WorldNativeInfo
{
public:

	gcroot<MogreNewt::World^> managedWorld;

	WorldNativeInfo( MogreNewt::World^ world )
		:	managedWorld( world )

	{
	}
};



}



	
#endif





// _INCLUDE_OGRENEWT_WORLD



