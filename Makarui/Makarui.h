// Makarui.h



#pragma once

#ifndef __INCLUDE_MAKARUI_PREREQ__
#define __INCLUDE_MAKARUI_PREREQ__

#if _DEBUG
	#using "Mogre_d.dll"
#else
	#using "Mogre.dll"
#endif

#using "MOIS.dll"

#include "Akarui.h"
#include "Ogre.h"
#include "OgreBitwise.h"
#include "OIS.h"
#include <gcroot.h>
#include <direct.h>
#include "Utilities.h"

using namespace System;
using namespace System::Runtime::InteropServices;
using namespace System::Text;
using namespace Akarui;
using namespace Ogre;
using namespace OIS;
using namespace Mogre;
using namespace MOIS;
using namespace System::Collections::Generic;


#endif 



