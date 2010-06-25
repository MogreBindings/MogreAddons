/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#include "stdafx.h"

#include "MOISEffect.h"

namespace MOIS
{
	//################################################################
	//Effect
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	Effect::Effect( MOIS::Effect::EForce ef, MOIS::Effect::EType et )
	{
		_createdByCLR = true;
		_native = new OIS::Effect( (OIS::Effect::EForce)ef, (OIS::Effect::EType)et);
	}
	
	unsigned int Effect::OIS_INFINITE::get()
	{
		return OIS::Effect::OIS_INFINITE;
	}
	
	MOIS::Effect::EForce Effect::force::get()
	{
		return (MOIS::Effect::EForce)static_cast<OIS::Effect*>(_native)->force;
	}
	
	MOIS::Effect::EType Effect::type::get()
	{
		return (MOIS::Effect::EType)static_cast<OIS::Effect*>(_native)->type;
	}
	
	MOIS::Effect::EDirection Effect::direction::get()
	{
		return (MOIS::Effect::EDirection)static_cast<OIS::Effect*>(_native)->direction;
	}
	void Effect::direction::set( MOIS::Effect::EDirection value )
	{
		static_cast<OIS::Effect*>(_native)->direction = (OIS::Effect::EDirection)value;
	}
	
	short Effect::trigger_button::get()
	{
		return static_cast<OIS::Effect*>(_native)->trigger_button;
	}
	void Effect::trigger_button::set( short value )
	{
		static_cast<OIS::Effect*>(_native)->trigger_button = value;
	}
	
	unsigned int Effect::trigger_interval::get()
	{
		return static_cast<OIS::Effect*>(_native)->trigger_interval;
	}
	void Effect::trigger_interval::set( unsigned int value )
	{
		static_cast<OIS::Effect*>(_native)->trigger_interval = value;
	}
	
	unsigned int Effect::replay_length::get()
	{
		return static_cast<OIS::Effect*>(_native)->replay_length;
	}
	void Effect::replay_length::set( unsigned int value )
	{
		static_cast<OIS::Effect*>(_native)->replay_length = value;
	}
	
	unsigned int Effect::replay_delay::get()
	{
		return static_cast<OIS::Effect*>(_native)->replay_delay;
	}
	void Effect::replay_delay::set( unsigned int value )
	{
		static_cast<OIS::Effect*>(_native)->replay_delay = value;
	}
	
	int Effect::_handle::get()
	{
		return static_cast<OIS::Effect*>(_native)->_handle;
	}
	void Effect::_handle::set( int value )
	{
		static_cast<OIS::Effect*>(_native)->_handle = value;
	}
	
	MOIS::ForceEffect^ Effect::ForceEffect::get()
	{
		return static_cast<const OIS::Effect*>(_native)->getForceEffect( );
	}
	
	short Effect::NumAxes::get()
	{
		return static_cast<const OIS::Effect*>(_native)->getNumAxes( );
	}
	void Effect::NumAxes::set( short nAxes )
	{
		static_cast<OIS::Effect*>(_native)->setNumAxes( nAxes );
	}
	
	Effect^ Effect::_ToCorrectSubclass(OIS::Effect* t)
	{
		OIS::Effect* subptr;
		return gcnew Effect(t);
	}
	
	//Protected Declarations
	
	
	//################################################################
	//ForceEffect
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	ForceEffect::ForceEffect()
	{
		_createdByCLR = true;
		_native = new OIS::ForceEffect();
	}
	
	ForceEffect^ ForceEffect::_ToCorrectSubclass(OIS::ForceEffect* t)
	{
		OIS::ForceEffect* subptr;
		subptr = dynamic_cast<OIS::ConditionalEffect*>(t);
		if (subptr)
			return ConditionalEffect::_ToCorrectSubclass(t);
	
		subptr = dynamic_cast<OIS::ConstantEffect*>(t);
		if (subptr)
			return ConstantEffect::_ToCorrectSubclass(t);
	
		subptr = dynamic_cast<OIS::Envelope*>(t);
		if (subptr)
			return Envelope::_ToCorrectSubclass(t);
	
		subptr = dynamic_cast<OIS::PeriodicEffect*>(t);
		if (subptr)
			return PeriodicEffect::_ToCorrectSubclass(t);
	
		subptr = dynamic_cast<OIS::RampEffect*>(t);
		if (subptr)
			return RampEffect::_ToCorrectSubclass(t);
	
		return gcnew ForceEffect(t);
	}
	
