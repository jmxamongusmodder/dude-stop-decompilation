using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000150 RID: 336
	[CallbackIdentity(4512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_VerticalScroll_t
	{
		// Token: 0x040005B2 RID: 1458
		public const int k_iCallback = 4512;

		// Token: 0x040005B3 RID: 1459
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005B4 RID: 1460
		public uint unScrollMax;

		// Token: 0x040005B5 RID: 1461
		public uint unScrollCurrent;

		// Token: 0x040005B6 RID: 1462
		public float flPageScale;

		// Token: 0x040005B7 RID: 1463
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x040005B8 RID: 1464
		public uint unPageSize;
	}
}
