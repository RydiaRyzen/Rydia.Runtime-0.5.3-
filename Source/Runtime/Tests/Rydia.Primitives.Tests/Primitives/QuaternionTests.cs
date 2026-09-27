using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class QuaternionTests
    {

        [Test(Description = "Identityクォータニオンの値が正しいことを確認")]
        public void Identity_ReturnsCorrectValues()
        {
            var identity = Quaternion.Identity;
            Assert.AreEqual(Vector3.Zero, identity.Xyz);
            Assert.AreEqual(1f, identity.W);
        }

        [Test(Description = "コンストラクタでXYZとWが正しく設定されることを確認")]
        public void Constructor_VectorAndW_PropertiesSet()
        {
            var v = new Vector3(1f, 2f, 3f);
            var q = new Quaternion(v, 4f);
            Assert.AreEqual(v, q.Xyz);
            Assert.AreEqual(4f, q.W);
        }

        [Test(Description = "コンストラクタでX, Y, Z, Wが正しく設定されることを確認")]
        public void Constructor_XYZW_PropertiesSet()
        {
            var q = new Quaternion(1f, 2f, 3f, 4f);
            Assert.AreEqual(new Vector3(1f, 2f, 3f), q.Xyz);
            Assert.AreEqual(4f, q.W);
        }

        [Test(Description = "LengthとLengthSquaredが正しい値を返すことを確認")]
        public void LengthAndLengthSquared_ReturnCorrectValues()
        {
            var q = new Quaternion(1f, 2f, 3f, 4f);
            Assert.AreEqual(MathF.Sqrt(1 + 4 + 9 + 16), q.Length, 0.0001f);
            Assert.AreEqual(1 + 4 + 9 + 16, q.LengthSquared, 0.0001f);
        }

        [Test(Description = "Normalizeで単位長さに正規化されることを確認")]
        public void Normalize_SetsLengthToOne()
        {
            var q = new Quaternion(1f, 2f, 3f, 4f);
            q.Normalize();
            Assert.AreEqual(1f, q.Length, 0.0001f);
        }

        [Test(Description = "ConjugateでXYZが反転することを確認")]
        public void Conjugate_InvertsXYZ()
        {
            var q = new Quaternion(1f, -2f, 3f, 4f);
            q.Conjugate();
            Assert.AreEqual(new Vector3(-1f, 2f, -3f), q.Xyz);
            Assert.AreEqual(4f, q.W);
        }

        [Test(Description = "InvertでWが反転することを確認")]
        public void Invert_NegatesW()
        {
            var q = new Quaternion(1f, 2f, 3f, 4f);
            q.Invert();
            Assert.AreEqual(-4f, q.W);
            Assert.AreEqual(new Vector3(1f, 2f, 3f), q.Xyz);
        }

        [Test(Description = "加算演算子が正しく動作することを確認")]
        public void Operator_Add_Works()
        {
            var a = new Quaternion(1f, 2f, 3f, 4f);
            var b = new Quaternion(4f, 3f, 2f, 1f);
            var sum = a + b;
            Assert.AreEqual(new Quaternion(5f, 5f, 5f, 5f), sum);
        }

        [Test(Description = "減算演算子が正しく動作することを確認")]
        public void Operator_Subtract_Works()
        {
            var a = new Quaternion(5f, 5f, 5f, 5f);
            var b = new Quaternion(1f, 2f, 3f, 4f);
            var diff = a - b;
            Assert.AreEqual(new Quaternion(4f, 3f, 2f, 1f), diff);
        }

        [Test(Description = "スカラー乗算が正しく動作することを確認")]
        public void Operator_MultiplyScalar_Works()
        {
            var q = new Quaternion(1f, 2f, 3f, 4f);
            var result = q * 2f;
            Assert.AreEqual(new Quaternion(2f, 4f, 6f, 8f), result);
        }

        [Test(Description = "等価演算子で正しい結果が返ることを確認")]
        public void Operator_Equality_Works()
        {
            var a = new Quaternion(1f, 2f, 3f, 4f);
            var b = new Quaternion(1f, 2f, 3f, 4f);
            var c = new Quaternion(0f, 0f, 0f, 1f);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test(Description = "ToAxisAngleで角度と軸が取得できることを確認")]
        public void ToAxisAngle_ReturnsAxisAndAngle()
        {
            var axis = Vector3.UnitY;
            float angle = MathF.PI / 2;
            var q = Quaternion.FromAxisAngle(axis, angle);

            q.ToAxisAngle(out Vector3 resultAxis, out float resultAngle);
            Assert.AreEqual(axis.X, resultAxis.X, 0.0001f);
            Assert.AreEqual(axis.Y, resultAxis.Y, 0.0001f);
            Assert.AreEqual(axis.Z, resultAxis.Z, 0.0001f);
            Assert.AreEqual(angle, resultAngle, 0.0001f);
        }

        [Test(Description = "Slerpで2つのクォータニオン間を補間できることを確認")]
        public void Slerp_InterpolatesCorrectly()
        {
            var q1 = Quaternion.Identity;
            var q2 = Quaternion.FromAxisAngle(Vector3.UnitY, MathF.PI);
            var mid = Quaternion.Slerp(q1, q2, 0.5f);

            Assert.AreEqual(1f, mid.Length, 0.0001f);
        }

        [Test(Description = "FromAxisAngleで単位軸の回転が生成されることを確認")]
        public void FromAxisAngle_CreatesQuaternion()
        {
            var axis = Vector3.UnitX;
            var angle = MathF.PI;
            var q = Quaternion.FromAxisAngle(axis, angle);
            Assert.AreEqual(1f, q.Length, 0.0001f);
        }
    }
}
