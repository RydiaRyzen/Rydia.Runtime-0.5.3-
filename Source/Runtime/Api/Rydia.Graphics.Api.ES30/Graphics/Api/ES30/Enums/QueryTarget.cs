using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.BeginQuery, GL.EndQuery and 4 other functions
    public enum QueryTarget
    {
        //
        // 概要:
        //     Original was GL_TIME_ELAPSED_EXT = 0x88BF
        TimeElapsedExt = 35007,
        //
        // 概要:
        //     Original was GL_ANY_SAMPLES_PASSED = 0x8C2F
        AnySamplesPassed = 35887,
        //
        // 概要:
        //     Original was GL_ANY_SAMPLES_PASSED_EXT = 0x8C2F
        AnySamplesPassedExt = 35887,
        //
        // 概要:
        //     Original was GL_TRANSFORM_FEEDBACK_PRIMITIVES_WRITTEN = 0x8C88
        TransformFeedbackPrimitivesWritten = 35976,
        //
        // 概要:
        //     Original was GL_ANY_SAMPLES_PASSED_CONSERVATIVE = 0x8D6A
        AnySamplesPassedConservative = 36202,
        //
        // 概要:
        //     Original was GL_ANY_SAMPLES_PASSED_CONSERVATIVE_EXT = 0x8D6A
        AnySamplesPassedConservativeExt = 36202
    }
}
