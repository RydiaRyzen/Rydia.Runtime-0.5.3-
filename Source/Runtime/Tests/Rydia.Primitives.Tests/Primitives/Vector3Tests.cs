using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Vector3Tests
    {
        // --- コンストラクタ ---------------------------------------------

        [Test(Description = "3 つの値を指定するコンストラクタが正しく初期化されることを確認")]
        public void Constructor_SetsXYZCorrectly()
        {
            var v = new Vector3(1f, 2f, 3f);

            Assert.AreEqual(1f, v.X);
            Assert.AreEqual(2f, v.Y);
            Assert.AreEqual(3f, v.Z);
        }

        [Test(Description = "1 つの値で全成分が同じ値に初期化されることを確認")]
        public void Constructor_SetsSameValue()
        {
            var v = new Vector3(5f);

            Assert.AreEqual(5f, v.X);
            Assert.AreEqual(5f, v.Y);
            Assert.AreEqual(5f, v.Z);
        }

        [Test(Description = "Vector2 から X,Y をコピーし Z が 0 で初期化されることを確認")]
        public void Constructor_FromVector2_CopiesXY()
        {
            var v2 = new Vector2(3f, 4f);
            var v = new Vector3(v2);

            Assert.AreEqual(3f, v.X);
            Assert.AreEqual(4f, v.Y);
            Assert.AreEqual(0f, v.Z);
        }

        [Test(Description = "Vector2 から X,Y をコピーし Z に指定値を設定できることを確認")]
        public void Constructor_FromVector2_WithZ()
        {
            var v2 = new Vector2(3f, 4f);
            var v = new Vector3(v2, 9f);

            Assert.AreEqual(3f, v.X);
            Assert.AreEqual(4f, v.Y);
            Assert.AreEqual(9f, v.Z);
        }

        // --- プロパティ -------------------------------------------------

        [Test(Description = "Length プロパティが 3D ベクトルの長さを正しく計算することを確認")]
        public void Length_ComputesCorrectly()
        {
            var v = new Vector3(2f, 3f, 6f);
            Assert.AreEqual(7f, v.Length, 1e-6);
        }

        [Test(Description = "LengthSquared が長さの二乗を返すことを確認")]
        public void LengthSquared_ComputesCorrectly()
        {
            var v = new Vector3(2f, 3f, 6f);
            Assert.AreEqual(49f, v.LengthSquared);
        }

        [Test(Description = "Normalized が単位ベクトルを返すことを確認")]
        public void Normalized_ReturnsUnitVector()
        {
            var v = new Vector3(1f, 2f, 2f);
            var n = v.Normalized;

            Assert.That(n.Length, Is.EqualTo(1f).Within(1e-6));
        }

        [Test(Description = "ゼロベクトルの Normalized がゼロベクトルを返すことを確認")]
        public void Normalized_ZeroVector_ReturnsZero()
        {
            var n = Vector3.Zero.Normalized;
            Assert.AreEqual(Vector3.Zero, n);
        }

        [Test(Description = "Xy プロパティが X,Y を正しく反映することを確認")]
        public void XyProperty_WorksCorrectly()
        {
            var v = new Vector3(3f, 4f, 5f);
            var xy = v.Xy;

            Assert.AreEqual(3f, xy.X);
            Assert.AreEqual(4f, xy.Y);

            v.Xy = new Vector2(9f, 8f);
            Assert.AreEqual(9f, v.X);
            Assert.AreEqual(8f, v.Y);
        }

        // --- インデクサ ---------------------------------------------------

        [Test(Description = "インデクサが各成分にアクセスできることを確認")]
        public void Indexer_GetsAndSetsCorrectly()
        {
            var v = new Vector3(1f, 2f, 3f);

            Assert.AreEqual(1f, v[0]);
            Assert.AreEqual(2f, v[1]);
            Assert.AreEqual(3f, v[2]);

            v[0] = 10f;
            v[1] = 20f;
            v[2] = 30f;

            Assert.AreEqual(10f, v.X);
            Assert.AreEqual(20f, v.Y);
            Assert.AreEqual(30f, v.Z);
        }

        [Test(Description = "不正なインデックスが例外を投げることを確認")]
        public void Indexer_ThrowsOnInvalidIndex()
        {
            var v = new Vector3();

            Assert.Throws<IndexOutOfRangeException>(() => { var _ = v[3]; });
            Assert.Throws<IndexOutOfRangeException>(() => v[3] = 10f);
        }

        // --- メソッド -----------------------------------------------------

        [Test(Description = "Normalize メソッドがベクトル自身を正規化することを確認")]
        public void Normalize_WorksCorrectly()
        {
            var v = new Vector3(1f, 2f, 2f);
            v.Normalize();

            Assert.That(v.Length, Is.EqualTo(1f).Within(1e-6));
        }

        [Test(Description = "Normalize がゼロベクトルを変更しないことを確認")]
        public void Normalize_ZeroVector()
        {
            var v = Vector3.Zero;
            v.Normalize();
            Assert.AreEqual(Vector3.Zero, v);
        }

        // --- Dot / Cross ---------------------------------------------------

        [Test(Description = "Dot が内積を正しく計算することを確認")]
        public void Dot_ComputesCorrectly()
        {
            var a = new Vector3(1, 3, -5);
            var b = new Vector3(4, -2, -1);

            Assert.AreEqual(3, Vector3.Dot(a, b));
        }

        [Test(Description = "Cross が外積を正しく計算することを確認")]
        public void Cross_ComputesCorrectly()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);

            var c = Vector3.Cross(a, b);

            Assert.AreEqual(new Vector3(0, 0, 1), c);
        }

        // --- Lerp ----------------------------------------------------------

        [Test(Description = "Lerp が線形補間を正しく計算することを確認")]
        public void Lerp_ComputesCorrectly()
        {
            var a = new Vector3(0, 0, 0);
            var b = new Vector3(10, 20, 30);

            var r = Vector3.Lerp(a, b, 0.5f);

            Assert.AreEqual(new Vector3(5, 10, 15), r);
        }

        // --- Angle ---------------------------------------------------------

        [Test(Description = "AngleBetween が 2 つのベクトルの角度を正しく計算することを確認")]
        public void AngleBetween_ComputesCorrectly()
        {
            var a = new Vector3(1, 0, 0);
            var b = new Vector3(0, 1, 0);

            var angle = Vector3.AngleBetween(a, b);

            Assert.That(angle, Is.EqualTo(Math.PI / 2).Within(1e-6));
        }

        // --- Transform (Matrix4) -------------------------------------------

        [Test(Description = "Matrix4 による変換が正しく行われることを確認")]
        public void Transform_Matrix4_Works()
        {
            var v = new Vector3(1, 2, 3);

            var m = Matrix4.CreateTranslation(10, 20, 30);

            var r = Vector3.Transform(v, m);

            Assert.AreEqual(new Vector3(11, 22, 33), r);
        }

        // --- Transform (Quaternion) ----------------------------------------

        [Test(Description = "Quaternion による回転が正しく行われることを確認")]
        public void Transform_Quaternion_Works()
        {
            var v = new Vector3(1, 0, 0);

            var q = Quaternion.FromAxisAngle(new Vector3(0, 0, 1), (float)Math.PI / 2);

            var r = Vector3.Transform(v, q);

            Assert.That(r.X, Is.EqualTo(0).Within(1e-5));
            Assert.That(r.Y, Is.EqualTo(1).Within(1e-5));
        }

        // --- 演算子 --------------------------------------------------------

        [Test(Description = "加算演算子が正しく動作することを確認")]
        public void Operator_Addition()
        {
            var a = new Vector3(1, 2, 3);
            var b = new Vector3(4, 5, 6);

            Assert.AreEqual(new Vector3(5, 7, 9), a + b);
        }

        [Test(Description = "減算演算子が正しく動作することを確認")]
        public void Operator_Subtraction()
        {
            var a = new Vector3(5, 7, 9);
            var b = new Vector3(1, 2, 3);

            Assert.AreEqual(new Vector3(4, 5, 6), a - b);
        }

        [Test(Description = "単項マイナス演算子が正しく動作することを確認")]
        public void Operator_Negate()
        {
            var a = new Vector3(1, -2, 3);

            Assert.AreEqual(new Vector3(-1, 2, -3), -a);
        }

        [Test(Description = "スカラー倍演算子が正しく動作することを確認")]
        public void Operator_Multiply_Scalar()
        {
            var a = new Vector3(1, 2, 3);

            Assert.AreEqual(new Vector3(2, 4, 6), a * 2f);
        }

        [Test(Description = "スカラー除算演算子が正しく動作することを確認")]
        public void Operator_Divide_Scalar()
        {
            var a = new Vector3(2, 4, 6);

            Assert.AreEqual(new Vector3(1, 2, 3), a / 2f);
        }

        [Test(Description = "等値演算子が正しく動作することを確認")]
        public void Operator_Equality()
        {
            var a = new Vector3(1, 2, 3);
            var b = new Vector3(1, 2, 3);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test(Description = "ToString が正しい形式の文字列を返すことを確認")]
        public void ToString_ReturnsExpectedFormat()
        {
            var v = new Vector3(1, 2, 3);

            Assert.AreEqual("(1, 2, 3)", v.ToString());
        }
    }
}
