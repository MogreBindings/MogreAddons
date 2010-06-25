#include <OgreNewt_CollisionSerializer.h>
#include <OgreNewt_Collision.h>

#include "Ogre.h"

namespace MogreNewt
{
  CollisionSerializer::CollisionSerializer()
  {
	m_buffer_pos = 0;
	m_SerializeDelegate = gcnew NewtonSerializeDelegate( this, &CollisionSerializer::_newtonSerializeCallback );
	m_DeserializeDelegate = gcnew NewtonSerializeDelegate( this, &CollisionSerializer::_newtonDeserializeCallback );
	m_DeserializeByteArrayDelegate = gcnew NewtonSerializeDelegate(this,&CollisionSerializer::_newtonDeserializeByteArrayCallback);

	m_funcptr_newtonSerialize = (NewtonSerialize) Marshal::GetFunctionPointerForDelegate( m_SerializeDelegate ).ToPointer();
	m_funcptr_newtonDeserialize = (NewtonDeserialize) Marshal::GetFunctionPointerForDelegate( m_DeserializeDelegate ).ToPointer();
	m_funcptr_newtonDeserializeByteArray = (NewtonDeserialize) Marshal::GetFunctionPointerForDelegate( m_DeserializeByteArrayDelegate ).ToPointer();
  }


  void CollisionSerializer::ExportCollision(Collision^ collision, IO::Stream^ stream)
  {
	m_stream = stream;
    NewtonCollisionSerialize(collision->World->NewtonWorld,collision->m_col,m_funcptr_newtonSerialize,NULL);
	m_stream = nullptr;
  }


  void CollisionSerializer::ImportCollision(IO::Stream^ stream, Collision^ pDest)
  {
	m_stream = stream;
    NewtonCollision* col=NewtonCreateCollisionFromSerialization(pDest->World->NewtonWorld, m_funcptr_newtonDeserialize, NULL);
    pDest->m_col=col;
	m_stream = nullptr;
  }

  void CollisionSerializer::ImportCollision(array<unsigned char>^ Buffer,Collision^ pDest)
  {
	m_buffer = Buffer;
	NewtonCollision* col=NewtonCreateCollisionFromSerialization(pDest->World->NewtonWorld, m_funcptr_newtonDeserializeByteArray, NULL);
    pDest->m_col=col;
	m_buffer = nullptr;
	m_buffer_pos = 0;
  }


  void CollisionSerializer::_newtonSerializeCallback(void* serializeHandle, void* buffer, size_t size)
  {
	  array<unsigned char>^ mbuf = gcnew array<unsigned char>(size);
	  Marshal::Copy( (IntPtr)buffer, mbuf, 0, size );
	  
	  m_stream->Write( mbuf, 0, size );
  }


  void CollisionSerializer::_newtonDeserializeCallback(void* deserializeHandle, void* buffer, size_t size)
  {
	  array<unsigned char>^ mbuf = gcnew array<unsigned char>(size);
	  m_stream->Read( mbuf, 0, size );
	  Marshal::Copy( mbuf, 0, (IntPtr)buffer, size );
  }

  void CollisionSerializer::_newtonDeserializeByteArrayCallback(void* deserializeHandle, void* buffer, size_t size)
  {
	 Marshal::Copy(m_buffer,m_buffer_pos,(IntPtr)buffer,size);
	 m_buffer_pos += size;
  }

}	// end namespace MogreNewt



