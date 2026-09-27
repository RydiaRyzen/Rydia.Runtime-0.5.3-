using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.GetBufferPointer, GL.Oes.GetBufferPointer
    public enum BufferPointer
    {
        //
        // 概要:
        //     Original was GL_BUFFER_MAP_POINTER = 0x88BD
        BufferMapPointer = 35005,
        //
        // 概要:
        //     Original was GL_BUFFER_MAP_POINTER_OES = 0x88BD
        BufferMapPointerOes = 35005
    }
}
