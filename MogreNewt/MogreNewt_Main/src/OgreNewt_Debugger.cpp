#include "OgreNewt_Debugger.h"
#include "OgreNewt_Body.h"
#include "OgreNewt_ContactCallback.h"

#include<iostream>
using namespace std; 

namespace MogreNewt
{

//////////////////////////////////////////////////////////
// DEUBBER FUNCTIONS
//////////////////////////////////////////////////////////

Debugger::Debugger(MogreNewt::World^ world)
{
	m_world = world;
	m_debugnode = NULL;
	m_raycastsnode = NULL;
	m_defaultcolor = Mogre::ColourValue::White;
	m_colorstatic = Mogre::ColourValue(0.8f, 0.8f, 0.8f);
	m_colorasleep = Mogre::ColourValue::Red;

	m_recordraycasts = false;
    m_markhitbodies = false;

    m_raycol = Mogre::ColourValue::Green;
    m_convexcol = Mogre::ColourValue::Blue;
    m_hitbodycol = Mogre::ColourValue::Red;
	m_Font = "BlueHighway-10";
    m_prefilterdiscardedcol = Mogre::ColourValue::Black;
	m_materialcolors = gcnew System::Collections::Generic::Dictionary<int,Mogre::ColourValue>();
	m_cachemap = gcnew System::Collections::Generic::Dictionary<MogreNewt::Body^,BodyDebugData>();
	mRecordedRaycastObjects = gcnew System::Collections::Generic::List<ManageRaycastObject>();

	//added in mogrenewt
	m_showstaticasleep = false;
	m_usemovabletext = false;
	m_enablelogging = false;
	m_hasfont = false;
	m_IsInitialized = false;
}

Debugger::~Debugger()
{
	DeInit();
}

			
void Debugger::Init( Mogre::SceneManager^ smgr )
{
	if( !m_debugnode )
    {
        m_debugnode = smgr->RootSceneNode->CreateChildSceneNode("__OgreNewt__Debugger__");
        m_debugnode->setListener(this);
    }

    if( !m_raycastsnode )
    {
        m_raycastsnode = smgr->RootSceneNode->CreateChildSceneNode("__OgreNewt__Raycasts_Debugger__Node__");
        m_raycastsnode->setListener(this);
    }

	m_IsInitialized = true;
}

void Debugger::DeInit()
{
	ClearBodyDebugDataCache();

	if (m_debugnode)
    {
        m_debugnode->setListener(NULL);
        m_debugnode->removeAndDestroyAllChildren();
        m_debugnode->getParentSceneNode()->removeAndDestroyChild( m_debugnode->getName() );
        m_debugnode = NULL;
    }

	ClearRaycastsRecorded();

    if( m_raycastsnode )
    {
        m_raycastsnode->setListener(NULL);
        m_raycastsnode->removeAndDestroyAllChildren();
        m_raycastsnode->getParentSceneNode()->removeAndDestroyChild( m_raycastsnode->getName() );
        m_raycastsnode = NULL;
    }

	m_IsInitialized = false;
}

void Debugger::NodeDestroyed(Mogre::Node^ node)
{
	if(node == m_debugnode)
    {
        m_debugnode = NULL;
        ClearBodyDebugDataCache();
    }

    if(node == m_raycastsnode)
    {
        m_raycastsnode = NULL;
        ClearRaycastsRecorded();
    }
}

void Debugger::ClearBodyDebugDataCache()
{
	for each(BodyDebugData data in m_cachemap->Values)
	{
		 Ogre::ManualObject* mo = data.m_lines;
            if(mo)
                delete mo;

			MogreNewt::OgreAddons::MovableText* text = data.m_text;

            if(text)
                delete text;
	}

	m_cachemap->Clear();
}

void Debugger::ShowDebugInformation( bool ShowDebugText,bool LogDebugText)
{
	m_enablelogging = LogDebugText;
	ShowDebugInformation(ShowDebugText);
}

void Debugger::ShowDebugInformation( bool ShowDebugText )
{
	m_usemovabletext = ShowDebugText;
	//check if the font for movable text is available

	IntPtr strfont = Marshal::StringToHGlobalAnsi(m_Font);
	Ogre::String UMfont = static_cast<char*>(strfont.ToPointer());
	Marshal::FreeHGlobal( strfont );

	Ogre::Font* pFont = (Ogre::Font *)Ogre::FontManager::getSingleton().getByName(UMfont).getPointer();

	if(!pFont)
	{
		Mogre::LogManager::Singleton->LogMessage(Mogre::LogMessageLevel::LML_NORMAL,"MogreNewt - Could not find font "+m_Font+", Debug text disabled");
		m_usemovabletext = false;
		m_hasfont = false;
	}
	else
	{
		m_hasfont = true;
	}

	pFont = NULL;

	ShowDebugInformation();
}


/*void Debugger::ShowDebugInformation( bool ShowDebugText,bool ShowStaticAsleep )
{
	m_usemovabletext = ShowDebugText;
	m_showstaticasleep = ShowStaticAsleep;
	ShowDebugInformation();
}*/

void Debugger::ShowDebugInformation( )
{
    if (!m_debugnode)
        return;

    //m_debugnode->removeAllChildren();

	 // make the new lines.
	for (NewtonBody* b = NewtonWorldGetFirstBody(m_world->NewtonWorld); b; b = NewtonWorldGetNextBody(m_world->NewtonWorld, b))
	{
		Body^ body = static_cast<BodyNativeInfo*>(  NewtonBodyGetUserData(b) )->managedBody;
		ProcessBody(body);
	}
   
	
    // delete old entries
    System::Collections::Generic::Dictionary<MogreNewt::Body^,BodyDebugData>^ newmap = gcnew System::Collections::Generic::Dictionary<MogreNewt::Body^,BodyDebugData>();

	for each(MogreNewt::Body^ data in m_cachemap->Keys)
	{
		if(m_cachemap[data].m_updated)
		{
			BodyDebugData m_data = m_cachemap[data];
			m_data.m_updated = false;
			newmap->Add(data,m_data);
		}
		else
		{
			m_debugnode->removeChild(m_cachemap[data].m_node);
			Ogre::ManualObject* mo = m_cachemap[data].m_lines;
            if(mo)
                delete mo;

			MogreNewt::OgreAddons::MovableText* text = m_cachemap[data].m_text;

            if(text)
                delete text;
		}
	}

	m_cachemap = newmap;
}

void Debugger::HideDebugInformation()
{
    // erase any existing lines!
    if( m_debugnode )
        m_debugnode->removeAllChildren();

	for each(MogreNewt::Body^ data in m_cachemap->Keys)
	{
			Ogre::ManualObject* mo = m_cachemap[data].m_lines;
            if(mo)
                delete mo;

			MogreNewt::OgreAddons::MovableText* text = m_cachemap[data].m_text;

            if(text)
                delete text;
	}

	m_cachemap->Clear();
}

void Debugger::SetMaterialColor(MogreNewt::MaterialID^ mat, Mogre::ColourValue col)
{
	m_materialcolors[mat->ID] = col;
}

void Debugger::SetDefaultColors(Mogre::ColourValue DefaultColor)
{
    m_defaultcolor = DefaultColor;
}
/*
void Debugger::SetDefaultColors(Mogre::ColourValue DefaultColor,Mogre::ColourValue AsleepColor)
{
    m_defaultcolor = DefaultColor;
	m_colorasleep = AsleepColor;
}

void Debugger::SetDefaultColors(Mogre::ColourValue DefaultColor,Mogre::ColourValue AsleepColor,Mogre::ColourValue StaticColor)
{
    m_defaultcolor = DefaultColor;
	m_colorasleep = AsleepColor;
	m_colorstatic = StaticColor;
}*/


void Debugger::ProcessBody(MogreNewt::Body^ bod )
{
    NewtonBody* newtonBody = bod->NewtonBody;

	Mogre::ColourValue mcolor;

	if(bod->MaterialGroupID != nullptr)
	{
		if(m_materialcolors->ContainsKey(bod->MaterialGroupID->ID))
		{
			mcolor = m_materialcolors[bod->MaterialGroupID->ID];
		}
		else
		{
			mcolor = m_defaultcolor;
		}
	}
	else
	{
		mcolor = m_defaultcolor;
	}
		


	Mogre::Vector3 pos;
	Mogre::Quaternion ori;
	bod->GetPositionOrientation(pos,ori);

	Mogre::Vector3 inertia = Mogre::Vector3::ZERO;
	float mass = 1.0f;
	bod->GetMassMatrix(mass,inertia);

	if(m_showstaticasleep)
	{
		if (mass == 0.0f)
			mcolor = m_colorstatic;
		else
			mcolor = (!bod->IsFreezed) ? mcolor : m_colorasleep;
	}


	System::String^ oss_name = "__OgreNewt__Debugger__Body__"+bod->ToString()+"__";
	System::String^ oss_info = "mass: "+ mass.ToString()+" \n inertia: "+inertia.ToString()+" \n position: "+pos.ToString()+" \n orientation: "
		+ori.ToString()+" \n vel: "+bod->Velocity+" \n omega:"+bod->Omega;

	IntPtr str = Marshal::StringToHGlobalAnsi(oss_name);
	Ogre::String UMoss_name = static_cast<char*>(str.ToPointer());
	Marshal::FreeHGlobal( str );

	IntPtr str2 = Marshal::StringToHGlobalAnsi(oss_info);
	Ogre::String UMoss_info = static_cast<char*>(str2.ToPointer());
	Marshal::FreeHGlobal( str2 );

	IntPtr Fontstr = Marshal::StringToHGlobalAnsi(m_Font);
	Ogre::String UMFont =  static_cast<char*>(Fontstr.ToPointer());
	Marshal::FreeHGlobal( Fontstr );

	 // set color
	Ogre::ColourValue ColorValue;
	ColorValue.setAsABGR(mcolor.GetAsARGB());

    // look for cached data
    BodyDebugData data;
	
	if( m_cachemap->ContainsKey(bod)) // use cached data
    {
		data = m_cachemap[bod];
        // set new position...
		data.m_node->setPosition((Ogre::Vector3)pos);
		data.m_node->setOrientation((Ogre::Quaternion)ori);
        data.m_updated = true;

		if(m_hasfont)
		{
			data.m_text->setVisible(m_usemovabletext);
			data.m_text->setCaption(UMoss_info);
			data.m_text->setLocalTranslation((bod->BoundingBox->Size.y*1)*Ogre::Vector3::UNIT_Y);
		}

		if(m_enablelogging)
			Mogre::LogManager::Singleton->LogMessage("MogreNewt - SceneNode name : "+bod->OgreNode->Name+" "+oss_info);

		m_cachemap[bod] = data;
    }
    else
    {
		data.m_lastcol = bod->Collision;
        data.m_updated = true;

        if( data.m_node )
        {
            data.m_node->detachAllObjects();
            data.m_node->setPosition((Ogre::Vector3)pos);
            data.m_node->setOrientation((Ogre::Quaternion)ori);			
        }
        else
			data.m_node = m_debugnode->createChildSceneNode((Ogre::Vector3)pos, (Ogre::Quaternion)ori);

        if( data.m_lines )
            data.m_lines->clear();
        else
        {
			System::String^ oss =  "__OgreNewt__Debugger__Lines__"+bod->ToString()+"__";
			IntPtr str3 = Marshal::StringToHGlobalAnsi(oss);
			Ogre::String UMoss = static_cast<char*>(str3.ToPointer());
			Marshal::FreeHGlobal( str3 );
            data.m_lines = new Ogre::ManualObject(UMoss);
        }

        if( data.m_text && m_hasfont)
		{
			data.m_text->setVisible(m_usemovabletext);
            data.m_text->setCaption(UMoss_info);
            data.m_text->setLocalTranslation((bod->BoundingBox->Maximum.y*1)*Ogre::Vector3::UNIT_Y);
        }
        else
        {
			if(m_hasfont)
			{
				data.m_text = new MogreNewt::OgreAddons::MovableText( UMoss_name, UMoss_info, UMFont,3);
				data.m_text->setLocalTranslation((bod->BoundingBox->Maximum.y/2)*Ogre::Vector3::UNIT_Y+Ogre::Vector3::UNIT_Y*0.1);
				data.m_text->setTextAlignment( MogreNewt::OgreAddons::MovableText::H_LEFT, MogreNewt::OgreAddons::MovableText::V_ABOVE );
				data.m_text->setVisible(m_usemovabletext);
			}
        }

		if(m_enablelogging)
			Mogre::LogManager::Singleton->LogMessage("MogreNewt - SceneNode name : "+bod->OgreNode->Name+" "+oss_info);

		if(m_hasfont)
		{
			data.m_node->attachObject(data.m_text);
		}
        
        data.m_lines->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );

		data.m_lines->colour(ColorValue);

		float matrix[16];
		MogreNewt::Converter::QuatPosToMatrix(Mogre::Quaternion::IDENTITY,Mogre::Vector3::ZERO,&matrix[0]);
		NewtonCollisionForEachPolygonDo( NewtonBodyGetCollision(newtonBody), &matrix[0], Debugger_newtonPerPoly, data.m_lines );

        data.m_lines->end();
        data.m_node->attachObject(data.m_lines);
		m_cachemap->Add(bod,data);
    }
}


