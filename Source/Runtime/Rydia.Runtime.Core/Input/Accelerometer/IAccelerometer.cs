using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    public interface IAccelerometer
    {


        float AccelX
        {
            get;
        }

        float AccelY
        {
            get;
        }

        float AccelZ
        {
            get;
        }

    }

}
