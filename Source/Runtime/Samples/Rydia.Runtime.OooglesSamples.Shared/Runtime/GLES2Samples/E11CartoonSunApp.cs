using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;

namespace Rydia.Runtime.GLES2Samples
{

    public class E11CartoonSunApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private Uniform _uniTime;
        private Uniform _uniSunPos;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                attribute vec2 Position;

                varying vec2 vertPos;

                void main(void)
                {
                  gl_Position = vec4(Position, 0.0, 1.0);
                  vertPos = gl_Position.xy;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                uniform float Time;

                uniform vec2 SunPos;
                uniform vec3 Sun1, Sun2, Sky1, Sky2;

                varying vec2 vertPos;

                void main(void)
                {
                  vec2 v = vertPos - SunPos;
                  float l = length(v);
                  float a = (sin(l) + atan(v.y, v.x)) / 3.1415;
                  if (l < 0.1)
                  {
                    gl_FragColor = vec4(Sun1, 1.0);
                  }
                  else if (floor(mod(18.0 * (Time * 0.1 + 1.0 + a), 2.0)) == 0.0)
                  {
                    gl_FragColor = vec4(mix(Sun1, Sun2, l), 1.0);
                  }
                  else
                  {
                    gl_FragColor = vec4(mix(Sky1, Sky2, l), 1.0);
                  }
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
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

            // Uniforms
            this._uniTime = new Uniform(this._program, "Time");
            this._uniSunPos = new Uniform(this._program, "SunPos");

            new Uniform(this._program, "Sun1").SetValue(new Vector3(0.95f, 0.85f, 0.60f));
            new Uniform(this._program, "Sun2").SetValue(new Vector3(0.90f, 0.80f, 0.20f));
            new Uniform(this._program, "Sky1").SetValue(new Vector3(0.90f, 0.80f, 0.50f));
            new Uniform(this._program, "Sky2").SetValue(new Vector3(0.80f, 0.60f, 0.40f));

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

            // Update uniforms
            this._uniTime.SetValue((float)GameTime.TotalSec);

            double angle = GameTime.TotalSec * Math.PI * 0.1;
            this._uniSunPos.SetValue((float)-Math.Cos(angle), (float)Math.Sin(angle));

            // Draw the rectangle
            GL.DrawArrays(PrimitiveType.TriangleStrip, 4);
        }

        public override void Unload()
        {
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
