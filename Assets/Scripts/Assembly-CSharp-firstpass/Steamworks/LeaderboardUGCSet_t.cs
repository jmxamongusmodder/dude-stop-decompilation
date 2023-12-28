using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C2 RID: 450
	[CallbackIdentity(1111)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LeaderboardUGCSet_t
	{
		// Token: 0x04000756 RID: 1878
		public const int k_iCallback = 1111;

		// Token: 0x04000757 RID: 1879
		public EResult m_eResult;

		// Token: 0x04000758 RID: 1880
		public SteamLeaderboard_t m_hSteamLeaderboard;
	}
}
