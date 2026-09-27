using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// ログメッセージを受け取り、ログ出力を行うための基底クラスです。
    /// </summary>
    public abstract class LogListener : ILogListener
    {

        private static readonly char[] LineEndingChars = new[] { '\n', '\r', '\0' };

        private int indent = 0;
        private object builderLock = new object();
        private object writerLock = new object();
        private StringBuilder builder = new StringBuilder();

        /// <summary>
        /// 現在のインデントレベルを取得します。
        /// </summary>
        public int Indent
        {
            get { return this.indent; }
        }

        /// <summary>
        /// 指定されたログメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するログメッセージです。</param>
        public void Log(ILogMessage message)
        {
            string[] lines = BuildLines(ref message);

            lock (this.writerLock)
            {
                for (int i = 0; i < lines.Length; i++)
                    WriteLine(message, lines[i]);
            }
        }

        /// <summary>
        /// 指定されたログメッセージの1行を出力します。
        /// </summary>
        /// <param name="message">出力するログメッセージです。</param>
        /// <param name="formattedLine">整形済みのログメッセージです。</param>
        protected abstract void WriteLine(ILogMessage message, string formattedLine);

        /// <summary>
        /// 指定されたログメッセージを行単位に分割し、出力用に整形します。
        /// </summary>
        /// <param name="entry">整形するログメッセージです。</param>
        /// <returns>
        /// 整形されたログメッセージの各行を格納した配列を返します。
        /// </returns>
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
                            case LogMessageType.Debug: this.builder.Append("Debug  : "); break;
                            case LogMessageType.Info: this.builder.Append("Info   : "); break;
                            case LogMessageType.Warning: this.builder.Append("Warning: "); break;
                            case LogMessageType.Error: this.builder.Append("Error  : "); break;
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

        /// <summary>
        /// インデントレベルを1増加させます。
        /// </summary>
        public void PopIndent()
        {
            Interlocked.Increment(ref this.indent);
        }

        /// <summary>
        /// インデントレベルを1減少させます。
        /// </summary>
        public void PushIndent()
        {
            Interlocked.Decrement(ref this.indent);
        }

    }


}
