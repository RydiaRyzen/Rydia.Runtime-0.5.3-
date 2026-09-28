using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// キーボードのキー状態が変化したときに発生するイベントの引数を提供します。
    /// </summary>
    public class KeyboardKeyEventArgs : UserInputEventArgs
    {
        private Key key;
        private bool pressed;

        /// <summary>
        /// 状態が変化したキーを取得します。
        /// </summary>
        /// <value>
        /// 状態が変化したキーです。
        /// </value>
        public Key Key
        {
            get { return this.key; }
        }

        /// <summary>
        /// キーが押されているかどうかを取得します。
        /// </summary>
        /// <value>
        /// キーが押されている場合は <see langword="true"/>、
        /// キーが離されている場合は <see langword="false"/> を返します。
        /// </value>
        public bool IsPressed
        {
            get { return this.pressed; }
        }

        /// <summary>
        /// <see cref="KeyboardKeyEventArgs"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="inputChannel">
        /// このイベントを発生させたキーボード入力です。
        /// </param>
        /// <param name="key">
        /// 状態が変化したキーです。
        /// </param>
        /// <param name="pressed">
        /// キーが押されているかどうかを示す値です。
        /// </param>
        public KeyboardKeyEventArgs(KeyboardInput inputChannel, Key key, bool pressed) : base(inputChannel)
        {
            this.key = key;
            this.pressed = pressed;
        }

    }
}
