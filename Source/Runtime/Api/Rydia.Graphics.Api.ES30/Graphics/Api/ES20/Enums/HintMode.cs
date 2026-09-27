using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Mipmap hinting options, as used by <see cref="Texture.MipmapHint"/>
    /// </summary>
    //     Used in GL.Hint
    public enum HintMode
    {
        /// <summary>
        /// No preference.
        /// </summary>
        //     Original was GL_DONT_CARE = 0x1100
        DontCare = ESAllEnum.DontCare,
        /// <summary>
        /// The most efficient option should be chosen.
        /// </summary>
        //     Original was GL_Fastest = 0X1101
        Fastest = ESAllEnum.Fastest,
        /// <summary>
        /// The most correct, or highest quality, option should be chosen.
        /// </summary>
        //     Original was GL_Nicest = 0X1102
        Nicest = ESAllEnum.Nicest,
    }

}
