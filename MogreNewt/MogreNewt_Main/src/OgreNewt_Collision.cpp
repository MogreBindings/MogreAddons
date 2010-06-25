#include <Ogre.h>
#include <OgreNewt_Collision.h>
#include <OgreNewt_World.h>
#include <OgreNewt_Tools.h>


namespace MogreNewt
{

	
Collision::Collision( MogreNewt::World^ world )
{
	m_world = world;
}

Collision::!Collision()
{
	if (m_world)
	{
		if (m_world->NewtonWorld)
			NewtonReleaseCollision( m_world->NewtonWorld, m_col );

		m_world = nullptr;
	}
}


Mogre::AxisAlignedBox^ Collision::GetAABB( const Mogre::Quaternion orient, const Mogre::Vector3 pos )
{
	Mogre::AxisAlignedBox^ box;
	Mogre::Vector3 min, max;
	float matrix[16];
	MogreNewt::Converter::QuatPosToMatrix( orient, pos, matrix );

	NewtonCollisionCalculateAABB( m_col, matrix, &min.x, &max.x );

	box = gcnew Mogre::AxisAlignedBox(min, max);
	return box;
}


CollisionPrimitive Collision::GetCollisionPrimitiveType(Collision^ col)
{
	NewtonCollisionInfoRecord *info = new NewtonCollisionInfoRecord();

	NewtonCollisionGetInfo( col->NewtonCollision, info );

	return static_cast<CollisionPrimitive>(info->m_collisionType);
}



ConvexCollision::ConvexCollision( MogreNewt::World^ world ) : Collision( world )
{
}



ConvexModifierCollision::ConvexModifierCollision(MogreNewt::World^ world, Collision^ col,int id) : Collision(world)
{
	m_col = NewtonCreateConvexHullModifier( world->NewtonWorld, col->NewtonCollision,id );
}


void ConvexModifierCollision::ScalarMatrix::set( Mogre::Matrix4^ mat )
{
	float matrix[16];
	MogreNewt::Converter::Matrix4ToMatrix( mat, matrix );

	NewtonConvexHullModifierSetMatrix( m_col, matrix );	
}

Mogre::Matrix4^ ConvexModifierCollision::ScalarMatrix::get()
{
	float matrix[16];
	Mogre::Matrix4^ mat;
	
	NewtonConvexHullModifierGetMatrix( m_col, matrix );

	MogreNewt::Converter::MatrixToMatrix4( matrix, mat );

	return mat;
}



}	// end namespace MogreNewt
