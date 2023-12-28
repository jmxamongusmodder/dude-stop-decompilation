using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200012C RID: 300
	[CallbackIdentity(338)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedClanChatMsg_t
	{
		// Token: 0x04000521 RID: 1313
		public const int k_iCallback = 338;

		// Token: 0x04000522 RID: 1314
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000523 RID: 1315
		public CSteamID m_steamIDUser;

		// Token: 0x04000524 RID: 1316
		public int m_iMessageID;
	}
}
