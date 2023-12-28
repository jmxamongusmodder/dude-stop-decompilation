using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200021E RID: 542
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct FriendGameInfo_t
	{
		// Token: 0x04000BF5 RID: 3061
		public CGameID m_gameID;

		// Token: 0x04000BF6 RID: 3062
		public uint m_unGameIP;

		// Token: 0x04000BF7 RID: 3063
		public ushort m_usGamePort;

		// Token: 0x04000BF8 RID: 3064
		public ushort m_usQueryPort;

		// Token: 0x04000BF9 RID: 3065
		public CSteamID m_steamIDLobby;
	}
}
