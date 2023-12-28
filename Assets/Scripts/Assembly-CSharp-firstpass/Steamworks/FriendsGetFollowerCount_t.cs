using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000132 RID: 306
	[CallbackIdentity(344)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendsGetFollowerCount_t
	{
		// Token: 0x04000535 RID: 1333
		public const int k_iCallback = 344;

		// Token: 0x04000536 RID: 1334
		public EResult m_eResult;

		// Token: 0x04000537 RID: 1335
		public CSteamID m_steamID;

		// Token: 0x04000538 RID: 1336
		public int m_nCount;
	}
}
