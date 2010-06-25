/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISForceFeedback.h"
#include "MOISEffect.h"

namespace MOIS
{
	//################################################################
	//ForceFeedback
	//################################################################
	
	//Nested Types
	#define STLDECL_MANAGEDKEY MOIS::Effect::EForce
	#define STLDECL_MANAGEDVALUE MOIS::Effect::EType
	#define STLDECL_NATIVEKEY OIS::Effect::EForce
	#define STLDECL_NATIVEVALUE OIS::Effect::EType
	CPP_DECLARE_STLMAP( ForceFeedback::, SupportedEffectList, STLDECL_MANAGEDKEY, STLDECL_MANAGEDVALUE, STLDECL_NATIVEKEY, STLDECL_NATIVEVALUE )
	#undef STLDECL_MANAGEDKEY
	#undef STLDECL_MANAGEDVALUE
	#undef STLDECL_NATIVEKEY
	#undef STLDECL_NATIVEVALUE
	
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	short ForceFeedback::FFAxesNumber::get()
	{
		return static_cast<OIS::ForceFeedback*>(_native)->getFFAxesNumber( );
	}
	
	void ForceFeedback::SetMasterGain( float level )
	{
		static_cast<OIS::ForceFeedback*>(_native)->setMasterGain( level );
	}
	
	void ForceFeedback::SetAutoCenterMode( bool auto_on )
	{
		static_cast<OIS::ForceFeedback*>(_native)->setAutoCenterMode( auto_on );
	}
	
	void ForceFeedback::Upload( MOIS::Effect^ effect )
	{
		static_cast<OIS::ForceFeedback*>(_native)->upload( effect );
	}
	
	void ForceFeedback::Modify( MOIS::Effect^ effect )
	{
		static_cast<OIS::ForceFeedback*>(_native)->modify( effect );
	}
	
	void ForceFeedback::Remove( MOIS::Effect^ effect )
	{
		static_cast<OIS::ForceFeedback*>(_native)->remove( effect );
	}
	
	MOIS::ForceFeedback::Const_SupportedEffectList^ ForceFeedback::GetSupportedEffects( )
	{
		return static_cast<const OIS::ForceFeedback*>(_native)->getSupportedEffects( );
	}
	
	void ForceFeedback::_addEffectTypes( MOIS::Effect::EForce force, MOIS::Effect::EType type )
	{
		static_cast<OIS::ForceFeedback*>(_native)->_addEffectTypes( (OIS::Effect::EForce)force, (OIS::Effect::EType)type );
	}
	
	
	//Protected Declarations
	
	

}
