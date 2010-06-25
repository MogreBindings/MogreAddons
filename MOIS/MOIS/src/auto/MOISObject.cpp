/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISObject.h"
#include "MOISInputManager.h"
#include "MOISInterface.h"
#include "MOISJoyStick.h"
#include "MOISKeyboard.h"
#include "MOISMouse.h"

namespace MOIS
{
	//################################################################
	//OISObject
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::InputManager^ OISObject::Creator::get()
	{
		return static_cast<const OIS::Object*>(_native)->getCreator( );
	}
	
	int OISObject::ID::get()
	{
		return static_cast<const OIS::Object*>(_native)->getID( );
	}
	
	MOIS::Type OISObject::Type( )
	{
		return (MOIS::Type)static_cast<const OIS::Object*>(_native)->type( );
	}
	
	String^ OISObject::Vendor( )
	{
		return TO_CLR_STRING( static_cast<const OIS::Object*>(_native)->vendor( ) );
	}
	
	bool OISObject::Buffered( )
	{
		return static_cast<const OIS::Object*>(_native)->buffered( );
	}
	
	void OISObject::SetBuffered( bool buffered )
	{
		static_cast<OIS::Object*>(_native)->setBuffered( buffered );
	}
	
	void OISObject::Capture( )
	{
		static_cast<OIS::Object*>(_native)->capture( );
	}
	
	MOIS::Interface^ OISObject::QueryInterface( MOIS::Interface::IType type )
	{
		return static_cast<OIS::Object*>(_native)->queryInterface( (OIS::Interface::IType)type );
	}
	
	void OISObject::_initialize( )
	{
		static_cast<OIS::Object*>(_native)->_initialize( );
	}
	
	OISObject^ OISObject::_ToCorrectSubclass(OIS::Object* t)
	{
		OIS::Object* subptr;
		subptr = dynamic_cast<OIS::JoyStick*>(t);
		if (subptr)
			return gcnew JoyStick(subptr);
	
		subptr = dynamic_cast<OIS::Keyboard*>(t);
		if (subptr)
			return gcnew Keyboard(subptr);
	
		subptr = dynamic_cast<OIS::Mouse*>(t);
		if (subptr)
			return gcnew Mouse(subptr);
	
		return gcnew OISObject(t);
	}
	
	//Protected Declarations
	
	

}
