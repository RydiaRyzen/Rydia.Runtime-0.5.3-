using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.ClearBuffer
    public enum ClearBuffer
    {
        //
        // 概要:
        //     Original was GL_COLOR = 0x1800
        Color = 6144,
        //
        // 概要:
        //     Original was GL_DEPTH = 0x1801
        Depth,
        //
        // 概要:
        //     Original was GL_STENCIL = 0x1802
        Stencil
    }
}
