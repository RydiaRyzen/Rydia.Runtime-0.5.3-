using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Drawing
{

	/// <summary>
	/// を表すデータを格納する構造体です
	/// </summary>
	public struct Color4 : IColorData
	{

		private ColorRgba _Rgba;

		/// <summary>
		/// 赤色のコンポーネントを0.0f～1.0fの範囲で取得または設定します
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
		/// 緑色のコンポーネントを0.0f～1.0fの範囲で取得または設定します
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
		/// 青色のコンポーネントを0.0f～1.0fの範囲で取得または設定します
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
		/// 透明度のコンポーネントを0.0f～1.0fの範囲で取得または設定します
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
		///   <see cref="ColorArgb"/>を初期化します
		/// </summary>
		/// <param name="r">0.0f～1.0fの範囲で表される赤色のコンポーネントを表す数値</param>
		/// <param name="g">0.0f～1.0fの範囲で表される緑色のコンポーネントを表す数値</param>
		/// <param name="b">0.0f～1.0fの範囲で表される青色のコンポーネントを表す数値</param>
		/// <param name="a">0.0f～1.0fの範囲で表される透明度のコンポーネントを表す数値</param>
		public Color4(float r, float g, float b, float a = 1.0f)
		{
            this._Rgba = ColorRgba.Blue;
            this._Rgba.R = MathFR.ClampToByte(MathFR.Clamp01(r) * 255.0f);
            this._Rgba.G = MathFR.ClampToByte(MathFR.Clamp01(g) * 255.0f);
            this._Rgba.B = MathFR.ClampToByte(MathFR.Clamp01(b) * 255.0f);
            this._Rgba.A = MathFR.ClampToByte(MathFR.Clamp01(a) * 255.0f);
		}

		/// <summary>
		///   <see cref="ColorArgb"/>を初期化します
		/// </summary>
		/// <param name="value">0f-1fで表されるRGBを表す数値</param>
		/// <param name="a">0f-1fで表される透明度のコンポーネントを表す数値</param>
		public Color4(float value, float a = 1.0f)
			: this(value, value, value, a)
		{

		}

		public Color4(ColorRgba rgba)
		{
            this._Rgba = rgba;
		}

		public Vector4 Vector4
		{
			get
			{
				return new Vector4(Rf, Gf, Bf, Af);
			}
		}

        public int ToIntRgba()
        {
			return this._Rgba.ToIntRgba();
        }

        public void SetIntRgba(int rgba)
        {
            this._Rgba.SetIntRgba(rgba);
        }

        public int ToIntArgb()
        {
            return this._Rgba.ToIntArgb();
        }

        public void SetIntArgb(int argb)
        {
            this._Rgba.SetIntArgb(argb);
        }

    }
}
