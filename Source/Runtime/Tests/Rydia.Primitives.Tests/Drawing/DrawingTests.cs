using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Drawing;

namespace Rydia.Primitives.Tests.Drawing
{

    public class DrawingTests
    {

        [Test(Description = "RGBA の正しい分解・保持")]
        public void SetIntRgba_StoresValueCorrectly()
        {
            var color = new ColorRgba();
            int rgba = unchecked((int)0x11223344);

            color.SetIntRgba(rgba);

            Assert.AreEqual(0x11, color.R);
            Assert.AreEqual(0x22, color.G);
            Assert.AreEqual(0x33, color.B);
            Assert.AreEqual(0x44, color.A);
            Assert.AreEqual(rgba, color.ToIntRgba());
        }

        [Test(Description = "ARGB の正しい分解・保持")]
        public void SetIntArgb_StoresValueCorrectly()
        {
            var color = new ColorRgba();
            int argb = unchecked((int)0xAABBCCDD);

            color.SetIntArgb(argb);

            Assert.AreEqual(0xAA, color.A);
            Assert.AreEqual(0xBB, color.R);
            Assert.AreEqual(0xCC, color.G);
            Assert.AreEqual(0xDD, color.B);
            Assert.AreEqual(argb, color.ToIntArgb());
        }

        [Test(Description = "IColorData 拡張メソッドの動作")]
        public void ToColorRgba_ReturnsSameValues()
        {
            var color = new ColorRgba(0x11, 0x22, 0x33, 0x44);

            var rgba = color.ToColorRgba();

            Assert.AreEqual(color.ToIntRgba(), rgba.ToIntRgba());
        }

        [Test(Description = "RGBA ⇄ HSVA の往復")]
        public void ToColorHsva_UsesCorrectConversion()
        {
            var color = new ColorRgba(0x10, 0x20, 0x30, 0x40);

            var hsva = color.ToColorHsva();
            var back = hsva.ToRgba();

            Assert.That(back.R, Is.EqualTo(color.R).Within(1));
            Assert.That(back.G, Is.EqualTo(color.G).Within(1));
            Assert.That(back.B, Is.EqualTo(color.B).Within(1));
            Assert.That(back.A, Is.EqualTo(color.A).Within(1));
        }

        [Test(Description = "Color4 への変換")]
        public void ToColor4_ConvertsCorrectly()
        {
            // Red = (255, 0, 0, 255)
            var color = new ColorRgba(255, 0, 0, 255);

            Color4 gl = color.ToColor4();

            Assert.AreEqual(1f, gl.Rf, 0.0001f);
            Assert.AreEqual(0f, gl.Gf, 0.0001f);
            Assert.AreEqual(0f, gl.Bf, 0.0001f);
            Assert.AreEqual(1f, gl.Af, 0.0001f);
        }

        [Test(Description = "int→ColorRgba")]
        public void FromIntRgba_CreatesCorrectColor()
        {
            int rgba = unchecked((int)0x55667788);

            var color = ColorRgba.FromIntRgba(rgba);

            Assert.AreEqual(0x55, color.R);
            Assert.AreEqual(0x66, color.G);
            Assert.AreEqual(0x77, color.B);
            Assert.AreEqual(0x88, color.A);
        }

        [Test(Description = "int→ColorRgba(ARGB)")]
        public void FromIntArgb_CreatesCorrectColor()
        {
            int argb = unchecked((int)0x11223344);

            var color = ColorRgba.FromIntArgb(argb);

            Assert.AreEqual(0x22, color.R);
            Assert.AreEqual(0x33, color.G);
            Assert.AreEqual(0x44, color.B);
            Assert.AreEqual(0x11, color.A);
        }

        [Test(Description = "色補間処理の精度")]
        public void Lerp_ReturnsCorrectInterpolation()
        {
            var c1 = new ColorRgba(0, 0, 0, 0);
            var c2 = new ColorRgba(255, 255, 255, 255);

            var mid = ColorRgba.Lerp(c1, c2, 0.5f);

            Assert.AreEqual(128, mid.R);
            Assert.AreEqual(128, mid.G);
            Assert.AreEqual(128, mid.B);
            Assert.AreEqual(128, mid.A);
        }
    }

}
