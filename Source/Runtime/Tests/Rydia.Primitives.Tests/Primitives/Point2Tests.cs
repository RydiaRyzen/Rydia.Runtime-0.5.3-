using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class Point2Tests
    {

        [Test(Description = "Zeroプロパティが(0,0)であることを確認")]
        public void Zero_ReturnsCorrectValues()
        {
            Assert.AreEqual(0, Point2.Zero.X);
            Assert.AreEqual(0, Point2.Zero.Y);
        }

        [Test(Description = "コンストラクタでX,Yが正しく設定されることを確認")]
        public void Constructor_SetsValuesCorrectly()
        {
            var p = new Point2(3, 5);
            Assert.AreEqual(3, p.X);
            Assert.AreEqual(5, p.Y);
        }

        [Test(Description = "インデクサでX,Yを取得・設定できることを確認")]
        public void Indexer_GetSet_Works()
        {
            var p = new Point2(1, 2);
            Assert.AreEqual(1, p[0]);
            Assert.AreEqual(2, p[1]);

            p[0] = 10;
            p[1] = 20;
            Assert.AreEqual(10, p.X);
            Assert.AreEqual(20, p.Y);
        }

        [Test(Description = "インデクサで範囲外アクセスは例外が発生することを確認")]
        public void Indexer_OutOfRange_Throws()
        {
            var p = new Point2(1, 2);
            Assert.Throws<IndexOutOfRangeException>(() => { var x = p[2]; });
            Assert.Throws<IndexOutOfRangeException>(() => { p[2] = 10; });
        }

        [Test(Description = "Minメソッドが正しく動作することを確認")]
        public void Min_ReturnsCorrectComponentWiseMinimum()
        {
            var a = new Point2(3, 7);
            var b = new Point2(5, 4);
            var min = Point2.Min(a, b);
            Assert.AreEqual(new Point2(3, 4), min);
        }

        [Test(Description = "Maxメソッドが正しく動作することを確認")]
        public void Max_ReturnsCorrectComponentWiseMaximum()
        {
            var a = new Point2(3, 7);
            var b = new Point2(5, 4);
            var max = Point2.Max(a, b);
            Assert.AreEqual(new Point2(5, 7), max);
        }

        [Test(Description = "Distanceメソッドが正しい距離を返すことを確認")]
        public void Distance_ReturnsCorrectValue()
        {
            var a = new Point2(0, 0);
            var b = new Point2(3, 4);
            float dist = Point2.Distance(a, b);
            Assert.AreEqual(5f, dist, 0.0001f);
        }

        [Test(Description = "加算演算子が正しく動作することを確認")]
        public void Operator_Add_Works()
        {
            var a = new Point2(1, 2);
            var b = new Point2(3, 4);
            Assert.AreEqual(new Point2(4, 6), a + b);
        }

        [Test(Description = "減算演算子が正しく動作することを確認")]
        public void Operator_Subtract_Works()
        {
            var a = new Point2(5, 6);
            var b = new Point2(3, 4);
            Assert.AreEqual(new Point2(2, 2), a - b);
        }

        [Test(Description = "単項マイナス演算子が正しく動作することを確認")]
        public void Operator_UnaryMinus_Works()
        {
            var p = new Point2(3, -4);
            Assert.AreEqual(new Point2(-3, 4), -p);
        }

        [Test(Description = "スカラー乗算が正しく動作することを確認")]
        public void Operator_MultiplyScalar_Works()
        {
            var p = new Point2(2, 3);
            Assert.AreEqual(new Point2(4, 6), p * 2);
            Assert.AreEqual(new Point2(4, 6), 2 * p);
        }

        [Test(Description = "Point2同士の乗算が正しく動作することを確認")]
        public void Operator_MultiplyPoint_Works()
        {
            var a = new Point2(2, 3);
            var b = new Point2(4, 5);
            Assert.AreEqual(new Point2(8, 15), a * b);
        }

        [Test(Description = "スカラー除算が正しく動作することを確認")]
        public void Operator_DivideScalar_Works()
        {
            var p = new Point2(8, 12);
            Vector2 result = p / 4f;
            Assert.AreEqual(new Vector2(2f, 3f), result);
        }

        [Test(Description = "Point2同士の除算が正しく動作することを確認")]
        public void Operator_DividePoint_Works()
        {
            var a = new Point2(8, 12);
            var b = new Point2(2, 3);
            Assert.AreEqual(new Point2(4, 4), a / b);
        }

        [Test(Description = "等価演算子が正しく動作することを確認")]
        public void Operator_Equality_Works()
        {
            var a = new Point2(1, 2);
            var b = new Point2(1, 2);
            var c = new Point2(2, 3);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a == c);
            Assert.IsTrue(a != c);
        }

        [Test(Description = "暗黙変換でVector2に変換できることを確認")]
        public void ImplicitConversion_ToVector2_Works()
        {
            var p = new Point2(3, 4);
            Vector2 v = p;
            Assert.AreEqual(new Vector2(3f, 4f), v);
        }

        [Test(Description = "明示変換でPoint2に変換できることを確認")]
        public void ExplicitConversion_FromVector2_Works()
        {
            Vector2 v = new Vector2(3.7f, 4.2f);
            Point2 p = (Point2)v;
            Assert.AreEqual(new Point2(4, 4), p);
        }

        [Test(Description = "ToStringが正しい形式で文字列を返すことを確認")]
        public void ToString_ReturnsCorrectFormat()
        {
            var p = new Point2(3, 5);
            Assert.AreEqual("(3, 5)", p.ToString());
        }

        [Test(Description = "Equalsメソッドが正しく動作することを確認")]
        public void Equals_Works()
        {
            var a = new Point2(1, 2);
            var b = new Point2(1, 2);
            var c = new Point2(2, 3);
            Assert.IsTrue(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(null));
        }

    }
}
