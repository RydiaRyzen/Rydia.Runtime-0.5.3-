using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Hints how a <see cref="Data{T}(T[], IntPtr, Usage)"/>'s data is accessed.
    /// </summary>
    public enum BufferUsageHint : int
    {
        /// <summary>
        /// The data stored in a buffer will be modified once and used at most a few times.
        /// </summary>
        StreamDraw = ((int)0X88e0),
        /// <summary>
        /// The data stored in a buffer will be modified once and used many times.
        /// </summary>
        StaticDraw = ((int)0X88e4),
        /// <summary>
        /// The data stored in a buffer will be modified repeatedly and used many times.
        /// </summary>
        DynamicDraw = ((int)0X88e8),
    }

}
