using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class AlignmentTests
    {

        [Test(Description = "Center アラインメントが正しく適用されることを確認")]
        public void ApplyTo_Center_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.Center.ApplyTo(ref vec, ref size);
            Assert.AreEqual(8, vec.X);
            Assert.AreEqual(17, vec.Y);
        }

        [Test(Description = "TopLeft アラインメントが正しく適用されることを確認")]
        public void ApplyTo_TopLeft_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.TopLeft.ApplyTo(ref vec, ref size);
            Assert.AreEqual(10, vec.X);
            Assert.AreEqual(20, vec.Y);
        }

        [Test(Description = "TopRight アラインメントが正しく適用されることを確認")]
        public void ApplyTo_TopRight_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.TopRight.ApplyTo(ref vec, ref size);
            Assert.AreEqual(6, vec.X);
            Assert.AreEqual(20, vec.Y);
        }

        [Test(Description = "BottomLeft アラインメントが正しく適用されることを確認")]
        public void ApplyTo_BottomLeft_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.BottomLeft.ApplyTo(ref vec, ref size);
            Assert.AreEqual(10, vec.X);
            Assert.AreEqual(14, vec.Y);
        }

        [Test(Description = "BottomRight アラインメントが正しく適用されることを確認")]
        public void ApplyTo_BottomRight_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.BottomRight.ApplyTo(ref vec, ref size);
            Assert.AreEqual(6, vec.X);
            Assert.AreEqual(14, vec.Y);
        }

        [Test(Description = "Left アラインメントが正しく適用されることを確認")]
        public void ApplyTo_Left_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.Left.ApplyTo(ref vec, ref size);
            Assert.AreEqual(10, vec.X);
            Assert.AreEqual(17, vec.Y);
        }

        [Test(Description = "Right アラインメントが正しく適用されることを確認")]
        public void ApplyTo_Right_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.Right.ApplyTo(ref vec, ref size);
            Assert.AreEqual(6, vec.X);
            Assert.AreEqual(17, vec.Y);
        }

        [Test(Description = "Top アラインメントが正しく適用されることを確認")]
        public void ApplyTo_Top_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.Top.ApplyTo(ref vec, ref size);
            Assert.AreEqual(8, vec.X);
            Assert.AreEqual(20, vec.Y);
        }

        [Test(Description = "Bottom アラインメントが正しく適用されることを確認")]
        public void ApplyTo_Bottom_Test()
        {
            Vector2 vec = new Vector2(10, 20);
            Vector2 size = new Vector2(4, 6);
            Alignment.Bottom.ApplyTo(ref vec, ref size);
            Assert.AreEqual(8, vec.X);
            Assert.AreEqual(14, vec.Y);
        }

        [Test(Description = "float パラメータ版 ApplyTo が正しく動作することを確認")]
        public void ApplyTo_FloatVersion_Test()
        {
            float x = 10, y = 20;
            float width = 4, height = 6;
            Alignment.BottomRight.ApplyTo(ref x, ref y, width, height);
            Assert.AreEqual(6, x);
            Assert.AreEqual(14, y);
        }

        [Test(Description = "戻り値版 ApplyTo が正しく動作することを確認")]
        public void ApplyTo_ReturnVersion_Test()
        {
            Vector2 result = Alignment.Center.ApplyTo(new Vector2(10, 20), new Vector2(4, 6));
            Assert.AreEqual(8, result.X);
            Assert.AreEqual(17, result.Y);
        }
    }
}
