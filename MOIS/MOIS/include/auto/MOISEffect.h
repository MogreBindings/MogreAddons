/*  This file is produced by the C++/CLI AutoWrapper utility.
        Copyright (c) 2006 by Argiris Kirtzidis  */

#pragma once

#include "OISEffect.h"

namespace MOIS
{
	//################################################################
	//Effect
	//################################################################
	
	public ref class Effect
	{
		//Nested Types
	
		public: enum class EForce
		{
			UnknownForce = OIS::Effect::UnknownForce,
			ConstantForce = OIS::Effect::ConstantForce,
			RampForce = OIS::Effect::RampForce,
			PeriodicForce = OIS::Effect::PeriodicForce,
			ConditionalForce = OIS::Effect::ConditionalForce,
			CustomForce = OIS::Effect::CustomForce
		};
	
		public: enum class EType
		{
			Unknown = OIS::Effect::Unknown,
			Constant = OIS::Effect::Constant,
			Ramp = OIS::Effect::Ramp,
			Square = OIS::Effect::Square,
			Triangle = OIS::Effect::Triangle,
			Sine = OIS::Effect::Sine,
			SawToothUp = OIS::Effect::SawToothUp,
			SawToothDown = OIS::Effect::SawToothDown,
			Friction = OIS::Effect::Friction,
			Damper = OIS::Effect::Damper,
			Inertia = OIS::Effect::Inertia,
			Spring = OIS::Effect::Spring,
			Custom = OIS::Effect::Custom
		};
	
		public: enum class EDirection
		{
			NorthWest = OIS::Effect::NorthWest,
			North = OIS::Effect::North,
			NorthEast = OIS::Effect::NorthEast,
			East = OIS::Effect::East,
			SouthEast = OIS::Effect::SouthEast,
			South = OIS::Effect::South,
			SouthWest = OIS::Effect::SouthWest,
			West = OIS::Effect::West
		};
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		Effect( OIS::Effect* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~Effect()
		{
			this->!Effect();
		}
		!Effect()
		{
			if (_createdByCLR &&_native)
			{
				delete _native;
				_native = 0;
			}
		}
	
		OIS::Effect* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
		Effect( MOIS::Effect::EForce ef, MOIS::Effect::EType et );
	
	
		static property unsigned int OIS_INFINITE
		{
		public:
			unsigned int get();
		}
	
		property MOIS::Effect::EForce force
		{
		public:
			MOIS::Effect::EForce get();
		}
	
		property MOIS::Effect::EType type
		{
		public:
			MOIS::Effect::EType get();
		}
	
		property MOIS::Effect::EDirection direction
		{
		public:
			MOIS::Effect::EDirection get();
		public:
			void set(MOIS::Effect::EDirection value);
		}
	
		property short trigger_button
		{
		public:
			short get();
		public:
			void set(short value);
		}
	
		property unsigned int trigger_interval
		{
		public:
			unsigned int get();
		public:
			void set(unsigned int value);
		}
	
		property unsigned int replay_length
		{
		public:
			unsigned int get();
		public:
			void set(unsigned int value);
		}
	
		property unsigned int replay_delay
		{
		public:
			unsigned int get();
		public:
			void set(unsigned int value);
		}
	
		property int _handle
		{
		public:
			int get();
		public:
			void set(int value);
		}
	
		property MOIS::ForceEffect^ ForceEffect
		{
		public:
			MOIS::ForceEffect^ get();
		}
	
		property short NumAxes
		{
		public:
			short get();
		public:
			void set(short nAxes);
		}
	
		inline static operator Effect^ (const OIS::Effect* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::Effect*>(t)) : nullptr;
		}
		inline static operator Effect^ (const OIS::Effect& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::Effect&>(t));
		}
		inline static operator OIS::Effect* (Effect^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::Effect*>(t->_native);
		}
		inline static operator OIS::Effect& (Effect^ t) {
			return *static_cast<OIS::Effect*>(t->_native);
		}
	
