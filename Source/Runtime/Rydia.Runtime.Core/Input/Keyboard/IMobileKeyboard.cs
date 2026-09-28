using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    /// <summary>
    /// モバイル端末のキーボード入力を取得するためのインターフェースです。
    /// </summary>
    public interface IMobileKeyboard : IDisposableEx
    {

        /// <summary>
        /// 指定したキーが押されているかどうかを取得します。
        /// </summary>
        /// <param name="keyCode">
        /// 判定するキーのキーコードです。
        /// </param>
        /// <returns>
        /// キーが押されている場合は <see langword="true"/>、
        /// 押されていない場合は <see langword="false"/> を返します。
        /// </returns>
        bool IsKeyPressed(int keyCode);

        /// <summary>
        /// キーボードイベントを取得します。
        /// </summary>
        /// <returns>
        /// 現在のフレームで発生したキーボードイベントの一覧です。
        /// </returns>
        List<KeyEvent> GetKeyEvents();

    }

}
