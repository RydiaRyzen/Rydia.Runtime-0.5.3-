using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Randomizer
{
    /// <summary>
    /// SFMT乱数ジェネレータを表すクラスです
    /// </summary>
    /// <seealso cref="Rio.Randomizer.RandomGenerator" />
    public class SimdOrientedFastMersenneTwister : RandomGenerator
    {

        #region メンバ

        /// <summary>
        /// 周期を表す指数を保持するフィールドです。
        /// </summary>
        protected int m_MEXP;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected int m_POS1;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected int m_SL1;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected int m_SL2;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected int m_SR1;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected int m_SR2;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected uint m_MSK1;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected uint m_MSK2;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected uint m_MSK3;
        /// <summary>
        /// MTを決定するパラメーターの一つ。
        /// </summary>
        protected uint m_MSK4;
        /// <summary>
        /// MTの周期を保証するための確認に用いるパラメーターの一つ。
        /// </summary>
        protected uint m_PARITY1;
        /// <summary>
        /// MTの周期を保証するための確認に用いるパラメーターの一つ。
        /// </summary>
        protected uint m_PARITY2;
        /// <summary>
        /// MTの周期を保証するための確認に用いるパラメーターの一つ。
        /// </summary>
        protected uint m_PARITY3;
        /// <summary>
        /// MTの周期を保証するための確認に用いるパラメーターの一つ。
        /// </summary>
        protected uint m_PARITY4;

        /// <summary>
        /// 各要素を128bitとしたときの内部状態ベクトルの個数。
        /// </summary>
        protected int N;
        /// <summary>
        /// 各要素を32bitとしたときの内部状態ベクトルの個数。
        /// </summary>
        protected int N32;
        /// <summary>
        /// 計算の高速化用。
        /// </summary>
        protected int SL2_x8;
        /// <summary>
        /// 計算の高速化用。
        /// </summary>
        protected int SR2_x8;
        /// <summary>
        /// 計算の高速化用。
        /// </summary>
        protected int SL2_ix8;
        /// <summary>
        /// 計算の高速化用。
        /// </summary>
        protected int SR2_ix8;

        /// <summary>
        /// 内部状態ベクトル。
        /// </summary>
        protected uint[] sfmt;
        /// <summary>
        /// 内部状態ベクトルのうち、次に乱数として使用するインデックス。
        /// </summary>
        protected int idx;

        private static Dictionary<MTPeriodType, Func<MersenneTwisterDetail>> s_Details = new Dictionary<MTPeriodType, Func<MersenneTwisterDetail>>();
        #endregion

        #region コンストラクタ

        static SimdOrientedFastMersenneTwister()
        {
            s_Details[MTPeriodType.MT607] = MersenneTwisterDetail.MT607;
            s_Details[MTPeriodType.MT1279] = MersenneTwisterDetail.MT1279;
            s_Details[MTPeriodType.MT2281] = MersenneTwisterDetail.MT2281;
            s_Details[MTPeriodType.MT4253] = MersenneTwisterDetail.MT4253;
            s_Details[MTPeriodType.MT11213] = MersenneTwisterDetail.MT11213;
            s_Details[MTPeriodType.MT19937] = MersenneTwisterDetail.MT19937;
            s_Details[MTPeriodType.MT44497] = MersenneTwisterDetail.MT44497;
            s_Details[MTPeriodType.MT86243] = MersenneTwisterDetail.MT86243;
            s_Details[MTPeriodType.MT132049] = MersenneTwisterDetail.MT132049;
            s_Details[MTPeriodType.MT216091] = MersenneTwisterDetail.MT216091;
        }

        /// <summary>
        /// 時間に依存する既定のシード値を使用し、(2^19937-1)周期の<see cref="SimdOrientedFastMersenneTwister"/> classの新しいインスタンスを初期化します
        /// </summary>
        public SimdOrientedFastMersenneTwister()
            : this(Environment.TickCount)
        {

        }

        /// <summary>
        /// 指定したシード値を使用し、(2^19937-1)周期の<see cref="SimdOrientedFastMersenneTwister"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        public SimdOrientedFastMersenneTwister(int seed)
            : this(seed, MTPeriodType.MT19937)
        {

        }

        /// <summary>
        /// 指定したシード値を使用し、指定されたフラグに基づく周期の<see cref="SimdOrientedFastMersenneTwister"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        /// <param name="period">周期を表すフラグ</param>
        public SimdOrientedFastMersenneTwister(int seed, MTPeriodType period)
        {
            if (!Enum.IsDefined(typeof(MTPeriodType), period))
                throw new ArgumentOutOfRangeException();

            MersenneTwisterDetail data = s_Details[period].Invoke();
            this.m_MEXP = data.m_mexp;
            this.m_POS1 = data.m_POS1;
            this.m_SL1 = data.m_SL1;
            this.m_SL2 = data.m_SL2;
            this.m_SR1 = data.m_SR1;
            this.m_SR2 = data.m_SR2;
            this.m_MSK1 = data.m_MSK1;
            this.m_MSK2 = data.m_MSK2;
            this.m_MSK3 = data.m_MSK3;
            this.m_MSK4 = data.m_MSK4;
            this.m_PARITY1 = data.m_PARITY1;
            this.m_PARITY2 = data.m_PARITY2;
            this.m_PARITY3 = data.m_PARITY3;
            this.m_PARITY4 = data.m_PARITY4;
            InitializeGenerateRand(seed);
        }

        #endregion

        #region 実装

        public override RandomGenerator ReGenerate()
        {
            return ReGenerate(MTPeriodType.MT19937);
        }

        public SimdOrientedFastMersenneTwister ReGenerate(int seed)
        {
            return ReGenerate(seed, MTPeriodType.MT19937);
        }

        public SimdOrientedFastMersenneTwister ReGenerate(MTPeriodType period)
        {
            int seed = (int)(DateTime.Now.Ticks % int.MaxValue);
            return ReGenerate(seed, period);
        }

        public SimdOrientedFastMersenneTwister ReGenerate(int seed, MTPeriodType period)
        {
            return new SimdOrientedFastMersenneTwister(seed, period);
        }

        /// <summary>
        /// 符号なし32bit整数を生成します。
        /// </summary>
        /// <returns></returns>
        protected override uint GetGenerate()
        {
            if (this.idx >= this.N32)
            {
                GenerateRandAll();
                this.idx = 0;
            }
            return this.sfmt[this.idx++];
        }

        /// <summary>
        /// ジェネレータを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        protected void InitializeGenerateRand(int seed)
        {
            this.N = this.m_MEXP / 128 + 1;
            this.N32 = this.N * 4;
            this.SL2_x8 = this.m_SL2 * 8;
            this.SR2_x8 = this.m_SR2 * 8;
            this.SL2_ix8 = 64 - this.SL2_x8;
            this.SR2_ix8 = 64 - this.SR2_x8;
            this.sfmt = new uint[this.N32];
            this.sfmt[0] = (uint)seed;
            int i;
            for (i = 1; i < this.N32; i++)
            {
                //this.sfmt[i] = (uint)(1812433253 * (this.sfmt[i - 1] ^ (this.sfmt[i - 1] >> 30)) + i);
                //SFMTの初期化としては意図した 32bit unsigned arithmetic を明確にした方が安全です。
                this.sfmt[i] = 1812433253U * (this.sfmt[i - 1] ^ (this.sfmt[i - 1] >> 30)) + (uint)i;
            }
            PeriodCertification();
            this.idx = this.N32;
        }

        /// <summary>
        /// 内部状態ベクトルが適切かどうかを判断し、調節します。
        /// </summary>
        protected void PeriodCertification()
        {
            uint[] parity = new uint[] { this.m_PARITY1, this.m_PARITY2, this.m_PARITY3, this.m_PARITY4 };
            uint inner = 0;
            uint work;
            int i;
            int j;
            for (i = 0; i < 4; i++)
                inner ^= this.sfmt[i] & parity[i];
            for (i = 16; i > 0; i >>= 1)
                inner ^= inner >> i;
            inner &= 1;
            if (inner == 1)
                return;
            for (i = 0; i < 4; i++)
            {
                work = 1;
                for (j = 0; j < 32; j++)
                {
                    if ((work & parity[i]) != 0)
                    {
                        this.sfmt[i] ^= work;
                        return;
                    }
                    work = work << 1;
                }
            }
        }

        /// <summary>
        /// 内部状態ベクトルを更新します。
        /// </summary>
        protected virtual void GenerateRandAll()
        {
            if (this.m_MEXP == 19937)
            {
                GenerateRandAll19937();
                return;
            }
            var a = 0;
            var b = this.m_POS1 * 4;
            var c = (this.N - 2) * 4;
            var d = (this.N - 1) * 4;
            ulong xh;
            ulong xl;
            ulong yh;
            ulong yl;
            do
            {
                xh = ((ulong)this.sfmt[a + 3] << 32) | this.sfmt[a + 2];
                xl = ((ulong)this.sfmt[a + 1] << 32) | this.sfmt[a + 0];
                yh = xh << (this.SL2_x8) | xl >> (this.SL2_ix8);
                yl = xl << (this.SL2_x8);
                xh = ((ulong)this.sfmt[c + 3] << 32) | this.sfmt[c + 2];
                xl = ((ulong)this.sfmt[c + 1] << 32) | this.sfmt[c + 0];
                yh ^= xh >> (this.SR2_x8);
                yl ^= xl >> (this.SR2_x8) | xh << (this.SR2_ix8);

                this.sfmt[a + 3] = this.sfmt[a + 3] ^ ((this.sfmt[b + 3] >> this.m_SR1) & this.m_MSK4) ^ (this.sfmt[d + 3] << this.m_SL1) ^ ((uint)(yh >> 32));
                this.sfmt[a + 2] = this.sfmt[a + 2] ^ ((this.sfmt[b + 2] >> this.m_SR1) & this.m_MSK3) ^ (this.sfmt[d + 2] << this.m_SL1) ^ ((uint)yh);
                this.sfmt[a + 1] = this.sfmt[a + 1] ^ ((this.sfmt[b + 1] >> this.m_SR1) & this.m_MSK2) ^ (this.sfmt[d + 1] << this.m_SL1) ^ ((uint)(yl >> 32));
                this.sfmt[a + 0] = this.sfmt[a + 0] ^ ((this.sfmt[b + 0] >> this.m_SR1) & this.m_MSK1) ^ (this.sfmt[d + 0] << this.m_SL1) ^ ((uint)yl);

                c = d;
                d = a;
                a += 4;
                b += 4;
                if (b >= this.N32)
                    b = 0;
            } while (a < this.N32);
        }

        /// <summary>
        /// 内部状態ベクトルを更新します。
        /// [(2^19937-1)周期用]
        /// </summary>
        private void GenerateRandAll19937()
        {
            const int cMEXP = 19937;
            const int cPOS1 = 122;
            const uint cMSK1 = 0xdfffffefU;
            const uint cMSK2 = 0xddfecb7fU;
            const uint cMSK3 = 0xbffaffffU;
            const uint cMSK4 = 0xbffffff6U;
            const int cSL1 = 18;
            const int cSR1 = 11;
            const int cN = cMEXP / 128 + 1;
            const int cN32 = cN * 4;
            var a = 0;
            var b = cPOS1 * 4;
            var c = (cN - 2) * 4;
            var d = (cN - 1) * 4;
            uint[] p = this.sfmt;
            do
            {
                p[a + 3] = p[a + 3] ^ (p[a + 3] << 8) ^ (p[a + 2] >> 24) ^ (p[c + 3] >> 8) ^ ((p[b + 3] >> cSR1) & cMSK4) ^ (p[d + 3] << cSL1);
                p[a + 2] = p[a + 2] ^ (p[a + 2] << 8) ^ (p[a + 1] >> 24) ^ (p[c + 3] << 24) ^ (p[c + 2] >> 8) ^ ((p[b + 2] >> cSR1) & cMSK3) ^ (p[d + 2] << cSL1);
                p[a + 1] = p[a + 1] ^ (p[a + 1] << 8) ^ (p[a + 0] >> 24) ^ (p[c + 2] << 24) ^ (p[c + 1] >> 8) ^ ((p[b + 1] >> cSR1) & cMSK2) ^ (p[d + 1] << cSL1);
                p[a + 0] = p[a + 0] ^ (p[a + 0] << 8) ^ (p[c + 1] << 24) ^ (p[c + 0] >> 8) ^ ((p[b + 0] >> cSR1) & cMSK1) ^ (p[d + 0] << cSL1);
                c = d; d = a; a += 4; b += 4;
                if (b >= cN32) b = 0;
            } while (a < cN32);
        }

        #endregion

        #region プロパティ

        #endregion

    }

}
