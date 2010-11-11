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

#ifndef __FlashMovie_H__
#define __FlashMovie_H__

#include "PreCompiled.h"

class PluginInstance;

namespace Akarui {

class AkaruiManager;
class FlashValue;
class FlashHandler;

class _AkaruiExport FlashMovie
{
	PluginInstance* pluginInstance;
	friend class AkaruiManager;

	FlashMovie(const std::string& path, int width, int height, HWND winHandle, bool isTransparent, const FlashOptions& options);
	~FlashMovie();
public:

	void resize(int width, int height);

	FlashValue callFunction(const std::string& funcName, const FlashArguments& args);
	
	void setHandler(FlashHandler* handler);

	bool isDirty() const;

	void render(unsigned char* destination, unsigned int destRowSpan);

	void setTranparent(bool isTransparent);

	bool injectMouseMove(int x, int y);
	bool injectMouseDown(int x, int y);
	bool injectMouseUp(int x, int y);

	bool injectKeyboardEvent(UINT msg, WPARAM wParam, LPARAM lParam);
};

}

#endif