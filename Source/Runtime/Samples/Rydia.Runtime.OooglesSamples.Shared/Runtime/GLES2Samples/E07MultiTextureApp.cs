using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.IO;
using Rydia.Resources;
using Rydia.Runtime.ES30;
using Rydia.Serialization;
using SampleEngine.Drawing2D;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Rydia.Runtime.GLES2Samples
{

    public class E07MultiTextureApp : GLES30App
    {

        private NativeShaderProgram? _program;
        private VertexAttribute _attrPosition;
        private VertexAttribute _attrTexCoord;
        private Uniform _uniBaseMap;
        private Uniform _uniLightMap;
        private NativeTexture? _texBaseMap;
        private NativeTexture? _texLightMap;

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        private struct Vertex
        {
            public Vector3 pos;
            public Vector2 texCoord;

            public Vertex(Vector3 pos, Vector2 texCoord)
            {
                this.pos = pos;
                this.texCoord = texCoord;
            }
        }

        private readonly Vertex[] _vertices =
        {
            new Vertex(new Vector3(-0.5f,  0.5f, 0), new Vector2(0, 0)),
            new Vertex(new Vector3(-0.5f, -0.5f, 0), new Vector2(0, 1)),
            new Vertex(new Vector3( 0.5f, -0.5f, 0), new Vector2(1, 1)),
            new Vertex(new Vector3( 0.5f,  0.5f, 0), new Vector2(1, 0))
        };

        private readonly ushort[] _indices =
        {
            0, 1, 2,
            0, 2, 3
        };

        public override void Load()
        {

            // Compile vertex and fragment shaders
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                attribute vec4 a_position;
                attribute vec2 a_texCoord;

                varying vec2 v_texCoord;

                void main()
                {
                  gl_Position = a_position;
                  v_texCoord = a_texCoord;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec2 v_texCoord;

                uniform sampler2D s_baseMap;
                uniform sampler2D s_lightMap;

                void main()
                {
                  vec4 baseColor;
                  vec4 lightColor;

                  baseColor = texture2D(s_baseMap, v_texCoord);
                  lightColor = texture2D(s_lightMap, v_texCoord);

                  gl_FragColor = baseColor * (lightColor + 0.25);
                }");
            fragmentShader.Compile();

            // Link shaders into program
            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            // We don't need the shaders anymore. 
            // Note that the shaders won't actually be deleted until the program is deleted.
            vertexShader.Dispose();
            fragmentShader.Dispose();

            // Initialize vertex attributes
            this._attrPosition = new VertexAttribute(this._program, "a_position");
            this._attrTexCoord = new VertexAttribute(this._program, "a_texCoord");

            // Initialize uniforms
            this._uniBaseMap = new Uniform(this._program, "s_baseMap");
            this._uniLightMap = new Uniform(this._program, "s_lightMap");

            // Initialize the asset manager
            // Load the textures
            var embedded = new EmbeddedResources<E07MultiTextureApp>();

            var str = embedded.LoadText("Assets.basemap.PixelData.ryd");
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<PixelData>(temp);
            this._texBaseMap = resource.ToTexture();

            str = embedded.LoadText("Assets.lightmap.PixelData.ryd");
            temp = Convert.FromBase64String(str);
            resource = new BinarySerializer().Deserialize<PixelData>(temp);
            this._texLightMap = Texture2D.LoadEmbedded<E07MultiTextureApp>("Assets.lightmap.PixelData.ryd").Texture;

            // Set clear color to black
            GL.ClearColor(ColorRgba.DarkBlue);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            // Clear the color buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Use the program
            this._program.Use();

            // Set the data for the vertex attributes
            unsafe
            {
                fixed (float* pos = &this._vertices[0].pos.X)
                {
                    this._attrPosition.SetData(VertexAttribPointerType.Float, 3, pos, sizeof(Vertex));
                }
                fixed (float* texCoord = &this._vertices[0].texCoord.X)
                {
                    this._attrTexCoord.SetData(VertexAttribPointerType.Float, 2, texCoord, sizeof(Vertex));
                }
            }
            this._attrPosition.Enable();
            this._attrTexCoord.Enable();

            // Bind the base map
            this._texBaseMap.BindToTextureUnit(0);

            // Set the base map sampler to texture unit to 0
            this._uniBaseMap.SetValue(0);

            // Bind the light map
            this._texLightMap.BindToTextureUnit(1);

            // Set the light map sampler to texture unit to 1
            this._uniLightMap.SetValue(1);

            // Draw the quad
            GL.DrawElements(PrimitiveType.Triangles, this._indices);
        }

        public override void Unload()
        {
            // Release resources
            this._texBaseMap.Dispose();
            this._texLightMap.Dispose();
            this._program.Dispose();
        }
    }

}
