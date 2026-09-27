using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;

namespace Rydia.Runtime.GLES2Samples
{
    public class E08StencilOperationsApp : GLES30App
    {
        private NativeShaderProgram _program;
        private VertexAttribute _attrPosition;
        private Uniform _uniColor;

        private readonly Vector3[] _vertices = {
            // Quad #0
            new Vector3(-0.75f,  0.25f, 0.50f),
            new Vector3(-0.25f,  0.25f, 0.50f),
            new Vector3(-0.25f,  0.75f, 0.50f),
            new Vector3(-0.75f,  0.75f, 0.50f),
            // Quad #1
            new Vector3( 0.25f,  0.25f, 0.90f),
            new Vector3( 0.75f,  0.25f, 0.90f),
            new Vector3( 0.75f,  0.75f, 0.90f),
            new Vector3( 0.25f,  0.75f, 0.90f),
            // Quad #2
            new Vector3(-0.75f, -0.75f, 0.50f),
            new Vector3(-0.25f, -0.75f, 0.50f),
            new Vector3(-0.25f, -0.25f, 0.50f),
            new Vector3(-0.75f, -0.25f, 0.50f),
            // Quad #3
            new Vector3( 0.25f, -0.75f, 0.50f),
            new Vector3( 0.75f, -0.75f, 0.50f),
            new Vector3( 0.75f, -0.25f, 0.50f),
            new Vector3( 0.25f, -0.25f, 0.50f),
            // Big Quad
            new Vector3(-1.00f, -1.00f, 0.00f),
            new Vector3( 1.00f, -1.00f, 0.00f),
            new Vector3( 1.00f,  1.00f, 0.00f),
            new Vector3(-1.00f,  1.00f, 0.00f)
        };

        private readonly byte[] _indices0 = { 0, 1, 2, 0, 2, 3 }; // Quad #0
        private readonly byte[] _indices1 = { 4, 5, 6, 4, 6, 7 }; // Quad #1
        private readonly byte[] _indices2 = { 8, 9, 10, 8, 10, 11 }; // Quad #2
        private readonly byte[] _indices3 = { 12, 13, 14, 12, 14, 15 }; // Quad #3
        private readonly byte[] _indices4 = { 16, 17, 18, 16, 18, 19 }; // Big Quad

        private const int _testCount = 4;

        private readonly Vector4[] _colors =
        {
            new Vector4(1, 0, 0, 1),
            new Vector4(0, 1, 0, 1),
            new Vector4(0, 0, 1, 1),
            new Vector4(1, 1, 0, 0)
        };

        public override bool NeedStencilBuffer
        {
            get
            {
                return true;
            }
        }

        public override void Load()
        {

            // Compile vertex and fragment shaders
            NativeShader vertexShader = new NativeShader(ShaderType.VertexShader, @"
                attribute vec4 a_position;

                void main()
                {
                  gl_Position = a_position;
                }");
            vertexShader.Compile();

            NativeShader fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                uniform vec4 u_color;

                void main()
                {
                  gl_FragColor = u_color;
                }");
            fragmentShader.Compile();

            // Link shaders into program
            this._program = new NativeShaderProgram(vertexShader, fragmentShader);
            this._program.Link();

            // We don't need the shaders anymore. 
            // Note that the shaders won't actually be deleted until the program is deleted.
            vertexShader.Dispose();
            fragmentShader.Dispose();

            // Initialize vertex attribute
            this._attrPosition = new VertexAttribute(this._program, "a_position");

            // Initialize uniform
            this._uniColor = new Uniform(this._program, "u_color");

            // Set clear color to black
            GL.ClearColor(ColorRgba.DarkBlue);

            // Set the stencil clear value
            GL.ClearStencil(0x01);

            // Set the depth clear value
            GL.ClearDepth(0.75f);

            // Enable the depth and stencil tests
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.StencilTest);
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }

