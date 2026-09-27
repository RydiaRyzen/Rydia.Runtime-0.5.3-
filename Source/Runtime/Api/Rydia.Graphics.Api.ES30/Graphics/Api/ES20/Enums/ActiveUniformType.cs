using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.GetActiveUniform
    public enum ActiveUniformType
    {
        //
        // 概要:
        //     Original was GL_Int = 0X1404
        Int = ESAllEnum.Int,
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
        //     Original was GL_IntVec2 = 0X8b53
        IntVec2 = ESAllEnum.IntVec2,
        //
        // 概要:
        //     Original was GL_IntVec3 = 0X8b54
        IntVec3 = ESAllEnum.IntVec3,
        //
        // 概要:
        //     Original was GL_IntVec4 = 0X8b55
        IntVec4 = ESAllEnum.IntVec4,
        //
        // 概要:
        //     Original was GL_Bool = 0X8b56
        Bool = ESAllEnum.Bool,
        //
        // 概要:
        //     Original was GL_BoolVec2 = 0X8b57
        BoolVec2 = ESAllEnum.BoolVec2,
        //
        // 概要:
        //     Original was GL_BoolVec3 = 0X8b58
        BoolVec3 = ESAllEnum.BoolVec3,
        //
        // 概要:
        //     Original was GL_BoolVec4 = 0X8b59
        BoolVec4 = ESAllEnum.BoolVec4,
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
        //
        // 概要:
        //     Original was GL_Sampler2D = 0X8b5e
        Sampler2D = ESAllEnum.Sampler2D,
        //
        // 概要:
        //     Original was GL_SamplerCube = 0X8b60
        SamplerCube = ESAllEnum.SamplerCube,

    }

}
