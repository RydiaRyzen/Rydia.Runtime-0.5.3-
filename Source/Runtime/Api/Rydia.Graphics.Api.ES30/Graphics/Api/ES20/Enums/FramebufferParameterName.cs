using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    //
    // 概要:
    //     Used in GL.GetFramebufferAttachmentParameter
    public enum FramebufferParameterName
    {
        //
        // 概要:
        //     Original was GL_FramebufferAttachmentObjectType = 0X8cd0
        FramebufferAttachmentObjectType = ESAllEnum.FramebufferAttachmentObjectType,
        //
        // 概要:
        //     Original was GL_FramebufferAttachmentObjectName = 0X8cd1
        FramebufferAttachmentObjectName = ESAllEnum.FramebufferAttachmentObjectName,
        //
        // 概要:
        //     Original was GL_FramebufferAttachmentTextureLevel = 0X8cd2
        FramebufferAttachmentTextureLevel = ESAllEnum.FramebufferAttachmentTextureLevel,
        //
        // 概要:
        //     Original was GL_FramebufferAttachmentTextureCubeMapFace = 0X8cd3
        FramebufferAttachmentTextureCubeMapFace = ESAllEnum.FramebufferAttachmentTextureCubeMapFace,
    }

}
