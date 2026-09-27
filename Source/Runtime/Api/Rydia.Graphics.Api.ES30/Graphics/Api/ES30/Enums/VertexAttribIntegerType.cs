using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.VertexAttribIPointer
    public enum VertexAttribIntegerType
    {
        //
        // 概要:
        //     Original was GL_BYTE = 0X1400
        Byte = 5120,
        //
        // 概要:
        //     Original was GL_UNSIGNED_BYTE = 0x1401
        UnsignedByte,
        //
        // 概要:
        //     Original was GL_SHORT = 0X1402
        Short,
        //
        // 概要:
        //     Original was GL_UNSIGNED_SHORT = 0x1403
        UnsignedShort,
        //
        // 概要:
        //     Original was GL_INT = 0X1404
        Int,
        //
        // 概要:
        //     Original was GL_UNSIGNED_INT = 0x1405
        UnsignedInt
    }
}
