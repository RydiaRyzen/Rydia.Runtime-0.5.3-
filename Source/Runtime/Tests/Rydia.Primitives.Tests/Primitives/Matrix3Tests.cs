using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Matrix3Tests
    {

        [Test(Description = "Identity行列が正しく初期化されていることを確認")]
        public void TestIdentityMatrix()
        {
            var identity = Matrix3.Identity;
            Assert.AreEqual(1f, identity.M11);
            Assert.AreEqual(1f, identity.M22);
            Assert.AreEqual(1f, identity.M33);
            Assert.AreEqual(0f, identity.M12);
            Assert.AreEqual(0f, identity.M21);
        }

        [Test(Description = "Zero行列が正しく初期化されていることを確認")]
        public void TestZeroMatrix()
        {
            var zero = Matrix3.Zero;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.AreEqual(0f, zero[i, j]);
        }

        [Test(Description = "コンストラクタでの行指定初期化が正しく動作することを確認")]
        public void TestConstructorWithRows()
        {
            var row0 = new Vector3(1, 2, 3);
            var row1 = new Vector3(4, 5, 6);
            var row2 = new Vector3(7, 8, 9);
            var mat = new Matrix3(row0, row1, row2);
            Assert.AreEqual(row0, mat.Row0);
            Assert.AreEqual(row1, mat.Row1);
            Assert.AreEqual(row2, mat.Row2);
        }

        [Test(Description = "インデクサで要素にアクセスできることを確認")]
        public void TestIndexer()
        {
            var mat = Matrix3.Identity;
            Assert.AreEqual(1f, mat[0, 0]);
            mat[0, 0] = 42f;
            Assert.AreEqual(42f, mat.M11);
        }

        [Test(Description = "行列の転置が正しく計算されることを確認")]
        public void TestTranspose()
        {
            var mat = new Matrix3(
                1, 2, 3,
                4, 5, 6,
                7, 8, 9
            );
            var transposed = Matrix3.Transpose(mat);
            Assert.AreEqual(1f, transposed.M11);
            Assert.AreEqual(4f, transposed.M12);
            Assert.AreEqual(7f, transposed.M13);
            Assert.AreEqual(2f, transposed.M21);
            Assert.AreEqual(5f, transposed.M22);
            Assert.AreEqual(8f, transposed.M23);
            Assert.AreEqual(3f, transposed.M31);
            Assert.AreEqual(6f, transposed.M32);
            Assert.AreEqual(9f, transposed.M33);
        }

        [Test(Description = "行列の乗算が正しく動作することを確認")]
        public void TestMultiplication()
        {
            var a = new Matrix3(
                1, 2, 3,
                0, 1, 4,
                5, 6, 0
            );
            var b = new Matrix3(
                -2, 1, 0,
                3, 0, 0,
                4, -1, 0
            );
            var result = a * b;
            Assert.AreEqual(16f, result.M11);
            Assert.AreEqual(-2f, result.M12);
            Assert.AreEqual(0f, result.M13);
            Assert.AreEqual(19f, result.M21);
            Assert.AreEqual(-4f, result.M22);
            Assert.AreEqual(0f, result.M23);
            Assert.AreEqual(8f, result.M31);
            Assert.AreEqual(5f, result.M32);
            Assert.AreEqual(0f, result.M33);
        }

        [Test(Description = "行列の逆行列計算が正しく動作することを確認")]
        public void TestInvert()
        {
            var mat = new Matrix3(
                4, 7, 2,
                3, 6, 1,
                2, 5, 1
            );
            var inv = mat.Inverted();
            var identity = mat * inv;
            // 小数の誤差を考慮して近似チェック
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    Assert.That(identity[i, j], Is.EqualTo(Matrix3.Identity[i, j]).Within(0.0001));
        }

        [Test(Description = "行列のスケール抽出が正しく動作することを確認")]
        public void TestExtractScale()
        {
            var mat = Matrix3.CreateScale(2, 3, 4);
            var scale = mat.ExtractScale();
            Assert.AreEqual(new Vector3(2, 3, 4), scale);
        }

        [Test(Description = "行列の回転抽出が正しく動作することを確認")]
        public void TestExtractRotation()
        {
            var axis = new Vector3(0, 0, 1);
            var angle = (float)Math.PI / 2;
            var rotMat = Matrix3.CreateFromAxisAngle(axis, angle);
            var q = rotMat.ExtractRotation();
            Vector3 extractedAxis;
            float extractedAngle;
            q.ToAxisAngle(out extractedAxis, out extractedAngle);
            Assert.That(Math.Abs(extractedAngle - angle), Is.LessThan(0.0001));
            Assert.That((extractedAxis - axis).Length, Is.LessThan(0.0001));
        }

        [Test(Description = "Equalsおよび演算子==が正しく動作することを確認")]
        public void TestEquals()
        {
            var a = Matrix3.Identity;
            var b = Matrix3.Identity;
            var c = Matrix3.Zero;
            Assert.IsTrue(a.Equals(b));
            Assert.IsTrue(a == b);
            Assert.IsFalse(a.Equals(c));
            Assert.IsTrue(a != c);
        }

    }
}
