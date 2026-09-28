namespace Rydia.Runtime.Desktop
{

    /// <summary>
    /// デスクトッププラットフォーム固有の機能を提供するインターフェースを定義します。
    /// </summary>
    public interface IDesktopPlatform : IPlatform
    {

        /// <summary>
        /// デスクトップウィンドウの設定を取得します。
        /// </summary>
        WindowOptions WindowOptions
        {
            get;
        }

        /// <summary>
        /// デスクトップウィンドウを取得します。
        /// </summary>
        IDesktopWindow Window
        {
            get;
        }

    }

}
