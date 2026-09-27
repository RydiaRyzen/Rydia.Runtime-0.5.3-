using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// アプリケーション全体で使用するロガーを提供するクラスです。
    /// </summary>
    public static class GlobalLogger
    {

        private static GroupLogger Shelf
        {
            get;
        }

        /// <summary>
        /// <see cref="Rydia.Runtime"/> アセンブリから使用される
        /// <see cref="ILogger"/> を取得します。
        /// </summary>
        public static ILogger Core
        {
            get
            {
                return Shelf.GetLogger("Core");
            }
        }

        /// <summary>
        /// プロファイリング用の <see cref="ILogger"/> を取得します。
        /// </summary>
        public static ILogger Profiling
        {
            get
            {
                return Shelf.GetLogger("Profile");
            }
        }

        /// <summary>
        /// 登録されているすべてのログリスナーを取得します。
        /// </summary>
        public static IEnumerable<ILogListener> Listeners
        {
            get
            {
                return ((ILoggerShelf)Shelf).Listeners;
            }
        }

        static GlobalLogger()
        {
            Shelf = new GroupLogger();
        }

        /// <summary>
        /// 指定されたログリスナーを登録します。
        /// </summary>
        /// <param name="listener">登録するログリスナーです。</param>
        public static void AddListener(ILogListener listener)
        {
            ((ILoggerShelf)Shelf).AddListener(listener);
        }

        /// <summary>
        /// 指定されたログリスナーの登録を解除します。
        /// </summary>
        /// <param name="listener">登録を解除するログリスナーです。</param>
        public static void RemoveListener(ILogListener listener)
        {
            ((ILoggerShelf)Shelf).RemoveListener(listener);
        }

        /// <summary>
        /// 以降のログメッセージのインデントレベルを1段階増加させます。
        /// </summary>
        public static void PushIndent()
        {
            ((ILoggerShelf)Shelf).PushIndent();
        }

        /// <summary>
        /// 以降のログメッセージのインデントレベルを1段階減少させます。
        /// </summary>
        public static void PopIndent()
        {
            ((ILoggerShelf)Shelf).PopIndent();
        }

        /// <summary>
        /// 指定されたロガーを使用してログメッセージを出力します。
        /// </summary>
        /// <param name="logger">ログの出力に使用するロガーです。</param>
        /// <param name="level">ログメッセージのレベルです。</param>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        public static void Log(ILogger logger, LogMessageType level, string format, params object[] args)
        {
            ((ILoggerShelf)Shelf).Log(logger, level, format, args);
        }

        /// <summary>
        /// 指定された名前のロガーを取得します。
        /// </summary>
        /// <param name="name">取得するロガーの名前です。</param>
        /// <returns>指定された名前の <see cref="ILogger"/> を返します。</returns>
        public static ILogger GetLogger(string name)
        {
            return Shelf.GetLogger(name);
        }

    }

}
