/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISInterface.h"
#include "MOISForceFeedback.h"

namespace MOIS
{
	//################################################################
	//Interface
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	Interface^ Interface::_ToCorrectSubclass(OIS::Interface* t)
	{
		OIS::Interface* subptr;
		subptr = dynamic_cast<OIS::ForceFeedback*>(t);
		if (subptr)
			return gcnew ForceFeedback(subptr);
	
		return gcnew Interface(t);
	}
	
	//Protected Declarations
	
	

}
