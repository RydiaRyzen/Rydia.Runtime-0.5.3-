using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E20MorphingApp : GLES30App
    {
        private const int PointCount = 4096;

        private NativeShaderProgram _program;
        private NativeGraphicsBuffer[] _Vbos = new NativeGraphicsBuffer[4];
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;
        private Uniform _uniStatus;
        private float _status;

        public override void Load()
        {

            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;
                uniform vec3 Color1, Color2;
                uniform float Status, ScreenScale;

                attribute vec4 Position1, Position2;
                attribute float Radiance1, Radiance2;

                varying vec3 vertColor;

                void main(void)
                {
                  gl_Position = 
                    ProjectionMatrix * 
                    CameraMatrix * 
                    ModelMatrix * 
                    mix(Position1, Position2, Status);

                  gl_PointSize = (2.0 + 3.0 * mix(
                    Radiance1, 
                    Radiance2, 
                    Status)) * ScreenScale;

                  vertColor = mix(
                    (0.2 + Radiance1) * Color1,
                    (0.2 + Radiance2) * Color2,
                    Status);
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec3 vertColor;

                void main(void)
                {
                  gl_FragColor = vec4(vertColor, 1.0);
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            this._Vbos[0] = MakeShape1();
            this._Vbos[1] = MakeShape2();
            this._Vbos[2] = MakeRadiance("Radiance1");
            this._Vbos[3] = MakeRadiance("Radiance2");

            // Uniforms
            new Uniform(this._program, "Color1").SetValue(1.0f, 0.5f, 0.4f);
            new Uniform(this._program, "Color2").SetValue(1.0f, 0.8f, 0.7f);

            // The gl_PointSize vertex shader output does not take screen scale into account.
            // So we scale it ourselves.
            new Uniform(this._program, "ScreenScale").SetValue(Platform.ScreenScale);

            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
            this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");
            this._uniStatus = new Uniform(this._program, "Status");

            GL.ClearColor(0.2f, 0.2f, 0.2f, 0);
            GL.ClearDepth(1);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.Blend);
        }

        private NativeGraphicsBuffer MakeRadiance(string attrName)
        {
            Random random = new Random();
            float[] data = new float[PointCount];
            for (int i = 0; i < PointCount; i++)
            {
                data[i] = (float)(random.Next(100) * 0.01);
            }

            NativeGraphicsBuffer buffer = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            buffer.Bind();
            buffer.Data(data);

            VertexAttribute attr = new VertexAttribute(this._program, attrName);
            attr.SetConfig<float>();
            attr.Enable();

            return buffer;
        }

        private NativeGraphicsBuffer MakeShape1()
        {
            Random random = new Random();
            Vector3[] data = new Vector3[PointCount];
            for (int i = 0; i < PointCount; i++)
            {
                double phi = 2 * Math.PI * (random.Next(1000) * 0.001);
                double rho = 0.5 * Math.PI * ((random.Next(1000) * 0.002) - 1.0);

                float sPhi = (float)Math.Sin(phi);
                float cPhi = (float)Math.Cos(phi);
                float sRho = (float)Math.Sin(rho);
                float cRho = (float)Math.Cos(rho);

                data[i] = new Vector3(cPhi * cRho, sRho, sPhi * cRho);
            }

            NativeGraphicsBuffer buffer = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            buffer.Bind();
            buffer.Data(data);

            VertexAttribute attr = new VertexAttribute(this._program, "Position1");
            attr.SetConfig<Vector3>();
            attr.Enable();

            return buffer;
        }

        private NativeGraphicsBuffer MakeShape2()
        {
            Random random = new Random();
            Vector3[] data = new Vector3[PointCount];
            for (int i = 0; i < PointCount; i++)
            {
                double phi = 2 * Math.PI * (random.Next(1000) * 0.001);
                double rho = 2 * Math.PI * (random.Next(1000) * 0.001);

                float sPhi = (float)Math.Sin(phi);
                float cPhi = (float)Math.Cos(phi);
                float sRho = (float)Math.Sin(rho);
                float cRho = (float)Math.Cos(rho);

                data[i] = new Vector3(
                    cPhi * (0.5f + (0.5f * (1.0f + cRho))),
                    sRho * 0.5f,
                    sPhi * (0.5f + (0.5f * (1.0f + cRho))));
            }

            NativeGraphicsBuffer buffer = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            buffer.Bind();
            buffer.Data(data);

            VertexAttribute attr = new VertexAttribute(this._program, "Position2");
            attr.SetConfig<Vector3>();
            attr.Enable();

            return buffer;
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            if (((int)GameTime.TotalSec & 3) == 0)
            {
                this._status += GameTime.DeltaSec;
            }
            else
            {
                float truncStatus = (float)Math.Truncate(this._status);
                if (this._status != truncStatus)
                {
                    float frac = this._status - truncStatus;
                    if (frac < 0.5f)
                    {
                        this._status = truncStatus;
                    }
                    else
                    {
                        this._status = 1.0f + truncStatus;
                    }

                }
            }
            // Clear the color and depth buffer
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Use the program
            this._program.Use();

            this._uniStatus.SetValue(0.5f - (0.5f * (float)Math.Cos(Math.PI * this._status)));

            // Set the matrix for camera orbiting the origin
            Matrix4 cameraMatrix = Utils.OrbitCameraMatrix(
                Vector3.Zero, 5.5f,
                (float)(GameTime.TotalSec * Math.PI / 9.5),
                (float)MathFR.DegreesToRadians(45 + MathFR.Sin(MathFR.PI * GameTime.TotalSec / 7.5f) * 40));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Render
            Matrix4 modelMatrix = Matrix4.CreateRotationX((float)(this._status * Math.PI * 0.5));
            this._uniModelMatrix.SetValue(ref modelMatrix);
            GL.DrawArrays(PrimitiveType.Points, PointCount);
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(48), (float)newWidth / newHeight, 1, 20);
            this._program.Use();
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);
        }

        public override void Unload()
        {
            for (int i = 0; i < 3; i++)
            {
                this._Vbos[i].Dispose();
            }
            this._program.Dispose();
        }
    }

}
