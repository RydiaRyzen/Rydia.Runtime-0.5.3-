using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Resources;

namespace Rydia.ResourceConverter.Assets.Importer
{

    public class WavImporter : IResourceImporter<AudioData>
    {
        public bool TryImport(byte[] data, out AudioData resource)
        {
            try
            {
                var audioData = new AudioData();
                audioData.PCMData = DecodeWav(data, out int channels, out int sampleRate);
                audioData.SampleRate = sampleRate;
                audioData.Channels = channels;
                resource = audioData;
                return true;
            }
            catch (Exception e)
            {
                resource = null;
                return false;
            }
        }

        private short[] DecodeWav(byte[] wavData, out int channels, out int sampleRate)
        {
            using (MemoryStream ms = new MemoryStream(wavData))
            using (BinaryReader reader = new BinaryReader(ms))
            {
                reader.BaseStream.Seek(22, SeekOrigin.Begin); // チャネル数
                channels = reader.ReadInt16();

                reader.BaseStream.Seek(24, SeekOrigin.Begin); // サンプリングレート
                sampleRate = reader.ReadInt32();

                reader.BaseStream.Seek(44, SeekOrigin.Begin); // PCMデータ開始
                byte[] pcmBytes = reader.ReadBytes((int)(reader.BaseStream.Length - 44));

                short[] pcmData = new short[pcmBytes.Length / 2];
                Buffer.BlockCopy(pcmBytes, 0, pcmData, 0, pcmBytes.Length);
                return pcmData;
            }
        }

    }

}
