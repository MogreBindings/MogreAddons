using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class FSLSoundEntity : FSLSoundObject
    {
        protected Mogre.IRenderable _renderable;
    	
	    public FSLSoundEntity(string soundFile , Mogre.IRenderable renderable, string name , bool loop, bool streaming)
            : base(soundFile, name, loop, streaming)
        {
            _renderable = renderable;
            SetReferenceDistance( 80.0f );
        }
        public FSLSoundEntity(string package, string soundFile, Mogre.IRenderable renderable, string name, bool loop)
            : base(package, soundFile, name, loop)
        {
            _renderable = renderable;
            SetReferenceDistance(80.0f);
        }
        public void SetRenderable( Mogre.IRenderable renderable){
            _renderable = renderable;
        }

        public override void Update(){
            FreeSL.fslSoundSetPosition( _sound, 
                _renderable.WorldPosition.x,
                _renderable.WorldPosition.y,
                _renderable.WorldPosition.z );
        }

        public void SetMaxDistance(float distance){
            FreeSL.fslSoundSetMaxDistance(_sound, distance);
        }

        public void SetReferenceDistance(float distance){
            FreeSL.fslSoundSetReferenceDistance( _sound, distance );
        }
    }
}
