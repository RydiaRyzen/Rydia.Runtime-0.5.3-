using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// マウス入力を提供する入力ソースを表します。
    /// 通常はマウスなどの入力デバイスに対応します。
    /// </summary>
    public interface IMouseInputSource : IUserInputSource
    {
        /// <summary>
        /// [取得 / 設定] ウィンドウのローカル座標系における現在のカーソル位置を取得または設定します。
        /// 座標はネイティブウィンドウ座標で表されます。
        /// </summary>
        Point2 Pos { get; set; }

        /// <summary>
        /// [取得] 現在のマウスホイールの値を取得します。
        /// </summary>
        float Wheel { get; }

        /// <summary>
        /// [取得] 指定したマウスボタンが現在押されているかどうかを取得します。
        /// </summary>
        /// <param name="btn">状態を取得するマウスボタンです。</param>
        bool this[MouseButton btn] { get; }
    }
}
