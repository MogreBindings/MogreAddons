change in original source code : 
-------------------------------

flashvalue.h line 193: added struct ManagedReferenceHolder


flashcontrol.cpp line 507: change callback call

delegate.h line 479, change Flashdelegate declaration 

flashsite line 25 : change progid to ShockwaveFlash.ShocwaveFlash.9

flashhandler line 25 : change progid to ShockwaveFlash.ShocwaveFlash.9

//see the change in the hikari folder source code
 

Change log
-----------

-> Change string marshalling of arguments from ansi to unicode when calling a flash function
-> fix incorrect function for unicode marshalling

.NET Hikari Wrapper 
------------------------

Description : A simple wrapper for Hikari, 

Mogre version : 1.6.5
Hikari version : 0.3
Porter : GantZ

How to compile : 
-----------------

-> You must compile first the hikari source using the ogre modified source for MOGRE
then you can compile the hikari wrapper and compile the demo, remember to copy the newly compiled
hikari dll to the directory of the demo exe. alternatively, you can compile hikari as a static lib, so that it is embedded in
the hikariwrapper dll, avoiding the need to copy an hikari dll.

IMPORTANT : 
------------
-> the hikari original source has been modified for the purpose of this wrapper, 
so you can't use the hikari source directly from svn.

Request
--------
If you have request to made, or have spotted a bug, send me a pm
via the ogre forums, user GantZ




