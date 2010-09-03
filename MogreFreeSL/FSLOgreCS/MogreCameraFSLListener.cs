using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class MogreCameraFSLListener : IFSLListener
    {
        private Mogre.Camera _renderable;
        public bool ZFlipped = false;

        public MogreCameraFSLListener()
        {
            _renderable = null;
        }

        public MogreCameraFSLListener(Mogre.Camera renderable):this()
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

        public FSLPosition Position
        {
            get
            {
                Mogre.Vector3 position = _renderable.Position;
                return new FSLPosition(position.x, position.y, position.z);
            }
        }

        public FSLOrientation Orientation
        {
            get
            {
                int zflip = (ZFlipped) ? -1 : 1; // added

                Mogre.Vector3 yVec, zVec;
                yVec = _renderable.RealOrientation.YAxis;
                zVec = _renderable.RealOrientation.ZAxis * zflip;// change

                return new FSLOrientation(zVec.x, zVec.y, zVec.z, yVec.x, yVec.y, yVec.z);
            }
        }
    }
}
