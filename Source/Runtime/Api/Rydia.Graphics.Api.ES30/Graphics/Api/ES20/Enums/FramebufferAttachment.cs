using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Attachment points of a <see cref="Framebuffer"/>
    /// </summary>
    public enum FramebufferAttachment
    {
        /// <summary>
        /// Attachment point for a color buffer
        /// </summary>
        Color = (int)ESAllEnum.ColorAttachment0,

        /// <summary>
        /// Attachment point for a depth buffer
        /// </summary>
        Depth = (int)ESAllEnum.DepthAttachment,

        /// <summary>
        /// Attachment point for a stencil buffer
        /// </summary>
        Stencil = (int)ESAllEnum.StencilAttachment
    }

}
