using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    public class KeyboardKeyEventArgs : UserInputEventArgs
    {
        private Key key;
        private bool pressed;

        public Key Key
        {
            get { return this.key; }
        }
        public bool IsPressed
        {
            get { return this.pressed; }
        }

        public KeyboardKeyEventArgs(KeyboardInput inputChannel, Key key, bool pressed) : base(inputChannel)
        {
            this.key = key;
            this.pressed = pressed;
        }
    }
}
