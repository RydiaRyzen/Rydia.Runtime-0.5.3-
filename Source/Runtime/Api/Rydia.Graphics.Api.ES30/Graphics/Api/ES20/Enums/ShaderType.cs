using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// The type of a <see cref="Shader"/>
    /// </summary>
    //     Used in GL.CreateShader, GL.GetShaderPrecisionFormat
    public enum ShaderType
    {

        /// <summary>
        /// A fragment shader
        /// </summary>
        //     Original was GL_FragmentShader = 0X8b30
        FragmentShader = ESAllEnum.FragmentShader,
        /// <summary>
        /// A vertex shader
        /// </summary>
        //     Original was GL_VertexShader = 0X8b31
        VertexShader = ESAllEnum.VertexShader,
    }

}
