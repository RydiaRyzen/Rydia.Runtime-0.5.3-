using Rydia.Runtime.Desktop;

namespace Rydia.Runtime.Desktop
{

    /// <summary>
    /// ウィンドウの表示および更新に関する設定を定義します。
    /// </summary>
    public class WindowOptions
    {

        /// <summary>
        /// ウィンドウの幅をピクセル単位で取得または設定します。
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// ウィンドウの高さをピクセル単位で取得または設定します。
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// ウィンドウのタイトルを取得または設定します。
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 画面の更新方式を取得または設定します。
        /// </summary>
        public RefreshMode RefreshMode { get; set; }

        /// <summary>
        /// ウィンドウの表示モードを取得または設定します。
        /// </summary>
        public ScreenMode ScreenMode { get; set; }

    }
}