/*void Debugger::ShowLines( MogreNewt::World^ world ,bool clearPrevious)
{
	if (clearPrevious)
		m_debugger_debuglines->clear();

	m_debugger_debuglines->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );

  // make the new lines.
	for (NewtonBody* b = NewtonWorldGetFirstBody(world->NewtonWorld); b; b = NewtonWorldGetNextBody(world->NewtonWorld, b))
	{
		// set color.
		Mogre::Real mass;
		Mogre::Vector3 inertia;
		Body^ body = static_cast<BodyNativeInfo*>(  NewtonBodyGetUserData(b) )->managedBody;

		body->GetMassMatrix(mass, inertia);

		if (mass == 0.0f)
			m_currentcolor = m_colorstatic;
		else
			m_currentcolor = (!body->IsAsleep) ? m_coloractiveawake : m_coloractiveasleep;

		Ogre::ColourValue ColorValue;
		ColorValue.setAsABGR(m_currentcolor.GetAsARGB());
			
		Debugger_newtonPerBody(b,ColorValue);
	}

	m_debugger_debuglines->end();
}*/
/*
void Debugger::ShowContacts( MogreNewt::World^ world, bool clearPrevious )
{
	if (clearPrevious)
		m_debugger_debuglines->clear();

	m_debugger_debuglines->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );

	// using straight Newton functions here...
	for (NewtonBody* b = NewtonWorldGetFirstBody(world->NewtonWorld); b;
		b = NewtonWorldGetNextBody(world->NewtonWorld, b))
	{
		for (NewtonJoint* j = NewtonBodyGetFirstContactJoint(b); j; 
			j = NewtonBodyGetNextContactJoint(b, j))
		{
			ContactIterator it(j);

			do
			{
				Mogre::Vector3 pos;
				Mogre::Vector3 norm;
				Mogre::Vector3 tan0, tan1;

				it.GetCurrent()->GetContactPositionAndNormal(pos,norm);
				it.GetCurrent()->GetContactTangentDirections(tan0,tan1);

				// draw this contact!
				// NORMAL (RED)
				m_debugger_debuglines->position((Ogre::Vector3)pos );
				m_debugger_debuglines->colour( Ogre::ColourValue::Red );

				m_debugger_debuglines->position( (Ogre::Vector3)pos + ((Ogre::Vector3)norm * m_contactscale) );
				m_debugger_debuglines->colour( Ogre::ColourValue::Red );

				// TANGENT0 (BLUE)
				m_debugger_debuglines->position( (Ogre::Vector3)pos );
				m_debugger_debuglines->colour( Ogre::ColourValue::Blue );

				m_debugger_debuglines->position( (Ogre::Vector3)pos + ((Ogre::Vector3)tan0 * m_contactscale) );
				m_debugger_debuglines->colour( Ogre::ColourValue::Blue );

				// TANGENT0 (GREEN)
				m_debugger_debuglines->position( (Ogre::Vector3)pos );
				m_debugger_debuglines->colour( Ogre::ColourValue::Green );

				m_debugger_debuglines->position( (Ogre::Vector3)pos + ((Ogre::Vector3)tan1 * m_contactscale) );
				m_debugger_debuglines->colour( Ogre::ColourValue::Green );
				
			}
			while (it.MoveNext());

		}
	}

	m_debugger_debuglines->end();
}
*/

