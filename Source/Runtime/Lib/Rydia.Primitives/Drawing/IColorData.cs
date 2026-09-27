using System;

namespace Rydia.Drawing
{
    /// <summary>
    /// 各種の色情報を扱うための汎用インターフェースです。
    /// </summary>
    public interface IColorData
    {

        /// <summary>
        /// 色を <see cref="int"/> 型の RGBA 値に変換します。
        /// </summary>
        /// <returns>RGBA 形式の色情報を表す整数値です。</returns>
        int ToIntRgba();

        /// <summary>
        /// 指定された <see cref="int"/> 型の RGBA 値を使用して色を設定します。
        /// </summary>
        /// <param name="rgba">RGBA 形式の色情報を表す整数値です。</param>
        void SetIntRgba(int rgba);

        /// <summary>
        /// 色を <see cref="int"/> 型の ARGB 値に変換します。
        /// </summary>
        /// <returns>ARGB 形式の色情報を表す整数値です。</returns>
        int ToIntArgb();

        /// <summary>
        /// 指定された <see cref="int"/> 型の ARGB 値を使用して色を設定します。
        /// </summary>
        /// <param name="argb">ARGB 形式の色情報を表す整数値です。</param>
        void SetIntArgb(int argb);

    }
}
