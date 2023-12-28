using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000152 RID: 338
	[CallbackIdentity(4514)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSAlert_t
	{
		// Token: 0x040005C0 RID: 1472
		public const int k_iCallback = 4514;

		// Token: 0x040005C1 RID: 1473
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005C2 RID: 1474
		public string pchMessage;
	}
}
