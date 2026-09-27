using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.IO;
using Rydia.Resources;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;
using Rydia.Serialization;

namespace Rydia.Runtime.GLES2Samples
{

    public class E09ParticleSystemApp : GLES30App
    {
        private const int ParticleCount = 1024;

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        private struct Particle
        {
            public float Lifetime;
            public Vector3 StartPosition;
            public Vector3 EndPosition;
        }

        private NativeShaderProgram _program;
        private VertexAttribute _attrLifetime;
        private VertexAttribute _attrStartPos;
        private VertexAttribute _attrEndPos;
        private Uniform _uniTime;
        private Uniform _uniCenterPos;
        private Uniform _uniColor;
        private Uniform _uniSampler;
        private NativeTexture _texture;
        private Particle[] _particles = new Particle[ParticleCount];
        private float _particleTime;
        private Random _random = new Random();

        public override void Load()
        {

            // Compile vertex and fragment shaders
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform float u_time;
                uniform float u_pointScale;
                uniform vec3 u_centerPosition;

                attribute float a_lifetime;
                attribute vec3 a_startPosition;
                attribute vec3 a_endPosition;

                varying float v_lifetime;

                void main()
                {
                  if (u_time <= a_lifetime)
                  {
                    gl_Position.xyz = a_startPosition + (u_time * a_endPosition);
                    gl_Position.xyz += u_centerPosition;
                    gl_Position.w = 1.0;
                  }
                  else
                  {
                    gl_Position = vec4(-1000, -1000, 0, 0);
                  }

                  v_lifetime = 1.0 - (u_time / a_lifetime);
                  v_lifetime = clamp(v_lifetime, 0.0, 1.0);
                  gl_PointSize = (v_lifetime * v_lifetime) * u_pointScale;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                uniform vec4 u_color;
                uniform sampler2D s_texture;

                varying float v_lifetime;

                void main()
                {
                  vec4 texColor;
                  texColor = texture2D(s_texture, gl_PointCoord);
                  gl_FragColor = vec4(u_color) * texColor;
                  gl_FragColor.a *= v_lifetime;
                }");
            fragmentShader.Compile();

            // Link shaders into program
            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            // We don't need the shaders anymore. 
            // Note that the shaders won't actually be deleted until the program is deleted.
            vertexShader.Dispose();
            fragmentShader.Dispose();

            // Initialize vertex attributes
            this._attrLifetime = new VertexAttribute(this._program, "a_lifetime");
            this._attrStartPos = new VertexAttribute(this._program, "a_startPosition");
            this._attrEndPos = new VertexAttribute(this._program, "a_endPosition");

            // Initialize uniforms
            this._uniTime = new Uniform(this._program, "u_time");
            this._uniCenterPos = new Uniform(this._program, "u_centerPosition");
            this._uniColor = new Uniform(this._program, "u_color");
            this._uniSampler = new Uniform(this._program, "s_texture");

            // The gl_PointSize vertex shader output does not take screen scale into account.
            // So we scale it ourselves.
            this._program.Use();
            new Uniform(this._program, "u_pointScale").SetValue(40.0f * Platform.ScreenScale);

            // Set clear color to black
            GL.ClearColor(ColorRgba.DarkBlue);

            // Fill in particle data array
            for (int i = 0; i < ParticleCount; i++)
            {
                Particle particle = new Particle();
                particle.Lifetime = (float)this._random.NextDouble();

                double angle = this._random.NextDouble() * 2.0 * Math.PI;
                double radius = this._random.NextDouble() * 2.0;
                particle.EndPosition = new Vector3((float)(Math.Sin(angle) * radius), (float)(Math.Cos(angle) * radius), 0);

                angle = this._random.NextDouble() * 2.0 * Math.PI;
                radius = this._random.NextDouble() * 0.25;
                particle.StartPosition = new Vector3((float)(Math.Sin(angle) * radius), (float)(Math.Cos(angle) * radius), 0);

                this._particles[i] = particle;
            }

            this._particleTime = 1;

            // Load the textures
            var embedded = new EmbeddedResources<SampleApp>();

            var str = embedded.LoadText("Assets.smoke.PixelData.ryd");
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<PixelData>(temp);
            this._texture = resource.ToTexture();
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
            this._particleTime += GameTime.DeltaSec;
            if (this._particleTime >= 1)
            {
                this._particleTime = 0;

                // Pick a new start location and color
                Vector3 centerPos = new Vector3((float)this._random.NextDouble() - 0.5f, (float)this._random.NextDouble() - 0.5f, (float)this._random.NextDouble() - 0.5f);
                this._uniCenterPos.SetValue(ref centerPos);

                Vector4 color = new Vector4((float)this._random.NextDouble(), (float)this._random.NextDouble(), (float)this._random.NextDouble(), 1);
                this._uniColor.SetValue(ref color);
            }

            // Load uniform time variable
            this._uniTime.SetValue(this._particleTime);

            // Load the vertex attributes
            unsafe
            {
                fixed (float* lifetime = &this._particles[0].Lifetime)
                    this._attrLifetime.SetData(VertexAttribPointerType.Float, 1, lifetime, sizeof(Particle));

                fixed (float* startPos = &this._particles[0].StartPosition.X)
                    this._attrStartPos.SetData(VertexAttribPointerType.Float, 3, startPos, sizeof(Particle));

                fixed (float* endPos = &this._particles[0].EndPosition.X)
                    this._attrEndPos.SetData(VertexAttribPointerType.Float, 3, endPos, sizeof(Particle));
            }
            this._attrLifetime.Enable();
            this._attrStartPos.Enable();
            this._attrEndPos.Enable();

            // Blend particles
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.One);

            // Bind the texture
            this._texture.BindToTextureUnit(0);

            // Set the sampler texture unit to 0
            this._uniSampler.SetValue(0);

            // Draw the particle points
            GL.DrawArrays(PrimitiveType.Points, ParticleCount);
        }

        public override void Unload()
        {
            this._program.Dispose();
            this._texture.Dispose();
        }
    }

}
