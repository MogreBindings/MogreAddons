#include "MakaruiManager.h"

namespace Makarui
{

	void KeyboardListener::handleKeyMessage(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam)
	{
		if(_ManagedMakaruiMgr->FocusedControl != nullptr)
			_ManagedMakaruiMgr->FocusedControl->NativeControl->injectKeyboardEvent(msg, wParam, lParam);
	}

	void KeyboardListener::SetManager(MakaruiManager^ pManagedMakaruiMgr)
	{
		_ManagedMakaruiMgr = pManagedMakaruiMgr;
	}

	MakaruiManager::MakaruiManager(System::String^ AssetsDirectory,Mogre::RenderWindow^ oRenderWindow)
	{

		_ControlDictionary = gcnew Dictionary<System::String^,FlashControl^>();

		Ogre::String NativeAssetsDirectory;
		Utilities::GetNativeString(NativeAssetsDirectory,AssetsDirectory);
		Ogre::RenderWindow* nativeRenderwindow = (Ogre::RenderWindow*)oRenderWindow;
		HWND winHandle;
		nativeRenderwindow->getCustomAttribute("WINDOW", (void*)&winHandle);

		_KeyboardListener = new KeyboardListener();
		_KeyboardListener->SetManager(this);
		_keyboardHook = new KeyboardHook(_KeyboardListener);
	
		NativeManager = new Akarui::AkaruiManager(getCurrentWorkingDirectory()+NativeAssetsDirectory.c_str(),winHandle);
		_IsDestroyed = false;
	}



	MakaruiManager::~MakaruiManager()
	{
		if(!_IsDestroyed)
			this->Destroy();

		if(_keyboardHook)
			delete _keyboardHook;

		if(_KeyboardListener)
			delete _KeyboardListener;

		if(NativeManager)
		{
			delete NativeManager;
			NativeManager = NULL;
		}
	}

	void MakaruiManager::Destroy()
	{
		for each(KeyValuePair<System::String^,FlashControl^> KVFlash in _ControlDictionary)
			KVFlash.Value->Destroy();

		_ControlDictionary->Clear();

		_IsDestroyed = true;

	}



	std::string MakaruiManager::getCurrentWorkingDirectory()
	{
		std::string workingDirectory = "";
		char currentPath[_MAX_PATH];
		getcwd(currentPath, _MAX_PATH);
		workingDirectory = currentPath;

		return workingDirectory + "\\";
	}

	Makarui::FlashControl^ MakaruiManager::CreateFlashOverlay(System::String^ Name,Mogre::Viewport^ oViewport,int Width,int Height,unsigned short zOrder,unsigned short zTier,bool IsTransparent)
	{
		return this->CreateFlashOverlay(Name,oViewport,Width,Height,zOrder,zTier,IsTransparent,Makarui::ScaleMode::SHOWALL,Makarui::RenderQuality::MEDIUM);
	}

	Makarui::FlashControl^ MakaruiManager::CreateFlashOverlay(System::String^ Name,Mogre::Viewport^ oViewport,int Width,int Height,unsigned short zOrder,unsigned short zTier,bool IsTransparent,Makarui::ScaleMode sMode)
	{
		return this->CreateFlashOverlay(Name,oViewport,Width,Height,zOrder,zTier,IsTransparent,sMode,Makarui::RenderQuality::MEDIUM);
	}

	Makarui::FlashControl^ MakaruiManager::CreateFlashOverlay(System::String^ Name,Mogre::Viewport^ oViewport,int Width,int Height,unsigned short zOrder,unsigned short zTier,bool IsTransparent,Makarui::ScaleMode sMode, Makarui::RenderQuality sRenderQuality)
	{
		Ogre::String NativeCtrlName;
		Utilities::GetNativeString(NativeCtrlName,Name);

		if(zOrder == 0)
		{
			int highestZOrder = -1;

			for each(KeyValuePair<System::String^,FlashControl^> KVFlash in _ControlDictionary)
			{
				if(KVFlash.Value->HasOverlay)
					if(KVFlash.Value->Tier == zTier)
						if(KVFlash.Value->Overlay->ZOrder > highestZOrder)
							highestZOrder = KVFlash.Value->Overlay->ZOrder;
			}

			if(highestZOrder == -1)
				zOrder = 0;
			else
				zOrder = highestZOrder + 1;
		}


		Akarui::FlashOptions argVal;
		argVal.renderQuality = Utilities::ToUM_RenderQuality(sRenderQuality);
		argVal.scaleMode = Utilities::ToUM_ScaleMode(sMode);

		FlashControl^ ManagedCtrl = gcnew FlashControl(Name,oViewport);
		ManagedCtrl->createOverlay(Width,Height,zOrder,zTier, IsTransparent);

		Akarui::FlashMovie* nativeCtrl = NativeManager->createFlashMovie(NativeCtrlName,Width,Height,IsTransparent,argVal, (Ogre::TexturePtr)ManagedCtrl->WebTexture, (unsigned int)ManagedCtrl->Panel->Left, (unsigned int)ManagedCtrl->Panel->Top);

		ManagedCtrl->SetNativeControl(nativeCtrl);

		_ControlDictionary->Add(Name,ManagedCtrl);
		return ManagedCtrl;
	}

