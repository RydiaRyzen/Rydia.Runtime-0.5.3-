using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Input;
using Rydia.Input;
using Rydia.Runtime.Desktop;

namespace Rydia.Runtime.Windows
{

    /// <summary>
    /// OpenTK の <see cref="GameWindow"/> を基盤としたデスクトップ用ウィンドウを表します。
    /// </summary>
    public class GLWindow : GameWindow, IDesktopWindow
    {

        /// <summary>
        /// このウィンドウに関連付けられたアプリケーションを取得します。
        /// </summary>
        public AppRunner App
        {
            get;
            private set;
        }

        /// <summary>
        /// キーボード入力を取得します。
        /// </summary>
        public KeyboardInput Keyboard
        {
            get;
            private set;
        }

        /// <summary>
        /// マウス入力を取得します。
        /// </summary>
        public MouseInput Mouse
        {
            get;
            private set;
        }

        /// <summary>
        /// <see cref="GLWindow"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="application">このウィンドウで実行するアプリケーション。</param>
        /// <param name="options">ウィンドウの表示および描画に関する設定。</param>
        public GLWindow(AppRunner application, WindowOptions options)
            : base(options.Width, options.Height,
                  new  OpenTK.Graphics.GraphicsMode(32, 16, application.NeedStencilBuffer ? 8 : 0, 0, 0, 2, false),
                  options.Title,
                  options.ScreenMode == ScreenMode.Fullscreen ? GameWindowFlags.Fullscreen : 
                  options.ScreenMode == ScreenMode.FixedWindow ? GameWindowFlags.FixedWindow : 
                  GameWindowFlags.Default,
                  DisplayDevice.Default,
                  3, 0,
                  OpenTK.Graphics.GraphicsContextFlags.Embedded)
        {
            Debug.Assert(application != null);
            try
            {
                App = application;
                if (options.RefreshMode == RefreshMode.VSync)
                    VSync = VSyncMode.On;
                else if (options.RefreshMode == RefreshMode.AdaptiveVSync)
                    VSync = VSyncMode.Adaptive;
                else
                    VSync = VSyncMode.Off;
                Keyboard = new KeyboardInput();
                Mouse = new MouseInput();
                Keyboard.Source = new GameWindowKeyboardInputSource(this);
                Mouse.Source = new GameWindowMouseInputSource(this);
                if (options.ScreenMode == ScreenMode.FullWindow)
                {
                    WindowState = WindowState.Maximized;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// ウィンドウの読み込みが完了したときに呼び出されます。
        /// </summary>
        /// <param name="e">イベントの引数。</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            App.Load();
        }

        /// <summary>
        /// ウィンドウがアンロードされるときに呼び出されます。
        /// </summary>
        /// <param name="e">イベントの引数。</param>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            App.Unload();
        }

        /// <summary>
        /// ウィンドウの更新処理が実行されるときに呼び出されます。
        /// </summary>
        /// <param name="e">フレーム更新イベントの引数。</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Keyboard.Update(Width, Height);
            Mouse.Update(Width, Height);
            App.Update();
        }

        /// <summary>
        /// ウィンドウの描画処理が実行されるときに呼び出されます。
        /// </summary>
        /// <param name="e">フレーム描画イベントの引数。</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            App.Render();
            SwapBuffers();
        }

        /// <summary>
        /// ウィンドウのサイズが変更されたときに呼び出されます。
        /// </summary>
        /// <param name="e">サイズ変更イベントの引数。</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            App.Resize(Width, Height);
        }



    }

}
