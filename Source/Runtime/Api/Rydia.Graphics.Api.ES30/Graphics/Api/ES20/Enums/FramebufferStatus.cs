using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Completeness status of a <see cref="Framebuffer"/>.
	/// </summary>
	public enum FramebufferStatus
	{
		/// <summary>
		/// The framebuffer is complete
		/// Original was GL_FRAMEBUFFER_COMPLETE = 0x8CD5
		/// </summary>
		Complete = 36053,

		/// <summary>
		/// Original was GL_FRAMEBUFFER_INCOMPLETE_ATTACHMENT = 0x8CD6
		/// Not all framebuffer attachment points are framebuffer attachment complete.
		/// This means that at least one attachment point with a renderbuffer or texture attached has its attached object no longer in existence 
		/// or has an attached image with a width or height of zero, or the color attachment point has a non-color-renderable image attached, 
		/// or the depth attachment point has a non-depth-renderable image attached, or the stencil attachment point has a non-stencil-renderable image attached.
		/// </summary>
		IncompleteAttachment = 36054,

		/// <summary>
		/// Original was GL_FRAMEBUFFER_INCOMPLETE_DIMENSIONS = 0x8CD9
		/// Not all attached images have the same width and height.
		/// </summary>
		IncompleteDimensions = 36057,

		/// <summary>
		/// Original was GL_FRAMEBUFFER_INCOMPLETE_MISSING_ATTACHMENT = 0x8CD7
		/// No images are attached to the framebuffer.
		/// </summary>
		MissingAttachment = 36055,

		/// <summary>
		/// Original was GL_FRAMEBUFFER_UNSUPPORTED = 0x8CDD
		/// The combination of internal formats of the attached images violates an implementation-dependent set of restrictions.
		/// </summary>
		Unsupported = 36061,
	}

}
