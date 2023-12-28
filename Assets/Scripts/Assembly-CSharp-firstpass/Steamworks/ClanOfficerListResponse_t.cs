using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000129 RID: 297
	[CallbackIdentity(335)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ClanOfficerListResponse_t
	{
		// Token: 0x04000517 RID: 1303
		public const int k_iCallback = 335;

		// Token: 0x04000518 RID: 1304
		public CSteamID m_steamIDClan;

		// Token: 0x04000519 RID: 1305
		public int m_cOfficers;

		// Token: 0x0400051A RID: 1306
		public byte m_bSuccess;
	}
}
