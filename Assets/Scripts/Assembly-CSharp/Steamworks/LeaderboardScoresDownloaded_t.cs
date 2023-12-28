using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BC RID: 444
	[CallbackIdentity(1105)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardScoresDownloaded_t
	{
		// Token: 0x0400073E RID: 1854
		public const int k_iCallback = 1105;

		// Token: 0x0400073F RID: 1855
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x04000740 RID: 1856
		public SteamLeaderboardEntries_t m_hSteamLeaderboardEntries;

		// Token: 0x04000741 RID: 1857
		public int m_cEntryCount;
	}
}
