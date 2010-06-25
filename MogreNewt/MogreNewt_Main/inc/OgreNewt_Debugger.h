/* 
	OgreNewt Library

	Ogre implementation of Newton Game Dynamics SDK

	OgreNewt basically has no license, you may use any or all of the library however you desire... I hope it can help you in any way.

		by Walaber

*/
#ifndef _INCLUDE_OGRENEWT_DEBUGGER
#define _INCLUDE_OGRENEWT_DEBUGGER


#include <Ogre.h>
#include <Newton.h>
#include "OgreNewt_Tools.h"

namespace MogreNewt
{

	 ref class MaterialID;
	 ref class Body;
	//! For viewing the Newton rigid bodies visually.
	/*!
		This class implements a debug view of the Newton world.  it is a Singleton!
		2 versions of this class exist; 1 which uses the Dagon ManualObject class to draw the lines (and is therefore only compatible with
		Dagon branches of Ogre), and a second which uses the Line3D class previously released to the Ogre community.  the version that is
		compiled is the Line3D class by default.  to use the ManualObject version, recompile OgreNewt_Main with the preprocessor definition
		"_OGRENEWT_DEBUGGER_DAGON".  this will change the class to use ManualObject internally.  this version has no extra dependencies.
		However, the Line3D (default) version requires that you include Line3D.cpp into your project, as it is not compiled into OgreNewt_Main.
		This file is included with OgreNewt, in the demos/Include folder.
	*/
	public ref class Debugger : Mogre::Node::Listener
	{
	
	public:
		//! Standard constructor, create the debugger.
		Debugger(MogreNewt::World^ world);

		//! Standard Destructor, destroys the debugger.
		~Debugger();

		//! init the debugger.
		/*
			\param smgr pointer to your Ogre::SceneManager
		*/
		void Init( Mogre::SceneManager^ smgr );

		//! de-init the debugger (cleantup)
		void DeInit();

		virtual void NodeDestroyed(Mogre::Node^ node) override;

		//! show the newton world
		/*!
			Draws the Newton world as 3D lines.
			\param world pointer to the MogreNewt::World
		*/
		void ShowDebugInformation();

		/*
			overload, add the ability to show/hide the movabletext containing debug information at the top of the bodies
		*/
		void ShowDebugInformation( bool ShowDebugText);

		/*
			overload, add the ability to show/hide the movabletext containing debug information at the top of the bodies
			and the color change based on the body state (static, active, asleep)
		*/
		//void ShowDebugInformation( bool ShowDebugText,bool ShowStaticAsleep );

		/*
			overload, add the ability to show/hide the movabletext containing debug information at the top of the bodies
			and add a log line for each body with the same debug information
		*/
		void ShowDebugInformation(bool ShowDebugText,bool LogDebugText);


        //! remove lines and text drawn
        void HideDebugInformation();
    
        //! set default color
		void SetDefaultColors(Mogre::ColourValue col);

		//! set default color
		//void SetDefaultColors(Mogre::ColourValue DefaultColor,Mogre::ColourValue AsleepColor);

		//! set default color
		//void SetDefaultColors(Mogre::ColourValue DefaultColor,Mogre::ColourValue AsleepColor,Mogre::ColourValue StaticColor);

        //! set Material color
		void SetMaterialColor(MogreNewt::MaterialID^ mat, Mogre::ColourValue col);

        //! enable additional raycast-debugging (this also enables displaying of recorded raycasts!)
        void StartRaycastRecording(bool markhitbodies);

		//! returns true, if currently recording raycasts
		property bool IsRaycastRecording
		{
			bool get() {return m_recordraycasts;}
			void set(bool value){m_recordraycasts = true;}
		}

		//! returns true, if hit bodies are currently recording
		property bool IsRaycastRecordingHitBodies
		{
			bool get() { return m_markhitbodies;}
			void set(bool value){m_markhitbodies = value;}
		}

		//! returns if the debugger as been initialized on a scenemanager
		property bool IsInitialized
		{
			bool get() { return m_IsInitialized;}
		}


        //! clears all raycasts, that are currently shown, should probably be done once per frame!
        void ClearRaycastsRecorded();

        //! disables raycast-debugging
        void StopRaycastRecording();

        //! set the color of the raycast-debug-lines
        void SetRaycastRecordingColor(Mogre::ColourValue rayCol, Mogre::ColourValue convexCol, Mogre::ColourValue hitBodyCol, Mogre::ColourValue prefilterDiscardedBodyCol);

        //! this function is used internally
        void AddRay(Mogre::Vector3 startpt,Mogre::Vector3 endpt);

        //! this function is used internally
		void AddConvexRay(MogreNewt::Collision^ col,Mogre::Vector3 startpt,Mogre::Quaternion colori,Mogre::Vector3 endpt);

        //! this function is used internally
        void AddDiscardedBody(MogreNewt::Body^ body);

        //! this function is used internally
        void AddHitBody(MogreNewt::Body^ body);

		value class BodyDebugData
        {
			public:
			MogreNewt::Collision^ m_lastcol;
            Ogre::SceneNode* m_node;
            Ogre::ManualObject* m_lines;
            MogreNewt::OgreAddons::MovableText* m_text;
            bool m_updated;
        };  

		value class ManageRaycastObject
		{
			public:
			Ogre::ManualObject*	RaycastObject;
		};
	
	private:
		MogreNewt::World^ m_world;
		Ogre::SceneManager*		m_smgr;
		Ogre::SceneNode* m_debugnode;
		System::Collections::Generic::Dictionary<int,Mogre::ColourValue>^ m_materialcolors;
		System::Collections::Generic::List<ManageRaycastObject>^ mRecordedRaycastObjects;
		Mogre::ColourValue       m_defaultcolor;
		Mogre::ColourValue		 m_colorstatic;
		Mogre::ColourValue		 m_colorasleep;
		System::String^			 m_Font;
		bool					 m_showstaticasleep;
		bool					 m_usemovabletext;
		bool					 m_enablelogging;
		bool					 m_hasfont;

		Ogre::SceneNode*        m_raycastsnode;
        bool                    m_recordraycasts;
        Mogre::ColourValue       m_raycol, m_convexcol, m_hitbodycol, m_prefilterdiscardedcol;
        bool                    m_markhitbodies;
		bool					m_IsInitialized;


		System::Collections::Generic::Dictionary<MogreNewt::Body^,BodyDebugData>^ m_cachemap;

		//! create debug information for one body
		void ProcessBody(MogreNewt::Body^ body);

        //! clear debug data cache fo bodies (m_cachemap)
        void ClearBodyDebugDataCache();

	};



#pragma managed(push, off)

void _CDECL Debugger_newtonPerPoly(void* userData, int vertexCount, const float* faceVertec, int id );


#pragma managed(pop)


}	// end namespace MogreNewt


#endif	// _INCLUDE_OGRENEWT_DEBUGGER


