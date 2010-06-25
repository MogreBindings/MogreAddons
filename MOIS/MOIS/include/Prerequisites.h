#pragma once

// Compiler has a bug that emits this warning when "protected public" access modifier
// is used for forward class declarations
#pragma warning( disable : 4240 )

#define STATIC_ASSERT(A) typedef int __assertchecker##__COUNTER__[(A) ? +1 : -1];

#include "CLRHelp.h"
#include "Marshalling.h"
//We are not using CLRObjects inside OIS, so Wrapper.h is not needed
//#include "Wrapper.h"
#include "STLContainerWrappers.h"

namespace MOIS
{
	using namespace System::Collections;
	using namespace System::Collections::Specialized;
	//using namespace MOIS::Implementation;

	typedef System::String String;

	#include "PreDeclarations.h"
}