// ----------------- raycast-debugging -----------------------
void Debugger::StartRaycastRecording(bool markhitbodies)
{
    m_recordraycasts = true;
    m_markhitbodies = markhitbodies;
    ClearRaycastsRecorded();
}


void Debugger::ClearRaycastsRecorded()
{
	if( m_raycastsnode )
    {
        m_raycastsnode->removeAndDestroyAllChildren();
    }

	for each(ManageRaycastObject rObj in mRecordedRaycastObjects)
	{
		delete(*rObj.RaycastObject);
	}

	mRecordedRaycastObjects->Clear();
}

void Debugger::StopRaycastRecording()
{
    m_recordraycasts = false;
}

void Debugger::SetRaycastRecordingColor(Mogre::ColourValue rayCol, Mogre::ColourValue convexCol, Mogre::ColourValue hitBodyCol, Mogre::ColourValue prefilterDiscardedBodyCol)
{
    m_raycol = rayCol;
    m_convexcol = convexCol;
    m_hitbodycol = hitBodyCol;
    m_prefilterdiscardedcol = prefilterDiscardedBodyCol;
}

void Debugger::AddRay(Mogre::Vector3 startpt,Mogre::Vector3 endpt)
{
    if (!m_raycastsnode)
        return;

    static int i = 0;
    std::ostringstream oss;
    oss << "__OgreNewt__Raycast_Debugger__Lines__Raycastline__" << i++ << "__";
    Ogre::ManualObject *line = new Ogre::ManualObject(oss.str());
	ManageRaycastObject rObj;
	rObj.RaycastObject = line;

	mRecordedRaycastObjects->Add(rObj);

	Ogre::ColourValue umColorValue;
	umColorValue.setAsABGR(m_raycol.GetAsARGB());

    line->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );
	line->colour(umColorValue);
	line->position((Ogre::Vector3)startpt);
    line->position((Ogre::Vector3)endpt);
    line->end();

    m_raycastsnode->attachObject(line);    
}

