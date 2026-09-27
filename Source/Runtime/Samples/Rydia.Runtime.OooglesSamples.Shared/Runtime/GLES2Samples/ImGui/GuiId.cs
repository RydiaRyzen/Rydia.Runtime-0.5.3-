using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.GLES2Samples.ImGui
{
    public static class GuiId
    {

        public static uint Get(string text)
        {
            uint hash = 2166136261;
            for (int i = 0; i < text.Length; i++)
            {
                hash = (hash * 16777619) ^ text[i];
            }
            return hash;
        }

    }
}
