using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.GLES2Samples.ImGui
{
    public interface IGuiRenderer
    {

        void Draw(GuiContext guiContext, Matrix4 projection);

    }
}
