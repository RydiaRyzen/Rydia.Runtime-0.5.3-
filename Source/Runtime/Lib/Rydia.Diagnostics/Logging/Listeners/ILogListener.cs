using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// ログ出力を受け取るリスナーのインターフェースです。
    /// </summary>
    public interface ILogListener
    {

        /// <summary>
        /// 以降のログメッセージの出力時に使用するインデントレベルを1段階増加させます。
        /// </summary>
        void PushIndent();

        /// <summary>
        /// 以降のログメッセージの出力時に使用するインデントレベルを1段階減少させます。
        /// </summary>
        void PopIndent();

        /// <summary>
        /// 指定されたログメッセージを出力します。
        /// </summary>
        /// <param name="message">出力するログメッセージです。</param>
        void Log(ILogMessage message);

    }

}
