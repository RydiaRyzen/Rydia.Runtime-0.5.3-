using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Graphics.Api.ES30
{
    //
    // 概要:
    //     Not used directly.
    public enum WaitSyncStatus
    {
        //
        // 概要:
        //     Original was GL_ALREADY_SIGNALED = 0x911A
        AlreadySignaled = 37146,
        //
        // 概要:
        //     Original was GL_ALREADY_SIGNALED_APPLE = 0x911A
        AlreadySignaledApple = 37146,
        //
        // 概要:
        //     Original was GL_TIMEOUT_EXPIRED = 0x911B
        TimeoutExpired = 37147,
        //
        // 概要:
        //     Original was GL_TIMEOUT_EXPIRED_APPLE = 0x911B
        TimeoutExpiredApple = 37147,
        //
        // 概要:
        //     Original was GL_CONDITION_SATISFIED = 0x911C
        ConditionSatisfied = 37148,
        //
        // 概要:
        //     Original was GL_CONDITION_SATISFIED_APPLE = 0x911C
        ConditionSatisfiedApple = 37148,
        //
        // 概要:
        //     Original was GL_WAIT_FAILED = 0x911D
        WaitFailed = 37149,
        //
        // 概要:
        //     Original was GL_WAIT_FAILED_APPLE = 0x911D
        WaitFailedApple = 37149
    }
}
