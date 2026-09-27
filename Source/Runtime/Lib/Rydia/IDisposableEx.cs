using System;

namespace Rydia
{

    /// <summary>
    /// 破棄可能なオブジェクトの機能を拡張するインターフェースです。
    /// </summary>
    public interface IDisposableEx : IDisposable
    {

        /// <summary>
        /// このオブジェクトが破棄済みかどうかを示す値を取得します。
        /// </summary>
        bool IsDisposed
        {
            get;
        }

    }

}
