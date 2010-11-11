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

#include "IdentifierStore.h"

struct PrivateIdentifier
{
	std::string stringVal;
	int integerVal;
	bool isString;
};

IdentifierStore& IdentifierStore::getSingleton()
{
	static IdentifierStore store;
	return store;
}

NPIdentifier IdentifierStore::getIdentifier(const char* stringId)
{
	std::map<std::string, NPIdentifier>::iterator i = stringIdentMap.find(stringId);

	if(i == stringIdentMap.end())
	{
		PrivateIdentifier* ident = new PrivateIdentifier();
		ident->isString = true;
		ident->stringVal = stringId;

		stringIdentMap[stringId] = ident;

		return (NPIdentifier)ident;
	}
	else
	{
		return i->second;
	}
}

NPIdentifier IdentifierStore::getIdentifier(int integerId)
{
	std::map<int, NPIdentifier>::iterator i = intIdentMap.find(integerId);

	if(i == intIdentMap.end())
	{
		PrivateIdentifier* ident = new PrivateIdentifier();
		ident->isString = false;
		ident->integerVal = integerId;

		intIdentMap[integerId] = ident;

		return (NPIdentifier)ident;
	}
	else
	{
		return i->second;
	}
}

void IdentifierStore::removeIdentifier(NPIdentifier ident)
{
	PrivateIdentifier* pident = (PrivateIdentifier*)ident;

	if(pident->isString)
	{
		std::map<std::string, NPIdentifier>::iterator i = stringIdentMap.find(pident->stringVal);

		if(i != stringIdentMap.end())
		{
			delete i->second;
			stringIdentMap.erase(i);
		}
	}
	else
	{
		std::map<int, NPIdentifier>::iterator i = intIdentMap.find(pident->integerVal);

		if(i != intIdentMap.end())
		{
			delete i->second;
			intIdentMap.erase(i);
		}
	}

}

bool IdentifierStore::isIdentifierString(NPIdentifier ident)
{
	return static_cast<PrivateIdentifier*>(ident)->isString;
}

const char* IdentifierStore::identifierToString(NPIdentifier ident)
{
	return static_cast<PrivateIdentifier*>(ident)->stringVal.c_str();
}

int IdentifierStore::identifierToInteger(NPIdentifier ident)
{
	return static_cast<PrivateIdentifier*>(ident)->integerVal;
}