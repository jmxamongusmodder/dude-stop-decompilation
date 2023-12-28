using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000225 RID: 549
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardEntry_t
	{
		// Token: 0x04000C28 RID: 3112
		public CSteamID m_steamIDUser;

		// Token: 0x04000C29 RID: 3113
		public int m_nGlobalRank;

		// Token: 0x04000C2A RID: 3114
		public int m_nScore;

		// Token: 0x04000C2B RID: 3115
		public int m_cDetails;

		// Token: 0x04000C2C RID: 3116
		public UGCHandle_t m_hUGC;
	}
}
