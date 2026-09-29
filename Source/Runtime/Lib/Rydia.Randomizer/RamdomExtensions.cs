using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Randomizer
{
    public static class RamdomExtensions
    {

        public static float NextFloat(this Random rnd, float min, float max)
        {
            return rnd.Next((int)(min * 100), (int)(max * 100)) / 100f;
        }
    }
}
