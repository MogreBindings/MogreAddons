using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class Vector3Position : FSLBasePositionProvider
    {
        public Mogre.Vector3 Vector3 { get; set; }

        public Vector3Position() { }
        public Vector3Position(Mogre.Vector3 vector3)
        {
            this.Vector3 = vector3;
        }

        protected override FSLPosition OnGetPosition()
        {
            return new FSLPosition(Vector3.x, Vector3.y, Vector3.z);
        }
    }

    public class NodePositionProvider : FSLBasePositionProvider
    {
        public Mogre.Node Node { get; set; }

        public NodePositionProvider(Mogre.Node node)
        {
            this.Node = node;
        }

        protected override FSLPosition OnGetPosition()
        {
            Mogre.Vector3 vector3 = Node._getDerivedPosition();
            return new FSLPosition(vector3.x, vector3.y, vector3.z);
        }
    }
}
