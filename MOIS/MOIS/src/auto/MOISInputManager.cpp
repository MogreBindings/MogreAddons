/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISInputManager.h"
#include "MOISPrereqs.h"
#include "MOISObject.h"

namespace MOIS
{
	//################################################################
	//InputManager
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	String^ InputManager::VersionName::get()
	{
		return TO_CLR_STRING( static_cast<OIS::InputManager*>(_native)->getVersionName( ) );
	}
	
	unsigned int InputManager::VersionNumber::get()
	{
		return OIS::InputManager::getVersionNumber( );
	}
	
	String^ InputManager::InputSystemName( )
	{
		return TO_CLR_STRING( static_cast<OIS::InputManager*>(_native)->inputSystemName( ) );
	}
	
	int InputManager::GetNumberOfDevices( MOIS::Type iType )
	{
		return static_cast<OIS::InputManager*>(_native)->getNumberOfDevices( (OIS::Type)iType );
	}
	
	MOIS::DeviceList^ InputManager::ListFreeDevices( )
	{
		return MOIS::DeviceList::ByValue( static_cast<OIS::InputManager*>(_native)->listFreeDevices( ) );
	}
	
	MOIS::OISObject^ InputManager::CreateInputObject( MOIS::Type iType, bool bufferMode, String^ vendor )
	{
		DECLARE_NATIVE_STRING( o_vendor, vendor )
	
		return static_cast<OIS::InputManager*>(_native)->createInputObject( (OIS::Type)iType, bufferMode, o_vendor );
	}
	MOIS::OISObject^ InputManager::CreateInputObject( MOIS::Type iType, bool bufferMode )
	{
		return static_cast<OIS::InputManager*>(_native)->createInputObject( (OIS::Type)iType, bufferMode );
	}
	
	void InputManager::DestroyInputObject( MOIS::OISObject^ obj )
	{
		static_cast<OIS::InputManager*>(_native)->destroyInputObject( obj );
	}
	
	void InputManager::EnableAddOnFactory( MOIS::InputManager::AddOnFactories factory )
	{
		static_cast<OIS::InputManager*>(_native)->enableAddOnFactory( (OIS::InputManager::AddOnFactories)factory );
	}
	
	MOIS::InputManager^ InputManager::CreateInputSystem( std::size_t winHandle )
	{
		return OIS::InputManager::createInputSystem( winHandle );
	}
	
	MOIS::InputManager^ InputManager::CreateInputSystem( MOIS::ParamList^ paramList )
	{
		return OIS::InputManager::createInputSystem( paramList );
	}
	
	void InputManager::DestroyInputSystem( MOIS::InputManager^ manager )
	{
		OIS::InputManager::destroyInputSystem( manager );
	}
	
	
	//Protected Declarations
	
	

}
