using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FSLOgreCS
{
    public class FSLSoundManager
    {
        #region Variables
        private List<FSLSoundObject> _soundObjectVector = new List<FSLSoundObject>();

        /// <summary>
        /// Read only Collection of contained sounds. 
        /// </summary>
        public ReadOnlyCollection<FSLSoundObject> SoundObjectsCollection
        {
            get { return _soundObjectVector.AsReadOnly(); }
        }


        private bool _initSound;
        private IFSLListener _listener;
        #endregion

        #region Singleton Stuff

        FSLSoundManager()
        {
            _initSound = false;
            _listener = null;
        }
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static FSLSoundManager Instance
        {
            get
            {
                return SingletonCreator.instance;
            }
        }

        class SingletonCreator
        {
            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            static SingletonCreator()
            {
            }

            internal static readonly FSLSoundManager instance = new FSLSoundManager();
        }

        #endregion 

        #region Private Members

        /// <summary>
        /// Returns the number of sounds currently set up & playable.
        /// </summary>
        public int NumberOfSounds
        {
            get
            {
                return _soundObjectVector.Count;
            }
        }
        
        private FSLSoundObject AddSound(FSLSoundObject sound)
        {
            _soundObjectVector.Add( sound );
	        return sound;
        }

        #endregion

        /// <summary>
        /// Initializes the sound system.
        /// </summary>
        /// <param name="soundSystem">Enumeration of FSL_SOUND_SYSTEM to use.</param>
        /// <param name="listener">Camera to use as a listener for 3D sound.</param>
        /// <returns></returns>
        public bool InitializeSound(FreeSL.FSL_SOUND_SYSTEM soundSystem, Mogre.Camera listener)
        {
	        return InitializeSound(soundSystem,new MogreCameraFSLListener(listener));
        }

        /// <summary>
        /// Initializes the sound system.
        /// </summary>
        /// <param name="soundSystem">Enumeration of FSL_SOUND_SYSTEM to use.</param>
        /// <param name="listener">Sound receiver position listener for 3D sound a.k.a. "The Ears Position"</param>
        /// <returns></returns>
        public bool InitializeSound(FreeSL.FSL_SOUND_SYSTEM soundSystem,IFSLListener listener)
        {
            _listener = listener;
            if (_initSound)
                return true;

            if (!FreeSL.fslInit(soundSystem)) //Change if you desire
                return false;

            _initSound = true;
            return true;
        }

        internal void ShutDown()
        {
            FreeSL.fslShutDown();
            _initSound = false;
        }

        public float Volume
        {
            set
            {
                FreeSL.fslSetVolume(value);
            }
        }
        
        public bool Initialized
        {
            get
            {
                return _initSound;
            }
        }

        public bool RemoveSound(string name)
        {
            FSLSoundObject sound = GetSound(name);
            return RemoveSound(sound);
        }

        public bool RemoveSound(FSLSoundObject sound)
        {
            if (sound.HasSound())
                FreeSL.fslFreeSound(sound.SoundID, true);
            bool ret = _soundObjectVector.Remove(sound);
            if (ret)
                OnSoundRemoved(sound);

            return ret;
        }

        public FSLSoundObject GetSound(string name)
        {
	        if (_soundObjectVector.Count == 0)
			        return null;
            foreach (FSLSoundObject sound in _soundObjectVector)
            {
                if(sound.Name == name)
                    return sound;
            }
	        return null;
        }

        public void UpdateSoundObjects()
        {
	        if (!_initSound)
		        return;

            for (int i = _soundObjectVector.Count - 1; i >= 0; i--)
            {
                _soundObjectVector[i].Update();
            }
        }

        public void UpdateListener()
        {
            if (!_initSound)
                return;

            FSLPosition position = _listener.Position;
            FSLOrientation orientation = _listener.Orientation;

            FreeSL.fslSetListenerPosition(position.x, position.y, position.z);

            FreeSL.fslSetListenerOrientation(orientation.atx,orientation.aty,orientation.atz,
                                             orientation.upx,orientation.upy,orientation.upz);
        }

        public void SetListener(IFSLListener listener)
        {
            _listener = listener;
        }

        public IFSLListener GetListener()
        {
	        return _listener;
        }

        #region AmbientSound Creation

        /// <summary>
        /// Create a sound that may not be rendered in 3D space.
        /// </summary>
        /// <param name="soundFile">Location of file to use.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        /// <param name="streaming">Sets whether the sound should be streamed instead of statically loaded. FreeSL.fslAutoUpdate(true) should be called after sound manager initialization if this is set to true.</param>
        public FSLSoundObject CreateAmbientSound(string soundFile, string name, bool loop, bool streaming)
        {
	        return AddSound( new FSLAmbientSound( soundFile, name, loop, streaming) );
        }

        /// <summary>
        /// Create a sound that may not be rendered in 3D space.
        /// </summary>
        /// <param name="package">Zip file to load sound from.</param>
        /// <param name="soundFile">Name of file to use.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        public FSLSoundObject CreateAmbientSound(string package, string soundFile, string name, bool loop)
        {
            return AddSound(new FSLAmbientSound(package, soundFile, name, loop));
        }

        #endregion

        #region SoundEntity Creation

        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="soundFile">Location of file to use.</param>
        /// <param name="node">Node to attach the 3D sound to.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        /// <param name="streaming">Sets whether the sound should be streamed instead of statically loaded. FreeSL.fslAutoUpdate(true) should be called after sound manager initialization if this is set to true.</param>
        public FSLSoundObject CreateSoundEntity(string soundFile, Mogre.Node node, string name, bool loop, bool streaming)
        {
            return CreateSoundEntity(soundFile, new NodePositionProvider(node), name, loop, streaming);
        }
        
        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="package">Zip file to load sound from.</param>
        /// <param name="soundFile">Name of file to use.</param>
        /// <param name="node">Node to attach the 3D sound to.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        public FSLSoundObject CreateSoundEntity(string package, string soundFile, Mogre.Node node, string name, bool loop)
        {
            return CreateSoundEntity(package, soundFile, new NodePositionProvider(node), name, loop);
        }

        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="soundFile">Location of file to use.</param>
        /// <param name="positionProvider">Position provider for the 3D sound.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        /// <param name="streaming">Sets whether the sound should be streamed instead of statically loaded. FreeSL.fslAutoUpdate(true) should be called after sound manager initialization if this is set to true.</param>
        public FSLSoundObject CreateSoundEntity(string soundFile,IFSLPositionProvider positionProvider, string name, bool loop, bool streaming)
        {
            FSLSoundObject soundObject = new FSLSoundObject(soundFile, positionProvider, name, loop, streaming);
            soundObject.SetReferenceDistance(80.0f);
            return AddSound(soundObject);
        }

        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="package">Zip file to load sound from.</param>
        /// <param name="soundFile">Name of file to use.</param>
        /// <param name="positionProvider">Position provider for the 3D sound.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        public FSLSoundObject CreateSoundEntity(string package, string soundFile,IFSLPositionProvider positionProvider, string name, bool loop)
        {
            FSLSoundObject soundObject=new FSLSoundObject(package, soundFile, positionProvider, name, loop);
            soundObject.SetReferenceDistance(80.0f);
            return AddSound(soundObject);
        }

        #endregion

        #region SoundObject Creation

        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="soundFile">Location of file to use.</param>
        /// <param name="position">Position provider gives position for the 3D sound.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        /// <param name="streaming">Sets whether the sound should be streamed instead of statically loaded. FreeSL.fslAutoUpdate(true) should be called after sound manager initialization if this is set to true.</param>
        public FSLSoundObject CreateSoundObject(string soundFile, IFSLPositionProvider position, string name, bool loop, bool streaming)
        {
            FSLSoundObject soundObject = new FSLSoundObject(soundFile, name, loop, streaming);
            soundObject.PositionProvider = position;
            return AddSound(soundObject);
        }

        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="package">Zip file to load sound from.</param>
        /// <param name="soundFile">Name of file to use.</param>
        /// <param name="position">Position object to attach the 3D sound to.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        public FSLSoundObject CreateSoundObject(string package, string soundFile, IFSLPositionProvider position, string name, bool loop)
        {
            FSLSoundObject soundObject = new FSLSoundObject(package, soundFile, name, loop);
            soundObject.PositionProvider = position;
            return AddSound(soundObject);
        }


        #endregion

        public bool FrameStarted(Mogre.FrameEvent evt)
        {
            this.Update();
            return true;
        }

        public void Update()
        {
            UpdateListener();
            UpdateSoundObjects();
        }
                
        public void Destroy(){
	        if (_soundObjectVector.Count != 0){
                
                for (int i = 0; i < _soundObjectVector.Count; i++)
                {
                    _soundObjectVector[i].Destroy();
                }
                
                _soundObjectVector.Clear();
	        }
	        if ( _listener != null)
		        _listener = null;
	        if (_initSound)
		        ShutDown();
        }

        #region Environment Functions

        /// <summary>
        /// Sets the listener environment.
        /// </summary>
        /// <param name="prop">The prop.</param>
        public void SetListenerEnvironment(FreeSL.FSL_EAX_LISTENER_PROPERTIES prop)
        {
            FreeSL.fslSetListenerEnvironment(prop);
        }
        /// <summary>
        /// Sets the listener environment preset.
        /// </summary>
        /// <param name="type">The type.</param>
        public void SetListenerEnvironmentPreset(FreeSL.FSL_LISTENER_ENVIRONMENT type)
        {
            FreeSL.fslSetListenerEnvironmentPreset(type);
        }

        /// <summary>
        /// Sets the listener default environment.
        /// </summary>
        public void SetListenerDefaultEnvironment()
        {
            FreeSL.fslSetListenerDefaultEnvironment();
        }

        /// <summary>
        /// Gets the current listener environment.
        /// </summary>
        /// <returns></returns>
        public FreeSL.FSL_EAX_LISTENER_PROPERTIES GetCurrentListenerEnvironment()
        {
            return FreeSL.fslGetCurrentListenerEnvironment();
        }

        /// <summary>
        /// Loads the listener environment.
        /// </summary>
        /// <param name="strFile">The STR file.</param>
        /// <returns></returns>
        public FreeSL.FSL_EAX_LISTENER_PROPERTIES LoadListenerEnvironment(string strFile)
        {
            return FreeSL.fslLoadListenerEnvironment(strFile);
        }

        /// <summary>
        /// Loads the listener environment from zip.
        /// </summary>
        /// <param name="strFile">The STR file.</param>
        /// <param name="strPackage">The STR package.</param>
        /// <returns></returns>
        public FreeSL.FSL_EAX_LISTENER_PROPERTIES LoadListenerEnvironmentFromZip(string strFile, string strPackage)
        {
            return FreeSL.fslLoadListenerEnvironmentFromZip(strPackage, strFile);
        }

        /// <summary>
        /// Creates the listener environment.
        /// </summary>
        /// <param name="strData">The STR data.</param>
        /// <param name="Size">The size.</param>
        /// <returns></returns>
        public FreeSL.FSL_EAX_LISTENER_PROPERTIES CreateListenerEnvironment(string strData, uint Size)
        {
            return FreeSL.fslCreateListenerEnvironment(strData, Size);

        }
        #endregion

        public event EventHandler<FSLSoundRemovedEventArgs> SoundRemoved;
        protected void OnSoundRemoved(FSLSoundObject sound)
        {
            sound.OnSoundRemoved();
            if (SoundRemoved != null)
            {
                FSLSoundRemovedEventArgs soundRemovedEventArgs = new FSLSoundRemovedEventArgs(sound);
                SoundRemoved(this, soundRemovedEventArgs);
            }
        }
    }
}
