using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Rydia.Drawing
{

    /// <summary>
    /// <see cref="IColorData"/> に関する拡張メソッドを提供するクラスです。
    /// </summary>
    public static class IColorDataExtensions
    {

        /// <summary>
        /// 色情報を <see cref="Color4"/> に変換します。
        /// </summary>
        /// <param name="color">変換する色情報です。</param>
        /// <returns><see cref="Color4"/> に変換された色です。</returns>
        public static Color4 ToColor4(this IColorData color)
        {
            return new Color4(color.ToColorRgba());
        }

        /// <summary>
        /// 色情報を <see cref="ColorHsva"/> に変換します。
        /// </summary>
        /// <param name="color">変換する色情報です。</param>
        /// <returns><see cref="ColorHsva"/> に変換された色です。</returns>
        public static ColorHsva ToColorHsva(this IColorData color)
        {
            return ColorHsva.FromIntRgba(color.ToIntRgba());
        }

        /// <summary>
        /// 色情報を <see cref="ColorRgba"/> に変換します。
        /// </summary>
        /// <param name="color">変換する色情報です。</param>
        /// <returns><see cref="ColorRgba"/> に変換された色です。</returns>
        public static ColorRgba ToColorRgba(this IColorData color)
        {
            return ColorRgba.FromIntRgba(color.ToIntRgba());
        }
    }

}
