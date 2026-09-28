using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    /// <summary>
    /// <see cref="IMessageService"/> の拡張メソッドを提供します。
    /// </summary>
    public static class IMessageServiceExtensions
    {

        /// <summary>
        /// 指定したメッセージ、タイトル、およびボタンを使用してメッセージダイアログを表示します。
        /// </summary>
        /// <param name="service">メッセージサービスを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="buttons">表示するボタンの種類を指定します。</param>
        /// <returns>ユーザーが選択したダイアログの結果を返します。</returns>
        public static DialogResult ShowMessage(this IMessageService service, string message, string title, MessageBoxButtons buttons)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message, title, buttons));
        }

        /// <summary>
        /// 指定したメッセージとボタンを使用してメッセージダイアログを表示します。
        /// </summary>
        /// <param name="service">メッセージサービスを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="buttons">表示するボタンの種類を指定します。</param>
        /// <returns>ユーザーが選択したダイアログの結果を返します。</returns>
        public static DialogResult ShowMessage(this IMessageService service, string message, MessageBoxButtons buttons)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message, buttons));
        }

        /// <summary>
        /// 指定したメッセージとタイトルを使用してメッセージダイアログを表示します。
        /// </summary>
        /// <param name="service">メッセージサービスを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <returns>ユーザーが選択したダイアログの結果を返します。</returns>
        public static DialogResult ShowMessage(this IMessageService service, string message, string title)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message, title));
        }

        /// <summary>
        /// 指定したメッセージを使用してメッセージダイアログを表示します。
        /// </summary>
        /// <param name="service">メッセージサービスを指定します。</param>
        /// <param name="message">表示するメッセージを指定します。</param>
        /// <returns>ユーザーが選択したダイアログの結果を返します。</returns>
        public static DialogResult ShowMessage(this IMessageService service, string message)
        {
            return service.ShowMessage(new MessageDialogEventArgs(message));
        }

    }

}
