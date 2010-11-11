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

#include "Akarui.h"

using namespace Akarui;

AkaruiManager* AkaruiManager::instance = 0;

AkaruiManager::AkaruiManager(const std::string& baseDirectory, HWND winHandle) : baseDirectory(baseDirectory), winHandle(winHandle)
{
	instance = this;

	(new PluginHost())->load();
}

AkaruiManager::~AkaruiManager()
{
	for(std::vector<FlashMovie*>::iterator i = movies.begin(); i != movies.end(); i++)
		delete *i;

	delete PluginHost::getSingleton();
}

AkaruiManager* AkaruiManager::getSingleton()
{
	return instance;
}

FlashMovie* AkaruiManager::createFlashMovie(const std::string& source, int width, int height, bool isTransparent, const FlashOptions& options)
{
	FlashMovie* movie = new FlashMovie(baseDirectory + source, width, height, winHandle, isTransparent, options);
	movies.push_back(movie);

	return movie;
}

void AkaruiManager::destroyFlashMovie(FlashMovie* movie)
{
	for(std::vector<FlashMovie*>::iterator i = movies.begin(); i != movies.end(); i++)
	{
		if(*i == movie)
		{
			movies.erase(i);
			delete movie;
			break;
		}
	}
}

void AkaruiManager::update()
{
	PluginHost::getSingleton()->update();
}