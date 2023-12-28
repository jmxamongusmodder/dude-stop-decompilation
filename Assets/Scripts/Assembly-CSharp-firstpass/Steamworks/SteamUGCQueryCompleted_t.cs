using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019E RID: 414
	[CallbackIdentity(3401)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamUGCQueryCompleted_t
	{
		// Token: 0x040006D8 RID: 1752
		public const int k_iCallback = 3401;

		// Token: 0x040006D9 RID: 1753
		public UGCQueryHandle_t m_handle;

		// Token: 0x040006DA RID: 1754
		public EResult m_eResult;

		// Token: 0x040006DB RID: 1755
		public uint m_unNumResultsReturned;

		// Token: 0x040006DC RID: 1756
		public uint m_unTotalMatchingResults;

		// Token: 0x040006DD RID: 1757
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bCachedData;
	}
}
