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

#ifndef __ScriptObject_H__
#define __ScriptObject_H__

#include "PreCompiled.h"

class ScriptObjectMethodHandler;

struct ScriptObject
{
	std::map<NPIdentifier, NPVariant*>* properties;
	ScriptObjectMethodHandler* methodHandler;

	void construct();

	void dispose();

	void setMethodHandler(ScriptObjectMethodHandler* handler);

	bool hasProperty(NPIdentifier id);

	bool getProperty(NPIdentifier id, NPVariant* result);

	bool setProperty(NPIdentifier id, const NPVariant* value);

	bool removeProperty(NPIdentifier id);

	bool hasMethod(NPIdentifier id);

	bool invoke(NPIdentifier id, const NPVariant *args, int argCount, NPVariant* result);

	bool evaluate(NPString *script, NPVariant *result);

	void toXML(std::wstringstream& stream) const;
};


struct NPObjectWrapper
{
	NPObject npObject;
	ScriptObject scriptObject;
};

extern NPClass* NPObjectWrapperClass;

#endif