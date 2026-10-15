using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;


namespace Duality.Windows.Forms.DesktopIntegration
{
	public static class WindowsFormsExtensions
	{

		/// <summary>
		/// Draws the specified overlay icon over this form's taskbar button.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <param name="icon">The overlay icon.</param>
		/// <param name="description">The overlay icon's description.</param>
		public static void SetTaskbarOverlayIcon(this Form form, Icon icon, string description)
		{
			Taskbar.SetTaskbarOverlayIcon(form.Handle, icon, description);
		}

		/// <summary>
		/// Sets the progress bar in the containing form's taskbar button
		/// to this progress bar's progress.
		/// </summary>
		/// <param name="progressBar">The progress bar.</param>
		public static void SetTaskbarProgress(this ProgressBar progressBar)
		{
			Form form = (Form)progressBar.TopLevelControl;

			//Approximation:
			ulong maximum = (ulong)(progressBar.Maximum - progressBar.Minimum);
			ulong progress = (ulong)(progressBar.Value - progressBar.Minimum);

			Taskbar.SetProgressState(
				form.Handle, ThumbnailProgressState.Normal);
			Taskbar.SetProgressValue(
				form.Handle, progress, maximum);
		}

		/// <summary>
		/// Sets the progress bar in the containing form's taskbar button
		/// to this toolstrip progress bar's progress.
		/// </summary>
		/// <param name="progressBar">The progress bar.</param>
		public static void SetTaskbarProgress(this ToolStripProgressBar progressBar)
		{
			Form form = (Form)progressBar.Control.TopLevelControl;

			//Approximation:
			ulong maximum = (ulong)(progressBar.Maximum - progressBar.Minimum);
			ulong progress = (ulong)(progressBar.Value - progressBar.Minimum);

			Taskbar.SetProgressState(
				form.Handle, ThumbnailProgressState.Normal);
			Taskbar.SetProgressValue(
				form.Handle, progress, maximum);
		}

		/// <summary>
		/// Sets the progress bar of this form's taskbar button to the
		/// specified percentage.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <param name="percent">The progress percentage.</param>
		public static void SetTaskbarProgress(this Form form, float percent)
		{
			Taskbar.SetProgressState(
				form.Handle, ThumbnailProgressState.Normal);
			Taskbar.SetProgressValue(
				form.Handle, (ulong)percent, 100);
		}

		/// <summary>
		/// Sets the progress bar of this form's taskbar button to the
		/// specified state.
		/// </summary>
		/// <param name="form">The form.</param>
		/// <param name="state">The taskbar progress state.</param>
		public static void SetTaskbarProgressState(this Form form, ThumbnailProgressState state)
		{
			Taskbar.SetProgressState(form.Handle, state);
		}

	}
}
