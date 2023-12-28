using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000156 RID: 342
	[CallbackIdentity(4522)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SetCursor_t
	{
		// Token: 0x040005D2 RID: 1490
		public const int k_iCallback = 4522;

		// Token: 0x040005D3 RID: 1491
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005D4 RID: 1492
		public uint eMouseCursor;
	}
}
