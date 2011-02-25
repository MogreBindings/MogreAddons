/*
	This file is a part of Akarui, a library that makes it easy for developers 
	to embed Flash-content in their applications.

	Copyright (C) 2009 Adam J. Simmons

	Project Website:
	<http://princeofcode.com/akarui.php>

	This library is free software; you can redistribute it and/or
	modify it under the terms of the GNU Lesser General Public
	License as published by the Free Software Foundation; either
	version 2.1 of the License, or (at your option) any later version.

	This library is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
	Lesser General Public License for more details.

	You should have received a copy of the GNU Lesser General Public
	License along with this library; if not, write to the Free Software
	Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 
	02110-1301 USA
*/

#include "ScriptWindow.h"


ScriptWindow::ScriptWindow(const std::string& location, PluginInstance* owner) : location(location), owner(owner)
{
	npObject = reinterpret_cast<NPObjectWrapper*>(NPN_CreateObject(0, NPObjectWrapperClass));
	npObject->scriptObject.setMethodHandler(this);

	NPObjectWrapper* locationObject = reinterpret_cast<NPObjectWrapper*>(NPN_CreateObject(0, NPObjectWrapperClass));
	locationObject->scriptObject.setMethodHandler(this);

	NPVariant locationVal;
	OBJECT_TO_NPVARIANT((NPObject*)locationObject, locationVal);
	npObject->scriptObject.setProperty(NPN_GetStringIdentifier("location"), &locationVal);

	NPN_ReleaseObject((NPObject*)locationObject);
}

ScriptWindow::~ScriptWindow()
{
	NPN_ReleaseObject((NPObject*)npObject);
}

NPObject* ScriptWindow::getNPObject()
{
	return (NPObject*)npObject;
}

ScriptObject* ScriptWindow::getScriptObject()
{
	return &(npObject->scriptObject);
}

std::string ScriptWindow::getLocation()
{
	return location;
}

bool ScriptWindow::handleInvocation(NPIdentifier id, const NPVariant *args, int argCount, NPVariant* result)
{
	static const NPIdentifier locationToStringID = NPN_GetStringIdentifier("toString");
	static const NPIdentifier flashRequestID = NPN_GetStringIdentifier("__flash__request");

	if(id == locationToStringID)
	{
		unsigned int len = (unsigned int)location.length();
		char* buffer = reinterpret_cast<char*>(NPN_MemAlloc(len));
		memcpy(buffer, location.c_str(), len);

		/*result->type = NPVariantType_String;
		result->value.stringValue.UTF8Length = len;
		result->value.stringValue.UTF8Characters = buffer;*/
		return true;
	}
	else if(id == flashRequestID)
	{
		std::wstring request = translateFlashRequest(args, argCount);

		unsigned int len = (unsigned int)request.length();
		wchar_t* buffer = reinterpret_cast<wchar_t*>(NPN_MemAlloc(len));
		memcpy(buffer, request.c_str(), len);

		/*result->type = NPVariantType_String;
		result->value.stringValue.UTF8Length = len;
		result->value.stringValue.UTF8Characters = buffer;*/

		#ifdef _DEBUG
			printf("Flash Request: %s\n", request.c_str());
		#endif
		
		return true;
	}

	return true;
}

bool ScriptWindow::handleEvaluation(NPString *script, NPVariant *result)
{
	std::wstring js;
	js = utf8ToWChar(script->UTF8Characters, script->UTF8Length);

	if(js.substr(0, 8) == L"function")
	{
		VOID_TO_NPVARIANT(*result);
		return true;
	}
	else if(js.substr(0, 21) == L"try { __flash__toXML(")
	{
		std::wstring::size_type suffix = js.find(L") ; } catch", 21);

		if(suffix == std::wstring::npos)
			return false;

		std::wstring call = js.substr(21, suffix - 21);

		#ifdef _DEBUG
			printf("JS Call: %s\n", call.c_str());
		#endif

		std::wstring funcName;
		FlashArguments args;

		if(parseJSCall(call, funcName, args))
		{
			FlashValue flashValueResult = owner->handleFlashCall(funcName, args);
			NPVariant variantResult;
			FlashValueToNPVariant(flashValueResult, variantResult);
			std::wstringstream stream;
			variantToXML(variantResult, stream);
			NPN_ReleaseVariantValue(&variantResult);

			/*unsigned int len = (unsigned int)stream.str().length();
			wchar_t* buffer = reinterpret_cast<wchar_t*>(NPN_MemAlloc(len));
			memcpy(buffer, stream.str().c_str(), len);

			result->type = NPVariantType_String;
			result->value.stringValue.UTF8Length = len;
			result->value.stringValue.UTF8Characters = buffer;*/
		}
		else
		{
			VOID_TO_NPVARIANT(*result);
		}

		return true;
	}

	return false;
}