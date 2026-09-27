using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetActiveUniforms
    public enum ActiveUniformParameter
    {
        //
        // 概要:
        //     Original was GL_UNIFORM_TYPE = 0x8A37
        UniformType = 35383,
        //
        // 概要:
        //     Original was GL_UNIFORM_SIZE = 0x8A38
        UniformSize,
        //
        // 概要:
        //     Original was GL_UNIFORM_NAME_LENGTH = 0x8A39
        UniformNameLength,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_INDEX = 0x8A3A
        UniformBlockIndex,
        //
        // 概要:
        //     Original was GL_UNIFORM_OFFSET = 0x8A3B
        UniformOffset,
        //
        // 概要:
        //     Original was GL_UNIFORM_ARRAY_STRIDE = 0x8A3C
        UniformArrayStride,
        //
        // 概要:
        //     Original was GL_UNIFORM_MATRIX_STRIDE = 0x8A3D
        UniformMatrixStride,
        //
        // 概要:
        //     Original was GL_UNIFORM_IS_ROW_MAJOR = 0x8A3E
        UniformIsRowMajor
    }
}
