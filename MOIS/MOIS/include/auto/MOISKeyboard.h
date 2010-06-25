/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISKeyboard.h"
#include "MOISEvents.h"
#include "MOISObject.h"

namespace MOIS
{
	public enum class KeyCode
	{
		KC_UNASSIGNED = OIS::KC_UNASSIGNED,
		KC_ESCAPE = OIS::KC_ESCAPE,
		KC_1 = OIS::KC_1,
		KC_2 = OIS::KC_2,
		KC_3 = OIS::KC_3,
		KC_4 = OIS::KC_4,
		KC_5 = OIS::KC_5,
		KC_6 = OIS::KC_6,
		KC_7 = OIS::KC_7,
		KC_8 = OIS::KC_8,
		KC_9 = OIS::KC_9,
		KC_0 = OIS::KC_0,
		KC_MINUS = OIS::KC_MINUS,
		KC_EQUALS = OIS::KC_EQUALS,
		KC_BACK = OIS::KC_BACK,
		KC_TAB = OIS::KC_TAB,
		KC_Q = OIS::KC_Q,
		KC_W = OIS::KC_W,
		KC_E = OIS::KC_E,
		KC_R = OIS::KC_R,
		KC_T = OIS::KC_T,
		KC_Y = OIS::KC_Y,
		KC_U = OIS::KC_U,
		KC_I = OIS::KC_I,
		KC_O = OIS::KC_O,
		KC_P = OIS::KC_P,
		KC_LBRACKET = OIS::KC_LBRACKET,
		KC_RBRACKET = OIS::KC_RBRACKET,
		KC_RETURN = OIS::KC_RETURN,
		KC_LCONTROL = OIS::KC_LCONTROL,
		KC_A = OIS::KC_A,
		KC_S = OIS::KC_S,
		KC_D = OIS::KC_D,
		KC_F = OIS::KC_F,
		KC_G = OIS::KC_G,
		KC_H = OIS::KC_H,
		KC_J = OIS::KC_J,
		KC_K = OIS::KC_K,
		KC_L = OIS::KC_L,
		KC_SEMICOLON = OIS::KC_SEMICOLON,
		KC_APOSTROPHE = OIS::KC_APOSTROPHE,
		KC_GRAVE = OIS::KC_GRAVE,
		KC_LSHIFT = OIS::KC_LSHIFT,
		KC_BACKSLASH = OIS::KC_BACKSLASH,
		KC_Z = OIS::KC_Z,
		KC_X = OIS::KC_X,
		KC_C = OIS::KC_C,
		KC_V = OIS::KC_V,
		KC_B = OIS::KC_B,
		KC_N = OIS::KC_N,
		KC_M = OIS::KC_M,
		KC_COMMA = OIS::KC_COMMA,
		KC_PERIOD = OIS::KC_PERIOD,
		KC_SLASH = OIS::KC_SLASH,
		KC_RSHIFT = OIS::KC_RSHIFT,
		KC_MULTIPLY = OIS::KC_MULTIPLY,
		KC_LMENU = OIS::KC_LMENU,
		KC_SPACE = OIS::KC_SPACE,
		KC_CAPITAL = OIS::KC_CAPITAL,
		KC_F1 = OIS::KC_F1,
		KC_F2 = OIS::KC_F2,
		KC_F3 = OIS::KC_F3,
		KC_F4 = OIS::KC_F4,
		KC_F5 = OIS::KC_F5,
		KC_F6 = OIS::KC_F6,
		KC_F7 = OIS::KC_F7,
		KC_F8 = OIS::KC_F8,
		KC_F9 = OIS::KC_F9,
		KC_F10 = OIS::KC_F10,
		KC_NUMLOCK = OIS::KC_NUMLOCK,
		KC_SCROLL = OIS::KC_SCROLL,
		KC_NUMPAD7 = OIS::KC_NUMPAD7,
		KC_NUMPAD8 = OIS::KC_NUMPAD8,
		KC_NUMPAD9 = OIS::KC_NUMPAD9,
		KC_SUBTRACT = OIS::KC_SUBTRACT,
		KC_NUMPAD4 = OIS::KC_NUMPAD4,
		KC_NUMPAD5 = OIS::KC_NUMPAD5,
		KC_NUMPAD6 = OIS::KC_NUMPAD6,
		KC_ADD = OIS::KC_ADD,
		KC_NUMPAD1 = OIS::KC_NUMPAD1,
		KC_NUMPAD2 = OIS::KC_NUMPAD2,
		KC_NUMPAD3 = OIS::KC_NUMPAD3,
		KC_NUMPAD0 = OIS::KC_NUMPAD0,
		KC_DECIMAL = OIS::KC_DECIMAL,
		KC_OEM_102 = OIS::KC_OEM_102,
		KC_F11 = OIS::KC_F11,
		KC_F12 = OIS::KC_F12,
		KC_F13 = OIS::KC_F13,
		KC_F14 = OIS::KC_F14,
		KC_F15 = OIS::KC_F15,
		KC_KANA = OIS::KC_KANA,
		KC_ABNT_C1 = OIS::KC_ABNT_C1,
		KC_CONVERT = OIS::KC_CONVERT,
		KC_NOCONVERT = OIS::KC_NOCONVERT,
		KC_YEN = OIS::KC_YEN,
		KC_ABNT_C2 = OIS::KC_ABNT_C2,
		KC_NUMPADEQUALS = OIS::KC_NUMPADEQUALS,
		KC_PREVTRACK = OIS::KC_PREVTRACK,
		KC_AT = OIS::KC_AT,
		KC_COLON = OIS::KC_COLON,
		KC_UNDERLINE = OIS::KC_UNDERLINE,
		KC_KANJI = OIS::KC_KANJI,
		KC_STOP = OIS::KC_STOP,
		KC_AX = OIS::KC_AX,
		KC_UNLABELED = OIS::KC_UNLABELED,
		KC_NEXTTRACK = OIS::KC_NEXTTRACK,
		KC_NUMPADENTER = OIS::KC_NUMPADENTER,
		KC_RCONTROL = OIS::KC_RCONTROL,
		KC_MUTE = OIS::KC_MUTE,
		KC_CALCULATOR = OIS::KC_CALCULATOR,
		KC_PLAYPAUSE = OIS::KC_PLAYPAUSE,
		KC_MEDIASTOP = OIS::KC_MEDIASTOP,
		KC_VOLUMEDOWN = OIS::KC_VOLUMEDOWN,
		KC_VOLUMEUP = OIS::KC_VOLUMEUP,
		KC_WEBHOME = OIS::KC_WEBHOME,
		KC_NUMPADCOMMA = OIS::KC_NUMPADCOMMA,
		KC_DIVIDE = OIS::KC_DIVIDE,
		KC_SYSRQ = OIS::KC_SYSRQ,
		KC_RMENU = OIS::KC_RMENU,
		KC_PAUSE = OIS::KC_PAUSE,
		KC_HOME = OIS::KC_HOME,
		KC_UP = OIS::KC_UP,
		KC_PGUP = OIS::KC_PGUP,
		KC_LEFT = OIS::KC_LEFT,
		KC_RIGHT = OIS::KC_RIGHT,
		KC_END = OIS::KC_END,
		KC_DOWN = OIS::KC_DOWN,
		KC_PGDOWN = OIS::KC_PGDOWN,
		KC_INSERT = OIS::KC_INSERT,
		KC_DELETE = OIS::KC_DELETE,
		KC_LWIN = OIS::KC_LWIN,
		KC_RWIN = OIS::KC_RWIN,
		KC_APPS = OIS::KC_APPS,
		KC_POWER = OIS::KC_POWER,
		KC_SLEEP = OIS::KC_SLEEP,
		KC_WAKE = OIS::KC_WAKE,
		KC_WEBSEARCH = OIS::KC_WEBSEARCH,
		KC_WEBFAVORITES = OIS::KC_WEBFAVORITES,
		KC_WEBREFRESH = OIS::KC_WEBREFRESH,
		KC_WEBSTOP = OIS::KC_WEBSTOP,
		KC_WEBFORWARD = OIS::KC_WEBFORWARD,
		KC_WEBBACK = OIS::KC_WEBBACK,
		KC_MYCOMPUTER = OIS::KC_MYCOMPUTER,
		KC_MAIL = OIS::KC_MAIL,
		KC_MEDIASELECT = OIS::KC_MEDIASELECT
	};
	
