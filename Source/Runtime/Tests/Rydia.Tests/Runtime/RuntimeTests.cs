using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Runtime;
using Rydia.Runtime.Shared;

namespace Rydia.Tests.Runtime
{
    public class RuntimeTests
    {

        [Test(Description = "MessageDialogEventArgsが正しく動作するかをテストします")]
        public void MessageDialogEventArgsTest_Constructor()
        {
            var args1 = new Rydia.Runtime.Shared.MessageDialogEventArgs("Test Message");
            Assert.AreEqual("Test Message", args1.Message);
            Assert.AreEqual("Rydia Engine", args1.Title);
            Assert.AreEqual(Rydia.Runtime.Shared.MessageBoxButtons.OK, args1.Buttons);
            var args2 = new Rydia.Runtime.Shared.MessageDialogEventArgs("Test Message", "Test Title");
            Assert.AreEqual("Test Message", args2.Message);
            Assert.AreEqual("Test Title", args2.Title);
            Assert.AreEqual(Rydia.Runtime.Shared.MessageBoxButtons.OK, args2.Buttons);
            var args3 = new Rydia.Runtime.Shared.MessageDialogEventArgs("Test Message", Rydia.Runtime.Shared.MessageBoxButtons.YesNo);
            Assert.AreEqual("Test Message", args3.Message);
            Assert.AreEqual("Rydia Engine", args3.Title);
            Assert.AreEqual(Rydia.Runtime.Shared.MessageBoxButtons.YesNo, args3.Buttons);
            var args4 = new Rydia.Runtime.Shared.MessageDialogEventArgs("Test Message", "Test Title", Rydia.Runtime.Shared.MessageBoxButtons.YesNoCancel);
            Assert.AreEqual("Test Message", args4.Message);
            Assert.AreEqual("Test Title", args4.Title);
            Assert.AreEqual(Rydia.Runtime.Shared.MessageBoxButtons.YesNoCancel, args4.Buttons);
        }

        [Test(Description = "MessageBoxButtons列挙型が正しく動作するかをテストします")]
        public void MessageBoxButtonsTest_EnumValues()
        {
            Assert.AreEqual(0, (int)Rydia.Runtime.Shared.MessageBoxButtons.OK);
            Assert.AreEqual(1, (int)Rydia.Runtime.Shared.MessageBoxButtons.OKCancel);
            Assert.AreEqual(2, (int)Rydia.Runtime.Shared.MessageBoxButtons.YesNo);
            Assert.AreEqual(3, (int)Rydia.Runtime.Shared.MessageBoxButtons.YesNoCancel);
        }

        private class TestMessageService : Rydia.Runtime.Shared.IMessageService
        {
            public DialogResult ShowMessage(MessageDialogEventArgs args)
            {
                return DialogResult.OK;
            }

        }

        [Test(Description = "MessageDialogEventArgsのプロパティが正しく動作するかをテストします")]
        public void IMessageServiceTest()
        {
            var service = new TestMessageService();
            var result1 = service.ShowMessage("Test Message");
            Assert.AreEqual(DialogResult.OK, result1);
        }

        private class CmdArgs : CommandLineArgs
        {

            public const string CmdArgDebug = "debug";

            public bool IsDebug
            {
                get;
                private set;
            }

            public CmdArgs(string[] args)
            {
                IsDebug = args.Contains(CmdArgDebug, StringComparer.OrdinalIgnoreCase);
            }

        }