        public override void Render()
        {
            // Result of tests 0, 1 and 2
            byte[] stencilValues = { 0x07, 0x00, 0x02, 0x00 };

            // Clear the color, depth, and stencil buffers.
            // At this point, the stencil buffer will be 0x01 for all pixels.
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            // Use the program
            this._program.Use();

            // Set the data for the vertex attribute
            this._attrPosition.SetData(this._vertices);
            this._attrPosition.Enable();

            // Test 0:
            //
            // Initialize upper-left region. In this case, the stencil-buffer values will
            // be replaced because the stencil test for the rendered pixels will fail the
            // stencil test, which is:
            //
            //    ref    mask   stencil  mask
            //   (0x07 & 0x03) < (0x01 & 0x07)
            //
            // The value in the stencil buffer for these pixels will be 0x07.
            GL.StencilFunc(StencilFunction.Less, 0x07, 0x03);
            GL.StencilOperation(StencilOp.Replace, StencilOp.Decrement, StencilOp.Decrement);
            GL.DrawElements(PrimitiveType.Triangles, this._indices0);

            // Test 1:
            //
            // Initialize upper-right region. Here, we'll decrement the stencil-buffer
            // values where the stencil test passes but the depth test fails.
            // The stencil test is:
            //
            //    ref    mask   stencil  mask
            //   (0x03 & 0x03) > (0x01 & 0x03)
            //
            // But where the geometry fails the depth test. The stencil values for these
            // pixels will be 0x00.
            GL.StencilFunc(StencilFunction.Greater, 0x03, 0x03);
            GL.StencilOperation(StencilOp.Keep, StencilOp.Decrement, StencilOp.Keep);
            GL.DrawElements(PrimitiveType.Triangles, this._indices1);

            // Test 2:
            //
            // Initialize the lower - left region. Here we'll increment (with saturation) the
            // stencil value where both the stencil and depth tests pass. The stencil test
            // for these pixels will be:
            //
            //    ref    mask   stencil  mask
            //   (0x01 & 0x03) = (0x01 & 0x03)
            //
            // The stencil values for these pixels will be 0x02.
            GL.StencilFunc(StencilFunction.Equal, 0x01, 0x03);
            GL.StencilOperation(StencilOp.Keep, StencilOp.Increment, StencilOp.Increment);
            GL.DrawElements(PrimitiveType.Triangles, this._indices2);

            // Test 3:
            //
            // Finally, initialize the lower - right region. We'll invert the stencil value
            // where the stencil tests fails. The stencil test for these pixels will be:
            //
            //    ref    mask   stencil  mask
            //   (0x02 & 0x01) = (0x01 & 0x01)
            //
            // The stencil value here will be set to (~((2 ^ s - 1) & 0x01)), (with the 0x01
            // being from the stencil clear value), where 's' is the number of bits in the
            // stencil buffer.
            GL.StencilFunc(StencilFunction.Equal, 0x02, 0x01);
            GL.StencilOperation(StencilOp.Invert, StencilOp.Keep, StencilOp.Keep);
            GL.DrawElements(PrimitiveType.Triangles, this._indices3);

            // Since we don't know at compile time how many stencil bits are present, we'll
            // query, and update the value correct value in the stencilValues arrays for
            // the fourth tests. We'll use this value later in rendering.
            stencilValues[3] = (byte)((~(((1 << NativeFramebuffer.Current.StencilBits) - 1) & 0x01)) & 0xff);

            // Use the stencil buffer for controlling where rendering will occur. We
            // disable writing to the stencil buffer so we can test against them without
            // modifying the values we generated.
            GL.StencilMask(0x00);

            for (int i = 0; i < _testCount; i++)
            {
                GL.StencilFunc(StencilFunction.Equal, stencilValues[i], 0xff);
                this._uniColor.SetValue(this._colors[i]);
                GL.DrawElements(PrimitiveType.Triangles, this._indices4);
            }

            // Reset the stencil mask
            GL.StencilMask(0xff);
        }

        public override void Unload()
        {
            this._program.Dispose();
        }
    }


}
