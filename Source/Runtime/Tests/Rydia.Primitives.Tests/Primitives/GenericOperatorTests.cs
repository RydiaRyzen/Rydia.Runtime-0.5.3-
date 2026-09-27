using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Primitives.Tests
{
    internal class GenericOperatorTests
    {

        [Test(Description = "Add メソッドが int 型で正しく動作することを確認")]
        public void Add_Int_Test()
        {
            int a = 5, b = 3;
            int result = GenericOperator.Add(a, b);
            Assert.AreEqual(8, result);
        }

        [Test(Description = "Subtract メソッドが float 型で正しく動作することを確認")]
        public void Subtract_Float_Test()
        {
            float a = 5.5f, b = 2.2f;
            float result = GenericOperator.Subtract(a, b);
            Assert.AreEqual(3.3f, result, 1e-6);
        }

        [Test(Description = "Multiply メソッドが double 型で正しく動作することを確認")]
        public void Multiply_Double_Test()
        {
            double a = 2.5, b = 4;
            double result = GenericOperator.Multiply(a, b);
            Assert.AreEqual(10, result, 1e-10);
        }

        [Test(Description = "Divide メソッドが int 型で正しく動作することを確認")]
        public void Divide_Int_Test()
        {
            int a = 10, b = 2;
            int result = GenericOperator.Divide(a, b);
            Assert.AreEqual(5, result);
        }

        [Test(Description = "Modulo メソッドが int 型で正しく動作することを確認")]
        public void Modulo_Int_Test()
        {
            int a = 10, b = 3;
            int result = GenericOperator.Modulo(a, b);
            Assert.AreEqual(1, result);
        }

        [Test(Description = "Negate メソッドが int 型で正しく動作することを確認")]
        public void Negate_Int_Test()
        {
            int a = 5;
            int result = GenericOperator.Negate(a);
            Assert.AreEqual(-5, result);
        }

        [Test(Description = "Abs メソッドが int 型で正しく動作することを確認")]
        public void Abs_Int_Test()
        {
            int a = -10;
            int result = GenericOperator.Abs(a);
            Assert.AreEqual(10, result);
        }

        [Test(Description = "ビット演算 Or, And, Xor, Not が int 型で正しく動作することを確認")]
        public void BitwiseOperations_Int_Test()
        {
            int a = 5;  // 0101
            int b = 3;  // 0011
            Assert.AreEqual(7, GenericOperator.Or(a, b));
            Assert.AreEqual(1, GenericOperator.And(a, b));
            Assert.AreEqual(6, GenericOperator.Xor(a, b));
            Assert.AreEqual(~a, GenericOperator.Not(a));
        }

        [Test(Description = "比較演算 Equal, GreaterThan, LessThan などが int 型で正しく動作することを確認")]
        public void Comparison_Int_Test()
        {
            int a = 5, b = 3;
            Assert.IsTrue(GenericOperator.Equal(a, 5));
            Assert.IsTrue(GenericOperator.GreaterThan(a, b));
            Assert.IsTrue(GenericOperator.GreaterThanOrEqual(a, 5));
            Assert.IsTrue(GenericOperator.LessThan(b, a));
            Assert.IsTrue(GenericOperator.LessThanOrEqual(b, 3));
        }

        [Test(Description = "Convert メソッドが int から float に正しく変換できることを確認")]
        public void Convert_IntToFloat_Test()
        {
            int a = 5;
            float result = GenericOperator.Convert<int, float>(a);
            Assert.AreEqual(5f, result);
        }

        [Test(Description = "Lerp メソッドが float 型で正しく線形補間を行うことを確認")]
        public void Lerp_Float_Test()
        {
            float a = 0f, b = 10f;
            float result = GenericOperator.Lerp(a, b, 0.25f);
            Assert.AreEqual(2.5f, result, 1e-6);
        }
    }
}
