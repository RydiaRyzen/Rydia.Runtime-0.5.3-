using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rydia;
using Rydia.Diagnostics;

namespace Rydia.Diagnostics
{

    public sealed class SingleLogger : Logger
    {

        public SingleLogger(string module)
            : base(module)
        {
            Shelf = new LoggerShelf();
        }

        public override string Prefix
        {
            get
            {
                return Module;
            }
        }

    }
}
