using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{

    /// <summary>
    /// 単一のログ出力先を管理するロガーです。
    /// </summary>
    public sealed class SingleLogger : Logger
    {

        /// <summary>
        /// 指定されたモジュール名を使用して、
        /// <see cref="SingleLogger"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="module">ロガーのモジュール名です。</param>
        public SingleLogger(string module)
            : base(module)
        {
            Shelf = new LoggerShelf();
        }

        /// <summary>
        /// ログ出力時に使用するプレフィックスを取得します。
        /// </summary>
        /// <value>
        /// モジュール名を表すプレフィックスです。
        /// </value>
        public override string Prefix
        {
            get
            {
                return Module;
            }
        }

    }
}
