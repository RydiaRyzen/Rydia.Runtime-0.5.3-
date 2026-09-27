using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Runtime;
using Android.Views;
using Rydia.Input;
using static Android.Views.View;

namespace Rydia.Input
{

    public class KeyboardHandler : Java.Lang.Object, IOnKeyListener, IMobileKeyboard
    {

        bool[] pressedKeys = new bool[128];
        Pool<Rydia.Input.KeyEvent> keyEventPool;
        List<Rydia.Input.KeyEvent> keyEventsBuffer = new List<Rydia.Input.KeyEvent>();
        List<Rydia.Input.KeyEvent> keyEvents = new List<Rydia.Input.KeyEvent>();

        public KeyboardHandler(View view)
        {
            this.keyEventPool = new Pool<Rydia.Input.KeyEvent>(new KeyEventFactory(), 100);
            view.SetOnKeyListener(this);
            view.FocusableInTouchMode = true;
            view.RequestFocus();
        }

        public bool OnKey(View? v, [GeneratedEnum] Keycode keyCode, Android.Views.KeyEvent? e)
        {
            if (e.Action == KeyEventActions.Multiple)
                return false;

            lock (this)
            {
                var keyEvent = this.keyEventPool.NewObject();
                keyEvent.KeyCode = (int)keyCode;
                keyEvent.KeyChar = (char)e.UnicodeChar;

                if (e.Action == KeyEventActions.Down)
                {
                    keyEvent.Type = Rydia.Input.KeyEvent.KeyState.Down;
                    if ((int)keyCode > 0 && (int)keyCode < 127)
                        this.pressedKeys[(int)keyCode] = true;
                }

                if (e.Action == KeyEventActions.Up)
                {
                    keyEvent.Type = Rydia.Input.KeyEvent.KeyState.Up;
                    if ((int)keyCode > 0 && (int)keyCode < 127)
                        this.pressedKeys[(int)keyCode] = false;
                }

                this.keyEventsBuffer.Add(keyEvent);
            }

            return false;
        }
        public bool IsKeyPressed(int keyCode)
        {
            if (keyCode < 0 || keyCode > 127)
                return false;

            return this.pressedKeys[keyCode];
        }

        public List<Rydia.Input.KeyEvent> GetKeyEvents()
        {
            lock (this)
            {
                int len = this.keyEvents.Count;
                for (int i = 0; i < len; i++)
                    this.keyEventPool.Free(this.keyEvents[i]);

                this.keyEvents.Clear();
                this.keyEvents.AddRange(this.keyEventsBuffer);
                this.keyEventsBuffer.Clear();

                return this.keyEvents;
            }
        }
        private class KeyEventFactory : Pool<Rydia.Input.KeyEvent>.PoolObjectFactory<Rydia.Input.KeyEvent>
        {
            public Rydia.Input.KeyEvent CreateObject()
            {
                return new Rydia.Input.KeyEvent();
            }
        }

    }

}
