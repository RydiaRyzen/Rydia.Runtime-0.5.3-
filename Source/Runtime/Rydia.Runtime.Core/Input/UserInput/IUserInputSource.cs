using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// ユーザー入力の状態を提供する入力ソースを表す基本インターフェースです。
    /// </summary>
    public interface IUserInputSource
    {

        /// <summary>
        /// [取得] この入力ソースを一意に識別するIDを取得します。
        /// </summary>
        string Id { get; }

        /// <summary>
        /// [取得] この入力ソースを提供している製品を一意に識別するIDを取得します。
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        /// [取得] この入力ソースを提供している製品の名前を取得します。
        /// </summary>
        string ProductName { get; }

        /// <summary>
        /// [取得] この入力ソースが現在利用可能かどうかを取得します。
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// 入力ソースの現在の状態を更新します。
        /// </summary>
        void UpdateState();
    }

}
