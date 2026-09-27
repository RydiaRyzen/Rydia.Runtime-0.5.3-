using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Audio.Api.OpenAL.Alc
{

    public enum AlcGetInteger
    {
        /// <summary>
        /// この実装の仕様リビジョン (メジャー バージョン)。 NULL は許容されるデバイスです。
        /// </summary>
        MajorVersion = 0x1000,
        /// <summary>
        /// この実装の仕様リビジョン (マイナー バージョン)。 NULL は許容されるデバイスです。
        /// </summary>
        MinorVersion = 0x1001,
        /// <summary>
        /// 現在のコンテキストの 0 で終わる属性リストに必要なサイズ (ALCint 値の数)。
        /// NULL は無効なデバイスです
        /// </summary>
        AttributesSize = 0x1002,
        /// <summary>
        /// ALC_ATTRIBUTES_SIZEの宛先を想定し、
        /// 指定したデバイスの現在のコンテキストの属性リストを提供します。
        /// NULL は無効なデバイスです
        /// </summary>
        AllAttributes = 0x1003,
        /// <summary>
        /// 使用可能なキャプチャ サンプルの数。 NULL は無効なデバイスです。
        /// </summary>
        CaptureSamples = 0x312,
        /// <summary>
        /// (EFX 拡張機能)このプロパティは、
        /// この OpenAL 実装でサポートされている効果拡張機能のメジャー バージョン番号を取得するために、
        /// アプリケーションで使用できます。
        /// これは Context プロパティであるため、alcGetIntegerv を使用して取得する必要があります。
        /// </summary>
        EfxMajorVersion = 0x20001,
        /// <summary>
        /// (EFX 拡張機能)このプロパティは、
        /// この OpenAL 実装でサポートされている効果拡張機能のマイナー バージョン番号を取得するために、
        /// アプリケーションで使用できます。
        /// これは Context プロパティであるため、alcGetIntegerv を使用して取得する必要があります。
        /// </summary>
        EfxMinorVersion = 0x20002,
        /// <summary>
        /// (EFX 拡張機能)この Context プロパティは、
        /// コンテキストの作成時 (alcCreateContext) の間に OpenAL に渡して、
        /// 各ソースで必要な補助送信の最大数を要求できます。
        /// 目的の送信数が使用可能になることは保証されていないため、
        /// アプリケーションは、alcGetIntergerv を使用してコンテキストを作成した後、
        /// このプロパティに対してクエリを実行する必要があります。
        /// 既定値: 2
        /// </summary>
        EfxMaxAuxiliarySends = 0x20003,

    }

}
