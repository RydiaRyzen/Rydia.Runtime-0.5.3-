using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Randomizer
{
    /// <summary>
    /// Wellの擬似乱数ジェネレーターを表すクラスです
    /// </summary>
    /// <seealso cref="Rydia.Randomizer.RandomGenerator" />
    public class Well : RandomGenerator
    {

        #region メンバ
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const int W = 32;
        /// <summary>
        /// 内部状態ベクトルの個数。
        /// </summary>
        protected const int R = 624;
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const int P = 31;
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const uint MASKU = (0xffffffffU >> (W - P));
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const uint MASKL = (~MASKU);
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const int M1 = 70;
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const int M2 = 179;
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const int M3 = 449;
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const uint TEMPERB = 0x12345678U;
        /// <summary>
        /// Wellのパラメーターの一つ。
        /// </summary>
        protected const uint TEMPERC = 0x87654321U;

        /// <summary>
        /// 内部状態ベクトル。
        /// </summary>
        protected uint[] state;
        /// <summary>
        /// 内部状態ベクトルのうち、次に乱数として使用するインデックス。
        /// </summary>
        protected int state_i;
        #endregion

        #region コンストラクタ

        /// <summary>
        /// 時間に依存する既定のシード値を使用し、<see cref="Well"/> classの新しいインスタンスを初期化します
        /// </summary>
        public Well()
            : this(Environment.TickCount)
        {


        }

        /// <summary>
        /// 指定したシード値を使用し、<see cref="Well"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        public Well(int seed)
        {
            this.state = new uint[R];
            this.state_i = 0;
            //内部状態配列初期化
            this.state[0] = (uint)seed;
            for (int i = 1; i < R; i++)
                this.state[i] = (uint)(1812433253 * (this.state[i - 1] ^ (this.state[i - 1] >> 30)) + i);
        }

        public override RandomGenerator ReGenerate()
        {
            return new Well(Environment.TickCount);
        }

        #endregion

        #region 実装

        /// <summary>
        /// 符号なし32bit整数を生成します。
        /// </summary>
        /// <returns></returns>
        protected override uint GetGenerate()
        {
            uint z1, z2;
            int i1, i2, i3, i4, i5;
            if (this.state_i < 2)
            {
                if (this.state_i < 1)
                    i2 = this.state_i - 1 + R;
                else
                    i2 = this.state_i - 1;
                i1 = this.state_i - 2 + R;
            }
            else
            {
                i1 = this.state_i - 2;
                i2 = this.state_i - 1;
            }
            if (this.state_i + M3 >= R)
            {
                if (this.state_i + M2 >= R)
                {
                    if (this.state_i + M1 >= R)
                        i3 = this.state_i + M1 - R;
                    else
                        i3 = this.state_i + M1;
                    i4 = this.state_i + M2 - R;
                }
                else
                {
                    i3 = this.state_i + M1;
                    i4 = this.state_i + M2;
                }
                i5 = this.state_i + M3 - R;
            }
            else
            {
                i3 = this.state_i + M1;
                i4 = this.state_i + M2;
                i5 = this.state_i + M3;
            }
            z1 = (this.state[this.state_i] ^ (this.state[this.state_i] << 25)) ^ (this.state[i3] ^ (this.state[i3] >> 27));
            z2 = (this.state[i4] >> 9) ^ (this.state[i5] ^ (this.state[i5] >> 1));
            this.state[this.state_i] = z1 ^ z2;
            z1 = (this.state[i2] & MASKL) ^ (this.state[i1] & MASKU) ^ (z1 << 9) ^ (z2 << 21) ^ (this.state[this.state_i] >> 21);
            this.state_i = i2;
            this.state[i2] = z1;
            z1 = z1 ^ ((z1 << 7) & TEMPERB);
            z1 = z1 ^ ((z1 << 15) & TEMPERC);
            return z1;
            //以下、直訳。
            //uint z0, z1, z2;
            //uint y;
            //if (state_i == 0) {
            //    z0 = (state[state_i + R - 1] & MASKL) | (state[state_i + R - 2] & MASKU);
            //    z1 = (state[state_i] ^ (state[state_i] << (25))) ^ (state[state_i + M1] ^ (state[state_i + M1] >> 27));
            //    z2 = (state[state_i + M2] >> 9) ^ (state[state_i + M3] ^ (state[state_i + M3] >> 1));
            //    state[state_i] = z1 ^ z2;
            //    state[state_i - 1 + R] = (z0) ^ (z1 ^ (z1 << (9))) ^ (z2 ^ (z2 << (21))) ^ (state[state_i] ^ (state[state_i] >> 21));
            //} else if (state_i == 1) {
            //    z0 = (state[state_i - 1] & MASKL) | (state[state_i + R - 2] & MASKU);
            //    z1 = (state[state_i] ^ (state[state_i] << (25))) ^ (state[state_i + M1] ^ (state[state_i + M1] >> 27));
            //    z2 = (state[state_i + M2] >> 9) ^ (state[state_i + M3] ^ (state[state_i + M3] >> 1));
            //    state[state_i] = z1 ^ z2;
            //    state[state_i - 1] = (z0) ^ (z1 ^ (z1 << (9))) ^ (z2 ^ (z2 << (21))) ^ (state[state_i] ^ (state[state_i] >> 21));
            //} else if (state_i + M1 >= R) {
            //    z0 = (state[state_i - 1] & MASKL) | (state[state_i - 2] & MASKU);
            //    z1 = (state[state_i] ^ (state[state_i] << (25))) ^ (state[state_i + M1 - R] ^ (state[state_i + M1 - R] >> 27));
            //    z2 = (state[state_i + M2 - R] >> 9) ^ (state[state_i + M3 - R] ^ (state[state_i + M3 - R] >> 1));
            //    state[state_i] = z1 ^ z2;
            //    state[state_i - 1] = (z0) ^ (z1 ^ (z1 << (9))) ^ (z2 ^ (z2 << (21))) ^ (state[state_i] ^ (state[state_i] >> 21));
            //} else if (state_i + M2 >= R) {
            //    z0 = (state[state_i - 1] & MASKL) | (state[state_i - 2] & MASKU);
            //    z1 = (state[state_i] ^ (state[state_i] << (25))) ^ (state[state_i + M1] ^ (state[state_i + M1] >> 27));
            //    z2 = (state[state_i + M2 - R] >> 9) ^ (state[state_i + M3 - R] ^ (state[state_i + M3 - R] >> 1));
            //    state[state_i] = z1 ^ z2;
            //    state[state_i - 1] = (z0) ^ (z1 ^ (z1 << (9))) ^ (z2 ^ (z2 << (21))) ^ (state[state_i] ^ (state[state_i] >> 21));
            //} else if (state_i + M3 >= R) {
            //    z0 = (state[state_i - 1] & MASKL) | (state[state_i - 2] & MASKU);
            //    z1 = (state[state_i] ^ (state[state_i] << (25))) ^ (state[state_i + M1] ^ (state[state_i + M1] >> 27));
            //    z2 = (state[state_i + M2] >> 9) ^ (state[state_i + M3 - R] ^ (state[state_i + M3 - R] >> 1));
            //    state[state_i] = z1 ^ z2;
            //    state[state_i - 1] = (z0) ^ (z1 ^ (z1 << (9))) ^ (z2 ^ (z2 << (21))) ^ (state[state_i] ^ (state[state_i] >> 21));
            //} else {
            //    z0 = (state[state_i - 1] & MASKL) | (state[state_i - 2] & MASKU);
            //    z1 = (state[state_i] ^ (state[state_i] << (25))) ^ (state[state_i + M1] ^ (state[state_i + M1] >> 27));
            //    z2 = (state[state_i + M2] >> 9) ^ (state[state_i + M3] ^ (state[state_i + M3] >> 1));
            //    state[state_i] = z1 ^ z2;
            //    state[state_i - 1] = (z0) ^ (z1 ^ (z1 << (9))) ^ (z2 ^ (z2 << (21))) ^ (state[state_i] ^ (state[state_i] >> 21));
            //}
            //state_i--;
            //if (state_i == -1) state_i = R - 1;
            //y = state[state_i] ^ ((state[state_i] << 7) & TEMPERB);
            //y = y ^ ((y << 15) & TEMPERC);
            //return y;
        }

        #endregion

        #region プロパティ

        #endregion

    }
}
