using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duality.Windows.Forms.DesktopIntegration.Interop
{
	internal enum TBPFLAG
	{
		TBPF_NOPROGRESS = 0,
		TBPF_INDETERMINATE = 0x1,
		TBPF_NORMAL = 0x2,
		TBPF_ERROR = 0x4,
		TBPF_PAUSED = 0x8
	}
}
