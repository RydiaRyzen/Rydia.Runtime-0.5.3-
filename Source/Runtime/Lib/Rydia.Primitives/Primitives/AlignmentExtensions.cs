using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia
{

    /// <summary>
    /// <see cref="Alignment"/> に関する拡張メソッドを提供するクラスです。
    /// </summary>
    public static class AlignmentExtensions
    {

        /// <summary>
        /// 指定されたサイズと位置に対して、指定された配置を適用します。
        /// </summary>
        /// <param name="align">適用する配置です。</param>
        /// <param name="vec">配置を適用する位置です。</param>
        /// <param name="size">配置対象のサイズです。</param>
        public static void ApplyTo(this Alignment align, ref Vector2 vec, ref Vector2 size)
        {
            switch (align)
            {
                case Alignment.Bottom:
                    vec.X -= size.X * 0.5f;
                    vec.Y -= size.Y;
                    break;
                case Alignment.BottomLeft:
                    vec.Y -= size.Y;
                    break;
                case Alignment.BottomRight:
                    vec.X -= size.X;
                    vec.Y -= size.Y;
                    break;
                case Alignment.Center:
                    vec.X -= size.X * 0.5f;
                    vec.Y -= size.Y * 0.5f;
                    break;
                case Alignment.Left:
                    vec.Y -= size.Y * 0.5f;
                    break;
                case Alignment.Right:
                    vec.X -= size.X;
                    vec.Y -= size.Y * 0.5f;
                    break;
                case Alignment.Top:
                    vec.X -= size.X * 0.5f;
                    break;
                case Alignment.TopRight:
                    vec.X -= size.X;
                    break;
                default:
                case Alignment.TopLeft:
                    break;
            }
        }

        /// <summary>
        /// 指定されたサイズと位置に対して、指定された配置を適用します。
        /// </summary>
        /// <param name="align">適用する配置です。</param>
        /// <param name="vec">配置を適用する位置です。</param>
        /// <param name="size">配置対象のサイズです。</param>
        public static void ApplyTo(this Alignment align, ref Vector2 vec, Vector2 size)
        {
            ApplyTo(align, ref vec, ref size);
        }

        /// <summary>
        /// 指定されたサイズと位置に対して、指定された配置を適用した位置を返します。
        /// </summary>
        /// <param name="align">適用する配置です。</param>
        /// <param name="vec">配置を適用する位置です。</param>
        /// <param name="size">配置対象のサイズです。</param>
        /// <returns>指定された配置を適用した位置です。</returns>
        public static Vector2 ApplyTo(this Alignment align, Vector2 vec, Vector2 size)
        {
            ApplyTo(align, ref vec, ref size);
            return vec;
        }

        /// <summary>
        /// 指定された位置とサイズに対して、指定された配置を適用します。
        /// </summary>
        /// <param name="align">適用する配置です。</param>
        /// <param name="x">配置を適用する X 座標です。</param>
        /// <param name="y">配置を適用する Y 座標です。</param>
        /// <param name="width">配置対象の幅です。</param>
        /// <param name="height">配置対象の高さです。</param>
        public static void ApplyTo(this Alignment align, ref float x, ref float y, float width, float height)
        {
            Vector2 vec;
            Vector2 size;
            vec.X = x;
            vec.Y = y;
            size.X = width;
            size.Y = height;
            ApplyTo(align, ref vec, ref size);
            x = vec.X;
            y = vec.Y;
        }

        /// <summary>
        /// 指定された位置とサイズに対して、指定された配置を適用した位置を返します。
        /// </summary>
        /// <param name="align">適用する配置です。</param>
        /// <param name="x">配置を適用する X 座標です。</param>
        /// <param name="y">配置を適用する Y 座標です。</param>
        /// <param name="width">配置対象の幅です。</param>
        /// <param name="height">配置対象の高さです。</param>
        /// <returns>指定された配置を適用した位置です。</returns>
        public static Vector2 ApplyTo(this Alignment align, float x, float y, float width, float height)
        {
            Vector2 vec;
            Vector2 size;
            vec.X = x;
            vec.Y = y;
            size.X = width;
            size.Y = height;
            ApplyTo(align, ref vec, ref size);
            return vec;
        }

    }
}
