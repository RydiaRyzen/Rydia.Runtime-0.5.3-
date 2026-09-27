using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Randomizer
{

    /// <summary>
    /// メルセンヌツイスターで用いる周期をあらわす列挙型。
    /// </summary>
    public enum MTPeriodType
    {
        /// <summary>
        /// 2^607-1周期のメルセンヌツイスター。
        /// </summary>
        MT607 = 607,
        /// <summary>
        /// 2^1279-1周期のメルセンヌツイスター。
        /// </summary>
        MT1279 = 1279,
        /// <summary>
        /// 2^2281-1周期のメルセンヌツイスター。
        /// </summary>
        MT2281 = 2281,
        /// <summary>
        /// 2^4253-1周期のメルセンヌツイスター。
        /// </summary>
        MT4253 = 4253,
        /// <summary>
        /// 2^11213-1周期のメルセンヌツイスター。
        /// </summary>
        MT11213 = 11213,
        /// <summary>
        /// 2^19937-1周期のメルセンヌツイスター。
        /// </summary>
        MT19937 = 19937,
        /// <summary>
        /// 2^44497-1周期のメルセンヌツイスター。
        /// </summary>
        MT44497 = 44497,
        /// <summary>
        /// 2^86243-1周期のメルセンヌツイスター。
        /// </summary>
        MT86243 = 86243,
        /// <summary>
        /// 2^132049-1周期のメルセンヌツイスター。
        /// </summary>
        MT132049 = 132049,
        /// <summary>
        /// 2^216091-1周期のメルセンヌツイスター。
        /// </summary>
        MT216091 = 216091
    }

}
