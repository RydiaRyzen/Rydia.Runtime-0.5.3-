using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Api.ES30;
using Rydia.Graphics.Backend;

namespace Rydia.Runtime.GLES2Samples.ImGui
{
    public class ESGuiRenderer : IGuiRenderer
    {

        private static IGLES30 GL
        {
            get
            {
                return RuntimeHost.GLES30;
            }
        }

        private NativeShaderProgram _program;

        private NativeGraphicsBuffer _vbo;
        private NativeGraphicsBuffer _ibo;

        private VertexAttribute _positionLocation;
        private VertexAttribute _colorLocation;

        private Uniform _projectionLocation;

        private GuiContext gui;

        public void Draw(GuiContext gui, Matrix4 projection)
        {
        }

    }
}
