using System;
using System.Collections.Generic;
using System.Text;

namespace MogreFreeSL
{
    /// <summary>
    /// A sound that will play the same way regardless of the camera's position. Instantiated through FSLSoundManager.CreateAmbientSound()
    /// </summary>
    public class AmbientSound : SoundObject
    {
        internal AmbientSound( string soundFile, string name, bool loop,bool streaming ) 
            : base( soundFile, name, loop, streaming )
        {
	        FreeSL.fslSoundSetSourceRelative( _sound, true);
        }
        internal AmbientSound(string package, string soundFile, string name, bool loop)
            : base(package, soundFile, name, loop)
        {
            FreeSL.fslSoundSetSourceRelative(_sound, true);
        }

        internal override void Update()
        {
            AutoDelete();
        }

        /// <summary>
        /// Removes the sound from the SoundManager and frees the space it consumed.
        /// </summary>
        public override void RemoveSound()
        {
            SoundManager.Instance.RemoveAmbientSound(this);
        }
    }
}
