using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;

namespace Rydia.Runtime.GLES2Samples
{

    internal class E03SimpleTexture2DApp : GLES30App
    {

        private NativeShaderProgram _program;
        private VertexAttribute _attrPosition;
        private VertexAttribute _attrTexCoord;
        private Uniform _uniSampler;
        private NativeTexture _texture;

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

            // Initialize uniform
            this._uniSampler = new Uniform(this._program, "s_texture");

            // Load the texture
            this._texture = CreateSimpleTexture2D();

            // Set clear color to black
            GL.ClearColor(ColorRgba.DarkBlue);
        }

        public static NativeTexture CreateSimpleTexture2D()
        {
            const int Width = 2;
            const int Height = 2;
            byte[] Pixels = {
                255,   0,   0,  // Red
                  0, 255,   0,  // Green
                  0,   0, 255,  // Blue
                255, 255,   0}; // Yellow

            // Use tightly packed data
            GL.PixelStore(PixelStoreParameter.UnpackAlignment, PixelStoreValue.One);

            // Generate a texture object
            NativeTexture texture = new NativeTexture();

            // Bind the texture object
            texture.Bind();

            // Load the texture: 2x2 Image, 3 bytes per pixel (R, G, B)
            texture.TexImage2D(PixelFormat.Rgb, Width, Height, Pixels);

            // Set the filtering mode
            texture.MinificationFilter = TextureMinFilter.Nearest;
            texture.MagnificationFilter = TextureMagFilter.Nearest;

            return texture;
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

            // Bind the texture
            this._texture.BindToTextureUnit(0);

            // Set the texture sampler to texture unit to 0
            this._uniSampler.SetValue(0);

            // Draw the quad
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
