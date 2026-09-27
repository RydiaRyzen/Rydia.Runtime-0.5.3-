using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Rydia.Runtime
{
    public class GameTime
    {
        private float _secondsPerTick;
        private long _startTicks;
        private long _lastUpdateTicks;
        private float deltaSec;
        private float totalSec;

        public GameTime()
        {
            this._secondsPerTick = 1.0f / Stopwatch.Frequency;
            this._startTicks = Stopwatch.GetTimestamp();
            this._lastUpdateTicks = this._startTicks;
        }

        public void Update()
        {
            long ticks = Stopwatch.GetTimestamp();
            this.deltaSec = (ticks - this._lastUpdateTicks) * this._secondsPerTick;
            this.totalSec = (ticks - this._startTicks) * this._secondsPerTick;
            this._lastUpdateTicks = ticks;
        }

        public float DeltaSec
        {
            get
            {
                return this.deltaSec;
            }
        }

        public float TotalSec
        {
            get
            {
                return this.totalSec;
            }
        }

    }
}
