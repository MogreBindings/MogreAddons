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

#include "RenderBuffer.h"

void copyBuffers(int width, int height, unsigned char* src, int srcRowSpan, unsigned char* dest, int destRowSpan, int destDepth, bool convertToRGBA)
{
	assert(destDepth == 3 || destDepth == 4);

	if(!convertToRGBA)
	{
		if(destDepth == 3)
		{
			for(int row = 0; row < height; row++)
				for(int col = 0; col < width; col++)
					memcpy(dest + row * destRowSpan + col * 3, src + row * srcRowSpan + col * 4, 3);
		}
		else if(destDepth == 4)
		{
			for(int row = 0; row < height; row++)
				memcpy(dest + row * destRowSpan, src + row * srcRowSpan, srcRowSpan);
		}
	}
	else
	{
		if(destDepth == 3)
		{
			int srcRowOffset, destRowOffset;
			for(int row = 0; row < height; row++)
			{
				srcRowOffset = row * srcRowSpan;
				destRowOffset = row * destRowSpan;

				for(int col = 0; col < width; col++)
				{
					dest[destRowOffset + col * 3] = src[srcRowOffset + col * 4 + 2];
					dest[destRowOffset + col * 3 + 1] = src[srcRowOffset + col * 4 + 1];
					dest[destRowOffset + col * 3 + 2] = src[srcRowOffset + col * 4];
				}
			}
		}
		else if(destDepth == 4)
		{
			int srcRowOffset, destRowOffset;
			for(int row = 0; row < height; row++)
			{
				srcRowOffset = row * srcRowSpan;
				destRowOffset = row * destRowSpan;

				for(int colOffset = 0; colOffset < srcRowSpan; colOffset += 4)
				{
					dest[destRowOffset + colOffset] = src[srcRowOffset + colOffset + 2];
					dest[destRowOffset + colOffset + 1] = src[srcRowOffset + colOffset + 1];
					dest[destRowOffset + colOffset + 2] = src[srcRowOffset + colOffset];
					dest[destRowOffset + colOffset + 3] = src[srcRowOffset + colOffset + 3];
				}
			}
		}
	}
}

RenderBuffer::RenderBuffer(int width, int height) : width(0), height(0), buffer(0), rowSpan(0)
{
	reserve(width, height);
}

RenderBuffer::~RenderBuffer()
{
	if(buffer)
		delete[] buffer;
}

void RenderBuffer::reserve(int width, int height)
{
	if(this->width != width || this->height != height)
	{
		this->width = width;
		this->height = height;

		rowSpan = width * 4;

		if(buffer)
			delete[] buffer;

		buffer = new unsigned char[width * height * 4];
		memset(buffer, 255, width * height * 4);
	}
}

void RenderBuffer::copyFrom(unsigned char* srcBuffer, int srcRowSpan)
{
	for(int row = 0; row < height; row++)
		memcpy(buffer + row * rowSpan, srcBuffer + row * srcRowSpan, rowSpan);
}

void RenderBuffer::copyTo(unsigned char* destBuffer, int destRowSpan, int destDepth, bool convertToRGBA)
{
	copyBuffers(width, height, buffer, width * 4, destBuffer, destRowSpan, destDepth, convertToRGBA);
}
