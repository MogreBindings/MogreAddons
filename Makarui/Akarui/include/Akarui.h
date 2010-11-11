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

#ifndef __Akarui_H__
#define __Akarui_H__

#include "PreCompiled.h"

namespace Akarui {

class _AkaruiExport AkaruiManager
{
public:
	AkaruiManager(const std::string& baseDirectory, HWND winHandle);
	~AkaruiManager();

	static AkaruiManager* getSingleton();

	FlashMovie* createFlashMovie(const std::string& source, int width, int height, bool isTransparent, const FlashOptions& options);

	void destroyFlashMovie(FlashMovie* movie);

	void update();

protected:
	std::vector<FlashMovie*> movies;
	std::string baseDirectory;
	HWND winHandle;
	static AkaruiManager* instance;
};

}

#endif