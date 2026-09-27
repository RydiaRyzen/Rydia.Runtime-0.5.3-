using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Input;

namespace Rydia.Runtime.Mobile
{
    public interface IMobilePlatform : IPlatform
    {


        IAccelerometer Accelerometer
        {
            get;
        }

        IMobileKeyboard Keyboard
        {
            get;
        }

        ITouchHandler Touch
        {
            get;
        }

    }
}
