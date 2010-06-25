
#pragma once

#include <gcroot.h>
using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Text;
using namespace Hikari;
using namespace Ogre;
using namespace Mogre;
using namespace MOIS;
using namespace System::Collections::Generic;


namespace HikariWrapper {


	// Enums **********************************************************************************************

	/**
	* Defines the relative positions for the Position object.
	*/
	public enum class RelativePosition
	{
		Left,
		TopLeft,
		TopCenter,
		TopRight,
		Right,
		BottomRight,
		BottomCenter,
		BottomLeft,
		Center
	};

	/**
	* Used by FlashControl::setQuality, defines the Flash rendering quality.
	* 
	* <ul>
	* <li>RQ_LOW - Favors playback speed over appearance and never uses anti-aliasing.
	* <li>RQ_MEDIUM - Applies some anti-aliasing and does not smooth bitmaps. It produces a better quality than the Low setting, but lower quality than the High setting.
	* <li>RQ_HIGH - Favors appearance over playback speed and always applies anti-aliasing. If the movie does not contain animation, bitmaps are smoothed; if the movie has animation, bitmaps are not smoothed.
	* <li>RQ_BEST - Provides the best display quality and does not consider playback speed. All output is anti-aliased and all bitmaps are smoothed.
	* <li>RQ_AUTOLOW - Emphasizes speed at first but improves appearance whenever possible. Playback begins with anti-aliasing turned off. If the Flash Player detects that the processor can handle it, anti-aliasing is turned on.
	* <li>RQ_AUTOHIGH - Emphasizes playback speed and appearance equally at first but sacrifices appearance for playback speed if necessary. Playback begins with anti-aliasing turned on. If the actual frame rate drops below the specified frame rate, anti-aliasing is turned off to improve playback speed.
	* </ul>
	*/
	public enum class RenderQuality
	{
		LOW,
		MEDIUM,
		HIGH,
		BEST,
		AUTOLOW,
		AUTOHIGH
	};

	/**
	* Used by FlashControl::setScaleMode, defines the scaling mode to use when the aspect ratio of the control does not match that of the movie.
	*
	* <ul>
	* <li>SM_SHOWALL - Preserves the movie's aspect ratio by adding borders. (Default)
	* <li>SM_NOBORDER - Preserves the movie's aspect ratio by cropping the sides.
	* <li>SM_EXACTFIT - Does not preserve the movie's aspect ratio, scales the movie to the dimensions of the control.
	*/
	public enum class ScaleMode
	{
		SHOWALL,
		NOBORDER,
		EXACTFIT
	};

	// Delegates *******************************************************************************************
	public delegate void CallbackDelegate(array<System::Object^>^ args);

	// Utilities *********************************************************************************************
	public value class Utilities
	{
		public:
			static HikariWrapper::RelativePosition ToM_RelativePosition(Hikari::RelativePosition UM_RelativePosition);
			static HikariWrapper::RenderQuality ToM_RenderQuality(Hikari::RenderQuality M_RenderQuality);
			static HikariWrapper::ScaleMode ToM_ScaleMode(Hikari::ScaleMode UM_ScaleMode);

			static Hikari::RelativePosition ToUM_RelativePosition(HikariWrapper::RelativePosition M_RelativePosition);
			static Hikari::ScaleMode ToUM_ScaleMode(HikariWrapper::ScaleMode M_ScaleMode);
			static Hikari::RenderQuality ToUM_RenderQuality(HikariWrapper::RenderQuality M_RenderQuality);

			static void GetNativeString(Ogre::String& UnmanagedString,System::String^ ManagedString);
			static void GetNativeUnicodeString(Ogre::DisplayString& UnmanagedString,System::String^ ManagedString);
			static Mogre::ColourValue GetNumberAsColor(float ColorVal);

	};

	// Enum convertors

	// To Managed Enums

	HikariWrapper::ScaleMode Utilities::ToM_ScaleMode(Hikari::ScaleMode UM_ScaleMode)
	{
		return safe_cast<HikariWrapper::ScaleMode>(UM_ScaleMode);
	}

