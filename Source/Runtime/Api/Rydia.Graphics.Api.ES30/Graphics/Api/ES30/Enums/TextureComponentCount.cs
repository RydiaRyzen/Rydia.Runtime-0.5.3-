using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.TexImage2D, GL.TexImage3D and 1 other function
    public enum TextureComponentCount
    {
        //
        // 概要:
        //     Original was GL_ALPHA = 0X1906
        Alpha = 6406,
        //
        // 概要:
        //     Original was GL_RGB = 0X1907
        Rgb = 6407,
        //
        // 概要:
        //     Original was GL_RGBA = 0X1908
        Rgba = 6408,
        //
        // 概要:
        //     Original was GL_LUMINANCE = 0X1909
        Luminance = 6409,
        //
        // 概要:
        //     Original was GL_LUMINANCE_ALPHA = 0x190A
        LuminanceAlpha = 6410,
        //
        // 概要:
        //     Original was GL_ALPHA8_EXT = 0x803C
        Alpha8Ext = 32828,
        //
        // 概要:
        //     Original was GL_LUMINANCE8_EXT = 0x8040
        Luminance8Ext = 32832,
        //
        // 概要:
        //     Original was GL_LUMINANCE8_ALPHA8_EXT = 0x8045
        Luminance8Alpha8Ext = 32837,
        //
        // 概要:
        //     Original was GL_RGB8 = 0x8051
        Rgb8 = 32849,
        //
        // 概要:
        //     Original was GL_RGB10_EXT = 0x8052
        Rgb10Ext = 32850,
        //
        // 概要:
        //     Original was GL_RGBA4 = 0X8056
        Rgba4 = 32854,
        //
        // 概要:
        //     Original was GL_RGB5_A1 = 0x8057
        Rgb5A1 = 32855,
        //
        // 概要:
        //     Original was GL_RGBA8 = 0x8058
        Rgba8 = 32856,
        //
        // 概要:
        //     Original was GL_RGB10_A2 = 0x8059
        Rgb10A2 = 32857,
        //
        // 概要:
        //     Original was GL_RGB10_A2_EXT = 0x8059
        Rgb10A2Ext = 32857,
        //
        // 概要:
        //     Original was GL_DEPTH_COMPONENT16 = 0x81A5
        DepthComponent16 = 33189,
        //
        // 概要:
        //     Original was GL_DEPTH_COMPONENT24 = 0x81A6
        DepthComponent24 = 33190,
        //
        // 概要:
        //     Original was GL_R8 = 0x8229
        R8 = 33321,
        //
        // 概要:
        //     Original was GL_R8_EXT = 0x8229
        R8Ext = 33321,
        //
        // 概要:
        //     Original was GL_RG8 = 0x822B
        Rg8 = 33323,
        //
        // 概要:
        //     Original was GL_RG8_EXT = 0x822B
        Rg8Ext = 33323,
        //
        // 概要:
        //     Original was GL_R16F = 0x822D
        R16f = 33325,
        //
        // 概要:
        //     Original was GL_R16F_EXT = 0x822D
        R16fExt = 33325,
        //
        // 概要:
        //     Original was GL_R32F = 0x822E
        R32f = 33326,
        //
        // 概要:
        //     Original was GL_R32F_EXT = 0x822E
        R32fExt = 33326,
        //
        // 概要:
        //     Original was GL_RG16F = 0x822F
        Rg16f = 33327,
        //
        // 概要:
        //     Original was GL_RG16F_EXT = 0x822F
        Rg16fExt = 33327,
        //
        // 概要:
        //     Original was GL_RG32F = 0x8230
        Rg32f = 33328,
        //
        // 概要:
        //     Original was GL_RG32F_EXT = 0x8230
        Rg32fExt = 33328,
        //
        // 概要:
        //     Original was GL_R8I = 0x8231
        R8i = 33329,
        //
        // 概要:
        //     Original was GL_R8UI = 0x8232
        R8ui = 33330,
        //
        // 概要:
        //     Original was GL_R16I = 0x8233
        R16i = 33331,
        //
        // 概要:
        //     Original was GL_R16UI = 0x8234
        R16ui = 33332,
        //
        // 概要:
        //     Original was GL_R32I = 0x8235
        R32i = 33333,
        //
        // 概要:
        //     Original was GL_R32UI = 0x8236
        R32ui = 33334,
        //
        // 概要:
        //     Original was GL_RG8I = 0x8237
        Rg8i = 33335,
        //
        // 概要:
        //     Original was GL_RG8UI = 0x8238
        Rg8ui = 33336,
        //
        // 概要:
        //     Original was GL_RG16I = 0x8239
        Rg16i = 33337,
        //
        // 概要:
        //     Original was GL_RG16UI = 0x823A
        Rg16ui = 33338,
        //
        // 概要:
        //     Original was GL_RG32I = 0x823B
        Rg32i = 33339,
        //
        // 概要:
        //     Original was GL_RG32UI = 0x823C
        Rg32ui = 33340,
        //
        // 概要:
        //     Original was GL_RGBA32F = 0x8814
        Rgba32f = 34836,
        //
        // 概要:
        //     Original was GL_RGBA32F_EXT = 0x8814
        Rgba32fExt = 34836,
        //
        // 概要:
        //     Original was GL_RGB32F = 0x8815
        Rgb32f = 34837,
        //
        // 概要:
        //     Original was GL_RGB32F_EXT = 0x8815
        Rgb32fExt = 34837,
        //
        // 概要:
        //     Original was GL_ALPHA32F_EXT = 0x8816
        Alpha32fExt = 34838,
        //
        // 概要:
        //     Original was GL_LUMINANCE32F_EXT = 0x8818
        Luminance32fExt = 34840,
        //
        // 概要:
        //     Original was GL_LUMINANCE_ALPHA32F_EXT = 0x8819
        LuminanceAlpha32fExt = 34841,
        //
        // 概要:
        //     Original was GL_RGBA16F = 0x881A
        Rgba16f = 34842,
        //
        // 概要:
        //     Original was GL_RGBA16F_EXT = 0x881A
        Rgba16fExt = 34842,
        //
        // 概要:
        //     Original was GL_RGB16F = 0x881B
        Rgb16f = 34843,
        //
        // 概要:
        //     Original was GL_RGB16F_EXT = 0x881B
        Rgb16fExt = 34843,
        //
        // 概要:
        //     Original was GL_ALPHA16F_EXT = 0x881C
        Alpha16fExt = 34844,
        //
        // 概要:
        //     Original was GL_LUMINANCE16F_EXT = 0x881E
        Luminance16fExt = 34846,
        //
        // 概要:
        //     Original was GL_LUMINANCE_ALPHA16F_EXT = 0x881F
        LuminanceAlpha16fExt = 34847,
        //
        // 概要:
        //     Original was GL_DEPTH24_STENCIL8 = 0x88F0
        Depth24Stencil8 = 35056,
        //
        // 概要:
        //     Original was GL_RGB_RAW_422_APPLE = 0x8A51
        RgbRaw422Apple = 35409,
        //
        // 概要:
        //     Original was GL_R11F_G11F_B10F = 0x8C3A
        R11fG11fB10f = 35898,
        //
        // 概要:
        //     Original was GL_RGB9_E5 = 0x8C3D
        Rgb9E5 = 35901,
        //
        // 概要:
        //     Original was GL_SRGB8 = 0x8C41
        Srgb8 = 35905,
        //
        // 概要:
        //     Original was GL_SRGB8_ALPHA8 = 0x8C43
        Srgb8Alpha8 = 35907,
        //
        // 概要:
        //     Original was GL_DEPTH_COMPONENT32F = 0x8CAC
        DepthComponent32f = 36012,
        //
        // 概要:
        //     Original was GL_DEPTH32F_STENCIL8 = 0x8CAD
        Depth32fStencil8 = 36013,
        //
        // 概要:
        //     Original was GL_RGB565 = 0X8d62
        Rgb565 = 36194,
        //
        // 概要:
        //     Original was GL_RGBA32UI = 0x8D70
        Rgba32ui = 36208,
        //
        // 概要:
        //     Original was GL_RGB32UI = 0x8D71
        Rgb32ui = 36209,
        //
        // 概要:
        //     Original was GL_RGBA16UI = 0x8D76
        Rgba16ui = 36214,
        //
        // 概要:
        //     Original was GL_RGB16UI = 0x8D77
        Rgb16ui = 36215,
        //
        // 概要:
        //     Original was GL_RGBA8UI = 0x8D7C
        Rgba8ui = 36220,
        //
        // 概要:
        //     Original was GL_RGB8UI = 0x8D7D
        Rgb8ui = 36221,
        //
        // 概要:
        //     Original was GL_RGBA32I = 0x8D82
        Rgba32i = 36226,
        //
        // 概要:
        //     Original was GL_RGB32I = 0x8D83
        Rgb32i = 36227,
        //
        // 概要:
        //     Original was GL_RGBA16I = 0x8D88
        Rgba16i = 36232,
        //
        // 概要:
        //     Original was GL_RGB16I = 0x8D89
        Rgb16i = 36233,
        //
        // 概要:
        //     Original was GL_RGBA8I = 0x8D8E
        Rgba8i = 36238,
        //
        // 概要:
        //     Original was GL_RGB8I = 0x8D8F
        Rgb8i = 36239,
        //
        // 概要:
        //     Original was GL_R8_SNORM = 0x8F94
        R8Snorm = 36756,
        //
        // 概要:
        //     Original was GL_RG8_SNORM = 0x8F95
        Rg8Snorm = 36757,
        //
        // 概要:
        //     Original was GL_RGB8_SNORM = 0x8F96
        Rgb8Snorm = 36758,
        //
        // 概要:
        //     Original was GL_RGBA8_SNORM = 0x8F97
        Rgba8Snorm = 36759,
        //
        // 概要:
        //     Original was GL_RGB10_A2UI = 0x906F
        Rgb10A2ui = 36975,
        //
        // 概要:
        //     Original was GL_BGRA8_EXT = 0x93A1
        Bgra8Ext = 37793
    }
}
