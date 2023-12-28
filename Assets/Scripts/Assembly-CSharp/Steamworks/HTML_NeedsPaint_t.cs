using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000146 RID: 326
	[CallbackIdentity(4502)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct HTML_NeedsPaint_t
	{
		// Token: 0x0400057D RID: 1405
		public const int k_iCallback = 4502;

		// Token: 0x0400057E RID: 1406
		public HHTMLBrowser unBrowserHandle;

		// Token: 0x0400057F RID: 1407
		public IntPtr pBGRA;

		// Token: 0x04000580 RID: 1408
		public uint unWide;

		// Token: 0x04000581 RID: 1409
		public uint unTall;

		// Token: 0x04000582 RID: 1410
		public uint unUpdateX;

		// Token: 0x04000583 RID: 1411
		public uint unUpdateY;

		// Token: 0x04000584 RID: 1412
		public uint unUpdateWide;

		// Token: 0x04000585 RID: 1413
		public uint unUpdateTall;

		// Token: 0x04000586 RID: 1414
		public uint unScrollX;

		// Token: 0x04000587 RID: 1415
		public uint unScrollY;

		// Token: 0x04000588 RID: 1416
		public float flPageScale;

		// Token: 0x04000589 RID: 1417
		public uint unPageSerial;
	}
}
