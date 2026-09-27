using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Not used directly.
    public enum FramebufferErrorCode
    {
        //
        // 概要:
        //     Original was GL_FramebufferComplete = 0X8cd5
        FramebufferComplete = 36053,
        //
        // 概要:
        //     Original was GL_FramebufferIncompleteAttachment = 0X8cd6
        FramebufferIncompleteAttachment = 36054,
        //
        // 概要:
        //     Original was GL_FramebufferIncompleteMissingAttachment = 0X8cd7
        FramebufferIncompleteMissingAttachment = 36055,
        //
        // 概要:
        //     Original was GL_FramebufferIncompleteDimensions = 0X8cd9
        FramebufferIncompleteDimensions = 36057,
        //
        // 概要:
        //     Original was GL_FramebufferUnsupported = 0X8cdd
        FramebufferUnsupported = 36061
    }

}
