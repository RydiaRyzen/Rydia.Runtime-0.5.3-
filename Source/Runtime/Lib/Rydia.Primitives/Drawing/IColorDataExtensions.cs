using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rydia.Drawing
{

    public static class IColorDataExtensions
    {

        public static Color4 ToColor4(this IColorData color)
        {
            return new Color4(color.ToColorRgba());
        }
        public static ColorHsva ToColorHsva(this IColorData color)
        {
            return ColorHsva.FromIntRgba(color.ToIntRgba());
        }

        public static ColorRgba ToColorRgba(this IColorData color)
        {
            return ColorRgba.FromIntRgba(color.ToIntRgba());
        }
    }

}
