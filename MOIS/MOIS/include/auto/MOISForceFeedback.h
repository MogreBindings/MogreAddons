/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISForceFeedback.h"
#include "MOISInterface.h"
#include "MOISEffect.h"

namespace MOIS
{
	//################################################################
	//ForceFeedback
	//################################################################
	
	public ref class ForceFeedback : public Interface
	{
		//Nested Types
		public: ref class SupportedEffectList;
	
		#define STLDECL_MANAGEDKEY MOIS::Effect::EForce
		#define STLDECL_MANAGEDVALUE MOIS::Effect::EType
		#define STLDECL_NATIVEKEY OIS::Effect::EForce
		#define STLDECL_NATIVEVALUE OIS::Effect::EType
		public: INC_DECLARE_STLMAP( SupportedEffectList, STLDECL_MANAGEDKEY, STLDECL_MANAGEDVALUE, STLDECL_NATIVEKEY, STLDECL_NATIVEVALUE, public: , private: )
		#undef STLDECL_MANAGEDKEY
		#undef STLDECL_MANAGEDVALUE
		#undef STLDECL_NATIVEKEY
		#undef STLDECL_NATIVEVALUE
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		ForceFeedback( OIS::Interface* obj ) : Interface(obj)
		{
		}
	
	
		//Public Declarations
	public:
	
	
		property short FFAxesNumber
		{
		public:
			short get();
		}
	
		void SetMasterGain( float level );
	
		void SetAutoCenterMode( bool auto_on );
	
		void Upload( MOIS::Effect^ effect );
	
		void Modify( MOIS::Effect^ effect );
	
		void Remove( MOIS::Effect^ effect );
	
		MOIS::ForceFeedback::Const_SupportedEffectList^ GetSupportedEffects( );
	
		void _addEffectTypes( MOIS::Effect::EForce force, MOIS::Effect::EType type );
	
		DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER( ForceFeedback )
	
		//Protected Declarations
	protected public:
	
	};
	

}
