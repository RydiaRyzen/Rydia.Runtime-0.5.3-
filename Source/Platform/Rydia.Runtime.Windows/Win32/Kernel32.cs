using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Win32
{


    public static class Kernel32
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetDllDirectory(string path);


    }
}
