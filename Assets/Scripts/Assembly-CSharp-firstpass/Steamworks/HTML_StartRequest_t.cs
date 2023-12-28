using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000147 RID: 327
	[CallbackIdentity(4503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_StartRequest_t
	{
		// Token: 0x0400058A RID: 1418
		public const int k_iCallback = 4503;

		// Token: 0x0400058B RID: 1419
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400058C RID: 1420
		public string pchURL;

		// Token: 0x0400058D RID: 1421
		public string pchTarget;

		// Token: 0x0400058E RID: 1422
		public string pchPostData;

		// Token: 0x0400058F RID: 1423
		[MarshalAs(UnmanagedType.I1)]
		public bool bIsRedirect;
	}
}
