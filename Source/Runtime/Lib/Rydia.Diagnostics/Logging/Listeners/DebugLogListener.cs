using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// デバッグ出力にログメッセージを書き込むログリスナーです。
    /// </summary>
    public class DebugLogListener : LogListener
    {

        internal DebugLogListener()
        {

        }

#if DEBUG

        /// <summary>
        /// 指定されたログメッセージをデバッグ出力に書き込みます。
        /// </summary>
        /// <param name="message">出力するログメッセージです。</param>
        /// <param name="formattedLine">整形済みのログメッセージです。</param>
        protected override void WriteLine(ILogMessage message, string formattedLine)
        {
            Debug.WriteLine(formattedLine);
        }

#else

        /// <summary>
        /// ログメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するログメッセージです。</param>
        /// <param name="formattedLine">整形済みのログメッセージです。</param>
        protected override void WriteLine(LogMessageType level, string v)
        {
            
        }

#endif

    }

}
