using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000157 RID: 343
	[CallbackIdentity(4523)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StatusText_t
	{
		// Token: 0x040005D5 RID: 1493
		public const int k_iCallback = 4523;

		// Token: 0x040005D6 RID: 1494
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005D7 RID: 1495
		public string pchMsg;
	}
}
