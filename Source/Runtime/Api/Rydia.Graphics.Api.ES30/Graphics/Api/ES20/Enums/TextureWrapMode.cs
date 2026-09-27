using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{


	/// <summary>
	/// Texture wrapping modes
	/// </summary>
	//     Not used directly.
	public enum TextureWrapMode
	{

		/// <summary>
		/// Repeats the texture.
		/// Causes the integer part of the texture coordinate to be ignored; 
		/// the GL uses only the fractional part, thereby creating a repeating pattern.
		/// </summary>
		//     Original was GL_REPEAT = 0x2901
		Repeat = 10497,
		/// <summary>
		/// Repeats and mirrors the texture.
		/// Causes the final texture coordinate (Dst) to be set to the fractional part of the original texture coordinate (Src) 
		/// if the integer part of Src is even; if the integer part of Src is odd, then Dst is set to 1 - Frac(Src), 
		/// where Frac(Src) represents the fractional part of Src.
		/// </summary>
		MirroredRepeat = 0x8370,
		/// <summary>
		/// Clamps the texture to its edges.
		/// Causes the texture coordinate to be clamped to the size of the texture in the direction of clamping.
		/// </summary>
		//     Original was GL_CLAMP_TO_EDGE = 0x812F
		ClampToEdge = 33071,
	}

}
