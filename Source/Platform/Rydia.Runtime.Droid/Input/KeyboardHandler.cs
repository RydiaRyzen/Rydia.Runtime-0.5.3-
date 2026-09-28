using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Views;
using Rydia.Input;
using static Android.Views.View;

namespace Rydia.Input
{

    /// <summary>
    /// Androidのキーボード入力を処理するハンドラーです。
    /// </summary>
    /// <remarks>
    /// 指定されたAndroidの<see cref="View"/>にキーボードリスナーを登録し、
    /// キーの押下状態およびキーボードイベントを取得します。
    /// </remarks>
    public class KeyboardHandler : Java.Lang.Object, IOnKeyListener, IMobileKeyboard
    {

        bool[] pressedKeys = new bool[128];
        Pool<Rydia.Input.KeyEvent> keyEventPool;
        List<Rydia.Input.KeyEvent> keyEventsBuffer = new List<Rydia.Input.KeyEvent>();
        List<Rydia.Input.KeyEvent> keyEvents = new List<Rydia.Input.KeyEvent>();
        private readonly View view;
        private bool disposed;

        /// <summary>
        /// このオブジェクトが破棄済みかどうかを示す値を取得します。
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }

        /// <summary>
        /// <see cref="KeyboardHandler"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="view">
        /// キーボード入力を受け取るAndroidのビューです。
        /// </param>
        public KeyboardHandler(View view)
        {
            this.view = view;

            this.keyEventPool = new Pool<Rydia.Input.KeyEvent>(new KeyEventFactory(), 100);
            this.view.SetOnKeyListener(this);
            this.view.FocusableInTouchMode = true;
            this.view.RequestFocus();
        }

        /// <summary>
        /// キーイベントが発生したときに呼び出されます。
        /// </summary>
        /// <param name="v">
        /// キーイベントを受け取ったビューです。
        /// </param>
        /// <param name="keyCode">
        /// 押されたキーのキーコードです。
        /// </param>
        /// <param name="e">
        /// Androidのキーイベントです。
        /// </param>
        /// <returns>
        /// イベントを処理した場合は <see langword="true"/>、
        /// 処理しなかった場合は <see langword="false"/> を返します。
        /// </returns>
        public bool OnKey(View? v, [GeneratedEnum] Keycode keyCode, Android.Views.KeyEvent? e)
        {
            if (e.Action == KeyEventActions.Multiple)
                return false;

            lock (this)
            {
                var keyEvent = this.keyEventPool.NewObject();
                keyEvent.KeyCode = (int)keyCode;
                keyEvent.KeyChar = (char)e.UnicodeChar;

                if (e.Action == KeyEventActions.Down)
                {
                    keyEvent.Type = Rydia.Input.KeyEvent.KeyState.Down;
                    if ((int)keyCode > 0 && (int)keyCode < 127)
                        this.pressedKeys[(int)keyCode] = true;
                }

                if (e.Action == KeyEventActions.Up)
                {
                    keyEvent.Type = Rydia.Input.KeyEvent.KeyState.Up;
                    if ((int)keyCode > 0 && (int)keyCode < 127)
                        this.pressedKeys[(int)keyCode] = false;
                }

                this.keyEventsBuffer.Add(keyEvent);
            }

            return false;
        }

        /// <summary>
        /// 指定したキーが押されているかどうかを取得します。
        /// </summary>
        /// <param name="keyCode">
        /// 判定するキーのキーコードです。
        /// </param>
        /// <returns>
        /// キーが押されている場合は <see langword="true"/>、
        /// 押されていない場合は <see langword="false"/> を返します。
        /// </returns>
        public bool IsKeyPressed(int keyCode)
        {
            if (keyCode < 0 || keyCode > 127)
                return false;

            return this.pressedKeys[keyCode];
        }

        /// <summary>
        /// 現在のフレームで発生したキーボードイベントを取得します。
        /// </summary>
        /// <returns>
        /// キーボードイベントの一覧です。
        /// </returns>
        /// <remarks>
        /// 前回取得したイベントは再利用のためにオブジェクトプールへ返却され、
        /// 現在バッファに蓄積されているイベントが返されます。
        /// </remarks>
        public List<Rydia.Input.KeyEvent> GetKeyEvents()
        {
            lock (this)
            {
                int len = this.keyEvents.Count;
                for (int i = 0; i < len; i++)
                    this.keyEventPool.Free(this.keyEvents[i]);

                this.keyEvents.Clear();
                this.keyEvents.AddRange(this.keyEventsBuffer);
                this.keyEventsBuffer.Clear();

                return this.keyEvents;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            lock (this)
            {
                this.view.SetOnKeyListener(null);
                for (int i = 0; i < this.keyEvents.Count; i++)
                {
                    this.keyEventPool.Free(this.keyEvents[i]);
                }

                for (int i = 0; i < this.keyEventsBuffer.Count; i++)
                {
                    this.keyEventPool.Free(this.keyEventsBuffer[i]);
                }

                this.keyEvents.Clear();
                this.keyEventsBuffer.Clear();

                Array.Clear(this.pressedKeys, 0, this.pressedKeys.Length);
            }
            base.Dispose(disposing);
            this.disposed = true;
        }

        /// <summary>
        /// キーボードイベントを生成するためのオブジェクトファクトリです。
        /// </summary>
        private class KeyEventFactory : Pool<Rydia.Input.KeyEvent>.PoolObjectFactory<Rydia.Input.KeyEvent>
        {

            /// <summary>
            /// 新しいキーボードイベントを生成します。
            /// </summary>
            /// <returns>
            /// 生成されたキーボードイベントです。
            /// </returns>
            public Rydia.Input.KeyEvent CreateObject()
            {
                return new Rydia.Input.KeyEvent();
            }
        }

    }

}
