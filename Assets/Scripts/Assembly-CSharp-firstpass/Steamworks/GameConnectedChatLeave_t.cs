using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200012E RID: 302
	[CallbackIdentity(340)]
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct GameConnectedChatLeave_t
	{
		// Token: 0x04000528 RID: 1320
		public const int k_iCallback = 340;

		// Token: 0x04000529 RID: 1321
		public CSteamID m_steamIDClanChat;

		// Token: 0x0400052A RID: 1322
		public CSteamID m_steamIDUser;

		// Token: 0x0400052B RID: 1323
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bKicked;

		// Token: 0x0400052C RID: 1324
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bDropped;
	}
}
