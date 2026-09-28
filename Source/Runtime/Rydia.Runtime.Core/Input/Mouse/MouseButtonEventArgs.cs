using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// マウスボタンの状態が変化したときに発生するイベントの引数を表します。
    /// </summary>
    public class MouseButtonEventArgs : MouseEventArgs
    {

        private MouseButton button;
        private bool pressed;

        /// <summary>
        /// 状態が変化したマウスボタンを取得します。
        /// </summary>
        public MouseButton Button
        {
            get { return this.button; }
        }

        /// <summary>
        /// イベント発生時にマウスボタンが押されているかどうかを取得します。
        /// </summary>
        public bool IsPressed
        {
            get { return this.pressed; }
        }

        /// <summary>
        /// 指定したマウス入力チャネル、位置、ボタンの状態を使用して
        /// <see cref="MouseButtonEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="inputChannel">このイベントに関連付けられたマウス入力チャネルです。</param>
        /// <param name="pos">イベント発生時のマウスカーソル位置です。</param>
        /// <param name="button">状態が変化したマウスボタンです。</param>
        /// <param name="pressed">イベント発生時にマウスボタンが押されているかどうかを示します。</param>
        public MouseButtonEventArgs(MouseInput inputChannel, Vector2 pos, MouseButton button, bool pressed) : base(inputChannel, pos)
        {
            this.button = button;
            this.pressed = pressed;
        }

    }
}
