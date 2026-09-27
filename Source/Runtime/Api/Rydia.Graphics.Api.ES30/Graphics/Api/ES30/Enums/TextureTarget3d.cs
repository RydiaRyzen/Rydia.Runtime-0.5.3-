using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.CompressedTexImage3D, GL.CompressedTexSubImage3D and 10 other functions
    public enum TextureTarget3d
    {
        //
        // 概要:
        //     Original was GL_TEXTURE_3D = 0x806F
        Texture3D = 32879,
        //
        // 概要:
        //     Original was GL_TEXTURE_3D_OES = 0x806F
        Texture3DOes = 32879,
        //
        // 概要:
        //     Original was GL_TEXTURE_2D_ARRAY = 0x8C1A
        Texture2DArray = 35866
    }
}
