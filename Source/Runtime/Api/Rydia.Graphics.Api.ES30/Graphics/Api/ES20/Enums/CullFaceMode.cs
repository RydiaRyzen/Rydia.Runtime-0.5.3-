using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.CullFace, GL.StencilFuncSeparate and 2 other functions
    public enum CullFaceMode
    {
        //
        // 概要:
        //     Original was GL_Front = 0X0404
        Front = ESAllEnum.Front,
        //
        // 概要:
        //     Original was GL_Back = 0X0405
        Back = ESAllEnum.Back,
        //
        // 概要:
        //     Original was GL_FRONT_AND_BACK = 0x0408
        FrontAndBack = ESAllEnum.FrontAndBack,
    }

}
