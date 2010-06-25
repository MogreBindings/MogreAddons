#include <OgreNewt_BodyInAABBIterator.h>
#include <OgreNewt_World.h>


namespace MogreNewt
{

	BodyInAABBIterator::BodyInAABBIterator(World^ world)
	{
		m_world = world;
		m_callback = nullptr;
		m_NewtonBodyIteratorDelegate = gcnew NewtonBodyIteratorDelegate(this,&BodyInAABBIterator::NewtonIterator);
		m_funcptr_iterator = (NewtonBodyIterator)Marshal::GetFunctionPointerForDelegate( m_NewtonBodyIteratorDelegate ).ToPointer();
	}


	void BodyInAABBIterator::Go(Mogre::AxisAlignedBox^ aabb )
	{		
		NewtonWorldForEachBodyInAABBDo(m_world->NewtonWorld,&aabb->Minimum.x,&aabb->Maximum.x,m_funcptr_iterator,NULL);
	}

	void BodyInAABBIterator::NewtonIterator(const NewtonBody* newtonBody )
	{
		MogreNewt::Body^ body = static_cast<BodyNativeInfo*>( NewtonBodyGetUserData( newtonBody ) )->managedBody;
		NewtonWorld* newtonWorld = NewtonBodyGetWorld(newtonBody);
		MogreNewt::World^ world = static_cast<WorldNativeInfo*>( NewtonWorldGetUserData(newtonWorld) )->managedWorld;
		world->BodyInAABB->IteratorCallback(body);
	}

	void BodyInAABBIterator::IteratorCallback::add(IteratorCallbackHandler^ hnd)
	{
		m_callback += hnd;
	}

	void BodyInAABBIterator::IteratorCallback::remove(IteratorCallbackHandler^ hnd)
	{
		m_callback -= hnd;
	}
}