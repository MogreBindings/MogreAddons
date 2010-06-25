/* 
OgreNewt Library

Ogre implementation of Newton Game Dynamics SDK

OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_COLLISIONSERIALIZER
#define _INCLUDE_OGRENEWT_COLLISIONSERIALIZER

#include <Newton.h>
#include "OgreNewt_World.h"
#include "OgreSerializer.h"
#include "OgreString.h"

// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{
   ref class Collision;

  /*!
  This class can be used to (de)serialize a TreeCollision. Pre-building a TreeCollision and serializing from a tool,
  then deserializing it at runtime may be more efficient than building the TreeCollision on the fly, especially for complex objects.
  */
  public ref class CollisionSerializer
  {
  public:

    //! constructor
    CollisionSerializer();

    /*!
    Serialize a Collision object to a Stream.
    */
	void ExportCollision(Collision^ collision, IO::Stream^ stream);

    /*!
    Deserialize a Collision object from a Stream.
    */
	void ImportCollision(IO::Stream^ stream, Collision^ pDest);

	/*!
    Deserialize a Collision object from a byte array.
    */
	void ImportCollision(array<unsigned char>^ Buffer,Collision^ pDest);

  private:

	  IO::Stream^ m_stream;
	  array<unsigned char>^ m_buffer;
	  int m_buffer_pos;

	[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
	delegate void NewtonSerializeDelegate( void* handle, void* buffer, size_t size );

	NewtonSerializeDelegate^			m_SerializeDelegate;
	NewtonSerializeDelegate^			m_DeserializeDelegate;
	NewtonSerializeDelegate^			m_DeserializeByteArrayDelegate;

	NewtonSerialize		 m_funcptr_newtonSerialize;
	NewtonDeserialize	 m_funcptr_newtonDeserialize;
	NewtonDeserialize	 m_funcptr_newtonDeserializeByteArray;

    /*!
    Callback function for Newton. It should never be called directly, but will be called by Newton to save the TreeCollision to a stream.
    (Newton calls this function several times for each serialization, once for each chunk of its file format apparently)
    */
    void _newtonSerializeCallback(void* serializeHandle, void* buffer, size_t size);

    /*!
    Callback function for Newton. It should never be called directly, but will be called by Newton to load the TreeCollision from a stream.
    (Newton calls this function several times for each deserialization, once for each chunk of its file format apparently)
    */
    void _newtonDeserializeCallback(void* deserializeHandle, void* buffer, size_t size);

	void _newtonDeserializeByteArrayCallback(void* deserializeHandle,void* buffer, size_t size);


  };


}	// end namespace MogreNewt

#endif
// _INCLUDE_OGRENEWT_TREECOLLISIONSERIALIZER


