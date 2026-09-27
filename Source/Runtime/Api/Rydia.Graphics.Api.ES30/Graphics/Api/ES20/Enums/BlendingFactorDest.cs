using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.BlendFunc, GL.BlendFuncSeparate
    public enum BlendingFactorDest
    {
        //
        // 概要:
        //     Original was GL_Zero = 0
        Zero = ESAllEnum.Zero,
        //
        // 概要:
        //     Original was GL_SRC_COLOR = 0x0300
        SrcColor = ESAllEnum.SrcColor,
        //
        // 概要:
        //     Original was GL_ONE_MINUS_SRC_COLOR = 0x0301
        OneMinusSrcColor = ESAllEnum.OneMinusSrcColor,
        //
        // 概要:
        //     Original was GL_SRC_ALPHA = 0x0302
        SrcAlpha = 770,
        //
        // 概要:
        //     Original was GL_ONE_MINUS_SRC_ALPHA = 0x0303
        OneMinusSrcAlpha = 771,
        //
        // 概要:
        //     Original was GL_DST_ALPHA = 0x0304
        DstAlpha = 772,
        //
        // 概要:
        //     Original was GL_ONE_MINUS_DST_ALPHA = 0x0305
        OneMinusDstAlpha = 773,
        //
        // 概要:
        //     Original was GL_DstColor = 0X0306
        DstColor = 774,
        //
        // 概要:
        //     Original was GL_OneMinusDstColor = 0X0307
        OneMinusDstColor = 775,
        //
        // 概要:
        //     Original was GL_SrcAlphaSaturate = 0X0308
        SrcAlphaSaturate = 776,
        //
        // 概要:
        //     Original was GL_CONSTANT_COLOR_EXT = 0x8001
        ConstantColorExt = 32769,
        //
        // 概要:
        //     Original was GL_ConstantColor = 0X8001
        ConstantColor = 32769,
        //
        // 概要:
        //     Original was GL_ONE_MINUS_CONSTANT_COLOR_EXT = 0x8002
        OneMinusConstantColorExt = 32770,
        //
        // 概要:
        //     Original was GL_OneMinusConstantColor = 0X8002
        OneMinusConstantColor = 32770,
        //
        // 概要:
        //     Original was GL_CONSTANT_ALPHA_EXT = 0x8003
        ConstantAlphaExt = 32771,
        //
        // 概要:
        //     Original was GL_ConstantAlpha = 0X8003
        ConstantAlpha = 32771,
        //
        // 概要:
        //     Original was GL_ONE_MINUS_CONSTANT_ALPHA_EXT = 0x8004
        OneMinusConstantAlphaExt = 32772,
        //
        // 概要:
        //     Original was GL_OneMinusConstantAlpha = 0X8004
        OneMinusConstantAlpha = 32772,
        //
        // 概要:
        //     Original was GL_One = 1
        One = 1
    }

}
