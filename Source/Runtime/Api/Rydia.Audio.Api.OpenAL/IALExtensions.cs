using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Audio.Api.OpenAL;
using Rydia.Audio.Api.OpenAL.Alc;
using Rydia.Runtime;


namespace Rydia.Audio.Api.OpenAL
{
    public static class IALExtensions
    {

        public static void CheckError(this IAL al)
        {
            ALError error = al.GetError();
            if (error != ALError.NoError)
            {
                string formatErrMsg = string.Format("OpenAL Error: {0} - {1}",
                    al.GetErrorString(error), AudioHost.Alc.GetCurrentContext().ToString());
                throw new ALException(formatErrMsg);
            }
        }

        public static void CheckError(this IAlc alc)
        {
            AlcError error = alc.GetError(AudioHost.Device);

            if (error != AlcError.NoError)
            {
                string formatErrMsg = string.Format("OpenALc Error: {0} - {1}",
                    alc.GetString(AudioHost.Device, (AlcGetString)error), alc.GetCurrentContext().ToString());
                throw new ALException(formatErrMsg);
            }
        }

    }
}
