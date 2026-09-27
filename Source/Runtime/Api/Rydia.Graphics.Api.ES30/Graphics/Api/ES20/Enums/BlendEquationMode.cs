using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{
    //
    // 概要:
    //     Used in GL.BlendEquation, GL.BlendEquationSeparate and 2 other functions
    public enum BlendEquationMode
    {
        //
        // 概要:
        //     Original was GL_FuncAdd = 0X8006
        FuncAdd = ESAllEnum.FuncAdd,
        //
        // 概要:
        //     Original was GL_FuncSubtract = 0X800a
        FuncSubtract = ESAllEnum.FuncSubtract,
        //
        // 概要:
        //     Original was GL_FuncReverseSubtract = 0X800b
        FuncReverseSubtract = ESAllEnum.FuncReverseSubtract,
    }

}
