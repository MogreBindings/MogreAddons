This coded needs to be added to the Particle Universe Plugin.

Step one: Make sure the C++ code compiles and your plugin works with Mogre. 
That means updating the Particle Universe Project Depedancies:
There are the ones I used. They will be different as I use a oddball file structure ;)
I've inluded my modified project file here as well.

In Options:
C++ --> General:
AdditionalIncludeDirectories="..\include;&quot;D:\Code\C++\OGRE\OgreSDK_vc10_v1-7-2\boost_1_42&quot;;&quot;D:\Code\C#\MogreSDK\includes&quot;;..\..\..\include"

Linker --> Input:
AdditionalDependencies="D:\Code\C#\MogreSDK\Lib\OgreMain.lib"


Once it compiles on the Mogre Code...

Step two:

Add the following code to: ParticleUniversePrerequisites.h right before the last #endif statement (S/B the last line in the file)

//Added By: Tyler Grusendorf
//Need by wrapper
#define EXPORT extern "C" __declspec(dllexport)
extern char* CreateOutString(const Ogre::String& str);

Step three:

Add the Exports.cpp file to the Project and compile.