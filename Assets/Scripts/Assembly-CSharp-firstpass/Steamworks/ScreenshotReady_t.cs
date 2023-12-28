using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019C RID: 412
	[CallbackIdentity(2301)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ScreenshotReady_t
	{
		// Token: 0x040006D4 RID: 1748
		public const int k_iCallback = 2301;

		// Token: 0x040006D5 RID: 1749
		public ScreenshotHandle m_hLocal;

		// Token: 0x040006D6 RID: 1750
		public EResult m_eResult;
	}
}
