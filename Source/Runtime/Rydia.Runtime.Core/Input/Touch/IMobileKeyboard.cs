using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{


    public interface IMobileKeyboard
    {

        bool IsKeyPressed(int keyCode);

        List<KeyEvent> GetKeyEvents();

    }

}
