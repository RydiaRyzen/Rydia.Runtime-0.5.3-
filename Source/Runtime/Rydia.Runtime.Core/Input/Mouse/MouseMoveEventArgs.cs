using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    public class MouseMoveEventArgs : MouseEventArgs
    {
        private Vector2 vel;

        public Vector2 Vel
        {
            get { return this.vel; }
        }

        public MouseMoveEventArgs(MouseInput inputChannel, Vector2 pos, Vector2 vel) : base(inputChannel, pos)
        {
            this.vel = vel;
        }
    }
}
