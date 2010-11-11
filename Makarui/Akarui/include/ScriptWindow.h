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

#ifndef __ScriptWindow_H__
#define __ScriptWindow_H__

#include "PreCompiled.h"
#include "ScriptObject.h"

class PluginInstance;
struct NPObjectWrapper;
struct ScriptObject;

class ScriptObjectMethodHandler
{
public:
	virtual ~ScriptObjectMethodHandler() {}
	virtual bool handleInvocation(NPIdentifier id, const NPVariant *args, int argCount, NPVariant* result) = 0;
	virtual bool handleEvaluation(NPString *script, NPVariant *result) = 0;
};


class ScriptWindow : public ScriptObjectMethodHandler
{
	NPObjectWrapper* npObject;
	std::string location;
	PluginInstance* owner;

	bool handleInvocation(NPIdentifier id, const NPVariant *args, int argCount, NPVariant* result);
	bool handleEvaluation(NPString *script, NPVariant *result);
public:
	ScriptWindow(const std::string& location, PluginInstance* owner);
	~ScriptWindow();

	NPObject* getNPObject();
	ScriptObject* getScriptObject();

	std::string getLocation();
};

#endif