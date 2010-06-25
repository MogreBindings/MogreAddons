/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISJoyStick.h"
#include "MOISPrereqs.h"
#include "MOISEvents.h"
#include "MOISObject.h"

namespace MOIS
{
	//################################################################
	//JoyStickState_NativePtr
	//################################################################
	
	public value class JoyStickState_NativePtr
	{
		//Nested Types
	
		
		        public:
		        bool GetButton(int index);
		        Axis_NativePtr GetAxis(int index);
		        Vector3 GetVector(int index);
		
		        property int ButtonCount { int get() { return _native->mButtons.size(); } }
		        property int AxisCount { int get() { return _native->mAxes.size(); } }
		        property int VectorCount { int get() { return _native->mVectors.size(); } }
		      
	
		//Private Declarations
	private protected:
		OIS::JoyStickState* _native;
	
		//Internal Declarations
	internal:
	
		//Public Declarations
	public:
	
	
		property MOIS::Pov_NativePtr mPOV[int]
		{
		public:
			MOIS::Pov_NativePtr get(int index);
			void set(int index, MOIS::Pov_NativePtr value);
		}
	
		property MOIS::Slider_NativePtr mSliders[int]
		{
		public:
			MOIS::Slider_NativePtr get(int index);
			void set(int index, MOIS::Slider_NativePtr value);
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_NATIVEPTRVALUECLASS( JoyStickState_NativePtr, OIS::JoyStickState )
	
	
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
	//Slider_NativePtr
	//################################################################
	
	public value class Slider_NativePtr
	{
		//Nested Types
	
		//Private Declarations
	private protected:
		OIS::Slider* _native;
	
		//Internal Declarations
	internal:
	
		//Public Declarations
	public:
	
	
		property int abX
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		property int abY
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_NATIVEPTRVALUECLASS( Slider_NativePtr, OIS::Slider )
	
	
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
	//Pov_NativePtr
	//################################################################
	
	public value class Pov_NativePtr
	{
		//Nested Types
	
		//Private Declarations
	private protected:
		OIS::Pov* _native;
	
		//Internal Declarations
	internal:
	
		//Public Declarations
	public:
	
	
		static property int Centered
		{
		public:
			int get();
		}
	
		static property int North
		{
		public:
			int get();
		}
	
		static property int South
		{
		public:
			int get();
		}
	
		static property int East
		{
		public:
			int get();
		}
	
		static property int West
		{
		public:
			int get();
		}
	
		static property int NorthEast
		{
		public:
			int get();
		}
	
		static property int SouthEast
		{
		public:
			int get();
		}
	
		static property int NorthWest
		{
		public:
			int get();
		}
	
		static property int SouthWest
		{
		public:
			int get();
		}
	
		property int direction
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_NATIVEPTRVALUECLASS( Pov_NativePtr, OIS::Pov )
	
	
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
	//JoyStickEvent
	//################################################################
	
	public ref class JoyStickEvent : public EventArg
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		JoyStickEvent( OIS::EventArg* obj ) : EventArg(obj)
		{
		}
	
	
		//Public Declarations
	public:
	
	
		property MOIS::JoyStickState_NativePtr state
		{
		public:
			MOIS::JoyStickState_NativePtr get();
		}
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( JoyStickEvent )
	
		//Protected Declarations
	protected public:
	
	};
	
	interface class IJoyStickListener_Receiver
	{
		bool ButtonPressed( MOIS::JoyStickEvent^ arg, int button );
	
		bool ButtonReleased( MOIS::JoyStickEvent^ arg, int button );
	
		bool AxisMoved( MOIS::JoyStickEvent^ arg, int axis );
	
		bool SliderMoved( MOIS::JoyStickEvent^ param1, int index );
	
		bool PovMoved( MOIS::JoyStickEvent^ arg, int index );
	
		bool Vector3Moved( MOIS::JoyStickEvent^ arg, int index );
	
	};
	
	public ref class JoyStickListener abstract sealed
	{
	public:
		delegate static bool ButtonPressedHandler( MOIS::JoyStickEvent^ arg, int button );
		delegate static bool ButtonReleasedHandler( MOIS::JoyStickEvent^ arg, int button );
		delegate static bool AxisMovedHandler( MOIS::JoyStickEvent^ arg, int axis );
		delegate static bool SliderMovedHandler( MOIS::JoyStickEvent^ param1, int index );
		delegate static bool PovMovedHandler( MOIS::JoyStickEvent^ arg, int index );
		delegate static bool Vector3MovedHandler( MOIS::JoyStickEvent^ arg, int index );
	};
	
	//################################################################
	//JoyStickListener
	//################################################################
	
	class JoyStickListener_Director : public OIS::JoyStickListener
	{
		//Nested Types
	
		//Private Declarations
	private:
		gcroot<IJoyStickListener_Receiver^> _receiver;
	
		//Internal Declarations
	
		//Public Declarations
	public:
		JoyStickListener_Director( IJoyStickListener_Receiver^ recv )
			: _receiver(recv), doCallForButtonPressed(false), doCallForButtonReleased(false), doCallForAxisMoved(false), doCallForSliderMoved(false), doCallForPovMoved(false), doCallForVector3Moved(false)
		{
		}
	
		bool doCallForButtonPressed;
		bool doCallForButtonReleased;
		bool doCallForAxisMoved;
		bool doCallForSliderMoved;
		bool doCallForPovMoved;
		bool doCallForVector3Moved;
	
		virtual bool buttonPressed( const OIS::JoyStickEvent& arg, int button ) override;
	
		virtual bool buttonReleased( const OIS::JoyStickEvent& arg, int button ) override;
	
		virtual bool axisMoved( const OIS::JoyStickEvent& arg, int axis ) override;
	
		virtual bool sliderMoved( const OIS::JoyStickEvent& param1, int index ) override;
	
		virtual bool povMoved( const OIS::JoyStickEvent& arg, int index ) override;
	
		virtual bool vector3Moved( const OIS::JoyStickEvent& arg, int index ) override;
	
	
		//Protected Declarations
	
	};
	
	//################################################################
	//JoyStick
	//################################################################
	
	public ref class JoyStick : public OISObject, public IJoyStickListener_Receiver
	{
		//Nested Types
	
		//Private Declarations
	private protected:
		
		//Event and Listener fields
		JoyStickListener_Director* _joyStickListener;
		MOIS::JoyStickListener::ButtonPressedHandler^ _buttonPressed;
		MOIS::JoyStickListener::ButtonReleasedHandler^ _buttonReleased;
		MOIS::JoyStickListener::AxisMovedHandler^ _axisMoved;
		MOIS::JoyStickListener::SliderMovedHandler^ _sliderMoved;
		MOIS::JoyStickListener::PovMovedHandler^ _povMoved;
		MOIS::JoyStickListener::Vector3MovedHandler^ _vector3Moved;
	
	
		//Internal Declarations
	internal:
		JoyStick( OIS::Object* obj ) : OISObject(obj)
		{
		}
	
		~JoyStick()
		{
			this->!JoyStick();
		}
		!JoyStick()
		{
			if (_joyStickListener != 0)
			{
				delete _joyStickListener; _joyStickListener = 0;
			}
			if (_createdByCLR &&_native)
			{
				_native = 0;
			}
		}
	
	
		//Public Declarations
	public:
	
	
		event MOIS::JoyStickListener::ButtonPressedHandler^ ButtonPressed
		{
			void add(MOIS::JoyStickListener::ButtonPressedHandler^ hnd)
			{
				if (_buttonPressed == CLR_NULL)
				{
					if (_joyStickListener == 0)
					{
						_joyStickListener = new JoyStickListener_Director(this);
						static_cast<OIS::JoyStick*>(_native)->setEventCallback(_joyStickListener);
					}
					_joyStickListener->doCallForButtonPressed = true;
				}
				_buttonPressed += hnd;
			}
			void remove(MOIS::JoyStickListener::ButtonPressedHandler^ hnd)
			{
				_buttonPressed -= hnd;
				if (_buttonPressed == CLR_NULL) _joyStickListener->doCallForButtonPressed = false;
			}
		private:
			bool raise( MOIS::JoyStickEvent^ arg, int button )
			{
				if (_buttonPressed)
					return _buttonPressed->Invoke( arg, button );
			}
		}
	
		event MOIS::JoyStickListener::ButtonReleasedHandler^ ButtonReleased
		{
			void add(MOIS::JoyStickListener::ButtonReleasedHandler^ hnd)
			{
				if (_buttonReleased == CLR_NULL)
				{
					if (_joyStickListener == 0)
					{
						_joyStickListener = new JoyStickListener_Director(this);
						static_cast<OIS::JoyStick*>(_native)->setEventCallback(_joyStickListener);
					}
					_joyStickListener->doCallForButtonReleased = true;
				}
				_buttonReleased += hnd;
			}
			void remove(MOIS::JoyStickListener::ButtonReleasedHandler^ hnd)
			{
				_buttonReleased -= hnd;
				if (_buttonReleased == CLR_NULL) _joyStickListener->doCallForButtonReleased = false;
			}
		private:
			bool raise( MOIS::JoyStickEvent^ arg, int button )
			{
				if (_buttonReleased)
					return _buttonReleased->Invoke( arg, button );
			}
		}
	
		event MOIS::JoyStickListener::AxisMovedHandler^ AxisMoved
		{
			void add(MOIS::JoyStickListener::AxisMovedHandler^ hnd)
			{
				if (_axisMoved == CLR_NULL)
				{
					if (_joyStickListener == 0)
					{
						_joyStickListener = new JoyStickListener_Director(this);
						static_cast<OIS::JoyStick*>(_native)->setEventCallback(_joyStickListener);
					}
					_joyStickListener->doCallForAxisMoved = true;
				}
				_axisMoved += hnd;
			}
			void remove(MOIS::JoyStickListener::AxisMovedHandler^ hnd)
			{
				_axisMoved -= hnd;
				if (_axisMoved == CLR_NULL) _joyStickListener->doCallForAxisMoved = false;
			}
		private:
			bool raise( MOIS::JoyStickEvent^ arg, int axis )
			{
				if (_axisMoved)
					return _axisMoved->Invoke( arg, axis );
			}
		}
	
		event MOIS::JoyStickListener::SliderMovedHandler^ SliderMoved
		{
			void add(MOIS::JoyStickListener::SliderMovedHandler^ hnd)
			{
				if (_sliderMoved == CLR_NULL)
				{
					if (_joyStickListener == 0)
					{
						_joyStickListener = new JoyStickListener_Director(this);
						static_cast<OIS::JoyStick*>(_native)->setEventCallback(_joyStickListener);
					}
					_joyStickListener->doCallForSliderMoved = true;
				}
				_sliderMoved += hnd;
			}
			void remove(MOIS::JoyStickListener::SliderMovedHandler^ hnd)
			{
				_sliderMoved -= hnd;
				if (_sliderMoved == CLR_NULL) _joyStickListener->doCallForSliderMoved = false;
			}
		private:
			bool raise( MOIS::JoyStickEvent^ param1, int index )
			{
				if (_sliderMoved)
					return _sliderMoved->Invoke( param1, index );
			}
		}
	
		event MOIS::JoyStickListener::PovMovedHandler^ PovMoved
		{
			void add(MOIS::JoyStickListener::PovMovedHandler^ hnd)
			{
				if (_povMoved == CLR_NULL)
				{
					if (_joyStickListener == 0)
					{
						_joyStickListener = new JoyStickListener_Director(this);
						static_cast<OIS::JoyStick*>(_native)->setEventCallback(_joyStickListener);
					}
					_joyStickListener->doCallForPovMoved = true;
				}
				_povMoved += hnd;
			}
			void remove(MOIS::JoyStickListener::PovMovedHandler^ hnd)
			{
				_povMoved -= hnd;
				if (_povMoved == CLR_NULL) _joyStickListener->doCallForPovMoved = false;
			}
		private:
			bool raise( MOIS::JoyStickEvent^ arg, int index )
			{
				if (_povMoved)
					return _povMoved->Invoke( arg, index );
			}
		}
	
		event MOIS::JoyStickListener::Vector3MovedHandler^ Vector3Moved
		{
			void add(MOIS::JoyStickListener::Vector3MovedHandler^ hnd)
			{
				if (_vector3Moved == CLR_NULL)
				{
					if (_joyStickListener == 0)
					{
						_joyStickListener = new JoyStickListener_Director(this);
						static_cast<OIS::JoyStick*>(_native)->setEventCallback(_joyStickListener);
					}
					_joyStickListener->doCallForVector3Moved = true;
				}
				_vector3Moved += hnd;
			}
			void remove(MOIS::JoyStickListener::Vector3MovedHandler^ hnd)
			{
				_vector3Moved -= hnd;
				if (_vector3Moved == CLR_NULL) _joyStickListener->doCallForVector3Moved = false;
			}
		private:
			bool raise( MOIS::JoyStickEvent^ arg, int index )
			{
				if (_vector3Moved)
					return _vector3Moved->Invoke( arg, index );
			}
		}
	
	
		static property int MIN_AXIS
		{
		public:
			int get();
		}
	
		static property int MAX_AXIS
		{
		public:
			int get();
		}
	
		property MOIS::JoyStickState_NativePtr JoyStickState
		{
		public:
			MOIS::JoyStickState_NativePtr get();
		}
	
		property float Vector3Sensitivity
		{
		public:
			float get();
		public:
			void set(float degrees);
		}
	
		int GetNumberOfComponents( MOIS::ComponentType cType );
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( JoyStick )
	
		//Protected Declarations
	protected public:
		virtual bool OnButtonPressed( MOIS::JoyStickEvent^ arg, int button ) = IJoyStickListener_Receiver::ButtonPressed
		{
			return ButtonPressed( arg, button );
		}
	
		virtual bool OnButtonReleased( MOIS::JoyStickEvent^ arg, int button ) = IJoyStickListener_Receiver::ButtonReleased
		{
			return ButtonReleased( arg, button );
		}
	
		virtual bool OnAxisMoved( MOIS::JoyStickEvent^ arg, int axis ) = IJoyStickListener_Receiver::AxisMoved
		{
			return AxisMoved( arg, axis );
		}
	
		virtual bool OnSliderMoved( MOIS::JoyStickEvent^ param1, int index ) = IJoyStickListener_Receiver::SliderMoved
		{
			return SliderMoved( param1, index );
		}
	
		virtual bool OnPovMoved( MOIS::JoyStickEvent^ arg, int index ) = IJoyStickListener_Receiver::PovMoved
		{
			return PovMoved( arg, index );
		}
	
		virtual bool OnVector3Moved( MOIS::JoyStickEvent^ arg, int index ) = IJoyStickListener_Receiver::Vector3Moved
		{
			return Vector3Moved( arg, index );
		}
	
	
	
	};
	

}
