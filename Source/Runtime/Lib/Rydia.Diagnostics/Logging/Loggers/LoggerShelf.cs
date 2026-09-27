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
    /// ログメッセージを管理し、ログリスナーへ通知するクラスです。
    /// </summary>
    public class LoggerShelf : ILoggerShelf
    {

        private List<ILogMessage> _Messages = new List<ILogMessage>();
        private object syncRoot = new object();
        private List<ILogListener> _Listeners = new List<ILogListener>();

        /// <summary>
        /// 登録されているログリスナーのコレクションを取得します。
        /// </summary>
        public IEnumerable<ILogListener> Listeners
        {
            get
            {
                return this._Listeners;
            }
        }

        /// <summary>
        /// メモリ上に保持されているログメッセージの一覧を取得します。
        /// </summary>
        public IReadOnlyList<ILogMessage> InMemoryMessages
        {
            get
            {
                return this._Messages;
            }
        }

        /// <summary>
        /// 指定されたログリスナーを登録します。
        /// </summary>
        /// <param name="listener">登録するログリスナーです。</param>
        public void AddListener(ILogListener listener)
        {
            lock (this.syncRoot)
            {
                if (!Listeners.Contains(listener))
                {
                    this._Listeners.Add(listener);
                }
            }
        }

        /// <summary>
        /// 指定されたログリスナーの登録を解除します。
        /// </summary>
        /// <param name="listener">登録を解除するログリスナーです。</param>
        public void RemoveListener(ILogListener listener)
        {
            lock (this.syncRoot)
            {
                if (Listeners.Contains(listener))
                {
                    this._Listeners.Remove(listener);
                }
            }
        }

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階増加させます。
        /// </summary>
        public void PushIndent()
        {
            lock (this.syncRoot)
            {
                foreach (ILogListener target in Listeners)
                    target.PushIndent();
            }
        }

        /// <summary>
        /// 現在のログエントリのインデントレベルを1段階減少させます。
        /// </summary>
        public void PopIndent()
        {
            lock (this.syncRoot)
            {
                foreach (ILogListener target in Listeners)
                    target.PopIndent();
            }
        }

        /// <summary>
        /// 指定されたロガーとログレベルを使用してログメッセージを作成し、
        /// 登録されているすべてのログリスナーに通知します。
        /// </summary>
        /// <param name="logger">ログの出力元となるロガーです。</param>
        /// <param name="level">ログメッセージのレベルです。</param>
        /// <param name="format">ログメッセージの書式文字列です。</param>
        /// <param name="args">書式文字列に指定する引数です。</param>
        public void Log(ILogger logger, LogMessageType level, string format, params object[] args)
        {
            var callerInfo = CallerInfo.Extract(args);
            var message = new LogMessage(logger, level, string.Format(format, args), callerInfo);
            foreach (ILogListener listener in Listeners)
            {
                listener.Log(message);
            }
            this._Messages.Add(message);
        }

        /// <summary>
        /// <see cref="LoggerShelf"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public LoggerShelf()
        {
            DebugLog = new DebugLogListener();
            AddListener(DebugLog);
        }

        /// <summary>
        /// デバッグ出力用のログリスナーを取得します。
        /// </summary>
        public DebugLogListener DebugLog
        {
            get;
        }

    }
}
