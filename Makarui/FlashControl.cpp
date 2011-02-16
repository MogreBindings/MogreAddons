#include "FlashControl.h"

namespace Ogre
{
	class Bitwise;
}

namespace Makarui
{

		NativeCallbackDelegate::NativeCallbackDelegate(Akarui::FlashMovie* nativeControl)
		{
			_ManagedDelegate = gcnew Dictionary<System::String^,CallbackDelegate^>();
			_NativeControl = nativeControl;
		}

		NativeCallbackDelegate::~NativeCallbackDelegate()
	    {
			((Dictionary<System::String^,CallbackDelegate^>^)_ManagedDelegate)->Clear();
			delete _ManagedDelegate;
	    }

		FlashValue NativeCallbackDelegate::onFlashCall(const std::string& funcName, const FlashArguments& args)
		{
			try
			{
				System::String^ mFuncName;

				mFuncName = gcnew System::String(const_cast<char*>(funcName.c_str()));
				array<System::Object^>^ mArgs = gcnew array<System::Object^>(args.size());

				if(mFuncName == "isdirty" && !_NativeControl->isContinuousDirty())
				{
					_NativeControl->setDirtiness(true);
					return Akarui::FlashValue();
				}
				else if(mFuncName == "isclean")
				{
					_NativeControl->setDirtiness(false);
					return Akarui::FlashValue();
				}
				else if(mFuncName == "iscontinuousdirty")
				{
					_NativeControl->setDirtiness(true, true);
					return Akarui::FlashValue();
				}


				for (unsigned int i =0;i<args.size();i++)
				{
					Akarui::FlashValue fVal = args.at(i);

					if(fVal.isNull())
						mArgs[i] = nullptr;
					else if(fVal.isBoolean())
						mArgs[i] = fVal.toBoolean();
					else if(fVal.isInteger())
						mArgs[i] = fVal.toInteger();
					else if(fVal.isDouble())
						mArgs[i] = fVal.toDouble();
					else if(fVal.isString())
						mArgs[i] =  gcnew System::String(const_cast<char*>(fVal.toString().c_str()));
				}

				if(((Dictionary<System::String^,CallbackDelegate^>^)_ManagedDelegate)->ContainsKey(mFuncName))
					((Dictionary<System::String^,CallbackDelegate^>^)_ManagedDelegate)->default[mFuncName]->Invoke(mArgs);
				
			}
			catch(System::Exception^ ex)
			{
				Console::WriteLine("error : "+ex->ToString());
			}



			return Akarui::FlashValue();
		}

		void NativeCallbackDelegate::AddFunctionDelegate(Ogre::String funcName, CallbackDelegate^ pDel)
		{
			System::String^ MFuncName = gcnew System::String(const_cast<char*>(funcName.c_str()));
			((Dictionary<System::String^,CallbackDelegate^>^)_ManagedDelegate)->Add(MFuncName,pDel);
		}

		void NativeCallbackDelegate::RemoveFunctionDelegate(Ogre::String funcName)
		{
			System::String^ MFuncName = gcnew System::String(const_cast<char*>(funcName.c_str()));
			((Dictionary<System::String^,CallbackDelegate^>^)_ManagedDelegate)->Remove(MFuncName);
		}

		FlashControl::FlashControl(System::String^ pName,Mogre::Viewport^ pViewport)
		{
			_Name = pName;
			_TextureName = pName + "Texture";
			_MaterialName = pName + "Material";
			_IsDestroyed = false;
			_ForceRedraw = false;
			_FirstFrame = true;
			_HasOverlay = false;
			_Viewport = pViewport;
			_Visible = true;
			_RelPosition = Makarui::RelativePosition::TopLeft;
			_NoEvent = false;
		}

		void FlashControl::SetNativeControl(Akarui::FlashMovie* ofFlashMovie)
		{
			_NativeControl = ofFlashMovie;
			_NativeCallbackDelegate = new NativeCallbackDelegate(ofFlashMovie);
			_NativeControl->setHandler(_NativeCallbackDelegate);
		}

		FlashControl::~FlashControl()
		{
			if(!_IsDestroyed)
				Destroy();

			if(_NativeCallbackDelegate)
			{
				delete _NativeCallbackDelegate;
				_NativeCallbackDelegate = NULL;
			}
		}

		void FlashControl::Destroy()
		{
			Mogre::OverlayManager::Singleton->DestroyOverlayElement(_Panel);
			

			if(_Panel)
				delete _Panel;

			_Panel = nullptr;


			Mogre::OverlayManager::Singleton->Destroy(_Overlay);

			if(_Overlay) 
				delete _Overlay;

			_Overlay = nullptr;

			Mogre::MaterialManager::Singleton->Remove(_MaterialName);
			Mogre::TextureManager::Singleton->Remove(_TextureName);

			delete _WebMaterial;
			delete _WebTexture;

			_WebMaterial = nullptr;
			_WebTexture = nullptr;
			
			_IsDestroyed = true;
			_HasOverlay = false;
		}


