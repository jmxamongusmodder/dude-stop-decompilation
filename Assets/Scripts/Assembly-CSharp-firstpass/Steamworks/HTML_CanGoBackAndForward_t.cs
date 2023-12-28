using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200014E RID: 334
	[CallbackIdentity(4510)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CanGoBackAndForward_t
	{
		// Token: 0x040005A7 RID: 1447
		public const int k_iCallback = 4510;

		// Token: 0x040005A8 RID: 1448
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005A9 RID: 1449
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoBack;

		// Token: 0x040005AA RID: 1450
		[MarshalAs(UnmanagedType.I1)]
		public bool bCanGoForward;
	}
}
