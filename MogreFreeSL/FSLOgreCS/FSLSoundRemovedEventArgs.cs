using System;
using System.Collections.Generic;
using System.Text;

namespace FSLOgreCS
{
    public class FSLSoundRemovedEventArgs : EventArgs
    {
        public FSLSoundObject Sound { get; protected set; }

        public FSLSoundRemovedEventArgs(FSLSoundObject sound)
        {
            this.Sound = sound;
        }
    }
}
