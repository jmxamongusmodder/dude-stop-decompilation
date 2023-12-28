using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000133 RID: 307
	[CallbackIdentity(345)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsIsFollowing_t
	{
		// Token: 0x04000539 RID: 1337
		public const int k_iCallback = 345;

		// Token: 0x0400053A RID: 1338
		public EResult m_eResult;

		// Token: 0x0400053B RID: 1339
		public CSteamID m_steamID;

		// Token: 0x0400053C RID: 1340
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bIsFollowing;
	}
}
