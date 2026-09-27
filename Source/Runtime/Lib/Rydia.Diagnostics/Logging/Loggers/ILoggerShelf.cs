using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Diagnostics;
using Rydia;

namespace Rydia.Diagnostics
{

    public interface ILoggerShelf
    {

        IEnumerable<ILogListener> Listeners
        {
            get;
        }

        void AddListener(ILogListener listener);

        void RemoveListener(ILogListener listener);

        /// <summary>
        /// Increases the current log entry indent.
        /// </summary>
        void PushIndent();

        /// <summary>
        /// Decreases the current log entry indent.
        /// </summary>
        void PopIndent();

        void Log(ILogger logger, LogMessageType level, string format, params object[] args);
        

    }
}
