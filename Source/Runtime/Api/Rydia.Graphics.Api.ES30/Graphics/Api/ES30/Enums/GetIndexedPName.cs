using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetInteger64, GL.GetInteger and 1 other function
    public enum GetIndexedPName
    {
        //
        // 概要:
        //     Original was GL_DRAW_BUFFER_EXT = 0x0C01
        DrawBufferExt = 3073,
        //
        // 概要:
        //     Original was GL_READ_BUFFER_EXT = 0x0C02
        ReadBufferExt = 3074,
        //
        // 概要:
        //     Original was GL_UNIFORM_BUFFER_BINDING = 0x8A28
        UniformBufferBinding = 35368,
        //
        // 概要:
        //     Original was GL_UNIFORM_BUFFER_START = 0x8A29
        UniformBufferStart = 35369,
        //
        // 概要:
        //     Original was GL_UNIFORM_BUFFER_SIZE = 0x8A2A
        UniformBufferSize = 35370,
        //
        // 概要:
        //     Original was GL_TRANSFORM_FEEDBACK_BUFFER_START = 0x8C84
        TransformFeedbackBufferStart = 35972,
        //
        // 概要:
        //     Original was GL_TRANSFORM_FEEDBACK_BUFFER_SIZE = 0x8C85
        TransformFeedbackBufferSize = 35973,
        //
        // 概要:
        //     Original was GL_TRANSFORM_FEEDBACK_BUFFER_BINDING = 0x8C8F
        TransformFeedbackBufferBinding = 35983
    }
}
