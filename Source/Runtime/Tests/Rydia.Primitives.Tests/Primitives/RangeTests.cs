using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class RangeTests
    {

        [Test(Description = "コンストラクタで最小値と最大値が正しく設定されることを確認")]
        public void Constructor_MinMax_PropertiesSet()
        {
            var range = new Range(1f, 5f);
            Assert.AreEqual(1f, range.MinValue);
            Assert.AreEqual(5f, range.MaxValue);
        }

        [Test(Description = "単一値のコンストラクタで幅0の範囲が作成されることを確認")]
        public void Constructor_SingleValue_ZeroWidthRange()
        {
            var range = new Range(3f);
            Assert.AreEqual(3f, range.MinValue);
            Assert.AreEqual(3f, range.MaxValue);
        }

        [Test(Description = "Widthプロパティが正しい差を返すことを確認")]
        public void Width_ReturnsCorrectDifference()
        {
            var range = new Range(2f, 7f);
            Assert.AreEqual(5f, range.Width);
        }

        [Test(Description = "Centerプロパティが正しい中央値を返すことを確認")]
        public void Center_ReturnsCorrectMiddleValue()
        {
            var range = new Range(2f, 6f);
            Assert.AreEqual(4f, range.Center);
        }

        [Test(Description = "Normalizeメソッドで不正な範囲が正しい順序に修正されることを確認")]
        public void Normalize_FlipsValuesIfIrregular()
        {
            var range = new Range(5f, 2f);
            range.Normalize();
            Assert.AreEqual(2f, range.MinValue);
            Assert.AreEqual(5f, range.MaxValue);
        }

        [Test(Description = "Normalizedプロパティで正規化された範囲が取得でき、元の範囲は変更されないことを確認")]
        public void NormalizedProperty_ReturnsNormalizedRangeWithoutModifyingOriginal()
        {
            var range = new Range(5f, 2f);
            var normalized = range.Normalized;
            Assert.AreEqual(2f, normalized.MinValue);
            Assert.AreEqual(5f, normalized.MaxValue);
            Assert.AreEqual(5f, range.MinValue);
            Assert.AreEqual(2f, range.MaxValue);
        }

        [Test(Description = "Containsメソッドで値が範囲内の場合にtrueを返すことを確認")]
        public void Contains_ValueInside_ReturnsTrue()
        {
            var range = new Range(1f, 5f);
            Assert.IsTrue(range.Contains(3f));
        }

        [Test(Description = "Containsメソッドで値が範囲外の場合にfalseを返すことを確認")]
        public void Contains_ValueOutside_ReturnsFalse()
        {
            var range = new Range(1f, 5f);
            Assert.IsFalse(range.Contains(0f));
            Assert.IsFalse(range.Contains(6f));
        }

        [Test(Description = "Containsメソッドで他の範囲が完全に含まれる場合にtrueを返すことを確認")]
        public void Contains_RangeInside_ReturnsTrue()
        {
            var outer = new Range(0f, 10f);
            var inner = new Range(3f, 7f);
            Assert.IsTrue(outer.Contains(inner));
        }

        [Test(Description = "Containsメソッドで他の範囲が含まれない場合にfalseを返すことを確認")]
        public void Contains_RangeOutside_ReturnsFalse()
        {
            var outer = new Range(0f, 10f);
            var inner = new Range(-1f, 5f);
            Assert.IsFalse(outer.Contains(inner));
        }

        [Test(Description = "等価演算子で範囲が等しい場合にtrueを返すことを確認")]
        public void EqualsOperator_ReturnsTrueForEqualRanges()
        {
            var a = new Range(1f, 3f);
            var b = new Range(1f, 3f);
            Assert.IsTrue(a == b);
            Assert.IsFalse(a != b);
        }

        [Test(Description = "Lerpメソッドが指定比率で補間した値を返すことを確認")]
        public void Lerp_ReturnsInterpolatedValue()
        {
            var range = new Range(2f, 6f);
            Assert.AreEqual(2f, range.Lerp(0f));
            Assert.AreEqual(6f, range.Lerp(1f));
            Assert.AreEqual(4f, range.Lerp(0.5f));
        }

        [Test(Description = "四則演算演算子で範囲の各要素が正しく計算されることを確認")]
        public void ArithmeticOperators_AddSubMulDiv_WorkAsExpected()
        {
            var a = new Range(1f, 3f);
            var b = new Range(2f, 4f);

            var add = a + b;
            Assert.AreEqual(new Range(3f, 7f), add);

            var sub = a - b;
            Assert.AreEqual(new Range(-1f, -1f), sub);

            var mul = a * b;
            Assert.AreEqual(new Range(2f, 12f), mul);

            var div = b / a;
            Assert.AreEqual(new Range(2f, 4f / 3f), div);
        }

        [Test(Description = "暗黙変換で単一値から幅0の範囲を作成できることを確認")]
        public void ImplicitConversion_FromFloat_CreatesZeroWidthRange()
        {
            Range r = 5f;
            Assert.AreEqual(5f, r.MinValue);
            Assert.AreEqual(5f, r.MaxValue);
        }

    }
}
