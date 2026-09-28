using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// ユーザー入力を提供する入力デバイスまたは入力チャネルを表します。
    /// </summary>
    public interface IUserInput
    {

        /// <summary>
        /// [取得] この入力を一意に識別するIDを取得します。
        /// </summary>
        string Id { get; }

        /// <summary>
        ///  この入力を提供している製品を一意に識別するIDを取得します。
        /// </summary>
        Guid ProductId { get; }

        /// <summary>
        ///  この入力を提供している製品の名前を取得します。
        /// </summary>
        string ProductName { get; }

        /// <summary>
        /// [取得] この入力が現在利用可能かどうかを取得します。
        /// </summary>
        bool IsAvailable { get; }

        /// <summary>
        /// [取得 / 設定] この入力の状態を取得する入力ソースを取得または設定します。
        /// </summary>
        IUserInputSource Source { get; set; }

        /// <summary>
        /// 入力が利用可能になったときに発生します。
        /// </summary>
        event EventHandler BecomesAvailable;

        /// <summary>
        /// 入力が利用できなくなったときに発生します。
        /// </summary>
        event EventHandler NoLongerAvailable;

        /// <summary>
        /// 入力の現在の状態を更新します。
        /// </summary>
        /// <param name="width">入力対象となる領域の幅です。</param>
        /// <param name="height">入力対象となる領域の高さです。</param>
        void Update(int width, int height);

    }

}
