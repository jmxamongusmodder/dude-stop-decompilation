using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000148 RID: 328
	[CallbackIdentity(4504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_CloseBrowser_t
	{
		// Token: 0x04000590 RID: 1424
		public const int k_iCallback = 4504;

		// Token: 0x04000591 RID: 1425
		public HHTMLBrowser unBrowserHandle;
	}
}
