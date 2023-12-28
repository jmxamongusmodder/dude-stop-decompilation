using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001BF RID: 447
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsUnloaded_t
	{
		// Token: 0x0400074C RID: 1868
		public const int k_iCallback = 1108;

		// Token: 0x0400074D RID: 1869
		public CSteamID m_steamIDUser;
	}
}
