using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000130 RID: 304
	[CallbackIdentity(342)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct JoinClanChatRoomCompletionResult_t
	{
		// Token: 0x0400052F RID: 1327
		public const int k_iCallback = 342;

		// Token: 0x04000530 RID: 1328
		public CSteamID m_steamIDClanChat;

		// Token: 0x04000531 RID: 1329
		public EChatRoomEnterResponse m_eChatRoomEnterResponse;
	}
}
