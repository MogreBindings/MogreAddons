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

#include "PluginInstance.h"

using namespace std;

void destroyRenderContext(HDC& context, HBITMAP& bitmap);
void createRenderContext(HWND winHandle, HDC &context, HBITMAP& bitmap, unsigned char* &buffer, int width, int height);

PluginInstance::PluginInstance(int width, int height, const std::string& path, HWND win, bool isTransparent, unsigned short argc, char* argn[], char* argv[], int left, int top) 
	: width(width), height(height), path(path), dirtiness(false), winHandle(win), handler(0), lmbDown(false), transparency(isTransparent), left(left), top(top), manualInvalidation(true), iscontinuousdirty(false)
{
	nppHandle = new NPP_t();
	nppHandle->ndata = this;

	std::string urlLocation = translatePathToURL(path);
	scriptWindow = new ScriptWindow(urlLocation.substr(0, urlLocation.find_last_of("/") + 1), this);

	PluginHost::getSingleton()->pluginFuncs()->newp("application/x-shockwave-flash", nppHandle, NP_EMBED, argc, argn, argv, 0);

	createRenderContext(winHandle, renderContext, renderBitmap, renderBuffer, width, height);

	intermediateBuffer = new RenderBuffer(width, height);

	setWindow();
	loadFile();
	setWindow();
}

PluginInstance::~PluginInstance()
{
	delete nppHandle;
	delete intermediateBuffer;
	delete scriptWindow;

	PluginHost::getSingleton()->pluginFuncs()->destroy(nppHandle, 0);

	destroyRenderContext(renderContext, renderBitmap);
}

bool PluginInstance::injectMouseMove(int xPos, int yPos)
{
	return handleEvent(WM_MOUSEMOVE, lmbDown ? MK_LBUTTON : 0, MAKELPARAM(xPos, yPos));
}

bool PluginInstance::injectMouseDown(int xPos, int yPos)
{
	lmbDown = true;
	return handleEvent(WM_LBUTTONDOWN, MK_LBUTTON, MAKELPARAM(xPos, yPos));
}

bool PluginInstance::injectMouseUp(int xPos, int yPos)
{
	lmbDown = false;
	return handleEvent(WM_LBUTTONUP, 0, MAKELPARAM(xPos, yPos));
}

bool PluginInstance::handleEvent(UINT message, WPARAM wParam, LPARAM lParam)
{
	NPEvent event;
	event.event = message;
	event.wParam = (uint32_t)wParam;
	event.lParam = (uint32_t)lParam;

	return !!PluginHost::getSingleton()->pluginFuncs()->event(nppHandle, &event);
}

FlashValue PluginInstance::callFunction(const std::string& funcName, const FlashArguments& args)
{
	NPObject* scriptObject = 0;
	NPError rv = PluginHost::getSingleton()->pluginFuncs()->getvalue(nppHandle, NPPVpluginScriptableNPObject, (void*)&scriptObject);

	if(rv != NPERR_NO_ERROR)
		return FlashValue();

	int argc = (int)args.size();
	NPVariant* variantArgs = static_cast<NPVariant*>(NPN_MemAlloc(sizeof(NPVariant) * argc));

	for(int i = 0; i < argc; i++)
		FlashValueToNPVariant(args[i], variantArgs[i]);

	NPVariant result;
	VOID_TO_NPVARIANT(result);

	NPN_Invoke(nppHandle, scriptObject, NPN_GetStringIdentifier(funcName.c_str()), variantArgs, argc, &result);

	NPN_ReleaseObject(scriptObject);

	for(int i = 0; i < argc; i++)
		NPN_ReleaseVariantValue(&variantArgs[i]);

	NPN_MemFree(variantArgs);

	return NPVariantToFlashValue(result);
}

void PluginInstance::setHandler(FlashHandler* handler)
{
	this->handler = handler;
}

void PluginInstance::setTransparent(bool transparency)
{
	if(this->transparency != transparency)
	{
		this->transparency = transparency;
		dirtiness = true;
	}
}

bool PluginInstance::isDirty() const
{
	return dirtiness;
}

bool PluginInstance::isContinuousDirty() const
{
	return iscontinuousdirty;
}

void PluginInstance::setDirtiness(bool value)
{
	dirtiness = value;
	iscontinuousdirty = false;
}


void PluginInstance::setDirtiness(bool value, bool continuous)
{
	dirtiness = value;
	iscontinuousdirty = continuous;
}

bool PluginInstance::getManualInvalidation() const
{
	return manualInvalidation;
}

void PluginInstance::setManualInvalidation(bool value)
{
	manualInvalidation = value;
}

void PluginInstance::invalidateRect(const NPRect& rect)
{
	#ifdef _DEBUG
	//	printf("invalid: %d %d %d %d\n", rect.left, rect.top, rect.right, rect.bottom);
	#endif

	if(!manualInvalidation)
		dirtiness = true;
}

