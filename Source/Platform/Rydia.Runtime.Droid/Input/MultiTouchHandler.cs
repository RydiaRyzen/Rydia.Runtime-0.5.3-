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
    /// Androidのマルチタッチ入力を処理するハンドラーです。
    /// </summary>
    /// <remarks>
    /// 複数のポインターによるタッチ操作を処理し、
    /// タッチ状態、座標、およびタッチイベントを取得します。
    /// </remarks>
    public class MultiTouchHandler : TouchHandler, ITouchHandler
    {

        bool[] isTouched = new bool[20];
        int[] touchX = new int[20];
        int[] touchY = new int[20];
        Pool<TouchEvent> touchEventPool;
        List<TouchEvent> touchEvents = new List<TouchEvent>();
        List<TouchEvent> touchEventsBuffer = new List<TouchEvent>();
        float scaleX;
        float scaleY;
        private readonly View view;
        private bool disposed;

        /// <summary>
        /// このオブジェクトが破棄済みかどうかを示す値を取得します。
        /// </summary>
        public override bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }

        /// <summary>
        /// <see cref="MultiTouchHandler"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="view">
        /// タッチ入力を受け取るAndroidのビューです。
        /// </param>
        /// <param name="scaleX">
        /// X座標に適用するスケール値です。
        /// </param>
        /// <param name="scaleY">
        /// Y座標に適用するスケール値です。
        /// </param>
        public MultiTouchHandler(View view, float scaleX, float scaleY)
        {
            this.view = view;

            this.touchEventPool = new Pool<TouchEvent>(new TouchEventFactory(), 100);
            this.view.SetOnTouchListener(this);

            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        /// <summary>
        /// タッチイベントが発生したときに呼び出されます。
        /// </summary>
        /// <param name="v">
        /// タッチイベントを受け取ったビューです。
        /// </param>
        /// <param name="e">
        /// Androidのタッチイベントです。
        /// </param>
        /// <returns>
        /// タッチイベントを処理した場合は
        /// <see langword="true"/>を返します。
        /// </returns>
        public override bool OnTouch(View? v, MotionEvent? e)
        {
            lock (this)
            {
                var action = e.ActionMasked;
                int pointerIndex = e.ActionIndex;
                int pointerId = e.GetPointerId(pointerIndex);
                TouchEvent touchEvent;

                switch (action)
                {
                    case MotionEventActions.Down:
                    case MotionEventActions.PointerDown:
                        touchEvent = this.touchEventPool.NewObject();
                        touchEvent.Type = TouchEvent.TouchState.Down;
                        touchEvent.Pointer = pointerId;
                        touchEvent.X = this.touchX[pointerId] = (int)(e.GetX(pointerIndex) * this.scaleX);
                        touchEvent.Y = this.touchY[pointerId] = (int)(e.GetY(pointerIndex) * this.scaleY);
                        this.isTouched[pointerId] = true;
                        this.touchEventsBuffer.Add(touchEvent);
                        break;

                    case MotionEventActions.Up:
                    case MotionEventActions.PointerUp:
                    case MotionEventActions.Cancel:
                        touchEvent = this.touchEventPool.NewObject();
                        touchEvent.Type = TouchEvent.TouchState.Up;
                        touchEvent.Pointer = pointerId;
                        touchEvent.X = this.touchX[pointerId] = (int)(e.GetX(pointerIndex) * this.scaleX);
                        touchEvent.Y = this.touchY[pointerId] = (int)(e.GetY(pointerIndex) * this.scaleY);
                        this.isTouched[pointerId] = false;
                        this.touchEventsBuffer.Add(touchEvent);
                        break;

                    case MotionEventActions.Move:
                        int pointerCount = e.PointerCount;
                        for (int i = 0; i < pointerCount; i++)
                        {
                            pointerIndex = i;
                            pointerId = e.GetPointerId(pointerIndex);

                            touchEvent = this.touchEventPool.NewObject();
                            touchEvent.Type = TouchEvent.TouchState.Dragged;
                            touchEvent.Pointer = pointerId;
                            touchEvent.X = this.touchX[pointerId] = (int)(e.GetX(pointerIndex) * this.scaleX);
                            touchEvent.Y = this.touchY[pointerId] = (int)(e.GetY(pointerIndex) * this.scaleY);
                            this.touchEventsBuffer.Add(touchEvent);
                        }
                        break;
                }
                return true;
            }
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
        public override bool IsTouchDown(int pointer)
        {
            lock (this)
            {
                if (pointer < 0 || pointer >= 20)
                    return false;
                return this.isTouched[pointer];
            }
        }

        /// <summary>
        /// 指定したポインターのX座標を取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// X座標です。
        /// </returns>
        public override int GetTouchX(int pointer)
        {
            lock (this)
            {
                if (pointer < 0 || pointer >= 20)
                    return 0;
                return this.touchX[pointer];
            }
        }

        /// <summary>
        /// 指定したポインターのY座標を取得します。
        /// </summary>
        /// <param name="pointer">
        /// ポインター番号です。
        /// </param>
        /// <returns>
        /// Y座標です。
        /// </returns>
        public override int GetTouchY(int pointer)
        {
            lock (this)
            {
                if (pointer < 0 || pointer >= 20)
                    return 0;
                return this.touchY[pointer];
            }
        }

        /// <summary>
        /// 現在のフレームで発生したタッチイベントを取得します。
        /// </summary>
        /// <returns>
        /// タッチイベントの一覧です。
        /// </returns>
        public override List<TouchEvent> GetTouchEvents()
        {
            lock (this)
            {
                int len = this.touchEvents.Count;
                for (int i = 0; i < len; i++)
                    this.touchEventPool.Free(this.touchEvents[i]);

                this.touchEvents.Clear();
                this.touchEvents.AddRange(this.touchEventsBuffer);
                this.touchEventsBuffer.Clear();
                return this.touchEvents;
            }
        }

        /// <summary>
        /// タッチ入力のリスナーを解除し、
        /// 使用しているリソースを解放します。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if ((this.disposed))
            {
                return;
            }
            lock (this)
            {
                this.view.SetOnTouchListener(null);

                for (int i = 0; i < this.touchEvents.Count; i++)
                {
                    this.touchEventPool.Free(
                        this.touchEvents[i]);
                }

                for (int i = 0; i < this.touchEventsBuffer.Count; i++)
                {
                    this.touchEventPool.Free(
                        this.touchEventsBuffer[i]);
                }

                this.touchEvents.Clear();
                this.touchEventsBuffer.Clear();

                Array.Clear(
                    this.isTouched,
                    0,
                    this.isTouched.Length);

                Array.Clear(
                    this.touchX,
                    0,
                    this.touchX.Length);

                Array.Clear(
                    this.touchY,
                    0,
                    this.touchY.Length);
            }
            base.Dispose(disposing);
            this.disposed = true;
        }

        /// <summary>
        /// タッチイベントを生成するためのオブジェクトファクトリです。
        /// </summary>
        private class TouchEventFactory : Java.Lang.Object, Pool<TouchEvent>.PoolObjectFactory<TouchEvent>
        {

            /// <summary>
            /// 新しいタッチイベントを生成します。
            /// </summary>
            /// <returns>
            /// 生成されたタッチイベントです。
            /// </returns>
            public TouchEvent CreateObject()
            {
                return new TouchEvent();
            }

        }

    }
}