		int iFrame = 0;
		const int FRAME_PER_INVALIDATION = 2;

		void FlashControl::Draw()
		{
			// We force-render the first frame because Flash 10 doesn't notify 
			// us of further invalidations if we don't
			
			if(!_FirstFrame)
				if(!_NativeControl->isDirty() && !_ForceRedraw)
					return;

			_NativeControl->render();

			if(_NativeControl->getManualInvalidation() && !_FirstFrame)
			{
				if(!_NativeControl->isContinuousDirty())
				{
					iFrame++;

					if(iFrame == FRAME_PER_INVALIDATION)
					{
						_NativeControl->setDirtiness(false);
						iFrame = 0;
					}
				}
			}

			Console::WriteLine(DateTime::Now.ToString()+"Draw control "+_Name+" \n");

			_FirstFrame = false;

		}

		void FlashControl::createOverlay(unsigned int width,unsigned int height,unsigned short zOrder,unsigned short zTier, bool IsTransparent)
		{
			if(_HasOverlay)
			{
				Console::WriteLine("An overlay already exist for the control"+_Name+" Destroy it first");
				return;
			}

			_Width = width;
			_Height = height;
			_texWidth = _Width;
			_texHeight = height;
			
			if(!Bitwise::isPO2(width) || !Bitwise::isPO2(height))
			{
				
				if(Mogre::Root::Singleton->RenderSystem->Capabilities->HasCapability(Mogre::Capabilities::RSC_NON_POWER_OF_2_TEXTURES))
				{
					if(Mogre::Root::Singleton->RenderSystem->Capabilities->NonPOW2TexturesLimited)
						compensateNPOT = true;
				}
				else compensateNPOT = true;
				
				if(compensateNPOT)
				{
					_texWidth = Bitwise::firstPO2From(width);
					_texHeight = Bitwise::firstPO2From(height);
				}
			}
				
			_Tier = zTier;
			_zOrder = zOrder;

			Mogre::PixelFormat texturePF = (IsTransparent)? Mogre::PixelFormat::PF_BYTE_BGRA : Mogre::PixelFormat::PF_BYTE_BGR;

			_WebTexture = Mogre::TextureManager::Singleton->CreateManual(_TextureName,Mogre::ResourceGroupManager::DEFAULT_RESOURCE_GROUP_NAME,
				Mogre::TextureType::TEX_TYPE_2D,_texWidth,_texHeight,0, texturePF,(int)Mogre::TextureUsage::TU_DYNAMIC_WRITE_ONLY_DISCARDABLE);
					

				Ogre::TexturePtr nativeTexPtr = (Ogre::TexturePtr)_WebTexture;

				Ogre::HardwarePixelBufferSharedPtr pixelBuffer = nativeTexPtr->getBuffer();
				pixelBuffer->lock(Ogre::HardwareBuffer::HBL_DISCARD);
				const Ogre::PixelBox& pixelBox = pixelBuffer->getCurrentLock();
				size_t dstBpp = Ogre::PixelUtil::getNumElemBytes(pixelBox.format);
				size_t dstPitch = pixelBox.rowPitch * dstBpp;

				Ogre::uint8* dstData = static_cast<Ogre::uint8*>(pixelBox.data);
				memset(dstData, 255, dstPitch * height);
				pixelBuffer->unlock();

				_WebMaterial = static_cast<Mogre::MaterialPtr^>(Mogre::MaterialManager::Singleton->Create(_MaterialName, Mogre::ResourceGroupManager::DEFAULT_RESOURCE_GROUP_NAME));
				
				Mogre::Pass^ pass = _WebMaterial->GetTechnique(0)->GetPass(0);
				pass->DepthCheckEnabled = false;
				pass->DepthWriteEnabled = false;
				pass->LightingEnabled = false;
				pass->SetSceneBlending(Mogre::SceneBlendType::SBT_TRANSPARENT_ALPHA);

				_TexUnit = pass->CreateTextureUnitState(_TextureName);
				_TexUnit->SetTextureFiltering(Mogre::FilterOptions::FO_NONE,Mogre::FilterOptions::FO_NONE,Mogre::FilterOptions::FO_NONE);


				Mogre::OverlayManager^ overlayManager = Mogre::OverlayManager::Singleton;

				_Panel = overlayManager->CreateOverlayElement("Panel",_Name+"Panel");
				_Panel->MetricsMode = Mogre::GuiMetricsMode::GMM_PIXELS;
				_Panel->MaterialName = _MaterialName;
				_Panel->SetDimensions(width,height);
				_Panel->SetPosition(0,0);
				
				if(compensateNPOT)
					((Mogre::PanelOverlayElement^)_Panel)->SetUV(0, 0, (Real)width/(Real)_texWidth, (Real)height/(Real)_texHeight);

				_Overlay = overlayManager->Create(_Name+"Overlay");
				_Overlay->Add2D(static_cast<Mogre::OverlayContainer^>(_Panel));
				_Overlay->ZOrder = (100 * _Tier) + _zOrder;
				_Overlay->Show();
				_HasOverlay = true;
		}
		


