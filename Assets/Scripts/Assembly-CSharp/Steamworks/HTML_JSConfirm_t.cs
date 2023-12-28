using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000153 RID: 339
	[CallbackIdentity(4515)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_JSConfirm_t
	{
		// Token: 0x040005C3 RID: 1475
		public const int k_iCallback = 4515;

		// Token: 0x040005C4 RID: 1476
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005C5 RID: 1477
		public string pchMessage;
	}
}
