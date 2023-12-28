using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000131 RID: 305
	[CallbackIdentity(343)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct GameConnectedFriendChatMsg_t
	{
		// Token: 0x04000532 RID: 1330
		public const int k_iCallback = 343;

		// Token: 0x04000533 RID: 1331
		public CSteamID m_steamIDUser;

		// Token: 0x04000534 RID: 1332
		public int m_iMessageID;
	}
}
