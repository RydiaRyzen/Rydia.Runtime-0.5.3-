using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// [プラットフォーム依存] コンソールにログを出力するログリスナーを提供します。
    /// </summary>
    public class ConsoleLogListener : TextWriterLogListener
    {

        private string[] lastLogLines = new string[3];
        private int lastLogLineIndex = 0;

        /// <summary>
        /// <see cref="ConsoleLogListener"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ConsoleLogListener()
            : base(Console.Out)
        {
        }

        /// <summary>
        /// ログメッセージを整形してコンソールに出力します。
        /// </summary>
        /// <param name="message">出力するログメッセージを指定します。</param>
        /// <param name="formattedLine">整形済みのログメッセージを指定します。</param>
        protected override void WriteLine(ILogMessage message, string formattedLine)
        {
            ConsoleColor clrBg = Console.BackgroundColor;
            ConsoleColor clrFg = Console.ForegroundColor;

            bool highlight = IsHighlightLine(message.Source, formattedLine);

            // If we're writing the same kind of text again, "grey out" the repeating parts
            int beginGreyLength = 0;
            int endGreyLength = 0;
            if (!highlight)
            {
                for (int i = 0; i < this.lastLogLines.Length; i++)
                {
                    string lastLogLine = this.lastLogLines[i] ?? string.Empty;
                    beginGreyLength = Math.Max(beginGreyLength, GetEqualBeginChars(lastLogLine, formattedLine));
                    endGreyLength = Math.Max(endGreyLength, GetEqualEndChars(lastLogLine, formattedLine));
                }
                if (beginGreyLength == formattedLine.Length)
                    endGreyLength = 0;
                if (beginGreyLength + endGreyLength >= formattedLine.Length)
                    endGreyLength = 0;
            }

            // Dark beginning
            if (beginGreyLength != 0)
            {
                SetDarkConsoleColor(message.Level);
                Target.Write(formattedLine.Substring(0, beginGreyLength));
            }

            // Bright main part
            SetBrightConsoleColor(message.Level, highlight);
            Target.Write(formattedLine.Substring(beginGreyLength, formattedLine.Length - beginGreyLength - endGreyLength));

            // Dark ending
            if (endGreyLength != 0)
            {
                SetDarkConsoleColor(message.Level);
                Target.Write(formattedLine.Substring(formattedLine.Length - endGreyLength, endGreyLength));
            }

            // End the current line
            Target.WriteLine();

            this.lastLogLines[this.lastLogLineIndex] = formattedLine;
            this.lastLogLineIndex = (this.lastLogLineIndex + 1) % this.lastLogLines.Length;
            Console.ForegroundColor = clrFg;
            Console.BackgroundColor = clrBg;
        }

        /// <summary>
        /// ログレベルに応じて、コンソールの暗い文字色を設定します。
        /// </summary>
        /// <param name="type">ログメッセージの種類を指定します。</param>
        private void SetDarkConsoleColor(LogMessageType type)
        {
            switch (type)
            {
                default:
                case LogMessageType.Debug: Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case LogMessageType.Info: Console.ForegroundColor = ConsoleColor.DarkBlue; break;
                case LogMessageType.Warning: Console.ForegroundColor = ConsoleColor.DarkYellow; break;
                case LogMessageType.Error: Console.ForegroundColor = ConsoleColor.DarkRed; break;
            }
        }

        /// <summary>
        /// ログレベルと強調表示の状態に応じて、コンソールの明るい文字色を設定します。
        /// </summary>
        /// <param name="type">ログメッセージの種類を指定します。</param>
        /// <param name="highlight">ログを強調表示する場合は <c>true</c>、それ以外の場合は <c>false</c> を指定します。</param>
        private void SetBrightConsoleColor(LogMessageType type, bool highlight)
        {
            switch (type)
            {
                default:
                case LogMessageType.Debug: Console.ForegroundColor = ConsoleColor.Gray; break;
                case LogMessageType.Info:
                    Console.ForegroundColor = highlight ?
                                                                       ConsoleColor.White :
                                                                       ConsoleColor.Gray; break;
                case LogMessageType.Warning: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case LogMessageType.Error: Console.ForegroundColor = ConsoleColor.Red; break;
            }
        }

        /// <summary>
        /// 指定されたログ行を強調表示するかどうかを判定します。
        /// </summary>
        /// <param name="source">ログの出力元を指定します。</param>
        /// <param name="line">判定対象のログ行を指定します。</param>
        /// <returns>
        /// ログ行を強調表示する場合は <c>true</c>、それ以外の場合は <c>false</c> を返します。
        /// </returns>
        private bool IsHighlightLine(ILogger source, string line)
        {
            // If it's an indented line, don't highlight it
            if (Indent != 0) return false;

            // If the line ends with three dots, assume that it's the header of a series of actions
            if (line.EndsWith("...")) return true;

            return false;
        }

        /// <summary>
        /// 2つの文字列の先頭から一致している文字数を取得します。
        /// </summary>
        /// <param name="a">比較する最初の文字列を指定します。</param>
        /// <param name="b">比較する2番目の文字列を指定します。</param>
        /// <returns>先頭から一致している文字数を返します。</returns>
        private int GetEqualBeginChars(string a, string b)
        {
            int minLen = Math.Min(a.Length, b.Length);
            int lastBreakCount = 0;
            int i = 0;
            int j = 0;
            while (i < a.Length && j < b.Length)
            {
                // 空白やインデントをスキップします。
                if (a[i] == ' ') { ++i; continue; }
                if (b[j] == ' ') { ++j; lastBreakCount = j; continue; }

                if (a[i] != b[j])
                    return lastBreakCount;

                if (!char.IsLetterOrDigit(b[j]))
                    lastBreakCount = j + 1;

                ++i;
                ++j;
            }
            return minLen;
        }

        /// <summary>
        /// 2つの文字列の末尾から一致している文字数を取得します。
        /// </summary>
        /// <param name="a">比較する最初の文字列を指定します。</param>
        /// <param name="b">比較する2番目の文字列を指定します。</param>
        /// <returns>末尾から一致している文字数を返します。</returns>
        private int GetEqualEndChars(string a, string b)
        {
            int minLen = Math.Min(a.Length, b.Length);
            int lastBreakCount = 0;
            for (int i = 0; i < minLen; i++)
            {
                if (a[a.Length - 1 - i] != b[b.Length - 1 - i])
                    return lastBreakCount;
                if (!char.IsLetterOrDigit(a[a.Length - 1 - i]))
                    lastBreakCount = i + 1;
            }
            return minLen;
        }

    }
}
