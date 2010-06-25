#include "Stdafx.h"

#include "Marshalling.h"

namespace MOIS
{
	void InitNativeStringWithCLRString(std::string& ostr, System::String^ mstr)
	{
		if (mstr == nullptr)
			throw gcnew System::NullReferenceException("A null string cannot be converted to an Ogre string.");

		IntPtr p_mstr = Marshal::StringToHGlobalAnsi(mstr);
		ostr = static_cast<char*>(p_mstr.ToPointer());
		Marshal::FreeHGlobal( p_mstr );
	}
}