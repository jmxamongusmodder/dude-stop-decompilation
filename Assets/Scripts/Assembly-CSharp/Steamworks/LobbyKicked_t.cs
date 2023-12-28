using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200016A RID: 362
	[CallbackIdentity(512)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyKicked_t
	{
		// Token: 0x0400061F RID: 1567
		public const int k_iCallback = 512;

		// Token: 0x04000620 RID: 1568
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000621 RID: 1569
		public ulong m_ulSteamIDAdmin;

		// Token: 0x04000622 RID: 1570
		public byte m_bKickedDueToDisconnect;
	}
}
