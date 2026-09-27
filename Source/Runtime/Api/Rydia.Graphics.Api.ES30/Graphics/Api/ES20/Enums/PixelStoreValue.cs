using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Allowed values for TGLPixelStorage mode, as used by <see cref="GL.PixelStore"/>.
    /// </summary>
    public enum PixelStoreValue
    {
        /// <summary>
        /// Pixel data is aligned on a byte boundary
        /// </summary>
        One = 1,

        /// <summary>
        /// Pixel data is aligned on 2-byte boundary
        /// </summary>
        Two = 2,

        /// <summary>
        /// Pixel data is aligned on 4-byte boundary
        /// </summary>
        Four = 4,

        /// <summary>
        /// Pixel data is aligned on 8-byte boundary
        /// </summary>
        Eight = 8
    }

}
