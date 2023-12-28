using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000166 RID: 358
	[CallbackIdentity(506)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyChatUpdate_t
	{
		// Token: 0x0400060E RID: 1550
		public const int k_iCallback = 506;

		// Token: 0x0400060F RID: 1551
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000610 RID: 1552
		public ulong m_ulSteamIDUserChanged;

		// Token: 0x04000611 RID: 1553
		public ulong m_ulSteamIDMakingChange;

		// Token: 0x04000612 RID: 1554
		public uint m_rgfChatMemberStateChange;
	}
}
