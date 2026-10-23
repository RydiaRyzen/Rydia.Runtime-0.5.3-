using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// エディタープラグインを読み込むローダーです。
        /// </summary>
        private static EditorPluginLoader pluginLoader;

        /// <summary>
        /// エディタープラグインを読み込むローダーを取得します。
        /// </summary>
        public static EditorPluginLoader PluginLoader
        {
            get
            {
                return pluginLoader;
            }
        }

        /// <summary>
        /// エンジンのアセンブリを取得します。
        /// </summary>
        public static Assembly EngineAssembly
        {
            get;
            private set;
        }

        /// <summary>
        /// エディターのアセンブリを取得します。
        /// </summary>
        public static Assembly EditorAssembly
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在開いているプロジェクトのルートディレクトリを取得します。
        /// </summary>
        public static string ProjectPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在開いているプロジェクトのソリューションファイルのパスを取得します。
        /// </summary>
        public static string SolutionPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在開いているプロジェクトのアセットディレクトリのパスを取得します。
        /// </summary>
        public static string AssetsPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在開いているプロジェクトのメディアディレクトリのパスを取得します。
        /// </summary>
        public static string MediaPath
        {
            get;
            private set;
        }

        /// <summary>
        /// プロジェクト名を取得します
        /// </summary>
        public static string ProjectName
        {
            get;
            private set;
        }

        /// <summary>
        /// エディタープラグインを読み込みます。
        /// </summary>
        internal static void LoadPlugin()
        {
            // TODO: プラグインの読み込み処理
            pluginLoader = new EditorPluginLoader();
            pluginLoader.LoadPlugins();
        }

        /// <summary>
        /// エディターで使用する各種リソースを初期化します。
        /// エンジンとエディターのアセンブリを設定します。
        /// </summary>
        internal static void LoadResources()
        {
            // TODO: 各種サービスの初期化処理
            EngineAssembly = typeof(Rydia.Engine.LauncherApp).Assembly;
            EditorAssembly = typeof(HubForm).Assembly;
        }

        /// <summary>
        /// 指定されたディレクトリのプロジェクトを開き、関連するパスを設定します。
        /// </summary>
        /// <param name="path">開くプロジェクトのルートディレクトリのパス。</param>
        internal static void OpenProject(string path)
        {
            ProjectPath = path;
            ProjectName = Path.GetFileNameWithoutExtension(path);
            SolutionPath = Path.Combine(path, "Source", "Code", ProjectName, Path.GetFileName(path) + ".sln");
            AssetsPath = Path.Combine(path, "Assets");
            MediaPath = Path.Combine(path, "Media");
        }

    }

}
