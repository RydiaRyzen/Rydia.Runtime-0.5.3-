using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    public abstract class TextWriterLogListener : LogListener
    {

        protected TextWriterLogListener(System.IO.TextWriter writer)
        {
            Target = writer;
        }

        public System.IO.TextWriter Target { get; set; }

        protected override void WriteLine(ILogMessage message, string formattedLine)
        {
            Target.WriteLine(formattedLine);
        }

    }
}
