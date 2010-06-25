#include <OgreNewt_World.h>
#include <OgreNewt_Body.h>
#include <OgreNewt_BodyInAABBIterator.h>
#include <OgreNewt_MaterialID.h>
#include <OgreNewt_Tools.h>

namespace MogreNewt
{
	// Constructor
	World::World()
	{
		m_limits = gcnew Mogre::AxisAlignedBox(Mogre::Vector3(-100,-100,-100),Mogre::Vector3(100,100,100));
		m_world = NewtonCreate();

		if (!m_world)
		{
			// world not created!
			throw gcnew Exception("Creation of Newton World failed.");
		}

		// create the default ID.
		m_defaultMatID = gcnew MogreNewt::MaterialID( this, NewtonMaterialGetDefaultGroupID( m_world ) );

		m_newtonLeaveWorld = gcnew NewtonLeaveWorldDelegate( this, &World::NewtonLeaveWorld );
		m_fptr_newtonLeaveWorld = (NewtonBodyLeaveWorld) Marshal::GetFunctionPointerForDelegate( m_newtonLeaveWorld ).ToPointer();
		m_debugger = gcnew MogreNewt::Debugger(this);

		m_bodyUpdateNodeRequests = gcnew cli::array<System::Collections::Generic::List<MogreNewt::Body^>^>(GetThreadCount());

		m_bodyInAABBIterator = gcnew BodyInAABBIterator(this);

		m_nativeInfo = new WorldNativeInfo(this);

		NewtonWorldSetUserData( m_world, m_nativeInfo );
		m_IsDisposed = false;
	}

	// Destructor
	World::!World()
	{
		if (m_world)
		{
			NewtonDestroy( m_world );
			m_world = NULL;
		}

		m_IsDisposed = true;

		Disposed(this);
	}

	// update
	void World::Update( Mogre::Real t_step )
	{
		System::Array::Resize<System::Collections::Generic::List<MogreNewt::Body^>^>(m_bodyUpdateNodeRequests,GetThreadCount());
		NewtonUpdate( m_world, (float)t_step );
		m_step = t_step;

		  for(int i =0;i<m_bodyUpdateNodeRequests->Length;i++)
		  {
			  if(m_bodyUpdateNodeRequests[i] != nullptr)
			  {
			    for(int j = 0;j<m_bodyUpdateNodeRequests[i]->Count;j++ )
				{
					if( m_bodyUpdateNodeRequests[i][j]->IsNodeUpdateNeeded)
						m_bodyUpdateNodeRequests[i][j]->UpdateNode();
				}
			  }
		  }
	}


	void World::AddBodyUpdateNodeRequest( int threadIndex, MogreNewt::Body^ body )
	{
		if(m_bodyUpdateNodeRequests[threadIndex] == nullptr)
			m_bodyUpdateNodeRequests[threadIndex] = gcnew System::Collections::Generic::List<MogreNewt::Body^>();
		else
			m_bodyUpdateNodeRequests[threadIndex]->Add(body);
	}

	void World::SetWorldSize( const Mogre::Vector3 min, const Mogre::Vector3 max )
	{
		NewtonSetWorldSize( m_world, (float*)&min.x, (float*)&max.x );
		m_limits = gcnew Mogre::AxisAlignedBox(min, max);
	}

	void World::SetWorldSize( Mogre::AxisAlignedBox^ box )
	{
		NewtonSetWorldSize( m_world, (float*)&box->Minimum, (float*)&box->Maximum );
		m_limits = box;
	}

	void World::LeaveWorld::add(LeaveWorldEventHandler^ hnd)
	{
		if (m_leaveWorld == nullptr)
		{
			NewtonSetBodyLeaveWorldEvent( m_world, m_fptr_newtonLeaveWorld );
		}

		m_leaveWorld += hnd;
	}

	void World::LeaveWorld::remove(LeaveWorldEventHandler^ hnd)
	{
		m_leaveWorld -= hnd;

		if (m_leaveWorld == nullptr)
		{
			NewtonSetBodyLeaveWorldEvent( m_world, NULL );
		}
	}

	void World::Disposed::add(DisposeEventHandler^ hnd)
	{
		m_dispose += hnd;
	}

	void World::Disposed::remove(DisposeEventHandler^ hnd)
	{
		m_dispose -= hnd;
	}


	void World::NewtonLeaveWorld( const NewtonBody* body,int  threadIndex)
	{
		MogreNewt::Body^ b = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( body ) )->managedBody;
		LeaveWorld(b ,threadIndex);
	}

	World::BodyEnumerator::BodyEnumerator(World^ world) 
	{
		_world = world->NewtonWorld;
		Reset();
	}

	bool World::BodyEnumerator::MoveNext() 
	{
	  if (_current == (NewtonBody*)-1) {
		_current = NewtonWorldGetFirstBody(_world);
	  }
	  else {
		if (_current != NULL)
		  _current = NewtonWorldGetNextBody( _world, _current);
	  }
	 
	  return (_current != NULL);
	}

	Body^ World::BodyEnumerator::Current::get()
	{
	  if (_current == NULL)
		return nullptr;
	  return static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( _current ) )->managedBody;
	}
}


