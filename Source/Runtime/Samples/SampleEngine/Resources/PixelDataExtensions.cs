using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;

namespace Rydia.Resources
{

    public static class PixelDataExtensions
    {

        public static NativeTexture ToTexture(this PixelData pixelData)
        {
            NativeTexture texture = new NativeTexture();
            texture.Bind();
            texture.MinificationFilter = TextureMinFilter.LinearMipmapLinear;
            texture.MagnificationFilter = TextureMagFilter.Linear;
            RuntimeHost.GLES30.PixelStore(PixelStoreParameter.UnpackAlignment, PixelStoreValue.One);
            texture.TexImage2D(PixelFormat.Rgba, pixelData.Width, pixelData.Height, pixelData.Data);
            texture.GenerateMipmap();
            return texture;
        }

    }

}
