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

#include "InternalUtils.h"

void variantToXML(const NPVariant& variant, std::stringstream& stream)
{
	switch(variant.type)
	{
	case NPVariantType_Bool:
		stream << variant.value.boolValue ? "<true/>" : "<false/>";
		break;
	case NPVariantType_Int32:
		stream << "<number>" << variant.value.intValue << "</number>";
		break;
	case NPVariantType_Double:
		stream << "<number>" << variant.value.doubleValue << "</number>";
		break;
	case NPVariantType_String:
		stream << "<string>" << std::string(variant.value.stringValue.UTF8Characters, variant.value.stringValue.UTF8Length) << "</string>";
		break;
	case NPVariantType_Object:
		if(variant.value.objectValue->_class != NPObjectWrapperClass)
			stream << "<undefined/>";
		else
			reinterpret_cast<const NPObjectWrapper*>(variant.value.objectValue)->scriptObject.toXML(stream);
		break;
	case NPVariantType_Void:
		stream << "<undefined/>";
		break;
	case NPVariantType_Null:
		stream << "<null/>";
		break;
	}
}

std::string translateFlashRequest(const NPVariant* args, int argCount)
{
	if(!argCount)
		return "";
	else if(!NPVARIANT_IS_STRING(args[0]))
		return "";

	std::stringstream stream;

	stream << "<invoke name=\"" << std::string(args[0].value.stringValue.UTF8Characters, 
		args[0].value.stringValue.UTF8Length) << "\" returntype=\"javascript\"><arguments>";

	for(int i = 1; i < argCount; i++)
		variantToXML(args[i], stream);

	stream << "</arguments></invoke>";

	return stream.str();
}

void copyNPVariant(const NPVariant* source, NPVariant* destination)
{
	switch(source->type)
	{
	case NPVariantType_Bool:
		BOOLEAN_TO_NPVARIANT(source->value.boolValue, *destination);
		break;
	case NPVariantType_Int32:
		INT32_TO_NPVARIANT(source->value.intValue, *destination);
		break;
	case NPVariantType_Double:
		DOUBLE_TO_NPVARIANT(source->value.doubleValue, *destination);
		break;
	case NPVariantType_String:
		{
		int len = source->value.stringValue.UTF8Length;
		char* buffer = reinterpret_cast<char*>(NPN_MemAlloc(len));
		memcpy(buffer, source->value.stringValue.UTF8Characters, len);

		destination->type = NPVariantType_String;
		destination->value.stringValue.UTF8Length = len;
		destination->value.stringValue.UTF8Characters = buffer;
		}
		break;
	case NPVariantType_Object:
		OBJECT_TO_NPVARIANT(source->value.objectValue, *destination);
		NPN_RetainObject(destination->value.objectValue);
		break;
	case NPVariantType_Void:
		VOID_TO_NPVARIANT(*destination);
		break;
	case NPVariantType_Null:
		NULL_TO_NPVARIANT(*destination);
		break;
	}
}

int replaceAll(std::string &sourceStr, const std::string &replaceWhat, const std::string &replaceWith)
{
	int count = 0;

	for(size_t i = sourceStr.find(replaceWhat); i != std::string::npos; i = sourceStr.find(replaceWhat, i + replaceWith.length()))
	{
		sourceStr.erase(i, replaceWhat.length());
		sourceStr.insert(i, replaceWith);
		++count;
	}

	return count;
}

std::string translatePathToURL(const std::string& path)
{
	std::string result(path);

	replaceAll(result, "%", "%25");
	replaceAll(result, " ", "%20");
	replaceAll(result, "#", "%23");
	replaceAll(result, "\\", "/");
	replaceAll(result, "..", "uu");

	return "http://fake.com/" + result.substr(0, 1) + result.substr(2);
}

std::string translateURLToPath(const std::string& url)
{
	std::string result(url.substr(18));
	
	replaceAll(result, "uu", "..");
	replaceAll(result, "/", "\\");
	replaceAll(result, "%23", "#");
	replaceAll(result, "%20", " ");
	replaceAll(result, "%25", "%");

	result = url.substr(16, 1) + ":\\" + result;

	return result;
}

void FlashValueObjectToNPVariant(const FlashValue::Object& flashValObject, NPVariant& result);

void FlashValueToNPVariant(const FlashValue& flashValue, NPVariant& result)
{
	if(flashValue.isBoolean())
	{
		BOOLEAN_TO_NPVARIANT(flashValue.toBoolean(), result);
	}
	else if(flashValue.isInteger())
	{
		INT32_TO_NPVARIANT(flashValue.toInteger(), result);
	}
	else if(flashValue.isDouble())
	{
		DOUBLE_TO_NPVARIANT(flashValue.toDouble(), result);
	}
	else if(flashValue.isString())
	{
		const std::string& val = flashValue.toString();
		unsigned int len = (unsigned int)val.length();
		char* buffer = reinterpret_cast<char*>(NPN_MemAlloc(len));
		memcpy(buffer, val.c_str(), len);

		result.type = NPVariantType_String;
		result.value.stringValue.UTF8Length = len;
		result.value.stringValue.UTF8Characters = buffer;
	}
	else if(flashValue.isObject())
	{
		FlashValueObjectToNPVariant(flashValue.getObject(), result);
	}
	else if(flashValue.isNull())
	{
		NULL_TO_NPVARIANT(result);
	}
	else
	{
		VOID_TO_NPVARIANT(result);
	}
}

