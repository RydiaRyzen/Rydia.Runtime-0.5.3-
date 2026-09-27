using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Drawing
{

    /// <summary>
    /// 4 つの浮動小数点値で色を表すデータを格納する構造体です。
    /// </summary>
    public struct Color4 : IColorData
    {

        private ColorRgba _Rgba;

        /// <summary>
        /// 赤色成分を 0.0 ～ 1.0 の範囲で取得または設定します。
        /// </summary>
        public float Rf
        {
            get
            {
                return this._Rgba.R / 255f;
            }
            set
            {
                value = MathFR.Clamp01(value);
                this._Rgba.R = MathFR.ClampToByte(value * 255f);
            }
        }

        /// <summary>
        /// 緑色成分を 0.0 ～ 1.0 の範囲で取得または設定します。
        /// </summary>
        public float Gf
        {
            get
            {
                return this._Rgba.G / 255f;
            }
            set
            {
                value = MathFR.Clamp01(value);
                this._Rgba.G = MathFR.ClampToByte(value * 255f);
            }
        }

        /// <summary>
        /// 青色成分を 0.0 ～ 1.0 の範囲で取得または設定します。
        /// </summary>
        public float Bf
        {
            get
            {
                return this._Rgba.B / 255f;
            }
            set
            {
                value = MathFR.Clamp01(value);
                this._Rgba.B = MathFR.ClampToByte(value * 255f);
            }
        }

        /// <summary>
        /// アルファ成分を 0.0 ～ 1.0 の範囲で取得または設定します。
        /// </summary>
        public float Af
        {
            get
            {
                return this._Rgba.A / 255f;
            }
            set
            {
                value = MathFR.Clamp01(value);
                this._Rgba.A = MathFR.ClampToByte(value * 255f);
            }
        }

        /// <summary>
        /// RGBA 形式の色を取得または設定します。
        /// </summary>
        public ColorRgba Rgba
        {
            get
            {
                return this._Rgba;
            }
            set
            {
                this._Rgba = value;
            }
        }

        /// <summary>
        /// 指定された RGBA 成分を使用して、
        /// <see cref="Color4"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="r">
        /// 0.0 ～ 1.0 の範囲で表される赤色成分です。
        /// </param>
        /// <param name="g">
        /// 0.0 ～ 1.0 の範囲で表される緑色成分です。
        /// </param>
        /// <param name="b">
        /// 0.0 ～ 1.0 の範囲で表される青色成分です。
        /// </param>
        /// <param name="a">
        /// 0.0 ～ 1.0 の範囲で表されるアルファ成分です。
        /// 既定値は 1.0 です。
        /// </param>
        public Color4(float r, float g, float b, float a = 1.0f)
        {
            this._Rgba = ColorRgba.Blue;
            this._Rgba.R = MathFR.ClampToByte(MathFR.Clamp01(r) * 255.0f);
            this._Rgba.G = MathFR.ClampToByte(MathFR.Clamp01(g) * 255.0f);
            this._Rgba.B = MathFR.ClampToByte(MathFR.Clamp01(b) * 255.0f);
            this._Rgba.A = MathFR.ClampToByte(MathFR.Clamp01(a) * 255.0f);
        }

        /// <summary>
        /// 指定された値を使用してグレースケールの色を初期化します。
        /// </summary>
        /// <param name="value">
        /// 0.0 ～ 1.0 の範囲で表される RGB 各成分の値です。
        /// </param>
        /// <param name="a">
        /// 0.0 ～ 1.0 の範囲で表されるアルファ成分です。
        /// 既定値は 1.0 です。
        /// </param>
        public Color4(float value, float a = 1.0f)
            : this(value, value, value, a)
        {

        }

        /// <summary>
        /// 指定された RGBA 色を使用して、
        /// <see cref="Color4"/> の新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="rgba">初期値として使用する RGBA 色です。</param>
        public Color4(ColorRgba rgba)
        {
            this._Rgba = rgba;
        }

        /// <summary>
        /// 色を <see cref="Vector4"/> に変換して取得します。
        /// </summary>
        /// <value>
        /// R、G、B、A の各成分を 0.0 ～ 1.0 の範囲で格納した
        /// <see cref="Vector4"/> です。
        /// </value>
        public Vector4 Vector4
        {
            get
            {
                return new Vector4(Rf, Gf, Bf, Af);
            }
        }

        /// <summary>
        /// 色を RGBA 形式の整数値に変換します。
        /// </summary>
        /// <returns>RGBA 形式の色情報を表す整数値です。</returns>
        public int ToIntRgba()
        {
            return this._Rgba.ToIntRgba();
        }

        /// <summary>
        /// 指定された RGBA 形式の整数値を使用して色を設定します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の色情報を表す整数値です。</param>
        public void SetIntRgba(int rgba)
        {
            this._Rgba.SetIntRgba(rgba);
        }

        /// <summary>
        /// 色を ARGB 形式の整数値に変換します。
        /// </summary>
        /// <returns>ARGB 形式の色情報を表す整数値です。</returns>
        public int ToIntArgb()
        {
            return this._Rgba.ToIntArgb();
        }

        /// <summary>
        /// 指定された ARGB 形式の整数値を使用して色を設定します。
        /// </summary>
        /// <param name="argb">ARGB 形式の色情報を表す整数値です。</param>
        public void SetIntArgb(int argb)
        {
            this._Rgba.SetIntArgb(argb);
        }

    }
}
