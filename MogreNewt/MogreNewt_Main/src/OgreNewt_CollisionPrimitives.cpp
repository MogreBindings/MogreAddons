#include <OgreNewt_CollisionPrimitives.h>
#include <OgreNewt_Tools.h>

#include "Ogre.h"

namespace MogreNewt
{

	namespace CollisionPrimitives
	{

		// MogreNewt::CollisionPrimitives::Null
		Null::Null(MogreNewt::World^ world) : Collision( world )
		{
			m_col = NewtonCreateNull( m_world->NewtonWorld );
		}


		// MogreNewt::CollisionPrimitives::Box
		void Box::_ctor( Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos, int id )
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			m_col = NewtonCreateBox( m_world->NewtonWorld, (float)size.x, (float)size.y, (float)size.z, id ,&matrix[0] );
		}



		// MogreNewt::CollisionPrimitives::Ellipsoid
		void Ellipsoid::_ctor( Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos,int id )
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			m_col = NewtonCreateSphere( m_world->NewtonWorld, (float)size.x, (float)size.y, (float)size.z, id, &matrix[0] );
		}


		// MogreNewt::CollisionPrimitives::Cylinder
		void Cylinder::_ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id )
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			m_col = NewtonCreateCylinder( m_world->NewtonWorld, (float)radius, (float)height, id,&matrix[0] );
		}


		// MogreNewt::CollisionPrimitives::Capsule
		void Capsule::_ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos, int id )
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			m_col = NewtonCreateCapsule( m_world->NewtonWorld, (float)radius, (float)height, id, &matrix[0] );
		}


		// MogreNewt::CollisionPrimitives::Cone
		void Cone::_ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos,int id )
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			m_col = NewtonCreateCone( m_world->NewtonWorld, (float)radius, (float)height, id,&matrix[0] );
		}

		// MogreNewt::CollisionPrimitives::ChamferCylinder
		void ChamferCylinder::_ctor( Mogre::Real radius, Mogre::Real height, Mogre::Quaternion orient, Mogre::Vector3 pos ,int id)
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			m_col = NewtonCreateChamferCylinder( m_world->NewtonWorld, (float)radius, (float)height, id,&matrix[0] );
		}


		
		// MogreNewt::CollisionPrimitives::ConvexHull
		void ConvexHull::_ctor( Mogre::SceneNode^ mnode, Mogre::Quaternion orient, Mogre::Vector3 pos ,dFloat tolerance,int id)
		{
			Ogre::SceneNode* node = mnode;

			Ogre::Vector3 scale(1.0,1.0,1.0);
			

			//get the mesh!
			Ogre::Entity* obj = (Ogre::Entity*)node->getAttachedObject(0);
			Ogre::MeshPtr mesh = obj->getMesh();

			//get scale
			scale = node->getScale();

			//find number of submeshes
			unsigned short sub = mesh->getNumSubMeshes();

			size_t total_verts = 0;

			Ogre::VertexData* v_data;
			bool addedShared = false;

			for (unsigned short i=0;i<sub;i++)
			{
				Ogre::SubMesh* sub_mesh = mesh->getSubMesh(i);
				if (sub_mesh->useSharedVertices)
				{
					if (!addedShared)
					{
						v_data = mesh->sharedVertexData;
						total_verts += v_data->vertexCount;

						addedShared = true;
					}
				}
				else
				{
					v_data = sub_mesh->vertexData;
					total_verts += v_data->vertexCount;
				}
			}
		
			addedShared = false;

			//make array to hold vertex positions!
			Ogre::Vector3* vertices = new Ogre::Vector3[total_verts];
			unsigned int offset = 0;

			//loop back through, adding vertices as we go!
			for (unsigned short i=0;i<sub;i++)
			{
				Ogre::SubMesh* sub_mesh = mesh->getSubMesh(i);
				Ogre::VertexDeclaration* v_decl;
				const Ogre::VertexElement* p_elem;
				float* v_Posptr;
				size_t v_count;
		
				v_data = NULL;

				if (sub_mesh->useSharedVertices)
				{
					if (!addedShared)
					{
						v_data = mesh->sharedVertexData;
						v_count = v_data->vertexCount;
						v_decl = v_data->vertexDeclaration;
						p_elem = v_decl->findElementBySemantic( Ogre::VES_POSITION );
						addedShared = true;
					}
				}
				else
				{
					v_data = sub_mesh->vertexData;
					v_count = v_data->vertexCount;
					v_decl = v_data->vertexDeclaration;
					p_elem = v_decl->findElementBySemantic( Ogre::VES_POSITION );
				}

				if (v_data)
				{
					size_t start = v_data->vertexStart;
					//pointer
					Ogre::HardwareVertexBufferSharedPtr v_sptr = v_data->vertexBufferBinding->getBuffer( p_elem->getSource() );
					unsigned char* v_ptr = static_cast<unsigned char*>(v_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
					unsigned char* v_offset;

					//loop through vertex data...
					for (size_t j=start; j<(start+v_count); j++)
					{
						//get offset to Position data!
						v_offset = v_ptr + (j * v_sptr->getVertexSize());
						p_elem->baseVertexPointerToElement( v_offset, &v_Posptr );

						//now get vertex positions...
						vertices[offset].x = *v_Posptr; v_Posptr++;
						vertices[offset].y = *v_Posptr; v_Posptr++;
						vertices[offset].z = *v_Posptr; v_Posptr++;

						vertices[offset] *= scale;

						offset++;
					}

					//unlock buffer
					v_sptr->unlock();
				}
			}

			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );
	
			//okay, let's try making the ConvexHull!
			m_col = NewtonCreateConvexHull( m_world->NewtonWorld, (int)total_verts, (float*)&vertices[0].x, sizeof(Ogre::Vector3),tolerance, id,&matrix[0] );

			delete []vertices;

		}


		// MogreNewt::CollisionPrimitives::ConvexHull
		void ConvexHull::_ctor( array<Mogre::Vector3>^ verts, Mogre::Quaternion orient, Mogre::Vector3 pos ,dFloat tolerance,int id)
		{
			float matrix[16];
			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			pin_ptr<Mogre::Real> ptr = &verts[0].x;

			//make the collision primitive.
			m_col = NewtonCreateConvexHull( m_world->NewtonWorld, verts->Length, ptr, sizeof(Ogre::Vector3),tolerance, id,&matrix[0]);
		}




		TreeCollision::TreeCollision( MogreNewt::World^ world) : Collision(world)
		{
		}

		TreeCollision::TreeCollision( MogreNewt::World^ world, Mogre::SceneNode^ node, bool optimize ,int id) : Collision( world )
		{
			TreeCollision(world, node, optimize, id,FaceWinding::FW_DEFAULT);
		}
		
		TreeCollision::TreeCollision( MogreNewt::World^ world, Mogre::SceneNode^ node, bool optimize, int id,FaceWinding fw ) : Collision( world )
		{
			Mogre::Vector3 scale;

			Start(id);
			//now get the mesh!
			Mogre::MovableObject^ obj = node->GetAttachedObject(0);
			//obj->getMovableType();
			
			Mogre::MeshPtr^ mesh = ((Mogre::Entity^)obj)->GetMesh();

			//get scale
			scale = node->GetScale();

			//find number of sub-meshes
			unsigned short sub = mesh->NumSubMeshes;

			for (unsigned short cs=0;cs<sub;cs++)
			{
				Mogre::SubMesh^ sub_mesh = mesh->GetSubMesh(cs);

				//vertex data!
				Mogre::VertexData^ v_data;

				if (sub_mesh->useSharedVertices)
				{
					v_data = mesh->sharedVertexData;
				}
				else
				{
					v_data = sub_mesh->vertexData;
				}
		
				//let's find more information about the Vertices...
				Mogre::VertexDeclaration^ v_decl = v_data->vertexDeclaration;
				Mogre::VertexElement^ p_elem = v_decl->FindElementBySemantic( Mogre::VertexElementSemantic::VES_POSITION );
		
				// get pointer!
				Mogre::HardwareVertexBufferSharedPtr^ v_sptr = v_data->vertexBufferBinding->GetBuffer( p_elem->Source );
				unsigned char* v_ptr = static_cast<unsigned char*>(v_sptr->Lock( Mogre::HardwareBuffer::LockOptions::HBL_READ_ONLY ));
		
				//now find more about the index!!
				Mogre::IndexData^ i_data = sub_mesh->indexData;
				size_t index_count = i_data->indexCount;
				size_t poly_count = index_count / 3;
		
				// get pointer!
				Mogre::HardwareIndexBufferSharedPtr^ i_sptr = i_data->indexBuffer;
		
				// 16 or 32 bit indices?
				bool uses32bit = ( i_sptr->Type == Mogre::HardwareIndexBuffer::IndexType::IT_32BIT );
				unsigned long* i_Longptr;
				unsigned short* i_Shortptr;
		
		
				if ( uses32bit)
				{
					i_Longptr = static_cast<unsigned long*>(i_sptr->Lock( Mogre::HardwareBuffer::LockOptions::HBL_READ_ONLY ));
					
				}
				else
				{
					i_Shortptr = static_cast<unsigned short*>(i_sptr->Lock( Mogre::HardwareBuffer::LockOptions::HBL_READ_ONLY ));
				}


				//now loop through the indices, getting polygon info!
				int i_offset = 0;

				for (size_t i=0; i<poly_count; i++)
				{
					//Mogre::Vector3 poly_verts[3];
					array<Mogre::Vector3>^ poly_verts = gcnew array<Mogre::Vector3>(3);

					unsigned char* v_offset;
					float* v_Posptr;
					int idx;

					if (uses32bit)
					{
						for (int j=0;j<3;j++)
						{
							idx = i_Longptr[i_offset+j];		// index to first vertex!
							v_offset = v_ptr + (idx * v_sptr->VertexSize);
							p_elem->BaseVertexPointerToElement( v_offset, &v_Posptr );
							//now get vertex position from v_Posptr!
							poly_verts[j].x = *v_Posptr; v_Posptr++;
							poly_verts[j].y = *v_Posptr; v_Posptr++;
							poly_verts[j].z = *v_Posptr; v_Posptr++;

							poly_verts[j] *= scale;
						}
					}
					else
					{
						for (int j=0;j<3;j++)
						{
							idx = i_Shortptr[i_offset+j];		// index to first vertex!
							v_offset = v_ptr + (idx * v_sptr->VertexSize);
							p_elem->BaseVertexPointerToElement( v_offset, &v_Posptr );
							//now get vertex position from v_Posptr!

							// switch poly winding.
							poly_verts[j].x = *v_Posptr; v_Posptr++;
							poly_verts[j].y = *v_Posptr; v_Posptr++;
							poly_verts[j].z = *v_Posptr; v_Posptr++;
							
							poly_verts[j] *= scale;
						}
					}
					
					if (fw == FaceWinding::FW_DEFAULT)
					{
						AddPoly( poly_verts, cs );	
					}
					else
					{
						//Mogre::Vector3 rev_poly_verts[3];
						array<Mogre::Vector3>^ rev_poly_verts = gcnew array<Mogre::Vector3>(3);
						rev_poly_verts[0] = poly_verts[0];
						rev_poly_verts[0] = poly_verts[2];
						rev_poly_verts[0] = poly_verts[1];

						AddPoly( rev_poly_verts, cs );
					}

					i_offset += 3;
				}

				//unlock the buffers!
				v_sptr->Unlock();
				i_sptr->Unlock();
	
			}
			//done!
			Finish( optimize );
		}



		void TreeCollision::MakeTreeCollisionFromEntity( Mogre::Entity^ entity, Mogre::Vector3 scale, bool optimize,int id, FaceWinding fw )
		{
			Ogre::Vector3 o_scale = (Ogre::Vector3) scale;
			Ogre::MeshPtr mesh = ((Ogre::Entity*)entity)->getMesh();

			Start(id);

			//find number of sub-meshes
			unsigned short sub = mesh->getNumSubMeshes();

			for (unsigned short cs=0;cs<sub;cs++)
			{
				Ogre::SubMesh* sub_mesh = mesh->getSubMesh(cs);

				//vertex data!
				Ogre::VertexData* v_data;

				if (sub_mesh->useSharedVertices)
				{
					v_data = mesh->sharedVertexData;
				}
				else
				{
					v_data = sub_mesh->vertexData;
				}
		
				//let's find more information about the Vertices...
				Ogre::VertexDeclaration* v_decl = v_data->vertexDeclaration;
				const Ogre::VertexElement* p_elem = v_decl->findElementBySemantic( Ogre::VES_POSITION );
		
				// get pointer!
				Ogre::HardwareVertexBufferSharedPtr v_sptr = v_data->vertexBufferBinding->getBuffer( p_elem->getSource() );
				unsigned char* v_ptr = static_cast<unsigned char*>(v_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
		
				//now find more about the index!!
				Ogre::IndexData* i_data = sub_mesh->indexData;
				size_t index_count = i_data->indexCount;
				size_t poly_count = index_count / 3;
		
				// get pointer!
				Ogre::HardwareIndexBufferSharedPtr i_sptr = i_data->indexBuffer;
		
				// 16 or 32 bit indices?
				bool uses32bit = ( i_sptr->getType() == Ogre::HardwareIndexBuffer::IT_32BIT );
				unsigned long* i_Longptr;
				unsigned short* i_Shortptr;
		
		
				if ( uses32bit)
				{
					i_Longptr = static_cast<unsigned long*>(i_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
					
				}
				else
				{
					i_Shortptr = static_cast<unsigned short*>(i_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
				}


				//now loop through the indices, getting polygon info!
				int i_offset = 0;

				array<Mogre::Vector3>^ poly_verts = gcnew array<Mogre::Vector3>(3);
				array<Mogre::Vector3>^ rev_poly_verts = gcnew array<Mogre::Vector3>(3);

				for (size_t i=0; i<poly_count; i++)
				{
					unsigned char* v_offset;
					float* v_Posptr;
					int idx;

					if (uses32bit)
					{
						for (int j=0;j<3;j++)
						{
							idx = i_Longptr[i_offset+j];		// index to first vertex!
							v_offset = v_ptr + (idx * v_sptr->getVertexSize());
							p_elem->baseVertexPointerToElement( v_offset, &v_Posptr );
							//now get vertex position from v_Posptr!
							poly_verts[j].x = *v_Posptr; v_Posptr++;
							poly_verts[j].y = *v_Posptr; v_Posptr++;
							poly_verts[j].z = *v_Posptr; v_Posptr++;

							poly_verts[j] *= o_scale;
						}
					}
					else
					{
						for (int j=0;j<3;j++)
						{
							idx = i_Shortptr[i_offset+j];		// index to first vertex!
							v_offset = v_ptr + (idx * v_sptr->getVertexSize());
							p_elem->baseVertexPointerToElement( v_offset, &v_Posptr );
							//now get vertex position from v_Posptr!

							// switch poly winding.
							poly_verts[j].x = *v_Posptr; v_Posptr++;
							poly_verts[j].y = *v_Posptr; v_Posptr++;
							poly_verts[j].z = *v_Posptr; v_Posptr++;
							
							poly_verts[j] *= o_scale;
						}
					}
					
					//AddPoly( poly_verts, cs );
					if (fw == FaceWinding::FW_DEFAULT)
					{
						AddPoly( poly_verts, cs );	
					}
					else
					{
						//Ogre::Vector3 rev_poly_verts[3];
						rev_poly_verts[0] = poly_verts[0];
						rev_poly_verts[0] = poly_verts[2];
						rev_poly_verts[0] = poly_verts[1];

						AddPoly( rev_poly_verts, cs );
					}

					i_offset += 3;
				}

				//unlock the buffers!
				v_sptr->unlock();
				i_sptr->unlock();
	
			}
			//done!
			Finish( optimize );
		}


		//TreeCollision::TreeCollision( MogreNewt::World^ world, Mogre::SceneNode^ node, bool optimize ) : Collision( world )
		//{
		//	MakeTreeCollisionFromEntity( (Mogre::Entity^)node->GetAttachedObject(0), node->GetScale(), optimize, FaceWinding::FW_DEFAULT );
		//}

		//TreeCollision::TreeCollision( MogreNewt::World^ world, Mogre::SceneNode^ node, bool optimize, FaceWinding fw ) : Collision( world )
		//{
		//	MakeTreeCollisionFromEntity( (Mogre::Entity^)node->GetAttachedObject(0), node->GetScale(), optimize, fw );
		//}

		TreeCollision::TreeCollision( MogreNewt::World^ world, Mogre::Entity^ entity, bool optimize,int id ) : Collision( world )
		{
			MakeTreeCollisionFromEntity( entity, Mogre::Vector3::UNIT_SCALE, optimize, id,FaceWinding::FW_DEFAULT );
		}


		TreeCollision::TreeCollision(MogreNewt::World^ world, array<float>^ mvertices, array<int>^ mindices, bool optimize,int id) : MogreNewt::Collision( world )
		{
			TreeCollision(world, mvertices, mindices, optimize, id,FaceWinding::FW_DEFAULT);
		}

		TreeCollision::TreeCollision(MogreNewt::World^ world, array<float>^ mvertices, array<int>^ mindices, bool optimize,int id,FaceWinding fw) : MogreNewt::Collision( world )
		{
			int numVertices = mvertices->Length;
			int numIndices = mindices->Length;
			pin_ptr<float> vertices = &mvertices[0];
			pin_ptr<int> indices = &mindices[0];

			Start(id);
 
			int numPolys = numIndices / 3;
 
			array<Mogre::Vector3>^ vecVertices = gcnew array<Mogre::Vector3>(numVertices);
 
			for (int curVertex = 0; curVertex < numVertices; curVertex++)
			{
				vecVertices[curVertex].x = vertices[0 + curVertex * 3];
				vecVertices[curVertex].y = vertices[1 + curVertex * 3];
				vecVertices[curVertex].z = vertices[2 + curVertex * 3];
			}
 
			array<Mogre::Vector3>^ poly_verts = gcnew array<Mogre::Vector3>(3);

			for ( int poly = 0; poly < numPolys; poly++ )
			{
				if (fw == FaceWinding::FW_DEFAULT)
				{
					poly_verts[0] = vecVertices[indices[0 + poly * 3]];
					poly_verts[1] = vecVertices[indices[1 + poly * 3]];
					poly_verts[2] = vecVertices[indices[2 + poly * 3]];
				}
				else
				{
					poly_verts[0] = vecVertices[indices[0 + poly * 3]];
					poly_verts[2] = vecVertices[indices[1 + poly * 3]];
					poly_verts[1] = vecVertices[indices[2 + poly * 3]];
				}

				AddPoly( poly_verts, 0 );
			}

			Finish( optimize );
		}

		//TreeCollision::TreeCollision(MogreNewt::World^ world, array<Mogre::Vector3>^ mvertices, array<int>^ mindices, bool optimize) : MogreNewt::Collision( world )
		//{
		//	int numVertices = mvertices->Length;
		//	int numIndices = mindices->Length;
		//	pin_ptr<Mogre::Vector3> vertices = &mvertices[0];
		//	pin_ptr<int> indices = &mindices[0];

		//	Start();
 
		//	unsigned int numPolys = mindices->Length / 3;
 
		//	array<Mogre::Vector3>^ poly_verts = gcnew array<Mogre::Vector3>(3);

		//	unsigned int* curIndex=(unsigned int*)indices;
		//	for ( unsigned int poly = 0; poly < numPolys; poly++ )
		//	{
		//		poly_verts[0] = vertices[*curIndex]; curIndex++;
		//		poly_verts[1] = vertices[*curIndex]; curIndex++;
		//		poly_verts[2] = vertices[*curIndex]; curIndex++;

		//		AddPoly( poly_verts, 0 );
		//	}

		//	Finish( optimize );
		//}
		
		TreeCollision::TreeCollision( MogreNewt::World^ world, array<Mogre::Vector3>^ mvertices, Mogre::IndexData^ mindexData, bool optimize,int id) : Collision( world )
		{
			TreeCollision( world, mvertices, mindexData, optimize,id, FaceWinding::FW_DEFAULT);
		}


		TreeCollision::TreeCollision( MogreNewt::World^ world, array<Mogre::Vector3>^ mvertices, Mogre::IndexData^ mindexData, bool optimize,int id, FaceWinding fw) : Collision( world )
		{
			int numVertices = mvertices->Length;
			pin_ptr<Ogre::Vector3> vertices = interior_ptr<Ogre::Vector3>( &mvertices[0] );
			Ogre::IndexData* indexData = mindexData;

			Start(id);

			unsigned int numPolys = indexData->indexCount / 3;
			Ogre::HardwareIndexBufferSharedPtr hwIndexBuffer=indexData->indexBuffer;
			size_t indexSize=hwIndexBuffer->getIndexSize();
			void* indices=hwIndexBuffer->lock(Ogre::HardwareBuffer::HBL_READ_ONLY);

			assert((indexSize==2) || (indexSize==4));

			array<Mogre::Vector3>^ poly_verts = gcnew array<Mogre::Vector3>(3);

			if (indexSize==2)
			{
				unsigned short* curIndex=(unsigned short*)indices;
				for ( unsigned int poly = 0; poly < numPolys; poly++ )
				{
					if (fw == FaceWinding::FW_DEFAULT)
					{
						poly_verts[0] = vertices[*curIndex]; curIndex++;
						poly_verts[1] = vertices[*curIndex]; curIndex++;
						poly_verts[2] = vertices[*curIndex]; curIndex++;
					}
					else
					{
						poly_verts[0] = vertices[*curIndex]; curIndex++;
						poly_verts[2] = vertices[*curIndex]; curIndex++;
						poly_verts[1] = vertices[*curIndex]; curIndex++;
					}

					AddPoly( poly_verts, 0 );
				}
			}
			else
			{
				unsigned int* curIndex=(unsigned int*)indices;
				for ( unsigned int poly = 0; poly < numPolys; poly++ )
				{
					if (fw == FaceWinding::FW_DEFAULT)
					{
						poly_verts[0] = vertices[*curIndex]; curIndex++;
						poly_verts[1] = vertices[*curIndex]; curIndex++;
						poly_verts[2] = vertices[*curIndex]; curIndex++;
					}
					else
					{
						poly_verts[0] = vertices[*curIndex]; curIndex++;
						poly_verts[2] = vertices[*curIndex]; curIndex++;
						poly_verts[1] = vertices[*curIndex]; curIndex++;
					}

					AddPoly( poly_verts, 0 );
				}
			}
      
			hwIndexBuffer->unlock();
			Finish( optimize );
		} 


		void TreeCollision::Start(int id)
		{
			m_col = NewtonCreateTreeCollision( m_world->NewtonWorld,id);
			NewtonTreeCollisionBeginBuild( m_col );
		}

		void TreeCollision::AddPoly( array<Mogre::Vector3>^ polys, unsigned int ID )
		{
			pin_ptr<Mogre::Vector3> p_polys = &polys[0];
			NewtonTreeCollisionAddFace( m_col, polys->Length, (float*)&p_polys[0].x, sizeof(Mogre::Vector3), ID );
		}

		void TreeCollision::Finish( bool optimize)
		{
			NewtonTreeCollisionEndBuild( m_col, optimize );
		}


		TreeCollisionSceneParser::TreeCollisionSceneParser( MogreNewt::World^ world ) : TreeCollision( world )
		{
		}
		
		void TreeCollisionSceneParser::ParseScene( Mogre::SceneNode^ startNode,bool optimize,int id, FaceWinding fw)
		{
			count = 0;

			Start(id);

			// parse the individual nodes.
			Ogre::Quaternion rootOrient = Ogre::Quaternion::IDENTITY;
			Ogre::Vector3 rootPos = Ogre::Vector3::ZERO;
			Ogre::Vector3 rootScale = Ogre::Vector3::UNIT_SCALE;

			_ParseNode( startNode, rootOrient, rootPos, rootScale, fw, true );

			Finish( optimize );
		}

		void TreeCollisionSceneParser::_ParseNode(Ogre::SceneNode *node, const Ogre::Quaternion &curOrient, const Ogre::Vector3 &curPos, const Ogre::Vector3 &curScale, FaceWinding fw, bool startNode)
		{
			// parse this scene node.
			// do children first.
			Ogre::Quaternion thisOrient;// = curOrient * node->getOrientation();
			Ogre::Vector3 thisPos;// = curPos + (curOrient * (node->getPosition() * curScale));
			Ogre::Vector3 thisScale = node->getScale()*curScale;
			if(!startNode)
			{
				//regular behaviour
				thisOrient = curOrient * node->getOrientation();
				thisPos = curPos + (curOrient * node->getPosition() * curScale );            
			}
			else
			{
				//do not use the node's position and orientation,
				//because it's all relative to it
				thisOrient = curOrient;
				thisPos = curPos;
			}

			Ogre::SceneNode::ChildNodeIterator child_it = node->getChildIterator();
			
			while (child_it.hasMoreElements())
			{
				_ParseNode( (Ogre::SceneNode*)child_it.getNext(), thisOrient, thisPos, thisScale, fw, false );
			}


			// now add the polys from this node.
			//now get the mesh!
			unsigned int num_obj = node->numAttachedObjects();
			for (unsigned int co=0; co<num_obj; co++)
			{
				Ogre::MovableObject* obj = node->getAttachedObject(co);
				if (obj->getMovableType() != "Entity")
					continue;
			
				Ogre::Entity* ent = (Ogre::Entity*)obj;

				if (!EntityFilter(node, ent, fw))
					continue;

				Ogre::MeshPtr mesh = ent->getMesh();

				//find number of sub-meshes
				unsigned short sub = mesh->getNumSubMeshes();

				for (unsigned short cs=0;cs<sub;cs++)
				{
					Ogre::SubMesh* sub_mesh = mesh->getSubMesh(cs);

					//vertex data!
					Ogre::VertexData* v_data;

					if (sub_mesh->useSharedVertices)
					{	
						v_data = mesh->sharedVertexData;
					}
					else
					{
						v_data = sub_mesh->vertexData;
					}
		
					//let's find more information about the Vertices...
					Ogre::VertexDeclaration* v_decl = v_data->vertexDeclaration;
					const Ogre::VertexElement* p_elem = v_decl->findElementBySemantic( Ogre::VES_POSITION );
		
					// get pointer!
					Ogre::HardwareVertexBufferSharedPtr v_sptr = v_data->vertexBufferBinding->getBuffer( p_elem->getSource() );
					unsigned char* v_ptr = static_cast<unsigned char*>(v_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
		
					//now find more about the index!!
					Ogre::IndexData* i_data = sub_mesh->indexData;
					size_t index_count = i_data->indexCount;
					size_t poly_count = index_count / 3;
		
					// get pointer!
					Ogre::HardwareIndexBufferSharedPtr i_sptr = i_data->indexBuffer;
		
					// 16 or 32 bit indices?
					bool uses32bit = ( i_sptr->getType() == Ogre::HardwareIndexBuffer::IT_32BIT );
					unsigned long* i_Longptr;
					unsigned short* i_Shortptr;
		
					if ( uses32bit)
					{
						i_Longptr = static_cast<unsigned long*>(i_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
					}
					else
					{
						i_Shortptr = static_cast<unsigned short*>(i_sptr->lock( Ogre::HardwareBuffer::HBL_READ_ONLY ));
					}

					//now loop through the indices, getting polygon info!
					int i_offset = 0;

					array<Mogre::Vector3>^ poly_verts = gcnew array<Mogre::Vector3>(3);
					array<Mogre::Vector3>^ rev_poly_verts = gcnew array<Mogre::Vector3>(3);
					//Ogre::Vector3 rev_poly_verts[3];

					for (size_t i=0; i<poly_count; i++)
					{
						unsigned char* v_offset;
						float* v_Posptr;
						int idx;

						if (uses32bit)
						{
							for (int j=0;j<3;j++)
							{
								idx = i_Longptr[i_offset+j];		// index to first vertex!
								v_offset = v_ptr + (idx * v_sptr->getVertexSize());
								p_elem->baseVertexPointerToElement( v_offset, &v_Posptr );
								//now get vertex position from v_Posptr!
								poly_verts[j].x = *v_Posptr; v_Posptr++;
								poly_verts[j].y = *v_Posptr; v_Posptr++;
								poly_verts[j].z = *v_Posptr; v_Posptr++;
	
								poly_verts[j] = thisPos + (thisOrient * (poly_verts[j] * thisScale));
							}
						}
						else
						{
							for (int j=0;j<3;j++)
							{
								idx = i_Shortptr[i_offset+j];		// index to first vertex!
								v_offset = v_ptr + (idx * v_sptr->getVertexSize());
								p_elem->baseVertexPointerToElement( v_offset, &v_Posptr );
								//now get vertex position from v_Posptr!

								// switch poly winding.
								poly_verts[j].x = *v_Posptr; v_Posptr++;
								poly_verts[j].y = *v_Posptr; v_Posptr++;
								poly_verts[j].z = *v_Posptr; v_Posptr++;
							
								poly_verts[j] = thisPos + (thisOrient * (poly_verts[j] * thisScale));
							}
						}
					
						if (fw == FaceWinding::FW_DEFAULT)
						{
							AddPoly( poly_verts, cs );	
						}
						else
						{
							//Ogre::Vector3 rev_poly_verts[3];
							rev_poly_verts[0] = poly_verts[0];
							rev_poly_verts[0] = poly_verts[2];
							rev_poly_verts[0] = poly_verts[1];

							AddPoly( rev_poly_verts, cs );
						}
						
						i_offset += 3;
					}

					//unlock the buffers!
					v_sptr->unlock();
					i_sptr->unlock();

				}
			}

		}





		// MogreNewt::CollisionPrimitives::CompoundCollision
		CompoundCollision::CompoundCollision( MogreNewt::World^ world, array<MogreNewt::Collision^>^ col_array,int id ) : Collision( world )
		{
			//get the number of elements.
			unsigned int num = col_array->Length;

			// create simple array.
			::NewtonCollision** narray = new ::NewtonCollision*[num];

			for (unsigned int i=0;i<num;i++)
			{
				narray[i] = (::NewtonCollision*)col_array[i]->NewtonCollision;
			}

			m_col = NewtonCreateCompoundCollision( world->NewtonWorld, num, narray, id );

			delete[] narray;

		}

		
		// MogreNewt::CollisionPrimitives::Pyramid
		void Pyramid::_ctor( Mogre::Vector3 size, Mogre::Quaternion orient, Mogre::Vector3 pos, int id )
		{
			float matrix[16];

			MogreNewt::Converter::QuatPosToMatrix( orient, pos, &matrix[0] );

			// create a simple pyramid collision primitive using the Newton Convex Hull interface.
			// this function places the center of mass 1/3 up y from the base.

			float* vertices = new float[15];
			unsigned short idx = 0;

			// make the bottom base.
			for (int ix=-1; ix<=1; ix+=2)
			{
				for (int iz=-1; iz<=1; iz+=2)
				{
					vertices [idx++] = (size.x/2.0) * ix;
					vertices [idx++] = -(size.y/3.0);
					vertices [idx++] = (size.z/2.0) * iz;
				}
			}

			// make the tip.
			vertices [idx++] = 0.0f;
			vertices [idx++] = (size.y*2.0/3.0);
			vertices [idx++] = 0.0f;

			//make the collision primitive.
			m_col = NewtonCreateConvexHull( m_world->NewtonWorld, 5, vertices, sizeof(float)*3,0.001f, id,&matrix[0]);

			delete []vertices;
		}



	}	// end namespace CollisionPrimitives

}	// end namespace MogreNewt





