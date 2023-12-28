using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x02000144 RID: 324
	[CallbackIdentity(1108)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsUnloaded_t
	{
		// Token: 0x04000579 RID: 1401
		public const int k_iCallback = 1108;

		// Token: 0x0400057A RID: 1402
		public CSteamID m_steamIDUser;
	}
}
