using System;
using System.Collections.Generic;
using System.Text;

namespace Rydia.Runtime.Core
{

    public class AppTime
    {

        /// <summary>
		/// The amount of frame per second at the desired refresh rate of 60 FPS.
		/// </summary>
		public const float FramesPerSecond = 60.0f;
        /// <summary>
        /// Milliseconds a frame takes at the desired refresh rate of 60 FPS
        /// </summary>
        public const float MillisecondsPerFrame = 1000.0f / FramesPerSecond;
        /// <summary>
        /// Seconds a frame takes at the desired refresh rate of 60 FPS
        /// </summary>
        public const float SecondsPerFrame = 1.0f / FramesPerSecond;

    }

}
