using System;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{
    /// <summary>
    /// ログメッセージを表します。
    /// </summary>
    public class LogMessage : ILogMessage
    {

        /// <summary>
        /// ログメッセージの出力元となるロガーを取得します。
        /// </summary>
        public ILogger Source
        {
            get;
        }

        /// <summary>
        /// ログメッセージのレベルを取得します。
        /// </summary>
        public LogMessageType Level
        {
            get;
        }

        /// <summary>
        /// ログメッセージが記録された UTC の日時を取得します。
        /// </summary>
        public DateTime TimeStamp
        {
            get;
        }

        /// <summary>
        /// ログメッセージの本文を取得します。
        /// </summary>
        public string Message
        {
            get;
        }

        /// <summary>
        /// ログメッセージの呼び出し元情報を取得します。
        /// </summary>
        public CallerInfo CallerInfo
        {
            get;
        }

        /// <summary>
        /// 指定された情報を使用して、<see cref="LogMessage"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="logger">ログメッセージの出力元となるロガーです。</param>
        /// <param name="level">ログメッセージのレベルです。</param>
        /// <param name="message">ログメッセージの本文です。</param>
        /// <param name="callerInfo">ログメッセージの呼び出し元情報です。</param>
        public LogMessage(ILogger logger, LogMessageType level, string message, Rydia.Diagnostics.CallerInfo callerInfo)
        {
            Source = logger;
            Level = level;
            TimeStamp = DateTime.UtcNow;
            Message = message;
            CallerInfo = callerInfo;
        }


    }
}