#include "Makarui.h"

namespace Makarui
{
	// Enum convertors

	// To Managed Enums

	Makarui::ScaleMode Utilities::ToM_ScaleMode(Akarui::ScaleMode UM_ScaleMode)
	{
		return safe_cast<ScaleMode>(UM_ScaleMode);
	}

	Makarui::RenderQuality Utilities::ToM_RenderQuality(Akarui::RenderQuality UM_RenderQuality)
	{
		return safe_cast<RenderQuality>(UM_RenderQuality);
	}

	Akarui::ScaleMode Utilities::ToUM_ScaleMode(Makarui::ScaleMode M_ScaleMode)
	{
		return static_cast<Akarui::ScaleMode>(M_ScaleMode);
	}

	Akarui::RenderQuality Utilities::ToUM_RenderQuality(Makarui::RenderQuality M_RenderQuality)
	{
		return static_cast<Akarui::RenderQuality>(M_RenderQuality);
	}

	// *************************************************************************************************
	// getNumberAsColor(float ColorVal)
	// Gets the Ogre.ColourValue from passed float color
	Mogre::ColourValue Utilities::GetNumberAsColor(float ColorVal)
	{
		Mogre::ColourValue mColorVal;
		mColorVal.b = ((int)ColorVal % 256) / 255.0f;
		mColorVal.g = (((int)ColorVal / 256) % 256) / 255.0f;
		mColorVal.r = (((int)ColorVal / 256 / 256) % 256) / 255.0f;
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
}