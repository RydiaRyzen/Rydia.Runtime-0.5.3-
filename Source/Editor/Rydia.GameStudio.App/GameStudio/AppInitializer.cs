using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.GameStudio
{

    internal static class AppInitializer
    {

        public static async Task InitializeAsync(
            Action<string> reportStatus)
        {
            reportStatus("設定ファイルを読み込んでいます...");
            await LoadSettingsAsync();

            reportStatus("リソースを読み込んでいます...");
            await LoadResourcesAsync();

            reportStatus("初期化しています...");
            await InitializeServicesAsync();

            reportStatus("起動準備完了");
        }

        private static async Task LoadSettingsAsync()
        {
            await Task.Delay(3000);

            // TODO: 設定ファイルの読み込み処理
        }

        private static async Task LoadResourcesAsync()
        {
            await Task.Delay(5000);

            // TODO: 画像、フォントなどの読み込み処理
        }

        private static async Task InitializeServicesAsync()
        {
            await Task.Delay(6000);

            // TODO: 各種サービスの初期化処理
        }

    }

}
