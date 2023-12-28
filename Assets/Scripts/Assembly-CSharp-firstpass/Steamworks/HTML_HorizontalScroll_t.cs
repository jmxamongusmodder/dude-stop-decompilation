using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200014F RID: 335
	[CallbackIdentity(4511)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_HorizontalScroll_t
	{
		// Token: 0x040005AB RID: 1451
		public const int k_iCallback = 4511;

		// Token: 0x040005AC RID: 1452
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x040005AD RID: 1453
		public uint unScrollMax;

		// Token: 0x040005AE RID: 1454
		public uint unScrollCurrent;

		// Token: 0x040005AF RID: 1455
		public float flPageScale;

		// Token: 0x040005B0 RID: 1456
		[MarshalAs(UnmanagedType.I1)]
		public bool bVisible;

		// Token: 0x040005B1 RID: 1457
		public uint unPageSize;
	}
}
