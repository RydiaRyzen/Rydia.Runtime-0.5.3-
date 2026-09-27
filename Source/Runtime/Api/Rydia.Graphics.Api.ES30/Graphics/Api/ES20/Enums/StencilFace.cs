using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.StencilFuncSeparate, GL.StencilMaskSeparate and 1 other function
    public enum StencilFace
    {
        //
        // 概要:
        //     Original was GL_FRONT = 0X0404
        Front = 1028,
        //
        // 概要:
        //     Original was GL_BACK = 0X0405
        Back = 1029,
        //
        // 概要:
        //     Original was GL_FRONT_AND_BACK = 0x0408
        FrontAndBack = 1032
    }

}
