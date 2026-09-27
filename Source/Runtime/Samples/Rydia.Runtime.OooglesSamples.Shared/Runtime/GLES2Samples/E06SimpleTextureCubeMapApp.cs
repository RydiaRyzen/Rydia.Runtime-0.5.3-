using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E06SimpleTextureCubeMapApp : GLES30App
    {
        private NativeShaderProgram _program;
        private VertexAttribute _attrPosition;
        private VertexAttribute _attrNormal;
        private Uniform _uniSampler;
        private NativeTexture _texture;
        private SphereGeometry _sphere;

        public override void Load()
        {
            // Compile vertex and fragment shaders
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                attribute vec4 a_position;
                attribute vec3 a_normal;

                varying vec3 v_normal;

                void main()
                {
                  gl_Position = a_position;
                  v_normal = a_normal;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec3 v_normal;

                uniform samplerCube s_texture;

                void main()
                {
                  gl_FragColor = textureCube(s_texture, v_normal);
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
            this._attrNormal = new VertexAttribute(this._program, "a_normal");

            // Initialize uniforms
            this._uniSampler = new Uniform(this._program, "s_texture");

            // Load the texture
            this._texture = TextureUtils.CreateSimpleTextureCubeMap();

            // Generate the geometry data
            this._sphere = new SphereGeometry(128, 0.75f);

            // Set clear color to black
            GL.ClearColor(ColorRgba.DarkBlue);

            // Enable culling
            GL.CullFace(CullFaceMode.Back);
            GL.Enable(EnableCap.CullFace);
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
            this._attrPosition.SetData(this._sphere.Positions);
            this._attrPosition.Enable();
            this._attrNormal.SetData(this._sphere.Normals);
            this._attrNormal.Enable();

            // Bind the texture
            this._texture.BindToTextureUnit(0);

            // Set the texture sampler to texture unit to 0
            this._uniSampler.SetValue(0);

            // Draw the sphere
            this._sphere.DrawWithIndices();
        }

        public override void Unload()
        {
            // Release resources
            this._texture.Dispose();
            this._program.Dispose();
        }
    }

}