	HikariWrapper::RenderQuality Utilities::ToM_RenderQuality(Hikari::RenderQuality UM_RenderQuality)
	{
		return safe_cast<HikariWrapper::RenderQuality>(UM_RenderQuality);
	}

	HikariWrapper::RelativePosition Utilities::ToM_RelativePosition(Hikari::RelativePosition UM_RelativePosition)
	{
		return safe_cast<HikariWrapper::RelativePosition>(UM_RelativePosition);
	}

	// To UnManaged enums

	Hikari::RelativePosition Utilities::ToUM_RelativePosition(HikariWrapper::RelativePosition M_RelativePosition)
	{
		return static_cast<Hikari::RelativePosition>(M_RelativePosition);
	}

	Hikari::ScaleMode Utilities::ToUM_ScaleMode(HikariWrapper::ScaleMode M_ScaleMode)
	{
		return static_cast<Hikari::ScaleMode>(M_ScaleMode);
	}

	Hikari::RenderQuality Utilities::ToUM_RenderQuality(HikariWrapper::RenderQuality M_RenderQuality)
	{
		return static_cast<Hikari::RenderQuality>(M_RenderQuality);
	}

	// *************************************************************************************************
	// getNumberAsColor(float ColorVal)
	// Gets the Ogre.ColourValue from passed float color
	Mogre::ColourValue Utilities::GetNumberAsColor(float ColorVal)
	{
		FlashValue fVal(ColorVal);
		Ogre::ColourValue nColorVal = fVal.getNumberAsColor();
		Mogre::ColourValue mColorVal(nColorVal.r,nColorVal.g,nColorVal.b);
		return mColorVal;
	}

	// *************************************************************************************************
	// GetNativeString(Ogre::String& UnmanagedString,System::String^ ManagedString)
	// Gets the native string from managed string
	void Utilities::GetNativeString(Ogre::String& UnmanagedString,System::String^ ManagedString)
	{
		IntPtr str = Marshal::StringToHGlobalAnsi(ManagedString);
		UnmanagedString = static_cast<char*>(str.ToPointer());
		Marshal::FreeHGlobal( str );
	}

	// *************************************************************************************************
	// GetNativeUnicodeString(Ogre::DisplayString& UnmanagedString,System::String^ ManagedString)
	// Gets the native unicode string from managed string
	void Utilities::GetNativeUnicodeString(Ogre::DisplayString& UnmanagedString,System::String^ ManagedString)
	{
		IntPtr str = Marshal::StringToHGlobalUni(ManagedString);
		UnmanagedString = static_cast<wchar_t*>(str.ToPointer());
		Marshal::FreeHGlobal( str );
	}


	// *************************************************************************************************
	// HikariCallback class
	// 	
	class HikariCallback
	{
	public:

		HikariCallback(FlashControl * Control,Ogre::String FuncName)
		{
			Control->bind(FuncName,FlashDelegate(&HikariInternalCallback));
		}

		static Hikari::FlashValue HikariInternalCallback(FlashControl* caller,ManagedReferenceHolder args);
	};

