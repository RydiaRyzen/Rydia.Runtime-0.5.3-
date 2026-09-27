using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Rydia.Resources;

namespace Rydia.ResourceConverter.Assets.Importer
{

    public class TgaImporter : IResourceImporter<PixelData>
    {

        public bool TryImport(byte[] data, out PixelData resource)
        {
            try
            {
                var tga = new TgaImage(data);
                resource = new PixelData();
                resource.Data = tga.Data;
                resource.Width = tga.Width;
                resource.Height = tga.Height;
                return true;
            }
            catch (Exception ex)
            {
                resource = null;
                return false;
            }
        }

        public class TgaImage
        {

            private const int InvertedBit = 1 << 5;

            public int Width { get; }
            public int Height { get; }
            public byte[] Data { get; }

            [StructLayout(LayoutKind.Sequential, Pack = 1)]
            private unsafe struct Header
            {
                public byte size;
                public byte mapType;
                public byte imageType;
                public ushort paletteStart;
                public ushort paletteSize;
                public byte paletteEntryDepth;
                public ushort x;
                public ushort y;
                public ushort width;
                public ushort height;
                public byte colorDepth;
                public byte descriptor;
            }

            public unsafe TgaImage(byte[] bytes)
            {
                fixed (byte* p = &bytes[0])
                {
                    Header* header = (Header*)p;
                    Width = header->width;
                    Height = header->height;
                    int pixelComponentCount = header->colorDepth / 8;

                    if ((Width == 0) || (Height == 0) || (pixelComponentCount == 0))
                        throw new Exception($"Unable to load texture bytes.");

                    Data = new byte[Width * Height * 4];
                    int targetIdx = 0;
                    for (int y = 0; y < Height; y++)
                    {
                        int rowIdx = ((header->descriptor & InvertedBit) == 0) ? y : Height - 1 - y;
                        rowIdx = sizeof(Header) + (rowIdx * Width * pixelComponentCount);

                        for (int x = 0; x < Width; x++)
                        {
                            int pixelIdx = rowIdx + (x * pixelComponentCount);
                            Data[targetIdx++] = (pixelComponentCount > 2) ? bytes[pixelIdx + 2] : (byte)0;
                            Data[targetIdx++] = (pixelComponentCount > 1) ? bytes[pixelIdx + 1] : (byte)0;
                            Data[targetIdx++] = (pixelComponentCount > 0) ? bytes[pixelIdx + 0] : (byte)0;
                            Data[targetIdx++] = (pixelComponentCount > 3) ? bytes[pixelIdx + 2] : (byte)0xff;
                        }
                    }
                }
            }

        }

    }

}
