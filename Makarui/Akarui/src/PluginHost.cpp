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

#include "PluginHost.h"

using namespace std;

extern "C" {

PluginInstance* getInstance(NPP id);

}

struct URLRequest
{
	NPP id;
	std::string url;
	int notifyData;
};

PluginHost* PluginHost::singleton = 0;

PluginHost::PluginHost() : totalAllocated(0)
{
	singleton = this;
}

PluginHost::~PluginHost()
{
	singleton = 0;
	assert(NP_Shutdown_() == NPERR_NO_ERROR);
	FreeLibrary(pluginLibrary);
}

void PluginHost::load()
{
	bindFunctions();

	if(!loadLibrary())
	{
		printf("Error loading Flash plugin ! ");
	}
}

void PluginHost::bindFunctions()
{
	memset(&hostFunctions, 9, sizeof(hostFunctions));
	hostFunctions.size = sizeof(hostFunctions);
	hostFunctions.version = (NP_VERSION_MAJOR << 8) | (NP_VERSION_MINOR);

	hostFunctions.geturl = &NPN_GetURL;
	hostFunctions.posturl = &NPN_PostURL;
	hostFunctions.requestread = &NPN_RequestRead;
	hostFunctions.newstream = &NPN_NewStream;
	hostFunctions.write = &NPN_Write;
	hostFunctions.destroystream = &NPN_DestroyStream;
	hostFunctions.status = &NPN_Status;
	hostFunctions.uagent = &NPN_UserAgent;
	hostFunctions.memalloc = &NPN_MemAlloc;
	hostFunctions.memfree = &NPN_MemFree;
	hostFunctions.memflush = &NPN_MemFlush;
	hostFunctions.reloadplugins = &NPN_ReloadPlugins;
	hostFunctions.geturlnotify = &NPN_GetURLNotify;
	hostFunctions.posturlnotify = &NPN_PostURLNotify;
	hostFunctions.getvalue = &NPN_GetValue;
	hostFunctions.setvalue = &NPN_SetValue;
	hostFunctions.invalidaterect = &NPN_InvalidateRect;
	hostFunctions.invalidateregion = &NPN_InvalidateRegion;
	hostFunctions.forceredraw = &NPN_ForceRedraw;

	hostFunctions.getstringidentifier = &NPN_GetStringIdentifier;
    hostFunctions.getstringidentifiers = &NPN_GetStringIdentifiers;
    hostFunctions.getintidentifier = &NPN_GetIntIdentifier;
    hostFunctions.identifierisstring = &NPN_IdentifierIsString;
    hostFunctions.utf8fromidentifier = &NPN_UTF8FromIdentifier;
    hostFunctions.intfromidentifier = &NPN_IntFromIdentifier;

    hostFunctions.createobject = &NPN_CreateObject;
    hostFunctions.retainobject = &NPN_RetainObject;
    hostFunctions.releaseobject = &NPN_ReleaseObject;
    hostFunctions.invoke = &NPN_Invoke;
    hostFunctions.invokeDefault = &NPN_InvokeDefault;
    hostFunctions.evaluate = &NPN_Evaluate;
    hostFunctions.getproperty = &NPN_GetProperty;
    hostFunctions.setproperty = &NPN_SetProperty;
    hostFunctions.removeproperty = &NPN_RemoveProperty;
    hostFunctions.hasproperty = &NPN_HasProperty;
    hostFunctions.hasmethod = &NPN_HasMethod;
	hostFunctions.setexception = &NPN_SetException;
	hostFunctions.releasevariantvalue = &NPN_ReleaseVariantValue;
}

bool PluginHost::loadLibrary()
{
	pluginLibrary = LoadLibrary("NPSWF32.dll");

	if(!pluginLibrary)
		return false;

	NP_Initialize_ = (NP_InitializeFuncA)GetProcAddress(pluginLibrary, "NP_Initialize");
    if(!NP_Initialize_)
      return false;

    NP_GetEntryPoints_ = (NP_GetEntryPointsFuncA)GetProcAddress(pluginLibrary, "NP_GetEntryPoints");
							
	if(!NP_GetEntryPoints_)
		return false;

    NP_Shutdown_ = (NP_ShutdownFuncA)GetProcAddress(pluginLibrary, "NP_Shutdown");
    if(!NP_Shutdown_)
      return false;

	pluginFunctions.size = sizeof(pluginFunctions);
    pluginFunctions.version = (NP_VERSION_MAJOR << 8) | NP_VERSION_MINOR;
    if(NP_GetEntryPoints_(&pluginFunctions) != NPERR_NO_ERROR)
      return false;

	if(NP_Initialize_(&hostFunctions) != NPERR_NO_ERROR)
		return false;

	return true;
}

