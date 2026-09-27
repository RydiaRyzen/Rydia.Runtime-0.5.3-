using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    internal class E05TextureWrapApp : GLES30App
    {
        private NativeShaderProgram _program;
        private VertexAttribute _attrPosition;
        private VertexAttribute _attrTexCoord;
        private Uniform _uniSampler;
        private Uniform _uniOffset;
        private NativeTexture _texture;

        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        private struct Vertex
        {
            public Vector4 pos;
            public Vector2 texCoord;

            public Vertex(Vector4 pos, Vector2 texCoord)
            {
                this.pos = pos;
                this.texCoord = texCoord;
            }
        }

        private readonly Vertex[] _vertices =
        {
            new Vertex(new Vector4(-0.3f,  0.3f, 0, 1), new Vector2(-1, -1)),
            new Vertex(new Vector4(-0.3f, -0.3f, 0, 1), new Vector2(-1,  2)),
            new Vertex(new Vector4( 0.3f, -0.3f, 0, 1), new Vector2( 2,  2)),
            new Vertex(new Vector4( 0.3f,  0.3f, 0, 1), new Vector2( 2, -1))
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
                uniform float u_offset;

                attribute vec4 a_position;
                attribute vec2 a_texCoord;

                varying vec2 v_texCoord;

                void main()
                {
                  gl_Position = a_position;
                  gl_Position.x += u_offset;
                  v_texCoord = a_texCoord;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec2 v_texCoord;

                uniform sampler2D s_texture;

                void main()
                {
                  gl_FragColor = texture2D(s_texture, v_texCoord);
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
            this._uniSampler = new Uniform(this._program, "s_texture");
            this._uniOffset = new Uniform(this._program, "u_offset");

            // Load the texture
            this._texture = TextureUtils.CreateMipmappedTexture2D();

            // Set clear color to black
            GL.ClearColor(ColorRgba.DarkBlue);
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
                    this._attrPosition.SetData(VertexAttribPointerType.Float, 4, pos, sizeof(Vertex));
                }
                fixed (float* texCoord = &this._vertices[0].texCoord.X)
                {
                    this._attrTexCoord.SetData(VertexAttribPointerType.Float, 2, texCoord, sizeof(Vertex));
                }
            }
            this._attrPosition.Enable();
            this._attrTexCoord.Enable();

            // Bind the texture
            this._texture.BindToTextureUnit(0);

            // Set the texture sampler to texture unit to 0
            this._uniSampler.SetValue(0);

            // Draw quad with repeat wrap mode
            this._texture.WrapS = TextureWrapMode.Repeat;
            this._texture.WrapT = TextureWrapMode.Repeat;
            this._uniOffset.SetValue(-0.7f);
            GL.DrawElements(PrimitiveType.Triangles, this._indices);

            // Draw quad with clamp to edge wrap mode
            this._texture.WrapS = TextureWrapMode.ClampToEdge;
            this._texture.WrapT = TextureWrapMode.ClampToEdge;
            this._uniOffset.SetValue(0.0f);
            GL.DrawElements(PrimitiveType.Triangles, this._indices);

            // Draw quad with mirrored repeat wrap mode
            this._texture.WrapS = TextureWrapMode.MirroredRepeat;
            this._texture.WrapT = TextureWrapMode.MirroredRepeat;
            this._uniOffset.SetValue(0.7f);
            GL.DrawElements(PrimitiveType.Triangles, this._indices);
        }

        public override void Unload()
        {
            // Release resources
            this._texture.Dispose();
            this._program.Dispose();
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }
    }

}
