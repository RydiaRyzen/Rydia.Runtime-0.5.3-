using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Audio;
using Rydia.Resources;

namespace Rydia.Runtime.GLES2Samples.Shared
{

    public class Sound
    {

        public Sound() { }

        public AudioData AudioData
        {
            get;
            set;
        }

        public SoundInstance SoundInstance
        {
            get;
            private set;
        }

        public void CreateBufferAndSource()
        {
            var instance = new SoundInstance();
            instance.CreateBufferAndSource(this);
            SoundInstance = instance;
        }

    }

}
