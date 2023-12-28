using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001C0 RID: 448
	[CallbackIdentity(1109)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementIconFetched_t
	{
		// Token: 0x0400074E RID: 1870
		public const int k_iCallback = 1109;

		// Token: 0x0400074F RID: 1871
		public CGameID m_nGameID;

		// Token: 0x04000750 RID: 1872
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchAchievementName;

		// Token: 0x04000751 RID: 1873
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bAchieved;

		// Token: 0x04000752 RID: 1874
		public int m_nIconHandle;
	}
}
