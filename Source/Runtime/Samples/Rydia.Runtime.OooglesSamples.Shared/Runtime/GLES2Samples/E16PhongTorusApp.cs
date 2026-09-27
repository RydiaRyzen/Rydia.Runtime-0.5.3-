using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E16PhongTorusApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _normals;
        private NativeGraphicsBuffer _colors;
        private TwistedTorusGeometry _torus;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix;

                attribute vec3 Position;
                attribute vec3 Normal;
                attribute vec3 Color;

                varying vec3 vertColor;
                varying vec3 vertNormal;
                varying vec3 vertViewDir;

                void main(void)
                {
                  vertColor = normalize(vec3(1.0, 1.0, 1.0) - Color);
                  vertNormal = Normal;
                  vertViewDir = (vec4(0.0, 0.0, 1.0, 1.0) * CameraMatrix).xyz;
                  gl_Position = ProjectionMatrix * CameraMatrix * vec4(Position, 1.0);
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec3 vertColor;
                varying vec3 vertNormal;
                varying vec3 vertViewDir;

                uniform vec3 LightPos[3];

                void main(void)
                {
                  float amb = 0.2;
                  float diff = 0.0;
                  float spec = 0.0;
                  for (int i=0; i != 3; ++i)
                  {
                    diff += max(
                      dot(vertNormal,  LightPos[i]) /
                      dot(LightPos[i], LightPos[i]), 
                      0.0);
                    float k = dot(vertNormal, LightPos[i]);
                    vec3 r = 2.0*k*vertNormal - LightPos[i];
                    spec += pow(max(
                      dot(normalize(r), vertViewDir),
                      0.0), 32.0 * dot(r, r));
                  }
                  gl_FragColor = 
                    vec4(vertColor, 1.0) * (amb + diff) +
                    vec4(1.0, 1.0, 1.0, 1.0) * spec;
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            this._torus = new TwistedTorusGeometry();

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

            // Colors
            this._colors = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._colors.Bind();
            this._colors.Data(this._torus.Tangents);

            attr = new VertexAttribute(this._program, "Color");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Don't need data anymore
            this._torus.Clear();

            // Uniforms
            new Uniform(this._program, "LightPos").SetValues(new Vector3[] {
                new Vector3(2, -1,  0),
                new Vector3(0,  3, -1),
                new Vector3(0, -1,  4)});

            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");

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
                Vector3.Zero, 5.0f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * MathFR.PI * 2),
                (float)MathFR.DegreesToRadians(MathFR.PI * GameTime.TotalSec / 8) * 90);
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Render the torus
            this._torus.Draw();
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
            this._colors.Dispose();
            this._normals.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
