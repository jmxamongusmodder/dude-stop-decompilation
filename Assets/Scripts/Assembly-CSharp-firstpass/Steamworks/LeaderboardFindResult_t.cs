using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BB RID: 443
	[CallbackIdentity(1104)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardFindResult_t
	{
		// Token: 0x0400073B RID: 1851
		public const int k_iCallback = 1104;

		// Token: 0x0400073C RID: 1852
		public SteamLeaderboard_t m_hSteamLeaderboard;

		// Token: 0x0400073D RID: 1853
		public byte m_bLeaderboardFound;
	}
}
