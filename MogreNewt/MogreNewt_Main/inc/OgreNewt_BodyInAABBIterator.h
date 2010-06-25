/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/

#ifndef _INCLUDE_OGRENEWT_BODYINAABBITERATOR
#define _INCLUDE_OGRENEWT_BODYINAABBITERATOR

#include <Newton.h>

#include "OgreNewt_World.h"
#include "OgreNewt_Body.h"


// OgreNewt namespace.  all functions and classes use this namespace.
namespace MogreNewt
{

	class WorldNativeInfo;

//! function to be called for all bodies
/*!
	This function will be called for every body iterated.  you can put any functionality you might want inside this function.
*/
public delegate void IteratorCallbackHandler( MogreNewt::Body^ body );


//! Iterate through all bodies in the world.
/*!
	this class is an easy way to loop through all bodies in the world, performing some kind of action.
*/

public ref class BodyInAABBIterator
{
	public:


		
		//! perform an iteration
		/*!
			will call the provided function for all bodies in the world.
			\param callback pointer to a function to be used
		*/
		void Go(Mogre::AxisAlignedBox^ aabb);

		event IteratorCallbackHandler^ IteratorCallback
		{
			void add(IteratorCallbackHandler^ hnd);
			void remove(IteratorCallbackHandler^ hnd);

			private:
				void raise(MogreNewt::Body^ body )
				{
					if (m_callback)
						m_callback->Invoke(body );
				}
		}


	internal:
		BodyInAABBIterator(World^ world);

		MogreNewt::World^	m_world;
		IteratorCallbackHandler^	m_callback;

	private:

		[UnmanagedFunctionPointer(CallingConvention::Cdecl)]
		delegate void NewtonBodyIteratorDelegate( const ::NewtonBody* body );

		NewtonBodyIteratorDelegate^	 m_NewtonBodyIteratorDelegate;

		NewtonBodyIterator m_funcptr_iterator;

		void NewtonIterator(const NewtonBody* newtonBody );
    
};




}	// end namespace MogreNewt

#endif	// _INCLUDE_OGRENEWT_BODYINAABBITERATOR


