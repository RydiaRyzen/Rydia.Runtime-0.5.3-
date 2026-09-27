using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Audio.Api.OpenAL
{
    public static class ALHost
    {
        private static bool initialized;

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

        public static void Init(IAL al, IAlc alc)
        {
            if(!initialized)
            {
                AL = al ?? throw new ArgumentNullException(nameof(al));
                Alc = alc ?? throw new ArgumentNullException(nameof(alc));
                initialized = true;
            }
        }

    }
}
