using System;
using System.Runtime.InteropServices;

namespace Rydia.Drawing
{

    /// <summary>
    /// 16 バイトの HSVA 色値を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ColorHsva : IColorData, IEquatable<ColorHsva>
    {

        /// <summary>
        /// 色相成分を取得または設定します。
        /// 値は 0.0 ～ 1.0 の範囲で表されます。
        /// </summary>
        public float H;

        /// <summary>
        /// 彩度成分を取得または設定します。
        /// 値は 0.0 ～ 1.0 の範囲で表されます。
        /// </summary>
        public float S;

        /// <summary>
        /// 明度成分を取得または設定します。
        /// 値は 0.0 ～ 1.0 の範囲で表されます。
        /// </summary>
        public float V;

        /// <summary>
        /// アルファ成分を取得または設定します。
        /// 値は 0.0 ～ 1.0 の範囲で表されます。
        /// </summary>
        public float A;

        /// <summary>
        /// 指定された色をコピーして、<see cref="ColorHsva"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="clr">コピー元の色です。</param>
        public ColorHsva(ColorHsva clr)
        {
            this.H = clr.H;
            this.S = clr.S;
            this.V = clr.V;
            this.A = clr.A;
        }

        /// <summary>
        /// 指定された各成分を使用して、<see cref="ColorHsva"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="h">0.0 ～ 1.0 の範囲で指定する色相成分です。</param>
        /// <param name="s">0.0 ～ 1.0 の範囲で指定する彩度成分です。</param>
        /// <param name="v">0.0 ～ 1.0 の範囲で指定する明度成分です。</param>
        /// <param name="a">0.0 ～ 1.0 の範囲で指定するアルファ成分です。既定値は 1.0 です。</param>
        public ColorHsva(float h, float s, float v, float a = 1.0f)
        {
            this.H = h;
            this.S = s;
            this.V = v;
            this.A = a;
        }

        /// <summary>
        /// 色の輝度を計算します。
        /// 人間の視覚における明るさの知覚を近似するため、RGBA 各成分に異なる重みを適用します。
        /// </summary>
        /// <returns>0.0 ～ 1.0 の範囲で表される色の輝度です。</returns>
        public float GetLuminance()
        {
            return ToRgba().GetLuminance();
        }

        /// <summary>
        /// 色相成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="h">0.0 ～ 1.0 の範囲で指定する新しい色相成分です。</param>
        /// <returns>指定された色相成分を持つ新しい色です。</returns>
        public ColorHsva WithHue(float h)
        {
            return new ColorHsva(h, this.S, this.V, this.A);
        }

        /// <summary>
        /// 彩度成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="s">0.0 ～ 1.0 の範囲で指定する新しい彩度成分です。</param>
        /// <returns>指定された彩度成分を持つ新しい色です。</returns>
        public ColorHsva WithSaturation(float s)
        {
            return new ColorHsva(this.H, s, this.V, this.A);
        }

        /// <summary>
        /// 明度成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="v">0.0 ～ 1.0 の範囲で指定する新しい明度成分です。</param>
        /// <returns>指定された明度成分を持つ新しい色です。</returns>
        public ColorHsva WithValue(float v)
        {
            return new ColorHsva(this.H, this.S, v, this.A);
        }

        /// <summary>
        /// アルファ成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="a">0.0 ～ 1.0 の範囲で指定する新しいアルファ成分です。</param>
        /// <returns>指定されたアルファ成分を持つ新しい色です。</returns>
        public ColorHsva WithAlpha(float a)
        {
            return new ColorHsva(this.H, this.S, this.V, a);
        }

        /// <summary>
        /// 色を RGBA 形式の整数値に変換します。
        /// </summary>
        /// <returns>RGBA 形式を表す整数値です。</returns>
        public int ToIntRgba()
        {
            return ToRgba().ToIntRgba();
        }

        /// <summary>
        /// 色を ARGB 形式の整数値に変換します。
        /// </summary>
        /// <returns>ARGB 形式を表す整数値です。</returns>
        public int ToIntArgb()
        {
            return ToRgba().ToIntArgb();
        }

        /// <summary>
        /// 色を RGBA 形式に変換します。
        /// </summary>
        /// <returns>RGBA 形式で表される色です。</returns>
        public ColorRgba ToRgba()
        {
            float hTemp = NormalizeHue(this.H) * 360.0f / 60.0f;
            int hi = (int)Math.Floor(hTemp) % 6;
            float f = hTemp - (float)Math.Floor(hTemp);

            float vTemp = ClampToUnit(this.V) * 255.0f;
            float sTemp = ClampToUnit(this.S);
            byte v = (byte)vTemp;
            byte p = (byte)(vTemp * (1.0f - sTemp));
            byte q = (byte)(vTemp * (1.0f - f * sTemp));
            byte t = (byte)(vTemp * (1.0f - (1.0f - f) * sTemp));

            if (hi == 0) return new ColorRgba(v, t, p, ColorRgba.ClampToByte(this.A * 255.0f));
            else if (hi == 1) return new ColorRgba(q, v, p, ColorRgba.ClampToByte(this.A * 255.0f));
            else if (hi == 2) return new ColorRgba(p, v, t, ColorRgba.ClampToByte(this.A * 255.0f));
            else if (hi == 3) return new ColorRgba(p, q, v, ColorRgba.ClampToByte(this.A * 255.0f));
            else if (hi == 4) return new ColorRgba(t, p, v, ColorRgba.ClampToByte(this.A * 255.0f));
            else return new ColorRgba(v, p, q, ColorRgba.ClampToByte(this.A * 255.0f));
        }

        /// <summary>
        /// 指定された RGBA 形式の整数値に一致するように色を設定します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の整数値です。</param>
        public void SetIntRgba(int rgba)
        {
            SetRgba(ColorRgba.FromIntRgba(rgba));
        }

        /// <summary>
        /// 指定された ARGB 形式の整数値に一致するように色を設定します。
        /// </summary>
        /// <param name="argb">ARGB 形式の整数値です。</param>
        public void SetIntArgb(int argb)
        {
            SetRgba(ColorRgba.FromIntArgb(argb));
        }

        /// <summary>
        /// 指定された RGBA 色に一致するように色を設定します。
        /// </summary>
        /// <param name="rgba">設定する RGBA 色です。</param>
        public void SetRgba(ColorRgba rgba)
        {
            float min = Math.Min(Math.Min(rgba.R, rgba.G), rgba.B);
            float max = Math.Max(Math.Max(rgba.R, rgba.G), rgba.B);
            float delta = max - min;

            if (max > 0.0f)
            {
                this.S = delta / max;
                this.V = max / 255.0f;

                int maxInt = (int)Math.Round(max);
                if (delta != 0.0f)
                {
                    if ((int)Math.Round((float)rgba.R) == maxInt)
                    {
                        this.H = (float)(rgba.G - rgba.B) / delta;
                    }
                    else if ((int)Math.Round((float)rgba.G) == maxInt)
                    {
                        this.H = 2.0f + (float)(rgba.B - rgba.R) / delta;
                    }
                    else
                    {
                        this.H = 4.0f + (float)(rgba.R - rgba.G) / delta;
                    }
                    this.H *= 60.0f;
                    if (this.H < 0.0f) this.H += 360.0f;
                }
                else
                {
                    this.H = 0.0f;
                }
            }
            else
            {
                this.H = 0.0f;
                this.S = 0.0f;
                this.V = 0.0f;
            }

            this.H /= 360.0f;
            this.A = (float)rgba.A / 255.0f;
        }

        /// <summary>
        /// この色が指定された色と等しいかどうかを判定します。
        /// </summary>
        /// <param name="other">比較対象の色です。</param>
        /// <returns>色が等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public bool Equals(ColorHsva other)
        {
            return this.H == other.H && this.S == other.S && this.V == other.V && this.A == other.A;
        }

        /// <summary>
        /// この色が指定されたオブジェクトと等しいかどうかを判定します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクトです。</param>
        /// <returns>指定されたオブジェクトが同じ色を表す場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is ColorHsva))
                return false;
            else
                return Equals((ColorHsva)obj);
        }

