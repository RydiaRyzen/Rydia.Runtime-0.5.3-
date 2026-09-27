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

    public class AccelerometerHandler : Java.Lang.Object, ISensorEventListener, IAccelerometer
    {

        float accelX;
        float accelY;
        float accelZ;

        public AccelerometerHandler(Context context)
        {
            SensorManager manager = (SensorManager)context.GetSystemService(Context.SensorService);
            if (manager.GetSensorList(SensorType.Accelerometer).Count != 0)
            {
                Sensor accelerometer = manager.GetSensorList(SensorType.Accelerometer)[0];
                manager.RegisterListener(this, accelerometer, SensorDelay.Game);
            }
        }

        public void OnAccuracyChanged(Sensor? sensor, [GeneratedEnum] SensorStatus accuracy)
        {

        }

        public void OnSensorChanged(SensorEvent? e)
        {
            this.accelX = e.Values[0];
            this.accelY = e.Values[1];
            this.accelZ = e.Values[2];
        }

        public float AccelX
        {
            get
            {
                return this.accelX;
            }
        }

        public float AccelY
        {
            get
            {
                return this.accelY;
            }
        }

        public float AccelZ
        {
            get
            {
                return this.accelZ;
            }
        }

    }

}
