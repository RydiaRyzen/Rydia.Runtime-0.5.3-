using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Platform.Android;
using Rydia.Runtime.Mobile;

namespace Rydia.Runtime.Droid
{

    /// <summary>
    /// Android 上で OpenGL ES を使用してアプリケーションを描画するビューを提供します。
    /// アプリケーションのライフサイクル、更新、描画、およびフレームバッファの設定を管理します。
    /// </summary>
    public class GLView : AndroidGameView, IMobileWindow
    {

        private readonly AppRunner _application;

        /// <summary>
        /// <see cref="GLView"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="context">Android のコンテキストを指定します。</param>
        /// <param name="application">実行するアプリケーションを指定します。</param>
        public GLView(Context context, AppRunner application)
            : base(context)
        {
            Debug.Assert(application != null);
            this._application = application;
        }

        /// <summary>
        /// OpenGL ES のロード時に呼び出されます。
        /// アプリケーションを初期化し、ゲームループを開始します。
        /// </summary>
        /// <param name="e">イベントデータを指定します。</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this._application.Load();
            Run(60);
        }

        /// <summary>
        /// OpenGL ES のアンロード時に呼び出されます。
        /// アプリケーションの終了処理を実行します。
        /// </summary>
        /// <param name="e">イベントデータを指定します。</param>
        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            this._application.Unload();
        }

        /// <summary>
        /// フレームの描画時に呼び出されます。
        /// アプリケーションの更新処理と描画処理を実行し、描画結果を画面に反映します。
        /// </summary>
        /// <param name="e">フレームに関するイベントデータを指定します。</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            this._application.Update();
            this._application.Render();
            SwapBuffers();
        }

        /// <summary>
        /// OpenGL ES 用のフレームバッファを作成します。
        /// </summary>
        /// <remarks>
        /// OpenGL ES 2.0 コンテキストを使用し、アプリケーションの設定に応じて
        /// ステンシルバッファを確保します。
        /// </remarks>
        protected override void CreateFrameBuffer()
        {
            ContextRenderingApi = GLVersion.ES2;
            GraphicsMode = new GraphicsMode(32, 16, this._application.NeedStencilBuffer ? 8 : 0, 0, 0, 2, false);
            base.CreateFrameBuffer();
        }

        /// <summary>
        /// ビューのサイズが変更されたときに呼び出されます。
        /// アプリケーションに新しい描画領域のサイズを通知します。
        /// </summary>
        /// <param name="e">イベントデータを指定します。</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this._application.Resize(Width, Height);
        }
    }
}
