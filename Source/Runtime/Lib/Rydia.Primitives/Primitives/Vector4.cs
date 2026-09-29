#region --- License ---
/*
Copyright (c) 2006 - 2008 The Open Toolkit library.

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Note: This code has been heavily modified for the Rydia framework.

	*/
#endregion

using System;
using System.Runtime.InteropServices;

namespace Rydia
{

    /// <summary>
    /// 4つの単精度浮動小数点数で4次元ベクトルを表します。
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
	public struct Vector4 : IEquatable<Vector4>
	{


        /// <summary>
        /// X軸方向を向く単位ベクトルを定義します。
        /// </summary>
        public static Vector4 UnitX = new Vector4(1, 0, 0, 0);

        /// <summary>
        /// Y軸方向を向く単位ベクトルを定義します。
        /// </summary>
        public static Vector4 UnitY = new Vector4(0, 1, 0, 0);

        /// <summary>
        /// Z軸方向を向く単位ベクトルを定義します。
        /// </summary>
        public static Vector4 UnitZ = new Vector4(0, 0, 1, 0);

        /// <summary>
        /// W軸方向を向く単位ベクトルを定義します。
        /// </summary>
        public static Vector4 UnitW = new Vector4(0, 0, 0, 1);

        /// <summary>
        /// すべての成分が0のベクトルを定義します。
        /// </summary>
        public static Vector4 Zero = new Vector4(0, 0, 0, 0);

        /// <summary>
        /// すべての成分が1のベクトルを定義します。
        /// </summary>
        public static readonly Vector4 One = new Vector4(1, 1, 1, 1);

        /// <summary>
        /// X成分を取得または設定します。
        /// </summary>
        public float X;

        /// <summary>
        /// Y成分を取得または設定します。
        /// </summary>
        public float Y;

        /// <summary>
        /// Z成分を取得または設定します。
        /// </summary>
        public float Z;

        /// <summary>
        /// W成分を取得または設定します。
        /// </summary>
        public float W;

        /// <summary>
        /// このベクトルのX成分とY成分からなる <see cref="Vector2"/> を取得または設定します。
        /// </summary>
        public Vector2 Xy { get { return new Vector2(this.X, this.Y); } set { this.X = value.X; this.Y = value.Y; } }

        /// <summary>
        /// このベクトルのX成分、Y成分、Z成分からなる <see cref="Vector3"/> を取得または設定します。
        /// </summary>
        public Vector3 Xyz { get { return new Vector3(this.X, this.Y, this.Z); } set { this.X = value.X; this.Y = value.Y; this.Z = value.Z; } }


        /// <summary>
        /// ベクトルの長さを取得します。
        /// </summary>
        /// <seealso cref="LengthSquared"/>
        public float Length
		{
			get
			{
				return (float)Math.Sqrt(
					this.X * this.X + 
					this.Y * this.Y + 
					this.Z * this.Z + 
					this.W * this.W);
			}
		}

        /// <summary>
        /// ベクトルの長さの二乗を取得します。
        /// </summary>
        /// <remarks>
        /// <see cref="Length"/> で必要となる平方根の計算を行わないため、
        /// ベクトルの長さを比較する場合に適しています。
        /// </remarks>
        /// <seealso cref="Length"/>
        public float LengthSquared
		{
			get
			{
				return 
					this.X * this.X + 
					this.Y * this.Y + 
					this.Z * this.Z + 
					this.W * this.W;
			}
		}

        /// <summary>
        /// このベクトルを正規化したベクトルを取得します。
        /// </summary>
        /// <remarks>
        /// ベクトルの長さが極めて小さい場合は <see cref="Zero"/> を返します。
        /// </remarks>
        public Vector4 Normalized
		{
			get
			{
				float length = Length;
				if (length < 1e-15f) return Zero;

				float scale = 1.0f / length;
				return new Vector4(
					this.X * scale, 
					this.Y * scale, 
					this.Z * scale, 
					this.W * scale);
			}
		}

