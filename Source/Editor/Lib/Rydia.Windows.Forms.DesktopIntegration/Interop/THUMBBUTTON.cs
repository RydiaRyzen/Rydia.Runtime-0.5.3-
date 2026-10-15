using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Duality.Windows.Forms.DesktopIntegration.Interop
{
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
	internal struct THUMBBUTTON
	{
		[MarshalAs(UnmanagedType.U4)]
		public THBMASK dwMask;
		public uint iId;
		public uint iBitmap;
		public IntPtr hIcon;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		public string szTip;
		[MarshalAs(UnmanagedType.U4)]
		public THBFLAGS dwFlags;
	}
}
