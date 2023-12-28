using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000127 RID: 295
	[CallbackIdentity(333)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameLobbyJoinRequested_t
	{
		// Token: 0x0400050F RID: 1295
		public const int k_iCallback = 333;

		// Token: 0x04000510 RID: 1296
		public CSteamID m_steamIDLobby;

		// Token: 0x04000511 RID: 1297
		public CSteamID m_steamIDFriend;
	}
}
