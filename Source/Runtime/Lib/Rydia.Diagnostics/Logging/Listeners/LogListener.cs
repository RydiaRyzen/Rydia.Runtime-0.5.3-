using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    public abstract class LogListener : ILogListener
    {

        private static readonly char[] LineEndingChars = new[] { '\n', '\r', '\0' };

        private int indent = 0;
        private object builderLock = new object();
        private object writerLock = new object();
        private StringBuilder builder = new StringBuilder();

        public int Indent
        {
            get { return this.indent; }
        }

        public void Log(ILogMessage message)
        {
            string[] lines = BuildLines(ref message);

            lock (this.writerLock)
            {
                for (int i = 0; i < lines.Length; i++)
                    WriteLine(message, lines[i]);
            }
        }

        protected abstract void WriteLine(ILogMessage message, string formattedLine);

        private string[] BuildLines(ref ILogMessage entry)
        {
            string prefix = entry.Source.Prefix;
            string[] lines = entry.Message.Split(
                LineEndingChars, StringSplitOptions.RemoveEmptyEntries);
            lock (this.builderLock)
            {
                int headerLength = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    this.builder.Clear();
                    if (i == 0)
                    {
                        // Channel / source prefix
                        this.builder.Append(prefix);
                        this.builder.Append(' ');

                        // Message type
                        switch (entry.Level)
                        {
                            case LogMessageType.Debug: this.builder.Append(  "Debug  : "); break;
                            case LogMessageType.Info: this.builder.Append(   "Info   : "); break;
                            case LogMessageType.Warning: this.builder.Append("Warning: "); break;
                            case LogMessageType.Error: this.builder.Append(  "Error  : "); break;
                        }

                        // Indentation
                        this.builder.Append(' ', this.indent * 2);

                        headerLength = this.builder.Length;
                    }
                    else
                    {
                        this.builder.Append(' ', headerLength);
                    }
                    // Message line
                    this.builder.Append(lines[i]);

                    lines[i] = this.builder.ToString();
                }
            }
            return lines;
        }

        public void PopIndent()
        {
            Interlocked.Increment(ref this.indent);
        }

        public void PushIndent()
        {
            Interlocked.Decrement(ref this.indent);
        }

    }


}
