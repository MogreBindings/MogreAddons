using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mogre;
using System.ComponentModel;
using MogreDesignSupport.ProxyClasses;

namespace TestApplication
{
    public class TestObject
    {
        public ColourValue CV { get; set; }

        public Matrix3 M3 { get; set; }
        public Matrix3 M4 { get; set; }

        public Vector2 V2 { get; set; }

        public Vector3 V3 { get; set; }

        public Vector4 V4 { get; set; }

        public Quaternion Q { get; set; }

        public AxisAlignedBox AAB { get; set; }

        public Mogre.Root Root {get; set;}

        public Mogre.SceneManager Mgr
        {
            get { return Root.GetSceneManager("Main"); }
        }

        public SceneManagerProxy MgrProxy
        {
            get { return new SceneManagerProxy(this.Mgr); }
        }

        public TestObject()
        {
            AAB = new AxisAlignedBox(Vector3.ZERO,Vector3.UNIT_SCALE);
            M3 = new Matrix3();
            Root = new Mogre.Root();
            Root.CreateSceneManager(SceneType.ST_GENERIC,"Main");
        }


    }


}
