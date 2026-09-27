using System;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{
    public class LogMessage : ILogMessage
    {

        public ILogger Source
        {
            get;
        }

        public LogMessageType Level
        {
            get;
        }

        /// <summary>
        /// [GET] The messages timestamp in UTC.
        /// </summary>
        public DateTime TimeStamp
        {
            get;
        }

        public string Message
        {
            get;
        }

        public CallerInfo CallerInfo
        {
            get;
        }

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