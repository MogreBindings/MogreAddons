#include "OgreNewt_BasicFrameListener.h"
#include "OgreNewt_Debugger.h"

namespace OgreNewt
{

	BasicFrameListener::BasicFrameListener( Ogre::RenderWindow* win, Ogre::SceneManager* mgr, OgreNewt::World^ W, int update_framerate) :
		FrameListener()
{
	m_World = W;
	desired_framerate = update_framerate;

	m_update = (Ogre::Real)(1.0f / (Ogre::Real)desired_framerate);
	m_elapsed = 0.0f;

	// add the standard debug viewer.
	Debugger::getSingleton().init( mgr );

	mInputDevice = Ogre::PlatformManager::getSingleton().createInputReader();
    mInputDevice->initialise(win,true, true);

}

BasicFrameListener::~BasicFrameListener(void)
{
}

bool BasicFrameListener::frameStarted(const Ogre::FrameEvent &evt)
{
	m_elapsed += evt.timeSinceLastFrame;
	Ogre::LogManager::getSingleton().logMessage("   Newton Frame Listener... m_elapsed: "+Ogre::StringConverter::toString(m_elapsed)+
		"  m_update:"+Ogre::StringConverter::toString(m_update));

	int count = 0;

	if ((m_elapsed > m_update) && (m_elapsed < (1.0f)) )
	{
		while (m_elapsed > m_update)
		{
			m_World->update( m_update );
			m_elapsed -= m_update;
			count++;
		}
	}
	else
	{
		if (m_elapsed < (m_update))
		{
			// not enough time has passed this loop, so ignore for now.
		}
		else
		{
			m_World->update( m_elapsed );
			count++;
			m_elapsed = 0.0f; // reset the elapsed time so we don't become "eternally behind".
		}
	}

	Ogre::LogManager::getSingleton().logMessage("   Newton updates this loop: "+Ogre::StringConverter::toString(count));

	/////////////////////////////////////////////////////////////
	//		DEBUGGER
	mInputDevice->capture();

	if (mInputDevice->isKeyDown(Ogre::KC_F3))
	{
		Debugger::getSingleton().showLines( m_World );
	}
	else
	{
		Debugger::getSingleton().hideLines();
	}

	
	return true;
}


}	// end NAMESPACE OgreNewt
