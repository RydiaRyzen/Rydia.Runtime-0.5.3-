using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Views;
using Rydia.Input;

namespace Rydia.Input
{
    public class MultiTouchHandler : TouchHandler, ITouchHandler
    {

        bool[] isTouched = new bool[20];
        int[] touchX = new int[20];
        int[] touchY = new int[20];
        Pool<TouchEvent> touchEventPool;
        List<TouchEvent> touchEvents = new List<TouchEvent>();
        List<TouchEvent> touchEventsBuffer = new List<TouchEvent>();
        float scaleX;
        float scaleY;

        public MultiTouchHandler(View view, float scaleX, float scaleY)
        {
            this.touchEventPool = new Pool<TouchEvent>(new TouchEventFactory(), 100);
            view.SetOnTouchListener(this);

            this.scaleX = scaleX;
            this.scaleY = scaleY;
        }

        public override bool OnTouch(View? v, MotionEvent? e)
        {
            lock (this)
            {
                var action = e.ActionMasked;
                int pointerIndex = e.ActionIndex;
                int pointerId = e.GetPointerId(pointerIndex);
                TouchEvent touchEvent;

                switch (action)
                {
                    case MotionEventActions.Down:
                    case MotionEventActions.PointerDown:
                        touchEvent = this.touchEventPool.NewObject();
                        touchEvent.Type = TouchEvent.TouchState.Down;
                        touchEvent.Pointer = pointerId;
                        touchEvent.X = this.touchX[pointerId] = (int)(e.GetX(pointerIndex) * this.scaleX);
                        touchEvent.Y = this.touchY[pointerId] = (int)(e.GetY(pointerIndex) * this.scaleY);
                        this.isTouched[pointerId] = true;
                        this.touchEventsBuffer.Add(touchEvent);
                        break;

                    case MotionEventActions.Up:
                    case MotionEventActions.PointerUp:
                    case MotionEventActions.Cancel:
                        touchEvent = this.touchEventPool.NewObject();
                        touchEvent.Type = TouchEvent.TouchState.Up;
                        touchEvent.Pointer = pointerId;
                        touchEvent.X = this.touchX[pointerId] = (int)(e.GetX(pointerIndex) * this.scaleX);
                        touchEvent.Y = this.touchY[pointerId] = (int)(e.GetY(pointerIndex) * this.scaleY);
                        this.isTouched[pointerId] = false;
                        this.touchEventsBuffer.Add(touchEvent);
                        break;

                    case MotionEventActions.Move:
                        int pointerCount = e.PointerCount;
                        for (int i = 0; i < pointerCount; i++)
                        {
                            pointerIndex = i;
                            pointerId = e.GetPointerId(pointerIndex);

                            touchEvent = this.touchEventPool.NewObject();
                            touchEvent.Type = TouchEvent.TouchState.Dragged;
                            touchEvent.Pointer = pointerId;
                            touchEvent.X = this.touchX[pointerId] = (int)(e.GetX(pointerIndex) * this.scaleX);
                            touchEvent.Y = this.touchY[pointerId] = (int)(e.GetY(pointerIndex) * this.scaleY);
                            this.touchEventsBuffer.Add(touchEvent);
                        }
                        break;
                }
                return true;
            }
        }

        public override bool IsTouchDown(int pointer)
        {
            lock (this)
            {
                if (pointer < 0 || pointer >= 20)
                    return false;
                return this.isTouched[pointer];
            }
        }

        public override int GetTouchX(int pointer)
        {
            lock (this)
            {
                if (pointer < 0 || pointer >= 20)
                    return 0;
                return this.touchX[pointer];
            }
        }

        public override int GetTouchY(int pointer)
        {
            lock (this)
            {
                if (pointer < 0 || pointer >= 20)
                    return 0;
                return this.touchY[pointer];
            }
        }

        public override List<TouchEvent> GetTouchEvents()
        {
            lock (this)
            {
                int len = this.touchEvents.Count;
                for (int i = 0; i < len; i++)
                    this.touchEventPool.Free(this.touchEvents[i]);

                this.touchEvents.Clear();
                this.touchEvents.AddRange(this.touchEventsBuffer);
                this.touchEventsBuffer.Clear();
                return this.touchEvents;
            }
        }

        private class TouchEventFactory : Java.Lang.Object, Pool<TouchEvent>.PoolObjectFactory<TouchEvent>
        {

            public TouchEvent CreateObject()
            {
                return new TouchEvent();
            }

        }

    }
}