        /// <summary>
        /// この色のハッシュコードを取得します。
        /// </summary>
        /// <returns>RGBA 形式の整数値を使用したハッシュコードです。</returns>
        public override int GetHashCode()
        {
            return (int)ToIntRgba();
        }

        /// <summary>
        /// この色を表す文字列を取得します。
        /// </summary>
        /// <returns>HSVA 各成分を含む文字列です。</returns>
        public override string ToString()
        {
            return string.Format("HSVA ({0:F}, {1:F}, {2:F}, {3:F})", this.H, this.S, this.V, this.A);
        }

        /// <summary>
        /// 指定された RGBA 形式の整数値から新しい色を作成します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の整数値です。</param>
        /// <returns>指定された値を HSVA に変換した新しい <see cref="ColorHsva"/> です。</returns>
        public static ColorHsva FromIntRgba(int rgba)
        {
            ColorHsva temp = new ColorHsva();
            temp.SetIntRgba(rgba);
            return temp;
        }

        /// <summary>
        /// 指定された ARGB 形式の整数値から新しい色を作成します。
        /// </summary>
        /// <param name="argb">ARGB 形式の整数値です。</param>
        /// <returns>指定された値を HSVA に変換した新しい <see cref="ColorHsva"/> です。</returns>
        public static ColorHsva FromIntArgb(int argb)
        {
            ColorHsva temp = new ColorHsva();
            temp.SetIntArgb(argb);
            return temp;
        }

