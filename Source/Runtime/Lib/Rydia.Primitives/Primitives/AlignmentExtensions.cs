using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia
{

    /// <summary>
    /// <see cref="Alignment"/>に関する拡張メソッドを定義するクラスです
    /// </summary>
    public static class AlignmentExtensions
    {

        /// <summary>
        /// Applies the alignment to the specified vector.
        /// </summary>
        /// <param name="align"></param>
        /// <param name="vec"></param>
        /// <param name="size"></param>
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
        /// Applies the alignment to the specified vector.
        /// </summary>
        /// <param name="align"></param>
        /// <param name="vec"></param>
        /// <param name="size"></param>
        public static void ApplyTo(this Alignment align, ref Vector2 vec, Vector2 size)
        {
            ApplyTo(align, ref vec, ref size);
        }
        /// <summary>
        /// Applies the alignment to the specified vector.
        /// </summary>
        /// <param name="align"></param>
        /// <param name="vec"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Vector2 ApplyTo(this Alignment align, Vector2 vec, Vector2 size)
        {
            ApplyTo(align, ref vec, ref size);
            return vec;
        }
        /// <summary>
        /// Applies the alignment to the specified vector.
        /// </summary>
        /// <param name="align"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
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
        /// Applies the alignment to the specified vector.
        /// </summary>
        /// <param name="align"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
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
