
namespace Makarui
{
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

	// Utilities *********************************************************************************************
	public value class Utilities
	{
		public:
			static Makarui::RenderQuality ToM_RenderQuality(Akarui::RenderQuality M_RenderQuality);
			static Makarui::ScaleMode ToM_ScaleMode(Akarui::ScaleMode UM_ScaleMode);

			static Akarui::ScaleMode ToUM_ScaleMode(Makarui::ScaleMode M_ScaleMode);
			static Akarui::RenderQuality ToUM_RenderQuality(Makarui::RenderQuality M_RenderQuality);

			static void GetNativeString(Ogre::String& UnmanagedString,System::String^ ManagedString);
			static void GetNativeUnicodeString(Ogre::DisplayString& UnmanagedString,System::String^ ManagedString);
			static Mogre::ColourValue GetNumberAsColor(float ColorVal);
			static wchar_t* utf8ToWChar(const char *utf8, size_t len) ;
	};
}
