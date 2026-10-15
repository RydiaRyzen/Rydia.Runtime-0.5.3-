using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duality.Windows.Forms.DesktopIntegration.Interop
{
	internal enum THBFLAGS
	{
		THBF_ENABLED = 0,
		THBF_DISABLED = 0x1,
		THBF_DISMISSONCLICK = 0x2,
		THBF_NOBACKGROUND = 0x4,
		THBF_HIDDEN = 0x8
	}
}