PluginHost* PluginHost::getSingleton()
{
	return singleton;
}

NPNetscapeFuncs* PluginHost::hostFuncs()
{
	return &hostFunctions;
}

NPPluginFuncs* PluginHost::pluginFuncs()
{
	return &pluginFunctions;
}

void* PluginHost::allocateMem(unsigned int size)
{
	void* mem = malloc(size);
	allocationTracker[mem] = size;
	totalAllocated += size;

	#ifdef _DEBUG
		printf("+ [%d] %d bytes | %d\n", mem, size, totalAllocated);
	#endif
	//printf("+");
	return mem;
}

void PluginHost::deallocateMem(void* mem)
{
	std::map<void*, unsigned int>::iterator i = allocationTracker.find(mem);

	if(i != allocationTracker.end())
	{
		totalAllocated -= i->second;
		#ifdef _DEBUG
			printf("- [%d] %d bytes | %d\n", mem, i->second, totalAllocated);
		#endif
		allocationTracker.erase(mem);
		free(mem);
		//printf("-");
	}
}

std::string handleJavascriptRequest(NPP id, std::string request)
{
	request = request.substr(0, strlen("javascript:"));
	if(request.find("flashplugin_unique") != std::string::npos)
	{
		return getInstance(id)->getLocation() + "__flashplugin_unique__";
	}
	else
	{
		return "undefined";
	}
}

NPMIMEType getMimeTypeFromExtension(const std::string& ext)
{
	if(ext == "html" || ext == "htm" || ext == "shtml")	return "text/html";
	else if(ext == "php") return "text/php";
	else if(ext == "asp") return "text/asp";
	else if(ext == "xhtml") return "application/xhtml+xml";
	else if(ext == "xml") return "text/xml";
	else if(ext == "js") return "application/javascript";
	else if(ext == "cgi") return  "wwwserver/shellcgi";
	else if(ext == "css") return  "text/css";
	else if(ext == "swf") return "application/x-shockwave-flash";
	else if(ext == "mml") return "application/mathml+xml";
	else if(ext == "svg") return "image/svg+xml";
	else if(ext == "xslt") return "application/xslt+xml";
	else if(ext == "rss") return "application/rss+xml";
	else if(ext == "txt") return "text/plain";
	else if(ext == "rtf") return "text/rtf";
	else if(ext == "doc") return "application/doc";
	else if(ext == "gif") return "image/gif";
	else if(ext == "jpg" || ext == "jpeg") return "image/jpeg";
	else if(ext == "png") return "image/png";
	else if(ext == "tiff") return "image/tiff";
	else if(ext == "tga" || ext == "targa") return "image/tga";
	else if(ext == "bmp") return "image/bmp";
	else if(ext == "wbmp") return "image/vnd.wap.wbmp";
	else if(ext == "pdf") return "application/pdf";
	else if(ext == "flv") return "video/x-flv";
	else if(ext == "mpg" || ext == "mpeg") return "video/mpeg";
	else if(ext == "mp4") return "video/mp4";
	else if(ext == "mov") return "video/quicktime";
	else if(ext == "wmv") return "video/x-ms-wmv";
	else if(ext == "rm") return "application/vnd.rn-realmedia";
	else if(ext == "ogg") return "application/ogg";
	else if(ext == "mp3") return "audio/mpeg";
	else if(ext == "wav") return "audio/wav";
	else if(ext == "wma") return "audio/x-ms-wma";
	else if(ext == "asf") return "application/asx";
	else if(ext == "au") return "audio/au";
	else if(ext == "mid" || ext == "midi") return "audio/midi";
	else return "application/octet-stream";
}

