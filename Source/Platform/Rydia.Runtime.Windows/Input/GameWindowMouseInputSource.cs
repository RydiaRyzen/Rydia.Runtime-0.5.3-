using System;


using OpenTK;

using TKMouseState = OpenTK.Input.MouseState;
using TKMouseMoveEventArgs = OpenTK.Input.MouseMoveEventArgs;
using TKMouseWheelEventArgs = OpenTK.Input.MouseWheelEventArgs;
using TKMouseButtonEventArgs = OpenTK.Input.MouseButtonEventArgs;
using Rydia.Input;
using Rydia;


namespace Rydia.Input
{

    /// <summary>
    /// OpenTK の <see cref="GameWindow"/> からマウス入力を取得する入力ソースを表します。
    /// </summary>
    public class GameWindowMouseInputSource : IMouseInputSource
	{

        /// <summary>
        /// カーソル位置を設定するためのデリゲートを表します。
        /// </summary>
        /// <param name="v">設定する座標値。</param>
        public delegate void CursorPosSetter(int v);

		private GameWindow   window           = null;
		private bool         cursorInView     = false;
		private TKMouseState mouseState       = default(TKMouseState);
		private TKMouseState mouseStateBuffer = default(TKMouseState);

        /// <summary>
        /// 入力ソースを識別するための ID を取得します。
        /// </summary>
        public string Id
		{
			get { return "Mouse"; }
		}

        /// <summary>
        /// 入力デバイスの製品 ID を取得します。
        /// </summary>
        public Guid ProductId
		{
			get { return Guid.Empty; }
		}

        /// <summary>
        /// 入力デバイスの製品名を取得します。
        /// </summary>
        public string ProductName
		{
			get { return "Mouse"; }
        }

        /// <summary>
        /// マウス入力が現在利用可能かどうかを示す値を取得します。
        /// </summary>
        /// <remarks>
        /// 対象となる <see cref="GameWindow"/> が存在し、かつカーソルがウィンドウ内に
        /// 存在する場合に <see langword="true"/> を返します。
        /// </remarks>
        public bool IsAvailable
		{
			get { return this.window != null && this.cursorInView; }
        }

        /// <summary>
        /// マウスカーソルの位置を取得または設定します。
        /// </summary>
        /// <remarks>
        /// 取得時はウィンドウ内のクライアント座標を返します。
        /// 設定時は指定したクライアント座標をスクリーン座標へ変換してカーソルを移動します。
        /// </remarks>
        public Point2 Pos
		{
			get { return new Point2(this.mouseState.X, this.mouseState.Y); }
			set
			{
				System.Drawing.Point screenPoint = this.window.PointToScreen(new System.Drawing.Point(value.X, value.Y));
				OpenTK.Input.Mouse.SetPosition(screenPoint.X, screenPoint.Y);
			}
        }

        /// <summary>
        /// マウスホイールの現在の値を取得します。
        /// </summary>
        public float Wheel
		{
			get { return this.mouseState.WheelPrecise; }
        }

        /// <summary>
        /// 指定したマウスボタンが押されているかどうかを取得します。
        /// </summary>
        /// <param name="key">状態を取得するマウスボタン。</param>
        /// <returns>
        /// 指定したマウスボタンが押されている場合は <see langword="true"/>、
        /// それ以外の場合は <see langword="false"/>。
        /// </returns>
        public bool this[MouseButton key]
		{
			get { return this.mouseState[GetOpenTKMouseButton(key)]; }
		}

        /// <summary>
        /// <see cref="GameWindowMouseInputSource"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="window">マウス入力を取得する OpenTK のゲームウィンドウ。</param>
        public GameWindowMouseInputSource(GameWindow window)
		{
			this.window = window;
			this.window.MouseEnter += window_MouseEnter;
			this.window.MouseLeave += window_MouseLeave;
			this.window.MouseMove += window_MouseMove;
			this.window.MouseWheel += window_MouseWheel;
			this.window.MouseDown += window_MouseDown;
			this.window.MouseUp += window_MouseUp;
		}

