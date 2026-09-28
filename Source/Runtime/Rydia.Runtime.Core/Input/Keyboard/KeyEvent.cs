using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    /// <summary> /// キーボード入力イベントを表します。 /// </summary>
    public class KeyEvent
    {

        /// <summary> /// キーイベントの状態を表します。 /// </summary>
        public enum KeyState
        {

            /// <summary> /// キーが押されたことを表します。 /// </summary>
            Down,

            /// <summary> /// キーが離されたことを表します。 /// </summary>
            Up,
        }

        /// <summary> /// キーイベントの状態を取得または設定します。 /// </summary>
        public KeyState Type;

        /// <summary>
        /// キーコードを取得または設定します。
        /// </summary>
        public int KeyCode;

        /// <summary>
        /// キー入力によって生成された文字を取得または設定します。
        /// </summary>
        public char KeyChar;

    }
}
