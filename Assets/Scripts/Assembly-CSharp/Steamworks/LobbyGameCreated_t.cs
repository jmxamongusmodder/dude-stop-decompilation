using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000168 RID: 360
	[CallbackIdentity(509)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyGameCreated_t
	{
		// Token: 0x04000618 RID: 1560
		public const int k_iCallback = 509;

		// Token: 0x04000619 RID: 1561
		public ulong m_ulSteamIDLobby;

		// Token: 0x0400061A RID: 1562
		public ulong m_ulSteamIDGameServer;

		// Token: 0x0400061B RID: 1563
		public uint m_unIP;

		// Token: 0x0400061C RID: 1564
		public ushort m_usPort;
	}
}
