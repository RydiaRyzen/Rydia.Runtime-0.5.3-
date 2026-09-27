using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rydia.Audio.Api.OpenAL;
using Rydia.Graphics.Api.ES30;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.Shared;

namespace Rydia.Runtime.Windows
{


    public class WindowsPlatform : DisposableBase, IDesktopPlatform
    {

        #region static

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetDllDirectory(string path);

        static WindowsPlatform()
        {
            string path = Assembly.GetEntryAssembly().Location;
            path = Path.GetDirectoryName(path);
            path = Path.Combine(path, (IntPtr.Size == 8) ? "x64" : "x86");
            SetDllDirectory(path);
        }

        #endregion

        #region IDesktopPlatform

        public WindowOptions WindowOptions
        {
            get;
            private set;
        }

        public IDesktopWindow Window
        {
            get;
            private set;
        }

        #endregion

        #region IPlatform

        public AppRunner App
        {
            get;
            private set;
        }

        public float ScreenScale
        {
            get
            {
                return 1.0f;
            }
        }

        public AudioOptions AudioOptions
        {
            get;
            private set;
        }

        #endregion

        public WindowsPlatform()
        {
            WindowOptions = new WindowOptions();
            AudioOptions = new AudioOptions();
            RuntimeHost.Init(new GLES30Bindings());
            AudioHost.Init(new ALBindings(), new AlcBindings());
        }

        public void Run(AppRunner app)
        {
            App = app;
            AudioHost.CreateDeviceContext(AudioOptions);
            Window = new GLWindow(app, WindowOptions);
            app.Init(new CrossPlatform(this), WindowOptions.Width, WindowOptions.Height);
            Window.Run(60);
        }

        protected override void Disposing(bool disposing)
        {
            AudioHost.Terminate();
            Window.Dispose();
        }

        public Shared.DialogResult ShowMessage(MessageDialogEventArgs args)
        {
            var button = 
                args.Buttons == Shared.MessageBoxButtons.OKCancel ? System.Windows.Forms.MessageBoxButtons.OKCancel :
                args.Buttons == Shared.MessageBoxButtons.YesNo ? System.Windows.Forms.MessageBoxButtons.YesNo :
                args.Buttons == Shared.MessageBoxButtons.YesNoCancel ? System.Windows.Forms.MessageBoxButtons.YesNoCancel :
                System.Windows.Forms.MessageBoxButtons.OK;
            var result = MessageBox.Show(args.Message, args.Title, button);
            return
                result == System.Windows.Forms.DialogResult.OK ? Shared.DialogResult.OK :
                result == System.Windows.Forms.DialogResult.Cancel ? Shared.DialogResult.Cancel :
                result == System.Windows.Forms.DialogResult.Yes ? Shared.DialogResult.Yes : Shared.DialogResult.No;
        }

    }

}
