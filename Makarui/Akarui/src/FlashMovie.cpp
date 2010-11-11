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

#include "FlashMovie.h"

using namespace std;
using namespace Akarui;

FlashMovie::FlashMovie(const std::string& path, int width, int height, HWND winHandle, bool isTransparent, const FlashOptions& options)
{
	int argc = 5;
	char* argn[] = { "wmode" , "width", "height", "quality", "scale" };

	std::vector<char*> argv;
	argv.push_back(isTransparent? "transparent" : "opaque");
	argv.push_back((char*)numberToString(222).c_str());
	argv.push_back((char*)numberToString(222).c_str());

	switch(options.renderQuality)
	{
	case RQ_LOW:
		argv.push_back("low"); break;
	case RQ_MEDIUM:
		argv.push_back("medium"); break;
	case RQ_HIGH:
		argv.push_back("high"); break;
	case RQ_BEST:
		argv.push_back("best"); break;
	case RQ_AUTOLOW:
		argv.push_back("autolow"); break;
	case RQ_AUTOHIGH:
		argv.push_back("autohigh"); break;
	default:
		argv.push_back("autolow");
	};

	switch(options.scaleMode)
	{
		case SM_SHOWALL:
			argv.push_back("showall"); break;
		case SM_EXACTFIT:
			argv.push_back("exactfit"); break;
		case SM_NOBORDER:
			argv.push_back("noborder"); break;
		default:
			argv.push_back("showall");
	}


	pluginInstance = new PluginInstance(width, height, path, winHandle, isTransparent, argc, argn, &argv[0]);
}

FlashMovie::~FlashMovie()
{
	delete pluginInstance;
}

void FlashMovie::resize(int width, int height)
{
	pluginInstance->resize(width, height);
}

FlashValue FlashMovie::callFunction(const std::string& funcName, const FlashArguments& args)
{
	return pluginInstance->callFunction(funcName, args);
}

void FlashMovie::setHandler(FlashHandler* handler)
{
	pluginInstance->setHandler(handler);
}

bool FlashMovie::isDirty() const
{
	return pluginInstance->isDirty();
}

void FlashMovie::render(unsigned char* destination, unsigned int destRowSpan)
{
	pluginInstance->render(destination, destRowSpan);
}

void FlashMovie::setTranparent(bool isTransparent)
{
	pluginInstance->setTransparent(isTransparent);
}

bool FlashMovie::injectMouseMove(int x, int y)
{
	return pluginInstance->injectMouseMove(x, y);
}

bool FlashMovie::injectMouseDown(int x, int y)
{
	return pluginInstance->injectMouseDown(x, y);
}

bool FlashMovie::injectMouseUp(int x, int y)
{
	return pluginInstance->injectMouseUp(x, y);
}

bool FlashMovie::injectKeyboardEvent(UINT msg, WPARAM wParam, LPARAM lParam)
{
	return pluginInstance->handleEvent(msg, wParam, lParam);
}

 