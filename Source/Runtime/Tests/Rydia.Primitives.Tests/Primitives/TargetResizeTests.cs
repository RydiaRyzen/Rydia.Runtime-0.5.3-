using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class TargetResizeTests
    {

        [Test(Description = "None は元のサイズをそのまま返す")]
        public void Apply_None_ReturnsBaseSize()
        {
            var baseSize = new Vector2(100, 50);
            var target = new Vector2(200, 200);

            var result = TargetResize.None.Apply(baseSize, target);

            Assert.AreEqual(baseSize, result);
        }

        [Test(Description = "Stretch はターゲットサイズをそのまま返す")]
        public void Apply_Stretch_ReturnsTargetSize()
        {
            var baseSize = new Vector2(100, 50);
            var target = new Vector2(200, 300);

            var result = TargetResize.Stretch.Apply(baseSize, target);

            Assert.AreEqual(target, result);
        }

        [Test(Description = "Fit はアスペクト比を維持しつつターゲットに収まる最大サイズを返す（横が基準）")]
        public void Apply_Fit_UsesSmallerScale_XBased()
        {
            var baseSize = new Vector2(100, 50);
            var target = new Vector2(300, 400);

            // 横スケール = 300 / 100 = 3.0
            // 縦スケール = 400 / 50  = 8.0
            // 小さい方の 3.0 を使用
            var expected = new Vector2(300, 150);

            var result = TargetResize.Fit.Apply(baseSize, target);

            Assert.AreEqual(expected, result);
        }

        [Test(Description = "Fit はアスペクト比を維持しつつターゲットに収まる最大サイズを返す（縦が基準）")]
        public void Apply_Fit_UsesSmallerScale_YBased()
        {
            var baseSize = new Vector2(200, 100);
            var target = new Vector2(300, 200);

            // 横 = 300 / 200 = 1.5
            // 縦 = 200 / 100 = 2.0
            // 小さい 1.5 を使用
            var expected = new Vector2(300, 150);

            var result = TargetResize.Fit.Apply(baseSize, target);

            Assert.AreEqual(expected, result);
        }

        [Test(Description = "Fill はアスペクト比を維持しつつターゲットを満たす最小サイズを返す（横が基準）")]
        public void Apply_Fill_UsesLargerScale_XBased()
        {
            var baseSize = new Vector2(100, 50);
            var target = new Vector2(300, 400);

            // 横 = 300 / 100 = 3.0
            // 縦 = 400 / 50  = 8.0
            // 大きい 8.0 を使用
            var expected = new Vector2(800, 400);

            var result = TargetResize.Fill.Apply(baseSize, target);

            Assert.AreEqual(expected, result);
        }

        [Test(Description = "Fill はアスペクト比を維持しつつターゲットを満たす最小サイズを返す（縦が基準）")]
        public void Apply_Fill_UsesLargerScale_YBased()
        {
            var baseSize = new Vector2(200, 100);
            var target = new Vector2(300, 200);

            // 横 = 300 / 200 = 1.5
            // 縦 = 200 / 100 = 2.0
            // 大きい 2.0 を使用
            var expected = new Vector2(400, 200);

            var result = TargetResize.Fill.Apply(baseSize, target);

            Assert.AreEqual(expected, result);
        }

        [Test(Description = "Fit は baseSize が Zero の場合 One 扱いで安全に動作する")]
        public void Apply_Fit_BaseSizeZero_IsSafe()
        {
            var baseSize = Vector2.Zero;
            var target = new Vector2(300, 200);

            // baseSize = One として扱う
            // scale = min(300/1, 200/1) = 200
            var expected = new Vector2(200, 200);

            var result = TargetResize.Fit.Apply(baseSize, target);

            Assert.AreEqual(expected, result);
        }

        [Test(Description = "Fill は baseSize が Zero の場合 One 扱いで安全に動作する")]
        public void Apply_Fill_BaseSizeZero_IsSafe()
        {
            var baseSize = Vector2.Zero;
            var target = new Vector2(300, 200);

            // baseSize = One として扱う
            // scale = max(300/1, 200/1) = 300
            var expected = new Vector2(300, 300);

            var result = TargetResize.Fill.Apply(baseSize, target);

            Assert.AreEqual(expected, result);
        }

    }
}
