using System;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Api.ES30;
using Rydia.Graphics.Backend;
using Rydia.Runtime;


namespace Rydia.Runtime.GLES2Samples.Shared
{
    public static class TextureUtils
    {

        public static IGLES30 GL
        {
            get
            {
                return RuntimeHost.GLES30;
            }
        }

        

        public static NativeTexture CreateMipmappedTexture2D()
        {
            const int Width = 256;
            const int Height = 256;
            const int CheckerSize = 8;

            byte[] pixels = new byte[Width * Height * 3];
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    int r, b;
                    if (((x / CheckerSize) & 1) == 0)
                    {
                        r = 255 * ((y / CheckerSize) & 1);
                        b = 255 * (1 - ((y / CheckerSize) & 1));
                    }
                    else
                    {
                        b = 255 * ((y / CheckerSize) & 1);
                        r = 255 * (1 - ((y / CheckerSize) & 1));
                    }

                    pixels[(y * Height + x) * 3 + 0] = (byte)r;
                    pixels[(y * Height + x) * 3 + 1] = 0;
                    pixels[(y * Height + x) * 3 + 2] = (byte)b;
                }
            }

            // Generate a texture object
            NativeTexture texture = new NativeTexture();

            // Bind the texture object
            texture.Bind();

            // Load mipmap level 0
            texture.TexImage2D(PixelFormat.Rgb, Width, Height, pixels);

            // Generate mipmaps
            texture.GenerateMipmap();

            // Set the filtering mode
            texture.MinificationFilter = TextureMinFilter.NearestMipmapNearest;
            texture.MagnificationFilter = TextureMagFilter.Linear;

            return texture;
        }

        public static NativeTexture CreateSimpleTextureCubeMap()
        {
            byte[][] Pixels = {
                // Face 0 - Red
                new byte[] { 255,   0,   0 },

                // Face 1 - Green,
                new byte[] {   0, 255,   0 },

                // Face 3 - Blue
                new byte[] {   0,   0, 255 },

                // Face 4 - Yellow
                new byte[] { 255, 255,   0 },

                // Face 5 - Purple
                new byte[] { 255,   0, 255 },

                // Face 6 - White
                new byte[] { 255, 255, 255 } };

            // Generate a texture object
            NativeTexture texture = new NativeTexture(TextureTargetType.CubeMap);

            // Bind the texture object
            texture.Bind();

            for (int i = 0; i < Pixels.Length; i++)
            {
                texture.TexImage2D(PixelFormat.Rgb, 1, 1, Pixels[i], 0, PixelDataType.UnsignedByte, i);
            }

            // Set the filtering mode
            texture.MinificationFilter = TextureMinFilter.Nearest;
            texture.MagnificationFilter = TextureMagFilter.Nearest;

            return texture;
        }

    }
}
