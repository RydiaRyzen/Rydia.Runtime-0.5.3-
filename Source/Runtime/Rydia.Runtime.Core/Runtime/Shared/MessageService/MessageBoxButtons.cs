using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    /// <summary>
    /// メッセージボックスに表示するボタンの種類を指定します。
    /// </summary>
    public enum MessageBoxButtons
    {
        /// <summary>
        /// 「OK」ボタンを表示します。
        /// </summary>
        OK,

        /// <summary>
        /// 「OK」と「キャンセル」ボタンを表示します。
        /// </summary>
        OKCancel,

        /// <summary>
        /// 「はい」と「いいえ」ボタンを表示します。
        /// </summary>
        YesNo,

        /// <summary>
        /// 「はい」、「いいえ」、「キャンセル」ボタンを表示します。
        /// </summary>
        YesNoCancel,
    }

}
