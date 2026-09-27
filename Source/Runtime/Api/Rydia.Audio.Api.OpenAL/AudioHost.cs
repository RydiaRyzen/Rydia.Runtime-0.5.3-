using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Diagnostics;
using Rydia.Runtime;

namespace Rydia.Audio.Api.OpenAL
{
    public static class AudioHost
    {


        public static IAL AL
        {
            get;
            private set;
        }

        public static IAlc Alc
        {
            get;
            private set;
        }

        public static IntPtr Device
        {
            get;
            private set;
        }

        public static IntPtr Context
        {
            get;
            private set;
        }

        public static void Init(IAL al, IAlc alc)
        {
            GlobalLogger.Core.Debug("Initializing Runtime Host...");
            AL = al;
            Alc = alc;
            GlobalLogger.Core.Debug("Runtime Host initialized.");
        }

        public static void CreateDeviceContext(AudioOptions options)
        {
            GlobalLogger.Core.Debug("Creating OpenAL Device and Context...");
            Device = Alc.OpenDevice(options.DeviceName);
            Context = Alc.CreateContext(Device, options.Argument);
            Alc.MakeContextCurrent(Context);
            GlobalLogger.Core.Debug("OpenAL Device and Context created.");
        }

        public static void Terminate()
        {
            GlobalLogger.Core.Debug("Terminating Runtime Host...");
            Alc.DestroyContext(Context);
            Alc.CloseDevice(Device);
            GlobalLogger.Core.Debug("Runtime Host terminated.");
        }
    }
}
