using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Diagnostics;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Api.ES30;
using Rydia.Runtime;

namespace Rydia
{


    public static class RuntimeHost
    {

        public static IGLES30 GLES30
        {
            get;
            private set;
        }

        public static void Init(IGLES30 gles30)
        {
            GlobalLogger.Core.Debug("Initializing Runtime Host...");
            GLES30 = gles30;
            GlobalLogger.Core.Debug("Runtime Host initialized.");
        }


    }
}
