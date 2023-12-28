using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200011E RID: 286
	[CallbackIdentity(3902)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAppUninstalled_t
	{
		// Token: 0x040004F5 RID: 1269
		public const int k_iCallback = 3902;

		// Token: 0x040004F6 RID: 1270
		public AppId_t m_nAppID;
	}
}
