using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000145 RID: 325
	[CallbackIdentity(4501)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_BrowserReady_t
	{
		// Token: 0x0400057B RID: 1403
		public const int k_iCallback = 4501;

		// Token: 0x0400057C RID: 1404
		public HHTMLBrowser unBrowserHandle;
	}
}
