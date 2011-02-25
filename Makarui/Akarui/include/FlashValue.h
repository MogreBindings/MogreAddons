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

#ifndef __FlashValue_H__
#define __FlashValue_H__

#include "PreCompiled.h"

namespace Impl { struct FlashVariant; }

namespace Akarui {

struct _AkaruiExport FlashIdentifier
{
	void* npIdentifier;

	FlashIdentifier(int id);
	FlashIdentifier(const char* id);
	FlashIdentifier(const std::string& id);
	FlashIdentifier(const std::wstring& id);

	bool operator<(const FlashIdentifier& rhs) const;
	bool operator==(const FlashIdentifier& rhs) const;
};

enum FlashValueType
{
	FLASH_UNDEFINED,
	FLASH_NULL
};

class _AkaruiExport FlashValue
{
	Impl::FlashVariant* variant;
public:

	typedef std::map<FlashIdentifier, FlashValue> Object;
	typedef std::vector<FlashValue> Array;

	FlashValue();
	FlashValue(FlashValueType valueType);
	FlashValue(bool value);
	FlashValue(int value);
	FlashValue(double value);
	FlashValue(const wchar_t* value);
	FlashValue(const std::wstring& value);
	FlashValue(const Object& value);
	FlashValue(const Array& value);
	FlashValue(const FlashValue& original);
	FlashValue& operator=(const FlashValue& rhs);
	~FlashValue();

	bool isBoolean() const;
	bool isInteger() const;
	bool isDouble() const;
	bool isNumber() const;
	bool isString() const;
	bool isObject() const;
	bool isArray() const;
	bool isNull() const;
	bool isUndefined() const;

	std::wstring toString() const;
	int toInteger() const;
	double toDouble() const;
	bool toBoolean() const;
	
	Object& getObject();
	const Object& getObject() const;

	Array& getArray();
	const Array& getArray() const;
};


}

#endif