/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISInterface.h"

namespace MOIS
{
	//################################################################
	//Interface
	//################################################################
	
	public ref class Interface
	{
		//Nested Types
	
		public: enum class IType
		{
			ForceFeedback = OIS::Interface::ForceFeedback,
			Reserved = OIS::Interface::Reserved
		};
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		Interface( OIS::Interface* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~Interface()
		{
			this->!Interface();
		}
		!Interface()
		{
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
		OIS::Interface* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
	
	
		inline static operator Interface^ (const OIS::Interface* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::Interface*>(t)) : nullptr;
		}
		inline static operator Interface^ (const OIS::Interface& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::Interface&>(t));
		}
		inline static operator OIS::Interface* (Interface^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::Interface*>(t->_native);
		}
		inline static operator OIS::Interface& (Interface^ t) {
			return *static_cast<OIS::Interface*>(t->_native);
		}
	
		internal: static Interface^ _ToCorrectSubclass(OIS::Interface* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	

}