	Makarui::FlashControl^	MakaruiManager::CreateFlashMaterial(System::String^ Name, int Width, int Height,bool IsTransparent)
	{
		/*Ogre::String NativeCtrlName;
		Utilities::GetNativeString(NativeCtrlName,Name);
	
		Akarui::FlashMovie* nativeCtrl = NativeManager->createFlashMovie(NativeCtrlName,Width,Height,IsTransparent,Akarui::FlashOptions());
		FlashControl^ ManagedCtrl = gcnew FlashControl(nativeCtrl,Name,nullptr);

		_ControlDictionary->Add(Name,ManagedCtrl);*/
		return nullptr;
	}


	void MakaruiManager::DestroyFlashControl(Makarui::FlashControl^ ofControl)
	{
		if(!_ControlDictionary->ContainsKey(ofControl->Name))
			return;

		ofControl->Destroy();
		_ControlDictionary->Remove(ofControl->Name);
	}

	void MakaruiManager::Update()
	{
		NativeManager->update();

		for each(KeyValuePair<System::String^,FlashControl^> KVFlash in _ControlDictionary)
		{
			if(KVFlash.Value->Visible)
				KVFlash.Value->Draw();
		}
	}

	bool  MakaruiManager::InjectMouseMove(int X,int Y)
	{
		bool eventHandled = false;

		for each(KeyValuePair<System::String^,FlashControl^> KVFlash in _ControlDictionary)
		{
			if(KVFlash.Value->HasOverlay && KVFlash.Value->Visible && !KVFlash.Value->NoEvent)
				KVFlash.Value->InjectMouseMove(KVFlash.Value->GetRelativeX(X),KVFlash.Value->GetRelativeY(Y));

			if(!eventHandled)
				if(KVFlash.Value->IsPointOverMe(X,Y))
					eventHandled = true;
		}
				
		return eventHandled;
	}

	bool  MakaruiManager::InjectMouseDown(MOIS::MouseEvent^ arg, MOIS::MouseButtonID ID)
	{
		if(ID == MOIS::MouseButtonID::MB_Left)
		{
			if(FocusControl(arg->state.X.abs,arg->state.Y.abs,nullptr))
			{
				int relX = _FocusedControl->GetRelativeX(arg->state.X.abs);
				int relY = _FocusedControl->GetRelativeY(arg->state.Y.abs);

				_FocusedControl->InjectMouseDown(relX,relY);
			}
		}
		else if(ID == MOIS::MouseButtonID::MB_Right)
		{
				FocusControl(arg->state.X.abs,arg->state.Y.abs,nullptr);
				_mouseButtonRDown = true;
		}

		if(_FocusedControl != nullptr)
			return true;

		return false;
	}
	
	bool  MakaruiManager::InjectMouseUp(MOIS::MouseEvent^ arg,MOIS::MouseButtonID ID)
	{
		if(_FocusedControl == nullptr)
			return false;

		if(ID == MOIS::MouseButtonID::MB_Left)
		{
			if(_FocusedControl->HasOverlay)
				_FocusedControl->InjectMouseUp(_FocusedControl->GetRelativeX(arg->state.X.abs),_FocusedControl->GetRelativeY(arg->state.Y.abs));
		}
		else if(ID ==  MOIS::MouseButtonID::MB_Right)
		{
			_mouseButtonRDown = false;
		}

		return true;
	}
	
	bool  MakaruiManager::IsAnyFocused()
	{
		return !!_FocusedControl;
	}

	void MakaruiManager::DefocusAll()
	{
		_FocusedControl = nullptr;
	}

	Makarui::FlashControl^ MakaruiManager::GetTopControl(int x, int y)
	{
		Makarui::FlashControl^ top = nullptr;

		for each(KeyValuePair<System::String^,FlashControl^> KVFlash in _ControlDictionary)
		{
			if(!KVFlash.Value->IsPointOverMe(x,y))
				continue;

			if(top == nullptr)
			{
				top = KVFlash.Value;
			}
			else
			{
				top = (top->Tier * 100 + top->zOrder > KVFlash.Value->Tier * 100 + KVFlash.Value->zOrder) ? top : KVFlash.Value;
			}
		}

		return top;
	}

	bool MakaruiManager::FocusControl(int X,int Y,Makarui::FlashControl^ ofSelection)
	{
		DefocusAll();

		if(ofSelection)
		{
			if(!ofSelection->HasOverlay)
			{
				_FocusedControl = ofSelection;
				return true;
			}
		}

		Makarui::FlashControl^ controlToFocus = ofSelection ? ofSelection : this->GetTopControl(X,Y);

		if(!controlToFocus)
			return false;

		List<FlashControl^>^ sortedControls = gcnew List<FlashControl^>(_ControlDictionary->Count);

		//get flash control of our tier
		for each(KeyValuePair<System::String^,FlashControl^> KVFlash in _ControlDictionary)
			if(KVFlash.Value->HasOverlay)
				if(KVFlash.Value->Tier == controlToFocus->Tier)
					sortedControls->Add(KVFlash.Value);

		IComparer<FlashControl^>^ myComparer = gcnew CompareZorder;

		sortedControls->Sort(myComparer);

		if(sortedControls[0] != controlToFocus)
		{
			unsigned int popIdx = 0;
			for(; popIdx < sortedControls->Count; popIdx++)
				if(sortedControls[popIdx] == controlToFocus)
					break;

			unsigned short highestZ = sortedControls[0]->Overlay->ZOrder;

			for(unsigned int i = 0; i < popIdx; i++)
				sortedControls[i]->Overlay->ZOrder = sortedControls[i+1]->Overlay->ZOrder;
			
			sortedControls[popIdx]->Overlay->ZOrder = highestZ;
		}

		_FocusedControl = controlToFocus;

		return true;
	}

	



}