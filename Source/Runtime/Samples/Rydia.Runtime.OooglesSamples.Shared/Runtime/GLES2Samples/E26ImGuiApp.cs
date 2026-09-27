using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.Desktop;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.ImGui;

namespace Rydia.Runtime.GLES2Samples
{
    internal class E26ImGuiApp : GLES30App
    {

        private NativeShaderProgram _program;

        private NativeGraphicsBuffer _vbo;
        private NativeGraphicsBuffer _ibo;

        private VertexAttribute _positionLocation;
        private VertexAttribute _colorLocation;

        private Uniform _projectionLocation;

        private GuiContext gui;

        public override void Load()
        {
            this.gui = new GuiContext();
            // シェーダーを作成
            if (!CreateSimpleTextureShader(out this._program))
            {
                return;
            }
            this._program.Use();

            this._projectionLocation = this._program.GetUniformLocation("uProjection");

            this._positionLocation = this._program.GetAttribLocation("aPosition");
            this._colorLocation = this._program.GetAttribLocation("aColor");

            this._vbo = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._ibo = new NativeGraphicsBuffer(BufferTarget.ElementArrayBuffer);

            this.gui.Renderer = new ESGuiRenderer();
            GL.ClearColor(ColorRgba.DarkBlue);
        }

        public static bool CreateSimpleTextureShader(out NativeShaderProgram program)
        {
            string vertexShaderCode = @"
                attribute vec2 aPosition;
                attribute vec4 aColor;

                uniform mat4 uProjection;

                varying vec4 vColor;

                void main() {
                    vColor = aColor;

                    gl_Position = uProjection * vec4(aPosition, 0.0, 1.0);
                }";

            string fragmentShaderCode = @"
                precision mediump float;

                varying vec4 vColor;

                void main() {
                    gl_FragColor = vColor;
                }";

            var vertexShader = new NativeShader(ShaderType.VertexShader, vertexShaderCode);
            var fragmentShader = new NativeShader(ShaderType.FragmentShader, fragmentShaderCode);
            if (!vertexShader.Compile())
            {
                vertexShader.Dispose();
                program = null;
                return false;
            }
            if (!fragmentShader.Compile())
            {
                fragmentShader.Dispose();
                program = null;
                return false;
            }
            program = new NativeShaderProgram();
            program.AttachShader(vertexShader);
            program.AttachShader(fragmentShader);
            if (!program.Link())
            {
                program.Dispose();
                return false;
            }
            return true;
        }

        public override void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            var projection = Matrix4.CreateOrthographicOffCenter(0, Width, Height, 0, -1, 1);
            var mouse = Platform.As<IDesktopPlatform>().Window.Mouse;
            this.gui.BeginFrame(mouse.Pos, mouse.ButtonPressed(Input.MouseButton.Left));
            if(this.gui.Button("a"))
            {
                // ボタンが押されたときの処理
                Console.WriteLine("Button A pressed");
            }
            if(this.gui.Button("b"))
            {
                // ボタンが押されたときの処理
                Console.WriteLine("Button B pressed");
            }
            //this.gui.EndFrame(projection);
            this._program.Use();
            this._projectionLocation.SetValue(ref projection);

            this._vbo.Bind();
            this._vbo.Data(this.gui.Vertices.ToArray(), BufferUsageHint.DynamicDraw);

            this._ibo.Bind();
            this._ibo.Data(this.gui.Indices.ToArray(), BufferUsageHint.DynamicDraw);

            int stride = GuiVertex.SizeInBytes;
            this._positionLocation.Enable();
            this._positionLocation.SetConfig(VertexAttribPointerType.Float, 2, stride, 0);

            this._colorLocation.Enable();
            this._colorLocation.SetConfig(VertexAttribPointerType.UnsignedByte, 4, stride, 8);

            GL.DrawElements(PrimitiveType.Triangles, this.gui.Indices.Count, DrawElementsType.UnsignedShort, IntPtr.Zero);

            this._positionLocation.Disable();
            this._colorLocation.Disable();
        }

        public override void Unload()
        {
            this._program.Dispose();
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }
    }
}