void Debugger::AddConvexRay(MogreNewt::Collision^ col, Mogre::Vector3 startpt,Mogre::Quaternion colori,Mogre::Vector3 endpt)
{
    if (!m_raycastsnode)
        return;

    static int i = 0;
    // lines from aab
    std::ostringstream oss;
    oss << "__OgreNewt__Raycast_Debugger__Lines__Convexcastlines__" << i++ << "__";
    Ogre::ManualObject *line = new Ogre::ManualObject(oss.str());
	ManageRaycastObject rObj;
	rObj.RaycastObject = line;

	mRecordedRaycastObjects->Add(rObj);

	 m_raycastsnode->attachObject(line);

	Ogre::ColourValue umColorValue;
	umColorValue.setAsABGR(m_convexcol.GetAsARGB());

    line->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );
	line->colour(umColorValue);

    // aab1
	Mogre::AxisAlignedBox^ aab1 = col->GetAABB(colori,startpt);
	cli::array<Mogre::Vector3>^ corners1 = aab1->GetAllCorners();
	Mogre::AxisAlignedBox^ aab2 = col->GetAABB(colori,endpt);
	cli::array<Mogre::Vector3>^ corners2 = aab2->GetAllCorners();

	//create a temporary variable for storing each vector from array since we can't cast Mogre::Vector3 in Ogre::Vector3 straight from an array -_-
	Mogre::Vector3 TempVector;


    for(int i = 0; i < 4; i++)
    {
		TempVector = corners1[i]; line->position((Ogre::Vector3)TempVector); TempVector = corners1[(i+1)%4];line->position((Ogre::Vector3)TempVector);
		TempVector = corners1[i+4]; line->position((Ogre::Vector3)TempVector); TempVector = corners1[(i+1)%4+4];line->position((Ogre::Vector3)TempVector);
		TempVector = corners2[i]; line->position((Ogre::Vector3)TempVector); TempVector = corners2[(i+1)%4];line->position((Ogre::Vector3)TempVector);
		TempVector = corners2[i+4]; line->position((Ogre::Vector3)TempVector); TempVector = corners2[(i+1)%4+4];line->position((Ogre::Vector3)TempVector);
		TempVector = corners1[i]; line->position((Ogre::Vector3)TempVector); TempVector = corners2[i];line->position((Ogre::Vector3)TempVector);
		TempVector = corners1[i+4]; line->position((Ogre::Vector3)TempVector); TempVector = corners2[i+4];line->position((Ogre::Vector3)TempVector);
    }

    TempVector = corners1[0];line->position((Ogre::Vector3)TempVector); TempVector = corners1[6];line->position((Ogre::Vector3)TempVector);
	TempVector = corners1[1];line->position((Ogre::Vector3)TempVector); TempVector = corners1[5];line->position((Ogre::Vector3)TempVector);
	TempVector = corners1[2];line->position((Ogre::Vector3)TempVector); TempVector = corners1[4];line->position((Ogre::Vector3)TempVector);
	TempVector = corners1[3];line->position((Ogre::Vector3)TempVector); TempVector = corners1[7];line->position((Ogre::Vector3)TempVector);
	TempVector = corners2[0];line->position((Ogre::Vector3)TempVector); TempVector = corners2[6];line->position((Ogre::Vector3)TempVector);
	TempVector = corners2[1];line->position((Ogre::Vector3)TempVector); TempVector = corners2[5];line->position((Ogre::Vector3)TempVector);
	TempVector = corners2[2];line->position((Ogre::Vector3)TempVector); TempVector = corners2[4];line->position((Ogre::Vector3)TempVector);
	TempVector = corners2[3];line->position((Ogre::Vector3)TempVector); TempVector = corners2[7];line->position((Ogre::Vector3)TempVector);

    // bodies
    float matrix[16];
    Converter::QuatPosToMatrix(colori, startpt, &matrix[0]);
    NewtonCollisionForEachPolygonDo( col->NewtonCollision, &matrix[0], Debugger_newtonPerPoly, line );
    Converter::QuatPosToMatrix(colori, endpt, &matrix[0]);
    NewtonCollisionForEachPolygonDo( col->NewtonCollision, &matrix[0], Debugger_newtonPerPoly, line );

    line->end();
}

