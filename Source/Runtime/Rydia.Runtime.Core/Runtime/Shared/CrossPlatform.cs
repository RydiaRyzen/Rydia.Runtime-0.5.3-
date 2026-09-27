using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Text;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.Mobile;
using Rydia.Runtime.Shared;

namespace Rydia.Runtime
{

    public class CrossPlatform : IPlatform
    {

        #region IPlatform

        public IPlatform Platform
        {
            get;
            private set;
        }

        public float ScreenScale
        {
            get
            {
                return Platform.ScreenScale;
            }
        }

        public AppRunner App
        {
            get
            {
                return Platform.App;
            }
        }

        public Rydia.Runtime.Shared.DialogResult ShowMessage(MessageDialogEventArgs args)
        {
            return Platform.ShowMessage(args);
        }

        #endregion

        public CrossPlatform(IDesktopPlatform platform)
        {
            Platform = platform;
        }

        public CrossPlatform(IMobilePlatform platform)
        {
            Platform = platform;
        }

        public bool Is<TPlatform>() where TPlatform : IPlatform
        {
            return Platform is TPlatform;
        }

        public TPlatform As<TPlatform>()
            where TPlatform : IPlatform
        {
            return (TPlatform)Platform;
        }

        public bool IsAs<TPlatform>(out TPlatform platform)
            where TPlatform : IPlatform
        {
            if (Platform is TPlatform p)
            {
                platform = p;
                return true;
            }
            platform = default;
            return false;
        }

    }

}
