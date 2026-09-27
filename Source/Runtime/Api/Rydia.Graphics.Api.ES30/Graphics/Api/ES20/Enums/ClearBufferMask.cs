using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.Angle.BlitFramebuffer, GL.Clear and 1 other function
    [Flags]
    public enum ClearBufferMask
    {
        //
        // 概要:
        //     Original was GL_DEPTH_BUFFER_BIT = 0x00000100
        DepthBufferBit = 0x100,
        //
        // 概要:
        //     Original was GL_ACCUM_BUFFER_BIT = 0x00000200
        AccumBufferBit = 0x200,
        //
        // 概要:
        //     Original was GL_STENCIL_BUFFER_BIT = 0x00000400
        StencilBufferBit = 0x400,
        //
        // 概要:
        //     Original was GL_COLOR_BUFFER_BIT = 0x00004000
        ColorBufferBit = 0x4000,
        //
        // 概要:
        //     Original was GL_COVERAGE_BUFFER_BIT_NV = 0x00008000
        CoverageBufferBitNv = 0x8000
    }

}
