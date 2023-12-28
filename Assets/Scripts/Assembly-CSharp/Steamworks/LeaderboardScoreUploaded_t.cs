using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BD RID: 445
	[CallbackIdentity(1106)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoreUploaded_t
	{
		// Token: 0x04000742 RID: 1858
		public const int k_iCallback = 1106;

		// Token: 0x04000743 RID: 1859
		public byte m_bSuccess;

		// Token: 0x04000744 RID: 1860
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x04000745 RID: 1861
		public int m_nScore;

		// Token: 0x04000746 RID: 1862
		public byte m_bScoreChanged;

		// Token: 0x04000747 RID: 1863
		public int m_nGlobalRankNew;

		// Token: 0x04000748 RID: 1864
		public int m_nGlobalRankPrevious;
	}
}