	//################################################################
	//KeyEvent
	//################################################################
	
	public ref class KeyEvent : public EventArg
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		KeyEvent( OIS::EventArg* obj ) : EventArg(obj)
		{
		}
	
	
		//Public Declarations
	public:
	
	
		property MOIS::KeyCode key
		{
		public:
			MOIS::KeyCode get();
		}
	
		property unsigned int text
		{
		public:
			unsigned int get();
		public:
			void set(unsigned int value);
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( KeyEvent )
	
		//Protected Declarations
	protected public:
	
	};
	
	interface class IKeyListener_Receiver
	{
		bool KeyPressed( MOIS::KeyEvent^ arg );
	
		bool KeyReleased( MOIS::KeyEvent^ arg );
	
	};
	
	public ref class KeyListener abstract sealed
	{
	public:
		delegate static bool KeyPressedHandler( MOIS::KeyEvent^ arg );
		delegate static bool KeyReleasedHandler( MOIS::KeyEvent^ arg );
	};
	
	//################################################################
	//KeyListener
	//################################################################
	
	class KeyListener_Director : public OIS::KeyListener
	{
		//Nested Types
	
		//Private Declarations
	private:
		gcroot<IKeyListener_Receiver^> _receiver;
	
		//Internal Declarations
	
		//Public Declarations
	public:
		KeyListener_Director( IKeyListener_Receiver^ recv )
			: _receiver(recv), doCallForKeyPressed(false), doCallForKeyReleased(false)
		{
		}
	
