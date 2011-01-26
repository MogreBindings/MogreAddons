#include "Makarui.h"


namespace Makarui
{
	public delegate void CallbackDelegate(array<System::Object^>^ args);
		
	public class NativeCallbackDelegate : public FlashHandler
	{
		private:
			gcroot<Dictionary<System::String^,CallbackDelegate^>^> _ManagedDelegate;

		public:
			NativeCallbackDelegate();
			~NativeCallbackDelegate();

			FlashValue onFlashCall(const std::string& funcName, const FlashArguments& args);

			void AddFunctionDelegate(Ogre::String funcName, CallbackDelegate^ pDel);

			void RemoveFunctionDelegate(Ogre::String funcName);
	};


	public ref class FlashControl
	{
		private:
			System::String^ _Name;
			Akarui::FlashMovie* _NativeControl;
			System::String^ _TextureName;
			System::String^ _MaterialName; 
			Mogre::TexturePtr^ _WebTexture;
			Mogre::TextureUnitState^ _TexUnit;
			Mogre::MaterialPtr^ _WebMaterial;
			Mogre::OverlayElement^ _Panel;
			Mogre::Overlay^ _Overlay;
			bool _Visible;
			unsigned short _Tier;
			unsigned short _zOrder;
			bool _HasOverlay;
			NativeCallbackDelegate* _NativeCallbackDelegate;
			bool _IsDestroyed;
			bool _ForceRedraw;
			bool _FirstFrame;
			Mogre::Viewport^ _Viewport;
			bool compensateNPOT;
			unsigned int _Width;
			unsigned int _Height;
			unsigned int _texWidth;
			unsigned int _texHeight;
			Makarui::RelativePosition _RelPosition;
			

		public protected:
			FlashControl(Akarui::FlashMovie* ofFlashMovie,System::String^ pName,Mogre::Viewport^ pViewport);

			~FlashControl();

			void Destroy();

			//internal function to draw a movie
			void Draw();

			void createOverlay(unsigned int width,unsigned int height,unsigned short zOrder,unsigned short zTier, bool IsTransparent);

			Akarui::FlashMovie* GetNative()
			{
				return _NativeControl;
			}

		public:

			property bool ForceRedraw
			{
				bool get()
				{
					return _ForceRedraw;
				}
				void set(bool value)
				{
					_ForceRedraw = value;
				}
			}

			property bool FirstFrame
			{
				bool get()
				{
					return _FirstFrame;
				}
				void set(bool value)
				{
					_FirstFrame = value;
				}
			}

			property Akarui::FlashMovie* NativeControl
			{
				Akarui::FlashMovie* get()
				{
					return _NativeControl;
				}
			}

			property bool IsDestroyed
			{
				bool get()
				{
					return _IsDestroyed;
				}
			}

			property unsigned short Tier
			{
				unsigned short get()
				{
					return _Tier;
				}
			}

			property unsigned short zOrder
			{
				unsigned short get()
				{
					return _zOrder;
				}
			}
			
			property bool HasOverlay
			{
				bool get()
				{
					return _HasOverlay;
				}
			}

			property Mogre::Overlay^ Overlay
			{
				Mogre::Overlay^ get()
				{
					return _Overlay;
				}
			}

			property System::String^ Name
			{
				System::String^ get()
				{
					return _Name;
				}
			}

			property System::String^ MaterialName
			{
				System::String^ get()
				{ 
					return _MaterialName;
				}
			}

			property bool Visible
			{
				bool get()
				{
					return _Visible;
				}
				void set(bool value)
				{
					_Visible = value;
				}
			}


			void Play();

			void Stop();

			void Rewind();

			void GotoFrame(long FrameNum);

			void SetTransparent(bool bMakeTransparent);

			void SetLoop(bool ShouldLoop);

			void SetQuality(Makarui::RenderQuality RenderQuality);

			void SetScaleMode(Makarui::ScaleMode ScaleMode);

			void SetDraggable(bool IsDraggable);

			void SetIgnoreTransparentPixels(bool ShouldIgnore, float Threshold);

			void CallFunction(System::String^ FuncName,array<System::Object^>^ Args);

			void Bind(System::String^ FuncName,CallbackDelegate^ CallDelegate);

			void Unbind(System::String^ FuncName);

			void SetOpacity(float OpacityValue);

			void SetFocus();

			void Move(int DeltaX, int DeltaY);

			void InjectMouseMove(int XPos, int YPos);

			void InjectMouseDown(int XPos, int YPos);

			void InjectMouseUp(int XPos, int YPos);

			void InjectMouseWheel(int RelScroll, int XPos, int YPos);

			void SetPosition(Makarui::RelativePosition RelPos,int OffsetX,int OffsetY);

			void SetPosition(int AbsX,int AbsY);

			void ResetPosition();

			void Hide();

			void Show();

			void GetCoordinates([Out] int% x,[Out] int% y);

			void GetExtents([Out] unsigned short% width,[Out] unsigned short% height);

			void GetUVScale([Out] float% uScale,[Out] float% vScale);

			int GetRelativeX(int absX);

			int GetRelativeY(int absY);

			bool IsWithinBounds(int absX, int absY);

			bool IsPointOverMe(int screenX, int screenY);

	};




}