using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
namespace Duality.Windows.Forms.DesktopIntegration.Interop
{
	[ComImportAttribute()]
	[GuidAttribute("ea1afb91-9e28-4b86-90e9-9e9f8a5eefaf")]
	[InterfaceTypeAttribute(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface ITaskbarList3
	{
		// ITaskbarList
		[PreserveSig]
		void HrInit();
		[PreserveSig]
		void AddTab(IntPtr hwnd);
		[PreserveSig]
		void DeleteTab(IntPtr hwnd);
		[PreserveSig]
		void ActivateTab(IntPtr hwnd);
		[PreserveSig]
		void SetActiveAlt(IntPtr hwnd);

		// ITaskbarList2
		[PreserveSig]
		void MarkFullscreenWindow(
			IntPtr hwnd,
			[MarshalAs(UnmanagedType.Bool)] bool fFullscreen);

		// ITaskbarList3
		void SetProgressValue(IntPtr hwnd, ulong ullCompleted, ulong ullTotal);
		void SetProgressState(IntPtr hwnd, TBPFLAG tbpFlags);
		void RegisterTab(IntPtr hwndTab, IntPtr hwndMDI);
		void UnregisterTab(IntPtr hwndTab);
		void SetTabOrder(IntPtr hwndTab, IntPtr hwndInsertBefore);
		void SetTabActive(IntPtr hwndTab, IntPtr hwndMDI, TBATFLAG tbatFlags);
		void ThumbBarAddButtons(
			IntPtr hwnd,
			uint cButtons,
			[MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
		void ThumbBarUpdateButtons(
			IntPtr hwnd,
			uint cButtons,
			[MarshalAs(UnmanagedType.LPArray)] THUMBBUTTON[] pButtons);
		void ThumbBarSetImageList(IntPtr hwnd, IntPtr himl);
		void SetOverlayIcon(
		  IntPtr hwnd,
		  IntPtr hIcon,
		  [MarshalAs(UnmanagedType.LPWStr)] string pszDescription);
		void SetThumbnailTooltip(
			IntPtr hwnd,
			[MarshalAs(UnmanagedType.LPWStr)] string pszTip);
		void SetThumbnailClip(
			IntPtr hwnd,
			/*[MarshalAs(UnmanagedType.LPStruct)]*/ ref RECT prcClip);
	}
}