void PluginHost::update()
{
	if(requests.size() == 0)
		return;

	URLRequest* request = requests.front();
	requests.pop();

	if(request->url.substr(0, strlen("javascript:")) == "javascript:")
	{
		std::string response = handleJavascriptRequest(request->id, request->url);
		int len = response.length() + 1;
		char* buffer = new char[len];
		buffer[0] = 0;
		strcpy(buffer, response.c_str());

		NPStream* stream = new NPStream();
		memset((void*)stream, 0, sizeof(NPStream));
		stream->url = request->url.c_str();
		stream->end = (uint32_t)len;
		stream->notifyData = (void*)request->notifyData;

		NPMIMEType mimetype = "text/html";
		unsigned short type = NP_NORMAL;

		PluginHost::getSingleton()->pluginFuncs()->newstream(request->id, mimetype, stream, false, &type);
		int maxLen = PluginHost::getSingleton()->pluginFuncs()->writeready(request->id, stream);
		PluginHost::getSingleton()->pluginFuncs()->write(request->id, stream, 0, len, buffer);

		PluginHost::getSingleton()->pluginFuncs()->urlnotify(request->id, request->url.c_str(), NPRES_DONE, (void*)request->notifyData);
		PluginHost::getSingleton()->pluginFuncs()->destroystream(request->id, stream, NPRES_DONE);

		delete[] buffer;
		delete stream;
	}
	else if(request->url.substr(0, strlen("http:")) == "http:")
	{
		// ignore HTTP requests
	}
	else
	{
		std::string url = getInstance(request->id)->getLocation() + request->url;
		std::string path = translateURLToPath(url);

		std::ifstream file(path.c_str(), std::ios::in|std::ios::binary|std::ios::ate);
		size_t size = file.tellg();

		NPStream* stream = new NPStream();
		memset((void*)stream, 0, sizeof(NPStream));
		stream->url = url.c_str();
		stream->end = (uint32_t)size;
		stream->notifyData = (void*)request->notifyData; 

		std::string ext;
		std::string::size_type idx = request->url.find_last_of(".");
		if(idx != std::string::npos)
			ext = request->url.substr(idx + 1);

		NPMIMEType mimetype = getMimeTypeFromExtension(ext);
		unsigned short type = NP_NORMAL;
		PluginHost::getSingleton()->pluginFuncs()->newstream(request->id, mimetype, stream, false, &type);

		int offset = 0;
		file.seekg(0, std::ios::beg);

		while(offset < (int)size)
		{
			int maxLen = PluginHost::getSingleton()->pluginFuncs()->writeready(request->id, stream);

			int len = (int)size - offset;
			len = len > maxLen ? maxLen : len;

			char* buffer = new char[len];
			file.read(buffer, len);

			PluginHost::getSingleton()->pluginFuncs()->write(request->id, stream, offset, len, buffer);

			delete[] buffer;

			offset += len;
		}

		PluginHost::getSingleton()->pluginFuncs()->destroystream(request->id, stream, NPRES_DONE);
		PluginHost::getSingleton()->pluginFuncs()->urlnotify(request->id, url.c_str(), NPRES_DONE, (void*)request->notifyData);

		file.close();

		delete stream;
	}

	delete request;
}

extern "C" {

PluginInstance* getInstance(NPP id) 
{
	return (PluginInstance*)id->ndata;
}

void* NPN_MemAlloc(uint32_t size)
{
	return PluginHost::getSingleton()->allocateMem(size);
}

void NPN_MemFree(void* ptr)
{
	PluginHost::getSingleton()->deallocateMem(ptr);
}

uint32_t NPN_MemFlush(uint32_t size)
{
	// This is not relevant on Windows; MAC specific
	return size;
}

void NPN_ReloadPlugins(NPBool reloadPages)
{
}

NPError NPN_RequestRead(NPStream* stream, NPByteRange* range_list)
{
	return NPERR_GENERIC_ERROR;
}

static NPError GetURLNotify(NPP id, const char* url, const char* target, bool notify, void* notify_data)
{
	return NPERR_NO_ERROR;
}

NPError NPN_GetURLNotify(NPP id, const char* url, const char* target, void* notify_data)
{
	#ifdef _DEBUG
		printf("geturlnotify, %s, %s\n", url, target);
	#endif

	URLRequest* request = new URLRequest();
	request->id = id;
	request->url = url;
	request->notifyData = (int)notify_data;

	PluginHost::getSingleton()->requests.push(request);

	return NPERR_NO_ERROR;
}

NPError  NPN_GetURL(NPP id, const char* url, const char* target)
{
	return NPERR_GENERIC_ERROR;
}

static NPError PostURLNotify(NPP id, const char* url, const char* target, uint32_t len, const char* buf, NPBool file, bool notify, void* notify_data)
{
	return NPERR_GENERIC_ERROR;
}

NPError  NPN_PostURLNotify(NPP id, const char* url, const char* target, uint32_t len, const char* buf, NPBool file, void* notify_data)
{
	return NPERR_GENERIC_ERROR;
}

NPError NPN_PostURL(NPP id, const char* url, const char* target, uint32_t len, const char* buf, NPBool file)
{
	return NPERR_GENERIC_ERROR;
}

NPError NPN_NewStream(NPP id, NPMIMEType type, const char* target, NPStream** stream)
{
	return NPERR_GENERIC_ERROR;
}

int32_t NPN_Write(NPP id, NPStream* stream, int32_t len, void* buffer)
{
	return NPERR_GENERIC_ERROR;
}

NPError NPN_DestroyStream(NPP id, NPStream* stream, NPReason reason)
{
	return NPERR_GENERIC_ERROR;
}

const char* NPN_UserAgent(NPP id)
{
	static const char* agent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9a1) Gecko/20061103 Firefox/2.0a1";
	return agent;
}

