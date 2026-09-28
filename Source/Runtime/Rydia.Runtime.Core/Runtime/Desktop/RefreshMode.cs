using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Desktop
{

    /// <summary>
    /// 画面の更新およびゲームの更新を行う間隔とモードを指定します。
    /// </summary>
    public enum RefreshMode
    {
        /// <summary>
        /// 待機せず、可能な限り高速に画面を更新します。
        /// </summary>
        NoSync,

        /// <summary>
        /// 60 FPSを目標として画面を更新し、各フレームで利用可能な時間を
        /// 使い切るまで待機します。
        /// ハードウェアまたはドライバーの垂直同期（VSync）は使用せず、
        /// CPU使用率が100%になることを防ぎます。
        /// </summary>
        ManualSync,

        /// <summary>
        /// ハードウェアまたはドライバーの垂直同期（VSync）を待機して
        /// 画面を更新します。
        /// </summary>
        VSync,

        /// <summary>
        /// 目標フレームレートを維持できる場合はハードウェアまたは
        /// ドライバーの垂直同期（VSync）を使用します。
        /// 目標フレームレートを下回った場合は、一時的にVSyncを無効にします。
        /// </summary>
        AdaptiveVSync
    }

}
