using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Shared
{

    /// <summary>
    /// メッセージダイアログの表示内容を定義します。
    /// </summary>
    public class MessageDialogEventArgs
    {

        /// <summary>
        /// ダイアログに表示するメッセージを取得または設定します。
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// ダイアログのタイトルを取得または設定します。
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// ダイアログに表示するボタンの種類を取得または設定します。
        /// </summary>
        public MessageBoxButtons Buttons
        {
            get;
            set;
        }

        /// <summary>
        /// <see cref="MessageDialogEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">ダイアログに表示するメッセージを指定します。</param>
        /// <remarks>
        /// タイトルには「Rydia Engine」を使用し、「OK」ボタンを表示します。
        /// </remarks>
        public MessageDialogEventArgs(string message)
            : this(message, "Rydia Engine", MessageBoxButtons.OK)
        {

        }

        /// <summary>
        /// <see cref="MessageDialogEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">ダイアログに表示するメッセージを指定します。</param>
        /// <param name="buttons">ダイアログに表示するボタンの種類を指定します。</param>
        /// <remarks>
        /// タイトルには「Rydia Engine」を使用します。
        /// </remarks>
        public MessageDialogEventArgs(string message, MessageBoxButtons buttons)
            : this(message, "Rydia Engine", buttons)
        {

        }

        /// <summary>
        /// <see cref="MessageDialogEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">ダイアログに表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <remarks>
        /// 「OK」ボタンを表示します。
        /// </remarks>
        public MessageDialogEventArgs(string message, string title)
            : this(message, title, MessageBoxButtons.OK)
        {

        }

        /// <summary>
        /// <see cref="MessageDialogEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">ダイアログに表示するメッセージを指定します。</param>
        /// <param name="title">ダイアログのタイトルを指定します。</param>
        /// <param name="buttons">ダイアログに表示するボタンの種類を指定します。</param>
        public MessageDialogEventArgs(string message, string title, MessageBoxButtons buttons)
        {
            Message = message;
            Title = title;
            Buttons = buttons;
        }

    }

}
