using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200011D RID: 285
	[CallbackIdentity(3901)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamAppInstalled_t
	{
		// Token: 0x040004F3 RID: 1267
		public const int k_iCallback = 3901;

		// Token: 0x040004F4 RID: 1268
		public AppId_t m_nAppID;
	}
}
