using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Matrix2Tests
    {

        [Test(Description = "コンストラクタとプロパティの動作を確認する")]
        public void Constructor_And_Properties_Test()
        {
            var m = new Matrix2(1, 2, 3, 4);

            Assert.AreEqual(1, m.M11);
            Assert.AreEqual(2, m.M12);
            Assert.AreEqual(3, m.M21);
            Assert.AreEqual(4, m.M22);

            // Column check
            Assert.AreEqual(new Vector2(1, 3), m.Column0);
            Assert.AreEqual(new Vector2(2, 4), m.Column1);

            // Diagonal check
            Assert.AreEqual(new Vector2(1, 4), m.Diagonal);
            Assert.AreEqual(5, m.Trace);
        }

        [Test(Description = "行列の加算が正しく動作することを確認する")]
        public void Addition_Test()
        {
            var a = new Matrix2(1, 2, 3, 4);
            var b = new Matrix2(4, 3, 2, 1);
            var c = a + b;

            Assert.AreEqual(new Matrix2(5, 5, 5, 5), c);
        }

        [Test(Description = "行列の減算が正しく動作することを確認する")]
        public void Subtraction_Test()
        {
            var a = new Matrix2(5, 5, 5, 5);
            var b = new Matrix2(1, 2, 3, 4);
            var c = a - b;

            Assert.AreEqual(new Matrix2(4, 3, 2, 1), c);
        }

        [Test(Description = "スカラー乗算が正しく動作することを確認する")]
        public void ScalarMultiplication_Test()
        {
            var a = new Matrix2(1, 2, 3, 4);
            var b = a * 2f;
            var c = 2f * a;

            Assert.AreEqual(new Matrix2(2, 4, 6, 8), b);
            Assert.AreEqual(new Matrix2(2, 4, 6, 8), c);
        }

        [Test(Description = "行列乗算が正しく動作することを確認する")]
        public void MatrixMultiplication_Test()
        {
            var a = new Matrix2(1, 2, 3, 4);
            var b = new Matrix2(2, 0, 1, 2);
            var c = a * b;

            // 手計算: [1*2+2*1, 1*0+2*2; 3*2+4*1, 3*0+4*2] = [4,4;10,8]
            Assert.AreEqual(new Matrix2(4, 4, 10, 8), c);
        }

        [Test(Description = "転置が正しく動作することを確認する")]
        public void Transpose_Test()
        {
            var m = new Matrix2(1, 2, 3, 4);
            var t = Matrix2.Transpose(m);

            Assert.AreEqual(new Matrix2(1, 3, 2, 4), t);
        }

        [Test(Description = "逆行列が正しく計算されることを確認する")]
        public void Invert_Test()
        {
            var m = new Matrix2(4, 7, 2, 6);
            var inv = Matrix2.Invert(m);

            // 逆行列の手計算: 1/det * [6, -7; -2, 4], det=4*6-7*2=10
            var expected = new Matrix2(0.6f, -0.7f, -0.2f, 0.4f);

            Assert.AreEqual(expected.M11, inv.M11, 1e-6);
            Assert.AreEqual(expected.M12, inv.M12, 1e-6);
            Assert.AreEqual(expected.M21, inv.M21, 1e-6);
            Assert.AreEqual(expected.M22, inv.M22, 1e-6);
        }

        [Test(Description = "回転行列が正しく生成されることを確認する")]
        public void CreateRotation_Test()
        {
            float angle = (float)Math.PI / 2; // 90度
            var rot = Matrix2.CreateRotation(angle);

            // 90度回転行列: [0,1;-1,0]
            Assert.AreEqual(0f, rot.M11, 1e-6);
            Assert.AreEqual(1f, rot.M12, 1e-6);
            Assert.AreEqual(-1f, rot.M21, 1e-6);
            Assert.AreEqual(0f, rot.M22, 1e-6);
        }

        [Test(Description = "スケーリング行列が正しく生成されることを確認する")]
        public void CreateScale_Test()
        {
            var scale = Matrix2.CreateScale(2f, 3f);
            Assert.AreEqual(2f, scale.M11);
            Assert.AreEqual(0f, scale.M12);
            Assert.AreEqual(0f, scale.M21);
            Assert.AreEqual(3f, scale.M22);
        }

        [Test(Description = "Equals および == 演算子が正しく動作することを確認する")]
        public void Equals_Test()
        {
            var a = new Matrix2(1, 2, 3, 4);
            var b = new Matrix2(1, 2, 3, 4);
            var c = new Matrix2(4, 3, 2, 1);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
        }

    }
}
