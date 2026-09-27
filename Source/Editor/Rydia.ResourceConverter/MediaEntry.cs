using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.ResourceConverter.Assets.Importer;
using Rydia.Resources;

namespace Rydia.ResourceConverter
{
    internal class MediaEntry
    {

        public string FileName { get; set; }
        public string RelativePath { get; set; }
        public long SizeBytes { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime LastWriteUtc { get; set; }
        public string MD5 { get; set; }

        public byte[] FileAllBytes
        {
            get;
            set;
        }

        internal void Serialize()
        {
            if (!PixImport())
            {
                AudImport();
            }
        }

        private bool PixImport()
        {
            // インポート操作
            var ext = Path.GetExtension(RelativePath);
            IResourceImporter<PixelData> pixImporter = null;
            if (ext == ".jpeg" || ext == ".jpg")
            {
                pixImporter = PixelDataImporter.Get(PixelDataImporter.Jpeg);
            }
            else if (ext == ".png")
            {
                pixImporter = PixelDataImporter.Get(PixelDataImporter.Png);
            }
            else if (ext == ".bmp")
            {
                pixImporter = PixelDataImporter.Get(PixelDataImporter.Bmp);
            }
            else if (ext == ".tga")
            {
                pixImporter = PixelDataImporter.Get(PixelDataImporter.Tga);
            }
            if (pixImporter != null)
            {
                PixImport(pixImporter);
                return true;
            }
            return false;
        }

        private void PixImport(IResourceImporter<PixelData> pixImporter)
        {
            if (!pixImporter.TryImport(FileAllBytes, out PixelData pixResource))
            {
                throw new Exception("インポート失敗");
            }
            else
            {
                var name = Path.GetFileNameWithoutExtension(FileName);

                var filename = name + $".{pixResource.GetType().Name}.ryd";
                var path = Path.Combine("Assets", filename);
                var dir = Path.GetDirectoryName(path);
                var ms = new MemoryStream();
                new Rydia.Serialization.BinarySerializer().Serialize(ms, pixResource);
                var serialized = Convert.ToBase64String(ms.ToArray());
                Directory.CreateDirectory(dir);
                File.WriteAllText(path, serialized);
            }
        }

        private void AudImport()
        {
            // インポート操作
            var ext = Path.GetExtension(RelativePath);
            IResourceImporter<AudioData> audImporter;
            if (ext == ".wav")
            {
                audImporter = AudioDataImporter.Get(AudioDataImporter.Wav);
                AudImport(audImporter);
                return;
            }
            else if (ext == ".ogg")
            {
                audImporter = AudioDataImporter.Get(AudioDataImporter.Ogg);
                AudImport(audImporter);
                return;
            }
            else if (ext == ".mp3")
            {
                audImporter = AudioDataImporter.Get(AudioDataImporter.Mp3);
                AudImport(audImporter);
                return;
            }
        }

        private void AudImport(IResourceImporter<AudioData> audImporter)
        {
            if (!audImporter.TryImport(FileAllBytes, out AudioData audResource))
            {
                throw new Exception("インポート失敗");
            }
            else
            {
                audResource.Name = Path.GetFileNameWithoutExtension(FileName);
                var filename = audResource.Name + $".{audResource.GetType().Name}.ryd";
                var path = Path.Combine("Assets", filename);
                var dir = Path.GetDirectoryName(path);
                var ms = new MemoryStream();
                new Rydia.Serialization.BinarySerializer().Serialize(ms, audResource);
                var serialized = Convert.ToBase64String(ms.ToArray());
                Directory.CreateDirectory(dir);
                File.WriteAllText(path, serialized);
            }
        }


    }
}
