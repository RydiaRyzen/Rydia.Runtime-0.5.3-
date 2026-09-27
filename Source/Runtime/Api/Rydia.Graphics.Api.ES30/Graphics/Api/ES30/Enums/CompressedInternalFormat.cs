using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.CompressedTexImage2D, GL.CompressedTexImage3D and 1 other function
    public enum CompressedInternalFormat
    {
        //
        // 概要:
        //     Original was GL_ETC1_RGB8_OES = 0x8D64
        Etc1Rgb8Oes = 36196,
        //
        // 概要:
        //     Original was GL_COMPRESSED_R11_EAC = 0x9270
        CompressedR11Eac = 37488,
        //
        // 概要:
        //     Original was GL_COMPRESSED_SIGNED_R11_EAC = 0x9271
        CompressedSignedR11Eac = 37489,
        //
        // 概要:
        //     Original was GL_COMPRESSED_RG11_EAC = 0x9272
        CompressedRg11Eac = 37490,
        //
        // 概要:
        //     Original was GL_COMPRESSED_SIGNED_RG11_EAC = 0x9273
        CompressedSignedRg11Eac = 37491,
        //
        // 概要:
        //     Original was GL_COMPRESSED_RGB8_ETC2 = 0x9274
        CompressedRgb8Etc2 = 37492,
        //
        // 概要:
        //     Original was GL_COMPRESSED_SRGB8_ETC2 = 0x9275
        CompressedSrgb8Etc2 = 37493,
        //
        // 概要:
        //     Original was GL_COMPRESSED_RGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9276
        CompressedRgb8PunchthroughAlpha1Etc2 = 37494,
        //
        // 概要:
        //     Original was GL_COMPRESSED_SRGB8_PUNCHTHROUGH_ALPHA1_ETC2 = 0x9277
        CompressedSrgb8PunchthroughAlpha1Etc2 = 37495,
        //
        // 概要:
        //     Original was GL_COMPRESSED_RGBA8_ETC2_EAC = 0x9278
        CompressedRgba8Etc2Eac = 37496,
        //
        // 概要:
        //     Original was GL_COMPRESSED_SRGB8_ALPHA8_ETC2_EAC = 0x9279
        CompressedSrgb8Alpha8Etc2Eac = 37497
    }
}
