using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.GameStudio
{

    internal static class AppInitializer
    {

        public static async Task InitializeAsync(Action<string> reportStatus)
        {
            reportStatus("初期化しています...");
            await InitializeServicesAsync();

            reportStatus("設定ファイルを読み込んでいます...");
            await LoadSettingsAsync();

            reportStatus("アップデートを確認しています...");
            await LoadUpdater();

            reportStatus("プラグインを読み込んでいます...");
            await LoadPluginAsync();

            reportStatus("リソースを読み込んでいます...");
            await LoadResourcesAsync();

            reportStatus("起動準備完了");
            await Task.Delay(3000);
        }

        private static async Task LoadUpdater()
        {
            EditorContext.LoadUpdater();
            await Task.Delay(3000);
        }

        private static async Task LoadSettingsAsync()
        {
            EditorContext.LoadSettings();
            await Task.Delay(3000);
        }

        private static async Task LoadPluginAsync()
        {
            EditorContext.LoadPlugin();
            await Task.Delay(3000);
        }

        private static async Task LoadResourcesAsync()
        {
            EditorContext.LoadResorces();
            await Task.Delay(3000);
        }

        private static async Task InitializeServicesAsync()
        {
            // Pluginフォルダーの確認
            if(!System.IO.Directory.Exists("Plugins"))
            {
                System.IO.Directory.CreateDirectory("Plugins");
            }
            // Projectフォルダーの確認
            if (!System.IO.Directory.Exists("Projects"))
            {
                System.IO.Directory.CreateDirectory("Projects");
            }
            // Downloadフォルダーの確認
            if (!System.IO.Directory.Exists("Download"))
            {
                System.IO.Directory.CreateDirectory("Download");
            }
            // Serviceの初期化
            EditorContext.LoadServices();
            await Task.Delay(3000);
        }

    }

}
