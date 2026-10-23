

using Rydia.Engine;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.Windows;

namespace Rydia.GameStudio.App
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
            ApplicationConfiguration.Initialize();
            
            using (var splash = new SplashForm())
            {
                if (splash.ShowDialog() != DialogResult.OK)
                    return;
                
                using (var hub = new HubForm())
                {
                    if (hub.ShowDialog() != DialogResult.OK)
                        return;
                }
                
            }

            // ImGuiによるエディタを起動する場合は、以下のコードをコメントアウトしてください。
            // Application.Run(new MainForm());

            // WinFormsベースのアプリケーションを起動する場合は、以下のコードをコメントアウトしてください。
            using (var platform = new WindowsPlatform())
            {
                platform.WindowOptions.Width = 1024;
                platform.WindowOptions.Height = 768;
                platform.WindowOptions.Title = "Rydia GameStudio - " + EditorContext.ProjectName;
                platform.WindowOptions.RefreshMode = RefreshMode.VSync;
                platform.WindowOptions.ScreenMode = ScreenMode.Window;

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
                platform.Run(new EditorApp());
            }
        }
    }
}