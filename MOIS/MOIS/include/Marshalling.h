#pragma once

#include <vcclr.h>
#include "OISPrereqs.h"
#include "Custom\MOISPair.h"

namespace MOIS
{
	using namespace System;
	using namespace System::Runtime::InteropServices;

	#define CLR_NULL ((System::Object^)nullptr)


	#define DECLARE_NATIVE_STRING(nvar,mstr)									\
		std::string nvar;													\
		InitNativeStringWithCLRString(nvar,mstr);

	#define SET_NATIVE_STRING(nvar,mstr)		InitNativeStringWithCLRString(nvar,mstr);
	#define TO_CLR_STRING(ogrestr)			gcnew System::String((ogrestr).c_str())

	void InitNativeStringWithCLRString(std::string& ostr, System::String^ mstr);


	#if USE_CLROBJECT_CLASS
		#define RETURN_CLR_OBJECT(T,n)				\
				if (n == 0) return nullptr;			\
				System::Object^ clr = *n;					\
				if (nullptr == clr) {				\
					n->_Init_CLRObject();			\
					clr = *n;						\
				}									\
				return static_cast<T^>(clr);
	#else
		#define RETURN_CLR_OBJECT(T,n) return gcnew T( n );
	#endif

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS(T)						\
			static operator T^ (const OIS::T* t) {						\
				RETURN_CLR_OBJECT(T, (const_cast<OIS::T*>(t)) )		\
			}															\
			static operator T^ (const OIS::T& t) {						\
				RETURN_CLR_OBJECT(T, (&const_cast<OIS::T&>(t)) )		\
			}															\
			inline static operator OIS::T* (T^ t) {					\
				return (t == CLR_NULL) ? 0 : static_cast<OIS::T*>(t->_native);		\
			}															\
			inline static operator OIS::T& (T^ t) {					\
				return *static_cast<OIS::T*>(t->_native);				\
			}

