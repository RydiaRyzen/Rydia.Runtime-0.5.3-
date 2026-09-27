using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{
    public class TouchEvent
    {

        public enum TouchState
        {
            Down,
            Up,
            Dragged,
        }

        public TouchState Type;
        public int X;
        public int Y;
        public int Pointer;

    }
}
