using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class FSLSoundObject
    {
	    protected uint _sound;
        protected string _name;
        protected bool _withSound;

        /// <summary>
        /// Automatically destroys the sound after its first play-through. Will not work for looped sounds.
        /// </summary>
        public bool destroyAfterPlaying = false;
        internal bool wasPlayed = false;

        /// <summary>
        /// Gets the uint ID that FreeSL uses to identify this sound.
        /// </summary>
        public uint SoundID
        {
            get { return _sound; }
        }

	    public FSLSoundObject(string soundFile, string name, bool loop, bool streaming)
        {
            _withSound = false;
            _name = name;
	        SetSound(soundFile, loop, streaming);
        }

        public FSLSoundObject(string package, string soundFile, string name, bool loop)
        {
            _withSound = false;
            _name = name;
            SetSound(package, soundFile, loop);
        }

        public void Destroy()
        {
		    RemoveSound();
	    }

        /// <summary>
        /// Removes the sound data associated with the current SoundObject.
        /// </summary>
        public void RemoveSound()
        {
            FSLSoundManager.Instance.RemoveSound(this);
        }

        public void SetSound(string soundFile, bool loop, bool streaming)
        {
            if (System.IO.File.Exists(soundFile) == false)
                throw new System.IO.FileNotFoundException("The sound file : " + soundFile + " does not exist.");
            if (streaming)
                _sound = FreeSL.fslStreamSound(soundFile);
            else
                _sound = FreeSL.fslLoadSound(soundFile);

            LoopSound(loop);
	        _withSound = true;
        }

        public void SetSound(string package, string soundFile, bool loop)
        {
            if (System.IO.File.Exists(package) == false)
                throw new System.IO.FileNotFoundException("The sound file : " + soundFile + "in" + package + " does not exist.");
            _sound = FreeSL.fslLoadSoundFromZip(package, soundFile);

            LoopSound(loop);
            _withSound = true;
        }

        /// <summary>
        /// Returns whether the current SoundObject has a sound.
        /// </summary>
        public bool HasSound(){
	        return _withSound;
        }

        /// <summary>
        /// Gets or sets the name associated with the current SoundObject.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Starts the playback of the current SoundObject.
        /// </summary>
        public void Play(){
	        FreeSL.fslSoundPlay(_sound);
            wasPlayed = true;
        }

        /// <summary>
        /// Stops the playback of the current SoundObject.
        /// </summary>
        public void Stop(){
	        FreeSL.fslSoundStop( _sound );
        }

        /// <summary>
        /// Returns whether the current SoundObject is playing.
        /// </summary>
        public bool IsPlaying(){
	        return FreeSL.fslSoundIsPlaying( _sound );
        }

        /// <summary>
        /// Pauses the playback of the current SoundObject.
        /// </summary>
        public void Pause(){
	        FreeSL.fslSoundPause( _sound );
        }

        /// <summary>
        /// Returns whether the current SoundObject is paused.
        /// </summary>
        public bool IsPaused(){
	        return FreeSL.fslSoundIsPaused( _sound );
        }

        /// <summary>
        /// Sets whether the current SoundObject is looping.
        /// </summary>
        public void LoopSound(bool loop){
	        FreeSL.fslSoundSetLooping(_sound, loop);
        }

        /// <summary>
        /// Returns whether the current SoundObject is looping.
        /// </summary>
        public bool IsLooping(){
	        return FreeSL.fslSoundIsLooping( _sound );
        }

        /// <summary>
        /// Sets the playback speed of the current SoundObject.
        /// </summary>
        /// <param name="speed">Speed between 0 and 1 at which to play the sound.</param>
        public void SetSpeed(float speed)
        {
            FreeSL.fslSoundSetSpeed(_sound, speed);
        }

        /// <summary>
        /// Sets the gain of the current SoundObject.
        /// </summary>
        /// <param name="gain">Positive or negative gain to apply to the sound.</param>
        public void SetGain( float gain ){
	        FreeSL.fslSoundSetGain( _sound, gain );
        }

        public virtual void Update()
        {
        }

        internal void AutoDelete()
        {
            if (destroyAfterPlaying && !IsPlaying() && wasPlayed)
                Destroy();
        }
    }
}
