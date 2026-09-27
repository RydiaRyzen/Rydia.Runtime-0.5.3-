using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{
    

    public interface ITouchHandler
    {

        bool IsTouchDown(int pointer);

        int GetTouchX(int pointer);

        int GetTouchY(int pointer);

        List<TouchEvent> GetTouchEvents();

    }

}