        [Test(Description = "CommandLineArgsが正しく動作するかをテストします")]
        public void CmdArgsTest()
        {
            var args1 = new CmdArgs(new string[] { "debug" });
            Assert.IsTrue(args1.IsDebug);
            var args2 = new CmdArgs(new string[] { "DEBUG" });
            Assert.IsTrue(args2.IsDebug);
            var args3 = new CmdArgs(new string[] { "release" });
            Assert.IsFalse(args3.IsDebug);
            var args4 = new CmdArgs(new string[] { });
            Assert.IsFalse(args4.IsDebug);
        }
        /*
        [Test(Description = "CommandLineArgs.IsRunningDebugAssemblyが正しく動作するかをテストします")]
        [Ignore("ビルド構成に依存するため、自動テストでは無視します")]
        public void CommandLineArgs_IsRunningDebugAssemblyTest()
        {
            // このテストは実行されているアセンブリがデバッグビルドかリリースビルドかに依存します
            bool isDebug = CommandLineArgs.IsRunningDebugAssembly;
            // デバッグビルドの場合はtrue、リリースビルドの場合はfalseであることを確認します
#if DEBUG
            Assert.IsTrue(isDebug);
#else
            Assert.IsFalse(isDebug);
#endif
        }
        */
        [Test(Description = "DisposableBaseのDisposingとDisposedメソッドが正しく動作するかをテストします")]
        public void DisposableBase_DisposingDisposedTest()
        {
            bool disposingCalled = false;
            bool disposedCalled = false;
            var disposable = new TestDisposable(
                onDisposing: (disposing) =>
                {
                    disposingCalled = true;
                    Assert.IsTrue(disposing);
                },
                onDisposed: (disposing) =>
                {
                    disposedCalled = true;
                    Assert.IsTrue(disposing);
                });
            Assert.IsFalse(disposable.IsDisposed);
            disposable.Dispose();
            Assert.IsTrue(disposingCalled);
            Assert.IsTrue(disposedCalled);
            Assert.DoesNotThrow(() => disposable.Dispose());
            Assert.IsTrue(disposable.IsDisposed);
        }

        private class TestDisposable : DisposableBase
        {
            private readonly Action<bool> onDisposing;
            private readonly Action<bool> onDisposed;
            public TestDisposable(Action<bool> onDisposing, Action<bool> onDisposed)
            {
                this.onDisposing = onDisposing;
                this.onDisposed = onDisposed;
            }
            protected override void Disposing(bool disposing)
            {
                this.onDisposing?.Invoke(disposing);
            }
            protected override void Disposed(bool disposing)
            {
                this.onDisposed?.Invoke(disposing);
            }
        }
        // テスト用の列挙型
        internal enum TestEnum
        {
            A,
            B,
            C
        }

        [Test(Description = "Count プロパティが Enum.GetValues の数と一致することを確認します")]
        public void Count_ShouldMatchEnumValuesCount()
        {
            int expected = Enum.GetValues(typeof(TestEnum)).Length;
            Assert.AreEqual(expected, Enum<TestEnum>.Count);
        }

        [Test(Description = "Values プロパティが全ての列挙値を含むことを確認します")]
        public void Values_ShouldContainAllEnumValues()
        {
            var expected = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>().ToArray();
            var actual = Enum<TestEnum>.Values.ToArray();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [Test(Description = "ForEach がすべての列挙値に対して呼び出されることを確認します")]
        public void ForEach_ShouldInvokeActionForAllValues()
        {
            var collected = new System.Collections.Generic.List<TestEnum>();
            Enum<TestEnum>.ForEach(v => collected.Add(v));

            var expected = Enum.GetValues(typeof(TestEnum)).Cast<TestEnum>().ToArray();
            CollectionAssert.AreEquivalent(expected, collected);
        }

        [Test(Description = "CreateArray が列挙値の数と同じ長さの配列を生成することを確認します")]
        public void CreateArray_ShouldCreateArrayWithEnumLength()
        {
            var arr = Enum<TestEnum>.CreateArray<float>();
            Assert.AreEqual(Enum<TestEnum>.Count, arr.Length);
            Assert.IsTrue(arr.All(v => v == default(float)));
        }

        [Test(Description = "Values プロパティが新しいリストを返すことを確認します（参照の独立性）")]
        public void Values_ShouldReturnNewListEachTime()
        {
            var v1 = Enum<TestEnum>.Values;
            var v2 = Enum<TestEnum>.Values;

            Assert.AreNotSame(v1, v2, "Values は都度新しいリストを返す必要があります");
        }
    }
}
