using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Rydia.Runtime.Desktop;

namespace Rydia.Runtime
{

    /// <summary>
    /// アプリケーションの初期化、更新、描画、および終了処理を管理する基底クラスを定義します。
    /// </summary>
    public abstract class AppRunner
    {

        private Action _PlatformUpdate;

        /// <summary>
        /// ステンシルバッファが必要かどうかを取得します。
        /// </summary>
        /// <value>
        /// ステンシルバッファが必要な場合は <c>true</c>、それ以外の場合は <c>false</c> を返します。
        /// </value>
        public virtual bool NeedStencilBuffer
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// アプリケーションの描画領域の幅を取得します。
        /// </summary>
        public int Width
        {
            get;
            private set;
        }

        /// <summary>
        /// アプリケーションの描画領域の高さを取得します。
        /// </summary>
        public int Height
        {
            get;
            private set;
        }

        /// <summary>
        /// アプリケーションの経過時間を管理する <see cref="GameTime"/> を取得します。
        /// </summary>
        public GameTime GameTime
        {
            get;
            private set;
        }

        /// <summary>
        /// 現在使用しているクロスプラットフォーム環境を取得します。
        /// </summary>
        public CrossPlatform Platform
        {
            get;
            private set;
        }

        /// <summary>
        /// アプリケーションを初期化します。
        /// </summary>
        /// <param name="platform">使用するプラットフォームを指定します。</param>
        /// <param name="width">描画領域の幅を指定します。</param>
        /// <param name="height">描画領域の高さを指定します。</param>
        public void Init(CrossPlatform platform, int width, int height)
        {
            Debug.Assert(platform != null);
            Debug.Assert(width > 0);
            Debug.Assert(height > 0);

            Platform = platform;
            Width = width;
            Height = height;

            if(Platform.Is<IDesktopPlatform>())
            {
                this._PlatformUpdate = DesktopUpdate;
            }
            else
            {
                this._PlatformUpdate = MobileUpdate;
            }

            GameTime = new GameTime();
        }

        /// <summary>
        /// アプリケーションで使用するリソースなどを読み込みます。
        /// </summary>
        public abstract void Load();

        /// <summary>
        /// アプリケーションの更新処理を実行します。
        /// </summary>
        public void Update()
        {
            GameTime.Update();
            this._PlatformUpdate?.Invoke();
            Update(GameTime.DeltaSec, GameTime.TotalSec);
        }

        /// <summary>
        /// モバイルプラットフォーム固有の更新処理を実行します。
        /// </summary>
        private void MobileUpdate()
        {

        }

        /// <summary>
        /// デスクトッププラットフォーム固有の更新処理を実行します。
        /// </summary>
        private void DesktopUpdate()
        {
            if(Platform.IsAs<IDesktopPlatform>(out var desktopPlatform))
            {
                desktopPlatform.Window.Keyboard.Update(Width, Height);
                desktopPlatform.Window.Mouse.Update(Width, Height);
            }
        }

        /// <summary>
        /// アプリケーション固有の更新処理を実行します。
        /// </summary>
        /// <param name="deltaTimeSec">前回の更新からの経過時間を秒単位で指定します。</param>
        /// <param name="totalTimeSec">アプリケーション開始からの経過時間を秒単位で指定します。</param>
        public abstract void Update(float deltaTimeSec, float totalTimeSec);
        
        /// <summary>
        /// アプリケーションの描画処理を実行します。
        /// </summary>
        public abstract void Render();

        /// <summary>
        /// 描画領域のサイズを変更します。
        /// </summary>
        /// <param name="width">新しい描画領域の幅を指定します。</param>
        /// <param name="height">新しい描画領域の高さを指定します。</param>
        public virtual void Resize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// アプリケーションで使用しているリソースを解放します。
        /// </summary>
        public abstract void Unload();

    }

}
