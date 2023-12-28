using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000154 RID: 340
	[CallbackIdentity(4516)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_FileOpenDialog_t
	{
		// Token: 0x040005C6 RID: 1478
		public const int k_iCallback = 4516;

		// Token: 0x040005C7 RID: 1479
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005C8 RID: 1480
		public string pchTitle;

		// Token: 0x040005C9 RID: 1481
		public string pchInitialFile;
	}
}
