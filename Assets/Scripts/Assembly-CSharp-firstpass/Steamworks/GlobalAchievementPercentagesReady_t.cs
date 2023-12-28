using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C1 RID: 449
	[CallbackIdentity(1110)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GlobalAchievementPercentagesReady_t
	{
		// Token: 0x04000753 RID: 1875
		public const int k_iCallback = 1110;

		// Token: 0x04000754 RID: 1876
		public ulong m_nGameID;

		// Token: 0x04000755 RID: 1877
		public EResult m_eResult;
	}
}
