using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// Supported pixel formats
    /// </summary>
    public enum PixelFormat
    {
        /// <summary>
        /// Each element is a single alpha component.
        /// The GL converts it to floating point and assembles it into an RGBA element by attaching 0 for red, green, and blue.
        /// Each component is then clamped to the range [0,1].
        /// </summary>
        Alpha = ((int)0x1906),

        /// <summary>
        /// Each element is an RGB triple.
        /// The GL converts it to floating point and assembles it into an RGBA element by attaching 1 for alpha.
        /// Each component is then clamped to the range [0,1].
        /// </summary>
        Rgb = ((int)0x1907),

        /// <summary>
        /// Each element contains all four components.
        /// The GL converts it to floating point, then each component is clamped to the range [0,1].
        /// </summary>
        Rgba = ((int)0x1908),

        /// <summary>
        /// Each element is a single luminance value.
        /// The GL converts it to floating point, then assembles it into an RGBA element by replicating the luminance value three times for red, green, and blue and attaching 1 for alpha.
        /// Each component is then clamped to the range [0,1].
        /// </summary>
        Luminance = ((int)0x1909),

        /// <summary>
        /// Each element is a luminance/alpha pair.
        /// The GL converts it to floating point, then assembles it into an RGBA element by replicating the luminance value three times for red, green, and blue.
        /// Each component is then clamped to the range [0,1].
        /// </summary>
        LuminanceAlpha = ((int)0x190A),
    }

}