void Debugger::AddDiscardedBody(MogreNewt::Body^ body)
{
    if (!m_raycastsnode)
        return;

    static int i = 0;
    float matrix[16];

    std::ostringstream oss;
    oss << "__OgreNewt__Raycast_Debugger__Lines__DiscardedBody__" << i++ << "__";
    Ogre::ManualObject *line = new Ogre::ManualObject(oss.str());
	ManageRaycastObject rObj;
	rObj.RaycastObject = line;
	mRecordedRaycastObjects->Add(rObj);

	m_raycastsnode->attachObject(line);

	Ogre::ColourValue umColorValue;
	umColorValue.setAsABGR(m_prefilterdiscardedcol.GetAsARGB());

    line->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );
	line->colour(umColorValue);

	NewtonBodyGetMatrix(body->NewtonBody, &matrix[0]);
	NewtonCollisionForEachPolygonDo( body->Collision->NewtonCollision , &matrix[0], Debugger_newtonPerPoly, line );

    line->end();

}

void Debugger::AddHitBody(MogreNewt::Body^ body)
{
    if (!m_raycastsnode)
        return;

    static int i = 0;
    float matrix[16];

    std::ostringstream oss;
    oss << "__OgreNewt__Raycast_Debugger__Lines__HitBody__" << i++ << "__";
    Ogre::ManualObject *line = new Ogre::ManualObject(oss.str());
	ManageRaycastObject rObj;
	rObj.RaycastObject = line;
	mRecordedRaycastObjects->Add(rObj);

	m_raycastsnode->attachObject(line);

	Ogre::ColourValue umColorValue;
	umColorValue.setAsABGR(m_hitbodycol.GetAsARGB());

    line->begin("BaseWhiteNoLighting", Ogre::RenderOperation::OT_LINE_LIST );
	line->colour(umColorValue);

	NewtonBodyGetMatrix(body->NewtonBody, &matrix[0]);
	NewtonCollisionForEachPolygonDo( body->Collision->NewtonCollision , &matrix[0], Debugger_newtonPerPoly, line );

    line->end();

}


#pragma unmanaged


void _CDECL Debugger_newtonPerPoly( void* userData, int vertexCount, const float* faceVertec, int id )
{
	Ogre::ManualObject* lines = (Ogre::ManualObject*)userData;
    Ogre::Vector3 p0, p1;

    if( vertexCount < 2 )
        return;

    int i= vertexCount - 1;
    p0 = Ogre::Vector3( faceVertec[(i*3) + 0], faceVertec[(i*3) + 1], faceVertec[(i*3) + 2] );

    for (i=0;i<vertexCount;i++)
    {
        p1 = Ogre::Vector3( faceVertec[(i*3) + 0], faceVertec[(i*3) + 1], faceVertec[(i*3) + 2] );
		
        lines->position( p0 );
        lines->position( p1 );
        p0 = p1;
    }

}

}	// end namespace MogreNewt
