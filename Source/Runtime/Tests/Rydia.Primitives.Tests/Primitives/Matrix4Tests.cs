using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Matrix4Tests
    {

        [Test(Description = "Identity プロパティが正しい単位行列を返すことを確認")]
        public void Identity_ShouldBeIdentityMatrix()
        {
            Matrix4 identity = Matrix4.Identity;

            Assert.AreEqual(1, identity.M11);
            Assert.AreEqual(1, identity.M22);
            Assert.AreEqual(1, identity.M33);
            Assert.AreEqual(1, identity.M44);

            Assert.AreEqual(0, identity.M12);
            Assert.AreEqual(0, identity.M13);
            Assert.AreEqual(0, identity.M14);
            Assert.AreEqual(0, identity.M21);
            Assert.AreEqual(0, identity.M23);
            Assert.AreEqual(0, identity.M24);
            Assert.AreEqual(0, identity.M31);
            Assert.AreEqual(0, identity.M32);
            Assert.AreEqual(0, identity.M34);
            Assert.AreEqual(0, identity.M41);
            Assert.AreEqual(0, identity.M42);
            Assert.AreEqual(0, identity.M43);
        }

        [Test(Description = "Translation 行列が正しく移動ベクトルを反映することを確認")]
        public void Translation_ShouldMoveCorrectly()
        {
            Matrix4 translation = Matrix4.CreateTranslation(1, 2, 3);

            Vector3 extracted = translation.ExtractTranslation();
            Assert.AreEqual(1, extracted.X);
            Assert.AreEqual(2, extracted.Y);
            Assert.AreEqual(3, extracted.Z);
        }

        [Test(Description = "Scale 行列が正しくスケール値を反映することを確認")]
        public void Scale_ShouldScaleCorrectly()
        {
            Matrix4 scale = Matrix4.CreateScale(2, 3, 4);
            Vector3 extracted = scale.ExtractScale();

            Assert.AreEqual(2, extracted.X);
            Assert.AreEqual(3, extracted.Y);
            Assert.AreEqual(4, extracted.Z);
        }

        [Test(Description = "行列の乗算によりスケールと移動を正しく組み合わせられることを確認")]
        public void Multiply_ShouldCombineTransformations()
        {
            Matrix4 scale = Matrix4.CreateScale(2, 2, 2);
            Matrix4 translation = Matrix4.CreateTranslation(1, 1, 1);

            Matrix4 result = Matrix4.Mult(translation, scale);
            Vector3 extractedTranslation = result.ExtractTranslation();
            Vector3 extractedScale = result.ExtractScale();

            Assert.AreEqual(2, extractedScale.X);
            Assert.AreEqual(2, extractedScale.Y);
            Assert.AreEqual(2, extractedScale.Z);

            Assert.AreEqual(2, extractedTranslation.X); // scale affects translation
            Assert.AreEqual(2, extractedTranslation.Y);
            Assert.AreEqual(2, extractedTranslation.Z);
        }

        [Test(Description = "逆行列の計算が正しく、掛け合わせると単位行列になることを確認")]
        public void Inverse_ShouldReturnIdentityWhenMultiplied()
        {
            Matrix4 translation = Matrix4.CreateTranslation(5, -3, 2);
            Matrix4 inv = translation.Inverted();

            Matrix4 result = Matrix4.Mult(translation, inv);

            Assert.AreEqual(Matrix4.Identity, result);
        }

        [Test(Description = "行列の転置が正しく行われ、行と列が入れ替わることを確認")]
        public void Transpose_ShouldSwapRowsAndColumns()
        {
            Matrix4 mat = Matrix4.CreateTranslation(1, 2, 3);
            Matrix4 transposed = mat;
            transposed.Transpose();

            Assert.AreEqual(mat.M12, transposed.M21);
            Assert.AreEqual(mat.M13, transposed.M31);
            Assert.AreEqual(mat.M14, transposed.M41);
        }

        [Test(Description = "行列の行列式が正しく計算されることを確認")]
        public void Determinant_ShouldBeCorrect()
        {
            Matrix4 scale = Matrix4.CreateScale(2, 3, 4);
            float det = scale.Determinant;

            Assert.AreEqual(24, det); // 2*3*4
        }

    }
}
