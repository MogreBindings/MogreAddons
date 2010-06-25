/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISEvents.h"
#include "MOISObject.h"
#include "MOISJoyStick.h"
#include "MOISKeyboard.h"
#include "MOISMouse.h"

namespace MOIS
{
	//################################################################
	//EventArg
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::OISObject^ EventArg::device::get()
	{
		return static_cast<OIS::EventArg*>(_native)->device;
	}
	
	EventArg^ EventArg::_ToCorrectSubclass(OIS::EventArg* t)
	{
		OIS::EventArg* subptr;
		subptr = dynamic_cast<OIS::JoyStickEvent*>(t);
		if (subptr)
			return gcnew JoyStickEvent(subptr);
	
		subptr = dynamic_cast<OIS::KeyEvent*>(t);
		if (subptr)
			return gcnew KeyEvent(subptr);
	
		subptr = dynamic_cast<OIS::MouseEvent*>(t);
		if (subptr)
			return gcnew MouseEvent(subptr);
	
		return gcnew EventArg(t);
	}
	
	//Protected Declarations
	
	

}
