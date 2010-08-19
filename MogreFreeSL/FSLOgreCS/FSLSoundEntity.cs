using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class FSLSoundEntity : FSLSoundObject
    {
        protected Mogre.Node _node;
    	
	    public FSLSoundEntity(string soundFile , Mogre.Node node, string name , bool loop, bool streaming)
            : base(soundFile, name, loop, streaming)
        {
            _node = node;
            SetReferenceDistance( 80.0f );
        }

        public FSLSoundEntity(string package, string soundFile, Mogre.Node node, string name, bool loop)
            : base(package, soundFile, name, loop)
        {
            _node = node;
            SetReferenceDistance(80.0f);
        }

        public void SetRenderable( Mogre.Node node)
        {
            _node = node;
        }

        public override void Update()
        {
            Mogre.Vector3 pos = _node._getDerivedPosition();

            FreeSL.fslSoundSetPosition(_sound, pos.x, pos.y, pos.z);

            AutoDelete();
        }

        public void SetMaxDistance(float distance)
        {
            FreeSL.fslSoundSetMaxDistance(_sound, distance);
        }

        public void SetReferenceDistance(float distance)
        {
            FreeSL.fslSoundSetReferenceDistance( _sound, distance );
        }
    }
}