using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    public interface ILogListener
    {

        /// <summary>
		/// Increases the outputs indentation level for the following log messages.
		/// </summary>
		void PushIndent();

        /// <summary>
        /// Decreases the outputs indentation level for the following log messages.
        /// </summary>
        void PopIndent();

        /// <summary>
        /// Writes a single message to the output.
        /// </summary>
        /// <param name="entry">The new log entry that is to be written to the output.</param>
        /// <param name="context">The runtime context object of this log entry.</param>
        /// <param name="source">The <see cref="Log"/> instance that issued the log entry.</param>
        void Log(ILogMessage message);

    }

}
