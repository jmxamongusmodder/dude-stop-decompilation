using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200014B RID: 331
	[CallbackIdentity(4507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_OpenLinkInNewTab_t
	{
		// Token: 0x0400059D RID: 1437
		public const int k_iCallback = 4507;

		// Token: 0x0400059E RID: 1438
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400059F RID: 1439
		public string pchURL;
	}
}
