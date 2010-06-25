/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_COLLISIONPRIMITIVES
#define _INCLUDE_OGRENEWT_COLLISIONPRIMITIVES

#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreNewt_Collision.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

	//! set of basic collision shapes
	namespace CollisionPrimitives
	{

		//! face-winding enum.
		public enum class FaceWinding { FW_DEFAULT, FW_REVERSE };

		//! null collision (results in no collision)
		public ref class Null : public MogreNewt::Collision
		{
		public:
			//! constructor
			Null( MogreNewt::World^ world );
		};


		//! standard primitive Box.
		public ref class Box : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				\param world pointer to MogreNewt::World
				\param size vector representing width, height, depth
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			Box( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(size, orient, pos,id);
			}
			Box( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(size, orient, Mogre::Vector3::ZERO,id);
			}
			Box( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(size, Mogre::Quaternion::IDENTITY, pos,id);
			}
			Box( MogreNewt::World^ world, Mogre::Vector3 size,int id) : ConvexCollision( world )
			{
				_ctor(size, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO,id);
			}

		private:
			void _ctor( Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id );
		};

		//! standard primitive Ellipsoid.  
		public ref class Ellipsoid : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				for a sphere, pass the same radius for all 3 axis.
				\param world pointer to MogreNewt::World
				\param size vector representing radius for all 3 axis
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			Ellipsoid( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(size, orient, pos, id);
			}
			Ellipsoid( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(size, orient, Mogre::Vector3::ZERO, id);
			}
			Ellipsoid( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(size, Mogre::Quaternion::IDENTITY, pos, id);
			}
			Ellipsoid( MogreNewt::World^ world, Mogre::Vector3 size,int id) : ConvexCollision( world )
			{
				_ctor(size, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO, id);
			}

		private:
			void _ctor( Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id );
		};

		//! standard primitive cylinder.
		public ref class Cylinder : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				default aligned along the local X axis (x=height).
				\param world pointer to MogreNewt::World
				\param radius radius of the cylinder (Y and Z axis)
				\param height height of the cylinder (X axis)
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			Cylinder( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, pos, id);
			}
			Cylinder( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, Mogre::Vector3::ZERO, id);
			}
			Cylinder( MogreNewt::World^ world,Mogre::Real radius, Mogre::Real height, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, pos, id);
			}
			Cylinder( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO, id);
			}

		private:
			void _ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos , int id);
		};

		//! standard primitive capsule.
		public ref class Capsule : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				default aligned along the local X axis (x=height).
				\param world pointer to MogreNewt::World
				\param radius radius of the capsule (Y and Z axis)
				\param height height of the capsule (X axis)
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			Capsule( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, pos, id);
			}
			Capsule( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, Mogre::Vector3::ZERO, id);
			}
			Capsule( MogreNewt::World^ world,Mogre::Real radius, Mogre::Real height, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, pos, id);
			}
			Capsule( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO, id);
			}

		private:
			void _ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int  id );
		};

		//! standard primitive cone.
		public ref class Cone : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				default aligned along the local X axis (x=height).
				\param world pointer to MogreNewt::World
				\param radius radius of the cone (Y and Z axis)
				\param height height of the cone (X axis)
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			Cone( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, pos,id);
			}
			Cone( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, Mogre::Vector3::ZERO,id);
			}
			Cone( MogreNewt::World^ world,Mogre::Real radius, Mogre::Real height, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, pos,id);
			}
			Cone( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO,id);
			}

		private:
			void _ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id );
		};

		//! filled-donut shape primitive.
		public ref class ChamferCylinder : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				default aligned along the local X axis (x=height).
				\param world pointer to MogreNewt::World
				\param radius radius of the chamfer cylinder (Y and Z axis)
				\param height height of the chamfer cylinder (X axis)
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			ChamferCylinder( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, pos, id);
			}
			ChamferCylinder( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, orient, Mogre::Vector3::ZERO, id);
			}
			ChamferCylinder( MogreNewt::World^ world,Mogre::Real radius, Mogre::Real height, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, pos, id);
			}
			ChamferCylinder( MogreNewt::World^ world, Mogre::Real radius, Mogre::Real height,int id) : ConvexCollision( world )
			{
				_ctor(radius, height, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO, id);
			}

		private:
			void _ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos, int id );
		};	

		//! ConvexHull primitive
		/*!
			 "wrap" around a set cloud of vertices.  a convex hull is the smallest possible convex shape that fully encloses all points supplied.
		 */
		public ref class ConvexHull : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				Overloaded constructor.  pass a SceneNode*, and it will use the vertex data from the first attached object.
				\param world pointer to the MogreNewt::World
				\param node pointer to an Ogre::SceneNode with a single entity attached
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			ConvexHull( MogreNewt::World^ world, Mogre::SceneNode^ node, Mogre::Quaternion orient, Mogre::Vector3 pos,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(node, orient, pos,tolerance, id);
			}
			ConvexHull( MogreNewt::World^ world, Mogre::SceneNode^ node, Mogre::Quaternion orient,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(node, orient, Mogre::Vector3::ZERO,tolerance, id);
			}
			ConvexHull( MogreNewt::World^ world, Mogre::SceneNode^ node, Mogre::Vector3 pos,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(node, Mogre::Quaternion::IDENTITY, pos,tolerance, id);
			}
			ConvexHull( MogreNewt::World^ world, Mogre::SceneNode^ node,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(node, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO,tolerance, id);
			}

			/*!
				Overloaded constructor.  pass a pointer to an array of vertices and the hull will be made from that.
				\param world pointer to the MogreNewt::World
				\param verts pointer to an array of Ogre::Vector3's that contain vertex position data
				\param vertcount number ot vetices in the array
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			ConvexHull( MogreNewt::World^ world, array<Mogre::Vector3>^ verts, Mogre::Quaternion orient, Mogre::Vector3 pos,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(verts, orient, pos,tolerance,id);
			}
			ConvexHull( MogreNewt::World^ world, array<Mogre::Vector3>^ verts, Mogre::Quaternion orient,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(verts, orient, Mogre::Vector3::ZERO,tolerance,id);
			}
			ConvexHull( MogreNewt::World^ world, array<Mogre::Vector3>^ verts, Mogre::Vector3 pos,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(verts, Mogre::Quaternion::IDENTITY, pos,tolerance,id);
			}
			ConvexHull( MogreNewt::World^ world, array<Mogre::Vector3>^ verts,dFloat tolerance,int id) : ConvexCollision( world )
			{
				_ctor(verts, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO,tolerance,id);
			}

		private:
			void _ctor( Mogre::SceneNode^ node, Mogre::Quaternion orient, Mogre::Vector3 pos ,dFloat tolerance,int id);
			void _ctor( array<Mogre::Vector3>^ verts, Mogre::Quaternion orient, Mogre::Vector3 pos ,dFloat tolerance,int id);
		};

		



		//! TreeCollision - complex polygonal collision
		/*!
			TreeCollision objects are general polygon collision objects.  TreeCollision objects have a requirement that their mass must = 0 (aka are have infinite mass)
		*/
		public ref class TreeCollision : public MogreNewt::Collision
		{
		protected:
			//void MakeTreeCollisionFromEntity(Mogre::Entity^ entity, Mogre::Vector3 scale, bool optimize);
			void MakeTreeCollisionFromEntity(Mogre::Entity^ entity, Mogre::Vector3 scale, bool optimize,int id, FaceWinding fw);

		public:
			//! constructor
			/*!
				Create a 'blank' tree collision object.  Can be used for manual TreeCollision creation, or to be used with TreeCollisionSerializer::importTreeCollision
				\param world pointer to the MogreNewt::World
			*/
			TreeCollision( MogreNewt::World^ world);

			//! constructor
			/*!
				Create a tree collision object.
				\param world pointer to the MogreNewt::World
				\param node pointer to an Ogre::SceneNode with a single entity attached.
				\param optimize bool whether you want to optimize the collision or not.
			*/
			TreeCollision( MogreNewt::World^ world, Mogre::SceneNode^ node, bool optimize ,int id);

			TreeCollision( MogreNewt::World^ world, Mogre::SceneNode^ node, bool optimize,int id,FaceWinding fw );
		
			//! constructor
			/*!
				Create a tree collision object.
				\param world pointer to the MogreNewt::World
				\param Entity that the TreeCollision should get the polygons from.
				\param optimize bool whether you want to optimize the collision or not.
			*/
			TreeCollision( MogreNewt::World^ world, Mogre::Entity^ entity, bool optimize ,int id);

			//! constructor
			/*!
				build a TreeCollision from vertice and index information.  This can be used with the dotScene scene manager
				for building TreeCollision objects from each mesh in the scene.
				\param world pointer to MogreNewt::World
				\param numVertices number of vertices passed in the array.
				\param numIndices number of indices passed in the array.
				\param vertices pointer to array of vertices (positions only).
				\param indices pointer to array of indices.
				\param optimize bool whether you want to optimize the collision or not.
			*/
			TreeCollision( MogreNewt::World^ world, array<float>^ vertices, array<int>^ indices, bool optimize,int id);

			TreeCollision( MogreNewt::World^ world, array<float>^ vertices, array<int>^ indices, bool optimize,int id, FaceWinding fw);

			//TreeCollision( MogreNewt::World^ world, array<Mogre::Vector3>^ vertices, array<int>^ indices, bool optimize);

			//! constructor
			/*!
				build a TreeCollision from vertice and index information.  This can be used with the ogre Paging Landscape SceneManager,
				or other custom solutions.
				\param world pointer to MogreNewt::World
				\param numVertices number of vertices in the array.
				\param vertices pointer to array of Ogre::Vector3 vertices (positions only)
				\param indexData pointer to Ogre::IndexData for the mesh
				\param optimize bool whether you want to optimize the collision or not.
			*/
			TreeCollision( MogreNewt::World^ world, array<Mogre::Vector3>^ vertices, Mogre::IndexData^ indexData, bool optimize,int id);

			TreeCollision( MogreNewt::World^ world, array<Mogre::Vector3>^ vertices, Mogre::IndexData^ indexData, bool optimize,int id, FaceWinding fw);

			//! start a tree collision creation
			void Start(int id);

			//! add a poly to the tree collision
			/*!
				Add a single poly to the tree collision.
				\param polys pointer to an array of 3 Vector3D objects representing the global position of each poly.
				\param ID and identifier to assign to this poly, that can be retrieved later upon collision detection.
			*/
			void AddPoly( array<Mogre::Vector3>^ polys, unsigned int ID );

			//! finish the tree collision
			void Finish( bool optimize );
			void Finish()
			{
				Finish(true);
			}

		};

		////////////////////////////////////////////////////////
		//! TreeCollision created by parsing a tree of SceneNodes, adding collision data of all meshes.
		/*!
			Users can inherit this class, and inherit the "getID" function to perform their own filtering on the
			IDs to pass to Newton.  IDs are useful during collision callbacks to determine which part of the world
			is being hit.

			By default, the ID is set to an incrementing integer.
		*/
		public ref class TreeCollisionSceneParser : public TreeCollision
		{
		public:
			TreeCollisionSceneParser( MogreNewt::World^ world );

			//! parse the scene.
			void ParseScene( Mogre::SceneNode^ startNode, bool optimize,int id, FaceWinding fw );

			void ParseScene( Mogre::SceneNode^ startNode, bool optimize,int id )
			{
				ParseScene( startNode, optimize, id,FaceWinding::FW_DEFAULT );
			}

			void ParseScene( Mogre::SceneNode^ startNode,int id )
			{
				ParseScene( startNode, false,id );
			}

		protected:

			//! this is a user-inherited function that lets you filter which Entities will be added to the treeCollision.
			/*!
				return true to add this entity, return false to ignore it.
				You can also change the face winding on an entity-by-entity basis by changing the fw variable from within the filter.
			*/
			//virtual bool EntityFilter( Mogre::SceneNode^ currentNode, Mogre::Entity^ currentEntity ) { return true; }
			virtual bool EntityFilter( Mogre::SceneNode^ currentNode, Mogre::Entity^ currentEntity, FaceWinding fw ) { return true; }

			//! user inherit-able function, allows customization of the ID to be assigned to this group of polygons.
			virtual unsigned int GetID( Mogre::SceneNode^ currentNode, Mogre::Entity^ currentEntity, unsigned int currentSubMesh ) { return count++; }

		private:
			//! recursive function to parse a single scene node.
			//void _ParseNode( Ogre::SceneNode* node, const Ogre::Quaternion& curOrient, const Ogre::Vector3& curPos );
			void _ParseNode( Ogre::SceneNode* node, const Ogre::Quaternion& curOrient, const Ogre::Vector3& curPos, const Ogre::Vector3& curScale, FaceWinding fw, bool startNode );


			static int count = 0;
		};

		////////////////////////////////////////////////////////
		//	COMPOUND COLLISION!

		//! create a compound from several collision pieces.
		public ref class CompoundCollision : public MogreNewt::Collision
		{
		public:
			//! constructor
			/*!
				creates a compound collision object made from an array of simple primitive parts.  can be used to make very complex
				collision shapes.
				\param world pointer to the MogreNewt::World
				\param col_array std::vector of pointers to existing collision objects.
			*/
			CompoundCollision( MogreNewt::World^ world, array<MogreNewt::Collision^>^ col_array,int id );
		};


		////////////////////////////////////////////////////////
		// supplemental primitives built from convex hulls
		////////////////////////////////////////////////////////

		//! Pyramid primitive
		/*!
			4-sided base, comes to a single point. base is aligned on XZ plane. made from Convex Hull internally.  supplied as part of the
			OgreNewt library, but not a built-in function of Newton istelf.
		*/
		public ref class Pyramid : public MogreNewt::ConvexCollision
		{
		public:
			//! constructor
			/*!
				\param world pointer to the MogreNewt::World
				\param size Ogre::Vector3 size.
				\param orient orientation offset of the primitive
				\param pos position offset of the primitive
			*/
			Pyramid( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(size, orient, pos, id);
			}
			Pyramid( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Quaternion orient,int id) : ConvexCollision( world )
			{
				_ctor(size, orient, Mogre::Vector3::ZERO, id);
			}
			Pyramid( MogreNewt::World^ world, Mogre::Vector3 size, Mogre::Vector3 pos,int id) : ConvexCollision( world )
			{
				_ctor(size, Mogre::Quaternion::IDENTITY, pos, id);
			}
			Pyramid( MogreNewt::World^ world, Mogre::Vector3 size,int id) : ConvexCollision( world )
			{
				_ctor(size, Mogre::Quaternion::IDENTITY, Mogre::Vector3::ZERO, id);
			}

		private:
			void _ctor( Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id );
		};



	}	// end namespace CollisionPrimitives

}// end namespace MogreNewt

#endif	// _INCLUDE_OGRENEWT_COLLISIONPRIMITIVES


