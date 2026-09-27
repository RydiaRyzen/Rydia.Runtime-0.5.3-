using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E13CartoonTorusApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _normals;
        private NativeGraphicsBuffer _indices;
        private TorusGeometry _torus;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;

                attribute vec3 Position;
                attribute vec3 Normal;

                varying vec3 vertNormal;

                void main(void)
                {
                  vertNormal = mat3(ModelMatrix) * Normal;
                  gl_Position = 
                    ProjectionMatrix *
                    CameraMatrix *
                    ModelMatrix *
                    vec4(Position, 1.0);
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec3 vertNormal;

                uniform vec3 LightPos;

                void main(void)
                {
                  float intensity = 2.0 * max(
                    dot(vertNormal,  LightPos) /
                    length(LightPos),
                    0.0);
                  if (!gl_FrontFacing)
                  {
                    gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
                  }
                  else if (intensity > 0.9)
                  {
                    gl_FragColor = vec4(1.0, 0.9, 0.8, 1.0);
                  }
                  else if (intensity > 0.1)
                  {
                    gl_FragColor = vec4(0.7, 0.6, 0.4, 1.0);
                  }
                  else
                  {
                    gl_FragColor = vec4(0.3, 0.2, 0.1, 1.0);
                  }
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

            // Indices
            this._indices = new NativeGraphicsBuffer(BufferTarget.ElementArrayBuffer);
            this._indices.Bind();
            this._indices.Data(this._torus.Indices);

            // Don't need data anymore
            this._torus.Clear();

            // Uniforms
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
                Vector3.Zero, 3.5f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * 35),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * GameTime.TotalSec / 10) * 60));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Update and render the torus
            Matrix4 rotationY = Matrix4.CreateRotationY((float)(GameTime.TotalSec * Math.PI * 0.5));
            Matrix4 rotationX = Matrix4.CreateRotationX((float)(Math.PI * 0.5));
            Matrix4 modelMatrix = rotationX * rotationY;
            this._uniModelMatrix.SetValue(ref modelMatrix);
            this._torus.DrawWithBoundIndexBuffer();
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(70.0f), (float)newWidth / newHeight, 1, 30);
            this._program.Use();
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._indices.Dispose();
            this._normals.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
