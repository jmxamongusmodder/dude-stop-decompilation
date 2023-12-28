using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200015A RID: 346
	[CallbackIdentity(4526)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HideToolTip_t
	{
		// Token: 0x040005DE RID: 1502
		public const int k_iCallback = 4526;

		// Token: 0x040005DF RID: 1503
		public HHTMLBrowser unBrowserHandle;
	}
}
