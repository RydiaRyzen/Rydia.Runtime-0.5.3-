using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// ユーザーからのキーボード入力を提供する入力ソースを表します。
    /// </summary>
    /// <remarks>
    /// 通常は、キーボードなどの入力デバイスを表します。
    /// </remarks>
    public interface IKeyboardInputSource : IUserInputSource
    {

        /// <summary>
        /// 前回の入力更新以降に入力された文字を連結した文字列を取得します。
        /// </summary>
        /// <value>
        /// 入力された文字を連結した文字列です。
        /// </value>
        string CharInput { get; }

        /// <summary>
        /// 指定したキーが現在押されているかどうかを取得します。
        /// </summary>
        /// <param name="key">
        /// 状態を取得するキーです。
        /// </param>
        /// <returns>
        /// キーが押されている場合は <see langword="true"/>、
        /// 押されていない場合は <see langword="false"/> を返します。
        /// </returns>
        bool this[Key key] { get; }

    }
}
