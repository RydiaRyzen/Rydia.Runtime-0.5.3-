using Rydia.Diagnostics;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.Windows;

namespace Rydia.Engine.Windows.App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            // このアプリケーションはWindowsプラットフォーム専用のアプリケーションであるため、
            // Windowsプラットフォームの初期化を行う
            // Rydia.Engine.Debugging.Appとは違い、デバッグコードを無視するため、
            ApplicationConfiguration.Initialize();
            var ConsoleLog = new ConsoleLogListener();
            GlobalLogger.AddListener(ConsoleLog);

            // LauncherAppではログファイルを作成しないため、
            {
                // var fileLog = new FileLogListener("Logs\\Rydia.Runtime.Windows.App.log");
                // GlobalLogger.AddListener(fileLog);
            }
            // をコメントアウトしている

            var app = new LauncherApp();
            using (var platform = new WindowsPlatform())
            {
                // LauncherAppではアプリケーション設定を読み込んで設定するが、
                // ここではテストコードとしてウィンドウとオーディオの設定をリテラルコードで行っている

                // ウィンドウ設定
                // 最大幅768の[19:9]ウィンドウを作成
                var v = 768;
                var w = 19;
                var h = 9;
                var a = v / w;
                var b = a * h;
                platform.WindowOptions.Width = b;
                platform.WindowOptions.Height = 768;
                platform.WindowOptions.Title = "Rydia Runtime Windows App - " + app.GetType().Name;
                platform.WindowOptions.RefreshMode = RefreshMode.VSync;
                platform.WindowOptions.ScreenMode = ScreenMode.FixedWindow;

                // オーディオ設定
                // 既定のデバイスを使用
                platform.AudioOptions.DeviceName = null;
                // サンプリング周波数を44.1kHzに設定
                platform.AudioOptions.Frequency = 44100;
                // モノラル音源の数を16に設定
                platform.AudioOptions.MonoSources = 16;
                // ステレオ音源の数を16に設定
                platform.AudioOptions.StereoSources = 16;
                // 更新レートを60に設定
                platform.AudioOptions.Refresh = 60;
                // クロック同期を無効に設定
                platform.AudioOptions.Sync = false;
                platform.Run(app);
            }
        }
    }
}