using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Randomizer
{

    /// <summary>
    /// <see cref="RandomGenerator"/>を継承する乱数ジェネレータークラスを<see cref="System.Random"/>クラスと、
    /// 互換性能を維持するために必要なアダプター機能を提供するクラスです
    /// </summary>
    /// <seealso cref="System.Random" />
    public class DotNetRandomAdapter : System.Random
    {
        /// <summary>
        /// 乱数ジェネレータを保持するフィールドです
        /// </summary>
        private RandomGenerator m_Orign;

        /// <summary>
        /// サンプル算出係数を表す定数
        /// </summary>
        private const double SampleValue = 4.6566128752457969E-10;


        #region コンストラクタ

        /// <summary>
        ///   <see cref="DotNetRandomAdapter"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="random">乱数ジェネレータのポインタ</param>
        public DotNetRandomAdapter(RandomGenerator random)
        {
            this.m_Orign = random;
        }

        #endregion

        #region 実装

        public DotNetRandomAdapter ReGenerate()
        {
            this.m_Orign = this.m_Orign.ReGenerate();
            return this;
        }

        /// <summary>
        /// 0 以上のランダムな整数を返します。
        /// </summary>
        /// <returns>
        /// 0 以上で MaxValue より小さい 32 ビット符号付き整数。
        /// </returns>
        public override int Next()
        {
            uint rnd;
            do
                rnd = this.m_Orign.NextUInt32() & 0x7FFFFFFF;
            while (rnd == int.MaxValue);
            return (int)rnd;
        }

        /// <summary>
        /// 指定した最大値より小さい 0 以上のランダムな整数を返します。
        /// </summary>
        /// <param name="maxValue">生成される乱数の排他的上限値。 maxValue は、0 以上である必要があります。</param>
        /// <returns>
        /// 0 以上で maxValue 未満の 32 ビット符号付き整数。
        /// つまり、戻り値の範囲に 0 は含まれますが、maxValue は含まれません。
        /// ただし、maxValue が 0 の場合は、maxValue が返されます。
        /// </returns>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">maxValueが0未満です。</exception>
        public override int Next(int maxValue)
        {
            return base.Next(maxValue);
        }

        /// <summary>
        /// 指定した範囲内のランダムな整数を返します。
        /// </summary>
        /// <param name="minValue">返される乱数の包括的下限値。</param>
        /// <param name="maxValue">返される乱数の排他的上限値。 maxValue は、minValue 以上である必要があります。</param>
        /// <returns>
        /// minValue 以上で maxValue 未満の 32 ビット符号付き整数。
        /// つまり、戻り値の範囲に maxValue は含まれますが minValue は含まれません。
        /// minValue が maxValue に等しい場合は、minValue が返されます。
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// maxValueが0未満です。
        /// or
        /// minValueがMaxValueより大きい値です。
        /// </exception>
        public override int Next(int minValue, int maxValue)
        {
            if (maxValue < 0)
                throw new ArgumentOutOfRangeException("maxValueが0未満です。");
            if (minValue > maxValue)
                throw new ArgumentOutOfRangeException("minValueがMaxValueより大きい値です。");
            if (minValue == maxValue)
                return minValue;
            uint range = (uint)((long)maxValue - minValue);
            uint residue = (uint.MaxValue - range + 1) % range;
            uint r;
            do
                r = this.m_Orign.NextUInt32();
            while (r < residue);
            return (int)((long)((r - residue) % range) + minValue);
        }

        /// <summary>
        /// 指定したバイト配列の要素に乱数を格納します。
        /// </summary>
        /// <param name="buffer">乱数を格納するバイト配列。</param>
        public override void NextBytes(byte[] buffer)
        {
            this.m_Orign.NextBytes(buffer);
        }

        /// <summary>
        /// 0.0 以上 1.0 未満のランダムな浮動小数点数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        public override double NextDouble()
        {
            return this.m_Orign.NextDouble();
        }

        /// <summary>
        /// 0.0 と 1.0 の間のランダムな浮動小数点数を返します。
        /// </summary>
        /// <returns>0.0 以上 1.0 未満の倍精度浮動小数点数。</returns>
        protected override double Sample()
        {
            return Next() * SampleValue;
        }


        public override string ToString()
        {
            return this.m_Orign.GetType().FullName;
        }

        #endregion

    }

}
