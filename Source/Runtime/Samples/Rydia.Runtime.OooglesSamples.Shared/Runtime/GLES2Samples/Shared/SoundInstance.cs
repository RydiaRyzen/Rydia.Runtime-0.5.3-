using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Audio.Api.OpenAL;
using Rydia.Graphics;
using Rydia.Resources;
using Rydia.Runtime;
using Rydia.Runtime.GLES2Samples.Shared;
using Rydia.Serialization;

namespace Rydia.Audio
{
    public class SoundInstance : DisposableBase
    {

        public SoundInstance()
        {
            AudioBuffer = AL.GenBuffer();
            AudioSource = AL.GenSource();
        }

        [Ignore]
        public int AudioBuffer
        {
            get;
            protected set;
        }

        [Ignore]
        public int AudioSource
        {
            get;
            protected set;
        }

        [Ignore]
        protected IAL AL
        {
            get
            {
                return AudioHost.AL;
            }
        }

        public void Play()
        {
            AL.SourcePlay(AudioSource);
        }

        public void Pause()
        {
            AL.SourcePause(AudioSource);
        }

        public void Rewind()
        {
            AL.SourceRewind(AudioSource);
        }

        public void Stop()
        {
            AL.SourceStop(AudioSource);
        }

        public void CreateBufferAndSource(Sound sound)
        {
            var alBuffer = new NativeBuffer<short>(sound.AudioData.PCMData);
            AL.BufferData(AudioBuffer, sound.AudioData.Channels == 1 ? ALFormat.Mono16 : ALFormat.Stereo16,
            alBuffer,
            sound.AudioData.PCMData.Length * sizeof(short),
            sound.AudioData.SampleRate);
            AL.Source(AudioSource, ALSourcei.Buffer, AudioBuffer);
        }

        protected override void Disposing(bool disposing)
        {
            AL.SourceStop(AudioSource);
            AL.DeleteSource(AudioSource);
            AL.DeleteBuffer(AudioBuffer);
        }
    }

}