void NPN_Status(NPP id, const char* message)
{
}

void NPN_InvalidateRect(NPP id, NPRect *invalidRect)
{
	#ifdef _DEBUG
	//	printf("Invalidate a rect\n");
	#endif

	PluginInstance* plugin = getInstance(id);
	if(plugin)
		plugin->invalidateRect(*invalidRect);
}

void NPN_InvalidateRegion(NPP id, NPRegion invalidRegion)
{
	#ifdef _DEBUG
	//	printf("Invalidate a region");
	#endif
}

void NPN_ForceRedraw(NPP id)
{
}

NPError NPN_GetValue(NPP id, NPNVariable variable, void *value)
{
	if(variable == NPNVnetscapeWindow)
	{
		PluginInstance* plugin = getInstance(id);

		#ifdef _DEBUG
			printf("GET native window\n");
		#endif

		*((void**)value) = plugin->getNativeWindow();
		return NPERR_NO_ERROR;
	}
	else if(variable == NPNVWindowNPObject)
	{
		ScriptWindow* window = getInstance(id)->getScriptWindow();

		NPN_RetainObject(window->getNPObject());

		void **v = (void **)value;
		*v = window->getNPObject();
		return NPERR_NO_ERROR;
	}

	#ifdef _DEBUG
		printf("GET UNKNOWN %d\n", (int)variable);
	#endif

	return NPERR_GENERIC_ERROR;
}

NPError  NPN_SetValue(NPP id, NPPVariable variable, void *value)
{
	#ifdef _DEBUG
		printf("SET VALUE %d\n", variable);
	#endif

	if(variable == NPPVpluginTransparentBool)
		getInstance(id)->setTransparent(reinterpret_cast<bool>(value));

  return NPERR_NO_ERROR;
}

NPIdentifier NPN_GetStringIdentifier(const NPUTF8* name)
{
	IdentifierStore& store = IdentifierStore::getSingleton();

	return store.getIdentifier(name);
}

void NPN_GetStringIdentifiers(const NPUTF8** names, int32_t nameCount, NPIdentifier* identifiers)
{
	IdentifierStore& store = IdentifierStore::getSingleton();

	for(int i = 0; i < nameCount; i++)
		identifiers[i] = store.getIdentifier(names[i]);
}

NPIdentifier NPN_GetIntIdentifier(int32_t intid)
{
	IdentifierStore& store = IdentifierStore::getSingleton();

	return store.getIdentifier(intid);
}


bool NPN_IdentifierIsString(NPIdentifier identifier)
{
	IdentifierStore& store = IdentifierStore::getSingleton();

	return store.isIdentifierString(identifier);
}

NPUTF8* NPN_UTF8FromIdentifier(NPIdentifier identifier)
{
	IdentifierStore& store = IdentifierStore::getSingleton();
	return const_cast<NPUTF8*>(store.identifierToString(identifier));
}

int32_t NPN_IntFromIdentifier(NPIdentifier identifier)
{
	IdentifierStore& store = IdentifierStore::getSingleton();
	return store.identifierToInteger(identifier);
}

NPObject* NPN_CreateObject(NPP npp, NPClass *aClass)
{
	NPObject* result = 0;

	if(aClass->allocate)
		result = aClass->allocate(npp, aClass);
	else
		result = (NPObject*)NPN_MemAlloc(sizeof(NPObject));

	result->_class = aClass;
	result->referenceCount = 1;

	return result;
}

NPObject* NPN_RetainObject(NPObject *obj)
{
	obj->referenceCount++;
	return obj;
}

void NPN_ReleaseObject(NPObject *obj)
{
	if(obj->referenceCount > 1)
	{
		obj->referenceCount--;
		return;
	}

	// Else, time to destroy object
	if(obj->_class->deallocate)
		obj->_class->deallocate(obj);
	else
		NPN_MemFree(obj);		
}

