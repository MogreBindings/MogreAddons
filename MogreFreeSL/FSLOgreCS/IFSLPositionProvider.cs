using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public interface IFSLPositionProvider
    {
        FSLPosition Position { get; }
    }

    public class FSLPositionProvider:IFSLPositionProvider
    {
        #region IPositionProvider Members
        public FSLPosition Position
        {
            get;
            set;
        }
        #endregion

        public FSLPositionProvider(){}
        public FSLPositionProvider(FSLPosition position)
        {
            this.Position=position;
        }
    }

    public abstract class FSLBasePositionProvider:IFSLPositionProvider
    {
        #region IPositionProvider Members

        public FSLPosition Position
        {
            get { return OnGetPosition(); }
        }

        #endregion

        protected abstract FSLPosition OnGetPosition();
    }
}