	// *************************************************************************************************
	// FlashControl class
	// Managed FlashControl class
	public ref class FlashControl
	{
		private:
			System::String^ _MName;
			Dictionary<System::String^,CallbackDelegate^> _DictionnaryDelegate;
			Hikari::FlashControl* NativeControl;

		public protected:
			/**
			* Constructor. Used by HikariManager
			*/
			HikariWrapper::FlashControl::FlashControl(Hikari::FlashControl* nCtrl,System::String^ MName)
			{
				NativeControl = nCtrl;
				_MName = MName;
			}

		public:

			/**
			* Gets the name of the control
			*/
			System::String^ GetName()
			{
				return _MName;
			}

			/**
			* Retrieves the name of the Ogre::Material used by this FlashControl.
			*/
			property System::String^ MaterialName
			{
				virtual System::String^ get()
				{ 
					System::String^ managed_str = gcnew System::String(const_cast<char*>(NativeControl->getMaterialName().c_str()));
					return managed_str;
				}
			}

			Hikari::FlashControl* GetNative()
			{
				return NativeControl;
			}
			
			/**
			* Loads a movie (a .swf file) into this FlashControl and begins playing.
			*
			* @param	movieFilename	The filename of the movie to load.
			*
			* @note	The specified movie should reside in the "assetsDirectory" that
			*		was declared when the HikariManager was instantiated.
			*/
			void Load(System::String^ CtrlName)
			{
				Ogre::String FlashControlName;
				Utilities::GetNativeString(FlashControlName,CtrlName);
				NativeControl->load(FlashControlName);	
			}

				/**
			* Plays the currently-loaded movie.
			*/
			void Play()
			{
				NativeControl->play();
			}

			/**
			* Stops the currently loaded movie.
			*/
			void Stop()
			{
				NativeControl->stop();
			}

			/**
			* Rewinds the currently-loaded movie to the beginning.
			*/
			void Rewind()
			{
				NativeControl->rewind();
			}

			/**
			* Navigates the currently-loaded movie to a certain frame and stops there.
			*/
			void GotoFrame(long frameNum)
			{
				NativeControl->gotoFrame(frameNum);
			}

			/**
			* Sets whether or not the currently-loaded movie should use a
			* transparent background instead of the default background-color.
			*
			* @param	isTransparent	Whether or not the movie should use "transparent" rendering.
			*/
			void SetTransparent(bool bMakeTransparent)
			{
				NativeControl->setTransparent(bMakeTransparent);
			}

			/* Sets whether or not the currently-loaded movie should use a
			* transparent background instead of the default background-color.
			*
			* @param	isTransparent	Whether or not the movie should use "transparent" rendering.
			* @param	useAlphaHack	With some Flash versions, there are certain glitches with
			*							transparent rendering (usually with text and aliased geometry).
			*							Set this parameter to 'true' to use an alternative alpha-rendering
			*							hack that may mitigate these issues at the cost of some performance.
			*/
			void  SetTransparent(bool bMakeTransparent,bool useAlphaHack)
			{
				NativeControl->setTransparent(bMakeTransparent,useAlphaHack);
			}

			/**
			* Sets whether or not the currently-loaded movie should restart when
			* it reaches the end.
			*
			* @param	shouldLoop	Whether the currently-loaded movie should loop.
			*/
			void SetLoop(bool shouldLoop)
			{
				NativeControl->setLoop(shouldLoop);
			}

			/**
			* Sets the Flash rendering quality for the currently-loaded movie.
			*
			* @param	renderQuality	The RenderQuality to use.
			*/
			void SetQuality(HikariWrapper::RenderQuality renderQuality)
			{
				NativeControl->setQuality(Utilities::ToUM_RenderQuality(renderQuality));
			}

			/**
			* Sets the scaling mode to use when the aspect ratio of the movie and control do not match.
			*
			* @param	scaleMode	The ScaleMode to use.
			*/
			void SetScaleMode(HikariWrapper::ScaleMode scaleMode)
			{
				NativeControl->setScaleMode(Utilities::ToUM_ScaleMode(scaleMode));
			}

			/**
			* Sets whether this FlashControl is draggable via the right-mouse-button,
			* this is only applicable to FlashControls created as an overlay.
			*
			* @param	isDraggable		Whether or not this FlashControl should be draggable.
			*/
			void SetDraggable(bool isDraggable)
			{
				NativeControl->setDraggable(isDraggable);
			}

			/**
			* Sets whether or not mouse-clicks over transparent pixels should be ignored (this is on by default),
			* this is only applicable to transparent FlashControls created as an overlay.
			*
			* @param	shouldIgnore	Whether or not transparent pixels should be ignored.
			* @param	threshold	The opacity threshold (in percent, 0 to 1.0), pixels with
			*						opacities less than this amount will be ignored.
			*/

			void SetIgnoreTransparentPixels(bool shouldIgnore, float threshold)
			{
				NativeControl->setIgnoreTransparentPixels(shouldIgnore, threshold);
			}


			/**
			* Attempts to call a function declared as a callback in the ActionScript of the currently-loaded movie.
			* 
			* @param	funcName	The name of the callback that was declared using 'ExternalInterface.addCallback(funcName, function)'
			*						in the ActionScript of the currently-loaded movie.
			* @param	args	The arguments to pass to the ActionScript function.
			*
			* @return	If the invocation was successful and the ActionScript function returned a value, returns a FlashValue with a non-null type.
			*
			* @note	It is highly recommended to use the 'Args(arg1)(arg2)(arg3)...' helper class to pass arguments.
			*/
			void CallFunction(System::String^ funcName,array<System::Object^>^ args)
			{
				Ogre::String NativefuncName;
				Utilities::GetNativeString(NativefuncName,funcName);

				std::vector<Hikari::FlashValue> NativeArgs;

				for each(System::Object^ val in args)
				{
					if(val->GetType() == System::Int32::typeid)
					{
						 Hikari::FlashValue cVal((int)val);
						 NativeArgs.push_back(cVal);
					}

					if(val->GetType() == System::Boolean::typeid)
					{
						Hikari::FlashValue cVal((bool)val);
						NativeArgs.push_back(cVal);
					}

					if(val->GetType() == System::Single::typeid)
					{
						Hikari::FlashValue cVal((float)val);
						NativeArgs.push_back(cVal);
					}

					if(val->GetType() == System::String::typeid)
					{
						Ogre::DisplayString mStr;
						Utilities::GetNativeUnicodeString(mStr,(System::String^)val);
						Hikari::FlashValue cVal(mStr);
						NativeArgs.push_back(cVal);
					}
				}

				NativeControl->callFunction(NativefuncName,NativeArgs);
			}


			/**
			* Binds a local callback to a certain function name so that your Flash movie can call the function
			* from ActionScript using ExternalInterface.call('functionName').
			*
			* @param	funcName	The name to bind this callback to.
			* @param	callback	The local function to call, see below for examples of declaring a FlashDelegate.
			*
			*	\code
			*	// Example declaration of a compatible function (static function):
			*	FlashValue myStaticFunction(FlashControl* caller, const Arguments& args)
			*	{
			*		// Handle the callback here
			*		return FLASH_VOID;
			*	}
			*
			*	// Example declaration of a compatible function (member function):
			*	FlashValue MyClass::myMemberFunction(FlashControl* caller, const Arguments& args)
			*	{
			*		// Handle the callback here
			*		return "Some return value!";
			*	}
			*
			*	// FlashDelegate (member function) instantiation:
			*	FlashDelegate callback(this, &MyClass::myMemberFunction); // within a class
			*	FlashDelegate callback2(pointerToClassInstance, &MyClass::myMemberFunction);
			*
			*	// FlashDelegate (static function) instantiation:
			*	FlashDelegate callback(&myStaticFunction);
			*	\endcode
			*/
			void Bind(System::String^ funcName,CallbackDelegate^ CallDelegate)
			{
				Ogre::String NativefuncName;
				Utilities::GetNativeString(NativefuncName,funcName);
				HikariCallback cCallback(NativeControl,NativefuncName);
				_DictionnaryDelegate.Add(funcName,CallDelegate);
			}

			/**
			* Un-binds the specified callback.
			*
			* @param	funcName	The name that the callback was bound to.
			*/
			void Unbind(System::String^ funcName)
			{
				Ogre::String NativefuncName;
				Utilities::GetNativeString(NativefuncName,funcName);
				NativeControl->unbind(NativefuncName);
				_DictionnaryDelegate.Remove(funcName);
			}


			/**
			* Gets the delegate of the specified function name
			*
			* @param	funcName	The name that the callback was bound to.
			*/
			CallbackDelegate^ GetDelegateByFuncName(System::String^ funcName)
			{
				if(_DictionnaryDelegate.ContainsKey(funcName))
					return _DictionnaryDelegate[funcName];
				return nullptr;
			}


			/**
			* If this FlashControl was created as an overlay, hides the overlay.
			*/
			void Hide()
			{
				NativeControl->hide();
			}

			/**
			* If this FlashControl was created as an overlay, shows the overlay.
			*/
			void Show()
			{
				NativeControl->show();
			}

			/**
			* Returns whether or not the FlashControl overlay is currently visible. (See FlashControl::hide and FlashControl::show)
			* If this FlashControl was not created as an overlay, always returns false.
			*/
			bool GetVisibility()
			{
				return NativeControl->getVisibility();
			}

			/**
			* Sets the opacity of this FlashControl.
			*
			* @param	opacity	The opacity as a Real value; 0 is totally transparent, 1 is totally opaque.
			*/
			void SetOpacity(float opacityValue)
			{
				NativeControl->setOpacity(opacityValue);
			}
			
			/**
			* Gives this FlashControl keyboard focus. Additionally, if this FlashControl is an overlay, pops it to the front.
			*/
			void SetFocus()
			{
				NativeControl->focus();
			}

			
			/**
			* If this FlashControl was created as an overlay, moves the overlay in relative amounts.
			*
			* @param	deltaX	The amount (in pixels) to move the overlay in the X-axis.
			* @param	deltaY	The amount (in pixels) to move the overlay in the Y-axis.
			*/
			void Move(int deltaX, int deltaY)
			{
				NativeControl->move(deltaX, deltaY);
			}

			/**
			* Injects a mouse-move event into this FlashControl (in the control's local coordinate-space).
			*
			* @param	xPos	The local X-coordinate.
			* @param	yPos	The local Y-coordinate.
			*/
			void InjectMouseMove(int xPos, int yPos)
			{
				NativeControl->injectMouseMove(xPos, yPos);
			}

			/**
			* Injects a mouse-down event into this FlashControl (in the control's local coordinate-space).
			*
			* @param	xPos	The local X-coordinate.
			* @param	yPos	The local Y-coordinate.
			*/
			void InjectMouseDown(int xPos, int yPos)
			{
				NativeControl->injectMouseDown(xPos, yPos);
			}

			/**
			* Injects a mouse-up event into this FlashControl (in the control's local coordinate-space).
			*
			* @param	xPos	The local X-coordinate.
			* @param	yPos	The local Y-coordinate.
			*/
			void InjectMouseUp(int xPos, int yPos)
			{
				NativeControl->injectMouseUp(xPos, yPos);
			}

			/**
			* Injects a mouse-wheel event into this FlashControl (in the control's local coordinate-space).
			*
			* @param	relScroll	The relative scroll amount of the mouse-wheel.
			* @param	xPos	The local X-coordinate of the mouse.
			* @param	yPos	The local Y-coordinate of the mouse.
			*/
			void InjectMouseWheel(int relScroll, int xPos, int yPos)
			{
				NativeControl->injectMouseWheel(relScroll, xPos, yPos);
			}

			/**
			* Changes the current position and sets it as the default. Only applicable if this FlashControl was created as an overlay.
			*
			* @param	position	The new position.
			*/
			void SetPosition(HikariWrapper::RelativePosition RelPos,int OffsetX,int OffsetY)
			{
			  Position NativePos(Utilities::ToUM_RelativePosition(RelPos),OffsetX,OffsetY);
			  NativeControl->setPosition(NativePos);
			}

			void SetPosition(int AbsX,int AbsY)
			{
				Position NativePos(AbsX,AbsY);
				NativeControl->setPosition(NativePos);
			}

			/**
			* Resets the current position to the default. Only applicable if this FlashControl was created as an overlay.
			*/
			void ResetPosition()
			{
				NativeControl->resetPosition();
			}
			
			/**
			* Retrieves the current screen coordinates (in relation to the render-window) of the overlay. Only applicable if this 
			* FlashControl was created as an overlay.
			*
			* @param[out]	x	The integer to store the current x-coordinate in.
			* @param
			*/
			void GetCoordinates([Out] int% x,[Out] int% y)
			{
				int NatX = 0,NatY = 0;

				NativeControl->getCoordinates(NatX,NatY);
				
				x = static_cast<int%>(NatX);
                y = static_cast<int%>(NatY);

			}

			/**
			* Retrieves the width and height that this FlashControl was created with.
			*
			* @param[out]	width	The unsigned short that will be used to store the retrieved width.
			*
			* @param[out]	height	The unsigned short that will be used to store the retrieved height.
			*/
			void GetExtents([Out] unsigned short% width,[Out] unsigned short% height)
			{	
				unsigned short NatWidth = 0,NatHeight = 0;

				NativeControl->getExtents(NatWidth,NatHeight);
				
				width = static_cast<unsigned short%>(NatWidth);
                height = static_cast<unsigned short%>(NatHeight);

			}

			/**
			* Gets the UV scale of this FlashControl's internal texture. On certain systems we must compensate for lack of
			* NPOT-support on the videocard by using the next-highest POT texture. Normally, FlashControl overlays compensate 
			* their texture coordinates automatically however FlashControls created as pure materials will need to adjust 
			* their own by use of this function.
			*
			* @param[out]	uScale	The Ogre::Real that will be used to store the retrieved U-scale.
			* @param[out]	vScale	The Ogre::Real that will be used to store the retrieved V-scale.
			*/
			void GetUVScale([Out] float% uScale,[Out] float% vScale)
			{
				Ogre::Real NatuScale = 0,NatvScale = 0;

				NativeControl->getUVScale(NatuScale,NatvScale);

				uScale = static_cast<float%>(NatuScale);
				vScale = static_cast<float%>(NatvScale);
			}


	};




