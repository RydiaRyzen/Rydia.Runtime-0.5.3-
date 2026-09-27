using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.StencilOp, GL.StencilOpSeparate
    public enum StencilOp
    {
        //
        // 概要:
        //     Original was GL_Zero = 0X0000
        Zero = 0,
        //
        // 概要:
        //     Original was GL_Invert = 0X150a
        Invert = 5386,
        //
        // 概要:
        //     Original was GL_Keep = 0X1e00
        Keep = 7680,
        //
        // 概要:
        //     Original was GL_Replace = 0X1e01
        Replace = 7681,
        //
        // 概要:
        //     Original was GL_Incr = 0X1e02
        Increment = 7682,
        //
        // 概要:
        //     Original was GL_Decr = 0X1e03
        Decrement = 7683,
        //
        // 概要:
        //     Original was GL_IncrWrap = 0X8507
        IncrWrap = 34055,
        //
        // 概要:
        //     Original was GL_DecrWrap = 0X8508
        DecrWrap = 34056
    }

}
