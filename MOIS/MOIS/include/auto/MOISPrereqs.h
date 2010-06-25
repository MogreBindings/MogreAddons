/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISPrereqs.h"

namespace MOIS
{
	public enum class ComponentType
	{
		OIS_Unknown = OIS::OIS_Unknown,
		OIS_Button = OIS::OIS_Button,
		OIS_Axis = OIS::OIS_Axis,
		OIS_Slider = OIS::OIS_Slider,
		OIS_POV = OIS::OIS_POV,
		OIS_Vector3 = OIS::OIS_Vector3
	};
	
	public enum class Type
	{
		OISUnknown = OIS::OISUnknown,
		OISKeyboard = OIS::OISKeyboard,
		OISMouse = OIS::OISMouse,
		OISJoyStick = OIS::OISJoyStick,
		OISTablet = OIS::OISTablet
	};
	
	//################################################################
	//Axis_NativePtr
	//################################################################
	
	public value class Axis_NativePtr
	{
		//Nested Types
	
		//Private Declarations
	private protected:
		OIS::Axis* _native;
	
		//Internal Declarations
	internal:
	
		//Public Declarations
	public:
	
	
		property int abs
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		property int rel
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		property bool absOnly
		{
		public:
			bool get();
		public:
			void set(bool value);
		}
	
		void Clear( );
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_NATIVEPTRVALUECLASS( Axis_NativePtr, OIS::Axis )
	
	
		property IntPtr NativePtr
		{
			IntPtr get() { return (IntPtr)_native; }
		}
	
		property bool IsNull
		{
			bool get() { return (_native == 0); }
		}
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//Vector3
	//################################################################
	
	public value class Vector3
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
	
		//Public Declarations
	public:
	
	
		float x;
	
		float y;
	
		float z;
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_VALUECLASS( Vector3 )
	
		//Protected Declarations
	protected public:
	
	};
	
	#define STLDECL_MANAGEDKEY String^
	#define STLDECL_MANAGEDVALUE String^
	#define STLDECL_NATIVEKEY std::string
	#define STLDECL_NATIVEVALUE std::string
	INC_DECLARE_STLMULTIMAP( ParamList, STLDECL_MANAGEDKEY, STLDECL_MANAGEDVALUE, STLDECL_NATIVEKEY, STLDECL_NATIVEVALUE, public, private )
	#undef STLDECL_MANAGEDKEY
	#undef STLDECL_MANAGEDVALUE
	#undef STLDECL_NATIVEKEY
	#undef STLDECL_NATIVEVALUE
	
	#define STLDECL_MANAGEDKEY MOIS::Type
	#define STLDECL_MANAGEDVALUE String^
	#define STLDECL_NATIVEKEY OIS::Type
	#define STLDECL_NATIVEVALUE std::string
	INC_DECLARE_STLMULTIMAP( DeviceList, STLDECL_MANAGEDKEY, STLDECL_MANAGEDVALUE, STLDECL_NATIVEKEY, STLDECL_NATIVEVALUE, public, private )
	#undef STLDECL_MANAGEDKEY
	#undef STLDECL_MANAGEDVALUE
	#undef STLDECL_NATIVEKEY
	#undef STLDECL_NATIVEVALUE
	

}
