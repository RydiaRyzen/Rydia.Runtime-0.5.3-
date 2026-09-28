using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// マウス入力へのアクセスを提供します。
    /// </summary>
    public sealed class MouseInput : IUserInput
    {
        private class State
        {
            public bool IsAvailable = false;
            public Point2 WindowPos = Point2.Zero;
            public Vector2 ViewPos = Vector2.Zero;
            public float Wheel = 0.0f;
            public bool[] ButtonPressed = new bool[(int)MouseButton.Last + 1];

            /// <summary>
            /// [取得 / 設定] 0以外の値を設定した場合、ユーザー定義または既定のウィンドウサイズの制約内に収まるよう、
            /// ゲームのビューポートをこのサイズに合わせて調整します。
            /// </summary>
            public Point2 ForcedRenderSize
            {
                get;
                set;
            }

            /// <summary>
            /// [取得 / 設定] <see cref="ForcedRenderSize"/> に合わせて描画領域を調整する方法を指定します。
            /// </summary>
            public TargetResize ForcedRenderResizeMode
            {
                get;
                set;
            }

            public State()
            {
                
            }

            public State(State baseState)
            {
                baseState.CopyTo(this);
            }
            public void CopyTo(State other)
            {
                other.IsAvailable = this.IsAvailable;
                other.WindowPos = this.WindowPos;
                other.ViewPos = this.ViewPos;
                other.Wheel = this.Wheel;
                this.ButtonPressed.CopyTo(other.ButtonPressed, 0);
            }

            public void UpdateFromSource(IMouseInputSource source, int width, int height)
            {
                this.IsAvailable = source != null ? source.IsAvailable : false;
                if (source == null) return;

                this.WindowPos = source.Pos;
                this.Wheel = source.Wheel;
                for (int i = 0; i < this.ButtonPressed.Length; i++)
                {
                    this.ButtonPressed[i] = source[(MouseButton)i];
                }

                // Map window position to game view position
                RectF viewportRect;
                Vector2 gameViewSize;
                CalculateGameViewport(new Point2(width, height), out viewportRect, out gameViewSize);
                Vector2 relativePos = new Vector2(
                    MathFR.Clamp((this.WindowPos.X - viewportRect.X) / viewportRect.W, 0.0f, 1.0f),
                    MathFR.Clamp((this.WindowPos.Y - viewportRect.Y) / viewportRect.H, 0.0f, 1.0f));
                this.ViewPos = relativePos * gameViewSize;
            }

            /// <summary>
            /// 指定されたウィンドウサイズに基づいて、描画されるビューポートのウィンドウ内の矩形と、
            /// 強制描画サイズの設定を考慮したゲームの描画サイズを計算します。
            /// </summary>
            /// <param name="windowSize">ウィンドウのサイズです。</param>
            /// <param name="windowViewport">描画されるビューポートのウィンドウ内の矩形です。</param>
            /// <param name="renderTargetSize">ゲームの描画先サイズです。</param>
            public void CalculateGameViewport(Point2 windowSize, out RectF windowViewport, out Vector2 renderTargetSize)
            {
                Point2 forcedSize = ForcedRenderSize;
                TargetResize forcedResizeMode = ForcedRenderResizeMode;

                renderTargetSize = windowSize;
                windowViewport = new RectF(renderTargetSize);

                bool forcedResizeActive =
                    forcedResizeMode != TargetResize.None &&
                    forcedSize.X > 0 && forcedSize.Y > 0 &&
                    forcedSize != renderTargetSize;
                if (forcedResizeActive)
                {
                    Vector2 adjustedViewportSize = forcedResizeMode.Apply(forcedSize, windowViewport.Size);

                    // ビューポートと描画先サイズがウィンドウサイズを超えないように制限します。
                    // これは強制描画サイズの指定に厳密には反しますが、
                    // Fillなどのリサイズモードではこれ以外に適切な方法がありません。
                    if (adjustedViewportSize.X > windowSize.X)
                    {
                        forcedSize.X = MathFR.RoundToInt((float)forcedSize.X * (float)windowSize.X / (float)adjustedViewportSize.X);
                        adjustedViewportSize.X = windowSize.X;
                    }
                    if (adjustedViewportSize.Y > windowSize.Y)
                    {
                        forcedSize.Y = MathFR.RoundToInt((float)forcedSize.Y * (float)windowSize.Y / (float)adjustedViewportSize.Y);
                        adjustedViewportSize.Y = windowSize.Y;
                    }

                    renderTargetSize = forcedSize;
                    windowViewport = RectF.Align(
                        Alignment.Center,
                        windowViewport.Size.X * 0.5f,
                        windowViewport.Size.Y * 0.5f,
                        adjustedViewportSize.X,
                        adjustedViewportSize.Y);
                }
            }

        }

        private IMouseInputSource source = null;
        private State currentState = new State();
        private State lastState = new State();


        /// <summary>
        /// [取得 / 設定] マウス入力の状態を取得する入力ソースを取得または設定します。
        /// </summary>
        public IMouseInputSource Source
        {
            get { return this.source; }
            set { this.source = value; }
        }
        IUserInputSource IUserInput.Source
        {
            get { return Source; }
            set { Source = value as IMouseInputSource; }
        }

        /// <summary>
        /// [取得] この入力を一意に識別するIDを取得します。
        /// </summary>
        public string Id
        {
            get { return "Mouse"; }
        }

        /// <summary>
        /// [取得] この入力を提供している製品を一意に識別するIDを取得します。
        /// </summary>
        public Guid ProductId
        {
            get { return Guid.Empty; }
        }

        /// <summary>
        /// [取得] この入力を提供している製品の名前を取得します。
        /// </summary>
        public string ProductName
        {
            get { return "Mouse"; }
        }

        /// <summary>
        /// [取得] この入力が現在利用可能かどうかを取得します。
        /// </summary>
        public bool IsAvailable
        {
            get { return this.currentState.IsAvailable; }
        }

        /// <summary>
        /// [取得 / 設定] ネイティブウィンドウ座標における、
        /// ウィンドウローカルの現在のマウスカーソル位置を取得または設定します。
        /// </summary>
        public Point2 WindowPos
        {
            get { return this.currentState.WindowPos; }
            set
            {
                if (this.source != null)
                    this.source.Pos = value;
            }
        }

        /// <summary>
        /// [取得] ビューポートローカル座標における現在のマウスカーソル位置を取得します。
        /// </summary>
        public Vector2 Pos
        {
            get { return this.currentState.ViewPos; }
        }

        /// <summary>
        /// [取得] 前フレームからのビューポートローカル座標における
        /// マウスカーソル位置の変化量を取得します。
        /// </summary>
        public Vector2 Vel
        {
            get
            {
                return (this.currentState.IsAvailable && this.lastState.IsAvailable) ?
                    this.currentState.ViewPos - this.lastState.ViewPos :
                    Vector2.Zero;
            }
        }

        /// <summary>
        /// [取得] 現在のマウスホイールの値を取得します。
        /// </summary>
        public float Wheel
        {
            get { return this.currentState.Wheel; }
        }

        /// <summary>
        /// [取得] 前フレームからのマウスホイール値の変化量を取得します。
        /// </summary>
        public float WheelSpeed
        {
            get { return (this.currentState.IsAvailable && this.lastState.IsAvailable) ? this.currentState.Wheel - this.lastState.Wheel : 0.0f; }
        }

        /// <summary>
        /// [取得] 指定した <see cref="MouseButton"/> が現在押されているかどうかを取得します。
        /// </summary>
        /// <param name="btn">状態を取得するマウスボタンです。</param>
        /// <returns>指定したマウスボタンが押されている場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> を返します。</returns>
        public bool this[MouseButton btn]
        {
            get { return this.currentState.ButtonPressed[(int)btn]; }
        }

        /// <summary>
        /// <see cref="MouseButton"/> が離されたときに発生します。
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonUp;

        /// <summary>
        /// <see cref="MouseButton"/> が押されたときに一度だけ発生します。
        /// </summary>
        public event EventHandler<MouseButtonEventArgs> ButtonDown;

        /// <summary>
        /// マウスカーソルが移動したときに発生します。
        /// </summary>
        public event EventHandler<MouseMoveEventArgs> Move;

        /// <summary>
        /// マウスカーソルがビューポート領域から離れたときに発生します。
        /// </summary>
        public event EventHandler NoLongerAvailable;

        /// <summary>
        /// マウスカーソルがビューポート領域に入ったときに発生します。
        /// </summary>
        public event EventHandler BecomesAvailable;

        /// <summary>
        /// マウスホイールの値が変化したときに発生します。
        /// </summary>
        public event EventHandler<MouseWheelEventArgs> WheelChanged;

        /// <summary>
        /// <see cref="MouseInput"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public MouseInput() { }

        /// <summary>
        /// マウス入力の現在の状態を更新し、状態の変化に応じたイベントを発生させます。
        /// </summary>
        /// <param name="width">入力対象となるウィンドウの幅です。</param>
        /// <param name="height">入力対象となるウィンドウの高さです。</param>
        public void Update(int width, int height)
        {
            // 前回の状態を保存
            this.currentState.CopyTo(this.lastState);

            if (this.source != null)
            {
                // 入力ソースの状態を更新
                this.source.UpdateState();

                // 新しい状態を取得
                this.currentState.UpdateFromSource(this.source, width, height);
            }

            // イベントを発生させます
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
            if (this.currentState.ViewPos != this.lastState.ViewPos)
            {
                if (Move != null)
                    Move(this, new MouseMoveEventArgs(
                        this,
                        Pos,
                        Vel));
            }
            if (this.currentState.Wheel != this.lastState.Wheel)
            {
                if (WheelChanged != null)
                    WheelChanged(this, new MouseWheelEventArgs(
                        this,
                        Pos,
                        Wheel,
                        WheelSpeed));
            }
            for (int i = 0; i < this.currentState.ButtonPressed.Length; i++)
            {
                if (this.currentState.ButtonPressed[i] && !this.lastState.ButtonPressed[i])
                {
                    if (ButtonDown != null)
                        ButtonDown(this, new MouseButtonEventArgs(
                            this,
                            Pos,
                            (MouseButton)i,
                            this.currentState.ButtonPressed[i]));
                }
                if (!this.currentState.ButtonPressed[i] && this.lastState.ButtonPressed[i])
                {
                    if (ButtonUp != null)
                        ButtonUp(this, new MouseButtonEventArgs(
                            this,
                            Pos,
                            (MouseButton)i,
                            this.currentState.ButtonPressed[i]));
                }
            }
        }

        /// <summary>
        /// 指定したマウスボタンが現在押されているかどうかを取得します。
        /// </summary>
        /// <param name="button">状態を取得するマウスボタンです。</param>
        /// <returns>マウスボタンが押されている場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> を返します。</returns>
        public bool ButtonPressed(MouseButton button)
        {
            return this.currentState.ButtonPressed[(int)button];
        }

        /// <summary>
        /// 指定したマウスボタンがこのフレームで押されたかどうかを取得します。
        /// </summary>
        /// <param name="button">状態を確認するマウスボタンです。</param>
        /// <returns>このフレームで押された場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> を返します。</returns>
        public bool ButtonHit(MouseButton button)
        {
            return this.currentState.ButtonPressed[(int)button] && !this.lastState.ButtonPressed[(int)button];
        }

        /// <summary>
        /// 指定したマウスボタンがこのフレームで離されたかどうかを取得します。
        /// </summary>
        /// <param name="button">状態を確認するマウスボタンです。</param>
        /// <returns>このフレームで離された場合は <see langword="true"/>、それ以外の場合は <see langword="false"/> を返します。</returns>
        public bool ButtonReleased(MouseButton button)
        {
            return !this.currentState.ButtonPressed[(int)button] && this.lastState.ButtonPressed[(int)button];
        }
    }
}