		internal: static Effect^ _ToCorrectSubclass(OIS::Effect* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//ForceEffect
	//################################################################
	
	public ref class ForceEffect
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		ForceEffect( OIS::ForceEffect* obj ) : _native(obj), _createdByCLR(false)
		{
		}
	
		~ForceEffect()
		{
			this->!ForceEffect();
		}
		!ForceEffect()
		{
			if (_createdByCLR &&_native)
			{
				delete _native;
				_native = 0;
			}
		}
	
		OIS::ForceEffect* _native;
		bool _createdByCLR;
	
	
		//Public Declarations
	public:
		ForceEffect();
	
	
		inline static operator ForceEffect^ (const OIS::ForceEffect* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::ForceEffect*>(t)) : nullptr;
		}
		inline static operator ForceEffect^ (const OIS::ForceEffect& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::ForceEffect&>(t));
		}
		inline static operator OIS::ForceEffect* (ForceEffect^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::ForceEffect*>(t->_native);
		}
		inline static operator OIS::ForceEffect& (ForceEffect^ t) {
			return *static_cast<OIS::ForceEffect*>(t->_native);
		}
	
		internal: static ForceEffect^ _ToCorrectSubclass(OIS::ForceEffect* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//Envelope
	//################################################################
	
	public ref class Envelope : public ForceEffect
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		Envelope( OIS::ForceEffect* obj ) : ForceEffect(obj)
		{
		}
	
	
		//Public Declarations
	public:
		Envelope( );
	
	
		property unsigned short attackLength
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property unsigned short attackLevel
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property unsigned short fadeLength
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property unsigned short fadeLevel
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property bool IsUsed
		{
		public:
			bool get();
		}
	
		inline static operator Envelope^ (const OIS::Envelope* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::Envelope*>(t)) : nullptr;
		}
		inline static operator Envelope^ (const OIS::Envelope& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::Envelope&>(t));
		}
		inline static operator OIS::Envelope* (Envelope^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::Envelope*>(t->_native);
		}
		inline static operator OIS::Envelope& (Envelope^ t) {
			return *static_cast<OIS::Envelope*>(t->_native);
		}
	
		internal: static Envelope^ _ToCorrectSubclass(OIS::Envelope* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//ConstantEffect
	//################################################################
	
	public ref class ConstantEffect : public ForceEffect
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		ConstantEffect( OIS::ForceEffect* obj ) : ForceEffect(obj)
		{
		}
	
	
		//Public Declarations
	public:
		ConstantEffect( );
	
	
		property MOIS::Envelope^ envelope
		{
		public:
			MOIS::Envelope^ get();
		public:
			void set(MOIS::Envelope^ value);
		}
	
		property signed short level
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		inline static operator ConstantEffect^ (const OIS::ConstantEffect* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::ConstantEffect*>(t)) : nullptr;
		}
		inline static operator ConstantEffect^ (const OIS::ConstantEffect& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::ConstantEffect&>(t));
		}
		inline static operator OIS::ConstantEffect* (ConstantEffect^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::ConstantEffect*>(t->_native);
		}
		inline static operator OIS::ConstantEffect& (ConstantEffect^ t) {
			return *static_cast<OIS::ConstantEffect*>(t->_native);
		}
	
		internal: static ConstantEffect^ _ToCorrectSubclass(OIS::ConstantEffect* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//RampEffect
	//################################################################
	
	public ref class RampEffect : public ForceEffect
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		RampEffect( OIS::ForceEffect* obj ) : ForceEffect(obj)
		{
		}
	
	
		//Public Declarations
	public:
		RampEffect( );
	
	
		property MOIS::Envelope^ envelope
		{
		public:
			MOIS::Envelope^ get();
		public:
			void set(MOIS::Envelope^ value);
		}
	
		property signed short startLevel
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		property signed short endLevel
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		inline static operator RampEffect^ (const OIS::RampEffect* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::RampEffect*>(t)) : nullptr;
		}
		inline static operator RampEffect^ (const OIS::RampEffect& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::RampEffect&>(t));
		}
		inline static operator OIS::RampEffect* (RampEffect^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::RampEffect*>(t->_native);
		}
		inline static operator OIS::RampEffect& (RampEffect^ t) {
			return *static_cast<OIS::RampEffect*>(t->_native);
		}
	
		internal: static RampEffect^ _ToCorrectSubclass(OIS::RampEffect* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//PeriodicEffect
	//################################################################
	
	public ref class PeriodicEffect : public ForceEffect
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		PeriodicEffect( OIS::ForceEffect* obj ) : ForceEffect(obj)
		{
		}
	
	
		//Public Declarations
	public:
		PeriodicEffect( );
	
	
		property MOIS::Envelope^ envelope
		{
		public:
			MOIS::Envelope^ get();
		public:
			void set(MOIS::Envelope^ value);
		}
	
		property unsigned short magnitude
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property signed short offset
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		property unsigned short phase
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property unsigned int period
		{
		public:
			unsigned int get();
		public:
			void set(unsigned int value);
		}
	
		inline static operator PeriodicEffect^ (const OIS::PeriodicEffect* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::PeriodicEffect*>(t)) : nullptr;
		}
		inline static operator PeriodicEffect^ (const OIS::PeriodicEffect& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::PeriodicEffect&>(t));
		}
		inline static operator OIS::PeriodicEffect* (PeriodicEffect^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::PeriodicEffect*>(t->_native);
		}
		inline static operator OIS::PeriodicEffect& (PeriodicEffect^ t) {
			return *static_cast<OIS::PeriodicEffect*>(t->_native);
		}
	
		internal: static PeriodicEffect^ _ToCorrectSubclass(OIS::PeriodicEffect* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	
	//################################################################
	//ConditionalEffect
	//################################################################
	
	public ref class ConditionalEffect : public ForceEffect
	{
		//Nested Types
	
		//Private Declarations
	private protected:
	
		//Internal Declarations
	internal:
		ConditionalEffect( OIS::ForceEffect* obj ) : ForceEffect(obj)
		{
		}
	
	
		//Public Declarations
	public:
		ConditionalEffect( );
	
	
		property signed short rightCoeff
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		property signed short leftCoeff
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		property unsigned short rightSaturation
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property unsigned short leftSaturation
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property unsigned short deadband
		{
		public:
			unsigned short get();
		public:
			void set(unsigned short value);
		}
	
		property signed short center
		{
		public:
			signed short get();
		public:
			void set(signed short value);
		}
	
		inline static operator ConditionalEffect^ (const OIS::ConditionalEffect* t)
		{
			return (t) ? _ToCorrectSubclass(const_cast<OIS::ConditionalEffect*>(t)) : nullptr;
		}
		inline static operator ConditionalEffect^ (const OIS::ConditionalEffect& t)
		{
			return _ToCorrectSubclass(&const_cast<OIS::ConditionalEffect&>(t));
		}
		inline static operator OIS::ConditionalEffect* (ConditionalEffect^ t) {
			return (t == CLR_NULL) ? 0 : static_cast<OIS::ConditionalEffect*>(t->_native);
		}
		inline static operator OIS::ConditionalEffect& (ConditionalEffect^ t) {
			return *static_cast<OIS::ConditionalEffect*>(t->_native);
		}
	
		internal: static ConditionalEffect^ _ToCorrectSubclass(OIS::ConditionalEffect* t);
		public:
	
		//Protected Declarations
	protected public:
	
	};
	

}
