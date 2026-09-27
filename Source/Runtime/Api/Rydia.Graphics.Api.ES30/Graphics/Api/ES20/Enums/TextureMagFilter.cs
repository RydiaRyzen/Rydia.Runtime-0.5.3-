using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Texture magnification filters
	/// </summary>
	//     Not used directly.
	public enum TextureMagFilter
	{
		/// <summary>
		/// Returns the value of the texture element that is nearest (in Manhattan distance) to the center of the pixel being textured.
		/// This is usually the fastest method.
		/// </summary>
		//     Original was GL_Nearest = 0X2600
		Nearest = 9728,
		/// <summary>
		/// Returns the weighted average of the four texture elements that are closest to the center of the pixel being textured.
		/// This usually provides better quality.
		/// </summary>
		//     Original was GL_Linear = 0X2601
		Linear = 9729,
	}

}
