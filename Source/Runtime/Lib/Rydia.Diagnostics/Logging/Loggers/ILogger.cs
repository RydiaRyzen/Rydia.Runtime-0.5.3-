using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{
    public interface ILogger
    {

        string Module
        {
            get;
        }

        string Prefix
        {
            get;
        }

        /// <summary>
        /// Increases the current log entry indent.
        /// </summary>
        void PushIndent();

        /// <summary>
        /// Decreases the current log entry indent.
        /// </summary>
        void PopIndent();

        void Debug(string format, params object[] args);

        void Info(string format, params object[] args);

        void Warning(string format, params object[] args);

        void Error(string format, params object[] args);

    }
}
