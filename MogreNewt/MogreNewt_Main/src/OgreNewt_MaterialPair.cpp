#include <OgreNewt_MaterialPair.h>
#include <OgreNewt_World.h>
#include <OgreNewt_MaterialID.h>

namespace MogreNewt
{

	
MaterialPair::MaterialPair( World^ world, MaterialID^ mat1, MaterialID^ mat2 )
{
	m_world = world;
	id0 = mat1;
	id1 = mat2;
}


void MaterialPair::SetContactCallback( MogreNewt::ContactCallback^ callback )
{
	m_contactCallback = callback;

	if (callback)
	{
		// set the material callbacks to the functions inside the ContactCallback class.
		NewtonMaterialSetCollisionCallback( m_world->NewtonWorld, id0->ID, id1->ID, NULL,
			callback->m_funcptr_AABBOverlap,
			callback->m_funcptr_contactProcess );
	}
	else
	{
		NewtonMaterialSetCollisionCallback( m_world->NewtonWorld, id0->ID, id1->ID, NULL,
			NULL,
			NULL );
	}
}



}


