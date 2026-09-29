using Rydia.Randomizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Randomizer
{
    /// <summary>
    /// 線形合同法(LCG)による擬似乱数ジェネレーターを表すクラスです
    /// 詳細はremarksを参照してください
    /// </summary>
    /// <remarks>
    /// 長所:
    /// ほとんど記憶領域を必要としない。実用的な擬似乱数アルゴリズムでは最少である。
    /// 低機能なプロセッサ上でも極めて高速に実装できる。
    /// 素朴には乗算と除算が必要に見えるが、
    /// (1)定数による乗算なのでシフトと加減算の組合せにできる
    /// (2)除算そのものが必要なのではなく剰余が得られれば良いので合同算術による式変形が可能、
    /// 等の理由から効率的な式に変形できる。
    /// そのため、専用回路化すら容易である（ただし、適切なM系列をLFSRで実装したほうがより効率良く乱数としての質も良い）。
    /// 問題点は多いが、どのような問題があるか、どうやって回避すればいいかが十分に研究されている。
    /// 短所:
    /// 暗号論的擬似乱数生成器ではなく、そのまま暗号に使用してはならない。
    /// 線形合同法一般の欠点に、多次元で疎に分布するという性質がある。
    /// 線形合同法による乱数列r1, r2, r3, r4, ... から(r1, r2), (r3, r4), ... のように順番に割り当ててプロットすると、
    /// 一定の間隔の格子点状に点が並んでしまう。
    /// 科学技術シミュレーションで3次元の点の位置などを生成する場合に問題となる。
    /// 下位ビットのランダム性が低い。
    /// Mの値によっては、最下位に近いビットは完全にランダムでなく規則性を持っていることすらある。
    /// たとえばMが偶数の場合（コンピュータでの実装が楽であるために、Mに2の冪を採用した場合はこれに当たる）、
    /// 最下位ビットは、同じものが出続けるか、0と1が交互にでるかのどちらかである。
    /// すなわち、偶数ばかりが出る、奇数ばかりが出る、偶数と奇数が交互に出る、のどれかになるということである（最大周期ならば偶数と奇数が交互に出る）。
    /// 大量に乱数列を消費する科学技術シミュレーションなどでは、周期の短さ（たとえば32ビットでは最大周期でも約40億）が問題になる。
    /// プログラミング言語処理系に附属するライブラリの乱数列生成器（たとえばrand(3)やjava.util.Randomなど）が、
    /// 線形合同法を利用している場合があるため、
    /// たとえばサイコロの目を生成する場合はrand() % 6 + 1としてはならない。
    /// 前述のように周期2で偶数と奇数が循環するような場合、その規則性がそのまま顕れてしまう。
    /// rand() / (RAND_MAX / 6 + 1) + 1のようにすればランダムになる
    /// （注。このコードは考え方を示すものであり、厳密に1/6の確率になるものではない）。
    /// </remarks>
    public class LinearCongruentialGenerator : RandomGenerator
    {

        #region メンバ

        /// <summary>
        /// LOGパラメータAを保持するフィールド
        /// </summary>
        protected uint m_ParamA;

        /// <summary>
        /// LOGパラメータCを保持するフィールド
        /// </summary>
        protected uint m_ParamC;

        /// <summary>
        /// 内部状態を保持するフィールド
        /// </summary>
        protected uint m_Seed;

        /// <summary>
        /// LOGパラメータAの既定値を表す定数
        /// </summary>
        internal const uint DefaultParamA = 1664525;
        /// <summary>
        /// LOGパラメータCの既定値を表す定数
        /// </summary>
        internal const uint DefaultParamC = 1013904223;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// 時間に依存する既定のシード値を使用し、<see cref="LinearCongruentialGenerator"/> classの新しいインスタンスを初期化します
        /// </summary>
        public LinearCongruentialGenerator()
            : this(Environment.TickCount)
        {

        }

        /// <summary>
        /// 指定したシード値を使用して<see cref="LinearCongruentialGenerator"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        public LinearCongruentialGenerator(int seed)
            : this(seed, DefaultParamA, DefaultParamC)
        {

        }

        /// <summary>
        /// 指定したパラメータを使用して<see cref="LinearCongruentialGenerator"/> classの新しいインスタンスを初期化します
        /// </summary>
        /// <param name="seed">擬似乱数系列の開始値を計算するために使用する数値。負数を指定した場合、その数値の絶対値が使用されます。</param>
        /// <param name="paramA">LCGパラメータAを表す数値</param>
        /// <param name="paramC">LCGパラメータCを表す数値</param>
        public LinearCongruentialGenerator(int seed, uint paramA, uint paramC)
        {
            this.m_ParamA = paramA;
            this.m_ParamC = paramC;
            this.m_Seed = (uint)seed;
        }

        #endregion

        public override RandomGenerator ReGenerate()
        {
            return ReGenerate(Environment.TickCount);
        }

        public RandomGenerator ReGenerate(int seed)
        {
            return ReGenerate(seed, DefaultParamA, DefaultParamC);
        }

        public RandomGenerator ReGenerate(int seed, uint paramA, uint paramC)
        {
            return new LinearCongruentialGenerator(seed, paramA, paramC);
        }

        /// <summary>
        /// 符号なし32bit整数を生成します。
        /// </summary>
        /// <returns></returns>
		protected override uint GetGenerate()
		{
            this.m_Seed = (uint)((ulong)this.m_Seed * this.m_ParamA + this.m_ParamC);
			return this.m_Seed;
		}

    }
}
