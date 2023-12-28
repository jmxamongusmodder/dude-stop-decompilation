using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000151 RID: 337
	[CallbackIdentity(4513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_LinkAtPosition_t
	{
		// Token: 0x040005B9 RID: 1465
		public const int k_iCallback = 4513;

		// Token: 0x040005BA RID: 1466
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005BB RID: 1467
		public uint x;

		// Token: 0x040005BC RID: 1468
		public uint y;

		// Token: 0x040005BD RID: 1469
		public string pchURL;

		// Token: 0x040005BE RID: 1470
		[MarshalAs(UnmanagedType.I1)]
		public bool bInput;

		// Token: 0x040005BF RID: 1471
		[MarshalAs(UnmanagedType.I1)]
		public bool bLiveLink;
	}
}
