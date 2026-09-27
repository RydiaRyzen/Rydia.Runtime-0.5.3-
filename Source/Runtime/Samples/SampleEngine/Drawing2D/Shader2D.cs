using System;
using System.Collections.Generic;
using System.Text;
using Rydia;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;

namespace SampleEngine.Drawing2D
{

    public sealed class Shader2D : DisposableBase
    {

        private const string VertexShaderSource = @"
attribute vec2 aPosition;
attribute vec2 aTexCoord;

uniform mat4 uProjection;

varying vec2 vTexCoord;

void main()
{
    gl_Position = uProjection *
                  vec4(aPosition, 0.0, 1.0);

    vTexCoord = aTexCoord;
}";

        private const string FragmentShaderSource = @"
precision mediump float;

uniform sampler2D uTexture;

varying vec2 vTexCoord;

void main()
{
    gl_FragColor =
        texture2D(uTexture, vTexCoord);
}";

        public NativeShaderProgram Program { get; private set; }

        public VertexAttribute PositionLocation { get; private set; }
        public VertexAttribute TexCoordLocation { get; private set; }

        public Uniform ProjectionLocation { get; private set; }
        public Uniform TextureLocation { get; private set; }

        public Shader2D()
        {
            var vertexShader = new NativeShader(ShaderType.VertexShader, VertexShaderSource);
            vertexShader.Compile();

            var fragmentShader = new NativeShader(ShaderType.FragmentShader, FragmentShaderSource);
            fragmentShader.Compile();

            Program = new NativeShaderProgram(vertexShader, fragmentShader);
            Program.Link();

            // We don't need the shaders anymore. 
            // Note that the shaders won't actually be deleted until the program is deleted.
            vertexShader.Dispose();
            fragmentShader.Dispose();

            // Initialize vertex attribute
            PositionLocation = new VertexAttribute(Program, "aPosition");
            TexCoordLocation = new VertexAttribute(Program, "aTexCoord");
            ProjectionLocation = new Uniform(Program, "uProjection");
            TextureLocation = new Uniform(Program, "uTexture");
        }

        public void Use()
        {
            Program.Use();
        }

        protected override void Disposing(bool disposing)
        {
            if (Program != null)
            {
                Program.Dispose();
                Program = null;
            }
        }

    }

}
