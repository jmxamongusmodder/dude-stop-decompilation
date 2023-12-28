using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000149 RID: 329
	[CallbackIdentity(4505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_URLChanged_t
	{
		// Token: 0x04000592 RID: 1426
		public const int k_iCallback = 4505;

		// Token: 0x04000593 RID: 1427
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x04000594 RID: 1428
		public string pchURL;

		// Token: 0x04000595 RID: 1429
		public string pchPostData;

		// Token: 0x04000596 RID: 1430
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;

		// Token: 0x04000597 RID: 1431
		public string pchPageTitle;

		// Token: 0x04000598 RID: 1432
		[MarshalAs(UnmanagedType.I1)]
		public bool bNewNavigation;
	}
}
