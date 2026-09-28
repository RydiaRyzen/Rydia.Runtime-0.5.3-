using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Desktop
{

    /// <summary>
    /// ウィンドウの表示方法を指定します。
    /// </summary>
    public enum ScreenMode
    {
        /// <summary>
        /// ウィンドウモードで実行します。
        /// ユーザーによるウィンドウサイズの変更が可能です。
        /// </summary>
        Window,

        /// <summary>
        /// ウィンドウモードで実行します。
        /// ウィンドウサイズは固定され、ユーザーによる変更はできません。
        /// </summary>
        FixedWindow,

        /// <summary>
        /// ウィンドウモードで実行します。
        /// ウィンドウの枠線を非表示にし、画面全体を覆います。
        /// </summary>
        FullWindow,

        /// <summary>
        /// フルスクリーンモードで実行します。
        /// ユーザーのデスクトップで現在使用されている画面解像度を使用します。
        /// </summary>
        Fullscreen
    }
}
