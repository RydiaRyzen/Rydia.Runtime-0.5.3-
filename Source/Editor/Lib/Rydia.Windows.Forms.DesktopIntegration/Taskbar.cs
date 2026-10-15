using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Duality.Windows.Forms.DesktopIntegration.Interop;
using System.Drawing;


namespace Duality.Windows.Forms.DesktopIntegration
{
	public class Taskbar
	{

		private static ITaskbarList3 _taskbarList;

		internal static ITaskbarList3 TaskbarList
		{
			get
			{
				if (_taskbarList == null)
				{
					lock (typeof(Taskbar))
					{
						if (_taskbarList == null)
						{
							_taskbarList = (ITaskbarList3)new CTaskbarList();
							_taskbarList.HrInit();
						}
					}
				}
				return _taskbarList;
			}
		}

		/// <summary>
		/// Draws the specified overlay icon on the specified window's
		/// taskbar button.
		/// </summary>
		/// <param name="hwnd">The window handle.</param>
		/// <param name="icon">The overlay icon.</param>
		/// <param name="description">The overlay icon description.</param>
		public static void SetTaskbarOverlayIcon(IntPtr hwnd, Icon icon, string description)
		{
			TaskbarList.SetOverlayIcon(
				hwnd,
				icon == null ? IntPtr.Zero : icon.Handle,
				description);
		}

		/// <summary>
		/// Sets the progress state of the specified window's
		/// taskbar button.
		/// </summary>
		/// <param name="hwnd">The window handle.</param>
		/// <param name="state">The progress state.</param>
		public static void SetProgressState(IntPtr hwnd,
			ThumbnailProgressState state)
		{
			TaskbarList.SetProgressState(hwnd, (TBPFLAG)state);
		}

		/// <summary>
		/// Sets the progress value of the specified window's
		/// taskbar button.
		/// </summary>
		/// <param name="hwnd">The window handle.</param>
		/// <param name="current">The current value.</param>
		/// <param name="maximum">The maximum value.</param>
		public static void SetProgressValue(IntPtr hwnd,
			ulong current, ulong maximum)
		{
			TaskbarList.SetProgressValue(hwnd, current, maximum);
		}


	}
}
