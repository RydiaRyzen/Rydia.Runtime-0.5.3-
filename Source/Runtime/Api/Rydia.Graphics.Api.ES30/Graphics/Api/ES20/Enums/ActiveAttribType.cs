using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.GetActiveAttrib
    public enum ActiveAttribType
    {
        //
        // 概要:
        //     Original was GL_Float = 0X1406
        Float = ESAllEnum.Float,
        //
        // 概要:
        //     Original was GL_FloatVec2 = 0X8b50
        FloatVec2 = ESAllEnum.FloatVec2,
        //
        // 概要:
        //     Original was GL_FloatVec3 = 0X8b51
        FloatVec3 = ESAllEnum.FloatVec3,
        //
        // 概要:
        //     Original was GL_FloatVec4 = 0X8b52
        FloatVec4 = ESAllEnum.FloatVec4,
        //
        // 概要:
        //     Original was GL_FloatMat2 = 0X8b5a
        FloatMat2 = ESAllEnum.FloatMat2,
        //
        // 概要:
        //     Original was GL_FloatMat3 = 0X8b5b
        FloatMat3 = ESAllEnum.FloatMat3,
        //
        // 概要:
        //     Original was GL_FloatMat4 = 0X8b5c
        FloatMat4 = ESAllEnum.FloatMat4,
    }

}
