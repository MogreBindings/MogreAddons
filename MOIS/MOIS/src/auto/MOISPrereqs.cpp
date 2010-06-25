/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISPrereqs.h"

namespace MOIS
{
	//################################################################
	//Axis_NativePtr
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	int Axis_NativePtr::abs::get()
	{
		return _native->abs;
	}
	void Axis_NativePtr::abs::set( int value )
	{
		_native->abs = value;
	}
	
	int Axis_NativePtr::rel::get()
	{
		return _native->rel;
	}
	void Axis_NativePtr::rel::set( int value )
	{
		_native->rel = value;
	}
	
	bool Axis_NativePtr::absOnly::get()
	{
		return _native->absOnly;
	}
	void Axis_NativePtr::absOnly::set( bool value )
	{
		_native->absOnly = value;
	}
	
	void Axis_NativePtr::Clear( )
	{
		_native->clear( );
	}
	
	
	//Protected Declarations
	
	
	#define STLDECL_MANAGEDKEY String^
	#define STLDECL_MANAGEDVALUE String^
	#define STLDECL_NATIVEKEY std::string
	#define STLDECL_NATIVEVALUE std::string
	CPP_DECLARE_STLMULTIMAP( , ParamList, STLDECL_MANAGEDKEY, STLDECL_MANAGEDVALUE, STLDECL_NATIVEKEY, STLDECL_NATIVEVALUE )
	#undef STLDECL_MANAGEDKEY
	#undef STLDECL_MANAGEDVALUE
	#undef STLDECL_NATIVEKEY
	#undef STLDECL_NATIVEVALUE
	
	#define STLDECL_MANAGEDKEY MOIS::Type
	#define STLDECL_MANAGEDVALUE String^
	#define STLDECL_NATIVEKEY OIS::Type
	#define STLDECL_NATIVEVALUE std::string
	CPP_DECLARE_STLMULTIMAP( , DeviceList, STLDECL_MANAGEDKEY, STLDECL_MANAGEDVALUE, STLDECL_NATIVEKEY, STLDECL_NATIVEVALUE )
	#undef STLDECL_MANAGEDKEY
	#undef STLDECL_MANAGEDVALUE
	#undef STLDECL_NATIVEKEY
	#undef STLDECL_NATIVEVALUE
	

}
