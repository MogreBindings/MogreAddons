#include <OgreNewt_MaterialID.h>
#include <OgreNewt_World.h>

namespace MogreNewt
{

	
MaterialID::MaterialID( World^ world )
{
	m_world = world;
	id = NewtonMaterialCreateGroupID( m_world->NewtonWorld );
}

MaterialID::MaterialID( World^ world, int ID )
{
	m_world = world;
	id = ID;
}


}


