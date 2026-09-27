using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E19ShadedObjectsApp : GLES30App
    {
        private abstract class Shape : IDisposable
        {
            private NativeShaderProgram _program;
            private NativeGraphicsBuffer _verts;
            private NativeGraphicsBuffer _normals;
            private NativeGraphicsBuffer _texCoords;
            private NativeGraphicsBuffer _indices;
            private VertexAttribute _attrVerts;
            private VertexAttribute _attrNormals;
            private VertexAttribute _attrTexCoords;
            private Uniform _uniProjectionMatrix;
            private Uniform _uniCameraMatrix;
            private Uniform _uniModelMatrix;
            private Uniform _uniLightPos;

            public Shape(NativeShader vertexShader, NativeShader fragmentShader)
            {
                this._program = new NativeShaderProgram(vertexShader, fragmentShader);
                this._program.Link();
                this._program.Use();

                // Fragment shader no longer needed. Vertex shader is shared though.
                fragmentShader.Dispose();
            }

            public void Init(Vector3[] verts, Vector3[] normals, Vector2[] texCoords, ushort[] indices)
            {
                // Positions
                this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
                this._verts.Bind();
                this._verts.Data(verts);

                this._attrVerts = new VertexAttribute(this._program, "Position");

                // Normals
                this._normals = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
                this._normals.Bind();
                this._normals.Data(normals);

                this._attrNormals = new VertexAttribute(this._program, "Normal");

                // Texture coordinates
                this._texCoords = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
                this._texCoords.Bind();
                this._texCoords.Data(texCoords);

                this._attrTexCoords = new VertexAttribute(this._program, "TexCoord");

                // Indices
                this._indices = new NativeGraphicsBuffer(BufferTarget.ElementArrayBuffer);
                this._indices.Bind();
                this._indices.Data(indices);

                // Uniforms
                this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
                this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
                this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");
                this._uniLightPos = new Uniform(this._program, "LightPos");
            }

            public void SetProjection(ref Matrix4 projection)
            {
                this._program.Use();
                this._uniProjectionMatrix.SetValue(ref projection);
            }

            public virtual void Render(ref Vector3 light, ref Matrix4 camera, ref Matrix4 model)
            {
                this._program.Use();

                this._uniLightPos.SetValue(ref light);
                this._uniCameraMatrix.SetValue(ref camera);
                this._uniModelMatrix.SetValue(ref model);

                this._verts.Bind();
                this._attrVerts.SetConfig(VertexAttribPointerType.Float, 3);
                this._attrVerts.Enable();

                this._normals.Bind();
                this._attrNormals.SetConfig(VertexAttribPointerType.Float, 3);
                this._attrNormals.Enable();

                this._texCoords.Bind();
                this._attrTexCoords.SetConfig(VertexAttribPointerType.Float, 2);
                this._attrTexCoords.Enable();

                this._indices.Bind();
            }

            public void Dispose()
            {
                this._program.Dispose();
                this._verts.Dispose();
                this._normals.Dispose();
                this._texCoords.Dispose();
                this._indices.Dispose();
            }
        }

        private class Sphere : Shape
        {
            private SphereGeometry _sphere;

            public Sphere(NativeShader vertexShader, NativeShader fragmentShader) : base(vertexShader, fragmentShader)
            {
                this._sphere = new SphereGeometry();
                Init(this._sphere.Positions, this._sphere.Normals, this._sphere.TexCoords, this._sphere.Indices);
                this._sphere.Clear();
            }

            public override void Render(ref Vector3 light, ref Matrix4 camera, ref Matrix4 model)
            {
                base.Render(ref light, ref camera, ref model);
                this._sphere.DrawWithBoundIndexBuffer();
            }
        }

        private class Cube : Shape
        {
            private CubeGeometry _cube;

            public Cube(NativeShader vertexShader, NativeShader fragmentShader) : base(vertexShader, fragmentShader)
            {
                this._cube = new CubeGeometry();
                Init(this._cube.Positions, this._cube.Normals, this._cube.TexCoords, this._cube.Indices);
                this._cube.Clear();
            }

            public override void Render(ref Vector3 light, ref Matrix4 camera, ref Matrix4 model)
            {
                base.Render(ref light, ref camera, ref model);
                this._cube.DrawWithBoundIndexBuffer();
            }
        }

        private class Torus : Shape
        {
            private TorusGeometry _torus;

            public Torus(NativeShader vertexShader, NativeShader fragmentShader) : base(vertexShader, fragmentShader)
            {
                this._torus = new TorusGeometry();
                Init(this._torus.Positions, this._torus.Normals, this._torus.TexCoords, this._torus.Indices);
                this._torus.Clear();
            }

            public override void Render(ref Vector3 light, ref Matrix4 camera, ref Matrix4 model)
            {
                base.Render(ref light, ref camera, ref model);
                this._torus.DrawWithBoundIndexBuffer();
            }
        }

        private Sphere _sphere;
        private Cube _cubeX;
        private Cube _cubeY;
        private Cube _cubeZ;
        private Torus _torus;

        public override void Load()
        {

            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;

                attribute vec3 Position;
                attribute vec3 Normal;
                attribute vec2 TexCoord;

                varying vec2 vertTexCoord;
                varying vec3 vertNormal;
                varying vec3 vertLight;

                uniform vec3 LightPos;

                void main(void)
                {
                  vertTexCoord = TexCoord;
                  gl_Position = ModelMatrix * vec4(Position, 1.0);
                  vertNormal = mat3(ModelMatrix) * Normal;
                  vertLight = LightPos - gl_Position.xyz;
                  gl_Position = ProjectionMatrix * CameraMatrix * gl_Position;
                }");
            vertexShader.Compile();

            this._sphere = new Sphere(vertexShader, CreateFragmentShader(@"
                  float m = floor(mod((vertTexCoord.x + vertTexCoord.y) * 16.0, 2.0));
                  vec3 Color = mix(
                    vec3(0.0, 0.0, 0.0),
                    vec3(1.0, 1.0, 0.0),
                    m);
                "));

            this._cubeX = new Cube(vertexShader, CreateFragmentShader(@"
                  float c = floor(mod(
                    1.0 +
                    floor(mod(vertTexCoord.x * 8.0, 2.0)) +
                    floor(mod(vertTexCoord.y * 8.0, 2.0)), 
                    2.0));
                  vec3 Color = vec3(c, c, c);   
                "));

            this._cubeY = new Cube(vertexShader, CreateFragmentShader(@"
                  vec2 center = vertTexCoord - vec2(0.5, 0.5);
                  float m = floor(mod(sqrt(length(center)) * 16.0, 2.0));
                  vec3 Color = mix(
                    vec3(1.0, 0.0, 0.0),
                    vec3(0.0, 0.0, 1.0),
                    m);
                "));

            this._cubeZ = new Cube(vertexShader, CreateFragmentShader(@"
                  vec2  center = (vertTexCoord - vec2(0.5, 0.5)) * 16.0;
                  float l = length(center);
                  float t = atan(center.y, center.x) / (2.0 * asin(1.0));
                  float m = floor(mod(l + t, 2.0));
                  vec3 Color = mix(
                    vec3(0.0, 1.0, 0.0),
                    vec3(1.0, 1.0, 1.0),
                    m);
                "));

            this._torus = new Torus(vertexShader, CreateFragmentShader(@"
                  float m = floor(mod(vertTexCoord.x * 8.0, 2.0));
                  vec3 Color = mix(
                    vec3(1.0, 0.6, 0.1),
                    vec3(1.0, 0.9, 0.8),
                    m);
                "));

            // Vertex shader no longer needed
            vertexShader.Dispose();

            GL.ClearColor(0.5f, 0.5f, 0.5f, 0);
            GL.ClearDepth(1);
            GL.Enable(EnableCap.DepthTest);
        }

        private NativeShader CreateFragmentShader(string source)
        {
            NativeShader shader = new NativeShader(ShaderType.FragmentShader);
            shader.Source = @"
                precision mediump float;

                varying vec2 vertTexCoord;
                varying vec3 vertNormal;
                varying vec3 vertLight;

                void main(void)
                {
                  float Len = dot(vertLight, vertLight);
                  float Dot = Len > 0.0 ? dot(
                    vertNormal, 
                    normalize(vertLight)
                  ) / Len : 0.0;
                  float Intensity = 0.2 + max(Dot, 0.0) * 4.0;
                " + source + @"
                  gl_FragColor = vec4(Color * Intensity, 1.0);
                }";

            shader.Compile();
            return shader;
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            // Clear the color and depth buffer
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Vector3 light = new Vector3(2, 2, 2);

            // Set the matrix for camera orbiting the origin
            Matrix4 camera = Utils.OrbitCameraMatrix(
                Vector3.Zero, 6.0f,
                (float)MathFR.DegreesToRadians(GameTime.TotalSec * 15),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * GameTime.TotalSec / 3) * 45));

            // Render the shapes
            Matrix4 model = Matrix4.Identity;
            this._sphere.Render(ref light, ref camera, ref model);

            model = Matrix4.CreateTranslation(2, 0, 0);
            Matrix4 rotate = Matrix4.CreateRotationX((float)MathFR.DegreesToRadians(GameTime.TotalSec * 45));
            model = rotate * model;
            this._cubeX.Render(ref light, ref camera, ref model);

            model = Matrix4.CreateTranslation(0, 2, 0);
            rotate = Matrix4.CreateRotationY((float)MathFR.DegreesToRadians(GameTime.TotalSec * 90));
            model = rotate * model;
            this._cubeY.Render(ref light, ref camera, ref model);

            model = Matrix4.CreateTranslation(0, 0, 2);
            rotate = Matrix4.CreateRotationZ((float)MathFR.DegreesToRadians(GameTime.TotalSec * 135));
            model = rotate * model;
            this._cubeZ.Render(ref light, ref camera, ref model);

            model = Matrix4.CreateTranslation(-1, -1, -1);
            rotate = Matrix4.CreateFromAxisAngle(new Vector3(1, 1, 1), (float)MathFR.DegreesToRadians(GameTime.TotalSec * 45));
            Matrix4 m1 = Matrix4.CreateRotationY((float)MathFR.DegreesToRadians(45));
            Matrix4 m2 = Matrix4.CreateRotationX((float)MathFR.DegreesToRadians(45));
            model = m2 * m1 * rotate * model;
            this._torus.Render(ref light, ref camera, ref model);
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(60), (float)newWidth / newHeight, 1, 50);
            this._sphere.SetProjection(ref projectionMatrix);
            this._cubeX.SetProjection(ref projectionMatrix);
            this._cubeY.SetProjection(ref projectionMatrix);
            this._cubeZ.SetProjection(ref projectionMatrix);
            this._torus.SetProjection(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._sphere.Dispose();
            this._cubeX.Dispose();
            this._cubeY.Dispose();
            this._cubeZ.Dispose();
            this._torus.Dispose();
        }
    }

}
