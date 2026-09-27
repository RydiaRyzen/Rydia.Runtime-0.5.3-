using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics;
using Rydia.Drawing;

namespace Rydia.Runtime
{
    public static class GLES20BindingsExtensions
    {

        public static OpenTK.Graphics.Color4 TK(this IColorData color)
        {
            var temp = new Rydia.Drawing.Color4(color.ToColorRgba());
            return new OpenTK.Graphics.Color4(temp.Rf, temp.Gf, temp.Bf, temp.Af);
        }

        public static OpenTK.Matrix2 TK(this Rydia.Matrix2 matrix)
        {
            return new OpenTK.Matrix2(
                matrix.M11, matrix.M12,
                matrix.M21, matrix.M22);
        }

        public static OpenTK.Matrix3 TK(this Rydia.Matrix3 matrix)
        {
            return new OpenTK.Matrix3(
                matrix.M11, matrix.M12, matrix.M13,
                matrix.M21, matrix.M22, matrix.M23,
                matrix.M31, matrix.M32, matrix.M33);
        }

        public static OpenTK.Matrix4 TK(this Rydia.Matrix4 matrix)
        {
            return new OpenTK.Matrix4(
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44);
        }

    }
}
