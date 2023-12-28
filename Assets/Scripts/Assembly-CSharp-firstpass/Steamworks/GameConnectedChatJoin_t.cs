using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200012D RID: 301
	[CallbackIdentity(339)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameConnectedChatJoin_t
	{
		// Token: 0x04000525 RID: 1317
		public const int k_iCallback = 339;

		// Token: 0x04000526 RID: 1318
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000527 RID: 1319
		public CSteamID m_steamIDUser;
	}
}
