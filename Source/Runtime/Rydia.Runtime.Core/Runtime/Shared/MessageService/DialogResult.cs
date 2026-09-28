using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    /// <summary>
    /// ダイアログの操作結果を指定します。
    /// </summary>
    public enum DialogResult
    {
        /// <summary>
        /// 操作結果がありません。
        /// </summary>
        None,

        /// <summary>
        /// 「OK」が選択されました。
        /// </summary>
        OK,

        /// <summary>
        /// 「キャンセル」が選択されました。
        /// </summary>
        Cancel,

        /// <summary>
        /// 「はい」が選択されました。
        /// </summary>
        Yes,

        /// <summary>
        /// 「いいえ」が選択されました。
        /// </summary>
        No,
    }

}
