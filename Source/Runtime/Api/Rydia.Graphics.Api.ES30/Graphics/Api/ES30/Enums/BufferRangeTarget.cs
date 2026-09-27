using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.BindBufferBase, GL.BindBufferRange
    public enum BufferRangeTarget
    {
        //
        // 概要:
        //     Original was GL_UNIFORM_BUFFER = 0x8A11
        UniformBuffer = 35345,
        //
        // 概要:
        //     Original was GL_TRANSFORM_FEEDBACK_BUFFER = 0x8C8E
        TransformFeedbackBuffer = 35982
    }
}
