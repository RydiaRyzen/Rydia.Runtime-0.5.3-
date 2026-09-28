using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{
    public class KeyEvent
    {

        public enum KeyState
        {
            Down,
            Up,
        }

        public KeyState Type;
        public int KeyCode;
        public char KeyChar;

    }
}
