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

    public class GLWindow : GameWindow, IDesktopWindow
    {

        public AppRunner App
        {
            get;
            private set;
        }

        public KeyboardInput Keyboard
        {
            get;
            private set;
        }

        public MouseInput Mouse
        {
            get;
            private set;
        }

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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            App.Load();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            App.Unload();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            Keyboard.Update(Width, Height);
            Mouse.Update(Width, Height);
            App.Update();
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            App.Render();
            SwapBuffers();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            App.Resize(Width, Height);
        }



    }

}
