using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Rydia.Audio.Api.OpenAL;
using Rydia.Graphics.Api.ES30;
using Rydia.Input;
using Rydia.Runtime.Mobile;
using Rydia.Runtime.Shared;
using static Android.Icu.Text.CaseMap;

namespace Rydia.Runtime.Droid
{

    /// <summary>
    /// OpenGL ES を使用するモバイルアプリケーションのメインアクティビティを提供します。
    /// モバイルプラットフォームとしての各種入力、画面スケール、アプリケーション実行環境を管理します。
    /// </summary>
    public class GLActivity : Activity, IMobilePlatform
    {

        private GLView _view;
        AccelerometerHandler accelHandler;
        KeyboardHandler keyHandler;
        TouchHandler touchHandler;

        /// <summary>
        /// 加速度センサーの入力ハンドラーを取得します。
        /// </summary>
        public IAccelerometer Accelerometer
        {
            get
            {
                return this.accelHandler;
            }
        }

        /// <summary>
        /// モバイルキーボードの入力ハンドラーを取得します。
        /// </summary>
        public IMobileKeyboard Keyboard
        {
            get
            {
                return this.keyHandler;
            }
        }

        /// <summary>
        /// タッチ入力のハンドラーを取得します。
        /// </summary>
        public ITouchHandler Touch
        {
            get
            {
                return this.touchHandler;
            }
        }

        /// <summary>
        /// オーディオの設定を取得します。
        /// </summary>
        public AudioOptions AudioOptions
        {
            get;
            private set;
        }

        /// <summary>
        /// <see cref="GLActivity"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="runner">アプリケーションの実行を管理する <see cref="AppRunner"/> を指定します。</param>
        public GLActivity(AppRunner runner)
        {
            App = runner;
            AudioOptions = new AudioOptions();
            IniyAudioSettings();
            RuntimeHost.Init(new GLES30Bindings());
            AudioHost.Init(new ALBindings(), new AlcBindings());
            AudioHost.CreateDeviceContext(AudioOptions);
        }

        /// <summary>
        /// オーディオの初期設定を行います。
        /// </summary>
        protected virtual void IniyAudioSettings()
        {
            // 既定のデバイスを使用
            AudioOptions.DeviceName = null;
            // サンプリング周波数を44.1kHzに設定
            AudioOptions.Frequency = 44100;
            // モノラル音源の数を16に設定
            AudioOptions.MonoSources = 16;
            // ステレオ音源の数を16に設定
            AudioOptions.StereoSources = 16;
            // 更新レートを60に設定
            AudioOptions.Refresh = 60;
            // クロック同期を無効に設定
            AudioOptions.Sync = false;
        }

        // 2. 【必須】JNI アクティベーション用のコンストラクタ
        // プロセスの復帰時に、Monoランタイムがこのコンストラクタを使って
        // JavaインスタンスとC#インスタンスを紐付け直します。

        /// <summary>
        /// Android の Java インスタンスと C# インスタンスを関連付けるための
        /// JNI アクティベーション用コンストラクターです。
        /// </summary>
        /// <param name="javaReference">Java オブジェクトへの参照を指定します。</param>
        /// <param name="transfer">JNI ハンドルの所有権を指定します。</param>
        protected GLActivity(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        /// <summary>
        /// アクティビティの作成時に呼び出され、OpenGL ES ビューや各種入力ハンドラーを初期化します。
        /// </summary>
        /// <param name="savedInstanceState">
        /// 保存されたアクティビティの状態を指定します。
        /// 状態が保存されていない場合は <c>null</c> です。
        /// </param>
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            Window.SetFlags(Android.Views.WindowManagerFlags.Fullscreen,
                    Android.Views.WindowManagerFlags.Fullscreen);
            bool isLandscape = Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape;
            int frameBufferWidth = isLandscape ? 480 : 320;
            int frameBufferHeight = isLandscape ? 320 : 480;
            Bitmap frameBuffer = Bitmap.CreateBitmap(frameBufferWidth, frameBufferHeight, Bitmap.Config.Rgb565);
            float scaleX = (float)frameBufferWidth / WindowManager.DefaultDisplay.Width;
            float scaleY = (float)frameBufferHeight / WindowManager.DefaultDisplay.Height;

            this._view = new GLView(this, App);
            this.accelHandler = new AccelerometerHandler(this);
            this.keyHandler = new KeyboardHandler(this._view);
            this.touchHandler = new MultiTouchHandler(this._view, scaleX, scaleY);

            DisplayMetrics metrics = new DisplayMetrics();
            WindowManager.DefaultDisplay.GetMetrics(metrics);
            ScreenScale = metrics.Density;

            Point point = new Point();
            WindowManager.DefaultDisplay.GetRealSize(point);
            App.Init(new CrossPlatform(this), point.X, point.Y);

            SetContentView(this._view);
        }

