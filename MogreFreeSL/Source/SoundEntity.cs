using System;
using System.Collections.Generic;
using System.Text;

namespace MogreFreeSL
{
    /// <summary>
    /// A sound that will change in gain and balance depending on where the camera is in relation to a given node. Instantiated through FSLSoundManager.CreateSoundEntity()
    /// </summary>
    public class SoundEntity : SoundObject
    {
        internal Mogre.Node _node;

        internal SoundEntity(string soundFile, Mogre.Node node, string name, bool loop, bool streaming)
            : base(soundFile, name, loop, streaming)
        {
            _node = node;
            SetReferenceDistance( 80.0f );
        }

        internal SoundEntity(string package, string soundFile, Mogre.Node node, string name, bool loop)
            : base(package, soundFile, name, loop)
        {
            _node = node;
            SetReferenceDistance(80.0f);
        }

        /// <summary>
        /// Sets which node the SoundEntity is attached to.
        /// </summary>
        /// <param name="node"></param>
        public void SetNode( Mogre.Node node)
        {
            _node = node;
        }

        internal override void Update()
        {
            Mogre.Vector3 pos = _node._getDerivedPosition();

            FreeSL.fslSoundSetPosition(_sound, pos.x, pos.y, pos.z);

            AutoDelete();
        }

        /// <summary>
        /// Sets the distance from the camera at which this sound will no longer be audible.
        /// </summary>
        /// <param name="distance">Distance measured in MOGRE units</param>
        public void SetMaxDistance(float distance)
        {
            FreeSL.fslSoundSetMaxDistance(_sound, distance);
        }

        /// <summary>
        /// Sets the distance from the camera at which this sound will be at normal volume.
        /// </summary>
        /// <param name="distance">Distance measured in MOGRE units</param>
        public void SetReferenceDistance(float distance)
        {
            FreeSL.fslSoundSetReferenceDistance( _sound, distance );
        }

        /// <summary>
        /// Removes the sound from the SoundManager and frees the space it consumed.
        /// </summary>
        public override void RemoveSound()
        {
            SoundManager.Instance.RemoveSoundEntity(this);
        }
    }
}