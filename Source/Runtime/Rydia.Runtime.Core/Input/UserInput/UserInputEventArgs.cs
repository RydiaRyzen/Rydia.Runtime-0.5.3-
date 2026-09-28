using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Input
{

    /// <summary>
    /// ユーザー入力に関するイベントの引数を表す基底クラスです。
    /// </summary>
    public class UserInputEventArgs : EventArgs
    {
        private IUserInput inputChannel;

        /// <summary>
        /// このイベントが発生した入力チャネルを取得します。
        /// </summary>
        public IUserInput InputChannel
        {
            get { return this.inputChannel; }
        }

        /// <summary>
        /// 指定した入力チャネルを使用してイベント引数を初期化します。
        /// </summary>
        /// <param name="inputChannel">
        /// このイベントに関連付ける入力チャネルです。
        /// </param>
        public UserInputEventArgs(IUserInput inputChannel)
        {
            this.inputChannel = inputChannel;
        }

    }
}
