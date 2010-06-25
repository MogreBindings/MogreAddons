/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISInputManager.h"
#include "MOISPrereqs.h"

namespace MOIS
{
	//################################################################
	//InputManager
	//################################################################
	
	public ref class InputManager
	{
		//Nested Types
	
		public: enum class AddOnFactories
		{
			AddOn_All = OIS::InputManager::AddOn_All,
			AddOn_LIRC = OIS::InputManager::AddOn_LIRC,
			AddOn_WiiMote = OIS::InputManager::AddOn_WiiMote
		};
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		InputManager( OIS::InputManager* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~InputManager()
		{
			this->!InputManager();
		}
		!InputManager()
		{
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
		OIS::InputManager* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
	
	
		property String^ VersionName
		{
		public:
			String^ get();
		}
	
		property unsigned int VersionNumber
		{
		public:
			static unsigned int get();
		}
	
		String^ InputSystemName( );
	
		int GetNumberOfDevices( MOIS::Type iType );
	
		MOIS::DeviceList^ ListFreeDevices( );
	
		MOIS::OISObject^ CreateInputObject( MOIS::Type iType, bool bufferMode, String^ vendor );
		MOIS::OISObject^ CreateInputObject( MOIS::Type iType, bool bufferMode );
	
		void DestroyInputObject( MOIS::OISObject^ obj );
	
		void EnableAddOnFactory( MOIS::InputManager::AddOnFactories factory );
	
		static MOIS::InputManager^ CreateInputSystem( std::size_t winHandle );
	
		static MOIS::InputManager^ CreateInputSystem( MOIS::ParamList^ paramList );
	
		static void DestroyInputSystem( MOIS::InputManager^ manager );
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( InputManager )
	
		//Protected Declarations
	protected public:
	
	};
	

}
