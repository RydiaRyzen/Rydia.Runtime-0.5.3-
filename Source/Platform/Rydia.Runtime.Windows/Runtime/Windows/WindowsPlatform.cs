using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rydia.Audio.Api.OpenAL;
using Rydia.Graphics.Api.ES30;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.Shared;

namespace Rydia.Runtime.Windows
{

    /// <summary>
    /// Windows環境におけるプラットフォーム機能を提供します。
    /// </summary>
    public class WindowsPlatform : DisposableBase, IDesktopPlatform
    {

        #region static

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string path);

        static WindowsPlatform()
        {
            string path = Assembly.GetEntryAssembly().Location;
            path = Path.GetDirectoryName(path);
            path = Path.Combine(path, (IntPtr.Size == 8) ? "x64" : "x86");
            SetDllDirectory(path);
        }

        #endregion

        #region IDesktopPlatform

        /// <summary>
        /// ウィンドウの設定を取得します。
        /// </summary>
        public WindowOptions WindowOptions
        {
            get;
            private set;
        }

        /// <summary>
        /// デスクトップウィンドウを取得します。
        /// </summary>
        public IDesktopWindow Window
        {
            get;
            private set;
        }

        #endregion

        #region IPlatform

        /// <summary>
        /// 実行中のアプリケーションを取得します。
        /// </summary>
        public AppRunner App
        {
            get;
            private set;
        }

        /// <summary>
        /// 画面のスケールを取得します。
        /// </summary>
        /// <value>
        /// Windows環境では常に <c>1.0f</c> を返します。
        /// </value>
        public float ScreenScale
        {
            get
            {
                return 1.0f;
            }
        }

        /// <summary>
        /// オーディオ設定を取得します。
        /// </summary>
        public AudioOptions AudioOptions
        {
            get;
            private set;
        }

        #endregion

        /// <summary>
        /// <see cref="WindowsPlatform"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public WindowsPlatform()
        {
            WindowOptions = new WindowOptions();
            AudioOptions = new AudioOptions();
            RuntimeHost.Init(new GLES30Bindings());
            AudioHost.Init(new ALBindings(), new AlcBindings());
        }

        /// <summary>
        /// 指定したアプリケーションを実行します。
        /// </summary>
        /// <param name="app">実行するアプリケーション。</param>
        public void Run(AppRunner app)
        {
            App = app;
            AudioHost.CreateDeviceContext(AudioOptions);
            Window = new GLWindow(app, WindowOptions);
            app.Init(new CrossPlatform(this), WindowOptions.Width, WindowOptions.Height);
            Window.Run(60);
        }

        /// <summary>
        /// 管理対象および管理対象外のリソースを解放します。
        /// </summary>
        /// <param name="disposing">
        /// 管理対象リソースを解放する場合は <see langword="true"/>。
        /// </param>
        protected override void Disposing(bool disposing)
        {
            AudioHost.Terminate();
            Window.Dispose();
        }

        /// <summary>
        /// メッセージボックスを表示します。
        /// </summary>
        /// <param name="args">メッセージボックスの設定。</param>
        /// <returns>
        /// ユーザーが選択したダイアログ結果。
        /// </returns>
        public Shared.DialogResult ShowMessage(MessageDialogEventArgs args)
        {
            var button = 
                args.Buttons == Shared.MessageBoxButtons.OKCancel ? System.Windows.Forms.MessageBoxButtons.OKCancel :
                args.Buttons == Shared.MessageBoxButtons.YesNo ? System.Windows.Forms.MessageBoxButtons.YesNo :
                args.Buttons == Shared.MessageBoxButtons.YesNoCancel ? System.Windows.Forms.MessageBoxButtons.YesNoCancel :
                System.Windows.Forms.MessageBoxButtons.OK;
            var result = MessageBox.Show(args.Message, args.Title, button);
            return
                result == System.Windows.Forms.DialogResult.OK ? Shared.DialogResult.OK :
                result == System.Windows.Forms.DialogResult.Cancel ? Shared.DialogResult.Cancel :
                result == System.Windows.Forms.DialogResult.Yes ? Shared.DialogResult.Yes : Shared.DialogResult.No;
        }

    }

}
