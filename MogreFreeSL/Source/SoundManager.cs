using System;
using System.Collections.Generic;
using System.Text;

namespace MogreFreeSL
{
    /// <summary>
    /// The framework through which all sounds are created and all global sound settings are modified. Information and support can be found at http://www.ogre3d.org/tikiwiki/MogreFreeSL
    /// </summary>
    public class SoundManager
    {
        #region Variables

        private List<AmbientSound> _ambientSounds = new List<AmbientSound>();
        private List<SoundEntity> _soundEntities = new List<SoundEntity>();

        private bool _initSound;
        private Listener _listener;

        #endregion

        #region Singleton Stuff

        SoundManager()
        {
            _initSound = false;
            _listener = null;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static SoundManager Instance
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

            internal static readonly SoundManager instance = new SoundManager();
        }

        #endregion 

        /// <summary>
        /// Returns the number of SoundEntities currently set up and playable.
        /// </summary>
        public int NumberOfSoundEntities
        {
            get
            {
                return _soundEntities.Count;
            }
        }

        /// <summary>
        /// Returns the number of AmbientSounds currently set up and playable.
        /// </summary>
        public int NumberOfAmbientSounds
        {
            get
            {
                return _ambientSounds.Count;
            }
        }

        /// <summary>
        /// Initializes the sound system.
        /// </summary>
        /// <param name="soundSystem">Enumeration of FSL_SOUND_SYSTEM to use.</param>
        /// <param name="listener">Camera to use as a listener for 3D sound.</param>
        /// <returns></returns>
        public bool InitializeSound(FSL_SOUND_SYSTEM soundSystem, Mogre.Camera listener)
        {
            _listener = new Listener(listener);
            GetListener().ZFlipped = true;

            if (_initSound)
			    return true;

	        if (!FreeSL.fslInit(soundSystem)) //Change if you desire
			    return false;

            FreeSL.fslSetAutoUpdate(true);
	        _initSound = true;
	        return true;
        }

        internal void ShutDown()
        {
            FreeSL.fslShutDown();
            _initSound = false;
        }

        /// <summary>
        /// Sets the volume of all sounds belonging to this manager
        /// </summary>
        public float Volume
        {
            set
            {
                FreeSL.fslSetVolume(value);
            }
        }
        
        /// <summary>
        /// Gets if this manager is initialized
        /// </summary>
        public bool Initialized
        {
            get
            {
                return _initSound;
            }
        }

        internal bool RemoveAmbientSound(AmbientSound sound)
        {
            if (sound.HasSound())
                FreeSL.fslFreeSound(sound.SoundID, true);
            return _ambientSounds.Remove(sound);
        }

        internal bool RemoveSoundEntity(SoundEntity sound)
        {
            if (sound.HasSound())
                FreeSL.fslFreeSound(sound.SoundID, true);
            return _soundEntities.Remove(sound);
        }

        /// <summary>
        /// Returns the AmbientSound at the provided index. AmbientSounds and SoundEntities DO NOT share index systems.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public AmbientSound GetAmbientSound(int index)
        {
            if (index < _ambientSounds.Count)
                return _ambientSounds[index];
            else
                return null;
        }

        /// <summary>
        /// Returns the SoundEntity at the provided index. AmbientSounds and SoundEntities DO NOT share index systems.
        /// </summary>
        public SoundEntity GetSoundEntity(int index)
        {
            if (index < _soundEntities.Count)
                return _soundEntities[index];
            else
                return null;
        }

        /// <summary>
        /// Returns the AmbientSound with the given name.
        /// </summary>
        public AmbientSound GetAmbientSound(string name)
        {
            if (_ambientSounds.Count == 0)
			        return null;

            return _ambientSounds.Find(x => x.Name == name);
        }

        /// <summary>
        /// Returns the SoundEntity with the given name.
        /// </summary>
        public SoundEntity GetSoundEntity(string name)
        {
            if (_soundEntities.Count == 0)
                return null;

            return _soundEntities.Find(x => x.Name == name);
        }

