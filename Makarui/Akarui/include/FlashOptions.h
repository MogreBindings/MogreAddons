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

#ifndef __FlashOptions_H__
#define __FlashOptions_H__

#include "PreCompiled.h"

namespace Akarui {

/**
* Used by FlashControl::setQuality, defines the Flash rendering quality.
* 
* <ul>
* <li>RQ_LOW - Favors playback speed over appearance and never uses anti-aliasing.
* <li>RQ_MEDIUM - Applies some anti-aliasing and does not smooth bitmaps. It produces a better quality than the Low setting, but lower quality than the High setting.
* <li>RQ_HIGH - Favors appearance over playback speed and always applies anti-aliasing. If the movie does not contain animation, bitmaps are smoothed; if the movie has animation, bitmaps are not smoothed.
* <li>RQ_BEST - Provides the best display quality and does not consider playback speed. All output is anti-aliased and all bitmaps are smoothed.
* <li>RQ_AUTOLOW - Emphasizes speed at first but improves appearance whenever possible. Playback begins with anti-aliasing turned off. If the Flash Player detects that the processor can handle it, anti-aliasing is turned on.
* <li>RQ_AUTOHIGH - Emphasizes playback speed and appearance equally at first but sacrifices appearance for playback speed if necessary. Playback begins with anti-aliasing turned on. If the actual frame rate drops below the specified frame rate, anti-aliasing is turned off to improve playback speed.
* </ul>
*/
enum RenderQuality
{
	RQ_LOW,
	RQ_MEDIUM,
	RQ_HIGH,
	RQ_BEST,
	RQ_AUTOLOW,
	RQ_AUTOHIGH
};

/**
* Used by FlashControl::setScaleMode, defines the scaling mode to use when the aspect ratio of the control does not match that of the movie.
*
* <ul>
* <li>SM_SHOWALL - Preserves the movie's aspect ratio by adding borders. (Default)
* <li>SM_NOBORDER - Preserves the movie's aspect ratio by cropping the sides.
* <li>SM_EXACTFIT - Does not preserve the movie's aspect ratio, scales the movie to the dimensions of the control.
*/
enum ScaleMode
{
	SM_SHOWALL,
	SM_NOBORDER,
	SM_EXACTFIT
};

struct _AkaruiExport FlashOptions
{
	FlashOptions()
	{
		shouldAutoPlay = true;
		shouldLoop = true;
		renderQuality = RQ_HIGH;
		scaleMode = SM_SHOWALL;
		backgroundColor = "";
	}

	bool shouldAutoPlay;
	bool shouldLoop;
	RenderQuality renderQuality;
	ScaleMode scaleMode;
	std::string backgroundColor;
};

}

#endif