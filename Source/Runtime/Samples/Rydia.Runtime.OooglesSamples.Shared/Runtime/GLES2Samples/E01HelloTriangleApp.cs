using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.Runtime.ES30;

namespace Rydia.Runtime.GLES2Samples
{

    /// <summary>
    /// プログラムの本体を表すクラスです
    /// </summary>
    /// <seealso cref="Rydia.Runtime.ES20App" />
    public class E01HelloTriangleApp : GLES30App
    {

        // OpenGL ES1.0では固定機能シェーダーがありましたが、
        // ES2.0では同様の機能を持つシェーダーを自分で作成する必要があります。
        // プログラマブルシェーダーの実行環境はアプリケーションの実行環境とは異なり、
        // アプリ側では頂点シェーダーとフラグメントシェーダーを作成し、
        // ハンドル経由で変数を代入することで、プログラマブルシェーダーを操作します
        // ハンドルとは外部のプログラムから、シェーダー変数にアクセスするためのIDになります
        // 
        private NativeShaderProgram? _program;
        private VertexAttribute _attrPosition;

        private readonly Vector3[] _vertices = {
            new Vector3( 0.0f,  0.5f, 0.0f),
            new Vector3(-0.5f, -0.5f, 0.0f),
            new Vector3( 0.5f, -0.5f, 0.0f)
        };

        public override bool NeedStencilBuffer
        {
            get
            {
                return false;
            }
        }

        public override void Load()
        {
            // Compile vertex and fragment shaders
            var vertexShader = new NativeShader(ShaderType.VertexShader, @"
                attribute vec4 vPosition;

                void main(void)
                {
                    gl_Position = vPosition;
                }"
            );
            vertexShader.Compile();

            var fragmentShader = new NativeShader(ShaderType.FragmentShader, @"
                precision mediump float;

                void main(void)
                {
                    gl_FragColor = vec4(1.0, 0.0, 0.0, 1.0);
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
            this._attrPosition = new VertexAttribute(this._program, "vPosition");

            // Set clear color to black
            GL.ClearColor(0, 0, 0, 0);
        }

        public override void Render()
        {
            // Clear the color buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
            
            // Use the program
            this._program?.Use();

            // Set the data for the vertex attribute
            this._attrPosition.SetData(this._vertices);
            this._attrPosition.Enable();

            // Draw the triangle
            GL.DrawArrays(PrimitiveType.Triangles, 3);
        }

        public override void Unload()
        {
            this._program?.Dispose();
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {

        }
    }

}
