/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISJoyStick.h"

namespace MOIS
{
	//################################################################
	//JoyStickState_NativePtr
	//################################################################
	
	//Nested Types
	
	        bool JoyStickState_NativePtr::GetButton(int index)
	        {
	          return _native->mButtons[index];
	        }
	        Axis_NativePtr JoyStickState_NativePtr::GetAxis(int index)
	        {
	          return _native->mAxes[index];
	        }
	        Vector3 JoyStickState_NativePtr::GetVector(int index)
	        {
	          return _native->mVectors[index];
	        }
	      
	
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::Pov_NativePtr JoyStickState_NativePtr::mPOV::get(int index)
	{
		if (index < 0 || index >= 4) throw gcnew IndexOutOfRangeException();
		return _native->mPOV[index];
	}
	void JoyStickState_NativePtr::mPOV::set(int index, MOIS::Pov_NativePtr value )
	{
		if (index < 0 || index >= 4) throw gcnew IndexOutOfRangeException();
		_native->mPOV[index] = value;
	}
	
	MOIS::Slider_NativePtr JoyStickState_NativePtr::mSliders::get(int index)
	{
		if (index < 0 || index >= 4) throw gcnew IndexOutOfRangeException();
		return _native->mSliders[index];
	}
	void JoyStickState_NativePtr::mSliders::set(int index, MOIS::Slider_NativePtr value )
	{
		if (index < 0 || index >= 4) throw gcnew IndexOutOfRangeException();
		_native->mSliders[index] = value;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//Slider_NativePtr
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	int Slider_NativePtr::abX::get()
	{
		return _native->abX;
	}
	void Slider_NativePtr::abX::set( int value )
	{
		_native->abX = value;
	}
	
	int Slider_NativePtr::abY::get()
	{
		return _native->abY;
	}
	void Slider_NativePtr::abY::set( int value )
	{
		_native->abY = value;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//Pov_NativePtr
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	int Pov_NativePtr::Centered::get()
	{
		return OIS::Pov::Centered;
	}
	
	int Pov_NativePtr::North::get()
	{
		return OIS::Pov::North;
	}
	
	int Pov_NativePtr::South::get()
	{
		return OIS::Pov::South;
	}
	
	int Pov_NativePtr::East::get()
	{
		return OIS::Pov::East;
	}
	
	int Pov_NativePtr::West::get()
	{
		return OIS::Pov::West;
	}
	
	int Pov_NativePtr::NorthEast::get()
	{
		return OIS::Pov::NorthEast;
	}
	
	int Pov_NativePtr::SouthEast::get()
	{
		return OIS::Pov::SouthEast;
	}
	
	int Pov_NativePtr::NorthWest::get()
	{
		return OIS::Pov::NorthWest;
	}
	
	int Pov_NativePtr::SouthWest::get()
	{
		return OIS::Pov::SouthWest;
	}
	
	int Pov_NativePtr::direction::get()
	{
		return _native->direction;
	}
	void Pov_NativePtr::direction::set( int value )
	{
		_native->direction = value;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//JoyStickEvent
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::JoyStickState_NativePtr JoyStickEvent::state::get()
	{
		return static_cast<OIS::JoyStickEvent*>(_native)->state;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//JoyStickListener
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	bool JoyStickListener_Director::buttonPressed( const OIS::JoyStickEvent& arg, int button )
	{
		if (doCallForButtonPressed)
		{
			bool mp_return = _receiver->ButtonPressed( arg, button );
			return mp_return;
		}
		else
			return true;
	}
	
	bool JoyStickListener_Director::buttonReleased( const OIS::JoyStickEvent& arg, int button )
	{
		if (doCallForButtonReleased)
		{
			bool mp_return = _receiver->ButtonReleased( arg, button );
			return mp_return;
		}
		else
			return true;
	}
	
	bool JoyStickListener_Director::axisMoved( const OIS::JoyStickEvent& arg, int axis )
	{
		if (doCallForAxisMoved)
		{
			bool mp_return = _receiver->AxisMoved( arg, axis );
			return mp_return;
		}
		else
			return true;
	}
	
	bool JoyStickListener_Director::sliderMoved( const OIS::JoyStickEvent& param1, int index )
	{
		if (doCallForSliderMoved)
		{
			bool mp_return = _receiver->SliderMoved( param1, index );
			return mp_return;
		}
		else
			return true;
	}
	
	bool JoyStickListener_Director::povMoved( const OIS::JoyStickEvent& arg, int index )
	{
		if (doCallForPovMoved)
		{
			bool mp_return = _receiver->PovMoved( arg, index );
			return mp_return;
		}
		else
			return true;
	}
	
	bool JoyStickListener_Director::vector3Moved( const OIS::JoyStickEvent& arg, int index )
	{
		if (doCallForVector3Moved)
		{
			bool mp_return = _receiver->Vector3Moved( arg, index );
			return mp_return;
		}
		else
			return true;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//JoyStick
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	int JoyStick::MIN_AXIS::get()
	{
		return OIS::JoyStick::MIN_AXIS;
	}
	
	int JoyStick::MAX_AXIS::get()
	{
		return OIS::JoyStick::MAX_AXIS;
	}
	
	MOIS::JoyStickState_NativePtr JoyStick::JoyStickState::get()
	{
		return static_cast<const OIS::JoyStick*>(_native)->getJoyStickState( );
	}
	
	float JoyStick::Vector3Sensitivity::get()
	{
		return static_cast<const OIS::JoyStick*>(_native)->getVector3Sensitivity( );
	}
	void JoyStick::Vector3Sensitivity::set( float degrees )
	{
		static_cast<OIS::JoyStick*>(_native)->setVector3Sensitivity( degrees );
	}
	
	int JoyStick::GetNumberOfComponents( MOIS::ComponentType cType )
	{
		return static_cast<const OIS::JoyStick*>(_native)->getNumberOfComponents( (OIS::ComponentType)cType );
	}
	
	
	//Protected Declarations
	
	

}