        /// <summary>
        /// Updates all sounds controlled by the sound manager. This is called by SoundManager.FrameStarted(), so it is not neccessary to call this if you've added a frame listener to FrameStarted().
        /// </summary>
        public void Update()
        {
	        if (!_initSound)
		        return;
	        _listener.Update();

            for (int i = _soundEntities.Count - 1; i >= 0; i--)
            {
                _soundEntities[i].Update();
            }

            for (int i = _ambientSounds.Count - 1; i >= 0; i--)
            {
                _ambientSounds[i].Update();
            }
        }

        /// <summary>
        /// Sets the listener used by the sound manager for 3D sound calculations.
        /// </summary>
        public void SetListener(Mogre.Camera listener)
        {
	        _listener = new Listener( listener );
        }

        /// <summary>
        /// Returns the listener used by the sound manager for 3D sound calculations.
        /// </summary>
        public Listener GetListener()
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
        public AmbientSound CreateAmbientSound(string soundFile, string name, bool loop, bool streaming)
        {
            _ambientSounds.Add(new AmbientSound(soundFile, name, loop, streaming));
            return _ambientSounds[_ambientSounds.Count - 1];
        }

        /// <summary>
        /// Create a sound that may not be rendered in 3D space.
        /// </summary>
        /// <param name="package">Zip file to load sound from.</param>
        /// <param name="soundFile">Name of file to use.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        public AmbientSound CreateAmbientSound(string package, string soundFile, string name, bool loop)
        {
            _ambientSounds.Add(new AmbientSound(package, soundFile, name, loop));
            return _ambientSounds[_ambientSounds.Count - 1];
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
        public SoundEntity CreateSoundEntity(string soundFile, Mogre.Node node, string name, bool loop, bool streaming)
        {
            _soundEntities.Add(new SoundEntity(soundFile, node, name, loop, streaming));
            return _soundEntities[_soundEntities.Count - 1];
        }
        
        /// <summary>
        /// Create a sound that may be rendered in 3D space.
        /// </summary>
        /// <param name="package">Zip file to load sound from.</param>
        /// <param name="soundFile">Name of file to use.</param>
        /// <param name="node">Node to attach the 3D sound to.</param>
        /// <param name="name">Name of the 3D sound.</param>
        /// <param name="loop">Sets whether the sound should automatically loop.</param>
        public SoundEntity CreateSoundEntity(string package, string soundFile, Mogre.Node node, string name, bool loop)
        {
            _soundEntities.Add(new SoundEntity(package, soundFile, node, name, loop));
            return _soundEntities[_soundEntities.Count - 1];
        }

        #endregion

        /// <summary>
        /// Updates all sounds controlled by the sound manager. See the wiki for info on useage.
        /// </summary>
        public bool FrameStarted(Mogre.FrameEvent evt)
        {
            this.Update();
            return true;
        }

        /// <summary>
        /// Removes all sounds, frees the space they consumed, and shuts down the sound system.
        /// </summary>
        public void Destroy()
        {
            for (int i = 0; i < _soundEntities.Count; i++)
            {
                _soundEntities[i].RemoveSound();
            }

            for (int i = 0; i < _ambientSounds.Count; i++)
            {
                _ambientSounds[i].RemoveSound();
            }

            _soundEntities.Clear();
            _ambientSounds.Clear();

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
        public void SetListenerEnvironment(FSL_EAX_LISTENER_PROPERTIES prop)
        {
            FreeSL.fslSetListenerEnvironment(prop);
        }

        /// <summary>
        /// Sets the listener environment preset.
        /// </summary>
        /// <param name="type">The type.</param>
        public void SetListenerEnvironmentPreset(FSL_LISTENER_ENVIRONMENT type)
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
        public FSL_EAX_LISTENER_PROPERTIES GetCurrentListenerEnvironment()
        {
            return FreeSL.fslGetCurrentListenerEnvironment();
        }

        /// <summary>
        /// Loads the listener environment.
        /// </summary>
        /// <param name="strFile">The STR file.</param>
        /// <returns></returns>
        public FSL_EAX_LISTENER_PROPERTIES LoadListenerEnvironment(string strFile)
        {
            return FreeSL.fslLoadListenerEnvironment(strFile);
        }

        /// <summary>
        /// Loads the listener environment from zip.
        /// </summary>
        /// <param name="strFile">The STR file.</param>
        /// <param name="strPackage">The STR package.</param>
        /// <returns></returns>
        public FSL_EAX_LISTENER_PROPERTIES LoadListenerEnvironmentFromZip(string strFile, string strPackage)
        {
            return FreeSL.fslLoadListenerEnvironmentFromZip(strPackage, strFile);
        }

        /// <summary>
        /// Creates the listener environment.
        /// </summary>
        /// <param name="strData">The STR data.</param>
        /// <param name="Size">The size.</param>
        /// <returns></returns>
        public FSL_EAX_LISTENER_PROPERTIES CreateListenerEnvironment(string strData, uint Size)
        {
            return FreeSL.fslCreateListenerEnvironment(strData, Size);

        }
        #endregion

        #region Other Wrapped Functions

        /// <summary>
        /// Gets the current memory usage of all non-streaming sounds. This method does not seem to work properly.
        /// </summary>
        /// <returns></returns>
        public long GetMemoryUseage()
        {
            return (long)FreeSL.fslGetSoundMemoryUsage();
        }

        /// <summary>
        /// Pauses all sounds currently playing.
        /// </summary>
        public void PauseAllSounds()
        {
            FreeSL.fslSoundPauseAllSounds();
        }

        /// <summary>
        /// Unpauses all sounds currently paused.
        /// </summary>
        public void UnpauseAllSounds()
        {
            FreeSL.fslSoundUnPauseAllSounds();
        }

        /// <summary>
        /// Stops all sounds currently playing.
        /// </summary>
        public void StopAllSounds()
        {
            FreeSL.fslSoundStopAllSounds();
        }

        /// <summary>
        /// Sets the speed of all sounds.
        /// </summary>
        /// <param name="pitch">Speed between 0 and 1 at which to play the sound.</param>
        public void SetSpeed(float pitch)
        {
            FreeSL.fslSoundSetSpeedAllSounds(pitch);
        }

        /// <summary>
        /// Sets the gain of all sounds.
        /// </summary>
        /// <param name="gain">Positive or negative gain to apply to all sounds.</param>
        public void SetGain(float gain)
        {
            FreeSL.fslSoundSetGainAllSounds(gain);
        }

        /// <summary>
        /// Sets parameters for the doppler effect.
        /// </summary>
        /// <param name="factor">Factor by which to scale the Doppler effect. 0 = off, 1 = normal.</param>
        /// <param name="velocity">A leftover input variable from OpenAL 1.0. Default value is 1.</param>
        public void SetDopplerParameters(float factor, float velocity)
        {
            FreeSL.fslSetDopplerParameters(factor, velocity);
        }

        /// <summary>
        /// Sets speed of sound for use by the doppler effect.
        /// </summary>
        /// <param name="speed">Speed of sound to assign. The default value is 343.3.</param>
        public void SetSpeedOfSound(float speed)
        {
            FreeSL.fslSetSpeedOfSound(speed);
        }

        /// <summary>
        /// Sets the distance-based sound attenuation model. Controls how the gain of sound sources is affected by distance from the listener.
        /// </summary>
        /// <param name="model">Model to apply as the distance sound attenuation model. The default attenuation model is AL_INVERSE_DISTANCE_CLAMPED.</param>
        public void SetLinearDistanceModel(AL_DISTANCE_MODEL model)
        {
            FreeSL.fslSetListenerDistanceModel(model);
        }

        #endregion
    }
}