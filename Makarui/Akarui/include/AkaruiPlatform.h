#ifndef __AkaruiPlatform_H__
#define __AkaruiPlatform_H__

#if defined(AKARUI_NONCLIENT_BUILD)
#	define _AkaruiExport __declspec( dllexport )
#else
#	define _AkaruiExport __declspec( dllimport )
#endif

#endif