using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000164 RID: 356
	[CallbackIdentity(504)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LobbyEnter_t
	{
		// Token: 0x04000605 RID: 1541
		public const int k_iCallback = 504;

		// Token: 0x04000606 RID: 1542
		public ulong m_ulSteamIDLobby;

		// Token: 0x04000607 RID: 1543
		public uint m_rgfChatPermissions;

		// Token: 0x04000608 RID: 1544
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bLocked;

		// Token: 0x04000609 RID: 1545
		public uint m_EChatRoomEnterResponse;
	}
}
