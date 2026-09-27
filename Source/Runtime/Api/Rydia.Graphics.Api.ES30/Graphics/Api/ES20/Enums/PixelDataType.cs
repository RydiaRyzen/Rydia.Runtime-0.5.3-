using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

	/// <summary>
	/// Supported pixel data types
	/// </summary>
	public enum PixelDataType
	{
		/// <summary>
		/// Each byte is interpreted as one color component (red, green, blue or alpha).
		/// When converted to floating point, the value is divided by 255.
		/// </summary>
		UnsignedByte = ((int)0X1401),

		/// <summary>
		/// A single 16-bit integer contains 5 bits for the red component, 6 bits for the green component and 5 bits for the blue component.
		/// When converted to floating point, the red and blue components are divided by 31 and the green component is divided by 63.
		/// </summary>
		UnsignedShort565 = ((int)0x8363),

		/// <summary>
		/// A single 16-bit integer contains all components, with 4 bits for each component.
		/// When converted to floating point, every component is divided by 15.
		/// </summary>
		UnsignedShort4444 = ((int)0x8033),

		/// <summary>
		/// A single 16-bit integer contains all components, with 5 bits for the red, green and blue components, and 1 bit for the alpha component.
		/// When converted to floating point, the red, green and blue components are divided by 31 and the alpha component is used as-is.
		/// </summary>
		UnsignedShort5551 = ((int)0x8034),
	}

}
