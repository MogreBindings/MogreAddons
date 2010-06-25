/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISException.h"

namespace MOIS
{
	public enum class OIS_ERROR
	{
		E_InputDisconnected = OIS::E_InputDisconnected,
		E_InputDeviceNonExistant = OIS::E_InputDeviceNonExistant,
		E_InputDeviceNotSupported = OIS::E_InputDeviceNotSupported,
		E_DeviceFull = OIS::E_DeviceFull,
		E_NotSupported = OIS::E_NotSupported,
		E_NotImplemented = OIS::E_NotImplemented,
		E_Duplicate = OIS::E_Duplicate,
		E_InvalidParam = OIS::E_InvalidParam,
		E_General = OIS::E_General
	};
	
	//################################################################
	//OISException
	//################################################################
	
	public ref class OISException
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		OISException( OIS::Exception* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~OISException()
		{
			this->!OISException();
		}
		!OISException()
		{
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
		OIS::Exception* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
	
	
		property MOIS::OIS_ERROR eType
		{
		public:
			MOIS::OIS_ERROR get();
		}
	
		property int eLine
		{
		public:
			int get();
		}
	
		property String^ eFile
		{
		public:
			String^ get();
		}
	
		property String^ eText
		{
		public:
			String^ get();
		}
	
		property MOIS::OISException^ LastException
		{
		public:
			static MOIS::OISException^ get();
		}
	
		void CopyTo(OISException^ dest)
		{
			if (_native == NULL) throw gcnew Exception("The underlying native object for the caller is null.");
			if (dest->_native == NULL) throw gcnew ArgumentException("The underlying native object for parameter 'dest' is null.");
	
			*(dest->_native) = *_native;
		}
	
		const char* What( );
	
		static void ClearLastException( );
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER_EXPLICIT( OISException, Exception )
	
		//Protected Declarations
	protected public:
	
	};
	

}
