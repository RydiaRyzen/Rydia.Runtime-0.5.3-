using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{
    /// <summary>
    /// ログメッセージの情報を提供するインターフェースです。
    /// </summary>
    public interface ILogMessage
    {

        /// <summary>
        /// ログメッセージの出力元となるロガーを取得します。
        /// </summary>
        ILogger Source
        {
            get;
        }

        /// <summary>
        /// ログメッセージのレベルを取得します。
        /// </summary>
        LogMessageType Level
        {
            get;
        }

        /// <summary>
        /// ログメッセージが記録された UTC の日時を取得します。
        /// </summary>
        DateTime TimeStamp
        {
            get;
        }

        /// <summary>
        /// ログメッセージの本文を取得します。
        /// </summary>
        string Message
        {
            get;
        }

        /// <summary>
        /// ログメッセージの呼び出し元情報を取得します。
        /// </summary>
        CallerInfo CallerInfo
        {
            get;
        }

    }

}
