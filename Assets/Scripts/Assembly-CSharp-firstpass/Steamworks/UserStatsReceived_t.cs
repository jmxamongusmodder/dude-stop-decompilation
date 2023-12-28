using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001B8 RID: 440
	[CallbackIdentity(1101)]
	[StructLayout(LayoutKind.Explicit, Pack = 8)]
	public struct UserStatsReceived_t
	{
		// Token: 0x0400072E RID: 1838
		public const int k_iCallback = 1101;

		// Token: 0x0400072F RID: 1839
		[FieldOffset(0)]
		public ulong m_nGameID;

		// Token: 0x04000730 RID: 1840
		[FieldOffset(8)]
		public EResult m_eResult;

		// Token: 0x04000731 RID: 1841
		[FieldOffset(12)]
		public CSteamID m_steamIDUser;
	}
}
