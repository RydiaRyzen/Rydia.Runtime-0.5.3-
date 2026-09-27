using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Audio.Api.NVorbis;
using Rydia.Resources;

namespace Rydia.ResourceConverter.Assets.Importer
{

    public class OggImporter : IResourceImporter<AudioData>
    {

        public bool TryImport(byte[] data, out AudioData resource)
        {
            try
            {
                var audioData = new AudioData();
                audioData.PCMData = DecodeOgg(data, out int channels, out int sampleRate);
                audioData.SampleRate = sampleRate;
                audioData.Channels = channels;
                resource = audioData;
                return true;
            }
            catch
            {
                resource = null;
                return false;
            }
        }

        private short[] DecodeOgg(byte[] data, out int channels, out int sampleRate)
        {
            var srcStream = new MemoryStream(data);
            var dstStream = new MemoryStream();
            using (var vorbis = new VorbisReader(srcStream, false))
            {
                using (var writer = new BinaryWriter(dstStream))
                {

                    float[] buffer = new float[4096];
                    sampleRate = vorbis.SampleRate;
                    channels = vorbis.Channels;
                    // PCM データを書き込み
                    while (true)
                    {
                        int samples = vorbis.ReadSamples(buffer, 0, buffer.Length);
                        if (samples == 0)
                            break;
                        for (int i = 0; i < samples; i++)
                        {
                            short pcmSample = (short)(buffer[i] * short.MaxValue);
                            writer.Write(pcmSample);
                        }
                    }
                    return ConvertByteArrayToShortArray(dstStream.ToArray());
                }
            }
        }

        private static short[] ConvertByteArrayToShortArray(byte[] byteArray, bool isLittleEndian = true)
        {
            if (byteArray.Length % 2 != 0)
            {
                throw new ArgumentException("バイト配列の長さは偶数である必要があります。");
            }

            short[] shortArray = new short[byteArray.Length / 2];

            for (int i = 0; i < shortArray.Length; i++)
            {
                int byteIndex = i * 2;
                short value = BitConverter.ToInt16(byteArray, byteIndex);

                // エンディアン変換（必要なら）
                if (!isLittleEndian)
                {
                    value = (short)((value << 8) | ((value >> 8) & 0xFF));
                }

                shortArray[i] = value;
            }

            return shortArray;
        }

    }

}