		void FlashControl::Play()
		{
		}

		void FlashControl::Stop()
		{
		}

		void FlashControl::Rewind()
		{
		}

		void FlashControl::GotoFrame(long FrameNum)
		{
		}

		void FlashControl::SetTransparent(bool bMakeTransparent)
		{
			float width = _Panel->Width;
			float height = _Panel->Height;
			this->Destroy();
			_NativeControl->setTranparent(bMakeTransparent);
			this->createOverlay((unsigned int)width,(unsigned int)height,_zOrder,_Tier, bMakeTransparent);
			_IsDestroyed = false;
			
		}

		void FlashControl::SetLoop(bool ShouldLoop)
		{
		}

		void FlashControl::SetQuality(Makarui::RenderQuality RenderQuality)
		{
		}

		void FlashControl::SetScaleMode(Makarui::ScaleMode ScaleMode)
		{

		}

		void FlashControl::SetDraggable(bool IsDraggable)
		{
		}

		void FlashControl::SetIgnoreTransparentPixels(bool ShouldIgnore, float Threshold)
		{
		}

		void FlashControl::CallFunction(System::String^ FuncName)
		{
			this->CallFunction(FuncName, gcnew array<System::Object^>(0));
		}

		void FlashControl::CallFunction(System::String^ FuncName,array<System::Object^>^ Args)
		{
			Ogre::String NativefuncName;
			Utilities::GetNativeString(NativefuncName,FuncName);

			Akarui::FlashArguments NativeArgs;

			for each(System::Object^ val in Args)
			{
				if(val->GetType() == System::Int32::typeid)
				{
					Akarui::FlashValue cVal((int)val);
					NativeArgs.push_back(cVal);
				}

				if(val->GetType() == System::Boolean::typeid)
				{
					Akarui::FlashValue cVal((bool)val);
					NativeArgs.push_back(cVal);
				}

				if(val->GetType() == System::Single::typeid || val->GetType() == System::Double::typeid)
				{
					Akarui::FlashValue cVal(Convert::ToDouble(val));
					NativeArgs.push_back(cVal);
				}

				if(val->GetType() == System::String::typeid)
				{
					Ogre::DisplayString mStr;
					Utilities::GetNativeUnicodeString(mStr,(System::String^)val);
					Akarui::FlashValue cVal(mStr.asUTF8_c_str());
					NativeArgs.push_back(cVal);
				}
			}

			_NativeControl->callFunction(NativefuncName.c_str(),NativeArgs);
		}

		void FlashControl::Bind(System::String^ FuncName,CallbackDelegate^ CallDelegate)
		{
			if(FuncName == "isdirty" || FuncName == "isclean")
			{
				Console::WriteLine("isdirty and isclean are reserved name, please use another function callback name");
				return;
			}

			Ogre::String nativeFuncName;
			Utilities::GetNativeString(nativeFuncName,FuncName);

			_NativeCallbackDelegate->AddFunctionDelegate(nativeFuncName,CallDelegate);
		}

		void FlashControl::Unbind(System::String^ FuncName)
		{
			Ogre::String nativeFuncName;
			Utilities::GetNativeString(nativeFuncName,FuncName);

			_NativeCallbackDelegate->RemoveFunctionDelegate(nativeFuncName);
		}

		void FlashControl::SetOpacity(float OpacityValue)
		{
			if(_TexUnit)
				_TexUnit->SetAlphaOperation(Mogre::LayerBlendOperationEx::LBX_MODULATE, Mogre::LayerBlendSource::LBS_TEXTURE, Mogre::LayerBlendSource::LBS_MANUAL, 1, OpacityValue);
		}

		void FlashControl::SetFocus()
		{
		}

		void FlashControl::SetTopLeft()
		{
			_NativeControl->SetTopLeft((unsigned int)(_Viewport->ActualTop + _Panel->Top), (unsigned int)(_Viewport->ActualLeft +_Panel->Left));
		}

		void FlashControl::Hide()
		{
			if(_HasOverlay)
			{
				_Overlay->Hide();
				_Visible = false;
			}
		}
		void FlashControl::Show()
		{
			if(_HasOverlay)
			{
				_Overlay->Show();
				_Visible = true;
			}
		}

