using System;
using System.Collections.Generic;
using System.Text;

namespace MogreFreeSL
{
    /// <summary>
    /// A "listener," upon which the 3D effect for SoundEntities is based.
    /// </summary>
    public class Listener
    {
        private Mogre.Camera _renderable;

        /// <summary>
        /// Variable which fixes incorrect rendering of sound in 3D space. Do not call unless you have issues with SoundEntities rendering improperly.
        /// </summary>
        public bool ZFlipped = false;

        internal Listener()
        {
            _renderable = null;
        }
	
	    internal Listener( Mogre.Camera renderable)
        {
            _renderable = renderable;
        }

        internal void Update()
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

        /// <summary>
        /// Gives the Vector3 position of the Listener.
        /// </summary>
        /// <returns></returns>
        public Mogre.Vector3 GetPosition()
        {
            return _renderable.Position;
        }
    }
}
