using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Texture minification filters
	/// </summary>
	//     Not used directly.
	public enum TextureMinFilter
	{
		/// <summary>
		/// Returns the value of the texture element that is nearest (in Manhattan distance) to the center of the pixel being textured.
		/// This is usually the fastest method.
		/// </summary>
		//     Original was GL_Nearest = 0X2600
		Nearest = 9728,
		/// <summary>
		/// Returns the weighted average of the four texture elements that are closest to the center of the pixel being textured.
		/// This usually provides better quality than Nearest.
		/// </summary>
		//     Original was GL_Linear = 0X2601
		Linear = 9729,
		/// <summary>
		/// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the Nearest criterion (the texture element nearest to the center of the pixel) to produce a texture value.
		/// This is usually the fastest method when using mipmapping.
		/// </summary>
		//     Original was GL_NEAREST_MIPMAP_NEAREST = 0x2700
		NearestMipmapNearest = 9984,
		/// <summary>
		/// Chooses the mipmap that most closely matches the size of the pixel being textured and uses the Linear criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value.
		/// </summary>
		//     Original was GL_LINEAR_MIPMAP_NEAREST = 0x2701
		LinearMipmapNearest = 9985,
		/// <summary>
		/// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the Nearest criterion (the texture element nearest to the center of the pixel) to produce a texture value from each mipmap.
		/// The final texture value is a weighted average of those two values.
		/// </summary>
		//     Original was GL_NEAREST_MIPMAP_LINEAR = 0x2702
		NearestMipmapLinear = 9986,
		/// <summary>
		/// Chooses the two mipmaps that most closely match the size of the pixel being textured and uses the Linear criterion (a weighted average of the four texture elements that are closest to the center of the pixel) to produce a texture value from each mipmap.
		/// The final texture value is a weighted average of those two values.
		/// This is usually the slowest (but highest quality) method when using mipmapping.
		/// </summary>
		//     Original was GL_LINEAR_MIPMAP_LINEAR = 0x2703
		LinearMipmapLinear = 9987,
	}

}
