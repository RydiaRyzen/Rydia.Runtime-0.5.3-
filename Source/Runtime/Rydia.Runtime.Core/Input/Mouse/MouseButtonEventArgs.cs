using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    public class MouseButtonEventArgs : MouseEventArgs
    {
        private MouseButton button;
        private bool pressed;

        public MouseButton Button
        {
            get { return this.button; }
        }
        public bool IsPressed
        {
            get { return this.pressed; }
        }

        public MouseButtonEventArgs(MouseInput inputChannel, Vector2 pos, MouseButton button, bool pressed) : base(inputChannel, pos)
        {
            this.button = button;
            this.pressed = pressed;
        }
    }
}
