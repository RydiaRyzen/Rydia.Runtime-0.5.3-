using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.IO;
using Rydia.Resources;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;
using Rydia.Serialization;

namespace Rydia.Runtime.GLES2Samples
{

    public class E18CubeMappingApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _normals;
        private NativeTexture _texture;
        private SpiralSphereGeometry _shape;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;

        public override void Load()
        {
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;

                attribute vec3 Position;
                attribute vec3 Normal;
                attribute vec2 TexCoord;

                varying vec3 vertNormal;
                varying vec3 vertLightDir;
                varying vec3 vertLightRefl;
                varying vec3 vertViewDir;
                varying vec3 vertViewRefl;

                uniform vec3 LightPos;

                void main(void)
                {
                  gl_Position = ModelMatrix * vec4(Position, 1.0);
                  vertNormal = mat3(ModelMatrix) * Normal;
                  vertLightDir = LightPos - gl_Position.xyz;

                  vertLightRefl = reflect(
                    -normalize(vertLightDir),
                    normalize(vertNormal));

                  vertViewDir = (
                    vec4(0.0, 0.0, 1.0, 1.0)*
                    CameraMatrix).xyz;

                  vertViewRefl = reflect(
                    normalize(vertViewDir),
                    normalize(vertNormal));

                  gl_Position = ProjectionMatrix * CameraMatrix * gl_Position;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                uniform samplerCube TexUnit;

                varying vec3 vertNormal;
                varying vec3 vertLightDir;
                varying vec3 vertLightRefl;
                varying vec3 vertViewDir;
                varying vec3 vertViewRefl;

                void main(void)
                {
                  float l = length(vertLightDir);

                  float d = dot(
                    normalize(vertNormal), 
                    normalize(vertLightDir)) / l;

                  float s = dot(
                    normalize(vertLightRefl),
                    normalize(vertViewDir));

                  vec3 lt = vec3(1.0, 1.0, 1.0);
                  vec3 env = textureCube(TexUnit, vertViewRefl).rgb;

                  gl_FragColor = vec4(
                    env * 0.4 + 
                    (lt + env) * 1.5 * max(d, 0.0) + 
                    lt * pow(max(s, 0.0), 64.0), 
                    1.0);
                }");
            fragmentShader.Compile();

            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            vertexShader.Dispose();
            fragmentShader.Dispose();

            this._program.Use();

            this._shape = new SpiralSphereGeometry();

            // Positions
            this._verts = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._verts.Bind();
            this._verts.Data(this._shape.Positions);

            VertexAttribute attr = new VertexAttribute(this._program, "Position");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Normals
            this._normals = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._normals.Bind();
            this._normals.Data(this._shape.Normals);

            attr = new VertexAttribute(this._program, "Normal");
            attr.SetConfig<Vector3>();
            attr.Enable();

            // Don't need data anymore
            this._shape.Clear();

            // Texture

            // Load the textures
            /*
            resource.Name = "Newton";
            resource.Serialize();
            */
            var embedded = new EmbeddedResources<E07MultiTextureApp>();

            var str = embedded.LoadText("Assets.Newton.PixelData.ryd");
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<PixelData>(temp);

            this._texture = new NativeTexture(TextureTargetType.CubeMap);
            this._texture.Bind();
            this._texture.MinificationFilter = TextureMinFilter.Linear;
            this._texture.MagnificationFilter = TextureMagFilter.Linear;
            this._texture.WrapS = TextureWrapMode.ClampToEdge;
            this._texture.WrapT = TextureWrapMode.ClampToEdge;
            for (int i = 0; i < 6; i++)
            {
                this._texture.TexImage2D(PixelFormat.Rgba,
                    resource.Width, resource.Height,
                    resource.Data, 0, PixelDataType.UnsignedByte, i);
            }

            // Uniforms
            new Uniform(this._program, "TexUnit").SetValue(0);
            new Uniform(this._program, "LightPos").SetValue(3.0f, 5.0f, 4.0f);

            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
            this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");

            GL.ClearColor(0.2f, 0.05f, 0.1f, 0);
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
                Vector3.Zero,
                (float)(4.5 - Math.Sin(GameTime.TotalSec * Math.PI / 8) * 2),
                (float)(GameTime.TotalSec * Math.PI / 6),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * GameTime.TotalSec / 15) * 90));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Update and render the sphere
            Matrix4 modelMatrix = Matrix4.CreateFromAxisAngle(new Vector3(1, 1, 1), (float)(GameTime.TotalSec * Math.PI / 5));
            this._uniModelMatrix.SetValue(ref modelMatrix);

            this._shape.Draw();
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(60.0f), (float)newWidth / newHeight, 1, 100);
            this._program.Use();
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._texture.Dispose();
            this._normals.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
        }
    }

}
