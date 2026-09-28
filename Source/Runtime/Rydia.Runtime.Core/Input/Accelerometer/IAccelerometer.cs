using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Input
{

    /// <summary>
    /// 加速度センサーから取得した加速度値を提供するインターフェースです。
    /// </summary>
    public interface IAccelerometer : IDisposableEx
    {

        /// <summary>
        /// X軸方向の加速度を取得します。
        /// </summary>
        /// <value>
        /// X軸方向の加速度値です。
        /// </value>
        float AccelX
        {
            get;
        }

        /// <summary>
        /// Y軸方向の加速度を取得します。
        /// </summary>
        /// <value>
        /// Y軸方向の加速度値です。
        /// </value>
        float AccelY
        {
            get;
        }

        /// <summary>
        /// Z軸方向の加速度を取得します。
        /// </summary>
        /// <value>
        /// Z軸方向の加速度値です。
        /// </value>
        float AccelZ
        {
            get;
        }

    }

}