        /// <summary>
        /// 指定された RGBA 色から新しい色を作成します。
        /// </summary>
        /// <param name="rgba">変換元の RGBA 色です。</param>
        /// <returns>指定された RGBA 色を HSVA に変換した新しい <see cref="ColorHsva"/> です。</returns>
        public static ColorHsva FromRgba(ColorRgba rgba)
        {
            ColorHsva temp = new ColorHsva();
            temp.SetRgba(rgba);
            return temp;
        }

        /// <summary>
        /// 2 つの色が等しいかどうかを判定します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>色が等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool operator ==(ColorHsva left, ColorHsva right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 2 つの色が等しくないかどうかを判定します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>色が異なる場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool operator !=(ColorHsva left, ColorHsva right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// 整数値を <see cref="ColorHsva"/> に明示的に変換します。
        /// </summary>
        /// <param name="c">RGBA 形式の整数値です。</param>
        /// <returns>変換された <see cref="ColorHsva"/> です。</returns>
        public static explicit operator ColorHsva(int c)
        {
            return FromIntRgba(c);
        }

        /// <summary>
        /// <see cref="ColorRgba"/> を <see cref="ColorHsva"/> に明示的に変換します。
        /// </summary>
        /// <param name="c">変換する RGBA 色です。</param>
        /// <returns>変換された <see cref="ColorHsva"/> です。</returns>
        public static explicit operator ColorHsva(ColorRgba c)
        {
            return FromRgba(c);
        }

        /// <summary>
        /// <see cref="ColorHsva"/> を RGBA 形式の整数値に明示的に変換します。
        /// </summary>
        /// <param name="c">変換する色です。</param>
        /// <returns>RGBA 形式の整数値です。</returns>
        public static explicit operator int(ColorHsva c)
        {
            return c.ToIntRgba();
        }

        /// <summary>
        /// <see cref="ColorHsva"/> を <see cref="ColorRgba"/> に明示的に変換します。
        /// </summary>
        /// <param name="c">変換する色です。</param>
        /// <returns>変換された <see cref="ColorRgba"/> です。</returns>
        public static explicit operator ColorRgba(ColorHsva c)
        {
            return c.ToRgba();
        }

        /// <summary>
        /// 指定された値を 0.0 ～ 1.0 の範囲に制限します。
        /// </summary>
        /// <param name="value">制限する値です。</param>
        /// <returns>0.0 ～ 1.0 の範囲に制限された値です。</returns>
        internal static float ClampToUnit(float value)
        {
            return (float)Math.Min(Math.Max(value, 0.0f), 1.0f);
        }

        /// <summary>
        /// 色相を 0.0 ～ 1.0 の範囲に正規化します。
        /// </summary>
        /// <param name="var">正規化する色相値です。</param>
        /// <returns>0.0 ～ 1.0 の範囲に正規化された色相値です。</returns>
        private static float NormalizeHue(float var)
        {
            if (var >= 0.0f && var < 1.0f) return var;

            if (var < 0.0f)
                var = 1.0f + (var % 1.0f);
            else
                var = var % 1.0f;

            return var;
        }

    }
}
