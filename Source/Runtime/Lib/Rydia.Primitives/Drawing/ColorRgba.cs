using System;
using System.Runtime.InteropServices;

namespace Rydia.Drawing
{

    /// <summary>
    /// 4 バイトの RGBA 色値を表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ColorRgba : IColorData, IEquatable<ColorRgba>
    {

        /// <summary>
        /// 現在の文字色を指定した色に変更するためのフォーマット文字列を返します。
        /// </summary>
        /// <returns>RGBA 値を 8 桁の 16 進数で表した文字列です。</returns>
        public string FormattedText()
        {
            int intClr = ToIntRgba();
            return string.Format("{0:X8}", intClr);
        }

        /// <summary>
        /// 赤色成分を取得または設定します。
        /// </summary>
        public byte R;

        /// <summary>
        /// 緑色成分を取得または設定します。
        /// </summary>
        public byte G;

        /// <summary>
        /// 青色成分を取得または設定します。
        /// </summary>
        public byte B;

        /// <summary>
        /// アルファ成分を取得または設定します。
        /// 通常は不透明度として扱われます。
        /// </summary>
        public byte A;

        /// <summary>
        /// 指定された色をコピーして、<see cref="ColorRgba"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="clr">コピー元の色です。</param>
        public ColorRgba(ColorRgba clr)
        {
            this.R = clr.R;
            this.G = clr.G;
            this.B = clr.B;
            this.A = clr.A;
        }

        /// <summary>
        /// 指定された RGBA 整数値を使用して、<see cref="ColorRgba"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の整数値です。</param>
        public ColorRgba(int rgba)
        {
            this.R = (byte)((rgba & 0xFF000000) >> 24);
            this.G = (byte)((rgba & 0x00FF0000) >> 16);
            this.B = (byte)((rgba & 0x0000FF00) >> 8);
            this.A = (byte)(rgba & 0x000000FF);
        }

        /// <summary>
        /// 指定された各成分を使用して、<see cref="ColorRgba"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="r">赤色成分です。</param>
        /// <param name="g">緑色成分です。</param>
        /// <param name="b">青色成分です。</param>
        /// <param name="a">アルファ成分です。既定値は 255 です。</param>
        public ColorRgba(byte r, byte g, byte b, byte a = 255)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        /// <summary>
        /// 指定された明度値とアルファ値を使用して、グレースケール色として
        /// <see cref="ColorRgba"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">色の明度を表す値です。</param>
        /// <param name="a">アルファ成分です。既定値は 255 です。</param>
        public ColorRgba(byte value, byte a = 255)
        {
            this.R = value;
            this.G = value;
            this.B = value;
            this.A = a;
        }

        /// <summary>
        /// 各色成分を 0.0 ～ 1.0 の範囲で指定して、
        /// <see cref="ColorRgba"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="r">0.0 ～ 1.0 の範囲で指定する赤色成分です。</param>
        /// <param name="g">0.0 ～ 1.0 の範囲で指定する緑色成分です。</param>
        /// <param name="b">0.0 ～ 1.0 の範囲で指定する青色成分です。</param>
        /// <param name="a">0.0 ～ 1.0 の範囲で指定するアルファ成分です。既定値は 1.0 です。</param>
        public ColorRgba(float r, float g, float b, float a = 1.0f)
        {
            this.R = ClampToByte(r * 255.0f);
            this.G = ClampToByte(g * 255.0f);
            this.B = ClampToByte(b * 255.0f);
            this.A = ClampToByte(a * 255.0f);
        }

        /// <summary>
        /// 明度とアルファ値を 0.0 ～ 1.0 の範囲で指定して、
        /// グレースケール色として <see cref="ColorRgba"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="value">0.0 ～ 1.0 の範囲で指定する色の明度です。</param>
        /// <param name="a">0.0 ～ 1.0 の範囲で指定するアルファ成分です。既定値は 1.0 です。</param>
        public ColorRgba(float value, float a = 1.0f)
        {
            this.R = ClampToByte(value * 255.0f);
            this.G = this.R;
            this.B = this.R;
            this.A = ClampToByte(a * 255.0f);
        }

        /// <summary>
        /// 赤色成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="r">新しい赤色成分です。</param>
        /// <returns>指定された赤色成分を持つ新しい色です。</returns>
        public ColorRgba WithRed(byte r)
        {
            return new ColorRgba(r, this.G, this.B, this.A);
        }

        /// <summary>
        /// 緑色成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="g">新しい緑色成分です。</param>
        /// <returns>指定された緑色成分を持つ新しい色です。</returns>
        public ColorRgba WithGreen(byte g)
        {
            return new ColorRgba(this.R, g, this.B, this.A);
        }

        /// <summary>
        /// 青色成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="b">新しい青色成分です。</param>
        /// <returns>指定された青色成分を持つ新しい色です。</returns>
        public ColorRgba WithBlue(byte b)
        {
            return new ColorRgba(this.R, this.G, b, this.A);
        }

        /// <summary>
        /// アルファ成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="a">新しいアルファ成分です。</param>
        /// <returns>指定されたアルファ成分を持つ新しい色です。</returns>
        public ColorRgba WithAlpha(byte a)
        {
            return new ColorRgba(this.R, this.G, this.B, a);
        }

        /// <summary>
        /// 赤色成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="r">0.0 ～ 1.0 の範囲で指定する新しい赤色成分です。</param>
        /// <returns>指定された赤色成分を持つ新しい色です。</returns>
        public ColorRgba WithRed(float r)
        {
            return new ColorRgba(ClampToByte(r * 255.0f), this.G, this.B, this.A);
        }

        /// <summary>
        /// 緑色成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="g">0.0 ～ 1.0 の範囲で指定する新しい緑色成分です。</param>
        /// <returns>指定された緑色成分を持つ新しい色です。</returns>
        public ColorRgba WithGreen(float g)
        {
            return new ColorRgba(this.R, ClampToByte(g * 255.0f), this.B, this.A);
        }

        /// <summary>
        /// 青色成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="b">0.0 ～ 1.0 の範囲で指定する新しい青色成分です。</param>
        /// <returns>指定された青色成分を持つ新しい色です。</returns>
        public ColorRgba WithBlue(float b)
        {
            return new ColorRgba(this.R, this.G, ClampToByte(b * 255.0f), this.A);
        }

        /// <summary>
        /// アルファ成分を変更した新しい色を返します。
        /// </summary>
        /// <param name="a">0.0 ～ 1.0 の範囲で指定する新しいアルファ成分です。</param>
        /// <returns>指定されたアルファ成分を持つ新しい色です。</returns>
        public ColorRgba WithAlpha(float a)
        {
            return new ColorRgba(this.R, this.G, this.B, ClampToByte(a * 255.0f));
        }

        /// <summary>
        /// 色の輝度を計算します。
        /// 人間の視覚における明るさの知覚を近似するため、各色成分に異なる重みを適用します。
        /// </summary>
        /// <returns>0.0 ～ 1.0 の範囲で表される色の輝度です。</returns>
        public float GetLuminance()
        {
            return (0.2126f * this.R + 0.7152f * this.G + 0.0722f * this.B) / 255.0f;
        }

        /// <summary>
        /// 色を RGBA 形式の整数値に変換します。
        /// </summary>
        /// <returns>RGBA 形式を表す整数値です。</returns>
        public int ToIntRgba()
        {
            return ((int)this.R << 24) | ((int)this.G << 16) | ((int)this.B << 8) | ((int)this.A);
        }

        /// <summary>
        /// 色を ARGB 形式の整数値に変換します。
        /// </summary>
        /// <returns>ARGB 形式を表す整数値です。</returns>
        public int ToIntArgb()
        {
            return ((int)this.A << 24) | ((int)this.R << 16) | ((int)this.G << 8) | ((int)this.B);
        }

        /// <summary>
        /// 色を HSVA 色空間の値に変換します。
        /// </summary>
        /// <returns>HSVA 形式で表される色です。</returns>
        public ColorHsva ToHsva()
        {
            return ColorHsva.FromRgba(this);
        }

        /// <summary>
        /// 指定された ARGB 形式の整数値に一致するように色を設定します。
        /// </summary>
        /// <param name="argb">ARGB 形式の整数値です。</param>
        public void SetIntArgb(int argb)
        {
            this.A = (byte)((argb & 0xFF000000) >> 24);
            this.R = (byte)((argb & 0x00FF0000) >> 16);
            this.G = (byte)((argb & 0x0000FF00) >> 8);
            this.B = (byte)(argb & 0x000000FF);
        }

        /// <summary>
        /// 指定された RGBA 形式の整数値に一致するように色を設定します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の整数値です。</param>
        public void SetIntRgba(int rgba)
        {
            this.R = (byte)((rgba & 0xFF000000) >> 24);
            this.G = (byte)((rgba & 0x00FF0000) >> 16);
            this.B = (byte)((rgba & 0x0000FF00) >> 8);
            this.A = (byte)(rgba & 0x000000FF);
        }

        /// <summary>
        /// 指定された HSVA 色に一致するように色を設定します。
        /// </summary>
        /// <param name="hsva">設定する HSVA 色です。</param>
        public void SetHsva(ColorHsva hsva)
        {
            this = hsva.ToRgba();
        }

        /// <summary>
        /// この色が指定された色と等しいかどうかを判定します。
        /// </summary>
        /// <param name="other">比較対象の色です。</param>
        /// <returns>色が等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public bool Equals(ColorRgba other)
        {
            return this.R == other.R && this.G == other.G && this.B == other.B && this.A == other.A;
        }

        /// <summary>
        /// この色が指定されたオブジェクトと等しいかどうかを判定します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクトです。</param>
        /// <returns>指定されたオブジェクトが同じ色を表す場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is ColorRgba))
                return false;
            else
                return Equals((ColorRgba)obj);
        }

