#include "Makarui.h"
#include "FlashControl.h"
#include "KeyboardHook.h"

namespace Makarui
{

	ref class MakaruiManager;

	public class KeyboardListener : public HookListener
	{
		private:
			gcroot<MakaruiManager^> _ManagedMakaruiMgr;
		public:
			void handleKeyMessage(HWND hwnd, UINT msg, WPARAM wParam, LPARAM lParam);
			void SetManager(MakaruiManager^ pManagedMakaruiMgr);
	};

	public ref class CompareZorder: public IComparer<FlashControl^>
	{
		private:

		   // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
		   virtual int Compare( FlashControl^ x, FlashControl^ y ) sealed = IComparer<FlashControl^>::Compare
		   {
			   return (x->Overlay->ZOrder -  y->Overlay->ZOrder);
		   }
	};

	public ref class MakaruiManager
	{
		private:
			Akarui::AkaruiManager* NativeManager;
			KeyboardListener* _KeyboardListener;
			KeyboardHook* _keyboardHook;
			Dictionary<System::String^,FlashControl^>^ _ControlDictionary;
			Makarui::FlashControl^ _FocusedControl;
			bool _IsDestroyed;
			bool _mouseButtonRDown;

			void MakaruiManager::DefocusAll();
		public:
			MakaruiManager(System::String^ AssetsDirectory,Mogre::RenderWindow^ oRenderWindow);

			~MakaruiManager();

			void Destroy();

		public:

			property bool IsDestroyed
			{
				bool get()
				{
					return _IsDestroyed;
				}
			}

			property Makarui::FlashControl^ FocusedControl
			{
				Makarui::FlashControl^ get()
				{
					return _FocusedControl;
				}
			}

			property Dictionary<System::String^,FlashControl^>^ ControlDictionary
			{
				Dictionary<System::String^,FlashControl^>^ get()
				{
					return _ControlDictionary;
				}
			}

			std::string getCurrentWorkingDirectory();

			Makarui::FlashControl^ MakaruiManager::CreateFlashOverlay(System::String^ Name,Mogre::Viewport^ oViewport,int Width,int Height,unsigned short zOrder,unsigned short zTier,bool IsTransparent);

			Makarui::FlashControl^ CreateFlashOverlay(System::String^ Name,Mogre::Viewport^ oViewport,int Width,int Height,unsigned short zOrder,unsigned short zTier,bool IsTransparent,Makarui::ScaleMode sMode);

			Makarui::FlashControl^ MakaruiManager::CreateFlashOverlay(System::String^ Name,Mogre::Viewport^ oViewport,int Width,int Height,unsigned short zOrder,unsigned short zTier,bool IsTransparent,Makarui::ScaleMode sMode, Makarui::RenderQuality sRenderQuality);

			Makarui::FlashControl^ CreateFlashMaterial(System::String^ Name, int Width, int Height,bool IsTransparent);

			void DestroyFlashControl(Makarui::FlashControl^ ofControl);

			void Update();

			bool InjectMouseMove(int X,int Y);

			bool InjectMouseDown(MOIS::MouseEvent^ arg,MOIS::MouseButtonID ID);
			
			bool InjectMouseUp(MOIS::MouseEvent^ arg,MOIS::MouseButtonID ID);
			
			bool IsAnyFocused();

			Makarui::FlashControl^ MakaruiManager::GetTopControl(int x, int y);

			bool FocusControl(int X,int Y,Makarui::FlashControl^ ofSelection);

			
	};



}