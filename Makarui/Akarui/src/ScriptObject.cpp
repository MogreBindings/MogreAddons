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

#include "ScriptObject.h"

void ScriptObject::construct()
{
	properties = new std::map<NPIdentifier, NPVariant*>();
	methodHandler = 0;
}

void ScriptObject::dispose()
{
	if(properties)
	{
		for(std::map<NPIdentifier, NPVariant*>::iterator i = properties->begin(); i != properties->end(); i++)
		{
			NPN_ReleaseVariantValue(i->second);
			NPN_MemFree(i->second);
		}

		delete properties;
		properties = 0;
	}
}

void ScriptObject::setMethodHandler(ScriptObjectMethodHandler* handler)
{
	methodHandler = handler;
}

bool ScriptObject::hasProperty(NPIdentifier id)
{
	return properties->find(id) != properties->end();
}

bool ScriptObject::getProperty(NPIdentifier id, NPVariant* result)
{
	std::map<NPIdentifier, NPVariant*>::iterator i = properties->find(id);

	if(i != properties->end())
	{
		copyNPVariant(i->second, result);
		return true;
	}
	
	return false;
}

bool ScriptObject::setProperty(NPIdentifier id, const NPVariant* value)
{
	std::map<NPIdentifier, NPVariant*>::iterator i = properties->find(id);

	if(i != properties->end())
	{
	
		NPN_ReleaseVariantValue(i->second);

		copyNPVariant(value, i->second);
	}
	else
	{
		NPVariant* var = reinterpret_cast<NPVariant*>(NPN_MemAlloc(sizeof(NPVariant)));

		copyNPVariant(value, var);

		(*properties)[id] = var;
	}

	return true;
}

bool ScriptObject::removeProperty(NPIdentifier id)
{
	return false;
}

bool ScriptObject::hasMethod(NPIdentifier id)
{
	return true;
}

bool ScriptObject::invoke(NPIdentifier id, const NPVariant *args, int argCount, NPVariant* result)
{
	if(!methodHandler)
		return false;

	return methodHandler->handleInvocation(id, args, argCount, result);
}

bool ScriptObject::evaluate(NPString *script, NPVariant *result)
{
	if(!methodHandler)
		return false;

	return methodHandler->handleEvaluation(script, result);
}

void ScriptObject::toXML(std::stringstream& stream) const
{
	stream << "<object>";
	for(std::map<NPIdentifier, NPVariant*>::const_iterator i = properties->begin(); i != properties->end(); i++)
	{
		stream << "<property id=\"";
		if(NPN_IdentifierIsString(i->first))
			stream << NPN_UTF8FromIdentifier(i->first);
		else
			stream << NPN_IntFromIdentifier(i->first);
		stream << "\">";

		variantToXML(*(i->second), stream);

		stream << "</property>";
	}
	stream << "</object>";
}

NPObject* NPObjectWrapper_Allocate(NPP npp, NPClass *aClass)
{
	NPObjectWrapper* object = (NPObjectWrapper*)NPN_MemAlloc(sizeof(NPObjectWrapper));
	object->scriptObject.construct();

	return reinterpret_cast<NPObject*>(object);
}

void NPObjectWrapper_Deallocate(NPObject *npobj)
{
	NPObjectWrapper* object = reinterpret_cast<NPObjectWrapper*>(npobj);
	object->scriptObject.dispose();

	NPN_MemFree(object);
}

static NPClass ObjectWrapperClass = {	NP_CLASS_STRUCT_VERSION,
										NPObjectWrapper_Allocate,
										NPObjectWrapper_Deallocate,
										0, 0, 0, 0, 0, 0, 0, 0 };

NPClass* NPObjectWrapperClass = &ObjectWrapperClass;