FlashValue NPVariantToFlashValue(const NPVariant& npVariant)
{
	return FlashValue();
}

void FlashValueObjectToNPVariant(const FlashValue::Object& flashValObject, NPVariant& result)
{		
	NPObjectWrapper* object = reinterpret_cast<NPObjectWrapper*>(NPN_CreateObject(0, NPObjectWrapperClass));

	for(FlashValue::Object::const_iterator i = flashValObject.begin(); i != flashValObject.end(); i++)
	{
		NPVariant tempVariant;
		FlashValueToNPVariant(i->second, tempVariant);

		object->scriptObject.setProperty(const_cast<NPIdentifier>(i->first.npIdentifier), &tempVariant);

		NPN_ReleaseVariantValue(&tempVariant);
	}

	OBJECT_TO_NPVARIANT((NPObject*)object, result);
}

#define PARSE_FAIL \
	i = std::string::npos; \
	return FLASH_UNDEFINED;

bool parseJSCall(const std::string& src, std::string& funcName, FlashArguments& args)
{
	if(!src.length())
		return false;
	else if(src[src.length() - 1] != ')')
		return false;

	std::string::size_type opParen = src.find('(');

	if(opParen == std::string::npos)
		return false;

	funcName = src.substr(0, opParen);
	std::string argsList = src.substr(opParen + 1, src.length() - opParen - 2);

	std::string::size_type iter = 0;
	while(iter < argsList.size())
	{
		FlashValue val = parseValue(argsList, iter);

		if(iter == std::string::npos)
			return false;

		args.push_back(val);
		iter++;
	}

	return true;
}

FlashValue parseValue(const std::string& src, std::string::size_type& i)
{
	if(i >= src.size())
		return FLASH_UNDEFINED;

	switch(src.at(i))
	{
	case 't':
		i += 4;
		return true;
	case 'f':
		i += 5;
		return false;
	case 'n':
		i += 4;
		return FLASH_NULL;
	case 'u':
		i += 9;
		return FLASH_UNDEFINED;
	case '"':
		return parseStringValue(src, i);
	case '(':
		return parseObjectValue(src, i);
	case '[':
		return parseArrayValue(src, i);
	default:
		return parseNumberValue(src, i);
		break;
	}
}

FlashValue parseStringValue(const std::string& src, std::string::size_type& i)
{
	std::string result;
	i++;

	while(true)
	{
		if(i >= src.size())
		{
			PARSE_FAIL
		}

		if(src[i] == '\\')
		{
			if(++i >= src.size())
				return FLASH_UNDEFINED;

			if(src[i] == '"')
				result.push_back('"');
			else if(src[i] == 'n')
				result.push_back('\n');
			else if(src[i] == 't')
				result.push_back('\t');
			else if(src[i] == '\\')
				result.push_back('\\');

			i++;
			continue;
		}
		else if(src[i] == '"')
		{
			i++;
			return FlashValue(result);
		}

		result += src[i++];
	}
}

FlashValue parseNumberValue(const std::string& src, std::string::size_type& i)
{
	std::string result;
	bool hasDecimalPt = false;

	while(true)
	{
		if(i >= src.size())
		{
			if(hasDecimalPt)
				return stringToNumber<double>(result);
			else
				return stringToNumber<int>(result);
		}

		if(isdigit(src[i]))
		{
			result += src[i++];
		}
		else if(src[i] == '.')
		{
			if(hasDecimalPt)
			{
				PARSE_FAIL
			}
			else
			{
				hasDecimalPt = true;
				result += src[i++];
			}
		}
		else if(src[i] == ',' || src[i] == ']' || src[i] == '}')
		{
			if(hasDecimalPt)
				return stringToNumber<double>(result);
			else
				return stringToNumber<int>(result);
		}
		else
		{
			PARSE_FAIL
		}
	}
}

FlashValue parseArrayValue(const std::string& src, std::string::size_type& i)
{
	FlashValue::Array result;
	i++;

	while(true)
	{
		if(i < src.size())
		{
			if(src[i] == ',')
			{
				i++;
				continue;
			}
		}
		else
		{
			PARSE_FAIL
		}

		if(src[i] == ']')
		{
			i++;
			return FlashValue(result);
		}

		FlashValue val = parseValue(src, i);

		if(i == std::string::npos)
			return FLASH_UNDEFINED;

		result.push_back(val);
	}
}

FlashValue parseObjectValue(const std::string& src, std::string::size_type& i)
{
	FlashValue::Object result;
	i += 2;

	while(true)
	{
		if(i < src.size())
		{
			if(src[i] == ',')
			{
				i++;
				continue;
			}
		}
		else
		{
			PARSE_FAIL
		}

		if(src[i] == '}')
		{
			i += 2;
			return FlashValue(result);
		}

		std::string::size_type colonIdx = src.find(':', i);

		if(colonIdx == std::string::npos)
		{
			PARSE_FAIL
		}

		std::string key = src.substr(i, colonIdx - i);

		i = colonIdx + 1;

		FlashValue val = parseValue(src, i);

		if(i == std::string::npos)
			return FLASH_UNDEFINED;

		result[key] = val;
	}
}