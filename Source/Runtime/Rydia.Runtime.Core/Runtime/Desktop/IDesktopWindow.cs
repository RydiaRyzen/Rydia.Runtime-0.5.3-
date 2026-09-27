using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Input;

namespace Rydia.Runtime.Desktop
{
    public interface IDesktopWindow : IDisposable
    {

        KeyboardInput Keyboard
        {
            get;
        }

        MouseInput Mouse
        {
            get;
        }

        void Run(double v);

    }

}
