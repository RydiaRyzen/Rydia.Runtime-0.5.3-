using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetActiveUniformBlock
    public enum ActiveUniformBlockParameter
    {
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_BINDING = 0x8A3F
        UniformBlockBinding = 35391,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_DATA_SIZE = 0x8A40
        UniformBlockDataSize = 35392,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_NAME_LENGTH = 0x8A41
        UniformBlockNameLength = 35393,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_ACTIVE_UNIFORMS = 0x8A42
        UniformBlockActiveUniforms = 35394,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_ACTIVE_UNIFORM_INDICES = 0x8A43
        UniformBlockActiveUniformIndices = 35395,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_REFERENCED_BY_VERTEX_SHADER = 0x8A44
        UniformBlockReferencedByVertexShader = 35396,
        //
        // 概要:
        //     Original was GL_UNIFORM_BLOCK_REFERENCED_BY_FRAGMENT_SHADER = 0x8A46
        UniformBlockReferencedByFragmentShader = 35398
    }
}
