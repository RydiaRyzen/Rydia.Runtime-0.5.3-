using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using Rydia.Input;

namespace Rydia.Input
{
    public abstract class TouchHandler : Java.Lang.Object, View.IOnTouchListener, ITouchHandler
    {

        public abstract bool IsTouchDown(int pointer);

        public abstract int GetTouchX(int pointer);

        public abstract int GetTouchY(int pointer);

        public abstract List<TouchEvent> GetTouchEvents();

        public abstract bool OnTouch(View? v, MotionEvent? e);

    }
}
