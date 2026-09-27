using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Vector4Tests
    {

        [Test(Description = "コンストラクタが正しく値を設定する")]
        public void Constructor_SetsValuesCorrectly()
        {
            var v = new Vector4(1f, 2f, 3f, 4f);
            Assert.AreEqual(1f, v.X);
            Assert.AreEqual(2f, v.Y);
            Assert.AreEqual(3f, v.Z);
            Assert.AreEqual(4f, v.W);
        }

        [Test(Description = "単一値コンストラクタが全成分を同じ値に設定する")]
        public void Constructor_SingleValue_SetsAllComponents()
        {
            var v = new Vector4(5f);
            Assert.AreEqual(5f, v.X);
            Assert.AreEqual(5f, v.Y);
            Assert.AreEqual(5f, v.Z);
            Assert.AreEqual(5f, v.W);
        }

        [Test(Description = "Vector2 と Vector3 のコンストラクタが正しく構築される")]
        public void Constructor_FromVector2_And_Vector3()
        {
            var v2 = new Vector2(1f, 2f);
            var v3 = new Vector3(3f, 4f, 5f);

            var vec2 = new Vector4(v2);
            Assert.AreEqual(1f, vec2.X);
            Assert.AreEqual(2f, vec2.Y);
            Assert.AreEqual(0f, vec2.Z);
            Assert.AreEqual(0f, vec2.W);

            var vec3 = new Vector4(v3);
            Assert.AreEqual(3f, vec3.X);
            Assert.AreEqual(4f, vec3.Y);
            Assert.AreEqual(5f, vec3.Z);
            Assert.AreEqual(0f, vec3.W);
        }

        [Test(Description = "インデクサが正しく動作する")]
        public void Indexer_GetsAndSetsCorrectly()
        {
            var v = new Vector4(1f, 2f, 3f, 4f);

            Assert.AreEqual(1f, v[0]);
            Assert.AreEqual(2f, v[1]);
            Assert.AreEqual(3f, v[2]);
            Assert.AreEqual(4f, v[3]);

            v[0] = 10f;
            v[1] = 20f;
            v[2] = 30f;
            v[3] = 40f;

            Assert.AreEqual(10f, v.X);
            Assert.AreEqual(20f, v.Y);
            Assert.AreEqual(30f, v.Z);
            Assert.AreEqual(40f, v.W);
        }

        [Test(Description = "長さと長さの二乗が正しく計算される")]
        public void Length_CalculationsCorrect()
        {
            var v = new Vector4(2f, 3f, 6f, 1f);

            Assert.AreEqual(2f * 2f + 3f * 3f + 6f * 6f + 1f * 1f, v.LengthSquared);
            Assert.AreEqual(MathF.Sqrt(v.LengthSquared), v.Length, 1e-6f);
        }

        [Test(Description = "正規化が正しく行われる")]
        public void Normalize_WorksCorrectly()
        {
            var v = new Vector4(3f, 4f, 0f, 0f);
            var n = v.Normalized;

            Assert.AreEqual(1.0f, n.Length, 1e-6f);
            Assert.AreEqual(3f / 5f, n.X, 1e-6f);
            Assert.AreEqual(4f / 5f, n.Y, 1e-6f);
        }

        [Test(Description = "ゼロベクトルを正規化したときゼロが返る")]
        public void Normalize_ZeroVector_ReturnsZero()
        {
            var v = new Vector4(0f, 0f, 0f, 0f);
            var n = v.Normalized;

            Assert.AreEqual(Vector4.Zero, n);
        }

        [Test(Description = "加算演算子が正しく動作する")]
        public void Operator_Add_WorksCorrectly()
        {
            var a = new Vector4(1f, 2f, 3f, 4f);
            var b = new Vector4(5f, 6f, 7f, 8f);
            var r = a + b;

            Assert.AreEqual(new Vector4(6f, 8f, 10f, 12f), r);
        }

        [Test(Description = "減算演算子が正しく動作する")]
        public void Operator_Subtract_WorksCorrectly()
        {
            var a = new Vector4(5f, 7f, 9f, 11f);
            var b = new Vector4(1f, 2f, 3f, 4f);
            var r = a - b;

            Assert.AreEqual(new Vector4(4f, 5f, 6f, 7f), r);
        }

        [Test(Description = "スカラー乗算が正しく動作する")]
        public void Operator_MultiplyScalar_WorksCorrectly()
        {
            var v = new Vector4(1f, 2f, 3f, 4f);
            var r = v * 2f;

            Assert.AreEqual(new Vector4(2f, 4f, 6f, 8f), r);
        }

        [Test(Description = "スカラー除算が正しく動作する")]
        public void Operator_DivideScalar_WorksCorrectly()
        {
            var v = new Vector4(2f, 4f, 6f, 8f);
            var r = v / 2f;

            Assert.AreEqual(new Vector4(1f, 2f, 3f, 4f), r);
        }

        [Test(Description = "ドット積が正しく計算される")]
        public void Dot_WorksCorrectly()
        {
            var a = new Vector4(1f, 3f, -5f, 2f);
            var b = new Vector4(4f, -2f, -1f, 1f);

            Assert.AreEqual(1f * 4f + 3f * -2f + -5f * -1f + 2f * 1f, Vector4.Dot(a, b));
        }

        [Test(Description = "線形補間が正しく動作する")]
        public void Lerp_WorksCorrectly()
        {
            var a = new Vector4(0f, 0f, 0f, 0f);
            var b = new Vector4(10f, 10f, 10f, 10f);

            var r = Vector4.Lerp(a, b, 0.25f);

            Assert.AreEqual(new Vector4(2.5f, 2.5f, 2.5f, 2.5f), r);
        }

        [Test(Description = "等価比較が正しく動作する")]
        public void Equality_WorksCorrectly()
        {
            var a = new Vector4(1f, 2f, 3f, 4f);
            var b = new Vector4(1f, 2f, 3f, 4f);
            var c = new Vector4(5f, 6f, 7f, 8f);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test(Description = "ToString が正しい形式の文字列を返す")]
        public void ToString_WorksCorrectly()
        {
            var v = new Vector4(1f, 2f, 3f, 4f);
            Assert.AreEqual("(1, 2, 3, 4)", v.ToString());
        }

    }
}
