using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Audio.Api.OpenAL;
using Rydia.Drawing;
using Rydia.Graphics;
using Rydia.Runtime.ES30;

namespace Rydia.Runtime
{
    
    public class E23ALSinewaveTestApp : GLES30App
    {

        private int frame;
        private int buffer293;
        private int source293;
        private int buffer440;
        private int source440;
        private int buffer587;
        private int source587;

        public override bool NeedStencilBuffer
        {
            get
            {
                return false;
            }
        }

        public override void Load()
        {
            CreateBufferAndSource(293.66f, ref this.buffer293, ref this.source293);
            CreateBufferAndSource(440.0f, ref this.buffer440, ref this.source440);
            CreateBufferAndSource(587.33f, ref this.buffer587, ref this.source587);
            GL.ClearColor(ColorRgba.CornflowerBlue);
        }

        private void CreateBufferAndSource(float frequency, ref int b, ref int s)
        {
            int sampleRate = 44100;
            int duration = 3; // 3 seconds
            int bufferSize = sampleRate * duration * 2;
            byte[] bufferData = new byte[bufferSize];

            // 正弦波データを生成
            for (int i = 0; i < bufferSize / 2; i++)
            {
                double time = (double)i / sampleRate;
                short sample = (short)(Math.Sin(2 * Math.PI * frequency * time) * short.MaxValue);
                bufferData[i * 2] = (byte)(sample & 0xFF);
                bufferData[i * 2 + 1] = (byte)((sample >> 8) & 0xFF);
            }
            var alBuffer = new NativeBuffer<byte>(bufferData);
            b = AL.GenBuffer();
            s = AL.GenSource();

            // OpenALバッファにデータを入れる
            AL.BufferData(b, ALFormat.Mono16, alBuffer, bufferData.Length * sizeof(byte), sampleRate);
            AL.Source(s, ALSourcei.Buffer, b);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            GL.Clear(Graphics.Api.ES20.ClearBufferMask.ColorBufferBit);
            if (this.frame == 0)
            {
                GL.ClearColor(ColorRgba.DarkBlue);
                AL.SourcePlay(this.source293);
            }
            else if (this.frame == 300)
            {
                GL.ClearColor(ColorRgba.Green);
                AL.SourcePlay(this.source440);
            }
            else if (this.frame == 600)
            {
                GL.ClearColor(ColorRgba.Yellow);
                AL.SourcePlay(this.source587);
            }
            else if (this.frame == 900)
            {
                this.frame = -1;
            }
            this.frame++;
        }

        public override void Resize(int width, int height)
        {
            base.Resize(width, height);
        }

        public override void Unload()
        {
            AL.DeleteSource(this.source293);
            AL.DeleteBuffer(this.buffer293);
            AL.DeleteSource(this.source440);
            AL.DeleteBuffer(this.buffer440);
            AL.DeleteSource(this.source587);
            AL.DeleteBuffer(this.buffer587);
        }

    }

}
