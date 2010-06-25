/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISObject.h"
#include "MOISPrereqs.h"
#include "MOISInterface.h"

namespace MOIS
{
	//################################################################
	//OISObject
	//################################################################
	
	public ref class OISObject
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		OISObject( OIS::Object* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~OISObject()
		{
			this->!OISObject();
		}
		!OISObject()
		{
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
		OIS::Object* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
	
	
		property MOIS::InputManager^ Creator
		{
		public:
			MOIS::InputManager^ get();
		}
	
		property int ID
		{
		public:
			int get();
		}
	
		MOIS::Type Type( );
	
		String^ Vendor( );
	
		bool Buffered( );
	
		void SetBuffered( bool buffered );
	
		void Capture( );
	
		MOIS::Interface^ QueryInterface( MOIS::Interface::IType type );
	
		void _initialize( );
	
		inline static operator OISObject^ (const OIS::Object* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::Object*>(t)) : nullptr;
		}
		inline static operator OISObject^ (const OIS::Object& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::Object&>(t));
		}
		inline static operator OIS::Object* (OISObject^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::Object*>(t->_native);
		}
		inline static operator OIS::Object& (OISObject^ t) {
			return *static_cast<OIS::Object*>(t->_native);
		}
	
		internal: static OISObject^ _ToCorrectSubclass(OIS::Object* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	

}
