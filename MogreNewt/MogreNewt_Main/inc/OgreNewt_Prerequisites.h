/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/

#ifndef __INCLUDE_OGRENEWT_PREREQ__
#define __INCLUDE_OGRENEWT_PREREQ__

#if _DEBUG
	#using "Mogre_d.dll"
#else
	#using "Mogre.dll"
#endif

using namespace System;
using namespace System::Runtime::InteropServices;


#include <Ogre.h>
#include "OgreFontManager.h"

#if OGRE_PLATFORM == OGRE_PLATFORM_WIN32
#   define _CDECL _cdecl
#   if defined( _OGRENEWT_EXPORTS ) && defined( _OGRENEWT_DYNAMIC )
#       define _OgreNewtExport __declspec( dllexport )
#   elif defined( _OGRENEWT_DYNAMIC )
#       define _OgreNewtExport __declspec( dllimport )
#   else
#       define _OgreNewtExport
#   endif
#else // Linux / Mac OSX etc
#   define _OgreNewtExport
#   define _CDECL
#endif

namespace Mogre
{
	typedef Ogre::Real Real;
}

#endif 

