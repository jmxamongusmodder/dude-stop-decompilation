using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AD RID: 429
	[CallbackIdentity(102)]
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServerConnectFailure_t
	{
		// Token: 0x0400070F RID: 1807
		public const int k_iCallback = 102;

		// Token: 0x04000710 RID: 1808
		public EResult m_eResult;

		// Token: 0x04000711 RID: 1809
		[MarshalAs(UnmanagedType.I1)]
		public bool m_bStillRetrying;
	}
}
