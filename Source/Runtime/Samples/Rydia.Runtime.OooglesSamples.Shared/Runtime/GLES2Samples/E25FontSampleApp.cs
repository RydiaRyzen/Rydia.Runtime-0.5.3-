using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Rydia.Drawing;
using Rydia.Graphics.Api.ES20;
using Rydia.Graphics.Backend;
using Rydia.IO;
using Rydia.Resources;
using Rydia.Runtime.ES30;
using Rydia.Serialization;

namespace Rydia.Runtime.GLES2Samples
{

    public class E25FontSampleApp : GLES30App
    {

        private NativeShaderProgram _program;
        private FontCharSet _textureDict;
        private float[] vertices =
            {
            -0.8f, 0.0f,  // 左上
             0.8f,  0.0f,  // 右上
            -0.8f, -0.1f,  // 左下
             0.8f, -0.1f   // 右下
        };

        private float[] texCoords = {
            0, 0,
            1, 0,
            0, 1,
            1, 1
        };

        public override void Load()
        {
            // シェーダーを作成
            if (!CreateSimpleTextureShader(out this._program))
            {
                return;
            }
            var er = new EmbeddedResources<E25FontSampleApp>();

            // フォントを読み込む
            var str = er.LoadText("Assets.Arial_33px_Regular.ryres");
            var temp = Convert.FromBase64String(str);
            var resource = new BinarySerializer().Deserialize<FontCharSet>(temp);
            resource.CreateCharSet();
            Console.WriteLine($"文字数: {resource.Chars.Count}");
            resource.CreateTextures(GL);
            this._textureDict = resource;
            // クリアカラーを青に設定
            GL.ClearColor(ColorRgba.Blue);
            // アルファブレンドを有効化
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }

        public static bool CreateSimpleTextureShader(out NativeShaderProgram program)
        {
            string vertexShaderCode = @"
                attribute vec2 a_Position;
                attribute vec2 a_TexCoord;
                varying vec2 v_TexCoord;
                void main() {
                    gl_Position = vec4(a_Position, 0.0, 1.0);
                    v_TexCoord = a_TexCoord;
                }";

            string fragmentShaderCode = @"
                precision mediump float;
                uniform sampler2D u_Texture;
                varying vec2 v_TexCoord;
                void main() {
                    gl_FragColor = texture2D(u_Texture, v_TexCoord);
                }";

            var vertexShader = new NativeShader(ShaderType.VertexShader, vertexShaderCode);
            var fragmentShader = new NativeShader(ShaderType.FragmentShader, fragmentShaderCode);
            if (!vertexShader.Compile())
            {
                vertexShader.Dispose();
                program = null;
                return false;
            }
            if (!fragmentShader.Compile())
            {
                fragmentShader.Dispose();
                program = null;
                return false;
            }
            program = new NativeShaderProgram();
            program.AttachShader(vertexShader);
            program.AttachShader(fragmentShader);
            if (!program.Link())
            {
                program.Dispose();
                return false;
            }
            return true;
        }

        public override void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            this._program.Use();

            var posHandle = this._program.GetAttribLocation("a_Position");
            var texHandle = this._program.GetAttribLocation("a_TexCoord");
            var texUniform = this._program.GetUniformLocation("u_Texture");

            posHandle.Enable();
            texHandle.Enable();

            //posHandle.SetData(VertexAttribPointerType.Float, 2, vertices);
            texHandle.SetData(VertexAttribPointerType.Float, 2, this.texCoords);

            int i = 0;
            var x = 40;
            var y = 100;
            var value = "HELLO WORLD";
            var hh = 0;
            var tC = value[0];
            foreach (var item in value)
            {
                //var textTex = this._textureDict.GetTexture(item);
                if (i != 0)
                {
                    hh = MathFR.Max(this._textureDict.GetPixelData(item).Height, this._textureDict.GetPixelData(tC).Height);
                    tC = item;
                }
                i++;
            }
            i = 0;
            tC = value[0];
            foreach (var item in value)
            {
                var textTex = this._textureDict.GetTexture(item);
                textTex.BindToTextureUnit(TextureUnit.Texture0);
                texUniform.SetValue(0);
                if (i != 0)
                {
                    var textTex2 = this._textureDict.GetTexture(tC);
                    x += this._textureDict.GetPixelData(tC).Width;
                    tC = item;
                }
                var w = WindowToScreen(x, y, this._textureDict.GetPixelData(tC).Width, hh);
                this.vertices = new float[]{
                    w[0], w[1],  // 左上
                    w[6], w[7],  // 右上
                    w[3], w[4],  // 左下
                    w[9], w[10]  // 右下
                };
                Debug.WriteLine("{0} = {1}, {2}, {3}, {4}", item, x, y, this._textureDict.GetPixelData(tC).Width, this._textureDict.GetPixelData(tC).Height);
                posHandle.SetData(VertexAttribPointerType.Float, 2, this.vertices);
                GL.DrawArrays(PrimitiveType.TriangleStrip, 0, 4);
                i++;
            }
            Debug.WriteLine("");

            posHandle.Disable();
            texHandle.Disable();
        }

        /// <summary>
        /// ウィンドウ座標を正規化デバイス座標に変換(1)します
        /// </summary>
        /// <param name="x">を表す数値</param>
        /// <param name="y">を表す数値</param>
        /// <param name="w">を表す数値</param>
        /// <param name="h">を表す数値</param>
        /// <returns></returns>
        protected float[] WindowToScreen(int x, int y, int w, int h)
        {
            float left = ((float)x / (float)Width) * 2.0f - 1.0f;
            float top = ((float)y / (float)Height) * 2.0f - 1.0f;
            float right = ((float)(x + w) / (float)Width) * 2.0f - 1.0f;
            float bottom = ((float)(y + h) / (float)Height) * 2.0f - 1.0f;
            top = -top;
            bottom = -bottom;

            //頂点バッファの生成
            float[] vertexs ={
            left, top,   0.0f,
            left, bottom,0.0f,
            right,top,   0.0f,
            right,bottom,0.0f,
            };
            return vertexs;
        }
        public override void Unload()
        {
            this._program.Dispose();
            this._textureDict.DisposeTextures();
        }

        public override void Update(float deltaTimeSec, float totalTimeSec)
        {
            GL.Viewport(0, 0, Width, Height);
        }
    }

}
