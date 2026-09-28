using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    /// <summary>
    /// メッセージダイアログを表示するためのサービスを定義します。
    /// </summary>
    public interface IMessageService
    {

        /// <summary>
        /// メッセージダイアログを表示し、ユーザーが選択した結果を取得します。
        /// </summary>
        /// <param name="args">メッセージダイアログの表示内容を指定します。</param>
        /// <returns>ユーザーが選択したダイアログの結果を返します。</returns>
        DialogResult ShowMessage(MessageDialogEventArgs args);

    }

}