        /// <summary>
        /// この色のハッシュコードを取得します。
        /// </summary>
        /// <returns>RGBA 形式の整数値を使用したハッシュコードです。</returns>
        public override int GetHashCode()
        {
            return ToIntRgba();
        }

        /// <summary>
        /// この色を表す文字列を取得します。
        /// </summary>
        /// <returns>RGBA 各成分と 16 進数表記の値を含む文字列です。</returns>
        public override string ToString()
        {
            return string.Format("RGBA ({0}, {1}, {2}, {3} / #{4:X8})", this.R, this.G, this.B, this.A, ToIntRgba());
        }

        /// <summary>
        /// 指定された RGBA 形式の整数値から新しい色を作成します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の整数値です。</param>
        /// <returns>指定された値を持つ新しい <see cref="ColorRgba"/> です。</returns>
        public static ColorRgba FromIntRgba(int rgba)
        {
            ColorRgba temp = new ColorRgba();
            temp.SetIntRgba(rgba);
            return temp;
        }

        /// <summary>
        /// 指定された ARGB 形式の整数値から新しい色を作成します。
        /// </summary>
        /// <param name="argb">ARGB 形式の整数値です。</param>
        /// <returns>指定された値を持つ新しい <see cref="ColorRgba"/> です。</returns>
        public static ColorRgba FromIntArgb(int argb)
        {
            ColorRgba temp = new ColorRgba();
            temp.SetIntArgb(argb);
            return temp;
        }

