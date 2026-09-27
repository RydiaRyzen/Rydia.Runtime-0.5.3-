using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Platform.Android;
using Rydia.Runtime.Mobile;

namespace Rydia.Runtime.Droid
{
    public class GLView : AndroidGameView, IMobileWindow
    {

        private readonly AppRunner _application;

        public GLView(Context context, AppRunner application)
            : base(context)
        {
            Debug.Assert(application != null);
            this._application = application;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._application.Load();
            Run(60);
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this._application.Unload();
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            this._application.Update();
            this._application.Render();
            SwapBuffers();
        }

        protected override void CreateFrameBuffer()
        {
            ContextRenderingApi = GLVersion.ES2;
            GraphicsMode = new GraphicsMode(32, 16, this._application.NeedStencilBuffer ? 8 : 0, 0, 0, 2, false);
            base.CreateFrameBuffer();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this._application.Resize(Width, Height);
        }
    }
}
