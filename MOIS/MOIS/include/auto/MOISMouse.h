/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISMouse.h"
#include "MOISPrereqs.h"
#include "MOISEvents.h"
#include "MOISObject.h"

namespace MOIS
{
	public enum class MouseButtonID
	{
		MB_Left = OIS::MB_Left,
		MB_Right = OIS::MB_Right,
		MB_Middle = OIS::MB_Middle,
		MB_Button3 = OIS::MB_Button3,
		MB_Button4 = OIS::MB_Button4,
		MB_Button5 = OIS::MB_Button5,
		MB_Button6 = OIS::MB_Button6,
		MB_Button7 = OIS::MB_Button7
	};
	
	//################################################################
	//MouseState_NativePtr
	//################################################################
	
	public value class MouseState_NativePtr
	{
		//Nested Types
	
		//Private Declarations
	private protected:
		OIS::MouseState* _native;
	
		//Internal Declarations
	internal:
	
		//Public Declarations
	public:
	
	
		property int width
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		property int height
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		property MOIS::Axis_NativePtr X
		{
		public:
			MOIS::Axis_NativePtr get();
		public:
			void set(MOIS::Axis_NativePtr value);
		}
	
		property MOIS::Axis_NativePtr Y
		{
		public:
			MOIS::Axis_NativePtr get();
		public:
			void set(MOIS::Axis_NativePtr value);
		}
	
		property MOIS::Axis_NativePtr Z
		{
		public:
			MOIS::Axis_NativePtr get();
		public:
			void set(MOIS::Axis_NativePtr value);
		}
	
		property int buttons
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		bool ButtonDown( MOIS::MouseButtonID button );
	
		void Clear( );
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_NATIVEPTRVALUECLASS( MouseState_NativePtr, OIS::MouseState )
	
	
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
	//MouseEvent
	//################################################################
	
	public ref class MouseEvent : public EventArg
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		MouseEvent( OIS::EventArg* obj ) : EventArg(obj)
		{
		}
	
	
		//Public Declarations
	public:
	
	
		property MOIS::MouseState_NativePtr state
		{
		public:
			MOIS::MouseState_NativePtr get();
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( MouseEvent )
	
		//Protected Declarations
	protected public:
	
	};
	
	interface class IMouseListener_Receiver
	{
		bool MouseMoved( MOIS::MouseEvent^ arg );
	
		bool MousePressed( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id );
	
		bool MouseReleased( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id );
	
	};
	
	public ref class MouseListener abstract sealed
	{
	public:
		delegate static bool MouseMovedHandler( MOIS::MouseEvent^ arg );
		delegate static bool MousePressedHandler( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id );
		delegate static bool MouseReleasedHandler( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id );
	};
	
	//################################################################
	//MouseListener
	//################################################################
	
	class MouseListener_Director : public OIS::MouseListener
	{
		//Nested Types
	
		//Private Declarations
	private:
		gcroot<IMouseListener_Receiver^> _receiver;
	
		//Internal Declarations
	
		//Public Declarations
	public:
		MouseListener_Director( IMouseListener_Receiver^ recv )
			: _receiver(recv), doCallForMouseMoved(false), doCallForMousePressed(false), doCallForMouseReleased(false)
		{
		}
	
		bool doCallForMouseMoved;
		bool doCallForMousePressed;
		bool doCallForMouseReleased;
	
		virtual bool mouseMoved( const OIS::MouseEvent& arg ) override;
	
		virtual bool mousePressed( const OIS::MouseEvent& arg, OIS::MouseButtonID id ) override;
	
		virtual bool mouseReleased( const OIS::MouseEvent& arg, OIS::MouseButtonID id ) override;
	
	
		//Protected Declarations
	
	};
	
	//################################################################
	//Mouse
	//################################################################
	
	public ref class Mouse : public OISObject, public IMouseListener_Receiver
	{
		//Nested Types
	
		//Private Declarations
	private protected:
		
		//Event and Listener fields
		MouseListener_Director* _mouseListener;
		MOIS::MouseListener::MouseMovedHandler^ _mouseMoved;
		MOIS::MouseListener::MousePressedHandler^ _mousePressed;
		MOIS::MouseListener::MouseReleasedHandler^ _mouseReleased;
	
	
		//Internal Declarations
	internal:
		Mouse( OIS::Object* obj ) : OISObject(obj)
		{
		}
	
		~Mouse()
		{
			this->!Mouse();
		}
		!Mouse()
		{
			if (_mouseListener != 0)
			{
				delete _mouseListener; _mouseListener = 0;
			}
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
	
		//Public Declarations
	public:
	
	
		event MOIS::MouseListener::MouseMovedHandler^ MouseMoved
		{
			void add(MOIS::MouseListener::MouseMovedHandler^ hnd)
			{
				if (_mouseMoved == CLR_NULL)
				{
					if (_mouseListener == 0)
					{
						_mouseListener = new MouseListener_Director(this);
						static_cast<OIS::Mouse*>(_native)->setEventCallback(_mouseListener);
					}
					_mouseListener->doCallForMouseMoved = true;
				}
				_mouseMoved += hnd;
			}
			void remove(MOIS::MouseListener::MouseMovedHandler^ hnd)
			{
				_mouseMoved -= hnd;
				if (_mouseMoved == CLR_NULL) _mouseListener->doCallForMouseMoved = false;
			}
		private:
			bool raise( MOIS::MouseEvent^ arg )
			{
				if (_mouseMoved)
					return _mouseMoved->Invoke( arg );
			}
		}
	
		event MOIS::MouseListener::MousePressedHandler^ MousePressed
		{
			void add(MOIS::MouseListener::MousePressedHandler^ hnd)
			{
				if (_mousePressed == CLR_NULL)
				{
					if (_mouseListener == 0)
					{
						_mouseListener = new MouseListener_Director(this);
						static_cast<OIS::Mouse*>(_native)->setEventCallback(_mouseListener);
					}
					_mouseListener->doCallForMousePressed = true;
				}
				_mousePressed += hnd;
			}
			void remove(MOIS::MouseListener::MousePressedHandler^ hnd)
			{
				_mousePressed -= hnd;
				if (_mousePressed == CLR_NULL) _mouseListener->doCallForMousePressed = false;
			}
		private:
			bool raise( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id )
			{
				if (_mousePressed)
					return _mousePressed->Invoke( arg, id );
			}
		}
	
		event MOIS::MouseListener::MouseReleasedHandler^ MouseReleased
		{
			void add(MOIS::MouseListener::MouseReleasedHandler^ hnd)
			{
				if (_mouseReleased == CLR_NULL)
				{
					if (_mouseListener == 0)
					{
						_mouseListener = new MouseListener_Director(this);
						static_cast<OIS::Mouse*>(_native)->setEventCallback(_mouseListener);
					}
					_mouseListener->doCallForMouseReleased = true;
				}
				_mouseReleased += hnd;
			}
			void remove(MOIS::MouseListener::MouseReleasedHandler^ hnd)
			{
				_mouseReleased -= hnd;
				if (_mouseReleased == CLR_NULL) _mouseListener->doCallForMouseReleased = false;
			}
		private:
			bool raise( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id )
			{
				if (_mouseReleased)
					return _mouseReleased->Invoke( arg, id );
			}
		}
	
	
		property MOIS::MouseState_NativePtr MouseState
		{
		public:
			MOIS::MouseState_NativePtr get();
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( Mouse )
	
		//Protected Declarations
	protected public:
		virtual bool OnMouseMoved( MOIS::MouseEvent^ arg ) = IMouseListener_Receiver::MouseMoved
		{
			return MouseMoved( arg );
		}
	
		virtual bool OnMousePressed( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id ) = IMouseListener_Receiver::MousePressed
		{
			return MousePressed( arg, id );
		}
	
		virtual bool OnMouseReleased( MOIS::MouseEvent^ arg, MOIS::MouseButtonID id ) = IMouseListener_Receiver::MouseReleased
		{
			return MouseReleased( arg, id );
		}
	
	
	
	};
	

}
