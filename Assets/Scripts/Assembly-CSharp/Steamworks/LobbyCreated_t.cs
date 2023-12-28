using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016B RID: 363
	[CallbackIdentity(513)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyCreated_t
	{
		// Token: 0x04000623 RID: 1571
		public const int k_iCallback = 513;

		// Token: 0x04000624 RID: 1572
		public EResult m_eResult;

		// Token: 0x04000625 RID: 1573
		public ulong m_ulSteamIDLobby;
	}
}
