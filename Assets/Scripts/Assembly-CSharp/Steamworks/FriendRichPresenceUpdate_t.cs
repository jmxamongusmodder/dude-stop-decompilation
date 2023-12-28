using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200012A RID: 298
	[CallbackIdentity(336)]
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	public struct FriendRichPresenceUpdate_t
	{
		// Token: 0x0400051B RID: 1307
		public const int k_iCallback = 336;

		// Token: 0x0400051C RID: 1308
		public CSteamID m_steamIDFriend;

		// Token: 0x0400051D RID: 1309
		public AppId_t m_nAppID;
	}
}
