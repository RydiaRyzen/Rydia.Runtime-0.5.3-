using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.CompressedTexImage2D, GL.CompressedTexSubImage2D and 6 other functions
    public enum TextureTarget2d
    {
        //
        // 概要:
        //     Original was GL_TEXTURE_2D = 0x0DE1
        Texture2D = 3553,
        //
        // 概要:
        //     Original was GL_TEXTURE_CUBE_MAP_POSITIVE_X = 0x8515
        TextureCubeMapPositiveX = 34069,
        //
        // 概要:
        //     Original was GL_TEXTURE_CUBE_MAP_NEGATIVE_X = 0x8516
        TextureCubeMapNegativeX = 34070,
        //
        // 概要:
        //     Original was GL_TEXTURE_CUBE_MAP_POSITIVE_Y = 0x8517
        TextureCubeMapPositiveY = 34071,
        //
        // 概要:
        //     Original was GL_TEXTURE_CUBE_MAP_NEGATIVE_Y = 0x8518
        TextureCubeMapNegativeY = 34072,
        //
        // 概要:
        //     Original was GL_TEXTURE_CUBE_MAP_POSITIVE_Z = 0x8519
        TextureCubeMapPositiveZ = 34073,
        //
        // 概要:
        //     Original was GL_TEXTURE_CUBE_MAP_NEGATIVE_Z = 0x851A
        TextureCubeMapNegativeZ = 34074
    }

}
