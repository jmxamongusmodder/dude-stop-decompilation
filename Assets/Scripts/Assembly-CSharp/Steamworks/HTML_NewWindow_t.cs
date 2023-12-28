using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000155 RID: 341
	[CallbackIdentity(4521)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NewWindow_t
	{
		// Token: 0x040005CA RID: 1482
		public const int k_iCallback = 4521;

		// Token: 0x040005CB RID: 1483
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005CC RID: 1484
		public string pchURL;

		// Token: 0x040005CD RID: 1485
		public uint unX;

		// Token: 0x040005CE RID: 1486
		public uint unY;

		// Token: 0x040005CF RID: 1487
		public uint unWide;

		// Token: 0x040005D0 RID: 1488
		public uint unTall;

		// Token: 0x040005D1 RID: 1489
		public HHTMLBrowser unNewWindow_BrowserHandle;
	}
}