        /// <summary>
        /// アクティビティが一時停止したときに呼び出されます。
        /// OpenGL ES ビューの描画処理を一時停止します。
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            this._view.Pause();
        }

        /// <summary>
        /// アクティビティが再開したときに呼び出されます。
        /// OpenGL ES ビューの描画処理を再開し、没入型フルスクリーン表示を設定します。
        /// </summary>
        protected override void OnResume()
        {
            base.OnResume();
            this._view.Resume();
            Window.DecorView.SystemUiVisibility |= (StatusBarVisibility)(Int32)
                (SystemUiFlags.HideNavigation
                | SystemUiFlags.Fullscreen
                | SystemUiFlags.LayoutFullscreen
                | SystemUiFlags.ImmersiveSticky
                | SystemUiFlags.LayoutStable);
        }

        /// <summary>
        /// アクティビティが破棄されるときに呼び出されます。
        /// オーディオデバイスなどのリソースを終了します。
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            AudioHost.Terminate();
        }

        /// <summary>
        /// メッセージダイアログを表示します。
        /// </summary>
        /// <param name="args">表示するメッセージダイアログの設定を指定します。</param>
        /// <returns>
        /// ユーザーが選択したダイアログの結果を返します。
        /// </returns>
        public Shared.DialogResult ShowMessage(MessageDialogEventArgs args)
        {
            AlertDialog.Builder alertDialogBuilder = new AlertDialog.Builder(this);
            alertDialogBuilder.SetTitle(args.Title);
            alertDialogBuilder.SetMessage(args.Message);
            var result = DialogResult.Cancel;
            if(args.Buttons == MessageBoxButtons.OK)
            {
                alertDialogBuilder.SetPositiveButton("OK", (sender, args) =>
                {
                    result = Shared.DialogResult.OK;
                });
            }
            else if(args.Buttons == MessageBoxButtons.YesNo)
            {
                alertDialogBuilder.SetPositiveButton("Yes", (sender, args) =>
                {
                    result = Shared.DialogResult.Yes;
                });
                alertDialogBuilder.SetNegativeButton("No", (sender, args) =>
                {
                    result = DialogResult.No;
                });
            }
            else if( args.Buttons == MessageBoxButtons.YesNoCancel)
            {
                alertDialogBuilder.SetPositiveButton("Yes", (sender, args) =>
                {
                    result = Shared.DialogResult.Yes;
                });
                alertDialogBuilder.SetNegativeButton("No", (sender, args) =>
                {
                    result = DialogResult.No;
                });
                alertDialogBuilder.SetNeutralButton("Cancel", (sender, args) =>
                {
                    result = DialogResult.Cancel;
                });
            }
            else
            {
                alertDialogBuilder.SetPositiveButton("OK", (sender, args) =>
                {
                    result = Shared.DialogResult.OK;
                });
                alertDialogBuilder.SetNegativeButton("Cancel", (sender, args) =>
                {
                    result = DialogResult.Cancel;
                });
            }
            alertDialogBuilder.SetCancelable(true);
            // アラートダイアログを作成します
            var alertDialog = alertDialogBuilder.Create();
            alertDialog.Show();
            return result;
        }

        /// <summary>
        /// 画面のスケールを取得します。
        /// </summary>
        public float ScreenScale
        {
            get;
            private set;
        }

        /// <summary>
        /// アプリケーションランナーを取得します。
        /// </summary>
        public AppRunner App
        {
            get;
            private set;
        }

    }
}
