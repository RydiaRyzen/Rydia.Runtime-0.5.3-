using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class RectFTests
    {

        // -------------------------------
        // 基本プロパティ
        // -------------------------------
        [Test(Description = "Pos と Size が正常に設定・取得できる")]
        public void PosSize_Accessors_WorkCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            Assert.AreEqual(new Vector2(10, 20), rect.Pos);
            Assert.AreEqual(new Vector2(30, 40), rect.Size);

            rect.Pos = new Vector2(5, 6);
            rect.Size = new Vector2(7, 8);

            Assert.AreEqual(new Vector2(5, 6), rect.Pos);
            Assert.AreEqual(new Vector2(7, 8), rect.Size);
        }

        // -------------------------------
        // 位置／サイズ 派生プロパティ
        // -------------------------------
        [Test(Description = "LeftX / TopY / RightX / BottomY が正のサイズで正しく計算される")]
        public void Bounds_PositiveSize()
        {
            var rect = new RectF(10, 20, 30, 40);

            Assert.AreEqual(10, rect.LeftX);
            Assert.AreEqual(20, rect.TopY);
            Assert.AreEqual(40, rect.RightX);
            Assert.AreEqual(60, rect.BottomY);
        }

        [Test(Description = "LeftX / TopY / RightX / BottomY が負のサイズで正しく計算される")]
        public void Bounds_NegativeSize()
        {
            var rect = new RectF(10, 20, -30, -40);

            Assert.AreEqual(-20, rect.LeftX);
            Assert.AreEqual(-20, rect.TopY);
            Assert.AreEqual(10, rect.RightX);
            Assert.AreEqual(20, rect.BottomY);
        }

        [Test(Description = "CenterX / CenterY が正しく計算される")]
        public void Center_Calculations()
        {
            var rect = new RectF(10, 20, 30, 40);

            Assert.AreEqual(25, rect.CenterX);
            Assert.AreEqual(40, rect.CenterY);
            Assert.AreEqual(new Vector2(25, 40), rect.Center);
        }

        [Test(Description = "四隅および辺中央の座標が正しく計算される")]
        public void Corner_Calculations()
        {
            var rect = new RectF(10, 20, 30, 40);

            Assert.AreEqual(new Vector2(10, 20), rect.TopLeft);
            Assert.AreEqual(new Vector2(40, 20), rect.TopRight);
            Assert.AreEqual(new Vector2(10, 60), rect.BottomLeft);
            Assert.AreEqual(new Vector2(40, 60), rect.BottomRight);

            Assert.AreEqual(new Vector2(25, 20), rect.Top);
            Assert.AreEqual(new Vector2(25, 60), rect.Bottom);
            Assert.AreEqual(new Vector2(10, 40), rect.Left);
            Assert.AreEqual(new Vector2(40, 40), rect.Right);
        }

        // -------------------------------
        // BoundingRadius
        // -------------------------------
        [Test(Description = "BoundingRadius が正しく計算される")]
        public void BoundingRadius_WorksCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            float expected = MathFR.Distance(
                40,
                60
            );

            Assert.AreEqual(expected, rect.BoundingRadius);
        }

        // -------------------------------
        // WithOffset
        // -------------------------------
        [Test(Description = "WithOffset が正しく位置を変更する")]
        public void WithOffset_WorksCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            var moved = rect.WithOffset(5, -3);

            Assert.AreEqual(new RectF(15, 17, 30, 40), moved);
        }

        // -------------------------------
        // Scaled / Transformed
        // -------------------------------
        [Test(Description = "Scaled がサイズのみスケールする")]
        public void Scaled_WorksCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            var scaled = rect.Scaled(2, 3);

            Assert.AreEqual(new RectF(10, 20, 60, 120), scaled);
        }

        [Test(Description = "Transformed が位置とサイズをスケールする")]
        public void Transformed_WorksCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            var trans = rect.Transformed(2, 3);

            Assert.AreEqual(new RectF(20, 60, 60, 120), trans);
        }

        // -------------------------------
        // ExpandedToContain
        // -------------------------------
        [Test(Description = "ExpandedToContain が点を正しく含むように拡張される")]
        public void ExpandedToContain_Point_WorksCorrectly()
        {
            var rect = new RectF(10, 10, 10, 10);

            var expanded = rect.ExpandedToContain(25, 5);

            Assert.AreEqual(new RectF(10, 5, 15, 15), expanded);
        }

        [Test(Description = "ExpandedToContain が別の Rect を正しく含むように拡張される")]
        public void ExpandedToContain_Rect_WorksCorrectly()
        {
            var rect = new RectF(10, 10, 10, 10);

            var other = new RectF(0, 0, 5, 5);

            var expanded = rect.ExpandedToContain(other);

            Assert.AreEqual(new RectF(0, 0, 20, 20), expanded);
        }

        // -------------------------------
        // Normalized
        // -------------------------------
        [Test(Description = "Normalized が幅と高さを正にし、位置を補正する")]
        public void Normalized_WorksCorrectly()
        {
            var rect = new RectF(10, 20, -30, -40);

            var norm = rect.Normalized();

            Assert.AreEqual(new RectF(-20, -20, 30, 40), norm);
        }

        // -------------------------------
        // Contains
        // -------------------------------
        [Test(Description = "Contains が点を正しく判定する")]
        public void Contains_Point_WorksCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            Assert.IsTrue(rect.Contains(15, 25));
            Assert.IsFalse(rect.Contains(5, 25));
            Assert.IsFalse(rect.Contains(15, 100));
        }

        [Test(Description = "Contains が矩形を正しく判定する")]
        public void Contains_Rect_WorksCorrectly()
        {
            var rect = new RectF(10, 20, 30, 40);

            Assert.IsTrue(rect.Contains(new RectF(15, 25, 10, 10)));
            Assert.IsFalse(rect.Contains(new RectF(0, 0, 100, 100)));
        }

        // -------------------------------
        // Intersects
        // -------------------------------
        [Test(Description = "Intersects が矩形の交差を正しく判定する")]
        public void Intersects_WorksCorrectly()
        {
            var rect = new RectF(10, 10, 30, 30);

            Assert.IsTrue(rect.Intersects(new RectF(20, 20, 30, 30)));
            Assert.IsFalse(rect.Intersects(new RectF(100, 100, 10, 10)));
        }

        // -------------------------------
        // Intersection
        // -------------------------------
        [Test(Description = "Intersection が交差領域を正しく返す")]
        public void Intersection_WorksCorrectly()
        {
            var a = new RectF(10, 10, 30, 30);
            var b = new RectF(20, 20, 30, 30);

            var result = a.Intersection(b);

            Assert.AreEqual(new RectF(20, 20, 20, 20), result);
        }

        [Test(Description = "Intersection が交差しない場合 Empty を返す")]
        public void Intersection_NoOverlap_ReturnsEmpty()
        {
            var a = new RectF(10, 10, 30, 30);
            var b = new RectF(100, 100, 30, 30);

            var result = a.Intersection(b);

            Assert.AreEqual(RectF.Empty, result);
        }

        // -------------------------------
        // Align
        // -------------------------------
        [Test(Description = "Align が各種アライメントで正しく Rect を作成する")]
        public void Align_WorksCorrectly()
        {
            Assert.AreEqual(new RectF(10, 20, 30, 40),
                RectF.Align(Alignment.TopLeft, 10, 20, 30, 40));

            Assert.AreEqual(new RectF(10 - 30, 20, 30, 40),
                RectF.Align(Alignment.TopRight, 10, 20, 30, 40));

            Assert.AreEqual(new RectF(10 - 15, 20 - 20, 30, 40),
                RectF.Align(Alignment.Center, 10, 20, 30, 40));
        }

        // -------------------------------
        // Equals / !=
        // -------------------------------
        [Test(Description = "Rect の等価比較が正しく動作する")]
        public void Equality_WorksCorrectly()
        {
            var a = new RectF(1, 2, 3, 4);
            var b = new RectF(1, 2, 3, 4);
            var c = new RectF(9, 9, 9, 9);

            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);

            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

    }
}
