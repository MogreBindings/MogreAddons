#include <gcroot.h>

#include "OgreAxisAlignedBox.h"
#include "OgreCamera.h"
#include "OgreMaterial.h"
#include "OgreVector3.h"
#include "OgreString.h"

#include "OgrePagingLandScapeCallBackEvent.h"


#define CLR_NULL ((Object^)nullptr)

#if OGRE_WCHAR_T_STRINGS
	#define DECLARE_OGRE_STRING(nvar,mstr)									\
		pin_ptr<const wchar_t> p_##nvar = PtrToStringChars(mstr);			\
		Ogre::String nvar( p_##nvar );

	#define SET_OGRE_STRING(nvar,mstr)										\
		pin_ptr<const wchar_t> __p_ostr##__COUNTER__ = PtrToStringChars(mstr);	\
		nvar = __p_ostr##__COUNTER__;

	#define TO_CLR_STRING(ogrestr) gcnew System::String((ogrestr).c_str())
#else
	#define DECLARE_OGRE_STRING(nvar,mstr)									\
		IntPtr p_##nvar = Marshal::StringToHGlobalAnsi(mstr);				\
		Ogre::String nvar( static_cast<char*>(p_##nvar.ToPointer()) );		\
		Marshal::FreeHGlobal( p_##nvar );

	#define SET_OGRE_STRING(nvar,mstr)										\
		IntPtr __p_ostr##__COUNTER__ = Marshal::StringToHGlobalAnsi(mstr);	\
		nvar = static_cast<char*>(__p_ostr##__COUNTER__.ToPointer());		\
		Marshal::FreeHGlobal( __p_ostr##__COUNTER__ );

	#define TO_CLR_STRING(ogrestr) gcnew System::String((ogrestr).c_str())
#endif


#using "Mogre.dll"

using namespace System;
using namespace System::Runtime::InteropServices;


namespace PLSM2Helper
{
	public ref class PagingLandscapeEvent sealed
	{
		Ogre::PagingLandscapeEvent* _native;
		//Cached
		Mogre::AxisAlignedBox^ _box;

	internal:

		PagingLandscapeEvent( Ogre::PagingLandscapeEvent* pEvent ) : _native(pEvent)
		{ 
		}

	public:

		property size_t PageX
		{
			size_t get() { return _native->mPagex; }
		}

		property size_t PageZ
		{
			size_t get() { return _native->mPagez; }
		}

		property size_t TileX
		{
			size_t get() { return _native->mTilex; }
		}

		property size_t TileZ
		{
			size_t get() { return _native->mTilez; }
		}

		property const Ogre::Real* HeightData
		{
			const Ogre::Real* get() { return _native->mHeightData; }
		}

		property Mogre::AxisAlignedBox^ Bbox
		{
			Mogre::AxisAlignedBox^ get() { return (CLR_NULL == _box) ? (_box = (Mogre::AxisAlignedBox^) _native->mBbox) : _box; }
		}
	};

	public delegate void PagingLandscapeEventHandler( PagingLandscapeEvent^ e );


	class PagingLandscapeDelegate_Director
	{
		gcroot<PagingLandscapeEventHandler^> _managedHandler;

	public:
		PagingLandscapeDelegate_Director( PagingLandscapeEventHandler^ handler )
		{
			_managedHandler = handler;
		}

		void invoke( Ogre::PagingLandscapeEvent* e )
		{
			_managedHandler->Invoke( gcnew PagingLandscapeEvent(e) );
		}
	};

	public ref class PagingLandscapeDelegate sealed
	{
	internal:

		PagingLandscapeDelegate_Director* _director;
		Ogre::PagingLandscapeDelegate* _native;

	public:

		PagingLandscapeDelegate( PagingLandscapeEventHandler^ handler )
		{
			_director = new PagingLandscapeDelegate_Director( handler );
			_native = new Ogre::PagingLandscapeDelegate(&(*_director), 
							&PagingLandscapeDelegate_Director::invoke);
		}

		~PagingLandscapeDelegate()
		{
			this->!PagingLandscapeDelegate();
		}
		!PagingLandscapeDelegate()
		{
			if (_director)
			{
				delete _native;
				delete _director;
				_director = NULL;
				_native = NULL;
			}
		}
	};


	public ref class PLSM2Options abstract sealed
	{
	public:
		static bool SetOption( Mogre::SceneManager^ sceneMgr, String^ key, PagingLandscapeDelegate^ handler )
		{
			return sceneMgr->SetOption( key, handler->_native );
		}

		static bool GetOption( Mogre::SceneManager^ sceneMgr, String^ key, [Out] Mogre::AxisAlignedBox^% box )
		{
			Ogre::AxisAlignedBox o_box;

			bool ret = sceneMgr->GetOption( key, (void*)&o_box );
			if (ret)
				box = (Mogre::AxisAlignedBox^) o_box;

			return ret;
		}

		static bool SetOption( Mogre::SceneManager^ sceneMgr, String^ key, Mogre::AxisAlignedBox^ box )
		{
			Ogre::AxisAlignedBox o_box = (Ogre::AxisAlignedBox) box;

			return sceneMgr->SetOption( key, (void*)&o_box );
		}

		static bool SetOption( Mogre::SceneManager^ sceneMgr, String^ key, Mogre::Camera^ camera )
		{
			return sceneMgr->SetOption( key, (void*)(Ogre::Camera*)camera );
		}

		static bool SetPaintChannelValues( Mogre::SceneManager^ sceneMgr, System::Collections::Generic::List<Ogre::Real>^ values )
		{
			std::vector<Ogre::Real> vec;
			for each (Ogre::Real val in values)
			{
				vec.push_back(val);
			}

			return sceneMgr->SetOption( "setPaintChannelValues", &vec );
		}

		static bool GetAreaSize( Mogre::SceneManager^ sceneMgr, array<Mogre::Vector3>^ values )
		{
			if (values->Length != 5)
				throw gcnew ArgumentException("'values' parameter must be an array with 5 elements");

			pin_ptr<Mogre::Vector3> ptr = &values[0];

			return sceneMgr->GetOption( "getAreaSize", (void*)ptr );
		}

		static bool GetVisibilityMaterial( Mogre::SceneManager^ sceneMgr, [Out] Mogre::MaterialPtr^% material )
		{
			Ogre::MaterialPtr o_mat;

			bool ret = sceneMgr->GetOption( "VisibilityMaterial", (void*)&o_mat );
			if (ret)
				material = (Mogre::MaterialPtr^) o_mat;

			return ret;
		}

		static bool GetPageGetTileVertexData( Mogre::SceneManager^ sceneMgr, 
						int pageX, int pageZ, int tileX, int tileZ, int lodLevel,
						[Out] array<Mogre::Vector3>^% vertices, [Out] Mogre::IndexData^% indexData )
		{
			std::vector<void*> vec;
			vec.push_back( &pageX );
			vec.push_back( &pageZ );
			vec.push_back( &tileX );
			vec.push_back( &tileZ );
			vec.push_back( &lodLevel );

			bool ret = sceneMgr->GetOption( "PageGetTileVertexData_2", &vec );
			if (ret)
			{
				unsigned int* numPtr = (unsigned int*) vec.at(5);
				Ogre::Vector3*	verts = (Ogre::Vector3*) vec.at(6);
				Ogre::IndexData* o_indexData = (Ogre::IndexData*) vec.at(7);

				vertices = gcnew array<Mogre::Vector3>(*numPtr);
				if ((*numPtr) > 0)
				{
					pin_ptr<Mogre::Vector3> p_vertices = &vertices[0];
					memcpy( p_vertices, verts, sizeof(Ogre::Vector3) * (*numPtr) );
				}

				indexData = (Mogre::IndexData^) o_indexData;

				// clean up
				delete numPtr;
				delete[] verts;
			}

			return ret;
		}

		static bool GetMaterialPageName( Mogre::SceneManager^ sceneMgr, Mogre::Vector3% pos, [Out] String^% name )
		{
			pin_ptr<Mogre::Vector3> posPtr = &pos;
			void* set_get_ptr = posPtr;

			bool ret = sceneMgr->GetOption( "getMaterialPageName", (void*)&set_get_ptr );
			if (ret)
			{
				name = TO_CLR_STRING( *static_cast<Ogre::String*>( set_get_ptr ) );
			}

			return ret;
		}
	};


}

