using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Supported data types for vertex attributes
    /// </summary>
    public enum VertexAttribPointerType
    {
        /// <summary>
        /// 8-bit signed integer.
        /// Corresponds to C#'s sbyte type.
        /// </summary>
        Byte = ESAllEnum.Byte,

        /// <summary>
        /// 8-bit unsigned integer.
        /// Corresponds to C#'s byte type.
        /// </summary>
        UnsignedByte = 0X1401,

        /// <summary>
        /// 16-bit signed integer.
        /// Corresponds to C#'s short type.
        /// </summary>
        Short = ((int)0X1402),

        /// <summary>
        /// 16-bit unsigned integer.
        /// Corresponds to C#'s ushort type.
        /// </summary>
        UnsignedShort = ((int)0X1403),

        /// <summary>
        /// 32-bit floating-point value.
        /// Corresponds to C#'s float type.
        /// </summary>
        Float = ((int)0X1406),

        /// <summary>
        /// Fixed type
        /// </summary>
        Fixed = ((int)0X140c),
    }

}
