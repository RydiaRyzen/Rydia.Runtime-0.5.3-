using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Audio.Api.OpenAL.Alc
{

    public enum AlcContextAttributes
    {
        /// <summary>
        /// ミキシング出力周波数をHzで指定
        /// </summary>
        Frequency = 0x1007,
        /// <summary>
        /// 更新間隔をHzで指定
        /// </summary>
        Refresh = 0x1008,
        /// <summary>
        /// 同期コンテキストかどうかを指定
        /// </summary>
        Sync = 0x1009,
        /// <summary>
        /// モノラルデータをサポートするソースがいくつあるべきかのヒント
        /// </summary>
        MonoSources = 0x1010,
        /// <summary>
        /// ステレオデータをサポートするソースがいくつあるべきかのヒント
        /// </summary>
        StereoSources = 0x1011,
    }

}
