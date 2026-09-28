using System;
using System.Collections.Generic;
using System.Text;
using Rydia.Runtime.Shared;
namespace Rydia.Runtime
{

    /// <summary>
    /// プラットフォーム固有の機能を提供するインターフェースを定義します。
    /// </summary>
    public interface IPlatform : IMessageService
    {

        /// <summary>
        /// 画面のスケールを取得します。
        /// </summary>
        float ScreenScale { get; }

        /// <summary>
        /// アプリケーションランナーを取得します。
        /// </summary>
        AppRunner App
        {
            get;
        }

    }

}