void PluginInstance::render(unsigned char* destination, int destRowspan)
{	
	if(!destination)
		return;

	if(transparency)
	{
		int size = width * height * 4;

		memset(renderBuffer, 0, size);
		handleEvent(WM_PAINT, 0, 0);

		intermediateBuffer->copyFrom(renderBuffer, width * 4);

		//memcpy(destination, renderBuffer, destRowspan * height);

		memset(renderBuffer, 255, size);
		handleEvent(WM_PAINT, 0, 0);

		for(int i = 0; i < size; i += 4)
			intermediateBuffer->buffer[i+3] = 255 - (renderBuffer[i] - intermediateBuffer->buffer[i]);

		intermediateBuffer->copyTo(destination, destRowspan, 4, false);
		//for(int i = 0; i < size; i += 4)
		//	destination[i+3] = 255 - (renderBuffer[i] - destination[i]);
	}
	else
	{
		RECT rect;
		rect.left = rect.top = 0;
		rect.bottom = height;
		rect.right = width;
		handleEvent(WM_PAINT, PtrToUlong(renderContext), PtrToUlong(&rect));

		int size = width * height * 4;
		//memcpy(destination, renderBuffer, size);
		//for(int i = 0; i < size; i += 4)
		//	destination[i + 3] = 255;

		intermediateBuffer->copyFrom(renderBuffer, width * 4);

		for(int i = 0; i < size; i += 4)
			intermediateBuffer->buffer[i+3] = 255;

		intermediateBuffer->copyTo(destination, destRowspan, 4, false);

	}

	if(!manualInvalidation)
		dirtiness = false;
}



void PluginInstance::resize(int width, int height)
{
	this->width = width;
	this->height = height;

	destroyRenderContext(renderContext, renderBitmap);
	createRenderContext(winHandle, renderContext, renderBitmap, renderBuffer, width, height);
	setWindow();

	delete intermediateBuffer;
	intermediateBuffer = new RenderBuffer(width, height);
}

void PluginInstance::setTopLeft(int top, int left)
{
	this->top = top;
	this->left = left;

	setWindow();
}

void PluginInstance::setWindow()
{
		WINDOWPOS win_pos = {0};
		win_pos.x = left;
		win_pos.y = top;
		win_pos.cx =  width;
		win_pos.cy = height;

		handleEvent(WM_WINDOWPOSCHANGED, 0, PtrToUlong(&win_pos));

	window.x = 0;
	window.y = 0;
	window.width = width;
	window.height = height;
	window.type = NPWindowTypeDrawable;
	window.window = (void*)renderContext;
	window.clipRect.left = 0;
	window.clipRect.top = 0;
	window.clipRect.right = width;
	window.clipRect.bottom = height;

	PluginHost::getSingleton()->pluginFuncs()->setwindow(nppHandle, &window);
}

void PluginInstance::loadFile()
{
	std::ifstream file(path.c_str(), std::ios::in|std::ios::binary|std::ios::ate);
	size_t size = file.tellg();

	std::string urlPath = translatePathToURL(path);

	NPStream* stream = new NPStream();
	stream->url = urlPath.c_str();
	stream->ndata = 0;
	stream->end = (uint32_t)size;
	stream->headers = 0;
	stream->pdata = 0;
	stream->lastmodified = 0;

	NPMIMEType mimetype = "application/x-shockwave-flash";
	unsigned short type = NP_NORMAL;
	PluginHost::getSingleton()->pluginFuncs()->newstream(nppHandle, mimetype, stream, false, &type);

	int offset = 0;
	file.seekg(0, std::ios::beg);

	while(offset < (int)size)
	{
		int maxLen = PluginHost::getSingleton()->pluginFuncs()->writeready(nppHandle, stream);

		int len = (int)size - offset;
		len = len > maxLen ? maxLen : len;

		char* buffer = new char[len];
		file.read(buffer, len);

		PluginHost::getSingleton()->pluginFuncs()->write(nppHandle, stream, offset, len, buffer);

		delete[] buffer;

		offset += len;
	}

	PluginHost::getSingleton()->pluginFuncs()->destroystream(nppHandle, stream, NPRES_DONE);

	file.close();

	delete stream;
}

ScriptWindow* PluginInstance::getScriptWindow()
{
	return scriptWindow;
}

HWND PluginInstance::getNativeWindow()
{
	return winHandle;
}

std::string PluginInstance::getLocation()
{
	return scriptWindow->getLocation();
}

FlashValue PluginInstance::handleFlashCall(const std::wstring& funcName, const FlashArguments& args)
{
	if(handler)
		return handler->onFlashCall(funcName, args);
	else
		return FLASH_UNDEFINED;
}

void destroyRenderContext(HDC& context, HBITMAP& bitmap)
{
	if(context)
	{
		DeleteDC(context);
		DeleteObject(bitmap);
		context = 0;
		bitmap = 0;
	}
}

void createRenderContext(HWND winHandle, HDC &context, HBITMAP& bitmap, unsigned char* &buffer, int width, int height)
{
	HDC hdc = GetDC(0);
	BITMAPINFOHEADER bih = {0};
	bih.biSize = sizeof(BITMAPINFOHEADER);
	bih.biBitCount = 32;
	bih.biCompression = BI_RGB;
	bih.biPlanes = 1;
	bih.biWidth = width;
	bih.biHeight = -height;
	context = CreateCompatibleDC(hdc);
	bitmap = CreateDIBSection(hdc, (BITMAPINFO*)&bih, DIB_RGB_COLORS, (void**)&buffer, 0, 0);

	SelectObject(context, bitmap);
	ReleaseDC(0, hdc);
}