using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class MathFRTests
    {

        [Test(Description = "Clamp関数が指定範囲内に値を制限できることを確認")]
        public void TestClamp()
        {
            Assert.AreEqual(5f, MathFR.Clamp(5f, 0f, 10f));
            Assert.AreEqual(0f, MathFR.Clamp(-1f, 0f, 10f));
            Assert.AreEqual(10f, MathFR.Clamp(15f, 0f, 10f));
        }

        [Test(Description = "Lerp関数が線形補間で正しい値を返すことを確認")]
        public void TestLerp()
        {
            Assert.AreEqual(5f, MathFR.Lerp(0f, 10f, 0.5f));
            Assert.AreEqual(0f, MathFR.Lerp(0f, 10f, 0f));
            Assert.AreEqual(10f, MathFR.Lerp(0f, 10f, 1f));
        }

        [Test(Description = "NormalizeAngle関数が角度を0〜360度範囲に正規化できることを確認")]
        public void TestNormalizeAngle()
        {
            Assert.AreEqual(MathFR.RadAngle90, MathFR.NormalizeAngle(MathFR.RadAngle90));
            Assert.AreEqual(0f, MathFR.NormalizeAngle(MathFR.TwoPi));
            Assert.AreEqual(MathFR.RadAngle180, MathFR.NormalizeAngle(-MathFR.RadAngle180));
        }

        [Test(Description = "Distance関数が2点間の距離を正しく計算できることを確認")]
        public void TestDistance()
        {
            Assert.AreEqual(5f, MathFR.Distance(0f, 0f, 3f, 4f));
            Assert.AreEqual(5f, MathFR.Distance(3f, 4f));
        }

        [Test(Description = "Round関数が小数点以下を正しく四捨五入できることを確認")]
        public void TestRound()
        {
            Assert.AreEqual(3f, MathFR.Round(3.2f));
            Assert.AreEqual(4f, MathFR.Round(3.6f));
            Assert.AreEqual(3, MathFR.RoundToInt(3.2f));
            Assert.AreEqual(4, MathFR.RoundToInt(3.6f));
        }

        [Test(Description = "TurnDir関数が目標方向への回転方向を正しく返すことを確認")]
        public void TestTurnDir()
        {
            Assert.AreEqual(1f, MathFR.TurnDir(10f, 20f, 0f, 100f));
            Assert.AreEqual(-1f, MathFR.TurnDir(20f, 10f, 0f, 100f));
            Assert.AreEqual(0f, MathFR.TurnDir(10f, 10f, 0f, 100f));
        }

        [Test(Description = "CircularDist関数が円周上の距離を正しく計算できることを確認")]
        public void TestCircularDist()
        {
            Assert.AreEqual(40f, MathFR.CircularDist(10f, 50f, 0f, 100f));
            Assert.AreEqual(10f, MathFR.CircularDist(350f, 0f, 0f, 360f));
            Assert.AreEqual(10f, MathFR.CircularDist(0f, 350f, 0f, 360f));
        }

        [Test(Description = "TransformCoord関数が座標を指定角度で正しく回転できることを確認")]
        public void TestTransformCoord()
        {
            float x = 1f, y = 0f;
            MathFR.TransformCoord(ref x, ref y, MathFR.RadAngle90);
            Assert.That(MathFR.Abs(x) < 0.0001f);
            Assert.That(MathFR.Abs(y - 1f) < 0.0001f);
        }

    }
}
