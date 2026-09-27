using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.MapBufferRange
    [Flags]
    public enum BufferAccessMask
    {
        //
        // 概要:
        //     Original was GL_MAP_READ_BIT = 0x0001
        MapReadBit = 1,
        //
        // 概要:
        //     Original was GL_MAP_WRITE_BIT = 0x0002
        MapWriteBit = 2,
        //
        // 概要:
        //     Original was GL_MAP_INVALIDATE_RANGE_BIT = 0x0004
        MapInvalidateRangeBit = 4,
        //
        // 概要:
        //     Original was GL_MAP_INVALIDATE_BUFFER_BIT = 0x0008
        MapInvalidateBufferBit = 8,
        //
        // 概要:
        //     Original was GL_MAP_FLUSH_EXPLICIT_BIT = 0x0010
        MapFlushExplicitBit = 0x10,
        //
        // 概要:
        //     Original was GL_MAP_UNSYNCHRONIZED_BIT = 0x0020
        MapUnsynchronizedBit = 0x20
    }
}
