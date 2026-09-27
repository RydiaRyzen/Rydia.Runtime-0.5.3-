using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Randomizer
{

    /// <summary>
    /// すべての疑似乱数クラスの基底クラスです。
    /// </summary>
    public abstract class RandomGenerator
    {

        #region 実装

        /// <summary>
        /// <see cref="System.Random"/>互換の<see cref="DotNetRandomAdapter"/>を通して<see cref="System.Random"/>へ変換して返します
        /// </summary>
        /// <returns></returns>
        public System.Random ToDotNetRandomAdapter()
        {
            return new DotNetRandomAdapter(this);
        }

        /// <summary>
        /// 符号なし32bit整数を生成します。
        /// 派生クラスでオーバーライドする必要があります。
        /// </summary>
        /// <returns></returns>
        protected abstract uint GetGenerate();

        /// <summary>
        /// 符号なし32bitの擬似乱数を生成して返します
        /// </summary>
        /// <returns>
        /// 0 以上で<see cref="uint.MaxValue"/>より小さい 32 ビット符号なし整数。
        /// </returns>
        public uint NextUInt32()
        {
            return GetGenerate();
        }

        /// <summary>
        /// 符号あり32bitの擬似乱数を生成して返します
        /// </summary>
        /// <returns>
        /// 0 以上で<see cref="int.MaxValue"/>より小さい 32 ビット符号付き整数。
        /// </returns>
        public int NextInt32()
        {
            int result = (int)GetGenerate();
            return Math.Abs(result);
        }

        public abstract RandomGenerator ReGenerate();

        /// <summary>
        /// 符号なし64bitの擬似乱数を取得します。
        /// </summary>
        public virtual ulong NextUInt64()
        {
            ulong result = ((ulong)NextUInt32() << 32) | NextUInt32();
            return result;
        }

        /// <summary>
        /// 符号あり64bitの擬似乱数を取得します。
        /// </summary>
        public virtual long NextInt64()
        {
            long result = (long)NextUInt64();
            return Math.Abs(result);
        }

        /// <summary>
        /// 指定したバイト配列の要素に乱数を格納します。
        /// </summary>
        /// <param name="buffer">乱数を格納するバイト配列。</param>
        public virtual void NextBytes(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException("buffer は null です。");
            int i = 0;
            int ii = 0;
            uint r;
            while (i + 4 <= buffer.Length)
            {
                r = NextUInt32();
                for (ii = 0; ii < 4; ii++)
                {
                    buffer[i++] = NextBytes(ii, r);
                }
            }
            if (i >= buffer.Length)
                return;
            r = NextUInt32();
            for (ii = 0; ii < 3; ii++)
            {
                if (i + ii >= buffer.Length)
                    return;
                buffer[i + ii] = NextBytes(ii, r);
            }
        }

        private byte NextBytes(int index, uint r)
        {
            if (index < 0 || index > 3)
                throw new ArgumentOutOfRangeException();
            switch (index)
            {
                case 1:
                    return (byte)(r >> 8);
                case 2:
                    return (byte)(r >> 16);
                case 3:
                    return (byte)(r >> 24);
            }
            return (byte)r;
        }

        /// <summary>
        /// 0.0 以上 1.0 未満のランダムな浮動小数点数を返します。
        /// [0,1]を<see cref="UInt32.MaxValue"/>個に均等にわけ、そのうち一つを返します。
        /// <see cref="GetGenerate()"/>を1回呼び出します。
        /// </summary>
        public virtual double NextDouble()
        {
            var r1 = GetGenerate();
            double result = r1 * (1.0 / UInt32.MaxValue);
            if (result >= 0.0 && result <= 1.0)
                return result;
            if (result > 1.0)
                return 1.0;
            return 0.0;
        }

        #endregion

    }

}
