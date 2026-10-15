using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duality.Windows.Forms.DesktopIntegration
{
	/// <summary>
	/// Represents the thumbnail progress bar state.
	/// </summary>
	public enum ThumbnailProgressState
	{
		/// <summary>
		/// No progress is displayed.
		/// </summary>
		NoProgress = 0,
		/// <summary>
		/// The progress is indeterminate (marquee).
		/// </summary>
		Indeterminate = 0x1,
		/// <summary>
		/// Normal progress is displayed.
		/// </summary>
		Normal = 0x2,
		/// <summary>
		/// An error occurred (red).
		/// </summary>
		Error = 0x4,
		/// <summary>
		/// The operation is paused (yellow).
		/// </summary>
		Paused = 0x8
	}
}
