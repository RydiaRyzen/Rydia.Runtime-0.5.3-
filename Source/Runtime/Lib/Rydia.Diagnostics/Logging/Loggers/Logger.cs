using System.Collections.Generic;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// ロガーの基本機能を提供する抽象基底クラスです。
    /// </summary>
    public abstract class Logger : ILogger
    {

        /// <summary>
        /// このロガーのモジュール名を取得します。
        /// </summary>
        public string Module
        {
            get;
            private set;
        }

        /// <summary>
        /// ログ出力時に使用するプレフィックスを取得します。
        /// <see cref="SingleLogger"/> ではモジュール名がプレフィックスとして使用されます。
        /// <see cref="GroupLogger"/> ではモジュール名を基にプレフィックスが生成されます。
        /// </summary>
        public abstract string Prefix
        {
            get;
        }

        /// <summary>
        /// このロガーが使用するログリスナー管理オブジェクトを取得または設定します。
        /// </summary>
        protected ILoggerShelf Shelf
        {
            get;
            set;
        }

        /// <summary>
        /// 指定されたモジュール名を使用して
        /// <see cref="Logger"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="module">モジュール名です。</param>
        protected Logger(string module)
        {
            Module = module;
        }

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階増加させます。
        /// </summary>
        public void PushIndent()
        {
            Shelf.PushIndent();
        }

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階減少させます。
        /// </summary>
        public void PopIndent()
        {
            Shelf.PopIndent();
        }

        /// <summary>
        /// デバッグレベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        public void Debug(string format, params object[] args)
        {
            Log(LogMessageType.Debug, format, args);
        }

        /// <summary>
        /// 情報レベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        public void Info(string format, params object[] args)
        {
            Log(LogMessageType.Info, format, args);
        }

        /// <summary>
        /// 警告レベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        public void Warning(string format, params object[] args)
        {
            Log(LogMessageType.Warning, format, args);
        }

        /// <summary>
        /// エラーレベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        public void Error(string format, params object[] args)
        {
            Log(LogMessageType.Error, format, args);
        }

        /// <summary>
        /// 指定されたレベルのログメッセージを出力します。
        /// </summary>
        /// <param name="level">ログメッセージのレベルです。</param>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        protected void Log(LogMessageType level, string format, params object[] args)
        {
            Shelf.Log(this, level, format, args);
        }

    }

}