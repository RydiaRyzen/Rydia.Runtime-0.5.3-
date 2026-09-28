using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    /// <summary>
    /// タッチ入力を取得するためのインターフェースです。
    /// </summary>
    public interface ITouchHandler : IDisposableEx
    {

        /// <summary>
        /// 指定したポインターがタッチされているかどうかを取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// タッチされている場合は <see langword="true"/>、
        /// それ以外の場合は <see langword="false"/> を返します。
        /// </returns>
        bool IsTouchDown(int pointer);

        /// <summary>
        /// 指定したポインターのX座標を取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// X座標です。
        /// </returns>
        int GetTouchX(int pointer);

        /// <summary>
        /// 指定したポインターのY座標を取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// Y座標です。
        /// </returns>
        int GetTouchY(int pointer);

        /// <summary>
        /// 現在のフレームで発生したタッチイベントを取得します。
        /// </summary>
        /// <returns>
        /// タッチイベントの一覧です。
        /// </returns>
        List<TouchEvent> GetTouchEvents();

    }

}
