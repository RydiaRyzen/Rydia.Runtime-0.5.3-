using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Drawing;
using Rydia.IO;
using Rydia.Resources;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;
using Rydia.Runtime.Mobile;
using Rydia.Serialization;

namespace Rydia.Runtime.GLES2Samples
{

    internal class E24ALTestApp : GLES30App
    {
        private Sound wavSound;
        private Sound oggSound;
        private Sound mp3Sound;
        private int frame = 0;

        public override void Load()
        {

            // OpenALバッファとソースの作成

            var embedded = new EmbeddedResources<SampleApp>();


            /*
            byte[] tempData = embedded.Load(@"Assets.0_001.wav");
            AssetImporters<AudioData>.Get("wav").TryImport(tempData, out AudioData wavFile);
            wavFile.Name = "0_001";
            wavFile.Serialize();
            */
            this.wavSound = new Sound();
            var str = embedded.LoadText("Assets.0_001.AudioData.ryd");
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<AudioData>(temp);
            this.wavSound.AudioData = resource;
            this.wavSound.CreateBufferAndSource();
            
            /*
            tempData = embedded.Load(@"Assets.chicken_cluck_happy.ogg");
            AssetImporters<AudioData>.Get("ogg").TryImport(tempData, out AudioData oggFile);
            oggFile.Name = "chicken_cluck_happy";
            oggFile.Serialize();
            */
            this.oggSound = new Sound();
            str = embedded.LoadText(@"Assets.chicken_cluck_happy.AudioData.ryd");
            temp = Convert.FromBase64String(str);
            resource = new BinarySerializer().Deserialize<AudioData>(temp);
            this.oggSound.AudioData = resource;
            this.oggSound.CreateBufferAndSource();
            /*
            AssetImporters<AudioData>.Registered("mp3", new Mp3Importer());
            var tempData = embedded.Load(@"Assets.background.mp3");
            AssetImporters<AudioData>.Get("mp3").TryImport(tempData, out AudioData mp3File);
            mp3File.Name = "background";
            mp3File.Serialize();
            */
            this.mp3Sound = new Sound();
            str = embedded.LoadText(@"Assets.background.AudioData.ryd");
            temp = Convert.FromBase64String(str);
            resource = new BinarySerializer().Deserialize<AudioData>(temp);
            this.mp3Sound.AudioData = resource;
            this.mp3Sound.CreateBufferAndSource();

            GL.ClearColor(ColorRgba.DarkBlue);
        }

        public override void Render()
        {
            GL.Clear(Graphics.Api.ES20.ClearBufferMask.ColorBufferBit);
            if (this.frame == 0)
            {
                GL.ClearColor(ColorRgba.DarkBlue);
                this.wavSound.SoundInstance.Play();
            }
            else if (this.frame == 300)
            {
                GL.ClearColor(ColorRgba.Green);
                this.oggSound.SoundInstance.Play();
            }
            else if (this.frame == 600)
            {
                GL.ClearColor(ColorRgba.Yellow);
                this.mp3Sound.SoundInstance.Play();
            }
            else if (this.frame == 900)
            {
                this.frame = -1;
            }
            this.frame++;
        }

        public override void Unload()
        {
            // クリーンアップ
            this.wavSound.SoundInstance.Dispose();
            this.oggSound.SoundInstance.Dispose();
            this.mp3Sound.SoundInstance.Dispose();
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {
            if(Platform.IsAs<IDesktopPlatform>(out var desktopPlatform))
            {
                var keyboard = desktopPlatform.Window.Keyboard;
                var mouse = desktopPlatform.Window.Mouse;
            }
            else
            {
                var acc = Platform.As<IMobilePlatform>().Accelerometer;
                var keyboard = Platform.As<IMobilePlatform>().Keyboard;
                var touch = Platform.As<IMobilePlatform>().Touch;
            }
        }

    }
}
