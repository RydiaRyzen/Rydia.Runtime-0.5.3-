using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// <see cref="TextWriter"/> にログメッセージを書き込むログリスナーの基底クラスです。
    /// </summary>
    public abstract class TextWriterLogListener : LogListener
    {

        /// <summary>
        /// 指定された <see cref="TextWriter"/> を使用して
        /// <see cref="TextWriterLogListener"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="writer">ログの出力先となる <see cref="TextWriter"/> です。</param>
        protected TextWriterLogListener(System.IO.TextWriter writer)
        {
            Target = writer;
        }

        /// <summary>
        /// ログの出力先となる <see cref="TextWriter"/> を取得または設定します。
        /// </summary>
        public System.IO.TextWriter Target { get; set; }

        /// <summary>
        /// 指定されたログメッセージを <see cref="Target"/> に書き込みます。
        /// </summary>
        /// <param name="message">出力するログメッセージです。</param>
        /// <param name="formattedLine">整形済みのログメッセージです。</param>
        protected override void WriteLine(ILogMessage message, string formattedLine)
        {
            Target.WriteLine(formattedLine);
        }

    }
}
