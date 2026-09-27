using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Diagnostics
{


    /// <summary>
    /// ログメッセージの種類を表します。
    /// </summary>
    public enum LogMessageType
    {
        /// <summary>
        /// デバッグ情報を表します。
        /// </summary>
        Debug,

        /// <summary>
        /// 一般的な情報を表します。
        /// </summary>
        Info,

        /// <summary>
        /// 警告を表します。
        /// </summary>
        Warning,

        /// <summary>
        /// エラーを表します。
        /// </summary>
        Error,
    }

}
