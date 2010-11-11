#ifndef __precompiled_H__
#define __precompiled_H__

#include <sstream>
#include <string>
#include <map>
#include <queue>
#include <iostream>
#include <fstream>
#include <assert.h>
#include <vector>
#include <windows.h>
#include <cctype>

namespace Akarui 
{
	class FlashValue;
	typedef std::vector<FlashValue> FlashArguments;
}

#include "npfunctions.h"

typedef NPError(OSCALL * NP_InitializeFuncA)(NPNetscapeFuncs* pFuncs);
typedef NPError(OSCALL * NP_GetEntryPointsFuncA)(NPPluginFuncs* pFuncs);
typedef NPError(OSCALL * NP_ShutdownFuncA)(void);

#include "AkaruiPlatform.h"
#include "FlashValue.h"
#include "FlashOptions.h"
#include "FlashMovie.h"
#include "PluginInstance.h"
#include "PluginHost.h"
#include "IdentifierStore.h"
#include "ScriptObject.h"
#include "ScriptWindow.h"
#include "InternalUtils.h"
#include "FlashHandler.h"
#include "RenderBuffer.h"
#include "InternalUtils.h"



#endif