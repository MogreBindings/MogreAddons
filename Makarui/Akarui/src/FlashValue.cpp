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

#include "FlashValue.h"

typedef enum {
    FVT_NULL,
	FVT_UNDEFINED,
	FVT_BOOLEAN,
	FVT_INTEGER,
	FVT_DOUBLE,
	FVT_STRING,
	FVT_ARRAY,
	FVT_OBJECT
} FlashVariantType;

namespace Impl {

struct FlashVariant
{
	FlashVariantType type;

	union {
		bool booleanValue;
		int integerValue;
		double doubleValue;
		std::wstring* stringValue;
		FlashValue::Array* arrayValue;
		FlashValue::Object* objectValue;
	} value;

	void reset()
	{
		if(type == FVT_STRING)
			delete value.stringValue;
		else if(type == FVT_ARRAY)
			delete value.arrayValue;
		else if(type == FVT_OBJECT)
			delete value.objectValue;

		type = FVT_UNDEFINED;
	}

	void initFrom(const FlashVariant& source)
	{
		type = source.type;

		if(type == FVT_STRING)
			value.stringValue = new std::wstring(*source.value.stringValue);
		else if(type == FVT_ARRAY)
			value.arrayValue = new FlashValue::Array(*source.value.arrayValue);
		else if(type == FVT_OBJECT)
			value.objectValue = new FlashValue::Object(*source.value.objectValue);
		else
			value = source.value;
	}
};

}

using namespace Akarui;

FlashIdentifier::FlashIdentifier(int id)
{
	npIdentifier = NPN_GetIntIdentifier(id);
}

FlashIdentifier::FlashIdentifier(const char* id)
{
	npIdentifier = NPN_GetStringIdentifier(id);
}

FlashIdentifier::FlashIdentifier(const std::string& id)
{
	npIdentifier = NPN_GetStringIdentifier(id.c_str());
}

FlashIdentifier::FlashIdentifier(const std::wstring& id)
{
	npIdentifier = NPN_GetStringIdentifier(reinterpret_cast<const char*>(id.c_str()));
}

bool FlashIdentifier::operator<(const FlashIdentifier& rhs) const
{
	return npIdentifier < rhs.npIdentifier;
}

bool FlashIdentifier::operator==(const FlashIdentifier& rhs) const
{
	return npIdentifier == rhs.npIdentifier;
}

FlashValue::FlashValue()
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_UNDEFINED;
}

FlashValue::FlashValue(FlashValueType valueType)
{
	variant = new Impl::FlashVariant();
	if(valueType == FLASH_NULL)
		variant->type = FVT_NULL;
	else
		variant->type = FVT_UNDEFINED;
}

FlashValue::FlashValue(bool value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_BOOLEAN;
	variant->value.booleanValue = value;
}

FlashValue::FlashValue(int value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_INTEGER;
	variant->value.integerValue = value;
}

FlashValue::FlashValue(double value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_DOUBLE;
	variant->value.doubleValue = value;
}

FlashValue::FlashValue(const wchar_t* value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_STRING;
	variant->value.stringValue = new std::wstring(value);
}

FlashValue::FlashValue(const std::wstring& value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_STRING;
	variant->value.stringValue = new std::wstring(value);
}

FlashValue::FlashValue(const Object& value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_OBJECT;
	variant->value.objectValue = new Object(value);
}

FlashValue::FlashValue(const Array& value)
{
	variant = new Impl::FlashVariant();
	variant->type = FVT_ARRAY;
	variant->value.arrayValue = new Array(value);
}

FlashValue::FlashValue(const FlashValue& original) 
{
	variant = new Impl::FlashVariant();
	variant->initFrom(*original.variant);	
}

FlashValue& FlashValue::operator=(const FlashValue& rhs) 
{
	variant->reset();
	variant->initFrom(*rhs.variant);

	return *this;
}

FlashValue::~FlashValue()
{
	variant->reset();
	delete variant;
}

bool FlashValue::isBoolean() const
{
	return variant->type == FVT_BOOLEAN;
}

bool FlashValue::isInteger() const
{
	return variant->type == FVT_INTEGER;
}

bool FlashValue::isDouble() const
{
	return variant->type == FVT_DOUBLE;
}

bool FlashValue::isNumber() const
{
	return isDouble() || isInteger();
}

bool FlashValue::isString() const
{
	return variant->type == FVT_STRING;
}

bool FlashValue::isObject() const
{
	return variant->type == FVT_OBJECT;
}

bool FlashValue::isArray() const
{
	return variant->type == FVT_ARRAY;
}

bool FlashValue::isNull() const
{
	return variant->type == FVT_NULL;
}

bool FlashValue::isUndefined() const
{
	return variant->type == FVT_UNDEFINED;
}

std::wstring FlashValue::toString() const
{
	if(isString())
		return *variant->value.stringValue;
	else if(isInteger())
		return numberToString<int>(variant->value.integerValue);
	else if(isDouble())
		return numberToString<double>(variant->value.doubleValue);
	else if(isBoolean())
		return numberToString<bool>(variant->value.booleanValue);
	else if(isArray())
		return L"[array]";
	else if(isObject())
		return L"[object]";
	else if(isNull())
		return L"null";
	else
		return L"undefined";
}

int FlashValue::toInteger() const
{
	if(isString())
		return stringToNumber<int>(*variant->value.stringValue);
	else if(isInteger())
		return variant->value.integerValue;
	else if(isDouble())
		return (int)variant->value.doubleValue;
	else if(isBoolean())
		return (int)variant->value.booleanValue;
	else
		return 0;
}

double FlashValue::toDouble() const
{
	if(isString())
		return stringToNumber<double>(*variant->value.stringValue);
	else if(isInteger())
		return (double)variant->value.integerValue;
	else if(isDouble())
		return variant->value.doubleValue;
	else if(isBoolean())
		return (double)variant->value.booleanValue;
	else
		return 0;
}

bool FlashValue::toBoolean() const
{
	if(isString())
		return stringToNumber<bool>(*variant->value.stringValue);
	else if(isInteger())
		return !!variant->value.integerValue;
	else if(isDouble())
		return !!((int)variant->value.doubleValue);
	else if(isBoolean())
		return variant->value.booleanValue;
	else
		return false;
}

FlashValue::Object& FlashValue::getObject()
{
	assert(isObject());

	return *(variant->value.objectValue);
}

const FlashValue::Object& FlashValue::getObject() const
{
	assert(isObject());

	return *(variant->value.objectValue);
}

FlashValue::Array& FlashValue::getArray()
{
	assert(isArray());

	return *(variant->value.arrayValue);
}

const FlashValue::Array& FlashValue::getArray() const
{
	assert(isArray());

	return *(variant->value.arrayValue);
}