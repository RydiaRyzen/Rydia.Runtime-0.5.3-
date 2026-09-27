using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.Apple.ClientWaitSync, GL.ClientWaitSync
    public enum ClientWaitSyncFlags
    {
        //
        // 概要:
        //     Original was GL_NONE = 0
        None = 0,
        //
        // 概要:
        //     Original was GL_SYNC_FLUSH_COMMANDS_BIT = 0x00000001
        SyncFlushCommandsBit = 1,
        //
        // 概要:
        //     Original was GL_SYNC_FLUSH_COMMANDS_BIT_APPLE = 0x00000001
        SyncFlushCommandsBitApple = 1
    }
}
