/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISMouse.h"

namespace MOIS
{
	//################################################################
	//MouseState_NativePtr
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	int MouseState_NativePtr::width::get()
	{
		return _native->width;
	}
	void MouseState_NativePtr::width::set( int value )
	{
		_native->width = value;
	}
	
	int MouseState_NativePtr::height::get()
	{
		return _native->height;
	}
	void MouseState_NativePtr::height::set( int value )
	{
		_native->height = value;
	}
	
	MOIS::Axis_NativePtr MouseState_NativePtr::X::get()
	{
		return _native->X;
	}
	void MouseState_NativePtr::X::set( MOIS::Axis_NativePtr value )
	{
		_native->X = value;
	}
	
	MOIS::Axis_NativePtr MouseState_NativePtr::Y::get()
	{
		return _native->Y;
	}
	void MouseState_NativePtr::Y::set( MOIS::Axis_NativePtr value )
	{
		_native->Y = value;
	}
	
	MOIS::Axis_NativePtr MouseState_NativePtr::Z::get()
	{
		return _native->Z;
	}
	void MouseState_NativePtr::Z::set( MOIS::Axis_NativePtr value )
	{
		_native->Z = value;
	}
	
	int MouseState_NativePtr::buttons::get()
	{
		return _native->buttons;
	}
	void MouseState_NativePtr::buttons::set( int value )
	{
		_native->buttons = value;
	}
	
	bool MouseState_NativePtr::ButtonDown( MOIS::MouseButtonID button )
	{
		return _native->buttonDown( (OIS::MouseButtonID)button );
	}
	
	void MouseState_NativePtr::Clear( )
	{
		_native->clear( );
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//MouseEvent
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::MouseState_NativePtr MouseEvent::state::get()
	{
		return static_cast<OIS::MouseEvent*>(_native)->state;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//MouseListener
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	bool MouseListener_Director::mouseMoved( const OIS::MouseEvent& arg )
	{
		if (doCallForMouseMoved)
		{
			bool mp_return = _receiver->MouseMoved( arg );
			return mp_return;
		}
		else
			return true;
	}
	
	bool MouseListener_Director::mousePressed( const OIS::MouseEvent& arg, OIS::MouseButtonID id )
	{
		if (doCallForMousePressed)
		{
			bool mp_return = _receiver->MousePressed( arg, (MOIS::MouseButtonID)id );
			return mp_return;
		}
		else
			return true;
	}
	
	bool MouseListener_Director::mouseReleased( const OIS::MouseEvent& arg, OIS::MouseButtonID id )
	{
		if (doCallForMouseReleased)
		{
			bool mp_return = _receiver->MouseReleased( arg, (MOIS::MouseButtonID)id );
			return mp_return;
		}
		else
			return true;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//Mouse
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::MouseState_NativePtr Mouse::MouseState::get()
	{
		return static_cast<const OIS::Mouse*>(_native)->getMouseState( );
	}
	
	
	//Protected Declarations
	
	

}
