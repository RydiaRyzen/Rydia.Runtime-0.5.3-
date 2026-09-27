using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetSamplerParameter, GL.SamplerParameter
    public enum SamplerParameterName
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
        //     Original was GL_TEXTURE_WRAP_R = 0x8072
        TextureWrapR = 32882,
        //
        // 概要:
        //     Original was GL_TEXTURE_MIN_LOD = 0x813A
        TextureMinLod = 33082,
        //
        // 概要:
        //     Original was GL_TEXTURE_MAX_LOD = 0x813B
        TextureMaxLod = 33083,
        //
        // 概要:
        //     Original was GL_TEXTURE_COMPARE_MODE = 0x884C
        TextureCompareMode = 34892,
        //
        // 概要:
        //     Original was GL_TEXTURE_COMPARE_FUNC = 0x884D
        TextureCompareFunc = 34893
    }
}
