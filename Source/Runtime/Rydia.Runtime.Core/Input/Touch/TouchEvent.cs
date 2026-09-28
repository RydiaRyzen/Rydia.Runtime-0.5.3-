using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    /// <summary>
    /// タッチ入力イベントの情報を表します。
    /// </summary>
    /// <remarks>
    /// タッチの状態、座標、およびポインター番号を保持します。
    /// オブジェクトプールによって再利用されることを想定しています。
    /// </remarks>
    public class TouchEvent
    {

        /// <summary>
        /// タッチイベントの状態を表します。
        /// </summary>
        public enum TouchState
        {
            /// <summary>
            /// タッチが開始されたことを表します。
            /// </summary>
            Down,
            /// <summary>
            /// タッチが終了したことを表します。
            /// </summary>
            Up,
            /// <summary>
            /// タッチ位置が移動したことを表します。
            /// </summary>
            Dragged,
        }

        /// <summary>
        /// タッチイベントの状態を取得または設定します。
        /// </summary>
        public TouchState Type;

        /// <summary>
        /// タッチされた位置のX座標を取得または設定します。
        /// </summary>
        public int X;

        /// <summary>
        /// タッチされた位置のY座標を取得または設定します。
        /// </summary>
        public int Y;

        /// <summary>
        /// タッチを識別するポインター番号を取得または設定します。
        /// </summary>
        /// <remarks>
        /// マルチタッチ時には、各タッチを識別するために使用されます。
        /// </remarks>
        public int Pointer;

    }
}
