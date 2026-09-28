using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// マウス入力に関するイベントの引数を表す基底クラスです。
    /// </summary>
    public class MouseEventArgs : UserInputEventArgs
    {
        private Vector2 pos;

        /// <summary>
        /// イベント発生時のマウスカーソル位置を取得します。
        /// </summary>
        public Vector2 Pos
        {
            get { return this.pos; }
        }

        /// <summary>
        /// 指定したマウス入力チャネルとカーソル位置を使用して
        /// <see cref="MouseEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="inputChannel">このイベントに関連付けられたマウス入力チャネルです。</param>
        /// <param name="pos">イベント発生時のマウスカーソル位置です。</param>
        public MouseEventArgs(MouseInput inputChannel, Vector2 pos) : base(inputChannel)
        {
            this.pos = pos;
        }

    }
}