        /// <summary>
        /// マウス入力の状態を更新します。
        /// </summary>
        /// <remarks>
        /// マウスイベントから取得した最新の入力状態を現在の状態へ反映します。
        /// </remarks>
        public void UpdateState()
		{
			this.mouseState = this.mouseStateBuffer;
		}

        /// <summary>
        /// マウスカーソルがウィンドウ内へ入ったときに呼び出されます。
        /// </summary>
        /// <param name="sender">イベントの送信元。</param>
        /// <param name="e">イベントの引数。</param>
        private void window_MouseEnter(object sender, EventArgs e)
		{
			this.cursorInView = true;
		}

        /// <summary>
        /// マウスカーソルがウィンドウ外へ出たときに呼び出されます。
        /// </summary>
        /// <param name="sender">イベントの送信元。</param>
        /// <param name="e">イベントの引数。</param>
        private void window_MouseLeave(object sender, EventArgs e)
		{
			this.cursorInView = false;
        }

        /// <summary>
        /// マウスカーソルが移動したときに呼び出されます。
        /// </summary>
        /// <param name="sender">イベントの送信元。</param>
        /// <param name="e">マウス移動イベントの引数。</param>
        private void window_MouseMove(object sender, TKMouseMoveEventArgs e)
		{
			this.mouseStateBuffer = e.Mouse;
        }

        /// <summary>
        /// マウスホイールが操作されたときに呼び出されます。
        /// </summary>
        /// <param name="sender">イベントの送信元。</param>
        /// <param name="e">マウスホイールイベントの引数。</param>
        private void window_MouseWheel(object sender, TKMouseWheelEventArgs e)
		{
			this.mouseStateBuffer = e.Mouse;
        }

        /// <summary>
        /// マウスボタンが押されたときに呼び出されます。
        /// </summary>
        /// <param name="sender">イベントの送信元。</param>
        /// <param name="e">マウスボタンイベントの引数。</param>
        private void window_MouseDown(object sender, TKMouseButtonEventArgs e)
		{
			this.mouseStateBuffer = e.Mouse;
        }

        /// <summary>
        /// マウスボタンが離されたときに呼び出されます。
        /// </summary>
        /// <param name="sender">イベントの送信元。</param>
        /// <param name="e">マウスボタンイベントの引数。</param>
        private void window_MouseUp(object sender, TKMouseButtonEventArgs e)
		{
			this.mouseStateBuffer = e.Mouse;
        }

        /// <summary>
        /// Rydia の <see cref="MouseButton"/> を OpenTK のマウスボタンに変換します。
        /// </summary>
        /// <param name="button">変換する Rydia のマウスボタン。</param>
        /// <returns>対応する OpenTK のマウスボタン。</returns>
        private static OpenTK.Input.MouseButton GetOpenTKMouseButton(MouseButton button)
		{
			switch (button)
			{
				case MouseButton.Left:   return OpenTK.Input.MouseButton.Left;
				case MouseButton.Right:  return OpenTK.Input.MouseButton.Right;
				case MouseButton.Middle: return OpenTK.Input.MouseButton.Middle;
				case MouseButton.Extra1: return OpenTK.Input.MouseButton.Button1;
				case MouseButton.Extra2: return OpenTK.Input.MouseButton.Button2;
				case MouseButton.Extra3: return OpenTK.Input.MouseButton.Button3;
				case MouseButton.Extra4: return OpenTK.Input.MouseButton.Button4;
				case MouseButton.Extra5: return OpenTK.Input.MouseButton.Button5;
				case MouseButton.Extra6: return OpenTK.Input.MouseButton.Button6;
				case MouseButton.Extra7: return OpenTK.Input.MouseButton.Button7;
				case MouseButton.Extra8: return OpenTK.Input.MouseButton.Button8;
				case MouseButton.Extra9: return OpenTK.Input.MouseButton.Button9;
			}

			return OpenTK.Input.MouseButton.LastButton;
		}
	}
}
