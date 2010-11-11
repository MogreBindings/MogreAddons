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

#ifndef __InternalUtils_H__
#define __InternalUtils_H__

#include "PreCompiled.h"

using namespace Akarui;

void variantToXML(const NPVariant& variant, std::stringstream& stream);

std::string translateFlashRequest(const NPVariant* args, int argCount);

void copyNPVariant(const NPVariant* source, NPVariant* destination);

int replaceAll(std::string &sourceStr, const std::string &replaceWhat, const std::string &replaceWith);

std::string translatePathToURL(const std::string& path);
std::string translateURLToPath(const std::string& url);

void FlashValueToNPVariant(const FlashValue& flashValue, NPVariant& result);

FlashValue NPVariantToFlashValue(const NPVariant& npVariant);

template<class NumberType>
inline NumberType stringToNumber(const std::string& numberString)
{
	if(numberString.substr(0, 4).compare("true") == 0) return 1;
	else if(numberString.substr(0, 4).compare("false") == 0) return 0;

	std::istringstream converter(numberString);
	
	if(typeid(NumberType)==typeid(bool))
	{
		int result;
		return (converter >> result).fail() ? false : !!result;
	}

	NumberType result;
	return (converter >> result).fail() ? 0 : result;
}

template<class NumberType>
inline std::string numberToString(const NumberType &number)
{
	std::ostringstream converter;

	if(typeid(NumberType)==typeid(bool))
	{
		return number ? "true" : "false";
	}

	return (converter << number).fail() ? "" : converter.str();
}

bool parseJSCall(const std::string& src, std::string& funcName, FlashArguments& args);
FlashValue parseValue(const std::string& src, std::string::size_type& i);
FlashValue parseStringValue(const std::string& src, std::string::size_type& i);
FlashValue parseNumberValue(const std::string& src, std::string::size_type& i);
FlashValue parseArrayValue(const std::string& src, std::string::size_type& i);
FlashValue parseObjectValue(const std::string& src, std::string::size_type& i);

#endif