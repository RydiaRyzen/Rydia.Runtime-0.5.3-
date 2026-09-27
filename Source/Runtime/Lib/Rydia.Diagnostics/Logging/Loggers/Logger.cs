using System.Collections.Generic;

namespace Rydia.Diagnostics
{

    public abstract class Logger : ILogger
    {

        public string Module
        {
            get;
            private set;
        }

        /// <summary>
        /// <see cref="SingleLogger"/>はModule名をPrefixとして使用します。
        /// <see cref="GroupLogger"/>はModule名をからPrefixとして使用します。
        /// </summary>
        public abstract string Prefix
        {
            get;
        }

        protected ILoggerShelf Shelf
        {
            get;
            set;
        }

        protected Logger(string module)
        {
            Module = module;
        }

        /// <summary>
        /// Increases the current log entry indent.
        /// </summary>
        public void PushIndent()
        {
            Shelf.PushIndent();
        }

        /// <summary>
        /// Decreases the current log entry indent.
        /// </summary>
        public void PopIndent()
        {
            Shelf.PopIndent();
        }

        public void Debug(string format, params object[] args)
        {
            Log(LogMessageType.Debug, format, args);
        }

        public void Info(string format, params object[] args)
        {
            Log(LogMessageType.Info, format, args);
        }

        public void Warning(string format, params object[] args)
        {
            Log(LogMessageType.Warning, format, args);
        }

        public void Error(string format, params object[] args)
        {
            Log(LogMessageType.Error, format, args);
        }

        protected void Log(LogMessageType level, string format, params object[] args)
        {
            Shelf.Log(this, level, format, args);
        }

    }

}