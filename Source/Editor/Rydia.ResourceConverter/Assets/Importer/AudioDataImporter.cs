using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Resources;

namespace Rydia.ResourceConverter.Assets.Importer
{

    public static class AudioDataImporter
    {

        private static Dictionary<string, IResourceImporter<AudioData>> _Importers =
            new Dictionary<string, IResourceImporter<AudioData>>();

        public static string Wav
        {
            get;
            private set;
        }

        public static string Ogg
        {
            get;
            private set;
        }

        public static string Mp3
        {
            get;
            private set;
        }

        static AudioDataImporter()
        {
            Wav = "wav";
            Ogg = "ogg";
            Mp3 = "mp3";
        }

        public static void Init()
        {
            Registered(Wav, new WavImporter());
            Registered(Ogg, new OggImporter());
            //Registered(Aiff, mp3);
            //Registered(Mp2, mp3);
            //Registered(Flac, mp3);
        }

        public static void Registered(string key, IResourceImporter<AudioData> impoter)
        {
            _Importers[key] = impoter;
        }

        public static IResourceImporter<AudioData> Get(string key)
        {
            return _Importers[key];
        }

    }

}