        /// <summary>
        /// 指定された HSVA 値から新しい色を作成します。
        /// </summary>
        /// <param name="hsva">変換元の HSVA 色です。</param>
        /// <returns>指定された HSVA 値を RGBA に変換した色です。</returns>
        public static ColorRgba FromHsva(ColorHsva hsva)
        {
            return hsva.ToRgba();
        }

        /// <summary>
        /// 2 つの色の間を線形補間して色を合成します。
        /// </summary>
        /// <param name="first">1 番目の色です。</param>
        /// <param name="second">2 番目の色です。</param>
        /// <param name="factor">線形補間係数です。0 の場合は 1 番目の色、1 の場合は 2 番目の色になります。</param>
        /// <returns>線形補間によって合成された色です。</returns>
        public static ColorRgba Lerp(ColorRgba first, ColorRgba second, float factor)
        {
            float invFactor = 1.0f - factor;
            return new ColorRgba(
                ClampToByte((float)Math.Round(first.R * invFactor + second.R * factor)),
                ClampToByte((float)Math.Round(first.G * invFactor + second.G * factor)),
                ClampToByte((float)Math.Round(first.B * invFactor + second.B * factor)),
                ClampToByte((float)Math.Round(first.A * invFactor + second.A * factor)));
        }

        /// <summary>
        /// 2 つの色が等しいかどうかを判定します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>色が等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool operator ==(ColorRgba left, ColorRgba right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// 2 つの色が等しくないかどうかを判定します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>色が異なる場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> です。</returns>
        public static bool operator !=(ColorRgba left, ColorRgba right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// 2 つの色を各成分単位で加算します。
        /// 各成分の値は 255 を上限として制限されます。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>各成分を加算した色です。</returns>
        public static ColorRgba operator +(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
                (byte)Math.Min(255, left.R + right.R),
                (byte)Math.Min(255, left.G + right.G),
                (byte)Math.Min(255, left.B + right.B),
                (byte)Math.Min(255, left.A + right.A));
        }

