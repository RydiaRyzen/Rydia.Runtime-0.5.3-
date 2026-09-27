using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;
using Rydia.Runtime.GLES2Samples.Shared;

namespace Rydia.Runtime.GLES2Samples
{

    public class E02SimpleVertexShaderApp : GLES30App
    {

        private NativeShaderProgram _program;
        private VertexAttribute _attrPosition;
        private VertexAttribute _attrTexCoord;
        private Uniform _uniMvpMatrix;
        private CubeGeometry _cube;
        private float _rotation;

        public override void Load()
        {
            // Compile vertex and fragment shaders
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                uniform mat4 u_mvpMatrix;

                attribute vec4 a_position;
                attribute vec2 a_texcoord;

                varying vec2 v_texcoord;

                void main()
                {
                  gl_Position = u_mvpMatrix * a_position;
                  v_texcoord = a_texcoord;
                }"
            );
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                varying vec2 v_texcoord;

                void main()
                {
                  gl_FragColor = vec4(v_texcoord.x, v_texcoord.y, 1.0, 1.0);
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
            this._attrTexCoord = new VertexAttribute(this._program, "a_texcoord");

            // Initialize uniform
            this._uniMvpMatrix = new Uniform(this._program, "u_mvpMatrix");

            // Generate the geometry data
            this._cube = new CubeGeometry(0.5f);

            // Set initial rotation
            this._rotation = 45;

            // Set clear color to black
            GL.ClearColor(0, 0, 0, 0);

            // Enable culling of back-facing polygons
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
            this._attrPosition.SetData(this._cube.Positions);
            this._attrPosition.Enable();

            this._attrTexCoord.SetData(this._cube.TexCoords);
            this._attrTexCoord.Enable();

            // Calculate and set MVP matrix
            this._rotation = (this._rotation + (GameTime.DeltaSec * 40.0f)) % 360.0f;

            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathFR.DegreesToRadians(60), (float)Width / (float)Height, 1, 20);
            Matrix4 translate = Matrix4.CreateTranslation(0, 0, -2);
            Matrix4 rotate = Matrix4.CreateFromAxisAngle(new Vector3(1, 0, 1), MathFR.DegreesToRadians(this._rotation));

            Matrix4 model = rotate * translate;
            Matrix4 Mvp = model * perspective;
            this._uniMvpMatrix.SetValue(ref Mvp);

            // Draw the cube
            GL.DrawElements(PrimitiveType.Triangles, this._cube.Indices);
        }

        public override void Unload()
        {
            this._program.Dispose();
        }

    }

}
