using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{
    //
    // 概要:
    //     Used in GL.DepthFunc
    public enum DepthFunction
    {
        //
        // 概要:
        //     Original was GL_Never = 0X0200
        Never = 0x200,
        //
        // 概要:
        //     Original was GL_Less = 0X0201
        Less = 0x201,
        //
        // 概要:
        //     Original was GL_Equal = 0X0202
        Equal = 0x202,
        //
        // 概要:
        //     Original was GL_Lequal = 0X0203
        Lequal = 0x203,
        //
        // 概要:
        //     Original was GL_Greater = 0X0204
        Greater = 0x204,
        //
        // 概要:
        //     Original was GL_Notequal = 0X0205
        Notequal = 0x205,
        //
        // 概要:
        //     Original was GL_Gequal = 0X0206
        Gequal = 0x206,
        //
        // 概要:
        //     Original was GL_Always = 0X0207
        Always = 0x207,
    }
}
