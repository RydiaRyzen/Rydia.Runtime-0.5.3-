using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Diagnostics;
using Rydia;


namespace Rydia.Diagnostics
{
    public class LoggerShelf : ILoggerShelf
    {

        private List<ILogMessage> _Messages = new List<ILogMessage>();
        private object syncRoot = new object();
        private List<ILogListener> _Listeners = new List<ILogListener>();

        public IEnumerable<ILogListener> Listeners
        {
            get
            {
                return this._Listeners;
            }
        }

        public IReadOnlyList<ILogMessage> InMemoryMessages
        {
            get
            {
                return this._Messages;
            }
        }

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
        /// Increases the current log entry indent.
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
        /// Decreases the current log entry indent.
        /// </summary>
        public void PopIndent()
        {
            lock (this.syncRoot)
            {
                foreach (ILogListener target in Listeners)
                    target.PopIndent();
            }
        }

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

        public LoggerShelf()
        {
            DebugLog = new DebugLogListener();
            AddListener(DebugLog);
        }

        public DebugLogListener DebugLog
        {
            get;
        }

    }
}
