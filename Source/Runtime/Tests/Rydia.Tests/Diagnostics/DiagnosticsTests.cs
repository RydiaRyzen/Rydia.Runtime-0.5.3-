using Rydia;
using Rydia.Diagnostics;

namespace Rydia.Tests.Diagnostics
{
    public class DiagnosticsTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test(Description = "CallerInfoが正しく動作するかをテストします")]
        public void CallerInfoTest_Get()
        {
            var info = Rydia.Diagnostics.CallerInfo.Get<DiagnosticsTests>();
            //　クラス名がTestsであることを確認します
            Assert.AreEqual(nameof(DiagnosticsTests), info.ClassName);
            // メソッド名がCallerInfoTest1であることを確認します
            Assert.AreEqual("CallerInfoTest_Get", info.MemberName);
            // ファイルパスがUnitTest1.csで終わることを確認します
            Assert.IsTrue(info.FilePath.EndsWith("DiagnosticsTests.cs"));
            // 行番号が13であることを確認します
            Assert.AreEqual(info.LineNumber, 16);
        }

        [Test(Description = "ExceptionInfoが正しく動作するかをテストします")]
        public void ExceptionInfoTest_GetExceptionInfo()
        {
            try
            {
                throw new InvalidOperationException("Test Exception", new ArgumentNullException("param"));
            }
            catch (Exception ex)
            {
                var info = new ExceptionInfo(ex);
                // 例外の型がInvalidOperationExceptionであることを確認します
                Assert.AreEqual(typeof(InvalidOperationException), info.BaseType);
                // メッセージが"Test Exception"であることを確認します
                Assert.AreEqual("Test Exception", info.Message);
                // InnerExceptionが存在することを確認します
                Assert.IsNotNull(info.InnerException);
                // InnerExceptionの型がArgumentNullExceptionであることを確認します
                Assert.AreEqual(typeof(ArgumentNullException), info.InnerException.BaseType);
                // InnerExceptionのメッセージが"Value cannot be null. (Parameter 'param')"であることを確認します
                Assert.AreEqual("Value cannot be null. (Parameter 'param')", info.InnerException.Message);
            }
        }

    }
}