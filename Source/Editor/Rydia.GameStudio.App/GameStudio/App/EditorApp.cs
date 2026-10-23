using ImGuiNET;
using Rydia.Graphics.Api.ES30;
using Rydia.Runtime;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;

namespace Rydia.GameStudio.App
{
    internal class EditorApp : AppRunner
    {


        public IGLES30 GL
        {
            get
            {
                return RuntimeHost.GLES30;
            }
        }

        public override void Load()
        {
            GL.ClearColor(Rydia.Drawing.ColorRgba.CornflowerBlue);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }

        public override void Unload()
        {

        }

    }
}