bool NPN_Invoke(NPP npp, NPObject* obj, NPIdentifier methodName, const NPVariant *args, uint32_t argCount, NPVariant *result)
{
	if(!obj)
		return false;

	#ifdef _DEBUG
		printf("NPN_Invoke: %s\n", NPN_UTF8FromIdentifier(methodName));
	#endif

	if(obj->_class == NPObjectWrapperClass)
	{
		NPObjectWrapper* wrappedObject = reinterpret_cast<NPObjectWrapper*>(obj);

		return wrappedObject->scriptObject.invoke(methodName, args, argCount, result);
	}

	if(obj->_class->invoke)
		return obj->_class->invoke(obj, methodName, args, argCount, result);

	VOID_TO_NPVARIANT(*result);

	/*IdentifierStore& store = IdentifierStore::getSingleton();

	store.removeIdentifier(methodName);*/
	
	return true;
}

bool NPN_InvokeDefault(NPP npp, NPObject* obj, const NPVariant *args, uint32_t argCount, NPVariant *result)
{
	#ifdef _DEBUG
		printf("NPN_InvokeDefault\n");
	#endif
	return false;
}

bool NPN_Evaluate(NPP npp, NPObject *obj, NPString *script, NPVariant *result)
{
	if(!obj)
		return false;

	#ifdef _DEBUG
		printf("NPN_Evaluate: %s\n", std::string(script->UTF8Characters, script->UTF8Length).c_str());
	#endif

	if(obj->_class == NPObjectWrapperClass)
	{
		NPObjectWrapper* wrappedObject = reinterpret_cast<NPObjectWrapper*>(obj);

		return wrappedObject->scriptObject.evaluate(script, result);
	}

	VOID_TO_NPVARIANT(*result);
	return true;
}

bool NPN_GetProperty(NPP npp, NPObject *obj, NPIdentifier propertyName, NPVariant *result)
{
	if(!obj)
		return false;

	if(obj->_class == NPObjectWrapperClass)
	{
		NPObjectWrapper* wrappedObject = reinterpret_cast<NPObjectWrapper*>(obj);

		return wrappedObject->scriptObject.getProperty(propertyName, result);
	}

	if(obj->_class->hasProperty && obj->_class->getProperty)
		if(obj->_class->hasProperty(obj, propertyName))
			return obj->_class->getProperty(obj, propertyName, result);

	VOID_TO_NPVARIANT(*result);


	return false;
}

bool NPN_SetProperty(NPP npp, NPObject *obj, NPIdentifier propertyName, const NPVariant *value)
{
	if(!obj)
		return false;

	if(obj->_class == NPObjectWrapperClass)
	{
		NPObjectWrapper* wrappedObject = reinterpret_cast<NPObjectWrapper*>(obj);

		return wrappedObject->scriptObject.setProperty(propertyName, value);
	}

	if(obj->_class->setProperty)
		return obj->_class->setProperty(obj, propertyName, value);

	return false;
}

bool NPN_RemoveProperty(NPP npp, NPObject *obj, NPIdentifier propertyName)
{
	IdentifierStore& store = IdentifierStore::getSingleton();
	//store.removeIdentifier(propertyName);

	return false;
}

bool NPN_HasProperty(NPP npp, NPObject *obj, NPIdentifier propertyName)
{
	if(!obj)
		return false;

	if(obj->_class == NPObjectWrapperClass)
	{
		NPObjectWrapper* wrappedObject = reinterpret_cast<NPObjectWrapper*>(obj);

		return wrappedObject->scriptObject.hasProperty(propertyName);
	}

	if(obj->_class->hasProperty)
		return obj->_class->hasProperty(obj, propertyName);

	return false;
}

bool NPN_HasMethod(NPP npp, NPObject *obj, NPIdentifier propertyName)
{
	#ifdef _DEBUG
		printf("NPN_HasMethod\n");
	#endif

	return false;
}

void NPN_SetException(NPObject *obj, const NPUTF8 *message)
{
	#ifdef _DEBUG
		printf("NPN_Exception : %s\n", message);
		ofstream myfile;
		myfile.open ("AkaruiError.txt");
		myfile << message;
		myfile.close();
	#endif
}

void NPN_ReleaseVariantValue(NPVariant *variant)
{
	if(variant->type == NPVariantType_Object)
		NPN_ReleaseObject(variant->value.objectValue);
	else if(variant->type == NPVariantType_String)
		NPN_MemFree((void*)variant->value.stringValue.UTF8Characters);

	variant->type = NPVariantType_Void;
	//NPN_MemFree(variant);
}

} 