	//Protected Declarations
	
	
	//################################################################
	//Envelope
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	Envelope::Envelope( ) : ForceEffect((OIS::ForceEffect*) 0)
	{
		_createdByCLR = true;
		_native = new OIS::Envelope();
	}
	
	unsigned short Envelope::attackLength::get()
	{
		return static_cast<OIS::Envelope*>(_native)->attackLength;
	}
	void Envelope::attackLength::set( unsigned short value )
	{
		static_cast<OIS::Envelope*>(_native)->attackLength = value;
	}
	
	unsigned short Envelope::attackLevel::get()
	{
		return static_cast<OIS::Envelope*>(_native)->attackLevel;
	}
	void Envelope::attackLevel::set( unsigned short value )
	{
		static_cast<OIS::Envelope*>(_native)->attackLevel = value;
	}
	
	unsigned short Envelope::fadeLength::get()
	{
		return static_cast<OIS::Envelope*>(_native)->fadeLength;
	}
	void Envelope::fadeLength::set( unsigned short value )
	{
		static_cast<OIS::Envelope*>(_native)->fadeLength = value;
	}
	
	unsigned short Envelope::fadeLevel::get()
	{
		return static_cast<OIS::Envelope*>(_native)->fadeLevel;
	}
	void Envelope::fadeLevel::set( unsigned short value )
	{
		static_cast<OIS::Envelope*>(_native)->fadeLevel = value;
	}
	
	bool Envelope::IsUsed::get()
	{
		return static_cast<OIS::Envelope*>(_native)->isUsed( );
	}
	
	Envelope^ Envelope::_ToCorrectSubclass(OIS::Envelope* t)
	{
		OIS::Envelope* subptr;
		return gcnew Envelope(t);
	}
	
	//Protected Declarations
	
	
	//################################################################
	//ConstantEffect
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	ConstantEffect::ConstantEffect( ) : ForceEffect((OIS::ForceEffect*) 0)
	{
		_createdByCLR = true;
		_native = new OIS::ConstantEffect();
	}
	
	MOIS::Envelope^ ConstantEffect::envelope::get()
	{
		return static_cast<OIS::ConstantEffect*>(_native)->envelope;
	}
	void ConstantEffect::envelope::set( MOIS::Envelope^ value )
	{
		static_cast<OIS::ConstantEffect*>(_native)->envelope = value;
	}
	
	signed short ConstantEffect::level::get()
	{
		return static_cast<OIS::ConstantEffect*>(_native)->level;
	}
	void ConstantEffect::level::set( signed short value )
	{
		static_cast<OIS::ConstantEffect*>(_native)->level = value;
	}
	
	ConstantEffect^ ConstantEffect::_ToCorrectSubclass(OIS::ConstantEffect* t)
	{
		OIS::ConstantEffect* subptr;
		return gcnew ConstantEffect(t);
	}
	
	//Protected Declarations
	
	
	//################################################################
	//RampEffect
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	RampEffect::RampEffect( ) : ForceEffect((OIS::ForceEffect*) 0)
	{
		_createdByCLR = true;
		_native = new OIS::RampEffect();
	}
	
	MOIS::Envelope^ RampEffect::envelope::get()
	{
		return static_cast<OIS::RampEffect*>(_native)->envelope;
	}
	void RampEffect::envelope::set( MOIS::Envelope^ value )
	{
		static_cast<OIS::RampEffect*>(_native)->envelope = value;
	}
	
	signed short RampEffect::startLevel::get()
	{
		return static_cast<OIS::RampEffect*>(_native)->startLevel;
	}
	void RampEffect::startLevel::set( signed short value )
	{
		static_cast<OIS::RampEffect*>(_native)->startLevel = value;
	}
	
	signed short RampEffect::endLevel::get()
	{
		return static_cast<OIS::RampEffect*>(_native)->endLevel;
	}
	void RampEffect::endLevel::set( signed short value )
	{
		static_cast<OIS::RampEffect*>(_native)->endLevel = value;
	}
	
	RampEffect^ RampEffect::_ToCorrectSubclass(OIS::RampEffect* t)
	{
		OIS::RampEffect* subptr;
		return gcnew RampEffect(t);
	}
	
	//Protected Declarations
	
	
	//################################################################
	//PeriodicEffect
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	PeriodicEffect::PeriodicEffect( ) : ForceEffect((OIS::ForceEffect*) 0)
	{
		_createdByCLR = true;
		_native = new OIS::PeriodicEffect();
	}
	
