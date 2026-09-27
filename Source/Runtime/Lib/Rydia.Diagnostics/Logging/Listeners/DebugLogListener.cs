using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    public class DebugLogListener : LogListener
    {

        internal DebugLogListener()
        {

        }

#if DEBUG

        protected override void WriteLine(ILogMessage message, string formattedLine)
        {
            Debug.WriteLine(formattedLine);
        }

#else

        protected override void WriteLine(LogMessageType level, string v)
        {
            
        }

#endif

    }

}
