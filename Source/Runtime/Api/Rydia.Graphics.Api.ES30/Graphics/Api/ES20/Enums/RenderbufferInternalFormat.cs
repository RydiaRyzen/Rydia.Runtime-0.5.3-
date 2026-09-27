using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Specifies the color-renderable, depth-renderable, or stencil-renderable format of a <see cref="Renderbuffer"/>.
    /// Used in GL.Angle.RenderbufferStorageMultisample, GL.Apple.RenderbufferStorageMultisample
    /// and 4 other functions
    /// </summary>
    public enum RenderbufferInternalFormat
    {
        /// <summary>
        /// Color format with alpha support, using 4 bits per component (red, green, blue and alpha)
        /// </summary>
        //     Original was GL_Rgba4 = 0X8056
        Rgba4 = 32854,
        /// <summary>
        /// Color format without alpha support, using 5 bits for the red and blue components and 6 bits for the green component.
        /// </summary>
        //     Original was GL_Rgb5A1 = 0X8057
        Rgb5A1 = 32855,
        /// <summary>
        /// Color format with alpha support, using 5 bits for each color component (red, green and blue) and 1 bit for the alpha component.
        /// </summary>
        //     Original was GL_DepthComponent16 = 0X81a5
        DepthComponent16 = 33189,
        /// <summary>
        /// 16-bit depth format
        /// </summary>
        //     Original was GL_StencilIndex8 = 0X8d48
        StencilIndex8 = 36168,
        /// <summary>
        /// 8-bit stencil format
        /// </summary>
        //     Original was GL_Rgb565 = 0X8d62
        Rgb565 = 36194
    }

}
