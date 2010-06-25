/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISEvents.h"

namespace MOIS
{
	//################################################################
	//EventArg
	//################################################################
	
	public ref class EventArg
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		EventArg( OIS::EventArg* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~EventArg()
		{
			this->!EventArg();
		}
		!EventArg()
		{
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
		OIS::EventArg* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
	
	
		property MOIS::OISObject^ device
		{
		public:
			MOIS::OISObject^ get();
		}
	
		inline static operator EventArg^ (const OIS::EventArg* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::EventArg*>(t)) : nullptr;
		}
		inline static operator EventArg^ (const OIS::EventArg& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::EventArg&>(t));
		}
		inline static operator OIS::EventArg* (EventArg^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::EventArg*>(t->_native);
		}
		inline static operator OIS::EventArg& (EventArg^ t) {
			return *static_cast<OIS::EventArg*>(t->_native);
		}
	
		internal: static EventArg^ _ToCorrectSubclass(OIS::EventArg* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	

}
