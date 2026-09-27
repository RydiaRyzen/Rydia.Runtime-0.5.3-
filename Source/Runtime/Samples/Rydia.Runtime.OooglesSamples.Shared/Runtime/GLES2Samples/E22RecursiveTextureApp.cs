using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E22RecursiveTextureApp : GLES30App
    {
        private const int TextureSize = 512;

        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _normals;
        private NativeGraphicsBuffer _texCoords;
        private NativeGraphicsBuffer _indices;
        private Uniform _uniTexUnit;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;
        private NativeFramebuffer _defaultFramebuffer;
        private NativeFramebuffer[] _framebuffers = new NativeFramebuffer[2];
        private NativeRenderbuffer[] _renderbuffers = new NativeRenderbuffer[2];
        private NativeTexture[] _textures = new NativeTexture[2];
        private int _currentTextureIndex;
        private CubeGeometry _cube;

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
                  vertNormal = mat3(ModelMatrix) * Normal;
                  gl_Position = ModelMatrix * vec4(Position, 1.0);
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
                  float d = (l > 0.0) ? dot(vertNormal, normalize(vertLight)) / l : 0.0;
                  float i = 0.6 + max(d, 0.0);
                  gl_FragColor = texture2D(TexUnit, vertTexCoord) * i;
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            this._cube = new CubeGeometry();

            // Positions
            this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._verts.Bind();
            this._verts.Data(this._cube.Positions);

            VertexAttribute attr = new VertexAttribute(this._program, "Position");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Normals
            this._normals = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._normals.Bind();
            this._normals.Data(this._cube.Normals);

            attr = new VertexAttribute(this._program, "Normal");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Texture coordinates
            this._texCoords = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._texCoords.Bind();
            this._texCoords.Data(this._cube.TexCoords);

            attr = new VertexAttribute(this._program, "TexCoord");
            attr.SetConfig<Vector2>();
            attr.Enable();

            // Indices
            this._indices = new NativeGraphicsBuffer(BufferTarget.ElementArrayBuffer);
            this._indices.Bind();
            this._indices.Data(this._cube.Indices);

            // Don't need data anymore
            this._cube.Clear();

            // Textures, renderbuffers and framebuffers
            this._defaultFramebuffer = NativeFramebuffer.Current;
            for (int i = 0; i < 2; i++)
            {
                NativeTexture texture = new NativeTexture();
                texture.BindToTextureUnit(i);
                texture.MinificationFilter = TextureMinFilter.Linear;
                texture.MagnificationFilter = TextureMagFilter.Linear;
                texture.WrapS = TextureWrapMode.Repeat;
                texture.WrapT = TextureWrapMode.Repeat;
                texture.Reserve(PixelFormat.Rgba, TextureSize, TextureSize);
                this._textures[i] = texture;

                NativeRenderbuffer renderbuffer = new NativeRenderbuffer();
                renderbuffer.Bind();
                renderbuffer.Storage(TextureSize, TextureSize, RenderbufferInternalFormat.DepthComponent16);
                this._renderbuffers[i] = renderbuffer;

                NativeFramebuffer framebuffer = new NativeFramebuffer();
                framebuffer.Bind();
                framebuffer.AttachTexture(FramebufferAttachment.Color, this._textures[i]);
                framebuffer.AttachRenderbuffer(FramebufferAttachment.Depth, this._renderbuffers[i]);
                Debug.Assert(framebuffer.CompletenessStatus == FramebufferStatus.Complete);

                this._framebuffers[i] = framebuffer;
            }

            // Uniforms
            new Uniform(this._program, "LightPos").SetValue(4.0f, 4.0f, -8.0f);

            this._uniTexUnit = new Uniform(this._program, "TexUnit");
            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
            this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");

            GL.ClearColor(1, 1, 1, 0);
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
            int frontIndex = this._currentTextureIndex;
            int backIndex = 1 - this._currentTextureIndex;
            this._currentTextureIndex = backIndex;

            // Use the program
            this._program.Use();

            // Render into texture
            this._uniTexUnit.SetValue(frontIndex);

            // Set the matrix for camera orbiting the origin
            Matrix4 cameraMatrix = Utils.OrbitCameraMatrix(
                Vector3.Zero, 3.0f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * 35),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * GameTime.TotalSec / 10) * 60));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Set model matrix
            Matrix4 modelMatrix = Matrix4.CreateRotationX((float)(Math.PI * GameTime.TotalSec * 0.5));
            this._uniModelMatrix.SetValue(ref modelMatrix);

            // Set projection matrix
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(40), 1, 1, 40);
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);

            // Render to framebuffer
            this._framebuffers[backIndex].Bind();
            GL.Viewport(TextureSize, TextureSize);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            this._cube.DrawWithBoundIndexBuffer();

            // Render textured cube to default framebuffer
            this._defaultFramebuffer.Bind();
            GL.Viewport(Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            var tempTotalTime = GameTime.TotalSec;
            tempTotalTime *= 0.3f;
            // Set the matrix for camera orbiting the origin
            cameraMatrix = Utils.OrbitCameraMatrix(
                Vector3.Zero, 3.0f,
                (float)MathFR.DegreesToRadians(tempTotalTime * 35),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * tempTotalTime / 10) * 60));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Set projection matrix
            projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(60), (float)Width / Height, 1, 40);
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);

            // Render
            this._cube.DrawWithBoundIndexBuffer();
        }

        public override void Unload()
        {
            for (int i = 0; i < 2; i++)
            {
                this._framebuffers[i].Dispose();
                this._renderbuffers[i].Dispose();
                this._textures[i].Dispose();
            }
            this._indices.Dispose();
            this._texCoords.Dispose();
            this._normals.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
