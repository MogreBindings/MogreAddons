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

#ifndef __IdentifierStore_H__
#define __IdentifierStore_H__

#include "PreCompiled.h"

class IdentifierStore
{
public:
	static IdentifierStore& getSingleton();

	NPIdentifier getIdentifier(const char* stringId);
	NPIdentifier getIdentifier(int integerId);

	bool isIdentifierString(NPIdentifier ident);

	const char* identifierToString(NPIdentifier ident);
	int identifierToInteger(NPIdentifier ident);

	void removeIdentifier(NPIdentifier ident);
protected:
	std::map<std::string, NPIdentifier> stringIdentMap;
	std::map<int, NPIdentifier> intIdentMap;
};

#endif