	private value class FlashControlHolder
	{
		private:
			static Dictionary<System::String^,FlashControl^>^ Dictionnary_FlashControls;
		public:	
			static void RegisterFlashControl(FlashControl^ cFlashControl);
			static void RemoveFlashControl(System::String^ CtrlName);
			static FlashControl^ GetFlashControlByName(System::String^ FlashControlName);
			static void RemoveAllFlashControls();
	};
	void FlashControlHolder::RegisterFlashControl(FlashControl^ cFlashControl)
	{
			if(Dictionnary_FlashControls == nullptr)
				Dictionnary_FlashControls = gcnew Dictionary<System::String^,FlashControl^>();

			if(!Dictionnary_FlashControls->ContainsKey(cFlashControl->GetName()))
				Dictionnary_FlashControls->Add(cFlashControl->GetName(),cFlashControl);
	}

	void FlashControlHolder::RemoveFlashControl(System::String^ CtrlName)
	{
		if(Dictionnary_FlashControls->ContainsKey(CtrlName))
				Dictionnary_FlashControls->Remove(CtrlName);
	}

	void FlashControlHolder::RemoveAllFlashControls()
	{
		Dictionnary_FlashControls->Clear();
	}

	FlashControl^ FlashControlHolder::GetFlashControlByName(System::String^ cName)
	{
			if(Dictionnary_FlashControls->ContainsKey(cName))
				return Dictionnary_FlashControls[cName];
			return nullptr;
	}



