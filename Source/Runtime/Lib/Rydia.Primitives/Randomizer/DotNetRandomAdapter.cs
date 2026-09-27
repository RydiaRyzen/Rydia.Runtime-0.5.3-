using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Randomizer
{

    /// <summary>
    /// <see cref="RandomGenerator"/> を <see cref="System.Random"/> と互換性のある
    /// 乱数ジェネレーターとして使用するためのアダプタークラスです。
    /// </summary>
    /// <seealso cref="System.Random"/>
    public class DotNetRandomAdapter : System.Random
    {

        /// <summary>
        /// 乱数ジェネレーターを取得または設定します。
        /// </summary>
        private RandomGenerator m_Orign;

        /// <summary>
        /// <see cref="Sample"/> の計算に使用する係数です。
        /// </summary>
        private const double SampleValue = 4.6566128752457969E-10;


        #region コンストラクタ

        /// <summary>
        /// 指定された乱数ジェネレーターを使用して、
        /// <see cref="DotNetRandomAdapter"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="random">使用する乱数ジェネレーターです。</param>
        public DotNetRandomAdapter(RandomGenerator random)
        {
            this.m_Orign = random;
        }

        #endregion

        #region 実装

        /// <summary>
        /// 乱数ジェネレーターを再生成し、自身を返します。
        /// </summary>
        /// <returns>再生成された乱数ジェネレーターを保持する自身のインスタンスです。</returns>
        public DotNetRandomAdapter ReGenerate()
        {
            this.m_Orign = this.m_Orign.ReGenerate();
            return this;
        }

        /// <summary>
        /// 0 以上のランダムな整数を返します。
        /// </summary>
        /// <returns>
        /// 0 以上 <see cref="int.MaxValue"/> 未満の 32 ビット符号付き整数です。
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
        /// 指定された最大値未満のランダムな整数を返します。
        /// </summary>
        /// <param name="maxValue">
        /// 生成される乱数の排他的上限値です。
        /// 0 以上である必要があります。
        /// </param>
        /// <returns>
        /// 0 以上 <paramref name="maxValue"/> 未満の 32 ビット符号付き整数です。
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue"/> が 0 未満です。
        /// </exception>
        public override int Next(int maxValue)
        {
            return base.Next(maxValue);
        }

        /// <summary>
        /// 指定された範囲内のランダムな整数を返します。
        /// </summary>
        /// <param name="minValue">
        /// 返される乱数の包括的な下限値です。
        /// </param>
        /// <param name="maxValue">
        /// 返される乱数の排他的な上限値です。
        /// <paramref name="maxValue"/> は <paramref name="minValue"/> 以上である必要があります。
        /// </param>
        /// <returns>
        /// <paramref name="minValue"/> 以上 <paramref name="maxValue"/> 未満の
        /// 32 ビット符号付き整数です。
        /// <paramref name="minValue"/> と <paramref name="maxValue"/> が等しい場合は、
        /// <paramref name="minValue"/> を返します。
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minValue"/> が <paramref name="maxValue"/> より大きい値です。
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
        /// 指定されたバイト配列の各要素に乱数を書き込みます。
        /// </summary>
        /// <param name="buffer">乱数を書き込むバイト配列です。</param>
        public override void NextBytes(byte[] buffer)
        {
            this.m_Orign.NextBytes(buffer);
        }

        /// <summary>
        /// 0.0 以上 1.0 未満のランダムな浮動小数点数を返します。
        /// </summary>
        /// <returns>
        /// 0.0 以上 1.0 未満の倍精度浮動小数点数です。
        /// </returns>
        public override double NextDouble()
        {
            return this.m_Orign.NextDouble();
        }

        /// <summary>
        /// 0.0 以上 1.0 未満のランダムな浮動小数点数を生成します。
        /// </summary>
        /// <returns>
        /// 0.0 以上 1.0 未満の倍精度浮動小数点数です。
        /// </returns>
        protected override double Sample()
        {
            return Next() * SampleValue;
        }

        /// <summary>
        /// このインスタンスを表す文字列を返します。
        /// </summary>
        /// <returns>
        /// 内部で使用している乱数ジェネレーターの完全修飾型名です。
        /// </returns>
        public override string ToString()
        {
            return this.m_Orign.GetType().FullName;
        }

        #endregion

    }

}
