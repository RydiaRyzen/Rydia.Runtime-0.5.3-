using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Audio.Api.OpenAL;
using Rydia.Graphics.Api.ES30;

namespace Rydia.Runtime.ES30
{
    public abstract class GLES30App : AppRunner
    {


        public static IGLES30 GL
        {
            get
            {
                return RuntimeHost.GLES30;
            }
        }

        public IAL AL
        {
            get
            {
                return AudioHost.AL;
            }
        }

    }
}
