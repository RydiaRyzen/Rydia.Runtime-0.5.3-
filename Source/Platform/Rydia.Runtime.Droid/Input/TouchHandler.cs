using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using Rydia.Input;

namespace Rydia.Input
{

    /// <summary>
    /// タッチ入力を処理するための基底クラスです。
    /// </summary>
    /// <remarks>
    /// Androidのタッチイベントを受け取り、タッチ状態、座標、
    /// およびタッチイベントを取得するための共通インターフェースを提供します。
    /// </remarks>
    public abstract class TouchHandler : Java.Lang.Object, View.IOnTouchListener, ITouchHandler
    {

        /// <summary>
        /// このオブジェクトが破棄済みかどうかを示す値を取得します。
        /// </summary>
        public abstract bool IsDisposed
        {
            get;
        }

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
        public abstract bool IsTouchDown(int pointer);

        /// <summary>
        /// 指定したポインターのX座標を取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// X座標です。
        /// </returns>
        public abstract int GetTouchX(int pointer);

        /// <summary>
        /// 指定したポインターのY座標を取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// Y座標です。
        /// </returns>
        public abstract int GetTouchY(int pointer);

        /// <summary>
        /// 現在のフレームで発生したタッチイベントを取得します。
        /// </summary>
        /// <returns>
        /// タッチイベントの一覧です。
        /// </returns>
        public abstract List<TouchEvent> GetTouchEvents();

        /// <summary>
        /// Androidのタッチイベントを処理します。
        /// </summary>
        /// <param name="v">
        /// タッチイベントを受け取ったビューです。
        /// </param>
        /// <param name="e">
        /// Androidのタッチイベントです。
        /// </param>
        /// <returns>
        /// イベントを処理した場合は <see langword="true"/>、
        /// 処理しなかった場合は <see langword="false"/> を返します。
        /// </returns>
        public abstract bool OnTouch(View? v, MotionEvent? e);

    }
}
