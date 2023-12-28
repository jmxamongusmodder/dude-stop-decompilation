using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200012B RID: 299
	[CallbackIdentity(337)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameRichPresenceJoinRequested_t
	{
		// Token: 0x0400051E RID: 1310
		public const int k_iCallback = 337;

		// Token: 0x0400051F RID: 1311
		public CSteamID m_steamIDFriend;

		// Token: 0x04000520 RID: 1312
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		public string m_rgchConnect;
	}
}
