using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Vector2Tests
    {

        [Test(Description = "コンストラクタが指定した値で初期化されることを確認")]
        public void Constructor_SetsValuesCorrectly()
        {
            var v = new Vector2(3f, 4f);
            Assert.AreEqual(3f, v.X);
            Assert.AreEqual(4f, v.Y);
        }

        [Test(Description = "同一の値で X と Y が設定されるコンストラクタの動作を確認")]
        public void Constructor_SetsSameValueForXandY()
        {
            var v = new Vector2(5f);
            Assert.AreEqual(5f, v.X);
            Assert.AreEqual(5f, v.Y);
        }

        [Test(Description = "Length プロパティが正しい長さを返すことを確認")]
        public void Length_ComputesCorrectly()
        {
            var v = new Vector2(3f, 4f);
            Assert.AreEqual(5f, v.Length);
        }

        [Test(Description = "LengthSquared プロパティが長さの二乗を正しく返すことを確認")]
        public void LengthSquared_ComputesCorrectly()
        {
            var v = new Vector2(3f, 4f);
            Assert.AreEqual(25f, v.LengthSquared);
        }

        [Test(Description = "Normalized が正規化されたベクトルを返すことを確認")]
        public void Normalized_ReturnsUnitVector()
        {
            var v = new Vector2(3f, 4f);
            var n = v.Normalized;

            Assert.That(n.Length, Is.EqualTo(1f).Within(1e-5f));
        }

        [Test(Description = "ゼロベクトルの Normalized がゼロベクトルを返すことを確認")]
        public void Normalized_ZeroVector_ReturnsZero()
        {
            var v = Vector2.Zero;
            var n = v.Normalized;

            Assert.AreEqual(Vector2.Zero, n);
        }

        [Test(Description = "Normalize メソッドがベクトル自身を正規化することを確認")]
        public void Normalize_ModifiesVectorInPlace()
        {
            var v = new Vector2(3f, 4f);
            v.Normalize();

            Assert.That(v.Length, Is.EqualTo(1f).Within(1e-5f));
        }

        [Test(Description = "インデクサが正しく値を取得・設定できることを確認")]
        public void Indexer_GetsAndSetsCorrectly()
        {
            var v = new Vector2(1f, 2f);

            Assert.AreEqual(1f, v[0]);
            Assert.AreEqual(2f, v[1]);

            v[0] = 10f;
            v[1] = 20f;

            Assert.AreEqual(10f, v.X);
            Assert.AreEqual(20f, v.Y);
        }

        [Test(Description = "インデクサが不正なインデックスで例外を投げることを確認")]
        public void Indexer_ThrowsOnInvalidIndex()
        {
            var v = new Vector2(1f, 2f);

            Assert.Throws<IndexOutOfRangeException>(() => { var x = v[2]; });
            Assert.Throws<IndexOutOfRangeException>(() => v[2] = 10f);
        }

        [Test(Description = "加算演算子が正しく計算されることを確認")]
        public void Operator_Addition_WorksCorrectly()
        {
            var a = new Vector2(1, 2);
            var b = new Vector2(3, 4);

            Assert.AreEqual(new Vector2(4, 6), a + b);
        }

        [Test(Description = "減算演算子が正しく計算されることを確認")]
        public void Operator_Subtraction_WorksCorrectly()
        {
            var a = new Vector2(5, 7);
            var b = new Vector2(2, 3);

            Assert.AreEqual(new Vector2(3, 4), a - b);
        }

        [Test(Description = "符号反転演算子が正しく動作することを確認")]
        public void Operator_UnaryNegation_WorksCorrectly()
        {
            var a = new Vector2(2, -3);

            Assert.AreEqual(new Vector2(-2, 3), -a);
        }

        [Test(Description = "スカラー倍演算子が正しく計算されることを確認")]
        public void Operator_MultiplyByScalar_WorksCorrectly()
        {
            var a = new Vector2(2, 3);

            Assert.AreEqual(new Vector2(4, 6), a * 2f);
        }

        [Test(Description = "スカラー除算演算子が正しく計算されることを確認")]
        public void Operator_DivideByScalar_WorksCorrectly()
        {
            var a = new Vector2(4, 6);

            Assert.AreEqual(new Vector2(2, 3), a / 2f);
        }

        [Test(Description = "Dot が内積を正しく計算することを確認")]
        public void Dot_ComputesCorrectly()
        {
            var a = new Vector2(1, 3);
            var b = new Vector2(4, -2);

            Assert.AreEqual(-2, Vector2.Dot(a, b));
        }

        [Test(Description = "AngleBetween が 2 つのベクトル間の角度を正しく計算することを確認")]
        public void AngleBetween_ComputesCorrectly()
        {
            var a = new Vector2(1, 0);
            var b = new Vector2(0, 1);

            var angle = Vector2.AngleBetween(a, b);

            Assert.That(angle, Is.EqualTo(MathF.PI / 2).Within(1e-5f));
        }

        [Test(Description = "Lerp が線形補間を正しく計算することを確認")]
        public void Lerp_ReturnsCorrectValue()
        {
            var a = new Vector2(0, 0);
            var b = new Vector2(10, 20);

            var r = Vector2.Lerp(a, b, 0.5f);

            Assert.AreEqual(new Vector2(5, 10), r);
        }

        [Test(Description = "等値演算子が正しく動作することを確認")]
        public void EqualityOperator_WorksCorrectly()
        {
            var a = new Vector2(2, 3);
            var b = new Vector2(2, 3);
            var c = new Vector2(2, 4);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
        }

        [Test(Description = "不等値演算子が正しく動作することを確認")]
        public void InequalityOperator_WorksCorrectly()
        {
            var a = new Vector2(2, 3);
            var b = new Vector2(3, 3);

            Assert.IsTrue(a != b);
        }

        [Test(Description = "ToString が指定形式の文字列を返すことを確認")]
        public void ToString_ReturnsExpectedFormat()
        {
            var a = new Vector2(1.23456f, 7.89012f);

            Assert.AreEqual("(1.23, 7.89)", a.ToString());
        }
    }
}
