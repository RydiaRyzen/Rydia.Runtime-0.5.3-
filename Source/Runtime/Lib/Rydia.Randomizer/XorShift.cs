using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Randomizer
{
    /// <summary>
    /// Xorshiftの擬似乱数ジェネレーターを表すクラスです
    /// </summary>
    /// <seealso cref="Rydia.Randomizer.RandomGenerator" />
    public class XorShift : RandomGenerator
    {

        #region メンバ

        private uint[] m_Vector;

        public const uint DefaultSeed2 = 362436069;

        public const uint DefaultSeed3 = 521288629;

        public const uint DefaultSeed4 = 88675123;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 時間に依存する既定のシード値を使用し、<see cref="XorShift"/> classの新しいインスタンスを初期化します
        /// </summary>
        public XorShift()
            : this(Environment.TickCount)
        {

        }

        /// <summary>
        /// 指定したシード値を使用し、<see cref="XorShift"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        public XorShift(int seed)
            : this((uint)seed, DefaultSeed2, DefaultSeed3, DefaultSeed4)
        {

        }

        /// <summary>
        /// すべてのシードを指定して、<see cref="XorShift"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed1">擬似乱数系列の開始値を計算するために使用する数値。</param>
        /// <param name="seed2">擬似乱数系列の開始値を計算するために使用する数値。</param>
        /// <param name="seed3">擬似乱数系列の開始値を計算するために使用する数値。</param>
        /// <param name="seed4">擬似乱数系列の開始値を計算するために使用する数値。</param>
        public XorShift(uint seed1, uint seed2, uint seed3, uint seed4)
        {
            this.m_Vector = new uint[4];
            X = seed1;
            Y = seed2;
            Z = seed3;
            W = seed4;
        }

        #endregion

        #region 実装

        /// <summary>
        /// 符号なし32bit整数を生成します。
        /// </summary>
        /// <returns></returns>
        protected override uint GetGenerate()
        {
            uint t = (X ^ (X << 11));
            X = Y;
            Y = Z;
            Z = W;
            return (W = (W ^ (W >> 19)) ^ (t ^ (t >> 8)));
        }

        public override RandomGenerator ReGenerate()
        {
            return new XorShift(Environment.TickCount);
        }

        #endregion

        #region プロパティ

        protected uint X
        {
            get
            {
                return this.m_Vector[0];
            }
            set
            {
                this.m_Vector[0] = value;
            }
        }

        protected uint Y
        {
            get
            {
                return this.m_Vector[1];
            }
            set
            {
                this.m_Vector[1] = value;
            }
        }

        protected uint Z
        {
            get
            {
                return this.m_Vector[2];
            }
            set
            {
                this.m_Vector[2] = value;
            }
        }

        protected uint W
        {
            get
            {
                return this.m_Vector[3];
            }
            set
            {
                this.m_Vector[3] = value;
            }
        }

        #endregion

    }
}
