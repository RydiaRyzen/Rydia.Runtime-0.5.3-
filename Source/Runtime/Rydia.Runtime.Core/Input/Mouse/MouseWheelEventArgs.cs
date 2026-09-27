using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{
    public class MouseWheelEventArgs : MouseEventArgs
    {
        private float wheelValue;
        private float wheelSpeed;

        public float WheelValue
        {
            get { return this.wheelValue; }
        }
        public float WheelSpeed
        {
            get { return this.wheelSpeed; }
        }

        public MouseWheelEventArgs(MouseInput inputChannel, Vector2 pos, float value, float delta) : base(inputChannel, pos)
        {
            this.wheelValue = value;
            this.wheelSpeed = delta;
        }
    }
}