        /// <summary>
        /// 2 つの色を各成分単位で減算します。
        /// 各成分の値は 0 を下限として制限されます。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>2 番目の色を減算した結果の色です。</returns>
        public static ColorRgba operator -(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
                (byte)Math.Max(0, left.R - right.R),
                (byte)Math.Max(0, left.G - right.G),
                (byte)Math.Max(0, left.B - right.B),
                (byte)Math.Max(0, left.A - right.A));
        }

        /// <summary>
        /// 2 つの色を各成分単位で乗算します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <returns>各成分を乗算した色です。</returns>
        public static ColorRgba operator *(ColorRgba left, ColorRgba right)
        {
            return new ColorRgba(
                ClampToByte((float)Math.Round((float)left.R * right.R / 255.0f)),
                ClampToByte((float)Math.Round((float)left.G * right.G / 255.0f)),
                ClampToByte((float)Math.Round((float)left.B * right.B / 255.0f)),
                ClampToByte((float)Math.Round((float)left.A * right.A / 255.0f)));
        }

        /// <summary>
        /// 指定された係数で色を拡大・縮小します。
        /// 色成分とアルファ成分の両方に同じ係数を適用します。
        /// </summary>
        /// <param name="left">拡大・縮小する色です。</param>
        /// <param name="right">拡大・縮小に使用する係数です。</param>
        /// <returns>指定された係数で拡大・縮小した色です。</returns>
        public static ColorRgba operator *(ColorRgba left, float right)
        {
            return new ColorRgba(
                ClampToByte((float)Math.Round(left.R * right)),
                ClampToByte((float)Math.Round(left.G * right)),
                ClampToByte((float)Math.Round(left.B * right)),
                ClampToByte((float)Math.Round(left.A * right)));
        }

        /// <summary>
        /// <see cref="ColorRgba"/> を <see cref="Color4"/> に暗黙的に変換します。
        /// </summary>
        /// <param name="color">変換する色です。</param>
        /// <returns>変換された <see cref="Color4"/> です。</returns>
        public static implicit operator Color4(ColorRgba color)
        {
            return new Color4(color);
        }

        /// <summary>
        /// <see cref="Color4"/> を <see cref="ColorRgba"/> に暗黙的に変換します。
        /// </summary>
        /// <param name="color">変換する色です。</param>
        /// <returns>変換された <see cref="ColorRgba"/> です。</returns>
        public static implicit operator ColorRgba(Color4 color)
        {
            return new ColorRgba(color);
        }

        /// <summary>
        /// 2 つの色を各成分単位で加算します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <param name="result">加算結果の色です。</param>
        public static void Add(ref ColorRgba left, ref ColorRgba right, out ColorRgba result)
        {
            result = new ColorRgba(
                (byte)Math.Min(255, left.R + right.R),
                (byte)Math.Min(255, left.G + right.G),
                (byte)Math.Min(255, left.B + right.B),
                (byte)Math.Min(255, left.A + right.A));
        }

        /// <summary>
        /// 2 つの色を各成分単位で減算します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <param name="result">減算結果の色です。</param>
        public static void Subtract(ref ColorRgba left, ref ColorRgba right, out ColorRgba result)
        {
            result = new ColorRgba(
                (byte)Math.Max(0, left.R - right.R),
                (byte)Math.Max(0, left.G - right.G),
                (byte)Math.Max(0, left.B - right.B),
                (byte)Math.Max(0, left.A - right.A));
        }

        /// <summary>
        /// 2 つの色を各成分単位で乗算します。
        /// </summary>
        /// <param name="left">1 番目の色です。</param>
        /// <param name="right">2 番目の色です。</param>
        /// <param name="result">乗算結果の色です。</param>
        public static void Multiply(ref ColorRgba left, ref ColorRgba right, out ColorRgba result)
        {
            result = new ColorRgba(
                ClampToByte((float)Math.Round((float)left.R * right.R / 255.0f)),
                ClampToByte((float)Math.Round((float)left.G * right.G / 255.0f)),
                ClampToByte((float)Math.Round((float)left.B * right.B / 255.0f)),
                ClampToByte((float)Math.Round((float)left.A * right.A / 255.0f)));
        }

        /// <summary>
        /// 指定された係数で色を拡大・縮小します。
        /// 色成分とアルファ成分の両方に同じ係数を適用します。
        /// </summary>
        /// <param name="left">拡大・縮小する色です。</param>
        /// <param name="right">拡大・縮小に使用する係数です。</param>
        /// <param name="result">拡大・縮小した結果の色です。</param>
        public static void Scale(ref ColorRgba left, float right, out ColorRgba result)
        {
            result = new ColorRgba(
                ClampToByte((float)Math.Round(left.R * right)),
                ClampToByte((float)Math.Round(left.G * right)),
                ClampToByte((float)Math.Round(left.B * right)),
                ClampToByte((float)Math.Round(left.A * right)));
        }

        /// <summary>
        /// 整数値を <see cref="ColorRgba"/> に明示的に変換します。
        /// </summary>
        /// <param name="c">RGBA 形式の整数値です。</param>
        /// <returns>変換された <see cref="ColorRgba"/> です。</returns>
        public static explicit operator ColorRgba(int c)
        {
            return new ColorRgba(c);
        }

        /// <summary>
        /// <see cref="ColorHsva"/> を <see cref="ColorRgba"/> に明示的に変換します。
        /// </summary>
        /// <param name="c">変換する HSVA 色です。</param>
        /// <returns>変換された <see cref="ColorRgba"/> です。</returns>
        public static explicit operator ColorRgba(ColorHsva c)
        {
            return c.ToRgba();
        }

        /// <summary>
        /// <see cref="ColorRgba"/> を RGBA 形式の整数値に明示的に変換します。
        /// </summary>
        /// <param name="c">変換する色です。</param>
        /// <returns>RGBA 形式の整数値です。</returns>
        public static explicit operator int(ColorRgba c)
        {
            return c.ToIntRgba();
        }

        /// <summary>
        /// <see cref="ColorRgba"/> を <see cref="ColorHsva"/> に明示的に変換します。
        /// </summary>
        /// <param name="c">変換する色です。</param>
        /// <returns>変換された <see cref="ColorHsva"/> です。</returns>
        public static explicit operator ColorHsva(ColorRgba c)
        {
            return ColorHsva.FromRgba(c);
        }

        /// <summary>
        /// 指定された整数値を 0 ～ 255 の範囲に制限して <see cref="byte"/> に変換します。
        /// </summary>
        /// <param name="value">変換する整数値です。</param>
        /// <returns>0 ～ 255 の範囲に制限された値です。</returns>
        internal static byte ClampToByte(int value)
        {
            return (byte)Math.Min(Math.Max(value, 0), 255);
        }

        /// <summary>
        /// 指定された浮動小数点値を 0 ～ 255 の範囲に制限して <see cref="byte"/> に変換します。
        /// </summary>
        /// <param name="value">変換する浮動小数点値です。</param>
        /// <returns>0 ～ 255 の範囲に制限された値です。</returns>
        internal static byte ClampToByte(float value)
        {
            return (byte)Math.Min(Math.Max((int)value, 0), 255);
        }

    }
}
