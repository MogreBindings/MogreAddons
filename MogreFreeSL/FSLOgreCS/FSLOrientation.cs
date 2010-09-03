using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public struct FSLOrientation
    {
        public float atx;
        public float aty;
        public float atz;

        public float upx;
        public float upy;
        public float upz;

        public FSLOrientation(float atx, float aty, float atz, float upx, float upy, float upz)
        {
            this.atx = atx;
            this.aty = aty;
            this.atz = atz;

            this.upx = upx;
            this.upy = upy;
            this.upz = upz;
        }
    }
}
