using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Rydia.Diagnostics
{

    public static class GlobalLogger
    {

        private static GroupLogger Shelf
        {
            get;
        }

        /// <summary>
        /// <see cref="Rydia.Runtime"/>アセンブリから参照される<see cref="ILogger"/>を取得します
        /// </summary>
        public static ILogger Core
        {
            get
            {
                return Shelf.GetLogger("Core");
            }
        }

        public static ILogger Profiling
        {
            get
            {
                return Shelf.GetLogger("Profile");
            }
        }

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

        public static void AddListener(ILogListener listener)
        {
            ((ILoggerShelf)Shelf).AddListener(listener);
        }

        public static void RemoveListener(ILogListener listener)
        {
            ((ILoggerShelf)Shelf).RemoveListener(listener);
        }

        public static void PushIndent()
        {
            ((ILoggerShelf)Shelf).PushIndent();
        }

        public static void PopIndent()
        {
            ((ILoggerShelf)Shelf).PopIndent();
        }

        public static void Log(ILogger logger, LogMessageType level, string format, params object[] args)
        {
            ((ILoggerShelf)Shelf).Log(logger, level, format, args);
        }

        public static ILogger GetLogger(string name)
        {
            return Shelf.GetLogger(name);
        }

    }

}
