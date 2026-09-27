using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Rydia.Runtime.Desktop;

namespace Rydia.Runtime
{


    public abstract class AppRunner
    {

        private Action _PlatformUpdate;

        public virtual bool NeedStencilBuffer
        {
            get
            {
                return false;
            }
        }

        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        public GameTime GameTime
        {
            get;
            private set;
        }

        public CrossPlatform Platform
        {
            get;
            private set;
        }

        public void Init(CrossPlatform platform, int width, int height)
        {
            Debug.Assert(platform != null);
            Debug.Assert(width > 0);
            Debug.Assert(height > 0);

            Platform = platform;
            Width = width;
            Height = height;

            if(Platform.Is<IDesktopPlatform>())
            {
                this._PlatformUpdate = DesktopUpdate;
            }
            else
            {
                this._PlatformUpdate = MobileUpdate;
            }

            GameTime = new GameTime();
        }

        public abstract void Load();

        public void Update()
        {
            GameTime.Update();
            this._PlatformUpdate?.Invoke();
            Update(GameTime.DeltaSec, GameTime.TotalSec);
        }

        private void MobileUpdate()
        {

        }

        private void DesktopUpdate()
        {
            if(Platform.IsAs<IDesktopPlatform>(out var desktopPlatform))
            {
                desktopPlatform.Window.Keyboard.Update(Width, Height);
                desktopPlatform.Window.Mouse.Update(Width, Height);
            }
        }

        public abstract void Update(float deltaTimeSec, float totalTimeSec);

        public abstract void Render();

        public virtual void Resize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public abstract void Unload();

    }

}
