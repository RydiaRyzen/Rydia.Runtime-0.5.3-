using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E15NoiseTorusApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _normals;
        private NativeGraphicsBuffer _texCoords;
        private NativeGraphicsBuffer _indices;
        private TorusGeometry _torus;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;
        private NativeTexture _texture;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;

                attribute vec3 Position;
                attribute vec3 Normal;
                attribute vec2 TexCoord;

                varying vec3 vertNormal;
                varying vec3 vertLight;
                varying vec2 vertTexCoord;

                uniform vec3 LightPos;

                void main(void)
                {
                  gl_Position = ModelMatrix * vec4(Position, 1.0);
                  vertNormal = mat3(ModelMatrix)*Normal;
                  vertLight = LightPos - gl_Position.xyz;
                  vertTexCoord = TexCoord;
                  gl_Position = ProjectionMatrix * CameraMatrix * gl_Position;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                uniform sampler2D TexUnit;

                varying vec3 vertNormal;
                varying vec3 vertLight;
                varying vec2 vertTexCoord;

                void main(void)
                {
                  float l = sqrt(length(vertLight));
                  float d = (l > 0.0) ? dot(
                    vertNormal, 
                    normalize(vertLight)
                  ) / l : 0.0;
                  float i = 0.2 + 3.2 * max(d, 0.0);
                  gl_FragColor = texture2D(TexUnit, vertTexCoord) * i;
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            this._torus = new TorusGeometry(72, 48, 1.0f, 0.5f);

            // Positions
            this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._verts.Bind();
            this._verts.Data(this._torus.Positions);

            VertexAttribute attr = new VertexAttribute(this._program, "Position");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Normals
            this._normals = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._normals.Bind();
            this._normals.Data(this._torus.Normals);

            attr = new VertexAttribute(this._program, "Normal");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Texture coordinates
            this._texCoords = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._texCoords.Bind();
            this._texCoords.Data(this._torus.TexCoords);

            attr = new VertexAttribute(this._program, "TexCoord");
            attr.SetConfig<Vector2>();
            attr.Enable();

            // Indices
            this._indices = new NativeGraphicsBuffer(BufferTarget.ElementArrayBuffer);
            this._indices.Bind();
            this._indices.Data(this._torus.Indices);

            // Don't need data anymore
            this._torus.Clear();

            // Texture
            Random random = new Random();
            byte[,] texData = new byte[256, 256];
            for (int v = 0; v < 256; v++)
            {
                for (int u = 0; u < 256; u++)
                {
                    texData[v, u] = (byte)random.Next(255);
                }
            }

            this._texture = new NativeTexture();
            this._texture.Bind();
            this._texture.MinificationFilter = TextureMinFilter.Linear;
            this._texture.MagnificationFilter = TextureMagFilter.Linear;
            this._texture.WrapS = TextureWrapMode.Repeat;
            this._texture.WrapT = TextureWrapMode.Repeat;
            this._texture.TexImage2D(PixelFormat.Luminance, 256, 256, texData);

            // Uniforms
            new Uniform(this._program, "TexUnit").SetValue(0);
            new Uniform(this._program, "LightPos").SetValue(4.0f, 4.0f, -8.0f);

            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
            this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");

            GL.ClearColor(0.8f, 0.8f, 0.7f, 0);
            GL.ClearDepth(1);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.FrontFace(FrontFaceDirection.Ccw);
            GL.CullFace(CullFaceMode.Back);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            // Clear the color and depth buffer
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            // Use the program
            this._program.Use();

            // Set the matrix for camera orbiting the origin
            Matrix4 cameraMatrix = Utils.OrbitCameraMatrix(
                Vector3.Zero, 4.5f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * 35),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * GameTime.TotalSec / 10) * 60));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Update and render the torus
            Matrix4 modelMatrix = Matrix4.CreateRotationX((float)(GameTime.TotalSec * Math.PI * 0.5));
            this._uniModelMatrix.SetValue(ref modelMatrix);
            this._torus.DrawWithBoundIndexBuffer();
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(60.0f), (float)newWidth / newHeight, 1, 20);
            this._program.Use();
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._indices.Dispose();
            this._normals.Dispose();
            this._texCoords.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
