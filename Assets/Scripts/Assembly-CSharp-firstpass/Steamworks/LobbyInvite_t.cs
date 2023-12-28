using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000163 RID: 355
	[CallbackIdentity(503)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyInvite_t
	{
		// Token: 0x04000601 RID: 1537
		public const int k_iCallback = 503;

		// Token: 0x04000602 RID: 1538
		public ulong m_ulSteamIDUser;

		// Token: 0x04000603 RID: 1539
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000604 RID: 1540
		public ulong m_ulGameID;
	}
}
