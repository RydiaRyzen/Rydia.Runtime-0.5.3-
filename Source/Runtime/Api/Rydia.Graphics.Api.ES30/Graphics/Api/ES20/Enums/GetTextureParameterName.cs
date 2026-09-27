using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.GetTexParameter
    public enum GetTextureParameterName
    {
        //
        // 概要:
        //     Original was GL_TEXTURE_MAG_FILTER = 0x2800
        TextureMagFilter = 10240,
        //
        // 概要:
        //     Original was GL_TEXTURE_MIN_FILTER = 0x2801
        TextureMinFilter = 10241,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_S = 0x2802
        TextureWrapS = 10242,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_T = 0x2803
        TextureWrapT = 10243,
        //
        // 概要:
        //     Original was GL_TEXTURE_WRAP_R_OES = 0x8072
        TextureWrapROes = 32882,
        //
        // 概要:
        //     Original was GL_TEXTURE_IMMUTABLE_FORMAT_EXT = 0x912F
        TextureImmutableFormatExt = 37167
    }

}
