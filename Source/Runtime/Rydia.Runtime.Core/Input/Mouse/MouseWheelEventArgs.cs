using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// マウスホイールの操作イベントに関するイベント引数を提供します。
    /// </summary>
    public class MouseWheelEventArgs : MouseEventArgs
    {
        private float wheelValue;
        private float wheelSpeed;

        /// <summary>
        /// マウスホイールの現在の値を取得します。
        /// </summary>
        public float WheelValue
        {
            get { return this.wheelValue; }
        }

        /// <summary>
        /// マウスホイールの変化量を取得します。
        /// </summary>
        public float WheelSpeed
        {
            get { return this.wheelSpeed; }
        }

        /// <summary>
        /// <see cref="MouseWheelEventArgs"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="inputChannel">マウス入力の種類を指定します。</param>
        /// <param name="pos">マウスカーソルの位置を指定します。</param>
        /// <param name="value">マウスホイールの現在の値を指定します。</param>
        /// <param name="delta">マウスホイールの変化量を指定します。</param>
        public MouseWheelEventArgs(MouseInput inputChannel, Vector2 pos, float value, float delta) : base(inputChannel, pos)
        {
            this.wheelValue = value;
            this.wheelSpeed = delta;
        }
    }
}