		void FlashControl::Move(int DeltaX, int DeltaY)
		{
			if(!_HasOverlay)
			{
				Console::WriteLine("Can't move a flash control without an overlay, use createOverlay() first");
				return;
			}
			
			_Panel->SetPosition(_Panel->Left+DeltaX, _Panel->Top+DeltaY);
		}

		//Coordinates MUST be in absolute position ! otherwise the flash plugin won't be able to track properly the mouse
		void FlashControl::InjectMouseMove(int XPos, int YPos)
		{
			_NativeControl->injectMouseMove(XPos, YPos);
		}

		//Coordinates MUST be in absolute position ! otherwise the flash plugin won't be able to handle properly the mouse click
		void FlashControl::InjectMouseDown(int XPos, int YPos)
		{
			_NativeControl->injectMouseDown(XPos, YPos);
		}

		//Coordinates MUST be in absolute position ! otherwise the flash plugin won't be able to handle properly the mouse up
		void FlashControl::InjectMouseUp(int XPos, int YPos)
		{
			_NativeControl->injectMouseUp(XPos, YPos);
		}

		void FlashControl::InjectMouseWheel(int RelScroll, int XPos, int YPos)
		{

		}

		void FlashControl::SetPosition(Makarui::RelativePosition RelPos,int OffsetX,int OffsetY)
		{
			int viewWidth = _Viewport->ActualWidth;
			int viewHeight = _Viewport->ActualHeight;

			int left = 0 + OffsetX;
			int center = (viewWidth/2)-(_Width/2) + OffsetX;
			int right = viewWidth - _Width + OffsetX;

			int top = 0 + OffsetY;
			int middle = (viewHeight/2)-(_Height/2) + OffsetY;
			int bottom = viewHeight-_Height + OffsetY;

			_RelPosition = RelPos;

			switch(RelPos)
			{
				case RelativePosition::Left:
					_Panel->SetPosition(left, middle);
					break;
				case RelativePosition::TopLeft:
					_Panel->SetPosition(left, top);
					break;
				case RelativePosition::TopCenter:
					_Panel->SetPosition(center, top);
					break;
				case RelativePosition::TopRight:
					_Panel->SetPosition(right, top);
					break;
				case RelativePosition::Right:
					_Panel->SetPosition(right, middle);
					break;
				case RelativePosition::BottomRight:
					_Panel->SetPosition(right, bottom);
					break;
				case RelativePosition::BottomCenter:
					_Panel->SetPosition(center, bottom);
					break;
				case RelativePosition::BottomLeft:
					_Panel->SetPosition(left, bottom);
					break;
				case RelativePosition::Center:
					_Panel->SetPosition(center, middle);
					break;
				default:
					_Panel->SetPosition(OffsetX, OffsetY);
					break;
			}

			this->SetTopLeft();
		}

		void FlashControl::SetPosition(int AbsX,int AbsY)
		{
			_Panel->SetPosition(AbsX, AbsY);
		}

		void FlashControl::ResetPosition()
		{
		
		}

		void FlashControl::GetCoordinates([Out] int% x,[Out] int% y)
		{
			if(_HasOverlay)
			{
				x = _Viewport->ActualLeft + _Panel->Left;
				y = _Viewport->ActualTop + _Panel->Top;
			}
		}

		int FlashControl::GetRelativeX(int absX)
		{
			return absX - _Viewport->ActualLeft - _Panel->Left;
		}

		int FlashControl::GetRelativeY(int absY)
		{
			return absY - _Viewport->ActualTop - _Panel->Top;
		}

		bool FlashControl::IsWithinBounds(int absX, int absY)
		{
			int localX = GetRelativeX(absX);
			int localY = GetRelativeY(absY);

			if(localX > 0 && localX < _Width)
				if(localY > 0 && localY < _Height)
					return true;

			return false;
		}

		bool FlashControl::IsPointOverMe(int screenX, int screenY)
		{
			if(_NoEvent)
				return false;

			if(!_HasOverlay)
				return false;

			if(!_Visible)
				return false;

			if(this->IsWithinBounds(screenX, screenY))
			{
					return true;
			}

			return false;
		}


		void FlashControl::GetExtents([Out] unsigned short% width,[Out] unsigned short% height)
		{
			width = _Width;
			height = _Height;
		}

		void FlashControl::GetUVScale([Out] float% uScale,[Out] float% vScale)
		{
			uScale = vScale = 1;

			if(compensateNPOT)
			{
				uScale = (Real)_Width/(Real)_texWidth;
				vScale = (Real)_Height/(Real)_texHeight;
			}
		}


}