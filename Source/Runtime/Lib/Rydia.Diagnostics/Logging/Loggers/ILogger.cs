using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{
    /// <summary>
    /// ログを出力するためのインターフェースです。
    /// </summary>
    public interface ILogger
    {

        /// <summary>
        /// ログのモジュール名を取得します。
        /// </summary>
        string Module
        {
            get;
        }

        /// <summary>
        /// ログ出力時に使用するプレフィックスを取得します。
        /// </summary>
        string Prefix
        {
            get;
        }

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階増加させます。
        /// </summary>
        void PushIndent();

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階減少させます。
        /// </summary>
        void PopIndent();

        /// <summary>
        /// デバッグレベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        void Debug(string format, params object[] args);

        /// <summary>
        /// 情報レベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        void Info(string format, params object[] args);

        /// <summary>
        /// 警告レベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        void Warning(string format, params object[] args);

        /// <summary>
        /// エラーレベルのログを出力します。
        /// </summary>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        void Error(string format, params object[] args);

    }
}
