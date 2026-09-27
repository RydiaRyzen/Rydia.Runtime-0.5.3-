using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.Apple.FenceSync, GL.FenceSync
    public enum SyncCondition
    {
        //
        // 概要:
        //     Original was GL_SYNC_GPU_COMMANDS_COMPLETE = 0x9117
        SyncGpuCommandsComplete = 37143,
        //
        // 概要:
        //     Original was GL_SYNC_GPU_COMMANDS_COMPLETE_APPLE = 0x9117
        SyncGpuCommandsCompleteApple = 37143
    }
}
