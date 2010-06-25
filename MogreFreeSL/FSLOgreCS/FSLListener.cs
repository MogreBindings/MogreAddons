using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class FSLListener
    {
        private Mogre.Camera _renderable;
    	public FSLListener()
        {
            _renderable = null;
        }
	
	    public FSLListener( Mogre.Camera renderable)
        {
            _renderable = renderable;
        }

        public void SetListener(Mogre.Camera renderable)
        {
            _renderable = renderable;
        }
    	
	    public void Update()
        {
            FreeSL.fslSetListenerPosition(_renderable.Position.x,
                _renderable.Position.y,
                _renderable.Position.z);
            Mogre.Vector3 yVec, zVec;
            yVec = _renderable.Orientation.YAxis;
            zVec = _renderable.Orientation.ZAxis;
            FreeSL.fslSetListenerOrientation(zVec.x, zVec.y, zVec.z, yVec.x, yVec.y, yVec.z);
        }

        public Mogre.Vector3 GetPosition()
        {
            return _renderable.Position;
        }
    }
}