	MOIS::Envelope^ PeriodicEffect::envelope::get()
	{
		return static_cast<OIS::PeriodicEffect*>(_native)->envelope;
	}
	void PeriodicEffect::envelope::set( MOIS::Envelope^ value )
	{
		static_cast<OIS::PeriodicEffect*>(_native)->envelope = value;
	}
	
	unsigned short PeriodicEffect::magnitude::get()
	{
		return static_cast<OIS::PeriodicEffect*>(_native)->magnitude;
	}
	void PeriodicEffect::magnitude::set( unsigned short value )
	{
		static_cast<OIS::PeriodicEffect*>(_native)->magnitude = value;
	}
	
	signed short PeriodicEffect::offset::get()
	{
		return static_cast<OIS::PeriodicEffect*>(_native)->offset;
	}
	void PeriodicEffect::offset::set( signed short value )
	{
		static_cast<OIS::PeriodicEffect*>(_native)->offset = value;
	}
	
	unsigned short PeriodicEffect::phase::get()
	{
		return static_cast<OIS::PeriodicEffect*>(_native)->phase;
	}
	void PeriodicEffect::phase::set( unsigned short value )
	{
		static_cast<OIS::PeriodicEffect*>(_native)->phase = value;
	}
	
	unsigned int PeriodicEffect::period::get()
	{
		return static_cast<OIS::PeriodicEffect*>(_native)->period;
	}
	void PeriodicEffect::period::set( unsigned int value )
	{
		static_cast<OIS::PeriodicEffect*>(_native)->period = value;
	}
	
	PeriodicEffect^ PeriodicEffect::_ToCorrectSubclass(OIS::PeriodicEffect* t)
	{
		OIS::PeriodicEffect* subptr;
		return gcnew PeriodicEffect(t);
	}
	
	//Protected Declarations
	
	
	//################################################################
	//ConditionalEffect
	//################################################################
	
	//Nested Types
	//Private Declarations
	
	//Internal Declarations
	
	//Public Declarations
	ConditionalEffect::ConditionalEffect( ) : ForceEffect((OIS::ForceEffect*) 0)
	{
		_createdByCLR = true;
		_native = new OIS::ConditionalEffect();
	}
	
	signed short ConditionalEffect::rightCoeff::get()
	{
		return static_cast<OIS::ConditionalEffect*>(_native)->rightCoeff;
	}
	void ConditionalEffect::rightCoeff::set( signed short value )
	{
		static_cast<OIS::ConditionalEffect*>(_native)->rightCoeff = value;
	}
	
	signed short ConditionalEffect::leftCoeff::get()
	{
		return static_cast<OIS::ConditionalEffect*>(_native)->leftCoeff;
	}
	void ConditionalEffect::leftCoeff::set( signed short value )
	{
		static_cast<OIS::ConditionalEffect*>(_native)->leftCoeff = value;
	}
	
	unsigned short ConditionalEffect::rightSaturation::get()
	{
		return static_cast<OIS::ConditionalEffect*>(_native)->rightSaturation;
	}
	void ConditionalEffect::rightSaturation::set( unsigned short value )
	{
		static_cast<OIS::ConditionalEffect*>(_native)->rightSaturation = value;
	}
	
	unsigned short ConditionalEffect::leftSaturation::get()
	{
		return static_cast<OIS::ConditionalEffect*>(_native)->leftSaturation;
	}
	void ConditionalEffect::leftSaturation::set( unsigned short value )
	{
		static_cast<OIS::ConditionalEffect*>(_native)->leftSaturation = value;
	}
	
	unsigned short ConditionalEffect::deadband::get()
	{
		return static_cast<OIS::ConditionalEffect*>(_native)->deadband;
	}
	void ConditionalEffect::deadband::set( unsigned short value )
	{
		static_cast<OIS::ConditionalEffect*>(_native)->deadband = value;
	}
	
	signed short ConditionalEffect::center::get()
	{
		return static_cast<OIS::ConditionalEffect*>(_native)->center;
	}
	void ConditionalEffect::center::set( signed short value )
	{
		static_cast<OIS::ConditionalEffect*>(_native)->center = value;
	}
	
	ConditionalEffect^ ConditionalEffect::_ToCorrectSubclass(OIS::ConditionalEffect* t)
	{
		OIS::ConditionalEffect* subptr;
		return gcnew ConditionalEffect(t);
	}
	
	//Protected Declarations
	
	

}
