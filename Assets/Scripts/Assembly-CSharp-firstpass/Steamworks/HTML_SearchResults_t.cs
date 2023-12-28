using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200014D RID: 333
	[CallbackIdentity(4509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_SearchResults_t
	{
		// Token: 0x040005A3 RID: 1443
		public const int k_iCallback = 4509;

		// Token: 0x040005A4 RID: 1444
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005A5 RID: 1445
		public uint unResults;

		// Token: 0x040005A6 RID: 1446
		public uint unCurrentMatch;
	}
}
