using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000167 RID: 359
	[CallbackIdentity(507)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatMsg_t
	{
		// Token: 0x04000613 RID: 1555
		public const int k_iCallback = 507;

		// Token: 0x04000614 RID: 1556
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000615 RID: 1557
		public ulong m_ulSteamIDUser;

		// Token: 0x04000616 RID: 1558
		public byte m_eChatEntryType;

		// Token: 0x04000617 RID: 1559
		public uint m_iChatID;
	}
}
