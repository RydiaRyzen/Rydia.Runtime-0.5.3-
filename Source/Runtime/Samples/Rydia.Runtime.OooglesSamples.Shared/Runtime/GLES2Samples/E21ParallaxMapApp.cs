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
    public class E21ParallaxMapApp : GLES30App
    {
        private NativeShaderProgram _program;
        private NativeGraphicsBuffer _verts;
        private NativeGraphicsBuffer _normals;
        private NativeGraphicsBuffer _tangents;
        private NativeGraphicsBuffer _texCoords;
        private NativeGraphicsBuffer _indices;
        private NativeTexture _texture;
        private Uniform _uniProjectionMatrix;
        private Uniform _uniCameraMatrix;
        private Uniform _uniModelMatrix;
        private Uniform _uniLightPos;
        private CubeGeometry _cube;

        public override void Load()
        {

            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 ProjectionMatrix, CameraMatrix, ModelMatrix;
                uniform vec3 LightPos;

                attribute vec3 Position;
                attribute vec3 Normal;
                attribute vec3 Tangent;
                attribute vec2 TexCoord;

                varying vec3 vertEye;
                varying vec3 vertLight;
                varying vec3 vertNormal;
                varying vec2 vertTexCoord;
                varying vec3 vertViewTangent;
                varying mat3 NormalMatrix;

                void main(void)
                {
                  vec4 EyePos = 
                    CameraMatrix *
                    ModelMatrix *
                    vec4(Position, 1.0);

                  vertEye = EyePos.xyz;

                  vec3 fragTangent = (
                    CameraMatrix *
                    ModelMatrix *
                    vec4(Tangent, 0.0)).xyz;

                  vertNormal = (
                    CameraMatrix *
                    ModelMatrix *
                    vec4(Normal, 0.0)).xyz;

                  vertLight = (
                    CameraMatrix *
                    vec4(LightPos - vertEye, 1.0)).xyz;

                  NormalMatrix = mat3(
                    fragTangent,
                    cross(vertNormal, fragTangent),
                    vertNormal);

                  vertViewTangent = vec3(
                    dot(NormalMatrix[0], vertEye),
                    dot(NormalMatrix[1], vertEye),
                    dot(NormalMatrix[2], vertEye));

                  vertTexCoord = TexCoord;

                  gl_Position = ProjectionMatrix * EyePos;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                uniform sampler2D BumpTex;
                uniform int BumpTexWidth;
                uniform int BumpTexHeight;

                float DepthMult = 0.1;

                varying vec3 vertEye;
                varying vec3 vertLight;
                varying vec3 vertNormal;
                varying vec2 vertTexCoord;
                varying vec3 vertViewTangent;
                varying mat3 NormalMatrix;

                void main(void)
                {
                  vec3 ViewTangent = normalize(vertViewTangent);
                  float perp = -dot(normalize(vertEye), vertNormal);

                  float sampleInterval = 1.0 / length(
                    vec2(BumpTexWidth, BumpTexHeight));

                  vec3 sampleStep = ViewTangent * sampleInterval;
                  float prevD = 0.0;
                  float depth = texture2D(BumpTex, vertTexCoord).w;
                  float maxOffs = min((depth * DepthMult) / -ViewTangent.z, 1.0);

                  vec3 viewOffs = vec3(0.0, 0.0, 0.0);
                  vec2 offsTexC = vertTexCoord + viewOffs.xy;

                  while (length(viewOffs) < maxOffs)
                  {
                    if ((offsTexC.x <= 0.0) || (offsTexC.x >= 1.0))
                      break;

                    if ((offsTexC.y <= 0.0) || (offsTexC.y >= 1.0))
                      break;

                    if ((depth * DepthMult * perp) <= -viewOffs.z)
                      break;

                    viewOffs += sampleStep;
                    offsTexC = vertTexCoord + viewOffs.xy;
                    prevD = depth;
                    depth = texture2D(BumpTex, offsTexC).w;
                  }

                  offsTexC = vec2(
                    clamp(offsTexC.x, 0.0, 1.0),
                    clamp(offsTexC.y, 0.0, 1.0));

                  float b = floor(mod(
                    1.0 +
                    floor(mod(offsTexC.x * 16.0, 2.0))+
                    floor(mod(offsTexC.y * 16.0, 2.0)), 2.0));

                  vec3 c = vec3(b, b, b);
                  vec3 n = texture2D(BumpTex, offsTexC).xyz;
                  vec3 finalNormal = NormalMatrix * n;
                  float l = length(vertLight);

                  float d = (l > 0.0) ? dot(
                    normalize(vertLight), 
                    finalNormal) / l : 0.0;

                  float i = 0.1 + 2.5 * max(d, 0.0);
                  gl_FragColor = vec4(c * i, 1.0);
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

            // Tangents
            this._tangents = new NativeGraphicsBuffer(BufferTarget.ArrayBuffer);
            this._tangents.Bind();
            this._tangents.Data(this._cube.Tangents);

            attr = new VertexAttribute(this._program, "Tangent");
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

            // Load the textures
            /*
            resource.Name = "BumpTex";
            resource.Serialize();
            */
            var embedded = new EmbeddedResources<E07MultiTextureApp>();

            var str = embedded.LoadText("Assets.texture.PixelData.ryd");
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<PixelData>(temp);

            this._texture = resource.ToTexture();
            this._texture.WrapS = TextureWrapMode.Repeat;
            this._texture.WrapT = TextureWrapMode.Repeat;

            // Uniforms
            new Uniform(this._program, "BumpTexWidth").SetValue(512);
            new Uniform(this._program, "BumpTexHeight").SetValue(512);
            new Uniform(this._program, "BumpTex").SetValue(0);

            this._uniProjectionMatrix = new Uniform(this._program, "ProjectionMatrix");
            this._uniCameraMatrix = new Uniform(this._program, "CameraMatrix");
            this._uniModelMatrix = new Uniform(this._program, "ModelMatrix");
            this._uniLightPos = new Uniform(this._program, "LightPos");

            GL.ClearColor(0.1f, 0.1f, 0.1f, 0);
            GL.ClearDepth(1);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.FrontFace(FrontFaceDirection.Ccw);
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

            // Set light position
            double lightAzimuth = -Math.PI * GameTime.TotalSec;
            float s = (float)Math.Sin(lightAzimuth);
            float c = (float)Math.Cos(lightAzimuth);
            this._uniLightPos.SetValue(-c * 2, 2, -s * 2);

            // Set the matrix for camera orbiting the origin
            Matrix4 cameraMatrix = Utils.OrbitCameraMatrix(
                Vector3.Zero, 3.0f,
                MathFR.DegreesToRadians(-45.0f),
                (float)MathFR.DegreesToRadians(MathFR.Sin(MathFR.PI * GameTime.TotalSec / 15) * 70));
            this._uniCameraMatrix.SetValue(ref cameraMatrix);

            // Update and render the cube
            Matrix4 modelMatrix = Matrix4.CreateFromAxisAngle(new Vector3(1, 1, 1), (float)(-Math.PI * GameTime.TotalSec * 0.025));
            this._uniModelMatrix.SetValue(ref modelMatrix);
            GL.CullFace(CullFaceMode.Back);
            this._cube.DrawWithBoundIndexBuffer();
        }

        public override void Resize(int newWidth, int newHeight)
        {
            base.Resize(newWidth, newHeight);
            Matrix4 projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(54), (float)newWidth / newHeight, 1, 10);
            this._program.Use();
            this._uniProjectionMatrix.SetValue(ref projectionMatrix);
        }

        public override void Unload()
        {
            this._indices.Dispose();
            this._texCoords.Dispose();
            this._tangents.Dispose();
            this._normals.Dispose();
            this._verts.Dispose();
            this._program.Dispose();
            this._texture.Dispose();
        }
    }
}