		bool doCallForKeyPressed;
		bool doCallForKeyReleased;
	
		virtual bool keyPressed( const OIS::KeyEvent& arg ) override;
	
		virtual bool keyReleased( const OIS::KeyEvent& arg ) override;
	
	
		//Protected Declarations
	
	};
	
	//################################################################
	//Keyboard
	//################################################################
	
	public ref class Keyboard : public OISObject, public IKeyListener_Receiver
	{
		//Nested Types
	
		public: enum class TextTranslationMode
		{
			Off = OIS::Keyboard::Off,
			Unicode = OIS::Keyboard::Unicode,
			Ascii = OIS::Keyboard::Ascii
		};
	
		public: enum class Modifier
		{
			Shift = OIS::Keyboard::Shift,
			Ctrl = OIS::Keyboard::Ctrl,
			Alt = OIS::Keyboard::Alt
		};
	
		//Private Declarations
	private protected:
		
		//Event and Listener fields
		KeyListener_Director* _keyListener;
		MOIS::KeyListener::KeyPressedHandler^ _keyPressed;
		MOIS::KeyListener::KeyReleasedHandler^ _keyReleased;
	
	
		//Internal Declarations
	internal:
		Keyboard( OIS::Object* obj ) : OISObject(obj)
		{
		}
	
		~Keyboard()
		{
			this->!Keyboard();
		}
		!Keyboard()
		{
			if (_keyListener != 0)
			{
				delete _keyListener; _keyListener = 0;
			}
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
	
		//Public Declarations
	public:
	
	
		event MOIS::KeyListener::KeyPressedHandler^ KeyPressed
		{
			void add(MOIS::KeyListener::KeyPressedHandler^ hnd)
			{
				if (_keyPressed == CLR_NULL)
				{
					if (_keyListener == 0)
					{
						_keyListener = new KeyListener_Director(this);
						static_cast<OIS::Keyboard*>(_native)->setEventCallback(_keyListener);
					}
					_keyListener->doCallForKeyPressed = true;
				}
				_keyPressed += hnd;
			}
			void remove(MOIS::KeyListener::KeyPressedHandler^ hnd)
			{
				_keyPressed -= hnd;
				if (_keyPressed == CLR_NULL) _keyListener->doCallForKeyPressed = false;
			}
		private:
			bool raise( MOIS::KeyEvent^ arg )
			{
				if (_keyPressed)
					return _keyPressed->Invoke( arg );
			}
		}
	
		event MOIS::KeyListener::KeyReleasedHandler^ KeyReleased
		{
			void add(MOIS::KeyListener::KeyReleasedHandler^ hnd)
			{
				if (_keyReleased == CLR_NULL)
				{
					if (_keyListener == 0)
					{
						_keyListener = new KeyListener_Director(this);
						static_cast<OIS::Keyboard*>(_native)->setEventCallback(_keyListener);
					}
					_keyListener->doCallForKeyReleased = true;
				}
				_keyReleased += hnd;
			}
			void remove(MOIS::KeyListener::KeyReleasedHandler^ hnd)
			{
				_keyReleased -= hnd;
				if (_keyReleased == CLR_NULL) _keyListener->doCallForKeyReleased = false;
			}
		private:
			bool raise( MOIS::KeyEvent^ arg )
			{
				if (_keyReleased)
					return _keyReleased->Invoke( arg );
			}
		}
	
	
		property MOIS::Keyboard::TextTranslationMode TextTranslation
		{
		public:
			MOIS::Keyboard::TextTranslationMode get();
		public:
			void set(MOIS::Keyboard::TextTranslationMode mode);
		}
	
		bool IsKeyDown( MOIS::KeyCode key );
	
		String^ GetAsString( MOIS::KeyCode kc );
	
		bool IsModifierDown( MOIS::Keyboard::Modifier mod );
	
		
		          void CopyKeyStates( array<System::Byte>^ keys );
		        
	
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( Keyboard )
	
		//Protected Declarations
	protected public:
		virtual bool OnKeyPressed( MOIS::KeyEvent^ arg ) = IKeyListener_Receiver::KeyPressed
		{
			return KeyPressed( arg );
		}
	
		virtual bool OnKeyReleased( MOIS::KeyEvent^ arg ) = IKeyListener_Receiver::KeyReleased
		{
			return KeyReleased( arg );
		}
	
	
	
	};
	

}
