using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Backend;
using Rydia.IO;
using Rydia.Resources;
using Rydia.Serialization;

namespace SampleEngine.Drawing2D
{
    internal class Texture2D
    {

        public NativeTexture Texture
        {
            get;
        }

        public int Width { get; }
        public int Height { get; }

        public Texture2D(PixelData image)
        {
            Width = image.Width;
            Height = image.Height;

            Texture = new NativeTexture(Rydia.Graphics.Api.ES20.TextureTargetType.TwoD);
            Texture.Bind();

            Texture.MinificationFilter = Rydia.Graphics.Api.ES20.TextureMinFilter.Linear;
            Texture.MagnificationFilter = Rydia.Graphics.Api.ES20.TextureMagFilter.Linear;
            Texture.WrapS = Rydia.Graphics.Api.ES20.TextureWrapMode.ClampToEdge;
            Texture.WrapT = Rydia.Graphics.Api.ES20.TextureWrapMode.ClampToEdge;

            byte[] pixels = image.Data;

            Texture.TexImage2D(Rydia.Graphics.Api.ES20.PixelFormat.Rgba, Width, Height, pixels);
            Texture.Unbind();
        }

        public static Texture2D LoadEmbedded<T>(string filename)
        {
            var embedded = new EmbeddedResources<T>();

            var str = embedded.LoadText(filename);
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<PixelData>(temp);
            return new Texture2D(resource);
        }


    }
}
