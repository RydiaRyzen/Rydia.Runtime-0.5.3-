using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    public class MouseEventArgs : UserInputEventArgs
    {
        private Vector2 pos;

        public Vector2 Pos
        {
            get { return this.pos; }
        }

        public MouseEventArgs(MouseInput inputChannel, Vector2 pos) : base(inputChannel)
        {
            this.pos = pos;
        }
    }
}
