using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Diagnostics;
using Rydia;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// ログリスナーを管理し、ログを出力する機能を提供するインターフェースです。
    /// </summary>
    public interface ILoggerShelf
    {

        /// <summary>
        /// 登録されているログリスナーのコレクションを取得します。
        /// </summary>
        IEnumerable<ILogListener> Listeners
        {
            get;
        }

        /// <summary>
        /// 指定されたログリスナーを登録します。
        /// </summary>
        /// <param name="listener">登録するログリスナーです。</param>
        void AddListener(ILogListener listener);

        /// <summary>
        /// 指定されたログリスナーの登録を解除します。
        /// </summary>
        /// <param name="listener">登録を解除するログリスナーです。</param>
        void RemoveListener(ILogListener listener);

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階増加させます。
        /// </summary>
        void PushIndent();

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階減少させます。
        /// </summary>
        void PopIndent();

        /// <summary>
        /// 指定されたロガーを使用してログメッセージを出力します。
        /// </summary>
        /// <param name="logger">ログの出力に使用するロガーです。</param>
        /// <param name="level">ログメッセージのレベルです。</param>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        void Log(ILogger logger, LogMessageType level, string format, params object[] args);


    }
}