        /// <summary>
        /// 指定したインデックスの成分を取得または設定します。
        /// </summary>
        /// <param name="index">
        /// 成分のインデックス。0はX、1はY、2はZ、3はWを示します。
        /// </param>
        /// <exception cref="IndexOutOfRangeException">
        /// インデックスが0～3の範囲外の場合に発生します。
        /// </exception>
        public float this[int index]
		{
			get
			{
				switch (index)
				{
					case 0: return this.X;
					case 1: return this.Y;
					case 2: return this.Z;
					case 3: return this.W;
					default: throw new IndexOutOfRangeException("Vector4 access at index: " + index);
				}
			}
			set
			{
				switch (index)
				{
					case 0: this.X = value; return;
					case 1: this.Y = value; return;
					case 2: this.Z = value; return;
					case 3: this.W = value; return;
					default: throw new IndexOutOfRangeException("Vector4 access at index: " + index);
				}
			}
		}

        /// <summary>
        /// このベクトルを正規化して単位長にします。
        /// </summary>
        /// <remarks>
        /// ベクトルの長さが極めて小さい場合は <see cref="Zero"/> に設定します。
        /// </remarks>
        public void Normalize()
		{
			float length = Length;
			if (length < 1e-15f)
			{
				this = Zero;
		}
			else
			{
				float scale = 1.0f / length;
				this.X *= scale;
				this.Y *= scale;
				this.Z *= scale;
				this.W *= scale;
			}
		}

        /// <summary>
        /// すべての成分を指定した値で初期化します。
        /// </summary>
        /// <param name="value">各成分に設定する値。</param>
        public Vector4(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
		}

        /// <summary>
        /// 指定した4つの成分からベクトルを生成します。
        /// </summary>
        /// <param name="x">X成分。</param>
        /// <param name="y">Y成分。</param>
        /// <param name="z">Z成分。</param>
        /// <param name="w">W成分。</param>
        public Vector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

        /// <summary>
        /// 指定した <see cref="Vector2"/> からベクトルを生成します。
        /// </summary>
        /// <param name="v">成分をコピーする <see cref="Vector2"/>。</param>
        /// <remarks>
        /// Z成分とW成分には0が設定されます。
        /// </remarks>
        public Vector4(Vector2 v)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = 0.0f;
			this.W = 0.0f;
		}

