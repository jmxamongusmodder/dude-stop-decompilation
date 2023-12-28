using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000165 RID: 357
	[CallbackIdentity(505)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyDataUpdate_t
	{
		// Token: 0x0400060A RID: 1546
		public const int k_iCallback = 505;

		// Token: 0x0400060B RID: 1547
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400060C RID: 1548
		public ulong m_ulSteamIDMember;

		// Token: 0x0400060D RID: 1549
		public byte m_bSuccess;
	}
}
