using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.StencilFunc, GL.StencilFuncSeparate
    public enum StencilFunction
    {
        //
        // 概要:
        //     Original was GL_Never = 0X0200
        Never = 0x200,
        //
        // 概要:
        //     Original was GL_Less = 0X0201
        Less = ESAllEnum.Less,
        //
        // 概要:
        //     Original was GL_Equal = 0X0202
        Equal = ESAllEnum.Equal,
        //
        // 概要:
        //     Original was GL_Lequal = 0X0203
        Lequal = ESAllEnum.Lequal,
        //
        // 概要:
        //     Original was GL_Greater = 0X0204
        Greater = ESAllEnum.Greater,
        //
        // 概要:
        //     Original was GL_Notequal = 0X0205
        Notequal = ESAllEnum.Notequal,
        //
        // 概要:
        //     Original was GL_Gequal = 0X0206
        Gequal = ESAllEnum.Gequal,
        //
        // 概要:
        //     Original was GL_Always = 0X0207
        Always = ESAllEnum.Always,
    }

}
