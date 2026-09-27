using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;

namespace Rydia.Runtime.GLES2Samples
{

    public class E10MandelbrotApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _coords;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                attribute vec2 Position;
                attribute vec2 Coord;

                varying vec2 vertCoord;

                void main(void)
                {
                  vertCoord = Coord;
                  gl_Position = vec4(Position, 0.0, 1.0);
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision highp float;

                varying vec2 vertCoord;

                const int nclr = 5;

                uniform vec4 clrs[5];

                void main(void)
                {
                  vec2 z = vec2(0.0, 0.0);
                  vec2 c = vertCoord;

                  int i = 0, max = 128;
                  while ((i != max) && (distance(z, c) < 2.0))
                  {
                    vec2 zn = vec2(
                      z.x * z.x - z.y * z.y + c.x,
                      2.0 * z.x * z.y + c.y);
                    z = zn;
                    ++i;
                  }

                  float a = sqrt(float(i) / float(max));

                  for (i = 0; i != (nclr - 1); ++i)
                  {
                    if((a > clrs[i].a) && (a <= clrs[i+1].a))
                    {
                      float m = (a - clrs[i].a) / (clrs[i+1].a - clrs[i].a);
                      gl_FragColor = vec4(
                        mix(clrs[i].rgb, clrs[i+1].rgb, m),
                        1.0
                      );
                      break;
                    }
                  }
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram();
            this._program.AttachShader(vertexShader);
            this._program.AttachShader(fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            // Positions
            this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._verts.Bind();
            this._verts.Data(new Vector2[] {
                new Vector2(-1, -1),
                new Vector2(-1,  1),
                new Vector2( 1, -1),
                new Vector2( 1,  1)});

            VertexAttribute attr = new VertexAttribute(this._program, "Position");
            attr.SetConfig<Vector2>();
            attr.Enable();

            // Mandelbrot coordinates
            this._coords = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._coords.Bind();
            this._coords.Data(new Vector2[] {
                new Vector2(-1.5f, -0.5f),
                new Vector2(-1.5f,  1.0f),
                new Vector2( 0.5f, -0.5f),
                new Vector2( 0.5f,  1.0f)});

            attr = new VertexAttribute(this._program, "Coord");
            attr.SetConfig<Vector2>();
            attr.Enable();

            // Color map
            Uniform uniform = new Uniform(this._program, "clrs");
            uniform.SetValues(new Vector4[] {
                new Vector4(0.4f, 0.2f, 1.0f, 0.00f),
                new Vector4(1.0f, 0.2f, 0.2f, 0.30f),
                new Vector4(1.0f, 1.0f, 1.0f, 0.95f),
                new Vector4(1.0f, 1.0f, 1.0f, 0.98f),
                new Vector4(0.1f, 0.1f, 0.1f, 1.00f)});

            GL.Disable(EnableCap.DepthTest);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            // Clear the color buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Use the program
            this._program.Use();

            // Draw the rectangle
            GL.DrawArrays(PrimitiveType.TriangleStrip, 4);
        }

        public override void Unload()
        {
            this._coords.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
