using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Rydia.Graphics.Api.ES20
{

    /// <summary>
    /// <see cref="DataBuffer"/> のサポートされているタイプ
    /// </summary>
    public enum BufferTarget
    {

        /// <summary>
        /// バーテックスバッファ (別名アレイバッファ)。
        /// 頂点のアレイを含みます。
        /// </summary>
        ArrayBuffer = ESAllEnum.ArrayBuffer,

        /// <summary>
        /// インデックスバッファ (別名エレメントアレイバッファ)。
        /// 頂点へのインデックスのアレイを含みます。
        /// </summary>
        ElementArrayBuffer = ESAllEnum.ElementArrayBuffer,

    }
}
