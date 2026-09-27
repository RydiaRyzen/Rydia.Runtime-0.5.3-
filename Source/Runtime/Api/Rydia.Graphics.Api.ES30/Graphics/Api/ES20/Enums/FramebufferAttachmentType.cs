using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Types of objects attached to a <see cref="Framebuffer"/>
	/// </summary>
	public enum FramebufferAttachmentType
	{
		/// <summary>
		/// No renderbuffer or texture is attached.
		/// </summary>
		None = (int)ESAllEnum.None,

		/// <summary>
		/// The attachment is a renderbuffer
		/// </summary>
		Renderbuffer = (int)ESAllEnum.Renderbuffer,

		/// <summary>
		/// The attachment is a texture
		/// </summary>
		Texture = (int)ESAllEnum.Texture
	}

}
