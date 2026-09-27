using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Used in GL.Apple.GetSync, GL.GetSync
    public enum SyncParameterName
    {
        //
        // 概要:
        //     Original was GL_OBJECT_TYPE = 0x9112
        ObjectType = 37138,
        //
        // 概要:
        //     Original was GL_OBJECT_TYPE_APPLE = 0x9112
        ObjectTypeApple = 37138,
        //
        // 概要:
        //     Original was GL_SYNC_CONDITION = 0x9113
        SyncCondition = 37139,
        //
        // 概要:
        //     Original was GL_SYNC_CONDITION_APPLE = 0x9113
        SyncConditionApple = 37139,
        //
        // 概要:
        //     Original was GL_SYNC_STATUS = 0x9114
        SyncStatus = 37140,
        //
        // 概要:
        //     Original was GL_SYNC_STATUS_APPLE = 0x9114
        SyncStatusApple = 37140,
        //
        // 概要:
        //     Original was GL_SYNC_FLAGS = 0x9115
        SyncFlags = 37141,
        //
        // 概要:
        //     Original was GL_SYNC_FLAGS_APPLE = 0x9115
        SyncFlagsApple = 37141
    }
}
