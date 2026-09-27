using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Randomizer
{

    /// <summary>
    /// メルセンヌ・ツイスタージェネレーターの内部パラメーターを保持する構造体です。
    /// </summary>
    internal struct MersenneTwisterDetail
    {

        /// <summary>
        /// メルセンヌ・ツイスターパラメーターの Mersenne 指数を取得または設定します。
        /// </summary>
        public int m_mexp;

        /// <summary>
        /// 状態配列の位置オフセットを取得または設定します。
        /// </summary>
        public int m_POS1;

        /// <summary>
        /// 左シフト演算量を取得または設定します。
        /// </summary>
        public int m_SL1;

        /// <summary>
        /// 左シフト演算時のワード間シフト量を取得または設定します。
        /// </summary>
        public int m_SL2;

        /// <summary>
        /// 右シフト演算量を取得または設定します。
        /// </summary>
        public int m_SR1;

        /// <summary>
        /// 右シフト演算時のワード間シフト量を取得または設定します。
        /// </summary>
        public int m_SR2;

        /// <summary>
        /// 状態更新時に使用するビットマスク 1 を取得または設定します。
        /// </summary>
        public uint m_MSK1;

        /// <summary>
        /// 状態更新時に使用するビットマスク 2 を取得または設定します。
        /// </summary>
        public uint m_MSK2;

        /// <summary>
        /// 状態更新時に使用するビットマスク 3 を取得または設定します。
        /// </summary>
        public uint m_MSK3;

        /// <summary>
        /// 状態更新時に使用するビットマスク 4 を取得または設定します。
        /// </summary>
        public uint m_MSK4;

        /// <summary>
        /// 状態配列の周期性を確認するために使用するパリティ値 1 を取得または設定します。
        /// </summary>
        public uint m_PARITY1;

        /// <summary>
        /// 状態配列の周期性を確認するために使用するパリティ値 2 を取得または設定します。
        /// </summary>
        public uint m_PARITY2;

        /// <summary>
        /// 状態配列の周期性を確認するために使用するパリティ値 3 を取得または設定します。
        /// </summary>
        public uint m_PARITY3;

        /// <summary>
        /// 状態配列の周期性を確認するために使用するパリティ値 4 を取得または設定します。
        /// </summary>
        public uint m_PARITY4;

        /// <summary>
        /// Mersenne 指数 607 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 607 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT607()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 607;
            result.m_POS1 = 2;
            result.m_SL1 = 15;
            result.m_SL2 = 3;
            result.m_SR1 = 13;
            result.m_SR2 = 3;
            result.m_MSK1 = 0xfdff37ffU;
            result.m_MSK2 = 0xef7f3f7dU;
            result.m_MSK3 = 0xff777b7dU;
            result.m_MSK4 = 0x7ff7fb2fU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0x00000000U;
            result.m_PARITY4 = 0x5986f054U;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 1279 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 1279 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT1279()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 1279;
            result.m_POS1 = 7;
            result.m_SL1 = 14;
            result.m_SL2 = 3;
            result.m_SR1 = 5;
            result.m_SR2 = 1;
            result.m_MSK1 = 0xf7fefffdU;
            result.m_MSK2 = 0x7fefcfffU;
            result.m_MSK3 = 0xaff3ef3fU;
            result.m_MSK4 = 0xb5ffff7fU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0x00000000U;
            result.m_PARITY4 = 0x20000000U;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 2281 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 2281 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT2281()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 2281;
            result.m_POS1 = 12;
            result.m_SL1 = 19;
            result.m_SL2 = 1;
            result.m_SR1 = 5;
            result.m_SR2 = 1;
            result.m_MSK1 = 0xbff7ffbfU;
            result.m_MSK2 = 0xfdfffffeU;
            result.m_MSK3 = 0xf7ffef7fU;
            result.m_MSK4 = 0xf2f7cbbfU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0x00000000U;
            result.m_PARITY4 = 0x41dfa600U;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 4253 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 4253 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT4253()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 4253;
            result.m_POS1 = 17;
            result.m_SL1 = 20;
            result.m_SL2 = 1;
            result.m_SR1 = 7;
            result.m_SR2 = 1;
            result.m_MSK1 = 0x9f7bffffU;
            result.m_MSK2 = 0x9fffff5fU;
            result.m_MSK3 = 0x3efffffbU;
            result.m_MSK4 = 0xfffff7bbU;
            result.m_PARITY1 = 0xa8000001U;
            result.m_PARITY2 = 0xaf5390a3U;
            result.m_PARITY3 = 0xb740b3f8U;
            result.m_PARITY4 = 0x6c11486dU;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 11213 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 11213 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT11213()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 11213;
            result.m_POS1 = 68;
            result.m_SL1 = 14;
            result.m_SL2 = 3;
            result.m_SR1 = 7;
            result.m_SR2 = 3;
            result.m_MSK1 = 0xeffff7fbU;
            result.m_MSK2 = 0xffffffefU;
            result.m_MSK3 = 0xdfdfbfffU;
            result.m_MSK4 = 0x7fffdbfdU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0xe8148000U;
            result.m_PARITY4 = 0xd0c7afa3U;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 19937 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 19937 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT19937()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 19937;
            result.m_POS1 = 122;
            result.m_SL1 = 18;
            result.m_SL2 = 1;
            result.m_SR1 = 11;
            result.m_SR2 = 1;
            result.m_MSK1 = 0xdfffffefU;
            result.m_MSK2 = 0xddfecb7fU;
            result.m_MSK3 = 0xbffaffffU;
            result.m_MSK4 = 0xbffffff6U;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0x00000000U;
            result.m_PARITY4 = 0x13c9e684U;
            result.m_PARITY4 = 0x20000000U;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 44497 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 44497 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT44497()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 44497;
            result.m_POS1 = 330;
            result.m_SL1 = 5;
            result.m_SL2 = 3;
            result.m_SR1 = 9;
            result.m_SR2 = 3;
            result.m_MSK1 = 0xeffffffbU;
            result.m_MSK2 = 0xdfbebfffU;
            result.m_MSK3 = 0xbfbf7befU;
            result.m_MSK4 = 0x9ffd7bffU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0xa3ac4000U;
            result.m_PARITY4 = 0xecc1327aU;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 86243 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 86243 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT86243()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 86243;
            result.m_POS1 = 366;
            result.m_SL1 = 6;
            result.m_SL2 = 7;
            result.m_SR1 = 19;
            result.m_SR2 = 1;
            result.m_MSK1 = 0xfdbffbffU;
            result.m_MSK2 = 0xbff7ff3fU;
            result.m_MSK3 = 0xfd77efffU;
            result.m_MSK4 = 0xbf9ff3ffU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0x00000000U;
            result.m_PARITY4 = 0xe9528d85U;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 132049 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 132049 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT132049()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 132049;
            result.m_POS1 = 110;
            result.m_SL1 = 19;
            result.m_SL2 = 1;
            result.m_SR1 = 21;
            result.m_SR2 = 1;
            result.m_MSK1 = 0xffffbb5fU;
            result.m_MSK2 = 0xfb6ebf95U;
            result.m_MSK3 = 0xfffefffaU;
            result.m_MSK4 = 0xcff77fffU;
            result.m_PARITY1 = 0x00000001U;
            result.m_PARITY2 = 0x00000000U;
            result.m_PARITY3 = 0xcb520000U;
            result.m_PARITY4 = 0xc7e91c7dU;
            return result;
        }

        /// <summary>
        /// Mersenne 指数 216091 のメルセンヌ・ツイスターパラメーターを取得します。
        /// </summary>
        /// <returns>
        /// Mersenne 指数 216091 に対応するメルセンヌ・ツイスターパラメーターです。
        /// </returns>
        public static MersenneTwisterDetail MT216091()
        {
            var result = new MersenneTwisterDetail();
            result.m_mexp = 216091;
            result.m_POS1 = 627;
            result.m_SL1 = 11;
            result.m_SL2 = 3;
            result.m_SR1 = 10;
            result.m_SR2 = 1;
            result.m_MSK1 = 0xbff7bff7U;
            result.m_MSK2 = 0xbfffffffU;
            result.m_MSK3 = 0xbffffa7fU;
            result.m_MSK4 = 0xffddfbfbU;
            result.m_PARITY1 = 0xf8000001U;
            result.m_PARITY2 = 0x89e80709U;
            result.m_PARITY3 = 0x3bd2b64bU;
            result.m_PARITY4 = 0x0c64b1e4U;
            return result;
        }

    }

}