	Hikari::FlashValue HikariCallback::HikariInternalCallback(Hikari::FlashControl* caller, ManagedReferenceHolder args)
	{
		try
		{
		System::String^ mFuncName;
		System::String^ mControlName;

		mFuncName = gcnew System::String(const_cast<wchar_t*>(args.FuncName.c_str()));
		mControlName = gcnew System::String(const_cast<char*>(caller->getName().c_str()));
		array<System::Object^>^ mArgs = gcnew array<System::Object^>(args.NativeArguments.size());

		for (unsigned int i =0;i<args.NativeArguments.size();i++)
		{
			short val =  args.NativeArguments.at(i).getType();

			switch(val)
			{
				case 0:
					mArgs[i] = nullptr;
				break;
				case 1:
					mArgs[i] = args.NativeArguments.at(i).getBool();
				break;
				case 2:
					mArgs[i] = args.NativeArguments.at(i).getNumber();
				break;
				case 3:
					mArgs[i] = gcnew System::String(const_cast<wchar_t*>(args.NativeArguments.at(i).getString().asWStr_c_str()));
				break;
			}
		}
		
		CallbackDelegate^ cDel = FlashControlHolder::GetFlashControlByName(mControlName)->GetDelegateByFuncName(mFuncName);

		if(cDel != nullptr)
			cDel->Invoke(mArgs);
		}
		catch(System::Exception^ ex)
		{
			Console::WriteLine("error : "+ex->ToString());
		}

		return FLASH_VOID;
	}


	
	

