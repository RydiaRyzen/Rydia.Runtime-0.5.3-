using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Randomizer
{
    /// <summary>
    /// MersenneTwisterの擬似乱数ジェネレーターを表すクラスです
    /// </summary>
    /// <seealso cref="Rydia.Randomizer.RandomGenerator" />
    public class MersenneTwister : RandomGenerator
    {

        #region メンバ

        /// <summary>
        /// 内部状態ベクトルの総数を表す定数値
        /// </summary>
        private const int N = 624;

        /// <summary>
        /// MTを決定するパラメータMを保持する定数値
        /// </summary>
        private const int M = 397;

        /// <summary>
        /// MTを決定するパラメータMatrixAを保持する定数値
        /// </summary>
        protected const uint MatrixA = 0x9908b0dfU;

        /// <summary>
        /// MTを決定するパラメータUpperMaskを保持する定数値
        /// </summary>
        protected const uint UpperMask = 0x80000000U;

        /// <summary>
        /// MTを決定するパラメータを保持する定数値
        /// </summary>
        protected const uint LowerMask = 0x7fffffffU;

        /// <summary>
        /// MTを決定するパラメータTemper1を保持する定数値
        /// </summary>
        protected const uint Temper1 = 0x9d2c5680U;

        /// <summary>
        /// MTを決定するパラメータTemper2を保持する定数値
        /// </summary>
        protected const uint Temper2 = 0xefc60000U;

        /// <summary>
        /// MTを決定するパラメータTemper3を保持する定数値
        /// </summary>
        protected const int Temper3 = 11;

        /// <summary>
        /// MTを決定するパラメータTemper4を保持する定数値
        /// </summary>
        protected const int Temper4 = 7;

        /// <summary>
        /// MTを決定するパラメータTemper5を保持する定数値
        /// </summary>
        protected const int Temper5 = 15;

        /// <summary>
        /// MTを決定するパラメータTemper6を保持する定数値
        /// </summary>
        protected const int Temper6 = 18;

        /// <summary>
        /// 内部状態ベクトルの配列を保持するフィールド
        /// </summary>
        protected uint[] m_MersenneTwister;

        /// <summary>
        /// 内部状態ベクトルのうち、次に乱数として使用するインデックスのフィールド
        /// </summary>
        protected int m_MersenneTwisterIndex;

        private uint[] m_Mag01;

        #endregion

        #region コンストラクタおよび初期化

        /// <summary>
        /// 時間に依存する既定のシード値を使用し、<see cref="MersenneTwister"/> classの新しいインスタンスを初期化します
        /// </summary>
        public MersenneTwister()
            : this(Environment.TickCount)
        {

        }

        /// <summary>
        /// 指定したシード値を使用して<see cref="MersenneTwister"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        public MersenneTwister(int seed)
        {
            this.m_MersenneTwister = new uint[N];
            this.m_MersenneTwisterIndex = N + 1;
            this.m_Mag01 = new uint[] { 0x0U, MatrixA };
            Initialize(seed);
        }

        /// <summary>
        /// 内部状態配列の初期化を実行します
        /// </summary>
        /// <param name="seed"></param>
        private void Initialize(int seed)
        {
            this.m_MersenneTwister[0] = (uint)seed;
            for (int i = 1; i < N; i++)
            {
                this.m_MersenneTwister[i] = (uint)(1812433253 * (this.m_MersenneTwister[i - 1] ^ (this.m_MersenneTwister[i - 1] >> 30)) + i);
            }
        }


        #endregion

        public override RandomGenerator ReGenerate()
        {
            return new MersenneTwister();
        }

        #region 実装

        /// <summary>
        /// 符号なし32bit整数を生成します。
        /// </summary>
        /// <returns></returns>
        protected override uint GetGenerate()
        {
            if (this.m_MersenneTwisterIndex >= N)
            {
                GenerateRandomAll();
                this.m_MersenneTwisterIndex = 0;
            }
            uint result = this.m_MersenneTwister[this.m_MersenneTwisterIndex++];
            result ^= (result >> Temper3);
            result ^= (result << Temper4) & Temper1;
            result ^= (result << Temper5) & Temper2;
            result ^= (result >> Temper6);
            return result;
        }

        /// <summary>
        /// 内部状態ベクトルを更新します。
        /// </summary>
        protected void GenerateRandomAll()
        {
            int kk = 1;
            uint y = this.m_MersenneTwister[0] & UpperMask;
            uint p;
            do
            {
                p = this.m_MersenneTwister[kk];
                this.m_MersenneTwister[kk - 1] = this.m_MersenneTwister[kk + (M - 1)] ^ ((y | (p & LowerMask)) >> 1) ^ this.m_Mag01[p & 1];
                y = p & UpperMask;
            } while (++kk < N - M + 1);
            do
            {
                p = this.m_MersenneTwister[kk];
                this.m_MersenneTwister[kk - 1] = this.m_MersenneTwister[kk + (M - N - 1)] ^ ((y | (p & LowerMask)) >> 1) ^ this.m_Mag01[p & 1];
                y = p & UpperMask;
            } while (++kk < N);
            p = this.m_MersenneTwister[0];
            this.m_MersenneTwister[N - 1] = this.m_MersenneTwister[M - 1] ^ ((y | (p & LowerMask)) >> 1) ^ this.m_Mag01[p & 1];
        }

        #endregion


    }
}
