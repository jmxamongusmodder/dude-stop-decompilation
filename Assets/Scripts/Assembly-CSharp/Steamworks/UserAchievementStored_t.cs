using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BA RID: 442
	[CallbackIdentity(1103)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementStored_t
	{
		// Token: 0x04000735 RID: 1845
		public const int k_iCallback = 1103;

		// Token: 0x04000736 RID: 1846
		public ulong m_nGameID;

		// Token: 0x04000737 RID: 1847
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bGroupAchievement;

		// Token: 0x04000738 RID: 1848
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		public string m_rgchAchievementName;

		// Token: 0x04000739 RID: 1849
		public uint m_nCurProgress;

		// Token: 0x0400073A RID: 1850
		public uint m_nMaxProgress;
	}
}
