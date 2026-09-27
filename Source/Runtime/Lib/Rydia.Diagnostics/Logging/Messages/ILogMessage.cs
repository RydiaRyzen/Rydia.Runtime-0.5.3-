using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{
    public interface ILogMessage
    {

        ILogger Source
        {
            get;
        }

        LogMessageType Level
        {
            get;
        }

        /// <summary>
        /// [GET] The messages timestamp in UTC.
        /// </summary>
        DateTime TimeStamp
        {
            get;
        }

        string Message
        {
            get;
        }

        CallerInfo CallerInfo
        {
            get;
        }

    }

}
