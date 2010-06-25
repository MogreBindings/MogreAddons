/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISKeyboard.h"

namespace MOIS
{
	//################################################################
	//KeyEvent
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::KeyCode KeyEvent::key::get()
	{
		return (MOIS::KeyCode)static_cast<OIS::KeyEvent*>(_native)->key;
	}
	
	unsigned int KeyEvent::text::get()
	{
		return static_cast<OIS::KeyEvent*>(_native)->text;
	}
	void KeyEvent::text::set( unsigned int value )
	{
		static_cast<OIS::KeyEvent*>(_native)->text = value;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//KeyListener
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	bool KeyListener_Director::keyPressed( const OIS::KeyEvent& arg )
	{
		if (doCallForKeyPressed)
		{
			bool mp_return = _receiver->KeyPressed( arg );
			return mp_return;
		}
		else
			return true;
	}
	
	bool KeyListener_Director::keyReleased( const OIS::KeyEvent& arg )
	{
		if (doCallForKeyReleased)
		{
			bool mp_return = _receiver->KeyReleased( arg );
			return mp_return;
		}
		else
			return true;
	}
	
	
	//Protected Declarations
	
	
	//################################################################
	//Keyboard
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	MOIS::Keyboard::TextTranslationMode Keyboard::TextTranslation::get()
	{
		return (MOIS::Keyboard::TextTranslationMode)static_cast<const OIS::Keyboard*>(_native)->getTextTranslation( );
	}
	void Keyboard::TextTranslation::set( MOIS::Keyboard::TextTranslationMode mode )
	{
		static_cast<OIS::Keyboard*>(_native)->setTextTranslation( (OIS::Keyboard::TextTranslationMode)mode );
	}
	
	bool Keyboard::IsKeyDown( MOIS::KeyCode key )
	{
		return static_cast<const OIS::Keyboard*>(_native)->isKeyDown( (OIS::KeyCode)key );
	}
	
	String^ Keyboard::GetAsString( MOIS::KeyCode kc )
	{
		return TO_CLR_STRING( static_cast<OIS::Keyboard*>(_native)->getAsString( (OIS::KeyCode)kc ) );
	}
	
	bool Keyboard::IsModifierDown( MOIS::Keyboard::Modifier mod )
	{
		return static_cast<const OIS::Keyboard*>(_native)->isModifierDown( (OIS::Keyboard::Modifier)mod );
	}
	
	
	          void Keyboard::CopyKeyStates( array<System::Byte>^ keys )
	          {
	            if (keys->Length < 256)
	              throw gcnew System::ArgumentException("The length for the keys buffer must be equal or larger than 256.", "keys");
	             
	             pin_ptr<System::Byte> p_keys = &keys[0];
	             static_cast<OIS::Keyboard*>(_native)->copyKeyStates((char*)p_keys);
	          }
	        
	
	
	
	//Protected Declarations
	
	

}
