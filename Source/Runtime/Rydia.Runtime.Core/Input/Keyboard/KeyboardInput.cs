using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// ユーザーからのキーボード入力を提供します。
    /// </summary>
    public sealed class KeyboardInput : IUserInput
    {

        /// <summary>
        /// キーボード入力の状態を保持します。
        /// </summary>
        private class State
        {

            /// <summary>
            /// キーボード入力が利用可能かどうかを示します。
            /// </summary>
            public bool IsAvailable = false;

            /// <summary>
            /// 各キーが押されているかどうかを保持します。
            /// </summary>
            public bool[] KeyPressed = new bool[(int)Key.Last + 1];

            /// <summary>
            /// 現在入力されている文字列を保持します。
            /// </summary>
            public string CharInput = string.Empty;

            /// <summary>
            /// <see cref="State"/>クラスの新しいインスタンスを初期化します。
            /// </summary>
            public State() { }

            /// <summary>
            /// 指定した状態をコピーして新しい状態を初期化します。
            /// </summary>
            /// <param name="baseState">
            /// コピー元の状態です。
            /// </param>
            public State(State baseState)
            {
                baseState.CopyTo(this);
            }

            /// <summary>
            /// 現在の状態を指定した状態へコピーします。
            /// </summary>
            /// <param name="other">
            /// コピー先の状態です。
            /// </param>
            public void CopyTo(State other)
            {
                other.IsAvailable = this.IsAvailable;
                other.CharInput = this.CharInput;
                this.KeyPressed.CopyTo(other.KeyPressed, 0);
            }

            /// <summary>
            /// 指定した入力ソースから状態を更新します。
            /// </summary>
            /// <param name="source">
            /// 状態の取得元となるキーボード入力ソースです。
            /// </param>
            public void UpdateFromSource(IKeyboardInputSource source)
            {
                this.IsAvailable = source != null ? source.IsAvailable : false;
                if (source == null) return;

                this.CharInput = source.CharInput ?? string.Empty;
                for (int i = 0; i < this.KeyPressed.Length; i++)
                {
                    this.KeyPressed[i] = source[(Key)i];
                }
            }

        }

        private IKeyboardInputSource source = null;
        private State currentState = new State();
        private State lastState = new State();


        /// <summary>
        /// キーボード入力データの取得元を取得または設定します。
        /// </summary>
        /// <value>
        /// キーボード入力を提供する入力ソースです。
        /// </value>
        public IKeyboardInputSource Source
        {
            get { return this.source; }
            set
            {
                if (this.source != value)
                {
                    this.source = value;
                }
            }
        }

        /// <summary>
        /// 入力ソースを取得または設定します。
        /// </summary>
        /// <value>
        /// キーボード入力ソースです。
        /// </value>
        IUserInputSource IUserInput.Source
        {
            get { return Source; }
            set { Source = value as IKeyboardInputSource; }
        }

        /// <summary>
        /// この入力デバイスを識別する一意のIDを取得します。
        /// </summary>
        /// <value>
        /// 入力デバイスのIDです。
        /// </value>
        public string Id
        {
            get { return "Keyboard"; }
        }

        /// <summary>
        /// この入力を提供している製品を識別する一意のIDを取得します。
        /// </summary>
        /// <value>
        /// 製品IDです。
        /// </value>
        public Guid ProductId
        {
            get { return Guid.Empty; }
        }

        /// <summary>
        /// この入力を提供している製品名を取得します。
        /// </summary>
        /// <value>
        /// 製品名です。
        /// </value>
        public string ProductName
        {
            get { return "Keyboard"; }
        }

        /// <summary>
        /// キーボード入力が現在利用可能かどうかを取得します。
        /// </summary>
        /// <value>
        /// キーボード入力が利用可能な場合は
        /// <see langword="true"/>、それ以外の場合は
        /// <see langword="false"/>です。
        /// </value>
        public bool IsAvailable
        {
            get { return this.currentState.IsAvailable; }
        }

        /// <summary>
        /// 前回の入力更新以降に入力された文字を連結した文字列を取得します。
        /// </summary>
        /// <value>
        /// 入力された文字を連結した文字列です。
        /// </value>
        public string CharInput
        {
            get { return this.currentState.CharInput; }
        }

        /// <summary>
        /// 指定したキーが現在押されているかどうかを取得します。
        /// </summary>
        /// <param name="key">
        /// 状態を取得するキーです。
        /// </param>
        /// <returns>
        /// キーが押されている場合は <see langword="true"/>、
        /// それ以外の場合は <see langword="false"/>です。
        /// </returns>
        public bool this[Key key]
        {
            get { return this.currentState.KeyPressed[(int)key]; }
        }

        /// <summary>
        /// キーが離されたときに発生します。
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyUp;

        /// <summary>
        /// キーが押されたときに発生します。
        /// </summary>
        public event EventHandler<KeyboardKeyEventArgs> KeyDown;

        /// <summary>
        /// キーボード入力が利用できなくなったときに発生します。
        /// </summary>
        public event EventHandler NoLongerAvailable;

        /// <summary>
        /// キーボード入力が利用可能になったときに発生します。
        /// </summary>
        public event EventHandler BecomesAvailable;

        /// <summary>
        /// <see cref="KeyboardInput"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        public KeyboardInput() { }

        /// <summary>
        /// キーボード入力の状態を更新します。
        /// </summary>
        /// <param name="width">
        /// 入力対象の幅です。
        /// </param>
        /// <param name="height">
        /// 入力対象の高さです。
        /// </param>
        /// <remarks>
        /// <para>
        /// 現在の入力状態を前回の状態として保存し、
        /// 入力ソースから最新の状態を取得します。
        /// </para>
        /// <para>
        /// キーの押下、解放、および入力デバイスの利用可能状態に
        /// 変化があった場合は、対応するイベントを発生させます。
        /// </para>
        /// </remarks>
        public void Update(int width, int height)
        {
            // Memorize last state
            this.currentState.CopyTo(this.lastState);

            if (this.source != null)
            {
                // Update source state
                this.source.UpdateState();
                // Obtain new state
                this.currentState.UpdateFromSource(this.source);
            }

            // Fire events
            if (this.currentState.IsAvailable && !this.lastState.IsAvailable)
            {
                if (BecomesAvailable != null)
                    BecomesAvailable(this, EventArgs.Empty);
            }
            if (!this.currentState.IsAvailable && this.lastState.IsAvailable)
            {
                if (NoLongerAvailable != null)
                    NoLongerAvailable(this, EventArgs.Empty);
            }
            for (int i = 0; i < this.currentState.KeyPressed.Length; i++)
            {
                if (this.currentState.KeyPressed[i] && !this.lastState.KeyPressed[i])
                {
                    if (KeyDown != null)
                        KeyDown(this, new KeyboardKeyEventArgs(this, (Key)i, this.currentState.KeyPressed[i]));
                }
                if (!this.currentState.KeyPressed[i] && this.lastState.KeyPressed[i])
                {
                    if (KeyUp != null)
                        KeyUp(this, new KeyboardKeyEventArgs(this, (Key)i, this.currentState.KeyPressed[i]));
                }
            }
        }

        /// <summary>
        /// 指定したキーが現在押されているかどうかを取得します。
        /// </summary>
        /// <param name="key">
        /// 判定するキーです。
        /// </param>
        /// <returns>
        /// キーが押されている場合は <see langword="true"/>、
        /// それ以外の場合は <see langword="false"/>です。
        /// </returns>
        public bool KeyPressed(Key key)
        {
            return this.currentState.KeyPressed[(int)key];
        }

        /// <summary>
        /// 指定したキーがこのフレームで押されたかどうかを取得します。
        /// </summary>
        /// <param name="key">
        /// 判定するキーです。
        /// </param>
        /// <returns>
        /// 前フレームでは押されておらず、現在のフレームで押されている場合は
        /// <see langword="true"/>、それ以外の場合は
        /// <see langword="false"/>です。
        /// </returns>
        public bool KeyHit(Key key)
        {
            return this.currentState.KeyPressed[(int)key] && !this.lastState.KeyPressed[(int)key];
        }

        /// <summary>
        /// 指定したキーがこのフレームで離されたかどうかを取得します。
        /// </summary>
        /// <param name="key">
        /// 判定するキーです。
        /// </param>
        /// <returns>
        /// 前フレームでは押されており、現在のフレームで離されている場合は
        /// <see langword="true"/>、それ以外の場合は
        /// <see langword="false"/>です。
        /// </returns>
        public bool KeyReleased(Key key)
        {
            return !this.currentState.KeyPressed[(int)key] && this.lastState.KeyPressed[(int)key];
        }

    }

}
