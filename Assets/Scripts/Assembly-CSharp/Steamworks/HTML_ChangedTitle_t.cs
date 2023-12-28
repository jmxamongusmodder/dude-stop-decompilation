using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200014C RID: 332
	[CallbackIdentity(4508)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_ChangedTitle_t
	{
		// Token: 0x040005A0 RID: 1440
		public const int k_iCallback = 4508;

		// Token: 0x040005A1 RID: 1441
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005A2 RID: 1442
		public string pchTitle;
	}
}
