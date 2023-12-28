using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200014A RID: 330
	[CallbackIdentity(4506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FinishedRequest_t
	{
		// Token: 0x04000599 RID: 1433
		public const int k_iCallback = 4506;

		// Token: 0x0400059A RID: 1434
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400059B RID: 1435
		public string pchURL;

		// Token: 0x0400059C RID: 1436
		public string pchPageTitle;
	}
}
