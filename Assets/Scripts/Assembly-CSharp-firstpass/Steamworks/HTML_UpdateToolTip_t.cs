using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000159 RID: 345
	[CallbackIdentity(4525)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_UpdateToolTip_t
	{
		// Token: 0x040005DB RID: 1499
		public const int k_iCallback = 4525;

		// Token: 0x040005DC RID: 1500
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005DD RID: 1501
		public string pchMsg;
	}
}
