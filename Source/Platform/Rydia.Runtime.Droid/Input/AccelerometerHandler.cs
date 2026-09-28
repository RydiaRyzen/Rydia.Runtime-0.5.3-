using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Hardware;
using Android.Runtime;
using Rydia.Input;

namespace Rydia.Input
{

    /// <summary>
    /// Androidの加速度センサーから加速度値を取得するためのハンドラーです。
    /// </summary>
    /// <remarks>
    /// <para>
    /// Androidの<see cref="SensorType.Accelerometer"/>を使用して加速度を取得します。
    /// </para>
    /// <para>
    /// 加速度値は、端末の座標系におけるX軸、Y軸、Z軸方向の値として取得されます。
    /// 値には重力加速度が含まれる場合があります。
    /// </para>
    /// </remarks>
    public class AccelerometerHandler : Java.Lang.Object, ISensorEventListener, IAccelerometer
    {

        private readonly SensorManager manager;
        private readonly Sensor? accelerometer;
        private bool disposed;

        float accelX;
        float accelY;
        float accelZ;

        /// <summary>
        /// <see cref="AccelerometerHandler"/>クラスの新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="context">
        /// Androidのセンサーサービスを取得するために使用するコンテキストです。
        /// </param>
        public AccelerometerHandler(Context context)
        {
            this.manager = (SensorManager)context.GetSystemService(Context.SensorService);
            if (this.manager.GetSensorList(SensorType.Accelerometer).Count != 0)
            {
                this.accelerometer = this.manager.GetSensorList(SensorType.Accelerometer)[0];
                this.manager.RegisterListener(this, this.accelerometer, SensorDelay.Game);
            }
        }

        /// <summary>
        /// センサーの精度が変更されたときに呼び出されます。
        /// </summary>
        /// <param name="sensor">
        /// 精度が変更されたセンサーです。
        /// </param>
        /// <param name="accuracy">
        /// センサーの現在の精度です。
        /// </param>
        public void OnAccuracyChanged(Sensor? sensor, [GeneratedEnum] SensorStatus accuracy)
        {

        }

        /// <summary>
        /// センサーから新しい値が取得されたときに呼び出されます。
        /// </summary>
        /// <param name="e">
        /// センサーイベントの情報です。
        /// </param>
        /// <remarks>
        /// <para>
        /// <paramref name="e"/>の値からX軸、Y軸、Z軸方向の加速度を取得して保持します。
        /// </para>
        /// <para>
        /// 加速度値はそれぞれ<see cref="AccelX"/>、<see cref="AccelY"/>、
        /// <see cref="AccelZ"/>から取得できます。
        /// </para>
        /// </remarks>
        public void OnSensorChanged(SensorEvent? e)
        {
            if (this.disposed || e == null)
                return;
            this.accelX = e.Values[0];
            this.accelY = e.Values[1];
            this.accelZ = e.Values[2];
        }

        protected override void Dispose(bool disposing)
        {
            if (this.disposed)
                return;
            this.manager.UnregisterListener(this);
            base.Dispose(disposing);
            this.disposed = true;
        }

        /// <summary>
        /// X軸方向の加速度を取得します。
        /// </summary>
        /// <value>
        /// X軸方向の加速度値です。
        /// </value>
        public float AccelX
        {
            get
            {
                return this.accelX;
            }
        }

        /// <summary>
        /// Y軸方向の加速度を取得します。
        /// </summary>
        /// <value>
        /// Y軸方向の加速度値です。
        /// </value>
        public float AccelY
        {
            get
            {
                return this.accelY;
            }
        }

        /// <summary>
        /// Z軸方向の加速度を取得します。
        /// </summary>
        /// <value>
        /// Z軸方向の加速度値です。
        /// </value>
        public float AccelZ
        {
            get
            {
                return this.accelZ;
            }
        }

        /// <summary>
        /// このオブジェクトが破棄済みかどうかを示す値を取得します。
        /// </summary>
        public bool IsDisposed
        {
            get
            {
                return this.disposed;
            }
        }
    }

}
