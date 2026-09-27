using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Rydia.Runtime.GLES2Samples.ImGui
{

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GuiVertex
    {

        public float X;
        public float Y;
        
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public GuiVertex(float x, float y, byte r, byte g, byte b, byte a)
        {
            this.X = x;
            this.Y = y;

            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }

        public static int SizeInBytes
        {
            get
            {
                return Marshal.SizeOf<GuiVertex>();
            }
        }

    }
}
