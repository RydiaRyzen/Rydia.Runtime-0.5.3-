using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Resources;

namespace Rydia.ResourceConverter.Assets.Importer
{

    public static class PixelDataImporter
    {

        private static Dictionary<string, IResourceImporter<PixelData>> _Importers =
            new Dictionary<string, IResourceImporter<PixelData>>();

        public static string Jpeg
        {
            get;
            private set;
        }

        public static string Png
        {
            get;
            private set;
        }

        public static string Bmp
        {
            get;
            private set;
        }

        public static string Tga
        {
            get;
            private set;
        }

        static PixelDataImporter()
        {
            Jpeg = "jpeg";
            Png = "png";
            Bmp = "bmp";
            Tga = "tga";
        }

        public static void Init()
        {
            var jpeg = new JpegImporter();
            Registered(Jpeg, jpeg);
            Registered(Png, jpeg);
            Registered(Bmp, jpeg);
            Registered(Tga, new TgaImporter());
        }

        public static void Registered(string key, IResourceImporter<PixelData> impoter)
        {
            _Importers[key] = impoter;
        }

        public static IResourceImporter<PixelData> Get(string key)
        {
            return _Importers[key];
        }

    }

}