	#define RETURN_CLR_OBJECT_FOR_INTERFACE(MT, NT, n)			\
			if (0 == n) return nullptr;							\
			CLRObject* clr_obj = dynamic_cast<CLRObject*>(n);	\
			if (0 == clr_obj)									\
				throw gcnew System::Exception("The native class that implements " #NT " isn't a subclass of CLRObject. Cannot create the CLR wrapper object.");	\
			System::Object^ clr = *clr_obj;						\
			if (nullptr == clr) {								\
				clr_obj->_Init_CLRObject();						\
				clr = *clr_obj;									\
			}													\
			return static_cast<MT^>(clr);

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_INTERFACE(MT, NT)				\
			static operator MT^ (const NT* t) {									\
				RETURN_CLR_OBJECT_FOR_INTERFACE(MT, NT, (const_cast<NT*>(t)) )	\
			}																	\
			inline static operator NT* (MT^ t) {								\
				return (t == CLR_NULL) ? 0 : t->_GetNativePtr();				\
			}

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_SHAREDPTR(T)					\
			inline static operator T^ (const OIS::T& ptr) {					\
				return gcnew T(const_cast<OIS::T&>(ptr));						\
			}																	\
			inline static operator T^ (const OIS::T* ptr) {					\
				return gcnew T(*const_cast<OIS::T*>(ptr));						\
			}																	\
			inline static operator OIS::T& (T^ t) {							\
				return *(t->_sharedPtr);										\
			}																	\
			inline static operator OIS::T* (T^ t) {							\
				return (t == CLR_NULL) ? 0 : t->_sharedPtr;						\
			}

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_VALUECLASS(T)			\
			inline static operator OIS::T& (T& obj)					\
			{															\
				return reinterpret_cast<OIS::T&>(obj);					\
			}															\
			inline static operator const T& ( const OIS::T& obj)		\
			{															\
				return reinterpret_cast<const T&>(obj);					\
			}															\
			inline static operator const T& ( const OIS::T* pobj)		\
			{															\
				return reinterpret_cast<const T&>(*pobj);				\
			}

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_NATIVEPTRVALUECLASS(MT, NT)	\
			inline static operator NT& (MT mobj)								\
			{																	\
				return *mobj._native;											\
			}																	\
			inline static operator NT* (MT mobj)								\
			{																	\
				return mobj._native;											\
			}																	\
			inline static operator MT ( const NT& obj)							\
			{																	\
				MT clrobj;														\
				clrobj._native = const_cast<NT*>(&obj);							\
				return clrobj;													\
			}																	\
			inline static operator MT ( const NT* pobj)							\
			{																	\
				MT clrobj;														\
				clrobj._native = const_cast<NT*>(pobj);							\
				return clrobj;													\
			}

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_CLRHANDLE(T)			\
			static operator T^ (const OIS::T* ct) {					\
				Ogre::T* t = const_cast<OIS::T*>(ct);					\
				if (t)													\
				{														\
					System::Object^ clr = t->_CLRHandle;				\
					if (CLR_NULL == clr)								\
					{													\
						clr = gcnew T(t);								\
						t->_CLRHandle = clr;							\
					}													\
					return static_cast<T^>(clr);						\
				}														\
				else													\
					return nullptr;										\
			}															\
			static operator T^ (const OIS::T& ct) {					\
				Ogre::T* t = &const_cast<OIS::T&>(ct);					\
				System::Object^ clr = t->_CLRHandle;					\
				if (CLR_NULL == clr)									\
				{														\
					clr = gcnew T(t);									\
					t->_CLRHandle = clr;								\
				}														\
				return static_cast<T^>(clr);							\
			}															\
			inline static operator OIS::T* (T^ t) {					\
				return (t == CLR_NULL) ? 0 : t->_native;				\
			}															\
			inline static operator OIS::T& (T^ t) {					\
				return *t->_native;										\
			}

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER(T)		\
			inline static operator T^ (const OIS::T* t) {				\
				if (t)													\
					return gcnew T(const_cast<OIS::T*>(t));			\
				else													\
					return nullptr;										\
			}															\
			inline static operator T^ (const OIS::T& t) {				\
				return gcnew T(&const_cast<OIS::T&>(t));				\
			}															\
			inline static operator OIS::T* (T^ t) {					\
			return (t == CLR_NULL) ? 0 : static_cast<OIS::T*>(t->_native);		\
			}															\
			inline static operator OIS::T& (T^ t) {					\
				return *static_cast<OIS::T*>(t->_native);				\
			}

	#define DEFINE_MANAGED_NATIVE_CONVERSIONS_FOR_PLAINWRAPPER_EXPLICIT(MT, NT)		\
			inline static operator MT^ (const OIS::NT* t) {			\
				if (t)													\
					return gcnew MT(const_cast<OIS::NT*>(t));			\
				else													\
					return nullptr;										\
			}															\
			inline static operator MT^ (const OIS::NT& t) {			\
				return gcnew MT(&const_cast<OIS::NT&>(t));				\
			}															\
			inline static operator OIS::NT* (MT^ t) {					\
			return (t == CLR_NULL) ? 0 : static_cast<OIS::NT*>(t->_native);		\
			}															\
			inline static operator OIS::NT& (MT^ t) {					\
				return *static_cast<OIS::NT*>(t->_native);				\
			}


	// ToNative and ToManaged are used to simplify conversions inside templates

	template <typename M, typename N>
	inline N ToNative(M value)
	{
		return (N)value;
	}

	template <typename M, typename N>
	inline M ToManaged(const N& value)
	{
		return (M)const_cast<N&>(value);
	}

	template <>
	inline std::string ToNative(System::String^ str)
	{
		DECLARE_NATIVE_STRING( o_str, str )
		return o_str;
	}

	template <>
	inline System::String^ ToManaged(const std::string& str)
	{
		return TO_CLR_STRING(str);
	}

	template <typename M, typename N>
	inline std::pair<typename N::first_type, typename N::second_type> ToNative(Pair<typename M::first_type, typename M::second_type> value)
	{
		return std::pair<N::first_type, N::second_type>(ToNative<M::first_type,N::first_type>(value.first), ToNative<M::second_type,N::second_type>(value.second));
	}

	template <typename M, typename N>
	inline Pair<typename M::first_type, typename M::second_type> ToManaged(const std::pair<typename N::first_type, typename N::second_type>& value)
	{
		return Pair<typename M::first_type, typename M::second_type>(ToManaged<M::first_type,N::first_type>(value.first), ToManaged<M::second_type,N::second_type>(value.second));
	}
}