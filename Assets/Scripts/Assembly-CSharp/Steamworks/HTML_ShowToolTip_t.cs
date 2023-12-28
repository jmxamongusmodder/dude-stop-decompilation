using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000158 RID: 344
	[CallbackIdentity(4524)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ShowToolTip_t
	{
		// Token: 0x040005D8 RID: 1496
		public const int k_iCallback = 4524;

		// Token: 0x040005D9 RID: 1497
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005DA RID: 1498
		public string pchMsg;
	}
}
