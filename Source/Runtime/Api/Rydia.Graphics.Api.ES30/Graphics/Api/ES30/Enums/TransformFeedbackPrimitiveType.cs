using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.BeginTransformFeedback
    public enum TransformFeedbackPrimitiveType
    {
        //
        // 概要:
        //     Original was GL_POINTS = 0X0000
        Points = 0,
        //
        // 概要:
        //     Original was GL_LINES = 0X0001
        Lines = 1,
        //
        // 概要:
        //     Original was GL_TRIANGLES = 0X0004
        Triangles = 4
    }
}
