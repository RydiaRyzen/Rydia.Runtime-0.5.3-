using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// マウスの移動イベントに関するイベント引数を提供します。
    /// </summary>
    public class MouseMoveEventArgs : MouseEventArgs
    {
        private Vector2 vel;

        /// <summary>
        /// マウスの移動量を取得します。
        /// </summary>
        public Vector2 Vel
        {
            get { return this.vel; }
        }

        /// <summary>
        /// <see cref="MouseMoveEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="inputChannel">マウス入力の種類を指定します。</param>
        /// <param name="pos">マウスカーソルの現在位置を指定します。</param>
        /// <param name="vel">マウスカーソルの移動量を指定します。</param>
        public MouseMoveEventArgs(MouseInput inputChannel, Vector2 pos, Vector2 vel) : base(inputChannel, pos)
        {
            this.vel = vel;
        }
    }
}
