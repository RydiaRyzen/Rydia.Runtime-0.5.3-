using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia.Serialization;

namespace Rydia.Resources
{

    // <root dataType="Struct" type="Duality.Resources.AudioData" id="129723834">
    // 
    public sealed class AudioData
    {

        [FieldOrder(0)]
        public string Name
        {
            get;
            set;
        }

        public AudioData()
        {
        }

        [FieldOrder(1)]
        public int Channels
        {
            get;
            set;
        }

        [FieldOrder(2)]
        public int SampleRate
        {
            get;
            set;
        }

        [FieldOrder(3)]
        public short[] PCMData
        {
            get;
            set;
        }


    }
}