        /// <summary>
        /// 指定した <see cref="Vector2"/> とZ成分からベクトルを生成します。
        /// </summary>
        /// <param name="v">X成分とY成分をコピーする <see cref="Vector2"/>。</param>
        /// <param name="z">Z成分。</param>
        /// <remarks>
        /// W成分には0が設定されます。
        /// </remarks>
        public Vector4(Vector2 v, float z)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = z;
			this.W = 0.0f;
		}

        /// <summary>
        /// 指定した <see cref="Vector2"/>、Z成分、W成分からベクトルを生成します。
        /// </summary>
        /// <param name="v">X成分とY成分をコピーする <see cref="Vector2"/>。</param>
        /// <param name="z">Z成分。</param>
        /// <param name="w">W成分。</param>
        public Vector4(Vector2 v, float z, float w)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = z;
			this.W = w;
		}

        /// <summary>
        /// 指定した <see cref="Vector3"/> からベクトルを生成します。
        /// </summary>
        /// <param name="v">成分をコピーする <see cref="Vector3"/>。</param>
        /// <remarks>
        /// W成分には0が設定されます。
        /// </remarks>
        public Vector4(Vector3 v)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
			this.W = 0.0f;
		}

        /// <summary>
        /// 指定した <see cref="Vector3"/> とW成分からベクトルを生成します。
        /// </summary>
        /// <param name="v">X成分、Y成分、Z成分をコピーする <see cref="Vector3"/>。</param>
        /// <param name="w">W成分。</param>
        public Vector4(Vector3 v, float w)
		{
			this.X = v.X;
			this.Y = v.Y;
			this.Z = v.Z;
			this.W = w;
		}

        /// <summary>
        /// 2つのベクトルを加算します。
        /// </summary>
        /// <param name="a">左辺のベクトル。</param>
        /// <param name="b">右辺のベクトル。</param>
        /// <param name="result">加算結果。</param>
        public static void Add(ref Vector4 a, ref Vector4 b, out Vector4 result)
		{
			result = new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
		}

        /// <summary>
        /// 2つのベクトルを減算します。
        /// </summary>
        /// <param name="a">左辺のベクトル。</param>
        /// <param name="b">右辺のベクトル。</param>
        /// <param name="result">減算結果。</param>
        public static void Subtract(ref Vector4 a, ref Vector4 b, out Vector4 result)
		{
			result = new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
		}

        /// <summary>
        /// ベクトルをスカラー倍します。
        /// </summary>
        /// <param name="vector">対象のベクトル。</param>
        /// <param name="scale">スカラー値。</param>
        /// <param name="result">計算結果。</param>
        public static void Multiply(ref Vector4 vector, float scale, out Vector4 result)
		{
			result = new Vector4(vector.X * scale, vector.Y * scale, vector.Z * scale, vector.W * scale);
		}

        /// <summary>
        /// ベクトルの各成分を別のベクトルの各成分で乗算します。
        /// </summary>
        /// <param name="vector">対象のベクトル。</param>
        /// <param name="scale">各成分の倍率を指定するベクトル。</param>
        /// <param name="result">計算結果。</param>
        public static void Multiply(ref Vector4 vector, ref Vector4 scale, out Vector4 result)
		{
			result = new Vector4(vector.X * scale.X, vector.Y * scale.Y, vector.Z * scale.Z, vector.W * scale.W);
		}

        /// <summary>
        /// ベクトルをスカラー値で除算します。
        /// </summary>
        /// <param name="vector">対象のベクトル。</param>
        /// <param name="scale">除数となるスカラー値。</param>
        /// <param name="result">計算結果。</param>
        public static void Divide(ref Vector4 vector, float scale, out Vector4 result)
		{
			Multiply(ref vector, 1 / scale, out result);
		}

        /// <summary>
        /// ベクトルの各成分を別のベクトルの各成分で除算します。
        /// </summary>
        /// <param name="vector">対象のベクトル。</param>
        /// <param name="scale">除数となるベクトル。</param>
        /// <param name="result">計算結果。</param>
        public static void Divide(ref Vector4 vector, ref Vector4 scale, out Vector4 result)
		{
			result = new Vector4(vector.X / scale.X, vector.Y / scale.Y, vector.Z / scale.Z, vector.W / scale.W);
		}

        /// <summary>
        /// 2つのベクトルの各成分について最小値を求めます。
        /// </summary>
        /// <param name="a">最初のベクトル。</param>
        /// <param name="b">2番目のベクトル。</param>
        /// <returns>各成分の最小値からなるベクトル。</returns>
        public static Vector4 Min(Vector4 a, Vector4 b)
		{
			a.X = a.X < b.X ? a.X : b.X;
			a.Y = a.Y < b.Y ? a.Y : b.Y;
			a.Z = a.Z < b.Z ? a.Z : b.Z;
			a.W = a.W < b.W ? a.W : b.W;
			return a;
		}

        /// <summary>
        /// 2つのベクトルの各成分について最小値を求めます。
        /// </summary>
        /// <param name="a">最初のベクトル。</param>
        /// <param name="b">2番目のベクトル。</param>
        /// <param name="result">各成分の最小値からなるベクトル。</param>
        public static void Min(ref Vector4 a, ref Vector4 b, out Vector4 result)
		{
			result.X = a.X < b.X ? a.X : b.X;
			result.Y = a.Y < b.Y ? a.Y : b.Y;
			result.Z = a.Z < b.Z ? a.Z : b.Z;
			result.W = a.W < b.W ? a.W : b.W;
		}

        /// <summary>
        /// 2つのベクトルの各成分について最大値を求めます。
        /// </summary>
        /// <param name="a">最初のベクトル。</param>
        /// <param name="b">2番目のベクトル。</param>
        /// <returns>各成分の最大値からなるベクトル。</returns>
        public static Vector4 Max(Vector4 a, Vector4 b)
		{
			a.X = a.X > b.X ? a.X : b.X;
			a.Y = a.Y > b.Y ? a.Y : b.Y;
			a.Z = a.Z > b.Z ? a.Z : b.Z;
			a.W = a.W > b.W ? a.W : b.W;
			return a;
		}

        /// <summary>
        /// 2つのベクトルの各成分について最大値を求めます。
        /// </summary>
        /// <param name="a">最初のベクトル。</param>
        /// <param name="b">2番目のベクトル。</param>
        /// <param name="result">各成分の最大値からなるベクトル。</param>
        public static void Max(ref Vector4 a, ref Vector4 b, out Vector4 result)
		{
			result.X = a.X > b.X ? a.X : b.X;
			result.Y = a.Y > b.Y ? a.Y : b.Y;
			result.Z = a.Z > b.Z ? a.Z : b.Z;
			result.W = a.W > b.W ? a.W : b.W;
		}

        /// <summary>
        /// 2つのベクトルの内積を計算します。
        /// </summary>
        /// <param name="left">最初のベクトル。</param>
        /// <param name="right">2番目のベクトル。</param>
        /// <returns>2つのベクトルの内積。</returns>
        public static float Dot(Vector4 left, Vector4 right)
		{
			return left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
		}

        /// <summary>
        /// 2つのベクトルの内積を計算します。
        /// </summary>
        /// <param name="left">最初のベクトル。</param>
        /// <param name="right">2番目のベクトル。</param>
        /// <param name="result">2つのベクトルの内積。</param>
        public static void Dot(ref Vector4 left, ref Vector4 right, out float result)
		{
			result = left.X * right.X + left.Y * right.Y + left.Z * right.Z + left.W * right.W;
		}

        /// <summary>
        /// 2つのベクトル間を線形補間します。
        /// </summary>
        /// <param name="a">最初のベクトル。</param>
        /// <param name="b">2番目のベクトル。</param>
        /// <param name="blend">
        /// 補間係数。0の場合は <paramref name="a"/>、1の場合は <paramref name="b"/> になります。
        /// </param>
        /// <returns>線形補間によって得られたベクトル。</returns>
        public static Vector4 Lerp(Vector4 a, Vector4 b, float blend)
		{
			a.X = blend * (b.X - a.X) + a.X;
			a.Y = blend * (b.Y - a.Y) + a.Y;
			a.Z = blend * (b.Z - a.Z) + a.Z;
			a.W = blend * (b.W - a.W) + a.W;
			return a;
		}

        /// <summary>
        /// 2つのベクトル間を線形補間します。
        /// </summary>
        /// <param name="a">最初のベクトル。</param>
        /// <param name="b">2番目のベクトル。</param>
        /// <param name="blend">
        /// 補間係数。0の場合は <paramref name="a"/>、1の場合は <paramref name="b"/> になります。
        /// </param>
        /// <param name="result">線形補間によって得られたベクトル。</param>
        public static void Lerp(ref Vector4 a, ref Vector4 b, float blend, out Vector4 result)
		{
			result.X = blend * (b.X - a.X) + a.X;
			result.Y = blend * (b.Y - a.Y) + a.Y;
			result.Z = blend * (b.Z - a.Z) + a.Z;
			result.W = blend * (b.W - a.W) + a.W;
		}

        /// <summary>
        /// 指定した行列によってベクトルを変換します。
        /// </summary>
        /// <param name="vec">変換するベクトル。</param>
        /// <param name="mat">変換に使用する行列。</param>
        /// <returns>変換後のベクトル。</returns>
        public static Vector4 Transform(Vector4 vec, Matrix4 mat)
		{
			Vector4 result;
			Transform(ref vec, ref mat, out result);
			return result;
		}

        /// <summary>
        /// 指定した行列によってベクトルを変換します。
        /// </summary>
        /// <param name="vec">変換するベクトル。</param>
        /// <param name="mat">変換に使用する行列。</param>
        /// <param name="result">変換後のベクトル。</param>
        public static void Transform(ref Vector4 vec, ref Matrix4 mat, out Vector4 result)
		{
			result.X = vec.X * mat.Row0.X + vec.Y * mat.Row1.X + vec.Z * mat.Row2.X + vec.W * mat.Row3.X;
			result.Y = vec.X * mat.Row0.Y + vec.Y * mat.Row1.Y + vec.Z * mat.Row2.Y + vec.W * mat.Row3.Y;
			result.Z = vec.X * mat.Row0.Z + vec.Y * mat.Row1.Z + vec.Z * mat.Row2.Z + vec.W * mat.Row3.Z;
			result.W = vec.X * mat.Row0.W + vec.Y * mat.Row1.W + vec.Z * mat.Row2.W + vec.W * mat.Row3.W;
		}

        /// <summary>
        /// クォータニオンによる回転変換をベクトルに適用します。
        /// </summary>
        /// <param name="vec">変換するベクトル。</param>
        /// <param name="quat">回転に使用するクォータニオン。</param>
        /// <returns>変換後のベクトル。</returns>
        public static Vector4 Transform(Vector4 vec, Quaternion quat)
		{
			Vector4 result;
			Transform(ref vec, ref quat, out result);
			return result;
		}

        /// <summary>
        /// クォータニオンによる回転変換をベクトルに適用します。
        /// </summary>
        /// <param name="vec">変換するベクトル。</param>
        /// <param name="quat">回転に使用するクォータニオン。</param>
        /// <param name="result">変換後のベクトル。</param>
        public static void Transform(ref Vector4 vec, ref Quaternion quat, out Vector4 result)
		{
			Quaternion v = new Quaternion(vec.X, vec.Y, vec.Z, vec.W), i, t;
			Quaternion.Invert(ref quat, out i);
			Quaternion.Multiply(ref quat, ref v, out t);
			Quaternion.Multiply(ref t, ref i, out v);

			result = new Vector4(v.X, v.Y, v.Z, v.W);
		}

        /// <summary>
        /// 2つのベクトルを加算します。
        /// </summary>
        /// <param name="left">左辺のベクトル。</param>
        /// <param name="right">右辺のベクトル。</param>
        /// <returns>加算結果。</returns>
        public static Vector4 operator +(Vector4 left, Vector4 right)
		{
			return new Vector4(
				left.X + right.X, 
				left.Y + right.Y, 
				left.Z + right.Z, 
				left.W + right.W);
		}

        /// <summary>
        /// 2つのベクトルを減算します。
        /// </summary>
        /// <param name="left">左辺のベクトル。</param>
        /// <param name="right">右辺のベクトル。</param>
        /// <returns>減算結果。</returns>
        public static Vector4 operator -(Vector4 left, Vector4 right)
		{
			return new Vector4(
				left.X - right.X, 
				left.Y - right.Y, 
				left.Z - right.Z, 
				left.W - right.W);
		}

        /// <summary>
        /// ベクトルの各成分の符号を反転します。
        /// </summary>
        /// <param name="vec">対象のベクトル。</param>
        /// <returns>符号を反転したベクトル。</returns>
        public static Vector4 operator -(Vector4 vec)
		{
			return new Vector4(
				-vec.X, 
				-vec.Y, 
				-vec.Z, 
				-vec.W);
		}

        /// <summary>
        /// ベクトルをスカラー値で乗算します。
        /// </summary>
        /// <param name="vec">対象のベクトル。</param>
        /// <param name="scale">スカラー値。</param>
        /// <returns>乗算結果。</returns>
        public static Vector4 operator *(Vector4 vec, float scale)
		{
			return new Vector4(
				vec.X * scale, 
				vec.Y * scale, 
				vec.Z * scale,
				vec.W * scale);
		}

        /// <summary>
        /// ベクトルの各成分を別のベクトルの各成分で乗算します。
        /// </summary>
        /// <param name="vec">対象のベクトル。</param>
        /// <param name="scale">各成分の倍率を指定するベクトル。</param>
        /// <returns>乗算結果。</returns>
        public static Vector4 operator *(Vector4 vec, Vector4 scale)
		{
			return new Vector4(
				vec.X * scale.X, 
				vec.Y * scale.Y, 
				vec.Z * scale.Z, 
				vec.W * scale.W);
		}

        /// <summary>
        /// スカラー値をベクトルの各成分に乗算します。
        /// </summary>
        /// <param name="scale">スカラー値。</param>
        /// <param name="vec">対象のベクトル。</param>
        /// <returns>乗算結果。</returns>
        public static Vector4 operator *(float scale, Vector4 vec)
		{
			return vec * scale;
		}

        /// <summary>
        /// ベクトルをスカラー値で除算します。
        /// </summary>
        /// <param name="vec">対象のベクトル。</param>
        /// <param name="scale">スカラー値。</param>
        /// <returns>除算結果。</returns>
        public static Vector4 operator /(Vector4 vec, float scale)
		{
			return vec * (1.0f / scale);
		}

        /// <summary>
        /// ベクトルの各成分を別のベクトルの各成分で除算します。
        /// </summary>
        /// <param name="vec">対象のベクトル。</param>
        /// <param name="scale">各成分の除数を指定するベクトル。</param>
        /// <returns>除算結果。</returns>
        public static Vector4 operator /(Vector4 vec, Vector4 scale)
		{
			return new Vector4(
				vec.X / scale.X, 
				vec.Y / scale.Y, 
				vec.Z / scale.Z, 
				vec.W / scale.W);
		}

        /// <summary>
        /// 2つのベクトルが等しいかどうかを比較します。
        /// </summary>
        /// <param name="left">左辺のベクトル。</param>
        /// <param name="right">右辺のベクトル。</param>
        /// <returns>2つのベクトルが等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/>。</returns>
        public static bool operator ==(Vector4 left, Vector4 right)
		{
			return left.Equals(right);
		}

        /// <summary>
        /// 2つのベクトルが等しくないかどうかを比較します。
        /// </summary>
        /// <param name="left">左辺のベクトル。</param>
        /// <param name="right">右辺のベクトル。</param>
        /// <returns>2つのベクトルが等しくない場合は <see langword="true"/>、それ以外の場合は <see langword="false"/>。</returns>
        public static bool operator !=(Vector4 left, Vector4 right)
		{
			return !left.Equals(right);
		}

        /// <summary>
        /// 現在の <see cref="Vector4"/> を表す文字列を返します。
        /// </summary>
        /// <returns>
        /// 現在のベクトルを表す文字列。
        /// </returns>
        public override string ToString()
		{
			return string.Format("({0}, {1}, {2}, {3})", this.X, this.Y, this.Z, this.W);
		}

        /// <summary>
        /// このインスタンスのハッシュコードを返します。
        /// </summary>
        /// <returns>このインスタンスのハッシュコード。</returns>
        public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
		}

        /// <summary>
        /// 指定したオブジェクトとこのインスタンスが等しいかどうかを示します。
        /// </summary>
        /// <param name="obj">比較対象のオブジェクト。</param>
        /// <returns>等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/>。</returns>
        public override bool Equals(object obj)
		{
			if (!(obj is Vector4))
				return false;

			return Equals((Vector4)obj);
		}

        /// <summary>
        /// 指定した <see cref="Vector4"/> とこのインスタンスが等しいかどうかを示します。
        /// </summary>
        /// <param name="other">比較対象のベクトル。</param>
        /// <returns>等しい場合は <see langword="true"/>、それ以外の場合は <see langword="false"/>。</returns>
        public bool Equals(Vector4 other)
		{
			return
				this.X == other.X &&
				this.Y == other.Y &&
				this.Z == other.Z &&
				this.W == other.W;
		}
	}
}
