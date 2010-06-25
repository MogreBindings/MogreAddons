/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_COLLISION
#define _INCLUDE_OGRENEWT_COLLISION

#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreNewt_CollisionSerializer.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

	enum class CollisionPrimitive
	{
		BoxPrimitive				=	SERIALIZE_ID_BOX,
		ConePrimitive				=	SERIALIZE_ID_CONE,
		EllipsoidPrimitive			=	SERIALIZE_ID_SPHERE,
		CapsulePrimitive			=	SERIALIZE_ID_CAPSULE,
		CylinderPrimitive			=	SERIALIZE_ID_CYLINDER,
		CompoundCollisionPrimitive	=	SERIALIZE_ID_COMPOUND,
		ConvexHullPrimitive			=	SERIALIZE_ID_CONVEXHULL,
		ConvexHullModifierPrimitive	=	SERIALIZE_ID_CONVEXMODIFIER,
		ChamferCylinderPrimitive	=	SERIALIZE_ID_CHAMFERCYLINDER,
		TreeCollisionPrimitive		=	SERIALIZE_ID_TREE,
		NullPrimitive				=	SERIALIZE_ID_NULL,
		HeighFieldPrimitive			=	SERIALIZE_ID_HEIGHTFIELD,
		ScenePrimitive				=	SERIALIZE_ID_SCENE
	};

/*
	CLASS DEFINITION:

		Collision

	USE:
		this class represents a NewtonCollision, which is the newton structure
		for collision geometry.
*/
//! represents a shape for collision detection
public ref class Collision
{
public:

	//! constructor
	Collision( World^ world );

	!Collision();

	//! destructor
	virtual ~Collision()
	{
		this->!Collision();
	}

	//! retrieve the Newton pointer
	/*!
		retrieves the pointer to the NewtonCollision object.
	*/
  property const ::NewtonCollision* NewtonCollision 
  { 
    const ::NewtonCollision* get() { return m_col; }
  }

  /*!
    Returns the Newton world this collision belongs to.
  */
  property MogreNewt::World^ World { MogreNewt::World^ get() { return m_world;} }

	//! set a user ID for collision callback identification
	/*!
		you can set different IDs for each piece in a compound collision object, and then use these IDs in a collision callback to
		determine which part is currently colliding.
	*/
	property unsigned UserID
  { 
    unsigned get() { return NewtonCollisionGetUserID( m_col ); }
    void set( unsigned id ) { NewtonCollisionSetUserID( m_col, id); }
  }

	//! make unique
	void MakeUnique() { NewtonCollisionMakeUnique( m_world->NewtonWorld, m_col ); }

	//! get user ID, for collision callback identification
	

	//! get the Axis-Aligned Bounding Box for this collision shape.
	Mogre::AxisAlignedBox^ GetAABB( const Mogre::Quaternion orient, const Mogre::Vector3 pos );
	Mogre::AxisAlignedBox^ GetAABB( const Mogre::Quaternion orient )
	{
		return GetAABB( orient, Mogre::Vector3::ZERO );
	}
	Mogre::AxisAlignedBox^ GetAABB( const Mogre::Vector3 pos )
	{
		return GetAABB( Mogre::Quaternion::IDENTITY, pos );
	}
	property Mogre::AxisAlignedBox^ AABB
	{
    Mogre::AxisAlignedBox^ get() { return GetAABB( Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO ); }
	}

	property CollisionPrimitive CollisionPrimitiveType
	{
		CollisionPrimitive get() { return GetCollisionPrimitiveType(this);}
	}

	//! Returns the Collisiontype for the given Newton-Collision
	CollisionPrimitive GetCollisionPrimitiveType(Collision^ col);

	//! friend functions for the Serializer
	//friend void CollisionSerializer::exportCollision(Collision^ collision, const Ogre::String& filename);
	//friend void CollisionSerializer::importCollision(Ogre::DataStreamPtr& stream, Collision* pDest);


 protected public:

  ::NewtonCollision* m_col;
	MogreNewt::World^ m_world;

};

//! represents a collision shape that is explicitly convex.
public ref class ConvexCollision : public Collision
{
public:
	//! constructor
	ConvexCollision( MogreNewt::World^ world );

	//! calculate the volume of the collision shape, useful for buoyancy calculations.
	Mogre::Real CalculateVolume() { return (Mogre::Real)NewtonConvexCollisionCalculateVolume( m_col ); }

	//! calculate the moment of inertia for this collision primitive, along with the theoretical center-of-mass for this shape.
	void CalculateInertialMatrix( [Out] Mogre::Vector3% inertia, [Out] Mogre::Vector3% offset )
	{
		pin_ptr<Mogre::Real> p_inertia = &inertia.x;
		pin_ptr<Mogre::Real> p_offset = &offset.x;
		NewtonConvexCollisionCalculateInertialMatrix( m_col, p_inertia, p_offset );
	}

	  //! returns true, if the collision is a trigger-volume
    /*!
       if a collision is marked as a trigger-volume, there's no calculation of contacts, so
       this acts like an accurate aabb test
    */    
	property bool IsTriggerVolume
	{
		bool get() { return NewtonCollisionIsTriggerVolume(m_col) != 0 ;}
		void set(bool Trigger) { NewtonCollisionSetAsTriggerVolume(m_col,(Trigger)?1:0);}
	}

};



//! represents a scalable collision shape.
public ref class ConvexModifierCollision : public Collision
{
public:
	//! constructor
	ConvexModifierCollision( MogreNewt::World^ world, MogreNewt::Collision^ col,int id );

	//! get/set a scalar matrix to the collision
  property Mogre::Matrix4^ ScalarMatrix
  {
    Mogre::Matrix4^ get();
    void set(Mogre::Matrix4^ mat);
  }

};


}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_COLLISION


