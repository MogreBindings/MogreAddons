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

#ifndef __PluginInstance_H__
#define __PluginInstance_H__

#include "FlashValue.h"
#include "PreCompiled.h"



namespace Akarui 
{ 
	class FlashHandler; 
	class FlashValue;
}

class PluginHost;
class ScriptWindow;
class RenderBuffer;

using namespace Akarui;

class PluginInstance
{
	NPP_t* nppHandle;
	NPRect dirtyRect;
	int width, height, left, top;
	HDC renderContext;
	HBITMAP renderBitmap;
	unsigned char *renderBuffer;
	NPWindow window;
	HWND winHandle;
	NPObject* scriptObject;
	ScriptWindow* scriptWindow;
	bool dirtiness;
	bool transparency;
	std::string path;
	FlashHandler* handler;
	bool lmbDown;
	bool manualInvalidation;
	bool iscontinuousdirty;

	RenderBuffer* intermediateBuffer;

	void setWindow();
	void updateGeometry();
	void loadFile();
	void invalidateRect(const NPRect& rect);
	FlashValue handleFlashCall(const std::wstring& funcName, const FlashArguments& args);

	friend class PluginHost;
	friend class ScriptWindow;
	
public:
	
	PluginInstance(int width, int height, const std::string& path, HWND win, bool isTransparent, unsigned short argc, char* argn[], char* argv[], int left, int top);

	~PluginInstance();

	bool injectMouseMove(int xPos, int yPos);
	bool injectMouseDown(int xPos, int yPos);
	bool injectMouseUp(int xPos, int yPos);

	bool handleEvent(UINT message, WPARAM wParam, LPARAM lParam);

	FlashValue callFunction(const std::string& funcName, const FlashArguments& args);

	void setHandler(FlashHandler* handler);

	void setTransparent(bool transparency);

	bool isDirty() const;
	
	bool isContinuousDirty() const;

	void setDirtiness(bool value);

	void setDirtiness(bool value, bool continuous);

	bool getManualInvalidation() const;

	void setManualInvalidation(bool value);

	void render(unsigned char* destination, int destRowspan);

	void PluginInstance::setTopLeft(int top, int left);

	void resize(int width, int height);

	ScriptWindow* getScriptWindow();

	HWND getNativeWindow();

	std::string getLocation();

	friend void NPN_InvalidateRect(NPP id, NPRect *invalidRect);
};

#endif