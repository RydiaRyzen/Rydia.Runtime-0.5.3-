using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E12StripedCubesApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _texCoords;
        private NativeGraphicsBuffer _indices;
        private CubeGeometry _cube;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;

                attribute vec3 Position;
                attribute vec2 TexCoord;

                varying vec2 vertTexCoord;

                void main(void)
                {
                  vertTexCoord = TexCoord;
                  gl_Position = 
                    ProjectionMatrix *
                    CameraMatrix *
                    ModelMatrix *
                    vec4(Position, 1.0);
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec2 vertTexCoord;

                void main(void)
                {
                  float i = floor(mod((vertTexCoord.x + vertTexCoord.y) * 8.0, 2.0));
                  gl_FragColor = mix(
                    vec4(0, 0, 0, 1),
                    vec4(1, 1, 0, 1),
                    i
                  );
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            this._cube = new CubeGeometry(0.5f);

            // Positions
            this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._verts.Bind();
            this._verts.Data(this._cube.Positions);

            VertexAttribute attr = new VertexAttribute(this._program, "Position");
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

            // Uniforms
            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
            this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");

            GL.ClearColor(0.8f, 0.8f, 0.7f, 0);
            GL.ClearDepth(1);
            GL.Enable(EnableCap.DepthTest);
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

            // Orbit camera around cubes
            Matrix4 cameraMatrix = Utils.OrbitCameraMatrix(
                Vector3.Zero, 3.5f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * 15),
                (float)MathFR.DegreesToRadians(MathFR.Sin(GameTime.TotalSec) * 45));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Update and render first cube
            Matrix4 translation = Matrix4.CreateTranslation(-1, 0, 0);
            Matrix4 rotation = Matrix4.CreateRotationZ((float)MathFR.DegreesToRadians(GameTime.TotalSec * 180));
            Matrix4 modelMatrix = rotation * translation;
            this._uniModelMatrix.SetValue(ref modelMatrix);
            this._cube.DrawWithBoundIndexBuffer();

            // Update and render second cube
            translation = Matrix4.CreateTranslation(1, 0, 0);
            rotation = Matrix4.CreateRotationY((float)MathFR.DegreesToRadians(GameTime.TotalSec * 90));
            modelMatrix = rotation * translation;
            this._uniModelMatrix.SetValue(ref modelMatrix);
            this._cube.DrawWithBoundIndexBuffer();
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(60.0f), (float)newWidth / newHeight, 1, 30);
            this._program.Use();
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._indices.Dispose();
            this._texCoords.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
