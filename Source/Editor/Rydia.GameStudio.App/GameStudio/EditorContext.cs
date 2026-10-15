using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.GameStudio.PluginSystem;

namespace Rydia.GameStudio
{

    /// <summary>
    /// エディターのリソースを管理するクラスです
    /// </summary>
    public static class EditorContext
    {

        private static EditorPluginLoader pluginLoader;

        public static EditorPluginLoader PluginLoader
        {
            get
            {
                return pluginLoader;
            }
        }

        internal static void LoadSettings()
        {
            // TODO: 設定ファイルの読み込み処理
        }

        internal static void LoadPlugin()
        {
            // TODO: プラグインの読み込み処理
            pluginLoader = new EditorPluginLoader();
            pluginLoader.LoadPlugins();
        }

        internal static void LoadResorces()
        {
            // TODO: 画像、フォントなどの読み込み処理

        }

        internal static void LoadServices()
        {
            // TODO: 各種サービスの初期化処理
        }

    }

}
