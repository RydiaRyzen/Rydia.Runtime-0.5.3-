using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Audio.Api.OpenAL.Alc
{

    public enum AlcError
    {

        NoError = 0,
        InvalidDevice = 0xA001,
        InvalidContext = 0xA002,
        InvalidEnum = 0xA003,
        InvalidValue = 0xA004,
        OutOfMemory = 0xA005,

    }

}
