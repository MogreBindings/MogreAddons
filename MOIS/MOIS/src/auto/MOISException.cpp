/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISException.h"

namespace MOIS
{
	//################################################################
	//OISException
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::OIS_ERROR OISException::eType::get()
	{
		return (MOIS::OIS_ERROR)static_cast<OIS::Exception*>(_native)->eType;
	}
	
	int OISException::eLine::get()
	{
		return static_cast<OIS::Exception*>(_native)->eLine;
	}
	
	String^ OISException::eFile::get()
	{
		return TO_CLR_STRING( static_cast<OIS::Exception*>(_native)->eFile );
	}
	
	String^ OISException::eText::get()
	{
		return TO_CLR_STRING( static_cast<OIS::Exception*>(_native)->eText );
	}
	
	MOIS::OISException^ OISException::LastException::get()
	{
		return OIS::Exception::getLastException( );
	}
	
	
	const char* OISException::What( )
	{
		return static_cast<const OIS::Exception*>(_native)->what( );
	}
	
	void OISException::ClearLastException( )
	{
		OIS::Exception::clearLastException( );
	}
	
	
	//Protected Declarations
	
	

}
