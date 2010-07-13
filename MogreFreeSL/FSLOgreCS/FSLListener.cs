using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class FSLListener
    {
        private Mogre.Camera _renderable;
        public bool ZFlipped = false;

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
            int zflip = (ZFlipped) ? -1 : 1; // added

            FreeSL.fslSetListenerPosition(_renderable.RealPosition.x,
                                          _renderable.RealPosition.y,
                                          _renderable.RealPosition.z);

            Mogre.Vector3 yVec, zVec;
            yVec = _renderable.RealOrientation.YAxis;
            zVec = _renderable.RealOrientation.ZAxis * zflip;// change

            FreeSL.fslSetListenerOrientation(zVec.x, zVec.y, zVec.z, yVec.x, yVec.y, yVec.z);
        }

        public Mogre.Vector3 GetPosition()
        {
            return _renderable.Position;
        }
    }
}
