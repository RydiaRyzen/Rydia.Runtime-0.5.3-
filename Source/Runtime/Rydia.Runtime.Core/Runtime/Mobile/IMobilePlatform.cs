using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Input;

namespace Rydia.Runtime.Mobile
{

    /// <summary>
    /// モバイルプラットフォーム固有の機能を提供するインターフェースを定義します。
    /// </summary>
    public interface IMobilePlatform : IPlatform
    {

        /// <summary>
        /// 加速度センサーを取得します。
        /// </summary>
        IAccelerometer Accelerometer
        {
            get;
        }

        /// <summary>
        /// モバイル端末のキーボード入力を取得します。
        /// </summary>
        IMobileKeyboard Keyboard
        {
            get;
        }

        /// <summary>
        /// タッチ入力を処理するハンドラーを取得します。
        /// </summary>
        ITouchHandler Touch
        {
            get;
        }

    }
}
