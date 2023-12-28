using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200013B RID: 315
	[CallbackIdentity(206)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientAchievementStatus_t
	{
		// Token: 0x04000553 RID: 1363
		public const int k_iCallback = 206;

		// Token: 0x04000554 RID: 1364
		public ulong m_SteamID;

		// Token: 0x04000555 RID: 1365
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_pchAchievement;

		// Token: 0x04000556 RID: 1366
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bUnlocked;
	}
}
