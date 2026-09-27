using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E17HeliumApp : GLES30App
    {
        private class Particle : IDisposable
        {
            private NativeShaderProgram _program;
            private NativeGraphicsBuffer _verts;
            private NativeGraphicsBuffer _normals;
            private NativeGraphicsBuffer _indices;
            private SphereGeometry _sphere;
            private Uniform _uniProjectionMatrix;
            private Uniform _uniCameraMatrix;
            private Uniform _uniModelMatrix;
            private Uniform _uniLightPos;

            public Particle(NativeShader vertexShader, NativeShader fragmentShader)
            {
                this._program = new NativeShaderProgram(vertexShader, fragmentShader);
                this._program.Link();
                this._program.Use();

                // Don't need fragment shader anymore (vertex shader is shared though)
                fragmentShader.Dispose();

                // Initialize uniforms
                this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
                this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
                this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");
                this._uniLightPos = new Uniform(this._program, "LightPos");

                this._sphere = new SphereGeometry(18, 1.0f);

                // Positions
                this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
                this._verts.Bind();
                this._verts.Data(this._sphere.Positions);

                VertexAttribute attr = new VertexAttribute(this._program, "Position");
                attr.SetConfig<Vector3>();
                attr.Enable();

                // Normals
                this._normals = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
                this._normals.Bind();
                this._normals.Data(this._sphere.Normals);

                attr = new VertexAttribute(this._program, "Normal");
                attr.SetConfig<Vector3>();
                attr.Enable();

                // Indices
                this._indices = new NativeGraphicsBuffer(BufferTarget.ElementArrayBuffer);
                this._indices.Bind();
                this._indices.Data(this._sphere.Indices);

                // Don't need data anymore
                this._sphere.Clear();
            }

            public void SetProjection(ref Matrix4 projection)
            {
                this._program.Use();
                this._uniProjectionMatrix.SetValue(ref projection);
            }

            public void SetLightAndCamera(ref Vector3 light, ref Matrix4 projection)
            {
                this._program.Use();
                this._uniLightPos.SetValue(ref light);
                this._uniCameraMatrix.SetValue(ref projection);
            }

            public void Render(Matrix4 model)
            {
                this._program.Use();
                this._uniModelMatrix.SetValue(ref model);
                this._verts.Bind();
                this._normals.Bind();
                this._sphere.DrawWithBoundIndexBuffer();
            }

            public void Dispose()
            {
                this._program.Dispose();
                this._verts.Dispose();
                this._normals.Dispose();
                this._indices.Dispose();
            }
        }

        private Particle _proton;
        private Particle _neutron;
        private Particle _electron;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;

                attribute vec3 Position;
                attribute vec3 Normal;

                varying vec3 vertNormal;
                varying vec3 vertLight;
                varying vec3 vertViewNormal;

                uniform vec3 LightPos;

                void main(void)
                {
                  gl_Position = ModelMatrix * vec4(Position, 1.0);
                  vertNormal = mat3(ModelMatrix) * Normal;
                  vertViewNormal = mat3(CameraMatrix) * vertNormal;
                  vertLight = LightPos - gl_Position.xyz;
                  gl_Position = ProjectionMatrix * CameraMatrix * gl_Position;
                }");
            vertexShader.Compile();

            this._proton = new Particle(vertexShader, CreateFragmentShader(@"
                  bool sig = (
                    abs(vertViewNormal.x) < 0.5 &&
                    abs(vertViewNormal.y) < 0.2 
                  ) || (
                    abs(vertViewNormal.y) < 0.5 &&
                    abs(vertViewNormal.x) < 0.2 
                  );
                  vec3 color = vec3(1.0, 0.0, 0.0);
                "));

            this._neutron = new Particle(vertexShader, CreateFragmentShader(@"
                  bool sig = false;
                  vec3 color = vec3(0.5, 0.5, 0.5);
                "));

            this._electron = new Particle(vertexShader, CreateFragmentShader(@"
                  bool sig = (
                    abs(vertViewNormal.x) < 0.5 &&
                    abs(vertViewNormal.y) < 0.2
                  );
                  vec3 color = vec3(0.0, 0.0, 1.0);
                "));

            // Don't need vertex shader anymore
            vertexShader.Dispose();

            GL.ClearColor(0.3f, 0.3f, 0.3f, 0);
            GL.ClearDepth(1);
            GL.Enable(EnableCap.DepthTest);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        private NativeShader CreateFragmentShader(string source)
        {
            NativeShader shader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec3 vertNormal;
                varying vec3 vertLight;
                varying vec3 vertViewNormal;

                void main(void)
                {
                  float lighting = dot(
                    vertNormal, 
                    normalize(vertLight));

                  float intensity = clamp(
                    0.4 + lighting * 1.0,
                    0.0,
                    1.0);
                " + source + @"
                  gl_FragColor = sig ? 
                    vec4(1.0, 1.0, 1.0, 1.0):
                    vec4(color * intensity, 1.0);
                }");

            shader.Compile();
            return shader;
        }

        public override void Render()
        {
            // Clear the color and depth buffer
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Vector3 light = new Vector3(8, 8, 8);

            // Set the matrix for camera orbiting the origin
            Matrix4 camera = Utils.OrbitCameraMatrix(
                Vector3.Zero, 21.0f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * 15),
                (float)MathFR.DegreesToRadians(MathFR.PI * GameTime.TotalSec * 0.3f) * 45);

            Matrix4 nucl = Matrix4.CreateFromAxisAngle(new Vector3(1, 1, 1), (float)(GameTime.TotalSec * 2 * Math.PI));

            this._proton.SetLightAndCamera(ref light, ref camera);
            Matrix4 model = Matrix4.CreateTranslation(1.4f, 0, 0);
            this._proton.Render(model * nucl);
            model = Matrix4.CreateTranslation(-1.4f, 0, 0);
            this._proton.Render(model * nucl);

            this._neutron.SetLightAndCamera(ref light, ref camera);
            model = Matrix4.CreateTranslation(0, 0, 1);
            this._neutron.Render(model * nucl);
            model = Matrix4.CreateTranslation(0, 0, -1);
            this._neutron.Render(model * nucl);

            this._electron.SetLightAndCamera(ref light, ref camera);

            Matrix4 rotate = Matrix4.CreateRotationY((float)(GameTime.TotalSec * Math.PI * 1.4));
            model = Matrix4.CreateTranslation(10, 0, 0);
            this._electron.Render(model * rotate);

            rotate = Matrix4.CreateRotationX((float)(GameTime.TotalSec * Math.PI * 1.4));
            model = Matrix4.CreateTranslation(0, 0, 10);
            this._electron.Render(model * rotate);
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(45.0f), (float)newWidth / newHeight, 1, 50);
            this._proton.SetProjection(ref projectionMatrix);
            this._neutron.SetProjection(ref projectionMatrix);
            this._electron.SetProjection(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._proton.Dispose();
            this._neutron.Dispose();
            this._electron.Dispose();
        }
    }

}