	public ref class HikariManager
	{
		private:
			Hikari::HikariManager* NativeManager;

		public:
			HikariManager(System::String^ AssetsDirectory)
			{
				Ogre::String NativeAssetsDirectory;
				Utilities::GetNativeString(NativeAssetsDirectory,AssetsDirectory);
				NativeManager = new Hikari::HikariManager(NativeAssetsDirectory);
			}

			~HikariManager()
			{
				if(NativeManager)
				{
					delete NativeManager;
					NativeManager = NULL;
				}
			}

		public:

			HikariWrapper::FlashControl^ CreateFlashOverlay(System::String^ CtrlName,Mogre::Viewport^ viewport,int Width,int Height,HikariWrapper::RelativePosition CtrlPos,unsigned short zOrder,unsigned short zTier)
			{
				Ogre::String NativeCtrlName;
				Utilities::GetNativeString(NativeCtrlName,CtrlName);
				Ogre::Viewport* nativeViewPort = (Ogre::Viewport*)viewport;   
				Hikari::FlashControl* nativeCtrl = NativeManager->createFlashOverlay(NativeCtrlName,nativeViewPort,Width,Height,Position(Utilities::ToUM_RelativePosition(CtrlPos)),zOrder,zTier);
				HikariWrapper::FlashControl^ ManagedCtrl = gcnew HikariWrapper::FlashControl(nativeCtrl,CtrlName);
				FlashControlHolder::RegisterFlashControl(ManagedCtrl);
				return ManagedCtrl;

			}

			HikariWrapper::FlashControl^ CreateFlashMaterial(System::String^ CtrlName, int width, int height)
			{
				// Get native string
				Ogre::String NativeCtrlName;
				Utilities::GetNativeString(NativeCtrlName,CtrlName);

				// Create managed flash material
				Hikari::FlashControl* nativeCtrl = NativeManager->createFlashMaterial(NativeCtrlName, width, height);
				HikariWrapper::FlashControl^ ManagedCtrl = gcnew HikariWrapper::FlashControl(nativeCtrl,CtrlName);
				FlashControlHolder::RegisterFlashControl(ManagedCtrl);
				return ManagedCtrl;
			}			

			/**
			* Flags the specified FlashControl for destruction.
			*
			* @param	controlName	The name of the control to flag for destruction.
			*/
			void DestroyFlashControl(System::String^ CtrlName)
			{
				// Get native string
				Ogre::String NativeCtrlName;
				Utilities::GetNativeString(NativeCtrlName,CtrlName);

				// Must remove the managed flash control from the list
				FlashControlHolder::RemoveFlashControl(CtrlName);

				NativeManager->destroyFlashControl(NativeCtrlName);
			}

			void DestroyFlashControl(HikariWrapper::FlashControl^ mControl)
			{
				FlashControlHolder::RemoveFlashControl(mControl->GetName());

				NativeManager->destroyFlashControl(mControl->GetNative());
			}

			void DestroyAllControls()
			{
				NativeManager->destroyAllControls();
				FlashControlHolder::RemoveAllFlashControls();
			}

			/**
			* Retrieves a previously-created FlashControl by name.
			*
			* @param	controlName	The name of the FlashControl to retrieve.
			*
			* @return	If it is found, returns a pointer to the FlashControl, else returns 0.
			*/
			HikariWrapper::FlashControl^ GetFlashControl(System::String^ CtrlName)
			{
				return FlashControlHolder::GetFlashControlByName(CtrlName);
			}

			void Update()
			{
				NativeManager->update();
			}

			bool InjectMouseMove(int x,int y)
			{
				return NativeManager->injectMouseMove(x,y);
			}

			bool InjectMouseDown(MOIS::MouseButtonID Id)
			{
				return NativeManager->injectMouseDown(static_cast<int>(Id));
			}
			
			bool InjectMouseUp(MOIS::MouseButtonID Id)
			{
				return NativeManager->injectMouseUp(static_cast<int>(Id));
			}
			
			bool IsAnyFocused()
			{
				return NativeManager->isAnyFocused();
			}

			bool FocusControl(int x,int y,FlashControl^ selection)
			{
				return NativeManager->focusControl(x,y,selection->GetNative());
			}
	};



}
